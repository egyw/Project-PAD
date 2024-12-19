using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace POS
{
    public partial class FormModifier : Form
    {
        int idUser;
        private string connectionString = "Server=localhost;Database=pos_aw;UserID=root;Password=;";
        private Dictionary<int, int> modifierQuantities = new Dictionary<int, int>();
        public List<string> modif { get; set; }
        public List<decimal> modifprice { get; set; }

        public FormModifier(int id, string namamenu, string productType)
        {
            InitializeComponent();
            this.idUser = id;
            label5.Text = namamenu;
            listView1.Columns.Add("Modifiers", 200);
            listView1.Columns.Add("Quantity", 80);
            listView1.Columns.Add("Price", 100);
            listView1.View = View.Details;
            listView1.Font = new Font(listView1.Font.FontFamily, 10, listView1.Font.Style);
            LoadModifiers(productType);
        }

        private void ToggleModifier(Button button)
        {
            var modifierInfo = button.Tag as dynamic;
            if (modifierInfo != null)
            {
                int modifierId = modifierInfo.ModifierId;
                string modifierText = button.Text.Split('\n')[0];

                if (modifierQuantities.ContainsKey(modifierId))
                {
                    hapusmodifier(modifierText, modifierId);
                }
                else
                {
                    AddModifier(modifierText, modifierInfo);
                }

                UpdateButtonAppearance(button);
            }
        }

        private void AddModifier(string modifierText, dynamic modifierInfo)
        {
            int modifierId = modifierInfo.ModifierId;
            decimal price = modifierInfo.Price;

            modifierQuantities[modifierId] = 1;

            UpdateListViewWithModifiers();
            UpdateModifierLabel();
        }

        private void hapusmodifier(string modifierText, int modifierId)
        {
            modifierQuantities.Remove(modifierId);

            UpdateListViewWithModifiers();
            UpdateModifierLabel();

        }

        private void UpdateListViewWithModifiers()
        {
            listView1.Items.Clear();

            foreach (var modifier in modifierQuantities)
            {
                Button modifierButton = this.Controls.Find($"modifier_{modifier.Key}", true)[0] as Button;
                if (modifierButton != null)
                {
                    string modifierText = modifierButton.Text.Split('\n')[0];
                    int quantity = modifier.Value;
                    decimal price = ((dynamic)modifierButton.Tag).Price;

                    ListViewItem item = new ListViewItem(modifierText);
                    item.SubItems.Add(quantity.ToString());
                    item.SubItems.Add((price * quantity).ToString());
                    listView1.Items.Add(item);
                }
            }
            UpdatePrices();
        }


        private void UpdateModifierLabel()
        {
            label4.Text = "";

            foreach (var modifier in modifierQuantities)
            {
                Button modifierButton = this.Controls.Find($"modifier_{modifier.Key}", true)[0] as Button;
                if (modifierButton != null)
                {
                    string modifierText = modifierButton.Text.Split('\n')[0];
                    int quantity = modifier.Value;

                    label4.Text += quantity > 1
                        ? $"{modifierText} x{quantity}, "
                        : $"{modifierText}, ";
                }
            }

            label4.Text = label4.Text.TrimEnd(',', ' ');

            if (string.IsNullOrWhiteSpace(label4.Text))
            {
                label4.Text = "None";
            }
        }

        private void UpdateButtonAppearance(Button button)
        {
            int modifierId = ((dynamic)button.Tag).ModifierId;
            decimal price = ((dynamic)button.Tag).Price;

            if (modifierQuantities.ContainsKey(modifierId))
            {
                int quantity = modifierQuantities[modifierId];
                button.BackColor = quantity > 1 ? Color.LightBlue : Color.LightGreen;
                button.Text = $"{button.Text.Split('\n')[0]}\n(x{quantity})";
            }
            else
            {
                button.BackColor = Color.White;
                button.Text = $"{button.Text.Split('\n')[0]}\n(+{price:C})";
            }
        }

        private void LoadModifiers(string productType)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"SELECT m.modifier_id, m.modifier_name, m.modifier_price 
                                    FROM modifiers m
                                    JOIN product_type_modifiers ptm ON m.modifier_id = ptm.modifier_id
                                    WHERE ptm.product_type = @ProductType AND m.is_active = TRUE";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ProductType", productType);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            ClearModifierButtons();

                            DataTable modifierTable = new DataTable();
                            modifierTable.Load(reader);

                            int buttonsPerRow = 4;
                            int totalButtons = modifierTable.Rows.Count;
                            int rows = (int)Math.Ceiling((double)totalButtons / buttonsPerRow);

                            int btnWidth = 183;
                            int btnHeight = 74;
                            int spacing = 10;

                            int panelWidth = (btnWidth * buttonsPerRow) + (spacing * (buttonsPerRow + 1));
                            int panelHeight = (btnHeight * rows) + (spacing * (rows + 1));

                            Panel modifierPanel = new Panel
                            {
                                Name = "modifierPanel",
                                Location = new Point(330, 100),
                                Size = new Size(panelWidth, panelHeight),
                                AutoScroll = true
                            };
                            this.Controls.Add(modifierPanel);

                            int x = spacing;
                            int y = spacing;
                            int count = 0;

                            foreach (DataRow row in modifierTable.Rows)
                            {
                                int modifierId = Convert.ToInt32(row["modifier_id"]);
                                string namamodifier = row["modifier_name"].ToString();
                                decimal hargamodifier = Convert.ToDecimal(row["modifier_price"]);

                                Button modifierButton = new Button
                                {
                                    Text = $"{namamodifier}\n(+{hargamodifier:C})",
                                    Name = $"modifier_{modifierId}",
                                    Size = new Size(btnWidth, btnHeight),
                                    Location = new Point(x, y),
                                    BackColor = Color.White,
                                    FlatStyle = FlatStyle.Flat,
                                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                                    Tag = new { ModifierId = modifierId, Price = hargamodifier }
                                };

                                modifierButton.Click += (s, e) => ToggleModifier((Button)s);
                                modifierButton.FlatAppearance.BorderSize = 1;
                                modifierButton.FlatAppearance.BorderColor = Color.Black;

                                modifierPanel.Controls.Add(modifierButton);

                                count++;
                                if (count % buttonsPerRow == 0)
                                {
                                    y += modifierButton.Height + spacing;
                                    x = spacing;
                                }
                                else
                                {
                                    x += modifierButton.Width + spacing;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading modifiers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearModifierButtons()
        {
            var existingPanel = this.Controls.Find("modifierPanel", true);
            if (existingPanel.Length > 0)
            {
                this.Controls.Remove(existingPanel[0]);
                existingPanel[0].Dispose();
            }
        }

        private void UpdatePrices()
        {
            decimal subtotal = 0;

            foreach (ListViewItem item in listView1.Items)
            {
                if (decimal.TryParse(item.SubItems[2].Text.Replace(",", "").Replace(".", ","), out decimal price))
                {
                    subtotal += price;
                }
            }
            subtotal = subtotal / 100;

            labelSubtotal.Text = $"$. {subtotal:N2}".Replace(".", ",");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            FormMain balik = new FormMain(idUser);
            this.Hide();
            balik.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Add Extra Menu!");
            }
            else
            {
                FormPayment bayar = new FormPayment();
                this.Hide();
                bayar.ShowDialog();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> selectedItems = new List<string>();
            List<decimal> selectedItemsPrice = new List<decimal>();

            foreach (ListViewItem item in listView1.Items)
            {
                string modifier = item.SubItems[0].Text;
                decimal price = decimal.Parse(item.SubItems[2].Text.Replace(",", "").Replace(".", ","));
                selectedItems.Add(modifier);
                selectedItemsPrice.Add(price);
            }

            modif = selectedItems;
            modifprice = selectedItemsPrice;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

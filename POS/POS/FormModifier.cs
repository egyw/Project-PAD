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
        private Dictionary<string, List<string>> menumodifierkhusus = new Dictionary<string, List<string>>();
        private string currentProductName;
        private static Dictionary<string, List<string>> appliedModifiers = new Dictionary<string, List<string>>();
        public List<string> modif { get; set; }
        public List<int> modifierId { get; set; }
        public List<decimal> modifprice { get; set; }

        public FormModifier(int id, string namamenu, string productType)
        {
            InitializeComponent();
            this.idUser = id;
            this.currentProductName = namamenu.Split('x')[0].Trim();
            label5.Text = namamenu;

            InitializeMutuallyExclusiveModifiers();
            InitializeListView();
            LoadModifiers(productType);
            LoadPreviousModifiers(namamenu);
        }

        private void InitializeListView()
        {
            listView1.Columns.Add("Modifiers", 200);
            listView1.Columns.Add("Quantity", 80);
            listView1.Columns.Add("Price", 100);
            listView1.View = View.Details;
            listView1.Font = new Font(listView1.Font.FontFamily, 10, listView1.Font.Style);
        }

        private void InitializeMutuallyExclusiveModifiers()
        {
            //pengecekan modifier yang mau di batasi
            menumodifierkhusus["Temperaturenya"] = new List<string> { "No Ice", "Less Ice", "Extra Ice" };
            menumodifierkhusus["Sizenya"] = new List<string> { "Small Size", "Regular Size", "Large Size" };
            menumodifierkhusus["Sizenya2"] = new List<string> { "Small Size Food", "Regular Size Food", "Large Size Food" };
            menumodifierkhusus["Sizenya3"] = new List<string> { "Small Size Drink", "Regular Size Drink", "Large Size Drink" };
            menumodifierkhusus["Sizenya4"] = new List<string> { "Small Size Desserts", "Regular Size Desserts", "Large Size Desserts" };
            menumodifierkhusus["Sugarnya"] = new List<string> { "No Sugar", "Less Sugar", "Extra Sugar" };
            menumodifierkhusus["Floatnya"] = new List<string> { "No Float", "Less Float", "Extra Float" };
            menumodifierkhusus["Aromanya"] = new List<string> { "No Aroma", "Golden Aroma", "Spicy Aroma" };
            menumodifierkhusus["Sayurnya"] = new List<string> { "No Vegetables", "Less Vegetables", "Extra Vegetable" };
            menumodifierkhusus["Cheesenya"] = new List<string> { "No Cheese", "Less Cheese", "Extra Cheese" };
            menumodifierkhusus["Saucenya"] = new List<string> { "No Sauce", "Less Sauce", "Extra Sauce" };
            menumodifierkhusus["Meatnya"] = new List<string> { "No Meat", "Less Meat", "Double Meat" };
            menumodifierkhusus["Toppingnya"] = new List<string> { "No Topping", "Extra Chocolate Topping", "Extra Strawberry Topping" };
            menumodifierkhusus["Chickennya"] = new List<string> { "Breast", "Wing", "Drumstick" };
        }

        private void LoadPreviousModifiers(string namamenu)
        {
            string productName = namamenu.Split('x')[0].Trim();
            if (appliedModifiers.ContainsKey(productName))
            {
                foreach (string modifier in appliedModifiers[productName])
                {
                    var modifierPanel = this.Controls.Find("modifierPanel", true)[0];
                    foreach (Control control in modifierPanel.Controls)
                    {
                        if (control is Button btn && btn.Text.Split('\n')[0] == modifier)
                        {
                            ToggleModifier(btn);
                            break;
                        }
                    }
                }
            }
        }

        private bool pengecekanmodifier(string newModifier)
        {
            foreach (var group in menumodifierkhusus)
            {
                if (group.Value.Contains(newModifier))
                {
                    foreach (var existingModifier in modifierQuantities)
                    {
                        Button existingBtn = this.Controls.Find($"modifier_{existingModifier.Key}", true)[0] as Button;
                        string existingModifierName = existingBtn.Text.Split('\n')[0];

                        if (group.Value.Contains(existingModifierName) && existingModifierName != newModifier)
                        {
                            MessageBox.Show($"Cannot add '{newModifier}' because you add '{existingModifierName}'",
                                          "Choose One!",
                                          MessageBoxButtons.OK,
                                          MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void ToggleModifier(Button button)
        {
            var modifierInfo = button.Tag as dynamic;
            if (modifierInfo != null)
            {
                int modifierId = modifierInfo.ModifierId;
                string modifierText = button.Text.Split('\n')[0];

                if (!modifierQuantities.ContainsKey(modifierId))
                {
                    if (!pengecekanmodifier(modifierText))
                    {
                        return;
                    }
                }

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

                            CreateModifierButtons(modifierTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading modifiers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateModifierButtons(DataTable modifierTable)
        {
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
            this.Hide();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Add Extra Menu!");
                return;
            }

            List<string> selectedModifiers = new List<string>();
            foreach (ListViewItem item in listView1.Items)
            {
                selectedModifiers.Add(item.SubItems[0].Text);
            }
            appliedModifiers[currentProductName] = selectedModifiers;

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

        private void FormModifier_Load(object sender, EventArgs e)
        {

        }
    }
}
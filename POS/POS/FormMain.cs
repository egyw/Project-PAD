using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS
{
    public partial class FormMain : Form
    {
        int idUser;
        DataTable dt;
        DataTable td;
        Boolean dine = false, take = false;
        public FormMain(int id)
        {
            InitializeComponent();
            this.idUser = id;

            this.FormBorderStyle = FormBorderStyle.None;

            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.MaximumSize = new Size(screenWidth, screenHeight);
            this.MinimumSize = new Size(screenWidth, screenHeight);
            try
            {
                Connection.open();
                dt = new DataTable();

                string command = "select * from products";
               
               MySqlDataAdapter adp = new MySqlDataAdapter(command, Connection.conn);

                adp.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.close();
            }

            panel_order.Enabled = false;
            panel_order.Visible = false;

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            label1.Select();
            this.FormBorderStyle = FormBorderStyle.Sizable;
            int centerX = (this.ClientSize.Width - panelTopMiddle.Width) / 2;

            panelTopMiddle.Location = new Point(centerX, panelTopMiddle.Location.Y);

            int buttonX = this.ClientSize.Width - btnOrders.Width - 4;  
            int buttonY = 4;  

            btnOrders.Location = new Point(buttonX, buttonY);

            pictureBox2.MouseUp += (obj, args) =>
            {
                if (args.Button == MouseButtons.Left)
                {
                    ContextMenuStrip contextMenu = new ContextMenuStrip();
                    contextMenu.Items.Add("Dine in", null, (s, ev) => 
                    { 
                        dine = true; 
                        take = false;
                        label2.Text = "Dine in";
                    });
                    contextMenu.Items.Add("Take out", null, (s, ev) => 
                    { 
                        dine = false; 
                        take = true;
                        label2.Text = "Take away";
                    });
                    contextMenu.Items.Add("Back", null, (s, ev) =>
                    {
                        dine = false;
                        take = false;

                        panel_order.Enabled = false;
                        panel_order.Visible = false;

                        richTextBox1.Text = "";
                    });
                    contextMenu.Show(pictureBox2, args.Location);
                }
            };


            loadMenu("");
        }

        public void loadMenu(string nama)
        {
            try
            {
                Connection.open();
                td = new DataTable();
                string command = "";
                if (string.IsNullOrWhiteSpace(nama)) 
                {
                    command = "select * from products";
                }
                else 
                {
                    command = "select * from products where product_name like @1";
                }
                MySqlCommand cmd = new MySqlCommand(command,Connection.conn);
                cmd.Parameters.AddWithValue("@1", "%" + nama + "%");

                MySqlDataReader reader = cmd.ExecuteReader();

                td.Load(reader);

                panelRight.Controls.Clear();

                int x = 22;
                int y = 26;
                int count = 0;

                foreach (DataRow row in td.Rows)
                {
                    string productName = row["product_name"].ToString();
                    bool isActive = Convert.ToBoolean(row["is_active"]);
                    Button btn = new Button
                    {
                        Text = productName,
                        Size = new Size(212, 46),
                        Location = new Point(x, y),
                        BackColor = Color.White,
                        FlatStyle = FlatStyle.Flat
                    };

                    btn.Click += btn_click;

                    if (isActive)
                    {
                        btn.FlatAppearance.BorderColor = Color.Green;
                    }
                    else
                    {
                        btn.FlatAppearance.BorderColor = Color.Red;
                    }
                    btn.FlatAppearance.BorderSize = 2;

                    panelRight.Controls.Add(btn);

                    count++;
                    if (count % 4 == 0)
                    {
                        x = 22; 
                        y += 68; 
                    }
                    else
                    {
                        x += 233;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.close();
            }
        }

        private void btn_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (dine || take)
            {
                // Tidak perlu implementasi tambahan
            }
            else
            {
                dine = true;
                take = false;
                panel_order.Enabled = true;
                panel_order.Visible = true;
                label2.Text = "Dine in";
            }

            decimal harga = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (btn.Text == row["product_name"].ToString())
                {
                    harga = (decimal)row["price"];
                    break;
                }
            }

            string nama = btn.Text;
            string hargaString = $"{harga:N2}"; 
            bool itemExists = false;
            string[] lines = richTextBox1.Lines;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Trim().StartsWith(nama)) 
                {
                    itemExists = true;

                    int xIndex = lines[i].IndexOf('x');
                    if (xIndex > 0)
                    {
                        string quantityPart = lines[i].Substring(xIndex + 1).Split(' ')[0].Trim();
                        if (int.TryParse(quantityPart, out int count))
                        {
                            count += 1; 

                            decimal totalHarga = harga * count; 
                            string totalHargaString = $"{totalHarga:N2}";

                            lines[i] = $"{nama} x{count}".PadRight(40) + $"$ {totalHargaString}";
                            break;
                        }
                    }
                }
            }

            if (!itemExists)
            {
                string formattedLine = $"{nama} x1".PadRight(40) + $"$ {hargaString}";
                richTextBox1.AppendText($"{formattedLine}\n");
            }
            else
            {
                richTextBox1.Lines = lines;
            }

            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();


        }


        private void pictureRefresh_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            loadMenu(textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dine = true;
            take = false;
            panel_order.Enabled = true;
            panel_order.Visible = true;
            label2.Text = "Dine in";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            string[] lines = richTextBox1.Text.Split('\n');
            decimal subtotal = 0, tax = 0, total = 0;

            foreach (string line in lines)
            {
                if (line.Contains("$"))
                {
                    int xIndex = line.IndexOf('x'); 
                    int lastSpaceIndex = line.LastIndexOf(' '); 

                    if (xIndex > 0 && lastSpaceIndex > xIndex)
                    {

                        string pricePart = line.Substring(lastSpaceIndex + 1).Trim();
                        pricePart = pricePart.Replace("$", "").Replace(",", ".").Trim();

                        if (decimal.TryParse(pricePart, out decimal price))
                        {
                            int quantity = 1;

                            string quantityPart = line.Substring(xIndex + 1, lastSpaceIndex - xIndex - 1).Trim();
                            if (int.TryParse(quantityPart, out int parsedQuantity))
                            {
                                quantity = parsedQuantity;
                            }

                            subtotal += price * quantity;
                        }
                    }
                }
            }

            tax = subtotal / 10; 
            total = subtotal + tax;

            label6.Text = $"$. {subtotal:N2}".Replace(".", ","); 
            label7.Text = $"$. {tax:N2}".Replace(".", ",");
            label8.Text = $"$. {total:N2}".Replace(".", ",");

        }

        private void richTextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int index = richTextBox1.GetCharIndexFromPosition(e.Location);

                string[] lines = richTextBox1.Text.Split(new[] { '\n' }, StringSplitOptions.None);

                foreach (string line in lines)
                {
                    int startIndex = richTextBox1.Text.IndexOf(line);
                    int endIndex = startIndex + line.Length;

                    if (index >= startIndex && index <= endIndex)
                    {
                        richTextBox1.Text = richTextBox1.Text.Remove(startIndex, line.Length + 1);
                        break; 
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            take = true;
            dine = false;
            panel_order.Enabled = true;
            panel_order.Visible = true;
            label2.Text = "Take Away";
        }
    }
}

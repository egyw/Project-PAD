﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        DataTable tableUser;
        public static bool isKeyboardActive = false;
        public FormMain(int id)
        {
            InitializeComponent();
            
            richTextBox1.Height = panel_order.Height - 10;
            this.idUser = id;
            getCashier();
            labelCashier.Text = tableUser.Rows[0]["firstName"].ToString();
            labelCashier.Location = new Point(this.ClientSize.Width - labelCashier.Width, panelBottom.Height - labelCashier.Height - 10);

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

            panelPay.Enabled = false;
            panelPay.Visible = false;

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

                        panelPay.Enabled = false;
                        panelPay.Visible = false;
                    });
                    contextMenu.Show(pictureBox2, args.Location);
                }
            };


            loadMenu("");
        }

        private void getCashier()
        {
            try
            {
                Connection.open();
                MySqlDataAdapter data = new MySqlDataAdapter("SELECT * FROM users WHERE user_id = @id", Connection.conn);
                data.SelectCommand.Parameters.AddWithValue("@id", idUser);

                tableUser = new DataTable();
                data.Fill(tableUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.close();
            }
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
                MySqlCommand cmd = new MySqlCommand(command, Connection.conn);
                cmd.Parameters.AddWithValue("@1", "%" + nama + "%");

                MySqlDataReader reader = cmd.ExecuteReader();

                td.Load(reader);

                panelRight.Controls.Clear();

                int buttonsPerRow = 5;
                int btnWidth = panelRight.Width / 6; 

                int spacing = (panelRight.Width - (buttonsPerRow * btnWidth)) / (buttonsPerRow + 1);

                int x = spacing;
                int y = 26; // posisi vertical baris pertama

                int count = 0;

                foreach (DataRow row in td.Rows)
                {
                    string productName = row["product_name"].ToString();
                    bool isActive = Convert.ToBoolean(row["is_active"]);
                    if (isActive)
                    {
                        // Membuat Panel untuk menggabungkan PictureBox dan Label
                        Panel productPanel = new Panel
                        {
                            Size = new Size(btnWidth, btnWidth + 20), // Tambahkan ruang untuk teks
                            Location = new Point(x, y),
                            BackColor = Color.White
                        };

                        // Membuat PictureBox
                        PictureBox btn = new PictureBox
                        {
                            Size = new Size(btnWidth, btnWidth),
                            Location = new Point(0, 0),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            BackColor = Color.LightGray
                        };

                        string imageName = row["image"].ToString();
                        string imagePath = Path.Combine("productImg", imageName);

                        try
                        {
                            // Memuat gambar dan melakukan resize
                            Image img = Image.FromFile(imagePath);
                            int maxWidth = 100; // Ukuran gambar yang diinginkan
                            int maxHeight = 100;
                            btn.Image = ResizeImage(img, maxWidth, maxHeight); // Resize gambar
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        // Membuat Label untuk teks
                        Label label = new Label
                        {
                            Text = productName,
                            Size = new Size(btnWidth, 20),
                            Location = new Point(0, btnWidth), // Di bawah PictureBox
                            TextAlign = ContentAlignment.MiddleCenter,
                            Font = new Font("Arial", 10, FontStyle.Regular),
                            BackColor = Color.White
                        };

                        btn.Click += (s, e) => btn_click(label, e);

                        label.Click += btn_click;

                        // Menambahkan PictureBox dan Label ke Panel
                        productPanel.Controls.Add(btn);
                        productPanel.Controls.Add(label);

                        // Menambahkan Panel ke panelRight
                        panelRight.Controls.Add(productPanel);

                        // Mengatur posisi berikutnya
                        count++;
                        if (count % buttonsPerRow == 0)
                        {
                            y += btnWidth + 35; // Tinggi PictureBox + jarak antar baris
                            x = spacing;
                        }
                        else
                        {
                            x += btnWidth + spacing;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.close();
            }
        }

        public static Image ResizeImage(Image img, int width, int height)
        {
            var bmp = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.DrawImage(img, 0, 0, width, height);
            }
            return bmp;
        }

        private void btn_click(object sender, EventArgs e)
        {
            string menu = string.Empty;

            if (sender is PictureBox btn)
            {
                menu = btn.Tag as string;
            }
            else if (sender is Label label)
            {
                menu = label.Text;
            }

            if (string.IsNullOrEmpty(menu))
            {
                MessageBox.Show("Nama produk tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
                panelPay.Enabled = true;
                panelPay.Visible = true;
            }

            decimal harga = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (menu == row["product_name"].ToString())
                {
                    harga = (decimal)row["price"];
                    break;
                }
            }

            string nama = menu;
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

            // Mengubah ukuran font untuk seluruh teks
            richTextBox1.SelectAll();
            richTextBox1.SelectionFont = new Font(richTextBox1.Font.FontFamily, 10);
            richTextBox1.DeselectAll();
        }


        private void pictureRefresh_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            Keyboard.clearKeyboard();
            buttonClosePressed();
            labelCashier.Select();
            panelBottom.Height = 39;
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

            panelPay.Enabled = true;
            panelPay.Visible = true;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            string[] lines = richTextBox1.Text.Split('\n');
            decimal subtotal = 0, tax = 0, total = 0;

            foreach (string line in lines)
            {
                if (line.Contains("$"))
                {
                    int lastSpaceIndex = line.LastIndexOf(' '); 

                    if (lastSpaceIndex > 0)
                    {
                        string pricePart = line.Substring(lastSpaceIndex + 1).Trim();
                        pricePart = pricePart.Replace("$", "").Replace(",", ".").Trim();

                        if (decimal.TryParse(pricePart, out decimal price))
                        {
                            subtotal += price;
                        }
                    }
                }
            }
            subtotal /= 100;
            tax = subtotal / 10;
            total = subtotal + tax;

            labelSubtotal.Text = $"$. {subtotal:N2}".Replace(".", ",");
            labelTax.Text = $"$. {tax:N2}".Replace(".", ",");
            labelTotal.Text = $"$. {total:N2}".Replace(".", ",");
            getLabelLocation();
        }

        private void richTextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int index = richTextBox1.GetCharIndexFromPosition(e.Location);

                int lineIndex = richTextBox1.GetLineFromCharIndex(index);
                string namamenu = richTextBox1.Lines[lineIndex];

                string menuName = namamenu.Split('x')[0].Trim();

                string tipeproduk = "";

                try
                {
                    Connection.open();
                    string query = "SELECT product_type FROM products WHERE product_name = @productName";

                    using (MySqlCommand cmd = new MySqlCommand(query, Connection.conn))
                    {
                        cmd.Parameters.AddWithValue("@productName", menuName);

                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            tipeproduk = result.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error retrieving product type: " + ex.Message);
                }
                finally
                {
                    Connection.close();
                }

                FormModifier customorder = new FormModifier(idUser, namamenu, tipeproduk);
                customorder.Show();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void panelRight_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelTop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            addKeyboard(textBox1);
            labelCashier.Text = "";
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (isKeyboardActive == false)
            {
                panelBottom.Height = 100;
                panelKeyboard.Height = 0;
                labelCashier.Text = tableUser.Rows[0]["firstName"].ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            take = true;
            dine = false;
            panel_order.Enabled = true;
            panel_order.Visible = true;
            label2.Text = "Take Away";
            panelPay.Enabled = true;
            panelPay.Visible = true;
        }

        private void addKeyboard(TextBox textBox)
        {
            Keyboard.OnKeyboardClosed += buttonClosePressed;
            panelBottom.Height = 350;
            panelKeyboard.Height = 350;
            Keyboard.addKeyboard(this, panelKeyboard, textBox, panelBottom, panelBottom.Width);
            isKeyboardActive = true;
        }

        private void getLabelLocation()
        {
            labelSubtotal.Location = new Point(panelPay.Width - labelSubtotal.Width, labelSubtotal.Location.Y);
            labelTax.Location = new Point(panelPay.Width - labelTax.Width, labelTax.Location.Y);
            labelTotal.Location = new Point(panelPay.Width - labelTotal.Width, labelTotal.Location.Y);
        }

        private void buttonClosePressed()
        {
            panelBottom.Height = 39;
            labelCashier.Text = tableUser.Rows[0]["firstName"].ToString();
            isKeyboardActive = false;
            Keyboard.OnKeyboardClosed -= buttonClosePressed;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace POS
{
    public partial class FormPayment : Form
    {
        public static string textCustom = ""; 
        public static string imgCustom = "";
        public static string otherPayment = "";
        public static int price = 0;
        public static double eMoney = 0;
        public static long entryCard = 0;
        public static int orderId = 0;
        double payCash = 0; 
        public static double grandTotal = 0;
        bool cekTransaction = false;
        public FormPayment(ListView ls, int id, int iduser, string type)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            CopyListViewData(ls, listView1);
            if (id == 0)
            {
                Random rand = new Random();
                string[] customers = {
                        "Taylor Swift",
                        "Beyoncé",
                        "BTS",
                        "Adele",
                        "Ed Sheeran",
                        "Ariana Grande",
                        "Drake",
                        "Billie Eilish",
                        "Blackpink",
                        "Bruno Mars"
                    };

                string namaArtis = customers[rand.Next(customers.Length)];
                Connection.open();
                MySqlTransaction transaction = Connection.conn.BeginTransaction();
                try
                {
                   
                    string query = "INSERT INTO orders (user_id, grand_total, order_status, customer_name, order_type) VALUES (@1,@2,@3,@4,@5)";
                    MySqlCommand cmd = new MySqlCommand(query, Connection.conn, transaction);
                    cmd.Parameters.AddWithValue("@1", iduser);
                    string totalText = label8.Text.Replace("Rp.", "").Trim();
                    decimal grand_total = decimal.Parse(totalText, System.Globalization.NumberStyles.Currency);
                    cmd.Parameters.AddWithValue("@2", grand_total);
                    cmd.Parameters.AddWithValue("@3", "pending");
                    cmd.Parameters.AddWithValue("@4", namaArtis);
                    string a = "";
                    if (type == "Dine in")
                    {
                        a = "dine_in";
                    }
                    else
                    {
                        a = "take_away";
                    }
                    cmd.Parameters.AddWithValue("@5", a);
                    cmd.ExecuteNonQuery();
                    MySqlDataAdapter data = new MySqlDataAdapter("SELECT order_id FROM orders ORDER BY order_id DESC", Connection.conn);

                    DataTable orderby = new DataTable();
                    data.Fill(orderby);
                    int idnya = 0;
                    foreach (DataRow row in orderby.Rows)
                    {
                        idnya = (int)row["order_id"];
                        break;
                    }
                    
                    string query2 = "INSERT INTO order_items (order_id, product_id, quantity, price, total) VALUES (@1,@2,@3,@4,@5)";

                    foreach (ListViewItem item in listView1.Items)
                    {
                        MySqlCommand cmd2 = new MySqlCommand(query2, Connection.conn, transaction);
                        
                        cmd2.Parameters.AddWithValue("@1", idnya);
                        cmd2.Parameters.AddWithValue("@2", GetProductId(item.Text));
                        cmd2.Parameters.AddWithValue("@3", int.Parse(item.SubItems[1].Text));
                        cmd2.Parameters.AddWithValue("@4", decimal.Parse(item.SubItems[2].Text.Replace(",", "")));
                        cmd2.Parameters.AddWithValue("@5", decimal.Parse(item.SubItems[2].Text.Replace(",", "")) * int.Parse(item.SubItems[1].Text));

                        cmd2.ExecuteNonQuery();
                    }

                    int rowIndex = listView1.Items.Count - 1;
                   
                    MySqlDataAdapter data2 = new MySqlDataAdapter("SELECT order_item_id FROM order_items ORDER BY order_item_id DESC", Connection.conn);

                    DataTable orderby2 = new DataTable();
                    data2.Fill(orderby2);
                    int idnya2 = 0;
                    foreach (DataRow row in orderby2.Rows)
                    {
                        idnya2 = (int)row["order_item_id"];
                        break;
                    }

                    string query3 = "INSERT INTO order_item_modifiers (order_item_id, modifier_id, price) VALUES (@1,@2,@3)";

                   
                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (!string.IsNullOrWhiteSpace(item.SubItems[4].Text))
                        {
                            string indexmodif = item.SubItems[4].Text;

                            string[] pisah = indexmodif.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                            int[] modifid = Array.ConvertAll(pisah, s => int.Parse(s.Trim()));
                            for (int i = 0; i < modifid.Length; i++)
                            {
                                MySqlCommand cmd3 = new MySqlCommand(query3, Connection.conn, transaction);
                                cmd3.Parameters.AddWithValue("@1", idnya2 - rowIndex);
                                int idmod = modifid[i];
                                cmd3.Parameters.AddWithValue("@2", idmod);
                                cmd3.Parameters.AddWithValue("@3", GetModifprice(idmod));

                                cmd3.ExecuteNonQuery();
                            }
                        }
                        rowIndex--;
                    }
                    MessageBox.Show("Finish");
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Connection.close();
                }
            }
            else
            {
                orderId = id;
            }
         
        }

        public int GetProductId(string nama)
        {
            int product = 0;

            try
            {
                string query = "SELECT product_id FROM products WHERE product_name = @1";
                MySqlCommand cmd = new MySqlCommand(query, Connection.conn);
                DataTable products = new DataTable();
                cmd.Parameters.AddWithValue("@1", nama);
                MySqlDataReader reader = cmd.ExecuteReader();
                products.Load(reader);
                foreach (DataRow row in products.Rows)
                {
                    product = (int)row["product_id"];
                    break;
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return product;
        }

        public decimal GetModifprice(int id)
        {
            decimal price = 0;

            try
            {
                string query = "SELECT modifier_price FROM modifiers WHERE modifier_id = @1";
                MySqlCommand cmd = new MySqlCommand(query, Connection.conn);
                DataTable prices = new DataTable();
                cmd.Parameters.AddWithValue("@1", id);
                MySqlDataReader reader = cmd.ExecuteReader();
                prices.Load(reader);
                foreach (DataRow row in prices.Rows)
                {
                    price = (decimal)row["modifier_price"];
                    break;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return price;
        }

        private void CopyListViewData(ListView source, ListView target)
        {
            foreach (ColumnHeader column in source.Columns)
            {
                target.Columns.Add((ColumnHeader)column.Clone());
            }

            foreach (ListViewItem item in source.Items)
            {
                target.Items.Add((ListViewItem)item.Clone());
            }

            target.View = View.Details;
            target.FullRowSelect = true;
            addToTotal();
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Maximized;
            CenterPanel();
            MessageBox.Show(grandTotal + " " + FormMain.statusLabel);
        }

        public void CenterPanel()
        {

            panel4.Left = ((this.Size.Width - panel4.Width) / 2) - (this.Size.Width);
            if(panel4.Left < 319)
            {
                panel4.Left = 320;
            }
            panel4.Top = ((this.Size.Height - panel4.Height) / 2) + this.Size.Height / 9;

        }

        private void FormPayment_Resize(object sender, EventArgs e)
        {
            CenterPanel();
        }

        private void buttonCashCustom_Click(object sender, EventArgs e)
        {
            panelButtonCash.Enabled = false; 
            textBox1.Clear();
            label11.Text = "Cash";
            imgCustom = "cash";
            FormCustomAllPayment fcpa = new FormCustomAllPayment();
            fcpa.ShowDialog();
            if(textCustom != "")
            {
                buttonCashCustom.Text = textCustom;
                string formatString = textCustom.Replace(".", "");
                payCash = int.Parse(formatString.Substring(2));
                textBox1.Text = payCash.ToString("N0", new System.Globalization.CultureInfo("id-ID"));
                label7.Text = "Rp. " + textBox1.Text;
                totalPay();

            }
            else
            {
                payCash = 0;
                panelButtonCash.Enabled = true;
                buttonCashCustom.Text = "Custom";
        
            }
            
        }

        public void addToTotal()
        {
            double subtotal = 0, tax = 0, total = 0;

            foreach (ListViewItem item in listView1.Items)
            {
 
                string priceText = item.SubItems[2].Text;
                double price = double.Parse(priceText);
                subtotal += price;
            }
            subtotal = subtotal;
            tax = subtotal / 10;
            total = subtotal + tax;

            label14.Text = $"Rp.  {subtotal:N2}";
            label15.Text = $"Rp.  {tax:N2}";
            label8.Text = $"Rp.  {total:N2}";
            grandTotal = total;
        }

        public void totalPay()
        {
           
            double subT = double.Parse(label14.Text.Substring(4));
            double allPay = double.Parse(label7.Text.Substring(4));
            MessageBox.Show("Result : " + subT + " " + allPay);
            double total = subT - allPay;
            label8.Text = "Rp. " + total.ToString("N2", new System.Globalization.CultureInfo("id-ID"));

        }

        private void buttonShopee_Click(object sender, EventArgs e)
        {
            otherPayment = "shopee";
            imgCustom = "otherPayment";
            FormCustomAllPayment fcpa = new FormCustomAllPayment();
            fcpa.ShowDialog();
            label11.Text = "E-Money";
            payCash = eMoney;
            label7.Text = "Rp. " + payCash.ToString("N0", new System.Globalization.CultureInfo("id-ID"));
        }

        private void buttonOvo_Click(object sender, EventArgs e)
        {
            otherPayment = "ovo";
            imgCustom = "otherPayment";
            FormCustomAllPayment fcpa = new FormCustomAllPayment();
            fcpa.ShowDialog();
            label11.Text = "E-Money";
            payCash = eMoney;
            label7.Text = "Rp. " + payCash.ToString("N0", new System.Globalization.CultureInfo("id-ID"));
            totalPay();
        }

        private void buttonDana_Click(object sender, EventArgs e)
        {
            otherPayment = "dana";
            imgCustom = "otherPayment";
            FormCustomAllPayment fcpa = new FormCustomAllPayment();
            fcpa.ShowDialog();
            label11.Text = "E-Money";
            payCash = eMoney;
            label7.Text = "Rp. " + payCash.ToString("N0", new System.Globalization.CultureInfo("id-ID"));
            totalPay();
        }

        private void buttonGopay_Click(object sender, EventArgs e)
        {
            otherPayment = "gopay";
            imgCustom = "otherPayment";
            FormCustomAllPayment fcpa = new FormCustomAllPayment();
            fcpa.ShowDialog();
            label11.Text = "E-Money";
            payCash = eMoney;
            label7.Text = "Rp. " + payCash.ToString("N0", new System.Globalization.CultureInfo("id-ID"));
            totalPay();
        }

        private void buttonCard_Click(object sender, EventArgs e)
        {
            if(entryCard == 0)
            {
                MessageBox.Show("Please Insert Your Card Entry First!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Done You Have Full Your Card Entry!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonCash1_Click(object sender, EventArgs e)
        {
            payCash += 1000; totalPay(); 
            label7.Text = "Rp. " + payCash.ToString("N0", new System.Globalization.CultureInfo("id-ID"));
        }

        private void buttonCash2_Click(object sender, EventArgs e)
        {
            payCash += 10000; totalPay(); 
            label7.Text = "Rp. " + payCash.ToString("N0", new System.Globalization.CultureInfo("id-ID"));
        }

        private void buttonCash3_Click(object sender, EventArgs e)
        {
            payCash += 25000; totalPay();
            label7.Text = "Rp. " + payCash.ToString("N0", new System.Globalization.CultureInfo("id-ID"));
        }

        private void buttonCash4_Click(object sender, EventArgs e)
        {
            payCash += 50000; totalPay(); 
            label7.Text = "Rp. " + payCash.ToString("N0", new System.Globalization.CultureInfo("id-ID"));
        }

        private void buttonCash5_Click(object sender, EventArgs e)
        {
            payCash += 100000; totalPay(); 
            label7.Text = "Rp. " + payCash.ToString("N0", new System.Globalization.CultureInfo("id-ID"));
        }

        private void buttonCash6_Click(object sender, EventArgs e)
        {
            payCash += 100; totalPay(); 
            label7.Text = "Rp. " + payCash.ToString("N0", new System.Globalization.CultureInfo("id-ID"));
        }

        private void buttonCash7_Click(object sender, EventArgs e)
        {
            payCash += 250; totalPay(); 
            label7.Text = "Rp. " + payCash.ToString("N0", new System.Globalization.CultureInfo("id-ID"));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            payCash += 500; totalPay();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            
            if((grandTotal-payCash) < 0)
            {
                double hah = grandTotal - payCash;
                MessageBox.Show("Uang yang kamu input gak kelebihan ta?", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            payOrder();
            if (cekTransaction)
            {
                label11.Text = "Cash";
                AnimationLoadingPayment alp = new AnimationLoadingPayment();
                alp.ShowDialog();
                //MessageBox.Show("Anda Berhasil Membayar Sebesar Rp. " + grandTotal, "Information Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panelButtonCash.Enabled = true;
                listView1.Items.Clear();
                payCash = 0; grandTotal = 0;
                label14.Text = "Rp. " + grandTotal.ToString().Replace(',', '.');
                label1.Text = "Amount ( " + "Rp. " + grandTotal.ToString().Replace(',', '.') + " )";
                buttonCard.Text = "Rp. " + grandTotal.ToString().Replace(',', '.');
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Color borderColor =  Color.FromArgb( 128, 255, 128);

            // Lebar border
            int borderWidth = 2;

            // Gambar border hijau di sekitar panel
            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                e.Graphics.DrawRectangle(pen, 0, 0, panel2.Width - 1, panel2.Height - 1);
            }
        }

        public void payOrder()
        {
            Connection.open();
            MySqlTransaction transaction = Connection.conn.BeginTransaction();
            try
            {

                MySqlCommand cmd = new MySqlCommand("UPDATE payments SET payment_status = 'Completed' " +
                    "WHERE order_id = @1", Connection.conn, transaction);
                cmd.Parameters.AddWithValue("@1", orderId);
                cmd.ExecuteNonQuery();

                transaction.Commit();
                cekTransaction = true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                cekTransaction = false;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Connection.close();
            }
        }

        private void buttonShopee_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                string discountText = "5%";

                Font font = new Font("Arial", 10, FontStyle.Bold);
                Brush brush = Brushes.LightGreen;

                SizeF textSize = e.Graphics.MeasureString(discountText, font);
                PointF location = new PointF(btn.Width - textSize.Width - 5, 5);

                e.Graphics.DrawString(discountText, font, brush, location);
            }
        }

        private void buttonOvo_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                string discountText = "10%";
                Font font = new Font("Arial", 10, FontStyle.Bold);
                Brush brush = Brushes.LightGreen;

                SizeF textSize = e.Graphics.MeasureString(discountText, font);
                PointF location = new PointF(btn.Width - textSize.Width - 5, 5);

                e.Graphics.DrawString(discountText, font, brush, location);
            }
        }

        private void buttonDana_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                string discountText = "15%";
                Font font = new Font("Arial", 10, FontStyle.Bold);
                Brush brush = Brushes.LightGreen;

                SizeF textSize = e.Graphics.MeasureString(discountText, font);
                PointF location = new PointF(btn.Width - textSize.Width - 5, 5);

                e.Graphics.DrawString(discountText, font, brush, location);
            }
        }

        private void buttonGopay_Paint(object sender, PaintEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                string discountText = "20%";
                Font font = new Font("Arial", 10, FontStyle.Bold);
                Brush brush = Brushes.LightGreen;

                SizeF textSize = e.Graphics.MeasureString(discountText, font);
                PointF location = new PointF(btn.Width - textSize.Width - 5, 5);

                e.Graphics.DrawString(discountText, font, brush, location);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            FormCustomAllPayment fcpa = new FormCustomAllPayment();
            imgCustom = "card";
            fcpa.ShowDialog();
            if (entryCard != 0)
            {
                listView1.Items.Clear();
                grandTotal = 0;
                label14.Text = "Rp. " + grandTotal.ToString().Replace(',', '.');
                label1.Text = "Amount ( " + "Rp. " + grandTotal.ToString().Replace(',', '.') + " )";
                buttonCard.Text = "Rp. " + grandTotal.ToString().Replace(',', '.');
                label11.Text = "E-Money";
                label7.Text = eMoney.ToString("N0", new System.Globalization.CultureInfo("id-ID"));
                totalPay();
            }
        }
    }
}

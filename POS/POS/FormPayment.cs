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
        public FormPayment(ListView ls)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            int id = FormMain.idTransfer;
            orderId = id;
            getDataLvw(id);
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Maximized;
            CenterPanel();
            //addToTotal();
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
            decimal subtotal = 0, tax = 0, total = 0;

            foreach (ListViewItem item in listView1.Items)
            {
                if (decimal.TryParse(item.SubItems[2].Text.Replace(",", "").Replace(".", ","), out decimal price))
                {
                    subtotal += price;
                }
            }
            subtotal = subtotal / 100;
            tax = subtotal / 10;
            total = subtotal + tax;

            label14.Text = $"Rp.  {subtotal:N2}".Replace(".", ",");
            label7.Text = $"Rp.  {tax:N2}".Replace(".", ",");
            label8.Text = $"Rp.  {total:N2}".Replace(".", ",");
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

            }
        }

        private void buttonCash1_Click(object sender, EventArgs e)
        {
            payCash += 1000;
        }

        private void buttonCash2_Click(object sender, EventArgs e)
        {
            payCash += 10000;
        }

        private void buttonCash3_Click(object sender, EventArgs e)
        {
            payCash += 25000;
        }

        private void buttonCash4_Click(object sender, EventArgs e)
        {
            payCash += 50000;
        }

        private void buttonCash5_Click(object sender, EventArgs e)
        {
            payCash += 100000;
        }

        private void buttonCash6_Click(object sender, EventArgs e)
        {
            payCash += 100;
        }

        private void buttonCash7_Click(object sender, EventArgs e)
        {
            payCash += 250;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            payCash += 500;
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

        public void getDataLvw(int orderid)
        {
            listView1.Items.Clear();

            listView1.Columns.Add("Product", 150);
            listView1.Columns.Add("Quantity", 80);
            listView1.Columns.Add("Price", 100);
            listView1.Columns.Add("Modifiers", 200);
            listView1.View = View.Details;
            listView1.Font = new Font(listView1.Font.FontFamily, 10, listView1.Font.Style);

            try
            {
                Connection.open();
                MySqlDataAdapter data = new MySqlDataAdapter("SELECT p.product_name, p.price ,oi.quantity FROM products p JOIN order_items oi ON oi.product_id = p.product_id JOIN orders o ON o.order_id = oi.order_id WHERE o.order_id = @id", Connection.conn);
                data.SelectCommand.Parameters.AddWithValue("@id", orderid);

                DataTable itemordered = new DataTable();
                data.Fill(itemordered);

                foreach (DataRow row in itemordered.Rows)
                {
                    string productName = row["product_name"].ToString();
                    string quantityText = row["quantity"].ToString();
                    string priceText = row["price"].ToString();

                    decimal price = decimal.TryParse(priceText, out decimal parsedPrice) ? parsedPrice : 0;
                    decimal quantity = decimal.TryParse(quantityText, out decimal parsedQuantity) ? parsedQuantity : 0;
                    decimal totalPrice = price * quantity;

                    ListViewItem newItem = new ListViewItem(productName);
                    newItem.SubItems.Add(quantityText);
                    newItem.SubItems.Add(totalPrice.ToString("N2"));
                    listView1.Items.Add(newItem);
                }
                listView1.View = View.Details;
                listView1.Columns[0].Width = 200;
                listView1.Columns[1].Width = 50;
                listView1.Columns[2].Width = 100;
                addToTotal();
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
    }
}

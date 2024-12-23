﻿using System;
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
        public static long entryCard = 0;
        public static int orderId = 0;
        double payCash = 0; 
        public static double grandTotal = 0;
        bool cekTransaction = false;
        public FormPayment()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            CenterPanel();
            loadDataPayment();
            
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

        private void label4_DoubleClick(object sender, EventArgs e)
        {
            FormCustomAllPayment fcpa = new FormCustomAllPayment();
            imgCustom = "card";
            fcpa.ShowDialog();
            if(entryCard != 0)
            {
                richTextBox1.Clear();
                grandTotal = 0;
                label14.Text = "Rp. " + grandTotal.ToString().Replace(',', '.');
                label1.Text = "Amount ( " + "Rp. " + grandTotal.ToString().Replace(',', '.') + " )";
                buttonCard.Text = "Rp. " + grandTotal.ToString().Replace(',', '.');
            }
        }

        private void buttonCashCustom_Click(object sender, EventArgs e)
        {
            panelButtonCash.Enabled = false; textBox1.Clear();
            imgCustom = "cash";
            FormCustomAllPayment fcpa = new FormCustomAllPayment();
            fcpa.ShowDialog();
            if(textCustom != "")
            {
                buttonCashCustom.Text = textCustom;
                string formatString = textCustom.Replace(".", "");
                payCash = int.Parse(formatString.Substring(2));
            }
            
        }

        private void buttonShopee_Click(object sender, EventArgs e)
        {
            otherPayment = "shopee";
            imgCustom = "otherPayment";
            FormCustomAllPayment fcpa = new FormCustomAllPayment();
            fcpa.ShowDialog();
        }

        private void buttonOvo_Click(object sender, EventArgs e)
        {
            otherPayment = "ovo";
            imgCustom = "otherPayment";
            FormCustomAllPayment fcpa = new FormCustomAllPayment();
            fcpa.ShowDialog();
        }

        private void buttonDana_Click(object sender, EventArgs e)
        {
            otherPayment = "dana";
            imgCustom = "otherPayment";
            FormCustomAllPayment fcpa = new FormCustomAllPayment();
            fcpa.ShowDialog();
        }

        private void buttonGopay_Click(object sender, EventArgs e)
        {
            otherPayment = "gopay";
            imgCustom = "otherPayment";
            FormCustomAllPayment fcpa = new FormCustomAllPayment();
            fcpa.ShowDialog();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = payCash.ToString("N0", new System.Globalization.CultureInfo("id-ID"));
            label7.Text = "Rp. "+ textBox1.Text;
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
                AnimationLoadingPayment alp = new AnimationLoadingPayment();
                alp.ShowDialog();
                MessageBox.Show("Anda Berhasil Membayar Sebesar Rp. " + grandTotal, "Information Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                panelButtonCash.Enabled = true;
                richTextBox1.Clear();
                payCash = 0; grandTotal = 0;
                label14.Text = "Rp. " + grandTotal.ToString().Replace(',', '.');
                label1.Text = "Amount ( " + "Rp. " + grandTotal.ToString().Replace(',', '.') + " )";
                buttonCard.Text = "Rp. " + grandTotal.ToString().Replace(',', '.');
            }
        }

        public void loadDataPayment()
        {
            try
            {
                Connection.open();
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM orders o " +
                    "JOIN order_items oi ON o.order_id = oi.order_id " +
                    "JOIN products p ON p.product_id = oi.product_id " +
                    "JOIN payments py ON py.order_id = o.order_id " +
                    "WHERE o.order_status != 'completed'  ", Connection.conn);
                //MySqlCommand cmd = new MySqlCommand("SELECT * FROM orders o " +
                //    "jOIN order_items oi ON o.order_id = oi.order_id " +
                //    "JOIN products p ON p.product_id = oi.product_id " +
                //    "JOIN payments py ON py.order_id = o.order_id " +
                //    "WHERE o.order_status != 'completed' and o.customer_name = @customerName ", Connection.conn);
                //cmd.Parameters.AddWithValue("@customerName", "Bob White");

                MySqlDataReader reader = cmd.ExecuteReader();
                double qty, price, grandtotal; string namaProduct = "";
                qty = price = grandtotal = 0;
                while (reader.Read())
                {
                    price = double.Parse(reader["price"].ToString());
                    qty = double.Parse(reader["quantity"].ToString());
                    namaProduct = reader["product_name"].ToString();
                    orderId = int.Parse(reader["order_id"].ToString());
                    double subtotal = price * qty;
                    subtotal *= 1000;
                    grandTotal += subtotal;
                    string kalimat = namaProduct + " " + qty + "x  = Rp. " + subtotal;
                    richTextBox1.AppendText(kalimat);
                }
                reader.Close();
                label14.Text = "Rp. " + grandTotal.ToString("N0", new System.Globalization.CultureInfo("id-ID")); ;
                label1.Text = "Amount ( " + "Rp. " + grandTotal.ToString("N0", new System.Globalization.CultureInfo("id-ID")) + " )";
                buttonCard.Text = "Rp. " + grandTotal.ToString("N0", new System.Globalization.CultureInfo("id-ID"));

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Connection.close();
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
                cmd.Parameters.AddWithValue("@1", FormPayment.orderId);
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
    }
}

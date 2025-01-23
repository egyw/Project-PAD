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
    public partial class FormCustomAllPayment : Form
    {
        public static bool cekTransaction = false;
        public FormCustomAllPayment()
        {
            InitializeComponent();
            FormPayment.eMoney = 0;
        }

        private void FormCustomAllPayment_Load(object sender, EventArgs e)
        {
            CenterPanel();
            if(FormPayment.imgCustom == "cash")
            {
                button10.Visible = true;
                pictureBox1.Height = 180;
                pictureBox1.Location = new Point(40, 0);
                pictureBox1.Image = Properties.Resources.money;
            }
            else if(FormPayment.imgCustom == "card")
            {
                button10.Visible = false;
                pictureBox1.Height = 190;
                pictureBox1.Location = new Point(40, -20);
                pictureBox1.Image = Properties.Resources.atm_card;
            }
            else
            {
                button10.Visible = false;
                if(FormPayment.otherPayment == "shopee")
                {
                    pictureBox1.Image = Properties.Resources.Shopee;
                    pictureBox1.Location = new Point(40, pictureBox1.Location.Y);
                }
                else if(FormPayment.otherPayment == "ovo")
                {
                    pictureBox1.Image = Properties.Resources.Ovo;
                    pictureBox1.Width = 165;
                    pictureBox1.Location = new Point(110,pictureBox1.Location.Y);
                }
                else if (FormPayment.otherPayment == "dana")
                {
                    pictureBox1.Image = Properties.Resources.Dana;
                    pictureBox1.Width = 165;
                    pictureBox1.Location = new Point(110, pictureBox1.Location.Y);
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.Gopay;
                    pictureBox1.Width = 165;
                    pictureBox1.Location = new Point(110, pictureBox1.Location.Y);
                }
               
            }
            button0.Click += button0_Click;
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
            button4.Click += button4_Click;
            button5.Click += button5_Click;
            button6.Click += button6_Click;
            button7.Click += button7_Click;
            button8.Click += button8_Click;
            button9.Click += button9_Click;
            button10.Click += button10_Click;

        }
        public void CenterPanel()
        {
            panel1.Left = (this.Size.Width - panel1.Width) / 2;
            panel1.Top = ((this.Size.Height - panel1.Height) / 2);

        }
        public void cekLength()
        {
            if (FormPayment.imgCustom == "cash" || FormPayment.imgCustom == "otherPayment")
            {
                string input = textBox1.Text.Replace(".", "");
                string reversedInput = ReverseString(input);
                string output = "";

                for (int i = 0; i < reversedInput.Length; i++)
                {
                    output += reversedInput[i];

                    if ((i + 1) % 3 == 0 && i + 1 != reversedInput.Length)
                    {
                        output += ".";
                    }
                }

                textBox1.Text = ReverseString(output);
                textBox1.SelectionStart = textBox1.Text.Length;
                FormPayment.textCustom = "Rp. " + ReverseString(output);
            }
            else if(FormPayment.imgCustom == "card")
            {

               if(textBox1.Text.Length > 16)
               {
                    textBox1.Text = textBox1.Text.ToString().Substring(0,textBox1.Text.Length-1);
 
               }
            }
            else
            {
                //// misal ada 9 no telepon
                //if(textBox1.Text.Length > 9)
                //{
                //    textBox1.Text = textBox1.Text.ToString().Substring(textBox1.Text.Length - 1);
                //}
            }
        }

        private string ReverseString(string input)
        {
            char[] array = input.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

        private void button0_Click(object sender, EventArgs e)
        {
            textBox1.Text += "0";
            cekLength();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += "1";
            cekLength();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += "2";
            cekLength();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += "3";
            cekLength();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += "4";
            cekLength();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += "5";
            cekLength();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += "6";
            cekLength();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "7";
            cekLength();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "8";
            cekLength();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "9";
            cekLength();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text += "00";
            cekLength();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            FormPayment.textCustom = "";
            textBox1.Text = "";
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if(FormPayment.imgCustom == "otherPayment")
            {
                int money = FormPayment.price;
                double price = FormPayment.grandTotal;
                int myMoney = int.Parse(textBox1.Text.ToString().Replace(".",""));
                price = paymentDiscount(price);
                //MessageBox.Show(price + " " + FormPayment.grandTotal + " " + money);

                if((myMoney - price) < 0)
                {
                    MessageBox.Show("Pembayaran Kurang!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                payOrder();
                double total = myMoney - price;
                if (cekTransaction)
                {
                    FormPayment.eMoney = total;
                    AnimationLoadingPayment alp = new AnimationLoadingPayment();
                    alp.ShowDialog();
                   // MessageBox.Show("Transaction Sucess!", "Information Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    AnimationLoadingPayment alp = new AnimationLoadingPayment();
                    alp.ShowDialog();
                    //MessageBox.Show("Transaction Fail!", "Information Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }
            else if(FormPayment.imgCustom == "card")
            {
                FormPayment.entryCard = Int64.Parse(textBox1.Text);
                double price = FormPayment.grandTotal;
                if (textBox1.Text.Length != 16)
                {
                    MessageBox.Show("Card Entry Minimal 16 digit?", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;

                }

                payOrder();
                if (cekTransaction)
                {
                    FormPayment.eMoney = price;
                    AnimationLoadingPayment alp = new AnimationLoadingPayment();
                    alp.ShowDialog();
                    //MessageBox.Show("Anda Berhasil Membayar Sebesar Rp. " + price, "Information Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    FormPayment.entryCard = 0;
                    AnimationLoadingPayment alp = new AnimationLoadingPayment();
                    alp.ShowDialog();
                    //MessageBox.Show("Transaction Fail!", "Information Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.Close();
            }
            else
            {
                //payOrder();
                double price = FormPayment.grandTotal;
                this.Close();
            }
 
        }

        private void FormCustomAllPayment_Resize(object sender, EventArgs e)
        {
            CenterPanel();
        }

        public double paymentDiscount(double price)
        {
            if (FormPayment.otherPayment == "shopee")
            {
                price -= price * 0.05; // Diskon 5%
            }
            else if (FormPayment.otherPayment == "ovo")
            {
                price -= price * 0.10; // Diskon 10%
            }
            else if (FormPayment.otherPayment == "gopay")
            {
                price -= price * 0.15; // Diskon 15%
            }
            else if (FormPayment.otherPayment == "dana")
            {
                price -= price * 0.20; // Diskon 20%
            }
            price = Math.Ceiling(price);
            return price;
        }

        public void insertToDatabase()
        {
            Connection.open();
            MySqlTransaction transaction = Connection.conn.BeginTransaction();
            try
            {
                MessageBox.Show(FormPayment.orderId + " ");
                if(FormPayment.orderId == 0)
                {
                    // order 
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

                    MySqlCommand cmd = new MySqlCommand("INSERT INTO orders (user_id, grand_total, order_status, customer_name, order_type) " +
                    "VALUES (@user_id, @grand_total, @order_status, @customer_name, @order_type)", Connection.conn, transaction);
                    cmd.Parameters.AddWithValue("@user_id", FormMain.idTransfer);
                    cmd.Parameters.AddWithValue("@grand_total", FormPayment.grandTotal);
                    cmd.Parameters.AddWithValue("@order_status", "completed");
                    cmd.Parameters.AddWithValue("@customer_name", namaArtis);
                    cmd.Parameters.AddWithValue("@order_type", FormMain.statusLabel);
                    cmd.ExecuteNonQuery();

                    // order_item
                    MySqlCommand cmd2 = new MySqlCommand("INSERT INTO order_items (order_id, product_id, quantity, price, total) " +
                    "VALUES (@1, @2, @3, @4, @5)", Connection.conn, transaction);
                    cmd2.Parameters.AddWithValue("@1", cmd.LastInsertedId);
                    cmd2.Parameters.AddWithValue("@2", 10.50);
                    cmd2.Parameters.AddWithValue("@3", "completed");
                    cmd2.Parameters.AddWithValue("@4", "Alice Brown");
                    cmd2.Parameters.AddWithValue("@5", FormPayment.grandTotal);
                    cmd2.ExecuteNonQuery();

                    MySqlCommand cmd3 = new MySqlCommand("INSERT INTO order_item_modifiers (order_item_modifier_id, " +
                        "order_item_id, modifier_id, quantity, price) " +
                    "VALUES (@1, @grand_total, @order_status, @customer_name, @order_type)", Connection.conn, transaction);
                    cmd3.Parameters.AddWithValue("@1", FormMain.idTransfer);
                    cmd3.Parameters.AddWithValue("@2", cmd2.LastInsertedId);
                    cmd3.Parameters.AddWithValue("@3", "completed");
                    cmd3.Parameters.AddWithValue("@4", "1");
                    cmd3.Parameters.AddWithValue("@5", FormPayment.grandTotal);
                    cmd3.ExecuteNonQuery();

                    transaction.Commit();
                }
                else
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE orders SET order_status = 'completed' " +
                    "WHERE order_id = @1", Connection.conn, transaction);
                    cmd.Parameters.AddWithValue("@1", FormPayment.orderId);
                    cmd.ExecuteNonQuery();

                    transaction.Commit();

                }
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                MessageBox.Show("Error : " + ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Connection.close();
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS
{
    public partial class FormCustomAllPayment : Form
    {

        public FormCustomAllPayment()
        {
            InitializeComponent();
        }

        private void FormCustomAllPayment_Load(object sender, EventArgs e)
        {
            CenterPanel();
            if(FormPayment.imgCustom == "cash")
            {
                button10.Visible = true;
                pictureBox1.Image = Properties.Resources.money;
            }
            else if(FormPayment.imgCustom == "card")
            {
                button10.Visible = false;
                textBox1.PasswordChar = '*';
                pictureBox1.Image = Properties.Resources.atm_card;
            }
            else
            {
                button10.Visible = false;
                pictureBox1.Image = Properties.Resources.payment_options;
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
            buttonEnter.Click += buttonEnter_Click;
            buttonClear.Click += buttonClear_Click;

        }
        public void CenterPanel()
        {
            panel1.Left = (this.Size.Width - panel1.Width) / 2;
            panel1.Top = ((this.Size.Height - panel1.Height) / 2);

        }
        public void cekLength()
        {
            if (FormPayment.imgCustom == "cash")
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
               if(textBox1.Text.Length > 3)
               {
                    textBox1.Text.ToString().Substring(textBox1.Text.Length-1);
               }
            }
            else
            {
                // misal ada 9 no telepon
                if(textBox1.Text.Length > 9)
                {
                    textBox1.Text.ToString().Substring(textBox1.Text.Length - 1);
                }
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
                int price = FormPayment.price;
                // pengecekan no telepon

                //kalau berhasil masuk kesini yah anggap aja no telepon pasti benar
                DialogResult dr =  MessageBox.Show("Anda yakin ingin membayar sebesar Rp. " + price + " ?", "Confirmation Payment", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if(dr == DialogResult.OK)
                {
                    // Pengecekan menggunakan transaction disini beserta sql nya dll

                    //jika berhasil
                    MessageBox.Show("Anda Berhasil Membayar Sebesar Rp. " + price , "Information Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
 
        }

        private void FormCustomAllPayment_Resize(object sender, EventArgs e)
        {
            CenterPanel();
        }
    }
}

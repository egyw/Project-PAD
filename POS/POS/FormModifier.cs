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
    public partial class FormModifier : Form
    {
        int idUser;
        public FormModifier(int id)
        {
            InitializeComponent();
            this.idUser = id;
            
            
        }

        private void pictureRefresh_Click(object sender, EventArgs e)
        {
            

        }

        private void FormModifier_Load(object sender, EventArgs e)
        {
           
        }
        private void SetPlaceholder(TextBox textBox, string placeholderText)
        {
            textBox.Text = placeholderText;
            textBox.ForeColor = Color.Gray;

            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholderText)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholderText;
                    textBox.ForeColor = Color.Gray;
                }
            };
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
            if (richTextBox1.Text == "")
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}

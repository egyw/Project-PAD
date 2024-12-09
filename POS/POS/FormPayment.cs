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

namespace POS
{
    public partial class FormPayment : Form
    {
        public static string textCustom = ""; 
        public static string imgCustom = "";
        public static string otherPayment = "";
        public static int price = 0;
        public FormPayment()
        {
            InitializeComponent();
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            CenterPanel();
        }

        public void CenterPanel()
        {
            panel4.Left = (this.Size.Width - panel4.Width) / 2;
            panel4.Top = ((this.Size.Height - panel4.Height) / 2) + this.Size.Height / 6;

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
        }

        private void buttonCashCustom_Click(object sender, EventArgs e)
        {
            imgCustom = "cash";
            FormCustomAllPayment fcpa = new FormCustomAllPayment();
            fcpa.ShowDialog();
            if(textCustom != "")
            {
                buttonCashCustom.Text = textCustom;
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

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

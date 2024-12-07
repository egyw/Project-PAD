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
    public partial class FormPayment : Form
    {
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

            panel1.Left = (this.Size.Width - panel1.Width) / 2;
            panel1.Top = ((this.Size.Height - panel1.Height) / 2) + panel1.Height / 6;


        }

        private void FormPayment_Resize(object sender, EventArgs e)
        {
            CenterPanel();
        }
    }
}

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
    public partial class FormCardEntry : Form
    {
        public FormCardEntry()
        {
            InitializeComponent();
        }

        private void FormCardEntry_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

        }
    }
}

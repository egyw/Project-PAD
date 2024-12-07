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
    public partial class FormWelcome : Form
    {
        int id;
        public FormWelcome(int id)
        {
            InitializeComponent();
            this.id = id;
            this.FormBorderStyle = FormBorderStyle.None;

            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.MaximumSize = new Size(screenWidth, screenHeight);
            this.MinimumSize = new Size(screenWidth, screenHeight);
        }

        private void FormWelcome_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            timerAnimation.Start();
        }

        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            if (logo.Size.Width < 1300 && logo.Size.Height < 550)
            {
                logo.Size = new Size(logo.Width + 5, logo.Height + 5);

                int x = (this.ClientSize.Width - logo.Width) / 2;
                int y = (this.ClientSize.Height - logo.Height) / 2;

                logo.Location = new Point(x, y);
            }
            else
            {
                timerAnimation.Stop();
                timerChangeForm.Start();
            }
        }

        private void timerChangeForm_Tick(object sender, EventArgs e)
        {
            FormMain form = new FormMain(id);
            form.FormClosed += formClosed;
            form.Show();
            timerChangeForm.Stop();

            this.Close();
        }

        private void formClosed(object sender, FormClosedEventArgs e)
        {
            FormLogin form = new FormLogin();
            form.Show();
        }
    }
}

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
    public partial class FormLogin : Form
    {
        bool isKeyboardActive = false;
        bool isTbUsernameActive = false;
        bool isTbPasswordActive = false;
        int formWidth;
        int formHeight;
        int panelWidth;
        int panelHeight;
        public FormLogin()
        {
            InitializeComponent();
            tbUsername.Text = "Username";
            tbUsername.ForeColor = Color.Gray;

            tbPassword.Text = "Password";
            tbPassword.ForeColor = Color.Gray;
            tbPassword.UseSystemPasswordChar = false;

            this.FormBorderStyle = FormBorderStyle.None;

            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.MaximumSize = new Size(screenWidth, screenHeight);
            this.MinimumSize = new Size(screenWidth, screenHeight);

            panelKeyboard.Width = panelBelow.Width;
            panelKeyboard.Height = panelBelow.Height;

            panelBelow.Height = 0;
        }
        private void FormLogin_Load(object sender, EventArgs e)
        {
            label1.Select();
            this.FormBorderStyle = FormBorderStyle.Sizable;

            panelWidth = panel1.Width;
            panelHeight = panel1.Height;

            formWidth = this.Width;
            formHeight = this.Height;

            panel1.Location = new Point((formWidth - panelWidth) / 2, (formHeight - panelHeight) / 2);

            panelBelow.Location = new Point((formWidth - panelWidth) / 2, panelBelow.Location.Y);
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbUsername.Text) && !string.IsNullOrWhiteSpace(tbPassword.Text) && tbUsername.Text != "Username" && tbPassword.Text != "Password")
            {
                try
                {
                    Connection.open();

                    string username = tbUsername.Text.ToString();
                    string password = tbPassword.Text.ToString();

                    MySqlCommand cmd = new MySqlCommand("SELECT * " +
                        "FROM users " +
                        "WHERE username = @username AND PASSWORD = @password", Connection.conn);
                    DataTable dt = new DataTable();
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        int id = Convert.ToInt32(reader["user_id"]);

                        
                        FormWelcome form = new FormWelcome(id);
                        form.Show();

                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Account is not found!");
                        clear();
                    }
                    clear();
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    clear();
                }
                finally
                {
                    Connection.close();
                }
            }
            else
            {
                MessageBox.Show("The input cannot be empty!");
                clear();
            }
            removeKeyboard();
        }

        private void clear()
        {
            tbUsername.Clear();
            tbPassword.Clear();
            tbUsername.Text = "Username";
            tbUsername.ForeColor = Color.Gray;

            tbPassword.Text = "Password";
            tbPassword.ForeColor = Color.Gray;
            tbPassword.UseSystemPasswordChar = false;

            isTbPasswordActive = false;
            isTbUsernameActive = false;
            isKeyboardActive = false;
        }

        private void tbUsername_Enter(object sender, EventArgs e)
        {
            if (tbUsername.Text == "Username")
            {
                tbUsername.Text = "";
                tbUsername.ForeColor = Color.Black;
            }

            Keyboard.clearKeyboard();
            addKeyboard(tbUsername);

            isTbPasswordActive = false;
            isTbUsernameActive = true;
        }

        private void tbUsername_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUsername.Text) && isKeyboardActive == false)
            {
                tbUsername.Text = "Username"; 
                tbUsername.ForeColor = Color.Gray;
            }
        }

        private void tbPassword_Enter(object sender, EventArgs e)
        {
            if (tbPassword.Text == "Password")
            {
                tbPassword.Text = "";
                tbPassword.ForeColor = Color.Black;
                tbPassword.UseSystemPasswordChar = true;
            }
            Keyboard.clearKeyboard();
            addKeyboard(tbPassword);

            isTbPasswordActive = true;
            isTbUsernameActive = false;
        }

        private void tbPassword_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbPassword.Text) && isKeyboardActive == false)
            {
                tbPassword.Text = "Password";
                tbPassword.ForeColor = Color.Gray;
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void FormLogin_Click(object sender, EventArgs e)
        {
            removeKeyboard();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            removeKeyboard();
        }

        private void addKeyboard(TextBox textBox)
        {
            panelBelow.Height = 350;
            Keyboard.addKeyboard(this, panelKeyboard, textBox, panelBelow);
            panel1.Location = new Point((formWidth - panelWidth) / 2, (formHeight - panelHeight) / 2 - 200);
            isKeyboardActive = true;
        }

        private void removeKeyboard()
        {
            Keyboard.clearKeyboard();
            panel1.Location = new Point((formWidth - panelWidth) / 2, (formHeight - panelHeight) / 2);
            label1.Select();
            isKeyboardActive = false;

            if (isTbUsernameActive)
            {
                if(tbUsername.Text == "")
                {
                    tbUsername.Text = "Username";
                    tbUsername.ForeColor = Color.Gray;
                }
            }
            else if (isTbPasswordActive)
            {
                if (tbPassword.Text == "")
                {
                    tbPassword.Text = "Password";
                    tbPassword.ForeColor = Color.Gray;
                    tbPassword.UseSystemPasswordChar = false;
                }
            }

            isTbPasswordActive = false;
            isTbUsernameActive = false;

            panelBelow.Height = 0;
        }
    }
}

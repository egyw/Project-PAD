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
    public partial class FormAdminEgy : Form
    {
        string filter = "";
        int selectedIndex = -1;
        DataTable tableUsers;
        int idUser = -1;
        public FormAdminEgy()
        {
            InitializeComponent();
            button1.BackColor = Color.White;
            btnUpd.Text = "...";
            this.FormBorderStyle = FormBorderStyle.None;

            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.MaximumSize = new Size(screenWidth, screenHeight);
            this.MinimumSize = new Size(screenWidth, screenHeight);

            labelAdmin.Location = new Point(panelTop.Width - labelAdmin.Width, 18);
            label2.Location = new Point(panelTop.Width - label2.Width, 50);

            dgvUsers.Size = new Size(panelUsers.Width - dgvUsers.Location.X - 28, panelUsers.Height - dgvUsers.Location.Y - 300);
            int btnY = (dgvUsers.Height + dgvUsers.Location.Y) + 10;
            btnUpd.Location = new Point((dgvUsers.Width + dgvUsers.Location.X) - btnUpd.Width, btnY);
            btnDelete.Location = new Point(btnUpd.Location.X - btnDelete.Width, btnY);
            btnAddUser.Location = new Point(btnDelete.Location.X - btnAddUser.Width, btnY);

            groupBox1.Location = new Point(dgvUsers.Location.X, btnY);
        }

        private void FormAdminEgy_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            loadDGVUsers("");
        }

        private void tbUser_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUser.Text))
            {
                loadDGVUsers("");
            }
            else
            {
                loadDGVUsers(tbUser.Text);
            }
        }

        private void loadDGVUsers(string filter)
        {
            try
            {
                Connection.open();
                MySqlDataAdapter data;

                if (filter == "")
                {
                    data = new MySqlDataAdapter("SELECT * FROM users WHERE delete_Status = FALSE", Connection.conn);
                }
                else
                {
                    data = new MySqlDataAdapter("SELECT * FROM users WHERE firstName LIKE @fName OR lastName LIKE @lName AND delete_Status = FALSE", Connection.conn);
                    data.SelectCommand.Parameters.AddWithValue("@fName", filter + "%");
                    data.SelectCommand.Parameters.AddWithValue("@lName", filter + "%");
                }

                tableUsers = new DataTable();
                data.Fill(tableUsers);
                dgvUsers.DataSource = tableUsers;
                dgvUsers.Columns["user_id"].Visible = false;
                dgvUsers.Columns["delete_status"].Visible = false;
                dgvUsers.Columns["deleted_at"].Visible = false;

                dgvUsers.Columns["user_id"].HeaderText = "ID";
                dgvUsers.Columns["firstName"].HeaderText = "First Name";
                dgvUsers.Columns["lastName"].HeaderText = "Last Name";
                dgvUsers.Columns["PASSWORD"].HeaderText = "Password";
                dgvUsers.Columns["ROLE"].HeaderText = "Role";
                dgvUsers.Columns["phone_number"].HeaderText = "Phone Number";
                dgvUsers.Columns["isActive"].HeaderText = "Is Active";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.close();
            }
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                resetFormAddUser();
                selectedIndex = e.RowIndex;
                btnUpd.Enabled = true;
                btnDelete.Enabled = true;
                groupBox1.Enabled = false;
                try
                {
                    Connection.open();
                    idUser = Convert.ToInt32(dgvUsers.Rows[selectedIndex].Cells["user_id"].Value);
                    string status = dgvUsers.Rows[selectedIndex].Cells["isActive"].Value.ToString();
                    if(status == "True") { status = "Disable"; }
                    else { status = "Enable"; }
                    btnUpd.Text = status;
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

        private void btnUpd_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.open();
                bool status;
                if (btnUpd.Text == "Enable") { status = true; }
                else { status = false; }
                MySqlCommand cmd = new MySqlCommand("UPDATE users SET isActive = @status WHERE user_id = @id", Connection.conn);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@id", idUser);
                cmd.ExecuteNonQuery();
                loadDGVUsers(filter);
                reset();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.close();
            }
        }

        private void reset()
        {
            dgvUsers.ClearSelection();
            selectedIndex = -1;
            idUser = -1;
            btnDelete.Enabled = false;
            btnUpd.Enabled = false;
            btnUpd.Text = "...";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.open();
                MySqlCommand cmd = new MySqlCommand("UPDATE users SET delete_status = TRUE, deleted_at = NOW() WHERE user_id = @id", Connection.conn);
                cmd.Parameters.AddWithValue("@id", idUser);
                cmd.ExecuteNonQuery();
                loadDGVUsers(filter);
                reset();
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

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            reset();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && comboBox1.Text != "")
            {
                try
                {
                    Connection.open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO users (firstName, lastName, username, PASSWORD, ROLE, email, phone_number) VALUES (@firstName, @lastName, @username, @password, @role, @email, @phone_number)", Connection.conn);
                    cmd.Parameters.AddWithValue("@firstName", textBox1.Text);
                    cmd.Parameters.AddWithValue("@lastName", textBox2.Text);
                    cmd.Parameters.AddWithValue("@username", textBox3.Text);
                    cmd.Parameters.AddWithValue("@password", textBox4.Text);
                    cmd.Parameters.AddWithValue("@phone_number", textBox5.Text);
                    cmd.Parameters.AddWithValue("@email", textBox6.Text);
                    cmd.Parameters.AddWithValue("@role", comboBox1.Text);
                    cmd.ExecuteNonQuery();
                    resetFormAddUser();
                    reset();
                    loadDGVUsers(filter);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Connection.close();
                }
            }
            else
            {
                MessageBox.Show("inputan tidak boleh kosong!");
                resetFormAddUser();
            }
        }

        private void resetFormAddUser()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            comboBox1.SelectedIndex = -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            resetChosenMenu();
            button1.BackColor = Color.White;
            panah1.Visible = true;
            panelUsers.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            resetChosenMenu();
            button2.BackColor = Color.White;
            panah2.Visible = true;
            panelUsers.Visible = true;
            panelProducts.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            resetChosenMenu();
            button3.BackColor = Color.White;
            panah3.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            resetChosenMenu();
            button4.BackColor = Color.White;
            panah4.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            resetChosenMenu();
            button5.BackColor = Color.White;
            panah5.Visible = true;
        }

        private void resetChosenMenu()
        {
            button1.BackColor = SystemColors.Control;
            button2.BackColor = SystemColors.Control;
            button3.BackColor = SystemColors.Control;
            button4.BackColor = SystemColors.Control;
            button5.BackColor = SystemColors.Control;

            panah1.Visible = false;
            panah2.Visible = false;
            panah3.Visible = false;
            panah4.Visible = false;
            panah5.Visible = false;

            panelUsers.Visible = false;
            panelProducts.Visible = false;
        }
    }
}

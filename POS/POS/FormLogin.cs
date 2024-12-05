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
        public FormLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbUsername.Text != "" && tbPassword.Text != "")
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
                        string role = reader["role"].ToString(); // Mengambil role user

                        // Lakukan aksi setelah login berhasil (misalnya membuka form utama)
                        if (role == "admin")
                        {
                            this.Hide();

                            FormAdmin form = new FormAdmin();
                            form.FormClosed += formClosed;
                            form.Show();
                        }
                        else
                        {
                            this.Hide();

                            POS form = new POS();
                            form.FormClosed += formClosed;
                            form.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Akun tidak ditemukan!");
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
                MessageBox.Show("Inputan tidak boleh kosong!");
                clear();
            }
        }

        private void clear()
        {
            tbUsername.Clear();
            tbPassword.Clear();
        }

        private void formClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }
    }
}

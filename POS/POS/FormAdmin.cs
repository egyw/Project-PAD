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
    public partial class FormAdmin : Form
    {
        public int row = -1;
        public int column = -1;
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            CenterPanel();
            loadData();
            SetPlaceholder(textBox1, "Search Here");
            SetComboBoxPlaceholder(comboBox1, "Filter");
        }

        public void CenterPanel()
        {

            panel1.Left = (this.Size.Width - panel1.Width) / 2;
            panel1.Top = (this.Size.Height - panel1.Height) / 2;
        }

        public void loadData()
        {
            try
            {
                Connection.open();
                MySqlDataAdapter adt = new MySqlDataAdapter("SELECT u.user_id as User_Id, u.firstName as FirstName, u.lastName as LastName, u.username as Username, u.PASSWORD as Password, " +
                    "u.ROLE as Role, u.email as Email, u.phone_number as Phone, u.isActive as Active " +
                    "FROM users u WHERE u.role != 'admin'", Connection.conn);
                DataTable dt = new DataTable();
                adt.Fill(dt);
                dataGridView1.DataSource = dt;

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Connection.close();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (row < 0)
            {
                MessageBox.Show("Pilih data nya terlebih dahulu! Dengan Double Click!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Connection.open();
                string id = dataGridView1.Rows[row].Cells[0].Value.ToString();
                MySqlCommand cmd = new MySqlCommand("Update Users Set isActive = @1 where user_id = @2", Connection.conn);
                cmd.Parameters.AddWithValue("@1", 0);
                cmd.Parameters.AddWithValue("@2", id);
                cmd.ExecuteNonQuery();
                loadData();
                row = -1;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Message Error" + ex.Message);
            }
            finally
            {
                Connection.close();
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            row = e.RowIndex;
            column = e.ColumnIndex;
            string getNm = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

            if(dataGridView1.Rows[row].Cells[8].Value.ToString() == "False")
            {
               DialogResult confirm =  MessageBox.Show("Are You Sure Want To Enable " + getNm + " ? ", "Information", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if(confirm == DialogResult.OK)
                {
                    try
                    {
                        Connection.open();
                        string id = dataGridView1.Rows[row].Cells[0].Value.ToString();
                        MySqlCommand cmd = new MySqlCommand("Update Users Set isActive = @1 where user_id = @2", Connection.conn);
                        cmd.Parameters.AddWithValue("@1", 1);
                        cmd.Parameters.AddWithValue("@2", id);
                        cmd.ExecuteNonQuery();
                        loadData();
                        row = -1;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Message Error" + ex.Message);
                    }
                    finally
                    {
                        Connection.close();
                    }
                }
            }
            
        }

        private void FormAdmin_Resize(object sender, EventArgs e)
        {
            CenterPanel();
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

        private void SetComboBoxPlaceholder(ComboBox comboBox, string placeholderText)
        {
            comboBox.Text = placeholderText;
            comboBox.ForeColor = Color.Gray;

            comboBox.Enter += (s, e) =>
            {
                if (comboBox.Text == placeholderText)
                {
                    comboBox.Text = "";
                    comboBox.ForeColor = Color.Black;
                }
            };

            comboBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(comboBox.Text))
                {
                    comboBox.Text = placeholderText;
                    comboBox.ForeColor = Color.Gray;
                }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            SetPlaceholder(textBox1, "Search Here");
            SetComboBoxPlaceholder(comboBox1, "Filter");
            loadData();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Connection.open();
                string searchValue = textBox1.Text;
                string query = @"
                SELECT u.user_id as User_Id, u.firstName as FirstName, u.lastName as LastName, 
                       u.username as Username, u.PASSWORD as Password, u.ROLE as Role, 
                       u.email as Email, u.phone_number as Phone 
                FROM users u
                WHERE (
                          `user_id` LIKE @searchValue
                       OR `firstName` LIKE @searchValue
                       OR `lastName` LIKE @searchValue
                       OR `username` LIKE @searchValue
                       OR `PASSWORD` LIKE @searchValue
                       OR `ROLE` LIKE @searchValue
                       OR `email` LIKE @searchValue
                       OR `phone_number` LIKE @searchValue
                      )
                   AND u.role != 'admin'";
                MySqlDataAdapter adt = new MySqlDataAdapter(query, Connection.conn);
                adt.SelectCommand.Parameters.AddWithValue("@searchValue", "%" + searchValue + "%");

                DataTable dt = new DataTable();
                adt.Fill(dt);

                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Message Error" + ex.Message);
            }
            finally
            {
                Connection.close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = comboBox1.Text;
            List<string> validColumns = new List<string> { "user_id", "firstName", "lastName", "username", "PASSWORD", "ROLE", "email", "phone_number" };
            try
            {
                Connection.open();
                MySqlDataAdapter adt = new MySqlDataAdapter($"SELECT {text} " +
                    "FROM users u WHERE u.role != 'admin'", Connection.conn);
                DataTable dt = new DataTable();
                adt.Fill(dt);
                dataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Connection.close();
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var cekTrue = dataGridView1.Rows[e.RowIndex].Cells[8].Value;
            if (cekTrue.ToString() == "True") 
            {
                dataGridView1.Rows[e.RowIndex].Cells[8].Style.BackColor = Color.LightGreen;
            }
            else
            {
                dataGridView1.Rows[e.RowIndex].Cells[8].Style.BackColor = Color.Red;
            }
        }
    }
}

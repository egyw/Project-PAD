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
                    "FROM users u WHERE u.role != 'admin' and u.isActive = 1", Connection.conn);
                DataTable dt = new DataTable();
                adt.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[8].Visible = false;

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
            string getId = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            MessageBox.Show(row + " " + column);
            
        }
    }
}

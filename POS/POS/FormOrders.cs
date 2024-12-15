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
    public partial class FormOrders : Form
    {
        DataTable tableAllOrders;
        bool isKeyboardActive = false;
        public FormOrders()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;

            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.MaximumSize = new Size(screenWidth, screenHeight);
            this.MinimumSize = new Size(screenWidth, screenHeight);

            dataGridView1.Size = new Size(screenWidth, screenHeight - panelTop.Height);
            dataGridView1.Location = new Point(0, panelTop.Height);

            int buttonWidth = panelRight.Width - 40;

            btnPay.Width = buttonWidth;
            btnEdit.Width = buttonWidth;
            btnDelete.Width = buttonWidth;

            btnPay.Height = 70;
            btnEdit.Height = 70;
            btnDelete.Height = 70;

            int spacing = 20;
            btnPay.Location = new Point(20, 55); 
            btnEdit.Location = new Point(20, btnPay.Location.Y + btnPay.Height + spacing);
            btnDelete.Location = new Point(20, btnEdit.Location.Y + btnEdit.Height + spacing);

            panelRight.Width = 0;
        }

        private void FormOrders_Load(object sender, EventArgs e)
        {
            label3.Select();
            this.FormBorderStyle = FormBorderStyle.Sizable;
            int centerX = (this.ClientSize.Width - panelTopMiddle.Width) / 2;

            panelTopMiddle.Location = new Point(centerX, panelTopMiddle.Location.Y);
            
            loadDGV("");
        }

        private void pictureRefresh_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void loadDGV(string namaCustomer)
        {
            try
            {
                Connection.open();
                MySqlDataAdapter data;
                if (string.IsNullOrWhiteSpace(namaCustomer))
                {
                    data = new MySqlDataAdapter(
                    "SELECT o.order_id, o.order_date, o.grand_total, o.customer_name, u.firstName " +
                    "FROM orders o " +
                    "JOIN payments p ON p.order_id = o.order_id " +
                    "JOIN users u ON o.user_id = u.user_id " +
                    "WHERE p.payment_status = 'pending'", Connection.conn);
                    tableAllOrders = new DataTable();
                }
                else
                {
                    data = new MySqlDataAdapter(
                    "SELECT o.order_id, o.order_date, o.grand_total, o.customer_name, u.firstName " +
                    "FROM orders o " +
                    "JOIN payments p ON p.order_id = o.order_id " +
                    "JOIN users u ON o.user_id = u.user_id " +
                    "WHERE p.payment_status = 'pending' AND o.customer_name LIKE @name", Connection.conn);
                    data.SelectCommand.Parameters.AddWithValue("@name", namaCustomer + "%");
                    tableAllOrders = new DataTable();
                }
                data.Fill(tableAllOrders);
                dataGridView1.DataSource = tableAllOrders;
                dataGridView1.ClearSelection();
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

        private void addKeyboard(TextBox textBox)
        {
            Keyboard.OnKeyboardClosed += buttonClosePressed;
            panelKbContainer.Height = 350;
            Keyboard.addKeyboard(this, panelKeyboard, textBox, panelKbContainer, panelKbContainer.Width);
            isKeyboardActive = true;
        }

        private void buttonClosePressed()
        {
            panelKbContainer.Height = 0;
            isKeyboardActive = false;
            Keyboard.OnKeyboardClosed -= buttonClosePressed;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            addKeyboard(textBox1);
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (isKeyboardActive == false)
            {
                panelKbContainer.Height = 0;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            loadDGV(textBox1.Text);
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "order_id")
            {
                dataGridView1.Columns["order_id"].Width = 40;
                dataGridView1.Columns["order_id"].HeaderText = "ID";
                dataGridView1.Columns["order_date"].HeaderText = "Placed";
                dataGridView1.Columns["grand_total"].HeaderText = "Total";
                dataGridView1.Columns["customer_name"].HeaderText = "Customer";
                dataGridView1.Columns["firstName"].HeaderText = "Employee";
            }

            if (dataGridView1.Columns[e.ColumnIndex].Name == "order_date")
            {
                DateTime orderDate = (DateTime)e.Value;

                e.Value = $"{orderDate:HH:mm}\n{orderDate:dd/MM/yyyy}";

                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.WrapMode = DataGridViewTriState.True;
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {
            panelRight.Width = 0;
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            timerAnimateRightPanel.Start();
        }

        private void timerAnimateRightPanel_Tick(object sender, EventArgs e)
        {
            if(panelRight.Width < 517)
            {
                panelRight.Width += 40;
            }
            else
            {
                timerAnimateRightPanel.Stop();
            }
        }
    }
}

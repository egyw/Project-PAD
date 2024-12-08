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
    public partial class FormMain : Form
    {
        int idUser;
        DataTable dt;
        DataTable td;
        public FormMain(int id)
        {
            InitializeComponent();
            this.idUser = id;

            this.FormBorderStyle = FormBorderStyle.None;

            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.MaximumSize = new Size(screenWidth, screenHeight);
            this.MinimumSize = new Size(screenWidth, screenHeight);
            try
            {
                Connection.open();
                dt = new DataTable();

                string command = "select * from products";
               
               MySqlDataAdapter adp = new MySqlDataAdapter(command, Connection.conn);

                adp.Fill(dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.close();
            }

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            label1.Select();
            this.FormBorderStyle = FormBorderStyle.Sizable;
            int centerX = (this.ClientSize.Width - panelTopMiddle.Width) / 2;

            panelTopMiddle.Location = new Point(centerX, panelTopMiddle.Location.Y);

            int buttonX = this.ClientSize.Width - btnOrders.Width - 4;  
            int buttonY = 4;  

            btnOrders.Location = new Point(buttonX, buttonY);


            loadMenu("");
        }

        public void loadMenu(string nama)
        {
            try
            {
                Connection.open();
                td = new DataTable();
                string command = "";
                if (string.IsNullOrWhiteSpace(nama)) 
                {
                    command = "select * from products";
                }
                else 
                {
                    command = "select * from products where product_name like @1";
                }
                MySqlCommand cmd = new MySqlCommand(command,Connection.conn);
                cmd.Parameters.AddWithValue("@1", "%" + nama + "%");

                MySqlDataReader reader = cmd.ExecuteReader();

                td.Load(reader);

                panelRight.Controls.Clear();

                int x = 22;
                int y = 26;
                int count = 0;

                foreach (DataRow row in td.Rows)
                {
                    string productName = row["product_name"].ToString();
                    bool isActive = Convert.ToBoolean(row["is_active"]);
                    Button btn = new Button
                    {
                        Text = productName,
                        Size = new Size(212, 46),
                        Location = new Point(x, y),
                        BackColor = Color.White,
                        FlatStyle = FlatStyle.Flat
                    };

                    if (isActive)
                    {
                        btn.FlatAppearance.BorderColor = Color.Green;
                    }
                    else
                    {
                        btn.FlatAppearance.BorderColor = Color.Red;
                    }
                    btn.FlatAppearance.BorderSize = 2;

                    panelRight.Controls.Add(btn);

                    count++;
                    if (count % 4 == 0)
                    {
                        x = 22; 
                        y += 68; 
                    }
                    else
                    {
                        x += 233;
                    }
                }
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

        private void pictureRefresh_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            loadMenu(textBox1.Text);
        }
    }
}

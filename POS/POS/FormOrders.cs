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
        public FormOrders()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;

            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            this.MaximumSize = new Size(screenWidth, screenHeight);
            this.MinimumSize = new Size(screenWidth, screenHeight);
        }

        private void FormOrders_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void loadOrders()
        {
            try
            {
                Connection.open();
                MySqlDataAdapter data = new MySqlDataAdapter(
                    "SELECT o.order_id, o.order_date, o.grand_total, o.customer_name " +
                    "FROM orders o " +
                    "JOIN payments p ON p.order_id = o.order_id " +
                    "WHERE p.payment_status = 'pending'", Connection.conn);
                data.Fill(tableAllOrders);

                foreach(DataRow row in tableAllOrders.Rows)
                {
                    Panel newPanel = new Panel
                    {
                        
                    };
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace POS
{
    class Connection
    {
        private static readonly string data = "Server=localhost; Database=pos_aw; User Id=root; Password=";
        public static MySqlConnection conn;

        public static void open()
        {
            if (conn == null)
            {
                conn = new MySqlConnection(data);
            }

            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public static void close()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}

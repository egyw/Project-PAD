using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace POS
{
    public partial class FormReport : Form
    {
        public int orderid;
        public FormReport(int orderid)
        {
            InitializeComponent();
            this.orderid = orderid;
        }
        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {
        }
        private void FormReport_Load(object sender, EventArgs e)
        {
            try
            {
                CrystalReport1 report = new CrystalReport1();

                // Set parameter orderid untuk report utama
                ParameterFieldDefinitions parameterFields = report.DataDefinition.ParameterFields;
                ParameterFieldDefinition parameterField = parameterFields["orderid"];
                ParameterValues parameterValues = new ParameterValues();
                ParameterDiscreteValue parameterValue = new ParameterDiscreteValue();
                parameterValue.Value = orderid;
                parameterValues.Add(parameterValue);
                parameterField.ApplyCurrentValues(parameterValues);


                Connection.open();
                string sql = "SELECT order_item_id FROM order_items WHERE order_id = @orderid";
                MySqlCommand cmd = new MySqlCommand(sql, Connection.conn);
                cmd.Parameters.AddWithValue("@orderid", orderid);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int order_item_id = reader.GetInt32(0);

                        // Set parameter untuk setiap order_item_id
                        ReportDocument subreport = report.OpenSubreport("CrystalReport2.rpt");
                        ParameterFieldDefinitions subParamFields = subreport.DataDefinition.ParameterFields;
                        ParameterFieldDefinition subParamField = subParamFields["itemku"];
                        ParameterValues subParamValues = new ParameterValues();
                        ParameterDiscreteValue subParamValue = new ParameterDiscreteValue();
                        subParamValue.Value = order_item_id;
                        subParamValues.Add(subParamValue);
                        subParamField.ApplyCurrentValues(subParamValues);
                    }
                }

                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                Connection.close();
            }
        }
    }
}
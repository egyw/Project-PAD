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

                ParameterFieldDefinitions parameterFields = report.DataDefinition.ParameterFields;
                ParameterFieldDefinition parameterField = parameterFields["orderid"];
                ParameterValues parameterValues = new ParameterValues();
                ParameterDiscreteValue parameterValue = new ParameterDiscreteValue();
                parameterValue.Value = orderid;
                parameterValues.Add(parameterValue);
                parameterField.ApplyCurrentValues(parameterValues);
            
                crystalReportViewer1.ReportSource = report;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace BlueCompanyGamification
{
    public partial class dash : Form
    {
       public string workerId { get; set; }
        public dash(string workerIdFromDashboard)
        {
            InitializeComponent();
            workerId = workerIdFromDashboard;
        }


        private void dash_Load(object sender, EventArgs e)
        {

 
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_users.mdb";


            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("SELECT points FROM tbl_users WHERE workerid=@workerId", connection);
                command.Parameters.AddWithValue("@workerId", workerId);

                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    int points = Convert.ToInt32(result);
                    label4.Text = "Puncte: " + points.ToString();
                }
                else
                {
                    label4.Text = "Puncte: 0";
                }
            }
        }
    }
}

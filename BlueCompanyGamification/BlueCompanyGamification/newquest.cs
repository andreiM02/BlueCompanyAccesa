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
    public partial class newquest : Form
    {
        public string workerId { get; set; }
        public newquest(string workerIdFromDashboard)
        {
            InitializeComponent();
            workerId = workerIdFromDashboard;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Get the quest name and code from the textboxes and richtextbox
            string questname = textBox1.Text;
            string questcode = richTextBox1.Text;

            DateTime currentDate = DateTime.Now;

            string deductPointsQuery = "UPDATE tbl_users SET points = points - 100 WHERE workerid = @workerid";
            OleDbConnection connection = new OleDbConnection();
            using (OleDbCommand command = new OleDbCommand(deductPointsQuery, connection))
            {
                command.Parameters.AddWithValue("@workerid", workerId);
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    MessageBox.Show("Error deducting points from user's account");
                    return;
                }
            }
        }
        
    }
}

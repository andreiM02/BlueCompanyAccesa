using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
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
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_users.mdb";
            // Get the quest name and code from the textboxes and richtextbox
            string questname = textBox1.Text;
            string questcode = richTextBox1.Text;
            string workerid = workerId;

            DateTime currentDate = DateTime.Now;
            string currentdateformat = currentDate.ToString("MM-dd-yyyy");

            string Query = "INSERT INTO quest_user (questcode, questname, workerid, dateposted) VALUES (@questcode, @questname, @workerid, @dateposted)";
            OleDbConnection connection = new OleDbConnection(connectionString);
            using OleDbCommand command = new OleDbCommand(Query, connection);
            command.Parameters.AddWithValue("@questcode", questcode);
            command.Parameters.AddWithValue("@questname", questname);
            command.Parameters.AddWithValue("@workerid", workerid);
            command.Parameters.AddWithValue("@dateposted", currentdateformat);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected != 0)
                {
                    string updateQuery = "UPDATE tbl_users SET points = points - 100 WHERE workerid = @workerid";
                    OleDbCommand updateCommand = new OleDbCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@workerid", workerId);
                    int updateRowsAffected = updateCommand.ExecuteNonQuery();
                    if (updateRowsAffected > 0) 
                    {
                        MessageBox.Show("Quest Posted and 100 points deducted from your account!");
                    }
                    else 
                    {
                        MessageBox.Show("Error deducting points from your account.");
                    }
                }
                else
                {
                    MessageBox.Show("Error posting the quest!");
                }
            }
            catch(OleDbException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            richTextBox1.Clear();
            textBox1.Focus();
        }
    }
}

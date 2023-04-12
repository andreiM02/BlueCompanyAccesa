using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueCompanyGamification
{
    public partial class badges : Form
    {
        private const string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_users.mdb";
        public string workerId { get; set; }
        public badges(string workerIdFromDashboard)
        {
            InitializeComponent();
            workerId = workerIdFromDashboard;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Retrieve user's current points from the database
                connection.Open();
                string pointsQuery = "SELECT points FROM tbl_users WHERE workerid = @workerid";
                OleDbCommand pointsCommand = new OleDbCommand(pointsQuery, connection);
                pointsCommand.Parameters.AddWithValue("@workerid", workerId);
                int currentPoints = Convert.ToInt32(pointsCommand.ExecuteScalar());

                if (currentPoints < 5000)
                {
                    // User does not have enough points to purchase the badge
                    MessageBox.Show("You do not have enough points to buy this badge!");
                }
                else
                {
                    // User has enough points to purchase the badge
                    string updateQuery = "UPDATE tbl_users SET badge1 = 1, points = @points WHERE workerid = @workerid AND badge1 = 0";
                    OleDbCommand updateCommand = new OleDbCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@points", currentPoints - 5000);
                    updateCommand.Parameters.AddWithValue("@workerid", workerId);
                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Badge purchased successfully
                        MessageBox.Show("Badge purchased successfully");
                    }
                    else
                    {
                        // User already owns this badge
                        MessageBox.Show("You already own this badge!");
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Retrieve user's current points from the database
                connection.Open();
                string pointsQuery = "SELECT points FROM tbl_users WHERE workerid = @workerid";
                OleDbCommand pointsCommand = new OleDbCommand(pointsQuery, connection);
                pointsCommand.Parameters.AddWithValue("@workerid", workerId);
                int currentPoints = Convert.ToInt32(pointsCommand.ExecuteScalar());

                if (currentPoints < 15000)
                {
                    // User does not have enough points to purchase the badge
                    MessageBox.Show("You do not have enough points to buy this badge!");
                }
                else
                {
                    // User has enough points to purchase the badge
                    string updateQuery = "UPDATE tbl_users SET badge2 = 1, points = @points WHERE workerid = @workerid AND badge2 = 0";
                    OleDbCommand updateCommand = new OleDbCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@points", currentPoints - 15000);
                    updateCommand.Parameters.AddWithValue("@workerid", workerId);
                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Badge purchased successfully
                        MessageBox.Show("Badge purchased successfully");
                    }
                    else
                    {
                        // User already owns this badge
                        MessageBox.Show("You already own this badge!");
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Retrieve user's current points from the database
                connection.Open();
                string pointsQuery = "SELECT points FROM tbl_users WHERE workerid = @workerid";
                OleDbCommand pointsCommand = new OleDbCommand(pointsQuery, connection);
                pointsCommand.Parameters.AddWithValue("@workerid", workerId);
                int currentPoints = Convert.ToInt32(pointsCommand.ExecuteScalar());

                if (currentPoints < 45000)
                {
                    // User does not have enough points to purchase the badge
                    MessageBox.Show("You do not have enough points to buy this badge!");
                }
                else
                {
                    // User has enough points to purchase the badge
                    string updateQuery = "UPDATE tbl_users SET badge3 = 1, points = @points WHERE workerid = @workerid AND badge3 = 0";
                    OleDbCommand updateCommand = new OleDbCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@points", currentPoints - 45000);
                    updateCommand.Parameters.AddWithValue("@workerid", workerId);
                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Badge purchased successfully
                        MessageBox.Show("Badge purchased successfully");
                    }
                    else
                    {
                        // User already owns this badge
                        MessageBox.Show("You already own this badge!");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Retrieve user's current points from the database
                connection.Open();
                string pointsQuery = "SELECT points FROM tbl_users WHERE workerid = @workerid";
                OleDbCommand pointsCommand = new OleDbCommand(pointsQuery, connection);
                pointsCommand.Parameters.AddWithValue("@workerid", workerId);
                int currentPoints = Convert.ToInt32(pointsCommand.ExecuteScalar());

                if (currentPoints < 100000)
                {
                    // User does not have enough points to purchase the badge
                    MessageBox.Show("You do not have enough points to buy this badge!");
                }
                else
                {
                    // User has enough points to purchase the badge
                    string updateQuery = "UPDATE tbl_users SET badge4 = 1, points = @points WHERE workerid = @workerid AND badge4 = 0";
                    OleDbCommand updateCommand = new OleDbCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@points", currentPoints - 100000);
                    updateCommand.Parameters.AddWithValue("@workerid", workerId);
                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Badge purchased successfully
                        MessageBox.Show("Badge purchased successfully");
                    }
                    else
                    {
                        // User already owns this badge
                        MessageBox.Show("You already own this badge!");
                    }
                }
            }
        }
    }
}

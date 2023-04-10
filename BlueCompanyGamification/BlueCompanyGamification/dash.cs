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
                OleDbCommand command = new OleDbCommand("SELECT points,score,nume,prenume FROM tbl_users WHERE workerid=@workerId", connection);
                command.Parameters.AddWithValue("@workerId", workerId);

                //object result = command.ExecuteScalar();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int points = Convert.ToInt32(reader["points"]);
                    int score = Convert.ToInt32(reader["score"]);

                    label4.Text = points.ToString();
                    label6.Text = score.ToString();
                    label7.Text = workerId.ToString();
                }
                else
                {
                    label4.Text = "0";
                    label6.Text = "0";
                    label7.Text = "salut";
                }
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

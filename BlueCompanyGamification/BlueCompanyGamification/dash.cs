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
                OleDbCommand command = new OleDbCommand("SELECT points,score,badge1,badge2,badge3,badge4,nume,prenume FROM tbl_users WHERE workerid=@workerId", connection);
                command.Parameters.AddWithValue("@workerId", workerId);

                //object result = command.ExecuteScalar();
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int points = Convert.ToInt32(reader["points"]);
                    int score = Convert.ToInt32(reader["score"]);
                    int badge1 = Convert.ToInt32(reader["badge1"]);
                    int badge2 = Convert.ToInt32(reader["badge2"]);
                    int badge3 = Convert.ToInt32(reader["badge3"]);
                    int badge4 = Convert.ToInt32(reader["badge4"]);

                    label4.Text = points.ToString();
                    label6.Text = score.ToString();
                    label7.Text = workerId.ToString();

                    if (badge1 == 0)
                    {
                        badge1picture.Visible = true;
                        pictureBox1.Visible = false;
                    }
                    else
                    {
                        badge1picture.Visible = false;
                        pictureBox1.Visible = true;
                    }
                    if (badge2 == 0)
                    {
                        pictureBox2.Visible = true;
                        pictureBox5.Visible = false;
                    }
                    else
                    {
                        pictureBox2.Visible = false;
                        pictureBox5.Visible = true;
                    }
                    if (badge3 == 0)
                    {
                        pictureBox6.Visible = false;
                        pictureBox3.Visible = true;
                    }
                    else
                    {
                        pictureBox6.Visible = true;
                        pictureBox3.Visible = false;
                    }
                    if (badge4 == 0)
                    {
                        pictureBox7.Visible = false;
                        pictureBox4.Visible = true;
                    }
                    else
                    {
                        pictureBox7.Visible = true;
                        pictureBox4.Visible = false;
                    }

                }
                else
                {
                    label4.Text = "0";
                    label6.Text = "0";
                    label7.Text = "salut";
                }

            }
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT questname, questcode, workerid, dateposted, completedby, completed  FROM quest_user WHERE workerid=@workerid ORDER BY dateposted ASC";
                OleDbCommand command = new OleDbCommand(query, connection);
                command.Parameters.AddWithValue("@workerid", workerId);
                OleDbDataReader reader = command.ExecuteReader();
                {
                    while (reader.Read())
                    {
                        Label lbl = new Label();
                        lbl.Text = $"Posted by: you on {reader.GetDateTime(3).ToShortDateString()} \nProblem: {reader.GetString(0)}";
                        lbl.Dock = DockStyle.Top;
                        lbl.Size = new Size(667, 36);
                        lbl.BackColor = Color.FromArgb(61, 150, 144);
                        lbl.Font = new Font("MS Reference Sans Serif", 9.5f, FontStyle.Bold);
                        lbl.ForeColor = Color.White;

                        RichTextBox rtb = new RichTextBox();
                        rtb.Text = reader.GetString(1);
                        rtb.Tag = rtb;
                        rtb.Dock = DockStyle.Top;
                        rtb.Margin = new Padding(10, 0, 0, 0);
                        rtb.BackColor = Color.LightGray;
                        rtb.ForeColor = Color.Black;
                        rtb.ReadOnly = true;

                        panel6.Controls.Add(rtb);
                        panel6.Controls.Add(lbl);

                    }
                } 
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

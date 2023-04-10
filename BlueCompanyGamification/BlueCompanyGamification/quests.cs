using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueCompanyGamification
{
    public partial class quests : Form
    {
        public string originaltext ="ziua buna";
        public string workerId { get; set; }
        public quests(string workerIdFromDashboard)
        {
            InitializeComponent();
            workerId = workerIdFromDashboard;

        }

        string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_users.mdb";
        
        private void quests_Load(object sender, EventArgs e)
        {
            
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT questname, questcode, workerid, dateposted, completedby, completed  FROM quest_user ORDER BY dateposted ASC";
                OleDbCommand command = new OleDbCommand(query, connection);

                using (OleDbDataReader reader = command.ExecuteReader()) 
                {
                   while (reader.Read()) 
                    {
                        string questname = reader.GetString(0);
                        //string completedBy = reader.GetString(4);
                        bool completed = reader.GetBoolean(5);

                        string completedBy = reader.IsDBNull(4) ? null : reader.GetString(4);
                        if (completedBy == null)
                        {
                            completedBy = workerId;
                        }

                        Label lbl = new Label();
                        lbl.Text = $"Posted by: WorkerId{reader.GetString(2)} on {reader.GetDateTime(3).ToShortDateString()} \nProblem: {reader.GetString(0)}";
                        lbl.Dock = DockStyle.Top;
                        lbl.Size = new Size(667, 36);
                        lbl.BackColor = Color.FromArgb(100, 120, 174);
                        lbl.Font = new Font("MS Reference Sans Serif",9.5f,FontStyle.Bold) ;
                        lbl.ForeColor = Color.White;
                        
                        RichTextBox rtb = new RichTextBox();
                        rtb.Text = reader.GetString(1);
                        rtb.Tag = rtb;
                        rtb.Dock = DockStyle.Top;
                        rtb.Margin = new Padding(10, 0, 0, 0);
                        rtb.BackColor = Color.LightGray;
                        rtb.ForeColor= Color.Black;
                        rtb.ReadOnly = true;

                        Button btn = new Button();
                        btn.Text = "Complete Quest";
                        btn.Dock = DockStyle.Top;
                        btn.Tag = rtb.Text;
                        btn.Tag = rtb;
                        btn.Tag = questname;
                        btn.Click += Btn_Click;

                        Button btn1 = new Button();
                        btn1.Text = "Enable Editing";
                        btn1.Dock= DockStyle.Top;
                        btn1.Tag = rtb;
                        btn1.Click += Btn1_Click;

                        panel1.Controls.Add(btn);
                        panel1.Controls.Add(btn1);
                        panel1.Controls.Add(rtb);
                        panel1.Controls.Add(lbl);

                        SetButtonState(btn, completedBy, completed);
                    }

                    void SetButtonState(Button btn, string completedBy, bool completed)
                    {
                        if (completed)
                        {
                            btn.Enabled = false;
                            btn.Text = "Completed by WorkerId: " + completedBy;
                        }
                        else
                        {
                            btn.Enabled = true;
                            btn.Text = "Complete Quest";
                        }
                    }

                    void Btn1_Click(object sender, EventArgs e)
                    {
                        Button btn1 = sender as Button;
                        string rtbText = btn1.Tag.ToString();
                        RichTextBox rtb = btn1.Tag as RichTextBox;

                        if (rtb != null)
                        {
                         
                            rtb.ReadOnly = !rtb.ReadOnly;
                            originaltext = rtb.Text;
                            MessageBox.Show($"Edit mode = {!rtb.ReadOnly}");

                        }

                    }
                    
                    void Btn_Click(object sender, EventArgs e)
                    {
                        Button btn = sender as Button;
                        string rtbText = btn.Tag.ToString();
                        RichTextBox rtb = btn.Tag as RichTextBox;
                        string questname = btn.Tag.ToString();
                        string completedBy = workerId;

                            using (OleDbConnection connection = new OleDbConnection(connectionString))
                            {
                                connection.Open();
                                string updateQuery = "UPDATE quest_user SET completed=@completed, questcode=@questcode, completedby=@completedBy  WHERE questname=@questname";
                                OleDbCommand command = new OleDbCommand(updateQuery, connection);
                                command.Parameters.AddWithValue("@completed", true);
                                command.Parameters.AddWithValue("@questcode", originaltext);
                                command.Parameters.AddWithValue("@completedby", completedBy);
                                command.Parameters.AddWithValue("@questname", questname);
                            int rowsAffected = command.ExecuteNonQuery();

                                // check if the update was successful
                                if (rowsAffected > 0)
                                {

                                    MessageBox.Show("Quest completed and your account got 500 Points and 500 Score");
                                    SetButtonState(btn, completedBy, true);
                                    string updateUser = "UPDATE tbl_users SET points = points + 500, score = score + 500 WHERE workerid=@workerId";
                                    OleDbCommand command1 = new OleDbCommand(updateUser, connection);
                                    command1.Parameters.AddWithValue("@workerId",workerId);
                                    int userRowsAffected = command1.ExecuteNonQuery();

                                }
                                else
                                {
                                    MessageBox.Show("Error updating the database.");

                                }
                            }
                            

                    }
                }
            }
        }
    }
}

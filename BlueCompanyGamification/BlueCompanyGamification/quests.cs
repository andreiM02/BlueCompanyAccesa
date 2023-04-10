using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueCompanyGamification
{
    public partial class quests : Form
    {
        
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
                string query = "SELECT questname, questcode FROM quest_user";
                OleDbCommand command = new OleDbCommand(query, connection);

                using (OleDbDataReader reader = command.ExecuteReader()) 
                {
                   while (reader.Read()) 
                    {
                        Label lbl = new Label();
                        lbl.Text = reader.GetString(0);
                        lbl.Dock = DockStyle.Top;
                        lbl.Size = new Size(667, 21);
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
                        btn.Click += Btn_Click;

                        Button btn1 = new Button();
                        btn1.Text = "Enable Editing";
                        btn1.Dock= DockStyle.Top;
                        btn1.Tag = rtb;
                        btn1.Click += Btn1_Click;

                        Label space = new Label();
                        space.Text = "";
                        space.Size = new Size(667, 21);

                        panel1.Controls.Add(btn);
                        panel1.Controls.Add(btn1);
                        panel1.Controls.Add(rtb);
                        panel1.Controls.Add(lbl);
                        
                    }

                    void Btn_Click(object sender, EventArgs e)
                    {

                        Button btn = sender as Button;
                        string rtbText = btn.Tag.ToString();

                        MessageBox.Show(rtbText);

                    }

                   

                    void Btn1_Click(object sender, EventArgs e) 
                    {
                        Button btn1 = sender as Button;
                        string rtbText = btn1.Tag.ToString();
                         RichTextBox rtb = btn1.Tag as RichTextBox;

                        if (rtb != null)
                        {
                            rtb.ReadOnly = !rtb.ReadOnly;
                            MessageBox.Show($"Edit mode = {!rtb.ReadOnly}");
                        }

                    }
                }
            }
        }
    }
}

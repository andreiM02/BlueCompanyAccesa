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
    public partial class leaderboard : Form
    {
        public leaderboard()
        {
            InitializeComponent();
        }

        private void leaderboard_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();

            label1.Text = DateTime.Now.ToString("h:mm tt");
            
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_users.mdb";
            int place = 1;

            

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Selectare date din baza de date si afisare in DataGridView
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT workerid, nume, prenume, score FROM tbl_users ORDER BY score DESC", connection);
                DataTable table = new DataTable();
                table.Columns.Add("Place", typeof(int));
                adapter.Fill(table);
                dataGridView1.DataSource = table;
               

                foreach (DataRow row in table.Rows)             // place este incrementat pentru fiecare coloana adaugata
                {
                    row["Place"] = place;
                    place++;
                }

                

                // Setare latime coloane si aliniere text la centru
                dataGridView1.DataSource = table;
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[3].Width = 100;
                dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString("h:mm tt");
        }
    }
}

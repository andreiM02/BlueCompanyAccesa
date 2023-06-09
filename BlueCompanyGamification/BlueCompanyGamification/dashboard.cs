﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueCompanyGamification
{
    public partial class dashboard : Form
    {
        public string workerId { get; set; }   
        public dashboard()
        {
            InitializeComponent();
        }

     

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
           // label1.Text = "Worker ID: " + workerId;
        }

        private void dashboard_Load_1(object sender, EventArgs e) // Load Function
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
                    string nume = reader["nume"].ToString();
                    string prenume = reader["prenume"].ToString();

                    string numecap = char.ToUpper(nume[0]) + nume.Substring(1);                   // capitalize first letter if its written in lowercase from the nume
                    string prenumecap = char.ToUpper(prenume[0]) + prenume.Substring(1);         // capitalize first letter if its written in lowercase from the prenume
                    label1.Text = numecap.ToString() + " " + prenumecap.ToString();
                }
                else
                {
                    label1.Text = "salut";
                }
            }

            if (Int32.TryParse(workerId, out int workeridInt) && workeridInt == 1)  // adminul are acces la o pagina noua de unde poate inregistra oameni noi veniti in compania
            {
                button5.Visible = true;
            }
            else
            {
                button5.Visible = false;
            }

            dash dash = new dash(workerId);
            //leaderboard = leader = new leaderboard(workerId);

            dash.TopLevel= false;
            dash.FormBorderStyle= FormBorderStyle.None;
            dash.Dock = DockStyle.Fill;

            panel1.Controls.Add(dash);

            dash.Show();

            label2.Text = "\u00A9 BlueCompany";
        }

        private void button4_Click(object sender, EventArgs e) // Exit Button
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e) // Minimize Button
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button6_Click(object sender, EventArgs e) // X Button ( Exit )
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e) // Quests Button
        {
            quests quest = new quests(workerId);

            quest.TopLevel = false;
            quest.FormBorderStyle= FormBorderStyle.None;
            quest.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(quest);
            quest.Show();
        }

        private void button3_Click(object sender, EventArgs e) // Dashboard Button
        {
            dash dash = new dash(workerId);

            dash.TopLevel = false;
            dash.FormBorderStyle= FormBorderStyle.None;
            dash.Dock= DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(dash);
            dash.Show();
        }

        private void button9_Click(object sender, EventArgs e) // Leaderboard Button
        {
            leaderboard leader = new leaderboard();

            leader.TopLevel = false;
            leader.FormBorderStyle= FormBorderStyle.None;
            panel1.Controls.Clear();
            panel1.Controls.Add(leader);
            leader.Show();
        }

        private void button5_Click(object sender, EventArgs e) // Admin  Button
        {
            admin adminn= new admin();

            adminn.TopLevel = false;
            adminn.FormBorderStyle= FormBorderStyle.None;
            panel1.Controls.Clear();
            panel1.Controls.Add(adminn);
            adminn.Show();
        }

        private void button2_Click(object sender, EventArgs e) // Badge Button
        {
            badges badge = new badges(workerId);

            badge.TopLevel = false;
            badge.FormBorderStyle= FormBorderStyle.None;
            panel1.Controls.Clear();
            panel1.Controls.Add(badge);
            badge.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e) // New Quest Button
        {
            newquest newquestt = new newquest(workerId);

            newquestt.TopLevel = false;
            newquestt.FormBorderStyle= FormBorderStyle.None;
            panel1.Controls.Clear();
            panel1.Controls.Add(newquestt);
            newquestt.Show();
        }
    }
}

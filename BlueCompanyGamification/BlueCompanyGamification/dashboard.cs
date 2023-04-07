using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            label1.Text = "Worker ID: " + workerId;
            dash dash = new dash(workerId);
            //dash dash = new dash();

            dash.TopLevel= false;
            dash.FormBorderStyle= FormBorderStyle.None;
            dash.Dock = DockStyle.Fill;

            panel1.Controls.Add(dash);

            dash.Show();
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
            quests quest = new quests();

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
    }
}

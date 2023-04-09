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
using System.Net;
using System.Net.Mail;

namespace BlueCompanyGamification
{
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }

        private void admin_Load(object sender, EventArgs e)
        {
           
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_users.mdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("Some fields are empty!", "Registration failed" , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                con.Open();

                string register = "INSERT INTO tbl_users  VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "', 0, 0, 0, 0, 0, 0,'" + textBox3.Text + "')"; 
                cmd = new OleDbCommand(register, con);
                cmd.ExecuteNonQuery();
                con.Close();            
                
                    MailMessage mail = new MailMessage();
                    SmtpClient smtpServer = new SmtpClient("smtp.zoho.eu",587);
                    mail.From = new MailAddress("mozandrei@zohomail.eu");
                    mail.To.Add(textBox6.Text);
                    mail.Subject = "Your BlueCompanyGamification account was created";
                    mail.Body = "Here are your credentials for login!\n" + "Worker id: " + textBox1.Text +"\n" + "Password: " + textBox2.Text;
                    smtpServer.Port = 587;
                    smtpServer.Credentials = new NetworkCredential("mozandrei@zohomail.eu", "andreiM2002");
                    smtpServer.EnableSsl= true;
                    smtpServer.Send(mail);

                MessageBox.Show("The account was successfully Created", "Registration Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
    }
}

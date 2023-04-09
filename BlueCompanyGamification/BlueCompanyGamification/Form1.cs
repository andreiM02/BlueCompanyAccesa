using System.Data.OleDb;
namespace BlueCompanyGamification
{
    public partial class Form1 : Form
    {

        public static string workerId; // adaug? aceast? linie
        public Form1()
        {
            InitializeComponent();
             
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_users.mdb");
        OleDbCommand cmd = new OleDbCommand();
        OleDbDataAdapter da = new OleDbDataAdapter();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            string login = "SELECT * FROM tbl_users WHERE workerid= '" + textBox1.Text + "' and password= '" + textBox2.Text + "'";
            cmd = new OleDbCommand(login,con);
            OleDbDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true) 
            {
                dashboard dashboardForm = new dashboard();
                dashboardForm.workerId = dr["workerid"].ToString();

                dashboardForm.Show();
                //new dashboard().Show();
                this.Hide();
            
            }
            else
            {
                MessageBox.Show("Invalid Username or Password, Please try Again","Login Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

           textBox1.Clear();
           textBox2.Clear();
           textBox1.Focus();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
                
            }
            else
            {
                textBox2.PasswordChar = '•';
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '•';
            
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void checkBox1_MouseDown(object sender, MouseEventArgs e)
        {
            checkBox1.Checked = true;
        }

        private void checkBox1_MouseUp(object sender, MouseEventArgs e)
        {
            checkBox1.Checked = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
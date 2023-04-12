using System.Data.OleDb;
namespace BlueCompanyGamification
{
    public partial class Form1 : Form
    {

        public static string workerId; 
        public Form1()
        {
            InitializeComponent();
             
        }

        OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_users.mdb"); // conection with database
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
            string login = "SELECT * FROM tbl_users WHERE workerid= '" + textBox1.Text + "' and password= '" + textBox2.Text + "'"; //select workerid and password from database
            cmd = new OleDbCommand(login,con);
            OleDbDataReader dr = cmd.ExecuteReader();

            if (dr.Read() == true) 
            {
                dashboard dashboardForm = new dashboard();
                dashboardForm.workerId = dr["workerid"].ToString();             // if workerid and password are ok , the dashboard will be enabled.

                dashboardForm.Show();
                //new dashboard().Show();
                this.Hide();
            
            }
            else
            {
                MessageBox.Show("Invalid Username or Password, Please try Again","Login Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                textBox1.Clear();
                textBox2.Clear();                 // if not , the context of the textbox will be erased and textbox1 will be focused.
                textBox1.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)  // clear button for clearing the fields.
        {

           textBox1.Clear();
           textBox2.Clear();                   
           textBox1.Focus(); 

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) // checkbox is made here to be togled for showing the password.
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e) // if you press enter when you are on any textbox , the button will be pressed for conection
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

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '•';
        }
    }
}
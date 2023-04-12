# Documentation

This application is made for developers from the BlueCompany. The main porpuse of this app is for the employees can easealy reseolve their problems by
posting the problem code in the new quest page. So there is a posibility for the employees to help eachother in their problems.

The good part of this application is that the user can earn a lot of points by resolving the quests from their colleagues , and be a high place in the
leaderboard , or just for buying a badge that can provide users with lot of benefits.

You can find how this app works on this link:https://youtu.be/FPYCXGNp3KA

The informations of the users are stored in a local database made with acces.

This is the path of the database:
```c#
OleDbConnection con = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_users.mdb");
OleDbCommand cmd = new OleDbCommand();
OleDbDataAdapter da = new OleDbDataAdapter();
```

<b>Login Form</b>

This <code><b>if</b></code> verify if the credentials from the textbox1(username) and textbox2(password) are the same as the credentials from the database.
if the credentials are the same , the <code><b>dashboard</b></code> is opened , and the current Form is closed.
If not , the textbox1(username) and textbox2(password) are cleared and the focus is put on textbox1.
```c#
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
```

<b>Dashboard Form</b>

The dashboard form is made for the user to navigate between the other forms.
In this line of code , the First name and Laste Name is taken from the database where the workerid from database is equal to the workerId saved from
the <code><b>Login form</code></b>

```c#connection.Open();
OleDbCommand command = new OleDbCommand("SELECT points,score,nume,prenume FROM tbl_users WHERE workerid=@workerId", connection);
command.Parameters.AddWithValue("@workerId", workerId);
```

This is an example for the Quest Button in how the <code><b>panel1</code></b> is changeing the <code><b>Forms</code></b> between them

```c#private void button1_Click(object sender, EventArgs e) // Quests Button
        {
            quests quest = new quests(workerId);

            quest.TopLevel = false;
            quest.FormBorderStyle= FormBorderStyle.None;
            quest.Dock = DockStyle.Fill;
            panel1.Controls.Clear();
            panel1.Controls.Add(quest);
            quest.Show();
        }
```

<b>Dash Form</b>

this is how the variable are saved from the database and saved.
The variables are then displayed in the dashboard for the user to see his progress.
Aswell as the owned badges and the quests posted by him.

```c#
 using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("SELECT points,score,badge1,badge2,badge3,badge4,nume,prenume FROM tbl_users WHERE workerid=@workerId", connection);
                command.Parameters.AddWithValue("@workerId", workerId);

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
                }
                else
                {
                    label4.Text = "0";
                    label6.Text = "0";
                    label7.Text = "salut";
                }
            }
```

Here are the quests made by the current <code><b>workerId</code></b> and dispalyed in order by dateposted.
```c#
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
```

<b>New Qeust</b>

In the <code><b>New Qeust Form</code></b> users are able to post a new quest with the cost of 100 points.
The variable from <code><b>textbox1</code></b> and <code><b>richtextbox1</code></b> and saved to a new 
row in the database . The datetime is also saved with datetime for the quest ot be ordered by the date created 
and 100 points to be taken from your account.
```c#
 private void button3_Click(object sender, EventArgs e)
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=db_users.mdb";
            // Get the quest name and code from the textboxes and richtextbox
            string questname = textBox1.Text;
            string questcode = richTextBox1.Text;
            string workerid = workerId;

            DateTime currentDate = DateTime.Now;
            string currentdateformat = currentDate.ToString("MM-dd-yyyy");

            string Query = "INSERT INTO quest_user (questcode, questname, workerid, dateposted) VALUES (@questcode, @questname, @workerid, @dateposted)";
            OleDbConnection connection = new OleDbConnection(connectionString);
            using OleDbCommand command = new OleDbCommand(Query, connection);
            command.Parameters.AddWithValue("@questcode", questcode);
            command.Parameters.AddWithValue("@questname", questname);
            command.Parameters.AddWithValue("@workerid", workerid);
            command.Parameters.AddWithValue("@dateposted", currentdateformat);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected != 0)
                {
                    string updateQuery = "UPDATE tbl_users SET points = points - 100 WHERE workerid = @workerid";
                    OleDbCommand updateCommand = new OleDbCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@workerid", workerId);
                    int updateRowsAffected = updateCommand.ExecuteNonQuery();
                    if (updateRowsAffected > 0) 
                    {
                        MessageBox.Show("Quest Posted and 100 points deducted from your account!");
                    }
                    else 
                    {
                        MessageBox.Show("Error deducting points from your account.");
                    }
                }
                else
                {
                    MessageBox.Show("Error posting the quest!");
                }
            }
            catch(OleDbException ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
```

<b>Qeust Form</b>

In the quest form are displayed every row from the databased , ordered ascendent by dateposted variable from database.\n
The users can enable editing and modify the text , and then complete the quest.\n
When the ueser complete the quest , his account will be supplemented with 500 points and 500 score.\n

Here are get the values from the database.
```c#
connection.Open();
string query = "SELECT questname, questcode, workerid, dateposted, completedby, completed  FROM quest_user ORDER BY dateposted ASC";
OleDbCommand command = new OleDbCommand(query, connection);
```

Here is created each <code><b>Label</b></code> , <code><b>RichTextBox</b></code> and <code><b>Buttons</b></code> for each rows from database
and cheked the state of the buttons if the Quest is completed with the value <code><b>completed</b></code> from the database.

```c#
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
                        lbl.BackColor = Color.FromArgb(61, 150, 144);
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

                        SetButtonState(btn, btn1, completedBy, completed);
                    }
```
The second big part of the code is represented by the <code><b>Btn_Click</b></code> and here are set the completed part for the quest 
and then the 100 points are deducted from the account when you press the Complete Quest Button.

```c#
void Btn_Click(object sender, EventArgs e)
                    {
                        Button btn = sender as Button;
                        Button btn1 = sender as Button;
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
                                    SetButtonState(btn, btn1, completedBy, true);
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
```

<b>Badge Form</b>

For the badge system , the program is checking first if the user have enough money for buying , and then is checking if the user already
have this badge.

```c#
private void button5_Click(object sender, EventArgs e)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Retrieve user's current points from the database
                connection.Open();
                string pointsQuery = "SELECT points FROM tbl_users WHERE workerid = @workerid";
                OleDbCommand pointsCommand = new OleDbCommand(pointsQuery, connection);
                pointsCommand.Parameters.AddWithValue("@workerid", workerId);
                int currentPoints = Convert.ToInt32(pointsCommand.ExecuteScalar());

                if (currentPoints < 5000)
                {
                    // User does not have enough points to purchase the badge
                    MessageBox.Show("You do not have enough points to buy this badge!");
                }
                else
                {
                    // User has enough points to purchase the badge
                    string updateQuery = "UPDATE tbl_users SET badge1 = 1, points = @points WHERE workerid = @workerid AND badge1 = 0";
                    OleDbCommand updateCommand = new OleDbCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@points", currentPoints - 5000);
                    updateCommand.Parameters.AddWithValue("@workerid", workerId);
                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Badge purchased successfully
                        MessageBox.Show("Badge purchased successfully");
                    }
                    else
                    {
                        // User already owns this badge
                        MessageBox.Show("You already own this badge!");
                    }
                }
            }
        }
```
the exemple is with the <code><b>Button_5</code></b> wich is the first Badge button , and the value that is checking for is 5000 points.

<b>Leaderboard Form</b>

For the leaderboard , there are saved from the database the following information: workerid , nume(Last Name) , prenume(First Name) and score 
and then , ordered Desc from the biggest score to the lowest.

```c#
 OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT workerid, nume, prenume, score FROM tbl_users ORDER BY score DESC", connection);
                DataTable table = new DataTable();
                table.Columns.Add("Place", typeof(int));
                adapter.Fill(table);
                dataGridView1.DataSource = table;
```

and then the variable palce is created and incremented by each row added to the leaderboard.

```c#
 foreach (DataRow row in table.Rows)             
                {
                    row["Place"] = place;
                    place++;
                }
```

<b>Admin Form</b>

For the normal user , the adming page its not visible , and only the workerid that is selected can see the button to open de admin form.
There , the CEO can add new accounts for the new employees and send the credentials to their mail.

The credentials are sent to the database from this string
```c#
string register = "INSERT INTO tbl_users  VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "', 0, 0, 0, 0, 0, 0,'" + textBox3.Text + "')"; 
                cmd = new OleDbCommand(register, con);
                cmd.ExecuteNonQuery();
                con.Close();
```

and the e-mail is sent with this format
```c#
"Here are your credentials for login!\n" + "Worker id: " + textBox1.Text +"\n" + "Password: " + textBox2.Text;
```
```c#
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
```

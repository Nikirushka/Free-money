using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Study
{
    public partial class Login : Form
    {
        SqlConnection connection=null;
        SqlDataReader reader = null;
        SqlCommand cmd;

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|courses.mdf';Integrated Security=True;Connect Timeout=30";
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string LoginTB, PasswordTB;
            LoginTB = textBox1.Text;
            PasswordTB = textBox2.Text;
            connection = new SqlConnection(connectionString);
            connection.Open();
            string query = $"SELECT * FROM [Users] WHERE (Login=N'{LoginTB}'COLLATE CYRILLIC_General_CS_AS)";
            cmd = new SqlCommand(query, connection);
            reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                MessageBox.Show("Wrong Login", "Error");
                reader.Close();
                return;
            }
            else
            {
                reader.Close();
                query = $"SELECT * FROM Users WHERE (Login=N'{LoginTB}'COLLATE CYRILLIC_General_CS_AS) AND (Password= N'{PasswordTB}' COLLATE CYRILLIC_General_CS_AS) ";
                cmd = new SqlCommand(query, connection);
                reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    MessageBox.Show("Wrong Password", "Error");
                    return;
                }
                else
                {
                    this.Hide();
                    reader.Close();
                    query = $"SELECT * FROM Users WHERE (Login=N'{LoginTB}'COLLATE CYRILLIC_General_CS_AS) AND (Password= N'{PasswordTB}' COLLATE CYRILLIC_General_CS_AS) AND Access=1";
                    cmd = new SqlCommand(query, connection);
                    reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        string id=null;
                        reader.Close();
                        query = $"exec getUserID N'{LoginTB}'";
                        cmd = new SqlCommand(query, connection);
                        reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                             id = reader.GetValue(0).ToString();
                        }
                        reader.Close();
                        Teacher teacher = new Teacher(id);
                        reader.Close();
                        connection.Close();
                        DialogResult dialogResult = new DialogResult();
                        dialogResult = teacher.ShowDialog();
                        if (dialogResult == DialogResult.Cancel)
                        {
                            this.Show();
                        }
                        else
                        { 
                            this.Close();
                        }
                    }
                    else
                    {
                        reader.Close();
                        query = $"SELECT * FROM Users WHERE (Login=N'{LoginTB}'COLLATE CYRILLIC_General_CS_AS) AND (Password= N'{PasswordTB}' COLLATE CYRILLIC_General_CS_AS) AND Access=0";
                        cmd = new SqlCommand(query, connection);
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            string id = null;
                            reader.Close();
                            query = $"exec getUserID N'{LoginTB}'";
                            cmd = new SqlCommand(query, connection);
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                id = reader.GetValue(0).ToString();
                            }
                            reader.Close();
                            Student student = new Student(id);
                            reader.Close();
                            connection.Close();
                            DialogResult dialogResult = new DialogResult();
                            dialogResult = student.ShowDialog();
                            if (dialogResult == DialogResult.Cancel)
                            {
                                this.Show();
                            }
                            else
                            {
                                this.Close();
                            }
                        }
                    }
                }
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            connection.Close();
            this.Close();
        }
        Point lastpoint;

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            if(connection == null && reader == null)
            {
                this.Close();
            }
            else if (connection == null)
            {
                reader.Close();
                this.Close();
            }else
            {
                connection.Close();
                this.Close();
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.Clear();
        }
    }
}

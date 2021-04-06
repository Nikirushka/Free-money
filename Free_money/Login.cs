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

namespace Free_money
{
    public partial class Login : Form
    {
        SqlConnection connection = null;
        SqlDataReader reader = null;
        SqlCommand cmd;

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|Free_money.mdf';Integrated Security=True;Connect Timeout=30";
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
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
                    MessageBox.Show("Неправильный логин", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    reader.Close();
                    return;
                }
                else
                {
                    reader.Close();
                    query = $"SELECT * FROM Users WHERE (Login=N'{LoginTB}'COLLATE CYRILLIC_General_CS_AS) AND (Password= '{PasswordTB}' COLLATE CYRILLIC_General_CS_AS) ";
                    cmd = new SqlCommand(query, connection);
                    reader = cmd.ExecuteReader();
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        MessageBox.Show("Неправильный пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        return;
                    }
                    else
                    {
                        this.Hide();
                        reader.Close();
                        query = $"SELECT * FROM Users WHERE (Login=N'{LoginTB}'COLLATE CYRILLIC_General_CS_AS) AND (Password= '{PasswordTB}' COLLATE CYRILLIC_General_CS_AS) AND Status=1";
                        cmd = new SqlCommand(query, connection);
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            string id = null;
                            reader.Close();
                            query = $"exec getID '{LoginTB}'";
                            cmd = new SqlCommand(query, connection);
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                id = reader.GetValue(0).ToString();
                            }
                            reader.Close();
                            Admin teacher = new Admin(id);
                            reader.Close();
                            connection.Close();
                            DialogResult dialogResult = new DialogResult();
                            dialogResult = teacher.ShowDialog();
                            if (dialogResult == DialogResult.OK)
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
                            query = $"SELECT * FROM Users WHERE (Login=N'{LoginTB}'COLLATE CYRILLIC_General_CS_AS) AND (Password= '{PasswordTB}' COLLATE CYRILLIC_General_CS_AS) AND Status=0";
                            cmd = new SqlCommand(query, connection);
                            reader = cmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                string id = null;
                                reader.Close();
                                query = $"exec getID '{LoginTB}'";
                                cmd = new SqlCommand(query, connection);
                                reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    id = reader.GetValue(0).ToString();
                                }
                                reader.Close();
                                User user = new User(id);
                                reader.Close();
                                connection.Close();
                                DialogResult dialogResult = new DialogResult();
                                dialogResult = user.ShowDialog();
                                if (dialogResult == DialogResult.OK)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (connection == null && reader == null)
            {
                this.Close();
            }
            else if (connection == null)
            {
                reader.Close();
                this.Close();
            }
            else
            {
                connection.Close();
                this.Close();
            }
        }
        Point lastpoint;

        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddEditUser newUser = new AddEditUser(0);
            DialogResult dialogResult = new DialogResult();
            dialogResult = newUser.ShowDialog();
            if (dialogResult == DialogResult.OK)
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

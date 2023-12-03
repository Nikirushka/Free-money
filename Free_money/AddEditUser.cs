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
    public partial class AddEditUser : Form
    {
        SqlConnection connection = null;
        SqlDataReader reader = null;
        SqlCommand cmd;

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|Free_money.mdf';Integrated Security=True;Connect Timeout=30";
        public AddEditUser()
        {
            InitializeComponent();
        }
        int who;
        public AddEditUser(int id)
        {
            InitializeComponent();
            who = id;
            change_button.Hide();
        }
        string login_id;
        public AddEditUser(string _name, string _surname, string _patronymic, string _phone, string _email, int _age, string _login, string _password, string _nickmane)
        {
            InitializeComponent();
            Add_button.Hide();
            login_id = _login;
            Add_Name.Text = _name;
            Add_Surname.Text = _surname;
            Add_Patronymic.Text = _patronymic;
            Add_Phone.Text = _phone;
            Add_Age.Text = _age.ToString();
            Add_Login.Text = _login;
            Add_Password.Text = _password;
            Add_Nickname.Text = _nickmane;
            Add_Email.Text = _email;
        }
        Point lastpoint;
        private void Edit_button_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                int check = 0;
                string query = $"exec check_login N'{Add_Login.Text}'";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    check = reader.GetInt32(0);
                }
                reader.Close();
                connection.Close();
                int gender=1;
                if(comboBox1.Text=="Man")
                {
                    gender = 1;
                }
                else
                {
                    gender = 2;
                }
                if (check == 1)
                {
                    MessageBox.Show("Пользователь с таким логином существует!\nПридумайте другой :)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                else
                {
                    if (who == 0)
                    {
                        query = $"insert into users values(N'{Add_Name.Text}',N'{Add_Surname.Text}',N'{Add_Patronymic.Text}',N'{Add_Nickname.Text}',N'{Add_Login.Text}',N'{Add_Password.Text}',N'{Add_Email.Text}',N'{Add_Phone.Text}',{Add_Age.Text},{gender},2)";
                    }
                    else
                    {
                        query = $"insert into users values(N'{Add_Name.Text}',N'{Add_Surname.Text}',N'{Add_Patronymic.Text}',N'{Add_Nickname.Text}',N'{Add_Login.Text}',N'{Add_Password.Text}',N'{Add_Email.Text}',N'{Add_Phone.Text}',{Add_Age.Text},{gender},1)";
                    }
                    connection.Open();
                   cmd = new SqlCommand(query, connection);
                   cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Пользователь создан :)", "ИНФОРМАЦИЯ", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Profile_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void Profile_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void Add_button_MouseEnter(object sender, EventArgs e)
        {
            Add_button.BackColor = Color.SkyBlue;
        }

        private void Add_button_MouseLeave(object sender, EventArgs e)
        {
            Add_button.BackColor = Color.FromArgb(172, 172, 164);
        }

        private void Close_MouseEnter(object sender, EventArgs e)
        {
            Close_button.BackColor = Color.SkyBlue;
        }

        private void Close_MouseLeave(object sender, EventArgs e)
        {
            Close_button.BackColor = Color.FromArgb(172, 172, 164);
        }

        private void change_button_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                string query = $"exec getID N'{login_id}'";
                int check = 0,logiin=0;
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    logiin = reader.GetInt32(0);
                }
                reader.Close();
                query = $"exec check_login N'{Add_Login.Text}'";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    check = reader.GetInt32(0);
                }
                reader.Close();
                connection.Close();
                int gender = 1;
                if (comboBox1.Text == "Man")
                {
                    gender = 1;
                }
                else
                {
                    gender = 2;
                }
                if (check == 1)
                {
                    MessageBox.Show("Пользователь с таким логином существует!\nПридумайте другой :)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                else
                {
                    query = $"update users set Name=N'{Add_Name.Text}',Surname=N'{Add_Surname.Text}',Patronymic=N'{Add_Patronymic.Text}',Nickname=N'{Add_Nickname.Text}',Login=N'{Add_Login.Text}',Password=N'{Add_Password.Text}',e_mail=N'{Add_Email.Text}',Phone=N'{Add_Phone.Text}',Age={Add_Age.Text},ID_gender={gender} where id_user={logiin}";
                    connection.Open();
                    cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Пользователь изменён :)", "ИНФОРМАЦИЯ", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
    }
}

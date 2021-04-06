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
    public partial class User : Form
    {
        SqlConnection connection = null;
        SqlDataReader reader = null;
        SqlCommand cmd;
        DataSet ds;
        SqlDataAdapter adapter;
        Point lastpoint;

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|Free_money.mdf';Integrated Security=True;Connect Timeout=30";
        public User()
        {
            InitializeComponent();
        }
        string id_user;
        public User(string id)
        {
            InitializeComponent();
            id_user = id;
        }
        Point loc = new Point(166, 46);
        private void User_Load(object sender, EventArgs e)
        {
            stat.Hide();
            Operations.Hide();
            panel8.Hide();
            Wallet.Hide();
            profile.Hide();
            new_wallet.Hide();
            connect_user();
            connect_operations();
            connect_wallets();
        }

        private void Profile_Click(object sender, EventArgs e)
        {
            stat.Hide();
            Nickname.Show();
            Wallet.Hide();
            Operations.Hide();
            connect_user();
            profile.Location = loc;
            profile.Show();
            label2.Text = "ПРОФИЛЬ";
        }
        private void connect_user()
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                string text = $"select * from users where ID_user={id_user}";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Name1.Text = reader.GetString(1).Trim();
                    Edit_Name.Text = reader.GetString(1).Trim();
                    Surname1.Text = reader.GetString(2).Trim();
                    Edit_Surname.Text = reader.GetString(2).Trim();
                    Patronymic1.Text = reader.GetString(3).Trim();
                    Edit_Patronymic.Text = reader.GetString(3).Trim();
                    Nickname.Text = reader.GetString(4).Trim();
                    Login1.Text = reader.GetString(5).Trim();
                    Edit_Login.Text = reader.GetString(5).Trim();
                    Password1.Text = reader.GetString(6).Trim();
                    Edit_Password.Text = reader.GetString(6).Trim();
                    Email.Text = reader.GetString(7).Trim();
                    Edit_Email.Text = reader.GetString(7).Trim();
                    Phone.Text = reader.GetString(8).Trim();
                    Edit_Phone.Text = reader.GetString(8).Trim();
                    Age.Text = reader.GetInt32(9).ToString() + " лет";
                    Edit_Age.Text = reader.GetInt32(9).ToString();
                    DateRegAndStatus.Text = "Дата регистрации : " + reader.GetDateTime(10).ToShortDateString() + " Статус : Пользователь"; 
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Edit_button_Click(object sender, EventArgs e)
        {
            try
            {
                int check = 0;
                connection = new SqlConnection(connectionString);
                connection.Open();
                string query = $"exec check_login N'{Edit_Login.Text}'";    
                if (Login1.Text != Edit_Login.Text)
                {
                    cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        check = reader.GetInt32(0);
                    }
                    reader.Close();
                }
                if (check == 1)
                {
                    MessageBox.Show("Пользователь с таким логином существует!\nПридумайте другой :)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                else
                {
                    query = $"update users set Name=N'{Edit_Name.Text}',Surname=N'{Edit_Surname.Text}',Patronymic=N'{Edit_Patronymic.Text}',Login=N'{Edit_Login.Text}', Password=N'{Edit_Password.Text}', e_mail=N'{Edit_Email.Text}', Phone=N'{Edit_Phone.Text}', Age={Edit_Age.Text} where id_user={id_user}";
                    cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }
                reader.Close();
                connection.Close();
                connect_user();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Wallet.Location = loc;
            connect_wallets();
            Wallet.Show();
            profile.Hide();
            Operations.Hide();
        }
        public void connect_wallets()
        {
            try
            {
                string query = $"select * from info_wallets where login=N'{Login1.Text}'";
                datawallets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                datawallets.AllowUserToAddRows = false;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    adapter = new SqlDataAdapter(query, connection);
                    ds = new DataSet();
                    adapter.Fill(ds);
                    datawallets.DataSource = ds.Tables[0];
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void connect_operations()
        {
            try
            {
                ////Открываем подключение
                connection.Open();
                ////string selectColumns = &quot;SELECT COLUMN_NAME FROM
                string select_num = $"SELECT wallet.id_wallet FROM wallet join users on users.ID_user=Wallet.ID_user where Users.Login=N'{Login1.Text}'";
                List<int> fd = new List<int>();
                cmd = new SqlCommand(select_num, connection);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    fd.Add(reader.GetInt32(0));
                }
                //list - название компонента ComboBox
                list.DataSource = fd;
                reader.Close();
                ////list - название компонента ComboBox
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                string query = $"select Critetion,Sum,Date,Commentary,Operation,ID_money_operations from Money_operations join wallet on Wallet.ID_wallet=Money_operations.ID_wallet join users on users.ID_user=Wallet.ID_user where Users.Login=N'{Login1.Text}'";
                dataoperations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataoperations.AllowUserToAddRows = false;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    adapter = new SqlDataAdapter(query, connection);
                    ds = new DataSet();
                    adapter.Fill(ds);
                    dataoperations.DataSource = ds.Tables[0];
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new_wallet.Hide();
        }

        private void add_wallet_Click(object sender, EventArgs e)
        {
            new_wallet.Show();
            button1.Show();
            edit_currency.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                    string query = $"insert into wallet values({id_user},0,GETDATE(),N'{currency.Text}')";
                    cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Кошелёк создан :)", "ИНФОРМАЦИЯ", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    new_wallet.Hide();
                connect_wallets();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void label7_Click(object sender, EventArgs e)
        {
            panel8.Hide();
            Wallet.Hide();
            Operations.Location = loc;
            Operations.Show();
            connect_operations();
            stat.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            panel8.Hide();
            Wallet.Hide();
            Operations.Hide();
            profile.Hide();
            stat.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            stat.Location = loc;
            stat.Show();
            panel8.Hide();
            Wallet.Hide();
            Operations.Hide();
            profile.Hide();
            try
            {
                string query = $"select Critetion,Sum,Date,Commentary,Operation,ID_money_operations from Money_operations join wallet on Wallet.ID_wallet=Money_operations.ID_wallet join users on users.ID_user=Wallet.ID_user where Users.Login=N'{Login1.Text}'";
                statik.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                statik.AllowUserToAddRows = false;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    adapter = new SqlDataAdapter(query, connection);
                    ds = new DataSet();
                    adapter.Fill(ds);
                    statik.DataSource = ds.Tables[0];
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void delete_wallet_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                int index = 0;
                foreach (DataGridViewCell cell in datawallets.SelectedCells)
                {
                    index = cell.RowIndex;
                }
                string choose_wallet = (datawallets[4, index].Value.ToString());
                string delQuery = $"DELETE FROM wallet WHERE id_wallet = {choose_wallet}";
                cmd = new SqlCommand(delQuery, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connect_wallets();
        }
        string choose_wallet_id;
        private void edit_wallet_Click(object sender, EventArgs e)
        {
            new_wallet.Show();
            edit_currency.Show();
            button1.Hide();
            int index = 0;
            foreach (DataGridViewCell cell in datawallets.SelectedCells)
            {
                index = cell.RowIndex;
            }
            currency.Text = (datawallets[3, index].Value.ToString());
            choose_wallet_id = (datawallets[4, index].Value.ToString());
        }

        private void edit_currency_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                
                string delQuery = $"update wallet set currency=N'{currency.Text}' where id_wallet={choose_wallet_id} ";
                cmd = new SqlCommand(delQuery, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connect_wallets();
            new_wallet.Hide();
        }

        private void add_operation_Click(object sender, EventArgs e)
        {
            panel8.Show();
            button3.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                string query = $"insert into money_operations values({list.Text},N'{textBox1.Text}','{textBox3.Text}',GETDATE(),N'{textBox2.Text}',{textBox4.Text})";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Операция проведена :)", "ИНФОРМАЦИЯ", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                new_wallet.Hide();
                connect_operations();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            panel8.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void edit_operation_Click(object sender, EventArgs e)
        {
            panel8.Show();
            button3.Show();
            int index = 0;
            foreach (DataGridViewCell cell in dataoperations.SelectedCells)
            {
                index = cell.RowIndex;
            }
            textBox1.Text = (dataoperations[0, index].Value.ToString());
            textBox3.Text = (dataoperations[1, index].Value.ToString());
            textBox2.Text = (dataoperations[3, index].Value.ToString());
            textBox4.Text = (dataoperations[4, index].Value.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel8.Hide();
        }

        private void del_operation_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                int index = 0;
                foreach (DataGridViewCell cell in dataoperations.SelectedCells)
                {
                    index = cell.RowIndex;
                }
                string choose_operation = (dataoperations[5, index].Value.ToString());
                string delQuery = $"DELETE FROM money_operations WHERE id_money_operations = {choose_operation}";
                cmd = new SqlCommand(delQuery, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connect_operations();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                int index = 0;
                foreach (DataGridViewCell cell in dataoperations.SelectedCells)
                {
                    index = cell.RowIndex;
                }
                string choose_operation = (dataoperations[5, index].Value.ToString());
                string delQuery = $"update money_operations set critetion=N'{textBox1.Text}',sum={textBox3.Text},commentary=N'{textBox2.Text}',operation={textBox4.Text} where id_money_operations={choose_operation} ";
                cmd = new SqlCommand(delQuery, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            panel8.Hide();
            connect_operations();
            
        }

        private void User_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }
        private void User_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                string query = $"select Critetion,Sum,Date,Commentary,Operation,ID_money_operations from Money_operations join wallet on Wallet.ID_wallet=Money_operations.ID_wallet join users on users.ID_user=Wallet.ID_user where Users.Login=N'{Login1.Text}' and critetion=N'{textBox5.Text}'  and sum={textBox7.Text} and date='{textBox6.Text}' and Commentary=N'{textBox8.Text}' and operation={textBox9.Text}";
                statik.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                statik.AllowUserToAddRows = false;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    adapter = new SqlDataAdapter(query, connection);
                    ds = new DataSet();
                    adapter.Fill(ds);
                    statik.DataSource = ds.Tables[0];
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                int index = 0;
                foreach (DataGridViewCell cell in datawallets.SelectedCells)
                {
                    index = cell.RowIndex;
                }
               string choose_wallet_id_ = (datawallets[4, index].Value.ToString());
                string text = $"exec best_day {choose_wallet_id_}";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    label21.Text = "Лучший день : " + reader.GetDateTime(0).ToShortDateString();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                int index = 0;
                foreach (DataGridViewCell cell in datawallets.SelectedCells)
                {
                    index = cell.RowIndex;
                }
                string choose_wallet_id_ = (datawallets[4, index].Value.ToString());
                string text = $"exec best_month {choose_wallet_id_}";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    label21.Text = "Лучший месяц : " + reader.GetInt32(0).ToString();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

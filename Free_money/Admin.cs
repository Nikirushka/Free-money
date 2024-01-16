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
    public partial class Admin : Form
    {
        SqlConnection connection = null;
        SqlDataReader reader = null;
        SqlCommand cmd;
        DataSet ds;
        SqlDataAdapter adapter;

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|Free_money.mdf';Integrated Security=True;Connect Timeout=30";
        public Admin()
        {
            InitializeComponent();
        }
        string id_user;
        public Admin(string id)
        {
            InitializeComponent();
            id_user = id;
        }

        Point lastpoint;
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.FromArgb(249, 245, 247);
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.FromArgb(172, 172, 164);
        }

        private void Admin_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void Admin_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.FromArgb(249, 245, 247);
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.FromArgb(172, 172, 164);
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            label5.ForeColor = Color.FromArgb(249, 245, 247);
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            label5.ForeColor = Color.FromArgb(172, 172, 164);
        }

        private void label7_MouseEnter(object sender, EventArgs e)
        {
            label7.ForeColor = Color.FromArgb(249, 245, 247);
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            label7.ForeColor = Color.FromArgb(172, 172, 164);
        }

        Point loc = new Point(166, 46);

        private void label4_Click(object sender, EventArgs e)
        {
            stat.Hide();
            Nickname.Show();
            connect_user();
            Profile.Location = loc;
            Profile.Show();
            Wallet.Hide();
            Operations.Hide();
            People.Hide();
            label2.Text = "ПРОФИЛЬ";
            label4.ForeColor = Color.FromArgb(152, 98, 115);
            label3.ForeColor = Color.FromArgb(172, 172, 164);
            label5.ForeColor = Color.FromArgb(172, 172, 164);
            label7.ForeColor = Color.FromArgb(172, 172, 164);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            stat.Hide();
            choose_who.Hide();
            Nickname.Hide();
            People.Location = loc;
            People.Show();
            Wallet.Hide();
            Operations.Hide();
            Profile.Hide();
            label2.Text = "ЛЮДИ";
            connect_people();
            label3.ForeColor = Color.FromArgb(152, 98, 115);
            label4.ForeColor = Color.FromArgb(172, 172, 164);
            label5.ForeColor = Color.FromArgb(172, 172, 164);
            label7.ForeColor = Color.FromArgb(172, 172, 164);
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            stat.Hide();
            new_wallet.Hide();
            panel8.Hide();
            choose_who.Hide();
            Nickname.Hide();
            People.Hide();
            Profile.Hide();
            Wallet.Hide();
            Operations.Hide();
            connect_user();
            connect_people();
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
                    DateRegAndStatus.Text = " Статус : Админ";
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void label5_Click(object sender, EventArgs e)
        {
            stat.Hide();
            connect_wallets();
            Nickname.Hide();
            Wallet.Location = loc;
            Wallet.Show();
            People.Hide();
            Operations.Hide();
            Profile.Hide();
            label2.Text = "КОШЕЛЬКИ";
            label5.ForeColor = Color.FromArgb(152, 98, 115);
            label4.ForeColor = Color.FromArgb(172, 172, 164);
            label3.ForeColor = Color.FromArgb(172, 172, 164);
            label7.ForeColor = Color.FromArgb(172, 172, 164);
            try
            {
                ////Открываем подключение
                connection.Open();
                ////string selectColumns = &quot;SELECT COLUMN_NAME FROM
                string select_num = $"SELECT users.login FROM wallet join users on users.ID_user=Wallet.ID_user";
                List<string> dd = new List<string>();
                cmd = new SqlCommand(select_num, connection);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    dd.Add(reader.GetString(0));
                }
                //list - название компонента ComboBox
                comboBox1.DataSource = dd;
                reader.Close();
                ////list - название компонента ComboBox
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            stat.Hide();
            Nickname.Hide();
            Operations.Location = loc;
            Operations.Show();
            People.Hide();
            Wallet.Hide();
            Profile.Hide();
            label2.Text = "ОПЕРАЦИИ";
            label7.ForeColor = Color.FromArgb(152, 98, 115);
            label4.ForeColor = Color.FromArgb(172, 172, 164);
            label5.ForeColor = Color.FromArgb(172, 172, 164);
            label3.ForeColor = Color.FromArgb(172, 172, 164);
            connect_operations();
        }
        public void connect_operations()
        {
            try
            {
                ////Открываем подключение
                connection.Open();
                ////string selectColumns = &quot;SELECT COLUMN_NAME FROM
                string select_num = $"SELECT wallet.id_wallet FROM wallet join users on users.ID_user=Wallet.ID_user";
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
                string query = $"select Critetion,Sum,Date,Commentary,Operation,ID_money_operations from Money_operations join wallet on Wallet.ID_wallet=Money_operations.ID_wallet join users on users.ID_user=Wallet.ID_user";
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

        private void label6_Click(object sender, EventArgs e)
        {
            Nickname.Hide();
            Operations.Hide();
            People.Hide();
            Wallet.Hide();
            Profile.Hide();
            label2.Text = "ВОПРОСЫ";
            label4.ForeColor = Color.FromArgb(172, 172, 164);
            label5.ForeColor = Color.FromArgb(172, 172, 164);
            label3.ForeColor = Color.FromArgb(172, 172, 164);
            label7.ForeColor = Color.FromArgb(172, 172, 164);
        }

        private void Edit_button_MouseLeave(object sender, EventArgs e)
        {
            Edit_button.BackColor = Color.FromArgb(172, 172, 164);
        }

        private void Edit_button_MouseEnter(object sender, EventArgs e)
        {
            Edit_button.BackColor = Color.SkyBlue;
        }

        private void Edit_button_Click(object sender, EventArgs e)
        {
            try
            {
                int check=0;
                connection = new SqlConnection(connectionString);
                connection.Open();
                string query = $"exec check_login N'{Edit_Login.Text}'";
                if (Login1.Text!=Edit_Login.Text)
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
        public void connect_people()
        {
            try
            {
            string query = $"select * from informations_about_users";
            datapeople.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            datapeople.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(query, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                datapeople.DataSource = ds.Tables[0];
            }
            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void connect_wallets()
        {
            try
            {
                string query = $"select * from info_wallets";
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

        private void add_user_MouseEnter(object sender, EventArgs e)
        {
            add_user.BackColor = Color.SkyBlue;
        }

        private void edit_user_MouseEnter(object sender, EventArgs e)
        {
           edit_user.BackColor = Color.SkyBlue;
        }

        private void del_user_MouseEnter(object sender, EventArgs e)
        {
            del_user.BackColor = Color.SkyBlue;
        }

        private void add_user_MouseLeave(object sender, EventArgs e)
        {
            add_user.BackColor = Color.FromArgb(172, 172, 164);
        }

        private void edit_user_MouseLeave(object sender, EventArgs e)
        {
            edit_user.BackColor = Color.FromArgb(172, 172, 164);
        }

        private void del_user_MouseLeave(object sender, EventArgs e)
        {
            del_user.BackColor = Color.FromArgb(172, 172, 164);
        }

        private void add_user_Click(object sender, EventArgs e)
        {
            choose_who.Show();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            choose_who.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
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
            choose_who.Hide();
            connect_people();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddEditUser newUser = new AddEditUser(1);
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
            choose_who.Hide();
            connect_people();
        }

        private void del_user_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                int index = 0;
                foreach (DataGridViewCell cell in datapeople.SelectedCells)
                {
                    index = cell.RowIndex;
                }
                string choose_login = (datapeople[4, index].Value.ToString());

                string delQuery = $"DELETE FROM [Users] WHERE login = N'{choose_login}'";

                cmd = new SqlCommand(delQuery, connection);

                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            connect_people();
        }

        private void edit_user_Click(object sender, EventArgs e)
        {
            
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                int index = 0;
                foreach (DataGridViewCell cell in datapeople.SelectedCells)
                {
                    index = cell.RowIndex;
                }
                string choose_login = (datapeople[4, index].Value.ToString());
                string text = $"select * from users where login=N'{choose_login}'";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AddEditUser newUser = new AddEditUser(reader.GetString(1).Trim(), reader.GetString(2).Trim(), reader.GetString(3).Trim(), reader.GetString(8).Trim(), reader.GetString(7).Trim(), reader.GetInt32(9), reader.GetString(5).Trim(), reader.GetString(6).Trim(), reader.GetString(4).Trim());
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
                reader.Close();
                connection.Close();
                connect_people();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Nickname.Hide();
            stat.Location = loc;
            stat.Show();
            Operations.Hide();
            People.Hide();
            Wallet.Hide();
            Profile.Hide();
            label2.Text = "СТАТИСТИКА";
            try
            {
                string query = $"select Critetion,Sum,Date,Commentary,Operation,ID_money_operations from Money_operations join wallet on Wallet.ID_wallet=Money_operations.ID_wallet join users on users.ID_user=Wallet.ID_user";
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

        private void add_operation_Click(object sender, EventArgs e)
        {
            panel8.Show();
            button4.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
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
                connect_operations();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            panel8.Hide();
        }

        private void edit_operation_Click(object sender, EventArgs e)
        {
            panel8.Show();
            button4.Show();
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

        private void button4_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            panel8.Hide();
        }

        private void add_wallet_Click(object sender, EventArgs e)
        {
            new_wallet.Show();
            button8.Show();
            edit_currency.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                string query = $"insert into wallet values({comboBox1.Text},0,GETDATE(),N'{currency.Text}')";
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
            button8.Hide();
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

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                string query = $"select Critetion,Sum,Date,Commentary,Operation,ID_money_operations from Money_operations join wallet on Wallet.ID_wallet=Money_operations.ID_wallet join users on users.ID_user=Wallet.ID_user where critetion=N'{textBox5.Text}' and sum={textBox7.Text} and date='{textBox6.Text}' and Commentary=N'{textBox8.Text}' and operation={textBox9.Text}";
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

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                
                label21.Text = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string query = $"select Critetion,Sum,Date,Commentary,Operation,ID_money_operations from Money_operations join wallet on Wallet.ID_wallet=Money_operations.ID_wallet join users on users.ID_user=Wallet.ID_user where date BETWEEN N'{dateTimePicker1.Value.ToString("yyyy-MM-dd")}' AND N'{dateTimePicker2.Value.ToString("yyyy-MM-dd")}'";
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

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.Image = Properties.Resources.close2;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = Properties.Resources.close;
        }
    }
}

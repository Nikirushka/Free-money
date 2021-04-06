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
    public partial class Groups : Form
    {
        SqlConnection connection = null;
        SqlDataReader reader = null;
        SqlCommand cmd;
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|courses.mdf';Integrated Security=True;Connect Timeout=30";
        public Groups()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Groups_Load(object sender, EventArgs e)
        {
            panel3.Hide();
            Connect_groups();
            panel1.Hide();
            panel2.Hide();
        }
        Point lastpoint;

        private void Groups_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }
        private void Groups_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        public void Connect_groups()
        {
            string query = $"select Number,Levels.Level, Levels.Name, Teachers.Name as TName,Teachers.Surname as TSurname,Teachers.Patronymic as TPatr, Teachers.Phone as TPhone, Teachers.Age as TAge, Students.Name as SName, Students.Surname as SSurname, Students.Patronymic as SPatr, Students.Phone as SPhone, Students.Age as SAge  from Groups join Teachers on groups.ID_teacher=Teachers.ID_teacher join Levels on levels.ID_level = Groups.ID_level join Students on Students.ID_group = Groups.ID_group";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(query, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string num, lev, name, tname, tsurname, tpatr, tphone, tage, sname, ssurname, spatr,sphone,sage;
            if (textBox1.Text == "Number") num = "";
            else num = textBox1.Text;
            if (textBox2.Text == "Level") lev = "";
            else lev = textBox2.Text;
            if (textBox3.Text == "Name") name = "";
            else name = textBox3.Text;
            if (textBox4.Text == "TName") tname = "";
            else tname = textBox4.Text;
            if (textBox5.Text == "TSurname") tsurname = "";
            else tsurname = textBox5.Text;
            if (textBox6.Text == "TPatr") tpatr = "";
            else tpatr = textBox6.Text;
            if (textBox7.Text == "TPhone") tphone = "";
            else tphone = textBox7.Text;
            if (textBox8.Text == "TAge") tage = "";
            else tage = textBox8.Text;
            if (textBox9.Text == "SName") sname = "";
            else sname = textBox9.Text;
            if (textBox10.Text == "SSurname") ssurname = "";
            else ssurname = textBox10.Text;
            if (textBox11.Text == "SPatr") spatr = "";
            else spatr = textBox11.Text;
            if (textBox12.Text == "SPhone") sphone = "";
            else sphone = textBox12.Text;
            if (textBox13.Text == "SAge") sage = "";
            else sage = textBox13.Text;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"select Number,Levels.Level, Levels.Name, Teachers.Name as TName,Teachers.Surname as TSurname,Teachers.Patronymic as TPatr, Teachers.Phone as TPhone, Teachers.Age as TAge, Students.Name as SName, Students.Surname as SSurname, Students.Patronymic as SPatr, Students.Phone as SPhone, Students.Age as SAge from Groups join Teachers on groups.ID_teacher=Teachers.ID_teacher join Levels on levels.ID_level = Groups.ID_level join Students on Students.ID_group = Groups.ID_group where Number like N'%{num}%' and Level like N'%{lev}%' and Levels.Name like N'%{name}%' and Teachers.Name like N'%{tname}%' and Teachers.Surname like N'%{tsurname}%' and Teachers.Patronymic like N'%{tpatr}%'and Teachers.Phone like N'%{tphone}%' and Teachers.Age like N'%{tage}%' and  Students.Name like N'%{sname}%' and  Students.Surname like N'%{ssurname}%' and  Students.Patronymic like N'%{spatr}%' and Students.Phone like N'%{sphone}%' and Students.Age like N'%{sage}%'";
                cmd = new SqlCommand(query, connection);
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.AllowUserToAddRows = false; 
                    adapter = new SqlDataAdapter(query, connection);
                    ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            connection.Close();
            }
        }
        public void Connect_teachers()
        {
            string query = $"select * from all_information_about_teachers";
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(query, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView2.DataSource = ds.Tables[0];
            }
            foreach (DataGridViewColumn column in dataGridView2.Columns)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        public void Connect_students()
        {
            string query = $"select * from all_information_about_students";
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(query, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView2.DataSource = ds.Tables[0];
            }
            foreach (DataGridViewColumn column in dataGridView2.Columns)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button12.Hide();
            button11.Hide();
            button10.Hide();
            panel1.Show();
            panel2.Hide();
            Connect_teachers();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button13.Hide();
            button14.Hide();
            comboBox1.Hide();
            label13.Hide();
            panel2.Show();
            button9.Hide();
            button8.Show();
            label2.Text = "Добавление изменение преподавателя :";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel2.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int check = 0;
                string query = $"exec CheckLogin N'{textBox25.Text}'";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    check = reader.GetInt32(0);
                }
                reader.Close();
                connection.Close();
                if (check == 1)
                {
                    MessageBox.Show("Пользователь с таким логином существует!\nПридумайте другой :)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                else
                {
                    connection.Open();
                    query = $"insert into Users values (N'{textBox25.Text}',N'{textBox26.Text}',1,GETDATE());";
                    cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    query = $"select ID_user from Users where Login=N'{textBox25.Text}' and Password=N'{textBox26.Text}';";
                    cmd = new SqlCommand(query, connection);
                    reader = cmd.ExecuteReader();
                    int newIdTeacher = 0;
                    while (reader.Read())
                    {
                        newIdTeacher = reader.GetInt32(0);
                    }
                    reader.Close();
                    query = $"insert into teachers values ({newIdTeacher},N'{textBox27.Text}',N'{textBox28.Text}',N'{textBox29.Text}',N'{textBox30.Text}',{textBox24.Text},{textBox23.Text});";
                    cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                    
            }
            panel2.Hide();
            Connect_teachers();
            
        }
        string level,login,password,name,surname,patr,phone,age;

        private void button11_Click(object sender, EventArgs e)
        {
            button13.Show();
            try
            {
                ////Открываем подключение\
                connection = new SqlConnection(connectionString);
                connection.Open();
                ////string selectColumns = &quot;SELECT COLUMN_NAME FROM
                string select_num = $"select Groups.Number from Groups";
                List<string> fd = new List<string>();
                cmd = new SqlCommand(select_num, connection);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    fd.Add(reader.GetString(0));
                }
                //list - название компонента ComboBox
                comboBox1.DataSource = fd;
                reader.Close();
                ////list - название компонента ComboBox
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            comboBox1.Show();
            label13.Show();
            label2.Text = "Добавление изменение студента :";
            int index = 0;
            foreach (DataGridViewCell cell in dataGridView2.SelectedCells)
            {
                index = cell.RowIndex;
            }
            name = Convert.ToString(dataGridView2[0, index].Value);
            surname = Convert.ToString(dataGridView2[1, index].Value);
            patr = Convert.ToString(dataGridView2[2, index].Value);
            phone = Convert.ToString(dataGridView2[4, index].Value);
            age = Convert.ToString(dataGridView2[3, index].Value);
            level = Convert.ToString(dataGridView2[5, index].Value);
            login = Convert.ToString(dataGridView2[8, index].Value);
            password = Convert.ToString(dataGridView2[9, index].Value);
            textBox27.Text = Convert.ToString(dataGridView2[0, index].Value);
            textBox28.Text = Convert.ToString(dataGridView2[1, index].Value);
            textBox29.Text = Convert.ToString(dataGridView2[2, index].Value);
            textBox23.Text = Convert.ToString(dataGridView2[3, index].Value);
            textBox30.Text = Convert.ToString(dataGridView2[4, index].Value);
            textBox24.Text = "ID_Level";
            textBox25.Text = Convert.ToString(dataGridView2[8, index].Value);
            textBox26.Text = Convert.ToString(dataGridView2[9, index].Value);
            panel2.Show();
            button9.Hide();
            button8.Hide();
            button14.Hide();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button13.Hide();
            button14.Show();
            comboBox1.Show();
            label13.Show();
            label2.Text = "Добавление изменение студента :";
            panel2.Show();
            button9.Hide();
            button8.Hide();
            try
            {
                ////Открываем подключение\
                connection = new SqlConnection(connectionString);
                connection.Open();
                ////string selectColumns = &quot;SELECT COLUMN_NAME FROM
                string select_num = $"select Groups.Number from Groups";
                List<string> fd = new List<string>();
                cmd = new SqlCommand(select_num, connection);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    fd.Add(reader.GetString(0));
                }
                //list - название компонента ComboBox
                comboBox1.DataSource = fd;
                reader.Close();
                ////list - название компонента ComboBox
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                int check = 0;
                string query = $"exec CheckLogin N'{textBox25.Text}'";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    check = reader.GetInt32(0);
                }
                reader.Close();
                connection.Close();
                if (check == 1)
                {
                    MessageBox.Show("Пользователь с таким логином существует!\nПридумайте другой :)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                else
                {
                    connection.Open();
                    query = $"insert into Users values (N'{textBox25.Text}',N'{textBox26.Text}',0,GETDATE());";
                    cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    query = $"select ID_user from Users where Login=N'{textBox25.Text}' and Password=N'{textBox26.Text}';";
                    cmd = new SqlCommand(query, connection);
                    reader = cmd.ExecuteReader();
                    int newIdTeacher = 0;
                    while (reader.Read())
                    {
                        newIdTeacher = reader.GetInt32(0);
                    }
                    reader.Close();
                    query = $"select ID_group from groups where Number=N'{comboBox1.Text}';";
                    cmd = new SqlCommand(query, connection);
                    reader = cmd.ExecuteReader();
                    int group = 0;
                    while (reader.Read())
                    {
                        group = reader.GetInt32(0);
                    }
                    reader.Close();
                    query = $"insert into students values ({newIdTeacher},N'{textBox27.Text}',N'{textBox28.Text}',N'{textBox29.Text}',N'{textBox23.Text}',N'{textBox30.Text}',N'{textBox24.Text}',{group},0);";
                    cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                
            }
            panel2.Hide();
            Connect_students();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Connect_groups();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            string query = $"select Number,Teachers.Name as TName,Surname,Level,Levels.Name as LName,groups.ID_group from Groups join Teachers on Teachers.ID_teacher=Groups.ID_teacher join Levels on Levels.ID_level=Groups.ID_level";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(query, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {
                int index = 0;
                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    index = cell.RowIndex;
                }
               string id_goup= Convert.ToString(dataGridView1[5, index].Value);
                connection = new SqlConnection(connectionString);
                connection.Open();
                
                string delQuery = $"DELETE FROM groups WHERE id_group = N'{id_goup}'";

                cmd = new SqlCommand(delQuery, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Connect_teachers();
            Connect_groups();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            button22.Hide();
            panel3.Show();
            try
            {
                ////Открываем подключение\
                connection = new SqlConnection(connectionString);
                connection.Open();
                ////string selectColumns = &quot;SELECT COLUMN_NAME FROM
                string select_num = $"select Surname from Teachers";
                List<string> fd = new List<string>();
                cmd = new SqlCommand(select_num, connection);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    fd.Add(reader.GetString(0));
                }
                //list - название компонента ComboBox
                comboBox2.DataSource = fd;
                reader.Close();
                ////list - название компонента ComboBox
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            button20.Hide();
            panel3.Show();
            button22.Show();
            try
            {
                ////Открываем подключение\
                connection = new SqlConnection(connectionString);
                connection.Open();
                ////string selectColumns = &quot;SELECT COLUMN_NAME FROM
                string select_num = $"select Surname from Teachers";
                List<string> fd = new List<string>();
                cmd = new SqlCommand(select_num, connection);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    fd.Add(reader.GetString(0));
                }
                //list - название компонента ComboBox
                comboBox2.DataSource = fd;
                reader.Close();
                ////list - название компонента ComboBox
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            int index = 0;
            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                index = cell.RowIndex;
            }
            textBox14.Text = Convert.ToString(dataGridView1[0, index].Value);
            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                int newIdTeacher = 0;
                connection.Open();
                string query = $"select ID_user from Users where Login=N'{login}' and Password=N'{password}';";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    newIdTeacher = reader.GetInt32(0);
                }
                reader.Close();
                query = $"select ID_student from students where ID_user={newIdTeacher};";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    newIdTeacher = reader.GetInt32(0);
                }
                reader.Close();
                query = $"select ID_group from groups where Number=N'{comboBox1.Text}';";
                cmd = new SqlCommand(query, connection);
                reader = cmd.ExecuteReader();
                int group = 0;
                while (reader.Read())
                {
                    group = reader.GetInt32(0);
                }
                reader.Close();
                query = $"update students set Name=N'{textBox27.Text}',Surname=N'{textBox28.Text}',Patronymic=N'{textBox29.Text}',Phone=N'{textBox30.Text}',ID_level=N'{textBox24.Text}',Age=N'{textBox23.Text}',Id_group=N'{group}' where ID_student={newIdTeacher};";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                panel2.Hide();
                Connect_teachers();
                Connect_groups();
                Connect_students();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string queryy = $"select ID_teacher from teachers where Surname=N'{comboBox2.Text}';";
                cmd = new SqlCommand(queryy, connection);
                reader = cmd.ExecuteReader();
                int newIdTeacher = 0;
                while (reader.Read())
                {
                    newIdTeacher = reader.GetInt32(0);
                }
                reader.Close();
               
                queryy = $"insert into groups values({newIdTeacher},N'{textBox14.Text}',{textBox15.Text})";
                cmd = new SqlCommand(queryy, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            panel2.Hide();
            string query = $"select Number,Teachers.Name as TName,Surname,Level,Levels.Name as LName,groups.ID_group from Groups join Teachers on Teachers.ID_teacher=Groups.ID_teacher join Levels on Levels.ID_level=Groups.ID_level";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(query, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            panel3.Hide();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string queryy = $"select ID_teacher from teachers where Surname=N'{comboBox2.Text}';";
                    cmd = new SqlCommand(queryy, connection);
                    reader = cmd.ExecuteReader();
                    int newIdTeacher = 0;
                    while (reader.Read())
                    {
                        newIdTeacher = reader.GetInt32(0);
                    }
                    reader.Close();
                    int index = 0;
                    foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                    {
                        index = cell.RowIndex;
                    }
                    string id_goup = Convert.ToString(dataGridView1[5, index].Value);
                    queryy = $"update groups set id_teacher={newIdTeacher}, Number=N'{textBox14.Text}', id_level={textBox15.Text} where id_group={id_goup}";
                    cmd = new SqlCommand(queryy, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                string query = $"select Number,Teachers.Name as TName,Surname,Level,Levels.Name as LName,groups.ID_group from Groups join Teachers on Teachers.ID_teacher=Groups.ID_teacher join Levels on Levels.ID_level=Groups.ID_level";
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.AllowUserToAddRows = false;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    adapter = new SqlDataAdapter(query, connection);

                    ds = new DataSet();
                    adapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];
                }
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                panel3.Hide();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            panel3.Hide();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                int index = 0;
                foreach (DataGridViewCell cell in dataGridView2.SelectedCells)
                {
                    index = cell.RowIndex;
                }
                connection = new SqlConnection(connectionString);
                connection.Open();
                login = Convert.ToString(dataGridView2[8, index].Value);
                password = Convert.ToString(dataGridView2[9, index].Value);
                int newIdTeacher = 0;
                string query = $"select ID_user from Users where Login=N'{login}' and Password=N'{password}';";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    newIdTeacher = reader.GetInt32(0);
                }
                reader.Close();
                string delQuery = $"DELETE FROM students WHERE id_user = N'{newIdTeacher}'";

                cmd = new SqlCommand(delQuery, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            Connect_teachers();
            Connect_groups();
            Connect_students();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Show();
            panel2.Hide();
            Connect_students();
            button12.Show();
            button11.Show();
            button10.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int index = 0;
                foreach (DataGridViewCell cell in dataGridView2.SelectedCells)
                {
                    index = cell.RowIndex;
                }
                connection = new SqlConnection(connectionString);
                connection.Open();
                login = Convert.ToString(dataGridView2[7, index].Value);
                password = Convert.ToString(dataGridView2[8, index].Value);
                int newIdTeacher = 0;
                string query = $"select ID_user from Users where Login=N'{login}' and Password=N'{password}';";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    newIdTeacher = reader.GetInt32(0);
                }
                reader.Close();
                string delQuery = $"DELETE FROM teachers WHERE id_user = N'{newIdTeacher}'";

                cmd = new SqlCommand(delQuery, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Connect_students();
            Connect_teachers();
            Connect_groups();
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            comboBox1.Hide();
            label13.Hide();
            label2.Text = "Добавление изменение преподавателя :";
            panel2.Show();
            button8.Hide();
            button9.Show();
            int index = 0;
            foreach (DataGridViewCell cell in dataGridView2.SelectedCells)
            {
                index = cell.RowIndex;
            }
            name = Convert.ToString(dataGridView2[0, index].Value);
            surname = Convert.ToString(dataGridView2[1, index].Value);
            patr = Convert.ToString(dataGridView2[2, index].Value);
            phone = Convert.ToString(dataGridView2[3, index].Value);
            age = Convert.ToString(dataGridView2[4, index].Value);
            level = Convert.ToString(dataGridView2[5, index].Value);
            login = Convert.ToString(dataGridView2[7, index].Value);
            password = Convert.ToString(dataGridView2[8, index].Value);
            textBox27.Text = Convert.ToString(dataGridView2[0, index].Value);
            textBox28.Text = Convert.ToString(dataGridView2[1, index].Value);
            textBox29.Text = Convert.ToString(dataGridView2[2, index].Value);
            textBox30.Text = Convert.ToString(dataGridView2[3, index].Value);
            textBox23.Text = Convert.ToString(dataGridView2[4, index].Value);
            textBox24.Text = "ID_Level";
            textBox25.Text = Convert.ToString(dataGridView2[7, index].Value);
            textBox26.Text = Convert.ToString(dataGridView2[8, index].Value);

        }

        private void button9_Click(object sender, EventArgs e)
        {
            try 
            { 
            connection = new SqlConnection(connectionString);
            int newIdTeacher = 0;
            connection.Open();
            string query = $"select ID_user from Users where Login=N'{login}' and Password=N'{password}';";
            cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                newIdTeacher = reader.GetInt32(0);
            }
            reader.Close();
            query = $"select ID_teacher from Teachers where Teachers.ID_user={newIdTeacher};";
            cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                newIdTeacher = reader.GetInt32(0);
            }
            reader.Close();
            query = $"update Teachers set Name=N'{textBox27.Text}',Surname=N'{textBox28.Text}',Patronymic=N'{textBox29.Text}',Phone=N'{textBox30.Text}',ID_level=N'{textBox24.Text}',Age=N'{textBox23.Text}' where ID_teacher={newIdTeacher};";
            cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                panel2.Hide();
                Connect_teachers();
                Connect_groups();
        }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}
    }
}

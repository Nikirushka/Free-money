using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Study
{
    public partial class Student : Form
    {

        SqlConnection connection = null;
        SqlDataReader reader = null;
        SqlCommand cmd;
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|courses.mdf';Integrated Security=True;Connect Timeout=30";
        public Student()
        {
            InitializeComponent();
        }
        string us_id;
        public Student(string user_id)
        {
            InitializeComponent();
            us_id = user_id;
        }
        Point loc = new Point(12, 182);
        private void Student_Load(object sender, EventArgs e)
        {
            panel7.Hide();
            panel1.Hide();
            panel2.Hide();
            panel3.Hide();
        }
        Point lastpoint;

        private void Student_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void Student_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel1.Show();
            panel1.Show();
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                panel1.Show();
                string text = $"select Name,Surname,Patronymic,Phone,Age,Login,Password,Students.ID_level, groups.Number from Students join Groups on Groups.ID_group=Students.ID_group join Users on Students.ID_user=users.ID_user where users.ID_user={us_id}";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = reader.GetString(0).Trim();
                    textBox2.Text = reader.GetString(1).Trim();
                    textBox3.Text = reader.GetString(2).Trim();
                    textBox4.Text = reader.GetString(3).Trim();
                    textBox5.Text = reader.GetInt32(4).ToString();
                    textBox6.Text = reader.GetString(5).Trim();
                    textBox7.Text = reader.GetString(6).Trim();
                    textBox8.Text = reader.GetInt32(7).ToString();
                    label10.Text = "Group : " + reader.GetString(8);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            panel1.Show();
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                panel1.Show();
                string text = $"select Name,Surname,Patronymic,Phone,Age,Login,Password,Students.ID_level, groups.Number from Students join Groups on Groups.ID_group=Students.ID_group join Users on Students.ID_user=users.ID_user where users.ID_user={us_id}";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = reader.GetString(0).Trim();
                    textBox2.Text = reader.GetString(1).Trim();
                    textBox3.Text = reader.GetString(2).Trim();
                    textBox4.Text = reader.GetString(3).Trim();
                    textBox5.Text = reader.GetInt32(4).ToString();
                    textBox6.Text = reader.GetString(5).Trim();
                    textBox7.Text = reader.GetString(6).Trim();
                    textBox8.Text = reader.GetInt32(7).ToString();
                    label10.Text = "Group : " + reader.GetString(8);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            connection = new SqlConnection(connectionString);
            int newIdTeacher = 0;
            connection.Open();
            string query = $"select ID_student from students join users on users.id_user=students.id_user where users.id_user={us_id}";
            cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                newIdTeacher = reader.GetInt32(0);
            }
            reader.Close();
            query = $"update students set Name=N'{textBox1.Text}', Surname=N'{textBox2.Text}', Patronymic=N'{textBox3.Text}',Phone=N'{textBox4.Text}',Age={textBox5.Text},id_level='{textBox8.Text}' where id_student='{newIdTeacher}'";
            cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            query = $"update users set Login=N'{textBox6.Text}', Password=N'{textBox7.Text}' where ID_user={us_id}";
            cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            panel1.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Show();
            panel2.Location = loc;
            try
            {
                int id_level = 1;
                connection = new SqlConnection(connectionString);
                connection.Open();
                string text = $"select Students.ID_level from Students join Groups on Groups.ID_group=Students.ID_group join Users on Students.ID_user=users.ID_user where users.ID_user={us_id}";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id_level = reader.GetInt32(0);
                }
                reader.Close();
                text = $"SELECT top 1 * FROM Word where ID_level={id_level} ORDER BY NEWID()";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    label12.Text = "1." + reader.GetString(1).Trim();
                    label17.Text = "1." + reader.GetString(2).Trim();
                }
                reader.Close();
                text = $"SELECT top 1 * FROM Word where ID_level={id_level} ORDER BY NEWID()";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    label13.Text = "1." + reader.GetString(1).Trim();
                    label16.Text = "1." + reader.GetString(2).Trim();
                }
                reader.Close();
                text = $"SELECT top 1 * FROM Word where ID_level={id_level} ORDER BY NEWID()";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    label14.Text = "1." + reader.GetString(1).Trim();
                    label15.Text = "1." + reader.GetString(2).Trim();
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            panel2.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel7.Show();
            panel7.Location = loc;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int id_level = 1;
                connection = new SqlConnection(connectionString);
                connection.Open();
                string text = $"select Students.ID_level from Students join Groups on Groups.ID_group=Students.ID_group join Users on Students.ID_user=users.ID_user where users.ID_user={us_id}";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id_level = reader.GetInt32(0);
                }
                reader.Close();
                text = $"SELECT top 1 * FROM Word where ID_level={id_level} ORDER BY NEWID()";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    label12.Text = "1." + reader.GetString(1).Trim();
                    label17.Text = "1." + reader.GetString(2).Trim();
                }
                reader.Close();
                text = $"SELECT top 1 * FROM Word where ID_level={id_level} ORDER BY NEWID()";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    label13.Text = "1." + reader.GetString(1).Trim();
                    label16.Text = "1." + reader.GetString(2).Trim();
                }
                reader.Close();
                text = $"SELECT top 1 * FROM Word where ID_level={id_level} ORDER BY NEWID()";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    label14.Text = "1." + reader.GetString(1).Trim();
                    label15.Text = "1." + reader.GetString(2).Trim();
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            panel3.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label28.Hide();
            label24.Hide();
            label25.Hide();
            label26.Hide();
            button10.Show();
            button12.Hide();
            label27.Hide();
            panel3.Show();
            panel3.Location = loc;
            try
            {
                string ans1 = "", ans2 = "", ans3 = "";
                int id_level = 1;
                connection = new SqlConnection(connectionString);
                connection.Open();
                string text = $"select Students.ID_level from Students join Groups on Groups.ID_group=Students.ID_group join Users on Students.ID_user=users.ID_user where users.ID_user={us_id}";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id_level = reader.GetInt32(0);
                }
                reader.Close();
                text = $"SELECT top 1 * FROM Sentence where ID_level={id_level} ORDER BY NEWID()";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    label21.Text = "Предложение :" + reader.GetString(1).Trim();
                    ans1 = reader.GetString(3).Trim();

                }
                reader.Close();
                text = $"SELECT top 1 * FROM Sentence where ID_level={id_level} ORDER BY NEWID()";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    label22.Text = "Предложение :" + reader.GetString(1).Trim();
                    ans2 = reader.GetString(3).Trim();

                }
                reader.Close();
                text = $"SELECT top 1 * FROM sentence where ID_level={id_level} ORDER BY NEWID()";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    label23.Text = "Предложение :" + reader.GetString(1).Trim();
                    ans3 = reader.GetString(3).Trim();
                }
                radioButton1.Text = ans1;
                radioButton2.Text = ans3;
                radioButton4.Text = ans1;
                radioButton3.Text = ans2;
                radioButton6.Text = ans2;
                radioButton5.Text = ans3;
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label24.Show();
            label25.Show();
            label26.Show();
            if (radioButton1.Checked)
            {
                label24.Text = "Правильно";
            }
            else label24.Text = "Ошибка";
            if (radioButton3.Checked)
            {
                label25.Text = "Правильно";
            }
            else label25.Text = "Ошибка";
            if (radioButton5.Checked)
            {
                label26.Text = "Правильно";
            }
            else label26.Text = "Ошибка";

        }

        private void button12_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            label24.Show();
            label25.Show();
            label26.Show();
            int count =3;
            if (radioButton1.Checked)
            {
                label24.Text = "Правильно";
                count--;
            }
            else label24.Text = "Ошибка";
            if (radioButton3.Checked)
            {
                label25.Text = "Правильно";
                count--;
            }
            else label25.Text = "Ошибка";
            if (radioButton5.Checked)
            {
                label26.Text = "Правильно";
                count--;
            }
            else label26.Text = "Ошибка";
            int mark = 0;
            if (count == 3) mark = 1;
            else if (count == 2) mark = 3;
            else if (count == 1) mark = 7;
            else if (count == 0) mark = 10;

            label27.Text = "Оценка :" + mark;
            label27.Show();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"select ID_student from students where id_user={us_id};";
                cmd = new SqlCommand(query, connection);
                reader = cmd.ExecuteReader();
                int newIdTeacher = 0;
                while (reader.Read())
                {
                    newIdTeacher = reader.GetInt32(0);
                }
                reader.Close();
               
                query = $"insert into tests values({newIdTeacher},{mark},'{i}',3,{count});";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }

        }
        int i = 0;
        private void button9_Click(object sender, EventArgs e)
        {
            panel3.Location = loc;
            timer1.Enabled = true;
            label28.Show();
            label24.Hide();
            label25.Hide();
            label26.Hide();
            button12.Show();
            button10.Hide();
            label27.Hide();
            panel3.Show();
            try
            {
                string ans1 = "", ans2 = "", ans3 = "";
                int id_level = 1;
                connection = new SqlConnection(connectionString);
                connection.Open();
                string text = $"select Students.ID_level from Students join Groups on Groups.ID_group=Students.ID_group join Users on Students.ID_user=users.ID_user where users.ID_user={us_id}";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    id_level = reader.GetInt32(0);
                }
                reader.Close();
                text = $"SELECT top 1 * FROM Sentence where ID_level={id_level} ORDER BY NEWID()";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    label21.Text = "Предложение :" + reader.GetString(1).Trim();
                    ans1 = reader.GetString(3).Trim();

                }
                reader.Close();
                text = $"SELECT top 1 * FROM Sentence where ID_level={id_level} ORDER BY NEWID()";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    label22.Text = "Предложение :" + reader.GetString(1).Trim();
                    ans2 = reader.GetString(3).Trim();

                }
                reader.Close();
                text = $"SELECT top 1 * FROM sentence where ID_level={id_level} ORDER BY NEWID()";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    label23.Text = "Предложение :" + reader.GetString(1).Trim();
                    ans3 = reader.GetString(3).Trim();
                }
                radioButton1.Text = ans1;
                radioButton2.Text = ans3;
                radioButton4.Text = ans1;
                radioButton3.Text = ans2;
                radioButton6.Text = ans2;
                radioButton5.Text = ans3;
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            label28.Text = "Время : " + i.ToString();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Польский алфавит и правила чтения - a1")
                webBrowser1.Navigate("https://www.youtube.com/watch?v=vuHBND0dqVM&feature=emb_logo");
            else if (comboBox1.Text == "Базовые фразы - а1")
                webBrowser1.Navigate("https://www.youtube.com/watch?v=VybjxBd_d3Q&feature=emb_logo");
            else if (comboBox1.Text == "Глагол быть - а1")
                webBrowser1.Navigate("https://www.youtube.com/watch?v=d_4skYyGBfs");
            else if (comboBox1.Text == "Покупки - а2")
                webBrowser1.Navigate("https://www.youtube.com/watch?v=E9gRcqzXDV0");
            else if (comboBox1.Text == "Винительный падеж - а2")
                webBrowser1.Navigate("https://www.youtube.com/watch?v=w9FqvLr558U");
            else if (comboBox1.Text == "StandUp на польском  - a2")
                webBrowser1.Navigate("https://www.youtube.com/watch?v=dvQ1xWi4fGQ&feature=emb_logo");
            else if (comboBox1.Text == "Еда в ресторане - б1")
                webBrowser1.Navigate("https://www.youtube.com/watch?v=_eTPmj39N0Y");
            else if (comboBox1.Text == "Хобби - б1")
                webBrowser1.Navigate("https://www.youtube.com/watch?v=N1QQCKBoDLI&feature=emb_logo");
            else if (comboBox1.Text == "Польский слэнг - б2")
                webBrowser1.Navigate("https://www.youtube.com/watch?v=TcFS4FfwH5w&feature=emb_logo");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            panel7.Hide();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("podcast.mp3");
        }
    }
}

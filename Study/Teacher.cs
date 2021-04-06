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
    public partial class Teacher : Form
    {
        int id_user;
        SqlConnection connection = null;
        SqlDataReader reader = null;
        SqlCommand cmd;
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|courses.mdf';Integrated Security=True;Connect Timeout=30";
        public Teacher()
        {
            InitializeComponent();
        }

        public Teacher(string id)
        {
            InitializeComponent();
            id_user =Convert.ToInt32(id);
        }
        public void Connect_tests()
        {
            string query = $"select Students.Name,Students.Surname,Tests.Mark,Tests.Time,Tests.Questions,Tests.Mistakes from tests join Students on Students.ID_student= Tests.ID_student ";
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
        public void Connect_level()
        {
            string query = $"select Level,Name from Levels";
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

        private void Teacher_Load(object sender, EventArgs e)
        {
            Connect_tests();
            panel1.Hide();
        }
        Point lastpoint;

        private void Teacher_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void Teacher_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label10.Text = "Данные по уровням :";
            Connect_level();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();   
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = Properties.Resources.profile_on;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.profile;
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel1.Hide();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            string query = $"exec edit_teacher {id_user},N'{textBox1.Text}',N'{textBox2.Text}',N'{textBox3.Text}',N'{textBox4.Text}',N'{textBox8.Text}',N'{textBox5.Text}'";
            cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            query = $"update users set Login=N'{textBox6.Text}', Password=N'{textBox7.Text}' where ID_user={id_user}";
            cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();
            panel1.Hide();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox2_MouseClick(object sender, MouseEventArgs e)
        {
            textBox2.Clear();
        }

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            textBox3.Clear();
        }

        private void textBox4_MouseClick(object sender, MouseEventArgs e)
        {
            textBox4.Clear();
        }

        private void textBox5_MouseClick(object sender, MouseEventArgs e)
        {
            textBox5.Clear();
        }

        private void textBox6_MouseClick(object sender, MouseEventArgs e)
        {
            textBox6.Clear();
        }

        private void textBox7_MouseClick(object sender, MouseEventArgs e)
        {
            textBox7.Clear();
        }

        private void textBox8_MouseClick(object sender, MouseEventArgs e)
        {
            textBox8.Clear();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                panel1.Show();
                string text = $"select Name,Surname,Patronymic,Phone,Age,Login,Password,ID_level from Teachers join Users on Teachers.ID_user=users.ID_user where ID_teacher={id_user}";
                cmd = new SqlCommand(text, connection);
                reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    textBox1.Text = reader.GetString(0).Trim();
                    textBox2.Text = reader.GetString(1).Trim();
                    textBox3.Text = reader.GetString(2).Trim();
                    textBox4.Text = reader.GetString(3).Trim();
                    textBox5.Text = reader.GetInt32(4).ToString();
                    textBox6.Text = reader.GetString(5).Trim();
                    textBox7.Text = reader.GetString(6).Trim();
                    textBox8.Text = reader.GetInt32(7).ToString();
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_MouseClick(object sender, MouseEventArgs e)
        {
            pictureBox1_MouseClick(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Groups groups = new Groups();
            DialogResult dialogResult = new DialogResult();
            dialogResult = groups.ShowDialog();
            if (dialogResult == DialogResult.Cancel)
            {
                this.Show();
            }
            else
            {
                this.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                panel1.Show();
                string text = $"select Name,Surname,Patronymic,Phone,Age,Login,Password,ID_level from Teachers join Users on Teachers.ID_user=users.ID_user where ID_teacher={id_user}";
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
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            panel1.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel1.Show();
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                panel1.Show();
                string text = $"select Name,Surname,Patronymic,Phone,Age,Login,Password,ID_level from Teachers join Users on Teachers.ID_user=users.ID_user where ID_teacher={id_user}";
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
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            label10.Text = "Данные по тестам :";
            Connect_tests();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            string name, surname, NameTest, Mark, Time, Questions, Mistakes;
            if (textBox9.Text == "Name") name = "%";
            else name = textBox9.Text;
            if (textBox10.Text == "Surname") surname = "%";
            else surname = textBox10.Text;
            if (textBox12.Text == "Mark") Mark = "%";
            else Mark = textBox12.Text;
            if (textBox13.Text == "Time") Time = "%";
            else Time = textBox13.Text;
            if (textBox14.Text == "Questions") Questions = "%";
            else Questions = textBox14.Text;
            if (textBox15.Text == "Mistakes") Mistakes = "%";
            else Mistakes = textBox15.Text;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"select Students.Name,Students.Surname,Tests.Mark,Tests.Time,Tests.Questions,Tests.Mistakes from tests join Students on Students.ID_student= Tests.ID_student where Students.Name like N'%{name}%' and Surname like N'%{surname}%' and Mark like N'%{Mark}%' and Time like N'%{Time}%' and Questions like N'%{Questions}%' and Mistakes like N'%{Mistakes}%'";
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

        private void button3_Click(object sender, EventArgs e)
        {
            Dictionary dictionary  = new Dictionary();
            DialogResult dialogResult = new DialogResult();
            dialogResult = dictionary.ShowDialog();
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

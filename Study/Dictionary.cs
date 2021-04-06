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
    public partial class Dictionary : Form
    {
        SqlConnection connection = null;
        SqlDataReader reader = null;
        SqlCommand cmd;
        DataSet ds;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|courses.mdf';Integrated Security=True;Connect Timeout=30";
        public Dictionary()
        {
            InitializeComponent();
        }

        private void Dictionary_Load(object sender, EventArgs e)
        {
            panel2.Hide();
            textBox1.Hide();
            textBox2.Hide();
            textBox4.Hide();
            textBox5.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label1.Text = "Предложения";
            button11.Hide();
            button10.Hide();
            button6.Hide();
            textBox5.Show();
            textBox4.Show();
            textBox1.Hide();
            textBox2.Hide();
            string query = $"select Sentence,Answer,Levels.Level, Levels.Name from Sentence join Levels on Levels.ID_level=Sentence.ID_level";
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

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Text = "Слова";
            button11.Show();
            button10.Show();
            button6.Show();
            textBox5.Hide();
            textBox4.Hide();
            textBox1.Show();
            textBox2.Show();
            string query = $"select Name_rus,Name_pol, Levels.Level, Levels.Name from Word join Levels on Levels.ID_level=Word.ID_level";
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

        private void button7_Click(object sender, EventArgs e)
        {
            panel2.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button8.Show();
            panel2.Hide();
            button9.Hide();
            button13.Hide();
            button14.Hide();
            panel2.Show();
            label4.Text = "Sentence";
            label5.Text = "Answer";
            

        }
        string a;
        private void button2_Click(object sender, EventArgs e)
        {
            
            panel2.Show();
            int index = 0;
            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                index = cell.RowIndex;
            }
            a = Convert.ToString(dataGridView1[0, index].Value);
            textBox24.Text = "ID_Level";
            textBox25.Text = Convert.ToString(dataGridView1[0, index].Value);
            textBox26.Text = Convert.ToString(dataGridView1[1, index].Value);
            button9.Show();
            button8.Hide();
            button13.Hide();
            button14.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                    connection.Open();
                    string query = $"insert into sentence values (N'{textBox25.Text}',N'{textBox24.Text}',N'{textBox26.Text}');";
                    cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
            }
            panel2.Hide();
            string query1 = $"select Sentence,Answer,Levels.Level, Levels.Name from Sentence join Levels on Levels.ID_level=Sentence.ID_level";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(query1, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    connection.Open();
                    string query = $"insert into word values (N'{textBox25.Text}',N'{textBox24.Text}',N'{textBox26.Text}');";
                    cmd = new SqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                panel2.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            string query1 = $"select Name_rus,Name_pol, Levels.Level, Levels.Name from Word join Levels on Levels.ID_level=Word.ID_level";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(query1, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();

                string query = $"update sentence set sentence=N'{textBox25.Text}', answer=N'{textBox26.Text}',id_level={textBox24.Text} where sentence=N'{a}';";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                panel2.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            string query1 = $"select Name_rus,Name_pol, Levels.Level, Levels.Name from Word join Levels on Levels.ID_level=Word.ID_level";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(query1, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            panel2.Hide();
            try
            {
                connection = new SqlConnection(connectionString);
                connection.Open();

                string query = $"update word set name_rus=N'{textBox25.Text}', name_pol=N'{textBox26.Text}',id_level={textBox24.Text} where name_rus=N'{a}';";
                cmd = new SqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                panel2.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            string query1 = $"select Name_rus,Name_pol, Levels.Level, Levels.Name from Word join Levels on Levels.ID_level=Word.ID_level";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(query1, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int index = 0;
                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    index = cell.RowIndex;
                }
                connection = new SqlConnection(connectionString);
                connection.Open();
                string delQuery = $"DELETE FROM sentence WHERE sentence = N'{Convert.ToString(dataGridView1[0, index].Value)}'";

                cmd = new SqlCommand(delQuery, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            string query1 = $"select Sentence,Answer,Levels.Level, Levels.Name from Sentence join Levels on Levels.ID_level=Sentence.ID_level";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(query1, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string sent, answ;
            if (textBox1.Text == "Name_rus") sent = "";
            else sent = textBox1.Text;
            if (textBox2.Text == "Name_pol") answ = "";
            else answ = textBox2.Text;


            string query = $"select Name_rus,Name_pol, Levels.Level, Levels.Name from Word join Levels on Levels.ID_level=Word.ID_level where Name_rus like N'%{sent}%' and Name_pol like N'%{answ}%'";
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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string sent, answ;
            if (textBox5.Text == "Sentence") sent = "";
            else sent = textBox5.Text;
            if (textBox4.Text == "Answer") answ = "";
            else answ = textBox4.Text;


            string query = $"select Sentence,Answer,Levels.Level, Levels.Name from Sentence join Levels on Levels.ID_level=Sentence.ID_level where sentence like N'%{sent}%' and answer like N'%{answ}%'";
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

        private void button11_Click(object sender, EventArgs e)
        {
            button14.Show();
            button8.Hide();
            panel2.Hide();
            button9.Hide();
            button13.Hide();
            
            panel2.Show();
            label4.Text = "Name_rus";
            label5.Text = "Name_pol";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel2.Show();
            int index = 0;
            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                index = cell.RowIndex;
            }
            a = Convert.ToString(dataGridView1[0, index].Value);
            textBox24.Text = "ID_Level";
            textBox25.Text = Convert.ToString(dataGridView1[0, index].Value);
            textBox26.Text = Convert.ToString(dataGridView1[1, index].Value);
            button14.Hide();
            button8.Hide();
            panel2.Hide();
            button9.Hide();
            button13.Show();

            panel2.Show();
            label4.Text = "Name_rus";
            label5.Text = "Name_pol";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                int index = 0;
                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    index = cell.RowIndex;
                }
                connection = new SqlConnection(connectionString);
                connection.Open();
                string delQuery = $"DELETE FROM word WHERE name_rus = N'{Convert.ToString(dataGridView1[0, index].Value)}'";

                cmd = new SqlCommand(delQuery, connection);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            string query1 = $"select Name_rus,Name_pol, Levels.Level, Levels.Name from Word join Levels on Levels.ID_level=Word.ID_level";
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(query1, connection);

                ds = new DataSet();
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            foreach (DataGridViewColumn column in dataGridView1.Columns)
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
    }
}

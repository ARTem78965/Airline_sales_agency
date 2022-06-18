using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Common;

namespace Курсовая_работа_1
{
    public partial class Site : Form
    {
        private const string bl = "Bilet";
        private const string re = "Reis";
        private const string pa = "Park";
        private const string us = "Users";
        DataSet ptkDataSet;
        SqlDataAdapter adapter;
        SqlConnection connect = new SqlConnection(@"Data Source=WINDOWS_X\SQLEXPRESS;Initial Catalog=db_registr;Integrated Security=True");
        SqlCommand command;
        DataTable table;
        public Site()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Avtorizacia auto = new Avtorizacia();
            auto.Show();
        }
        private void Site_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "DB_AirPlanec.DataTable1". При необходимости она может быть перемещена или удалена.
            this.DataTable1TableAdapter.Fill(this.DB_AirPlanec.DataTable1);
            ptkDataSet = new DataSet();
            adapter = new SqlDataAdapter();
            ptkDataSet.Tables.Add(bl);
            ptkDataSet.Tables.Add(re);
            ptkDataSet.Tables.Add(pa);
            ptkDataSet.Tables.Add(us);
            adapter = new SqlDataAdapter();

            command = new SqlCommand();
            command.Connection = connect;
            command.CommandType = CommandType.StoredProcedure;
            treeView1.Nodes.Add("Авиакомпания", "Авиакомпания", 0, 0);
            treeView1.Nodes[0].Nodes.Add(bl, "Билеты", 1, 1);
            treeView1.Nodes[0].Nodes.Add(re, "Рейсы", 2, 1);
            treeView1.Nodes[0].Nodes.Add(pa, "Парк", 3, 1);
            treeView1.Nodes[0].Nodes.Add(us, "Пользователь", 4, 1);
            treeView1.ExpandAll();
            label1.Text = Nikname.Text;
            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Parent != null)
            {
                string enityName = treeView1.SelectedNode.Name;
                table = ptkDataSet.Tables[enityName];
                command.CommandText = enityName + "_S";
                adapter.SelectCommand = command;
                table.Rows.Clear();
                adapter.Fill(table);
                connect.Open();
                SqlCommandBuilder cb = new SqlCommandBuilder(adapter);
                dataGridView1.DataSource = table;
                enityName = treeView1.SelectedNode.Name;
                bindingSource1.DataSource = ptkDataSet;
                bindingSource1.DataMember = enityName;
                dataGridView1.DataSource = bindingSource1;
                table = new DataTable();
                switch (enityName)
                {
                    case bl:
                        table = this.ptkDataSet.Tables[bl];
                        break;
                    case re:
                        table = this.ptkDataSet.Tables[re];
                        break;
                    case pa:
                        table = this.ptkDataSet.Tables[pa];
                        break;
                    case us:
                        table = this.ptkDataSet.Tables[us];
                        break;
                }

                connect.Close();
            }
        }
        private void button5_Click_1(object sender, EventArgs e)
        {
            if (textBox12.Text == "" || textBox5.Text == "")
            {
                label7.Visible = true;
                label7.Text = "Вы не заполнили все поля.";  //Сообщение
            }
            else
            {
                command = new SqlCommand($"INSERT INTO [Park] (number, Airplanec) VALUES(@number, @Airplanec); ", connect);
                //Запись в таблицу Парк
                command.Parameters.AddWithValue("number", textBox5.Text);
                command.Parameters.AddWithValue("Airplanec", textBox12.Text);
                connect.Open();
                command.ExecuteNonQuery();
                connect.Close();
                textBox12.Clear();
                MessageBox.Show("Добавлена новая запись");
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            textBox12.Clear();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox6.Text) && !string.IsNullOrWhiteSpace(textBox6.Text))
            {
                command = new SqlCommand($"delete from [Park] where [id]=@id", connect);
                command.Parameters.AddWithValue("id", textBox6.Text);
                connect.Open();
                command.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Запись удалена!");
                textBox6.Clear();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || dateTimePicker1.Text == "" || dateTimePicker2.Text == "" || dateTimePicker3.Text == "" || dateTimePicker4.Text == "" || textBox3.Text == "")
            {
                label10.Visible = true;
                label10.Text = "Вы не заполнили все поля.";  //Сообщение
            }
            else
            {
                command = new SqlCommand($"INSERT INTO [Reis] (Otkuda, Do_kuda, date_otp, time_otp, date_pri, time_pri, Код_парка) VALUES(@Otkuda, @Do_kuda, @date_otp, @time_otp, @date_pri, @time_pri, @Код_парка); ", connect);

                //Запись в таблиц
                command.Parameters.AddWithValue("Otkuda", textBox1.Text);
                command.Parameters.AddWithValue("Do_kuda", textBox2.Text);
                command.Parameters.AddWithValue("date_otp", dateTimePicker1.Text);
                command.Parameters.AddWithValue("time_otp", dateTimePicker2.Text);
                command.Parameters.AddWithValue("date_pri", dateTimePicker3.Text);
                command.Parameters.AddWithValue("time_pri", dateTimePicker4.Text);
                command.Parameters.AddWithValue("Код_парка", textBox3.Text);
                textBox1.Clear();
                textBox2.Clear();
                connect.Open();
                command.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Добавлена новая запись");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                command = new SqlCommand($"delete from [Reis] where [id]=@id", connect);
                command.Parameters.AddWithValue("id", textBox4.Text);
                connect.Open();
                command.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Запись удалена!");
                textBox6.Clear();
            }
        }
    }
}
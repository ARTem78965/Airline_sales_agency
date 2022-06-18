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
    public partial class Kassa : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=WINDOWS_X\SQLEXPRESS;Initial Catalog=db_registr;Integrated Security=True");
        SqlCommand command;
        SqlCommand select;
        SqlDataAdapter adapter;
        public Kassa()
        {
            InitializeComponent();
        }

        private void Cassa_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "cassa_b.Bilet_S". При необходимости она может быть перемещена или удалена.
            this.bilet_STableAdapter.Fill(this.cassa_b.Bilet_S);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "cassa_r.Reis_S". При необходимости она может быть перемещена или удалена.
            this.reis_STableAdapter.Fill(this.cassa_r.Reis_S);
            label1.Text = Nikname.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Avtorizacia auto = new Avtorizacia();
            auto.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || dateTimePicker1.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || comboBox1.Text == "")
            {
                label10.Visible = true;
                label10.Text = "Вы не заполнили все поля.";  //Сообщение
            }
            else
            {
                connect.Open();
                select = new SqlCommand($"SELECT id, date_otp, Otkuda, Do_kuda FROM [Reis] where date_otp = '{dateTimePicker1.Text}' and Otkuda =  '{textBox6.Text}' and Do_kuda =  '{textBox7.Text}' ;", connect);

                DataTable table = new DataTable();
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = select;
                adapter.Fill(table);
                if (table.Rows.Count > 0)
                {

                    command = new SqlCommand($"INSERT INTO [Bilet] (Fam, Name, Otch, mesto, Money, Passport, Код_рейса) VALUES(@Fam, @Name, @Otch, @mesto, @Money, @Passport, @Код_рейса); ", connect);
                    //Запись в таблицу БИЛЕТ
                    command.Parameters.AddWithValue("Fam", textBox1.Text);
                    command.Parameters.AddWithValue("Name", textBox2.Text);
                    command.Parameters.AddWithValue("Otch", textBox3.Text);
                    command.Parameters.AddWithValue("mesto", comboBox1.Text);
                    command.Parameters.AddWithValue("Passport", textBox4.Text);
                    command.Parameters.AddWithValue("Money", textBox8.Text);
                    command.Parameters.AddWithValue("Код_рейса", textBox10.Text);
                    MessageBox.Show("Билет оформлен!");
                    command.ExecuteNonQuery();
                    connect.Close();
                                
                }
                else
                {
                        MessageBox.Show("Нет рейсов",
                                        "Ошибка",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            long randnum2 = (long)(rand.NextDouble() * 900) + 1000;
            textBox8.Text = randnum2.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox5.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                command = new SqlCommand($"delete from [Bilet] where [id]=@id", connect);
                command.Parameters.AddWithValue("id", textBox5.Text);
                connect.Open();
                command.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Билет удален!");
                textBox6.Clear();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox15.Text) && !string.IsNullOrWhiteSpace(textBox15.Text) &&
                !string.IsNullOrEmpty(textBox14.Text) && !string.IsNullOrWhiteSpace(textBox14.Text) &&
                !string.IsNullOrEmpty(textBox13.Text) && !string.IsNullOrWhiteSpace(textBox13.Text) &&
                !string.IsNullOrEmpty(textBox12.Text) && !string.IsNullOrWhiteSpace(textBox12.Text) &&
                !string.IsNullOrEmpty(textBox9.Text) && !string.IsNullOrWhiteSpace(textBox9.Text) &&
                !string.IsNullOrEmpty(comboBox2.Text) && !string.IsNullOrWhiteSpace(comboBox2.Text)
                ) 
            {
                command = new SqlCommand($"update [Bilet] set Fam = @Fam, Name = @Name, Otch = @Otch, mesto=@mesto, Passport = @Passport where id = @id", connect);
                command.Parameters.AddWithValue("Fam", textBox15.Text);
                command.Parameters.AddWithValue("Name", textBox14.Text);
                command.Parameters.AddWithValue("Otch", textBox13.Text);
                command.Parameters.AddWithValue("Passport", textBox12.Text);
                command.Parameters.AddWithValue("mesto", comboBox2.Text);
                command.Parameters.AddWithValue("id", textBox9.Text);
                command.Parameters.AddWithValue("Код_рейса", textBox10.Text);
                connect.Open();
                command.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Запись изменена!");
            }
            
        }
    }
}

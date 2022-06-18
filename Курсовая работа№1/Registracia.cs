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
    public partial class Registracia : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=WINDOWS_X\SQLEXPRESS;Initial Catalog=db_registr;Integrated Security=True");
        public Registracia()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Проверка заполнения полей.
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.Text == "" || textBox4.Text == "" || textBox6.Text == "" || textBox7.Text == "")
            {
                label1.Visible = true;
                label1.Text = "Вы не заполнили все поля.";  //Сообщение
            }
            else
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Users] (Fam, Name, Otch, Role, Email, Telephone, Login, Password) VALUES(@Fam, @Name, @Otch, @Role, @Email, @Telephone, @Login, @Password); ", connect);

                //Регистрация
                command.Parameters.AddWithValue("Fam", textBox1.Text);
                command.Parameters.AddWithValue("Name", textBox2.Text);
                command.Parameters.AddWithValue("Otch", textBox3.Text);
                command.Parameters.AddWithValue("Role", comboBox1.Text);
                command.Parameters.AddWithValue("Email", textBox5.Text);
                command.Parameters.AddWithValue("Telephone", textBox4.Text);
                command.Parameters.AddWithValue("Login", textBox6.Text);
                command.Parameters.AddWithValue("Password", textBox7.Text);

                connect.Open();
                command.ExecuteNonQuery();
                connect.Close();

                Hide();
                Avtorizacia auto = new Avtorizacia();
                auto.Show();
            }
        }
    }
}

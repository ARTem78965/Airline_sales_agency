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
    public partial class Avtorizacia : Form
    {
        public Avtorizacia()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Hide();
            Registracia rg = new Registracia();
            rg.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connect = new SqlConnection(@"Data Source=WINDOWS_X\SQLEXPRESS;Initial Catalog=db_registr;Integrated Security=True");
            connect.Open();
            SqlCommand command = new SqlCommand($"SELECT * FROM [Users] where Login = '{textBox2.Text}' and Password =  '{textBox3.Text}' ;", connect);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    object Role = reader.GetValue(4);
                    if (Convert.ToString(Role) == "Администратор")
                    {
                        Hide();
                        Site st = new Site();
                        Nikname.Text = textBox2.Text;
                        st.Show();
                    }
                    else if (Convert.ToString(Role) == "Кассир")
                    {
                        Hide();
                        Kassa bl = new Kassa();
                        Nikname.Text = textBox2.Text;
                        bl.Show();
                    }
                    else if (Convert.ToString(Role) == "Клиент")
                    {
                        Hide();
                        Klient kl = new Klient();
                        Nikname.Text = textBox2.Text;
                        kl.Show();
                    }
                }
            }
            else
            {
                MessageBox.Show("Неверный логин и пароль",
                                "Ошибка",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Error);
                textBox2.Clear();
                textBox3.Clear();
            }
            connect.Close();
        }
    }
}

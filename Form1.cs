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
using ClassLibrary1;

namespace v2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        FormGuest formGuest = new FormGuest();
        FormCapt formCapt = new FormCapt();
        FormAdmin formAdmin = new FormAdmin();


        private void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;

            using (SqlConnection connection = new SqlConnection(BaseData.StringConnected))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM [dbo].[User] Where UserLogin = @login AND UserPassword = @password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);

                    int count = (int)command.ExecuteScalar();

                    if(count > 0)
                    {
                        if (BaseData.firstStart)
                        {
                            BaseData.firstStart = false;                            
                        }
                        else
                        {
                            DialogResult = DialogResult.Yes;                            
                        }
                        formAdmin.Show();
                        this.Hide();
                    }
                    else
                    {                        
                        DialogResult dialogResult = formCapt.ShowDialog();

                        if (dialogResult == DialogResult.Yes)
                        {
                            MessageBox.Show("Логин или пароль неверные, проверьте поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            Console.WriteLine("Ban");
                            BlockButtons();
                        }
                        else if (dialogResult == DialogResult.Cancel)
                        {
                            Console.WriteLine("Ban");
                            BlockButtons();
                        }
                    }
                    connection.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = formCapt.ShowDialog();

            if (dialogResult == DialogResult.Yes)
            {
                if (BaseData.firstStart)
                {
                    BaseData.firstStart = false;
                    formGuest.Show();
                }
                this.Hide();
            }
            else if (dialogResult == DialogResult.No)
            {
                Console.WriteLine("Ban");
                BlockButtons();
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                Console.WriteLine("Cancel");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(BaseData.firstStart)
            {
                Application.Exit();
            }
            else
            {
                this.Hide();
            }
        }
        private async void BlockButtons()
        {
            button2.Enabled = false;
            button1.Enabled = false;

            await Task.Delay(10000);

            button2.Enabled = true;
            button1.Enabled = true;
        }
    }
}

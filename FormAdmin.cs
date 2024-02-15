using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace v2
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        string selectedArticle;
        string imagePath;

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            BaseData.firstStart = true;
            form1.Show();
            this.Hide();

        }

        private void InsertImageToDatabase()
        {
            using (SqlConnection connection = new SqlConnection(BaseData.StringConnected))
            {
                connection.Open();

                string query = "UPDATE [dbo].[Product] SET ProductPhoto = @imageBytes WHERE ProductArticleNumber = @article";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@imageBytes", ImageFileToByteArray());
                    command.Parameters.AddWithValue("@article", selectedArticle);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void UpdateDataGrid()
        {
            string connectionString = BaseData.StringConnected;
            string query = "SELECT ProductArticleNumber as Артикль, ProductName as Наименование, ProductDescription as Описание, ProductCategory as Категория, ProductPhoto as Фотография, ProductManufacturer as Производитель, ProductCost as Цена, ProductDiscountAmount as Скидка, ProductQuantityInStock as 'Кол-Во' FROM [dbo].[Product]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                dataAdapter.Fill(table);
                dataGridView1.DataSource = table;
            }
            ((DataGridViewImageColumn)dataGridView1.Columns[4]).ImageLayout = DataGridViewImageCellLayout.Zoom;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                Console.WriteLine(row.Cells[8].Value.ToString());
                if (row.Cells[8].Value.ToString() == "0")
                {
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                }
            }

        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }

        private void FormAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!BaseData.firstStart)
            {
                Application.Exit();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    selectedArticle = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    Console.WriteLine(selectedArticle);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Изображение (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                imagePath = openFileDialog1.FileName;
            }
        }

        private byte[] ImageFileToByteArray()
        {
            byte[] imageArray = File.ReadAllBytes(imagePath);
            return imageArray;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertImageToDatabase();
            UpdateDataGrid();
        }
    }
}

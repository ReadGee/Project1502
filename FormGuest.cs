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

namespace v2
{
    public partial class FormGuest : Form
    {
        public FormGuest()
        {
            InitializeComponent();
        }

        private void FormGuest_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            DialogResult dialogResult = form1.ShowDialog();

            if (dialogResult == DialogResult.Yes)
            {
                this.Hide();
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                Console.WriteLine("Cancel");
            }
        }

        private void UpdateDataGrid()
        {
            string connectionString = BaseData.StringConnected;
            string query = "SELECT ProductArticleNumber as Артикль, ProductName as Наименование, ProductDescription as Описание, ProductCategory as Категория, ProductPhoto as Фотография, ProductManufacturer as Производитель, ProductCost as Цена, ProductDiscountAmount as Скидка, ProductQuantityInStock as Кол-Во FROM [dbo].[Product]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                dataAdapter.Fill(table);

                dataGridView1.DataSource = table;
            }
            ((DataGridViewImageColumn)dataGridView1.Columns[4]).ImageLayout = DataGridViewImageCellLayout.Zoom;
        }

        private void FormGuest_Load(object sender, EventArgs e)
        {
            UpdateDataGrid();
        }
    }
}

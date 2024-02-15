using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClassLibrary1;

namespace v2
{
    public partial class FormCapt : Form
    {
        public FormCapt()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = ClassLibrary1.CAPTCHA.CreateImage(pictureBox1.Width, pictureBox1.Height);
            textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = ClassLibrary1.CAPTCHA.CreateImage(pictureBox1.Width, pictureBox1.Height);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (BaseData.sumParse < 3)
            {
                if (ClassLibrary1.CAPTCHA.CheckCapt(textBox1.Text))
                {
                    BaseData.sumParse = 0;
                    DialogResult = DialogResult.Yes;
                    this.Close();
                }
                else
                {
                    BaseData.sumParse++;
                    MessageBox.Show($"Осталось попыток: {3 - BaseData.sumParse} \nДалее проследует блокировка на 10 секунд", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    button1_Click(e, e);
                    textBox1.Text = "";
                }
            }
            else
            {
                BaseData.sumParse = 0;
                DialogResult = DialogResult.No;
                this.Close();                
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

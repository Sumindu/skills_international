using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form10 : Form
    {
        private string memlog;

        public Form10(string memlogin)
        {
            InitializeComponent();
            if(memlogin=="yes")
            {
                memlog = memlogin;
                button2.Hide();
                button5.Hide();
            }
            else if(memlogin=="no")
            {
                memlog = memlogin;
            }
            else
            {
                MessageBox.Show("Null value for memlogin variable");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(memlog=="yes")
            {
                this.Hide();
                Form2 f = new Form2(memlog);
                f.ShowDialog();
                this.Close();
            } 
            else
            {
                memlog = "no";
                this.Hide();
                Form2 f = new Form2(memlog);
                f.ShowDialog();
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (memlog == "yes")
            {
                this.Hide();
                Form5 f = new Form5(memlog);
                f.ShowDialog();
                this.Close();
            }
            else
            {
                memlog = "no";
                this.Hide();
                Form5 f = new Form5(memlog);
                f.ShowDialog();
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (memlog == "yes")
            {
                this.Hide();
                Form4 f = new Form4(memlog);
                f.ShowDialog();
                this.Close();
            }
            else
            {
                memlog = "no";
                this.Hide();
                Form4 f = new Form4(memlog);
                f.ShowDialog();
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (memlog == "yes")
            {
                this.Hide();
                Form3 f = new Form3(memlog);
                f.ShowDialog();
                this.Close();
            }
            else
            {
                memlog = "no";
                this.Hide();
                Form3 f = new Form3(memlog);
                f.ShowDialog();
                this.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (memlog == "yes")
            {
                this.Hide();
                Form6 f = new Form6(memlog);
                f.ShowDialog();
                this.Close();
            }
            else
            {
                memlog = "no";
                this.Hide();
                Form6 f = new Form6(memlog);
                f.ShowDialog();
                this.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (memlog == "yes")
            {
                this.Hide();
                Form7 f = new Form7(memlog);
                f.ShowDialog();
                this.Close();
            }
            else
            {
                memlog = "no";
                this.Hide();
                Form7 f = new Form7(memlog);
                f.ShowDialog();
                this.Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (memlog == "yes")
            {
                this.Hide();
                Form8 f = new Form8(memlog);
                f.ShowDialog();
                this.Close();
            }
            else
            {
                memlog = "no";
                this.Hide();
                Form8 f = new Form8(memlog);
                f.ShowDialog();
                this.Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (memlog == "yes")
            {
                this.Hide();
                Form11 f = new Form11(memlog);
                f.ShowDialog();
                this.Close();
            }
            else
            {
                memlog = "no";
                this.Hide();
                Form11 f = new Form11(memlog);
                f.ShowDialog();
                this.Close();
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            panel1.Show();
            panel2.Hide();
            panel3.Hide();
        }

        private void Form10_MouseHover(object sender, EventArgs e)
        {
            panel1.Hide();
            panel2.Hide();
            panel3.Hide();
        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            panel2.Show();
            panel1.Hide();
            panel3.Hide();
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            panel3.Show();
            panel2.Hide();
            panel1.Hide();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.ShowDialog();
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SMK4S43\SQLEXPRESS;Initial Catalog=Skills_International;Integrated Security=True");

        //this is the variable who can passs the another form
        public string memlogin { get; set; }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                String q = "SELECT * FROM Emp_rg WHERE empid = '" + textBox1.Text + "'AND pswd = '" + textBox2.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(q, con);

                DataTable dtable = new DataTable();
                sda.Fill(dtable);
                if (dtable.Rows.Count > 0)
                {
                    string var1 = "yes";

                    String sq = "SELECT * FROM Emp_rg WHERE empid = '" + textBox1.Text + "'AND type = '" + var1 + "'";
                    SqlDataAdapter ssda = new SqlDataAdapter(sq, con);

                    DataTable ddtable = new DataTable();
                    ssda.Fill(ddtable);

                    if (ddtable.Rows.Count > 0)
                    {
                        //admin home form........
                        memlogin = "no";
                        this.Hide();
                        Form10 f = new Form10(memlogin);
                        f.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        //emp home form..........
                        memlogin = "yes";
                        this.Hide();
                        Form10 f = new Form10(memlogin);
                        f.ShowDialog();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid login credentials,please check Username and Password and try again.", "Invalid Login Details.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox1.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Sql Connection Error","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox1.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.ActiveControl = textBox2;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.ActiveControl = button2;
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            this.ActiveControl = textBox1;
        }
    }
}

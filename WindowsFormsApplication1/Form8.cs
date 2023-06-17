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
    public partial class Form8 : Form
    {
        private string memlogin;
        public Form8(string memlog)
        {
            memlogin = memlog;
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SMK4S43\SQLEXPRESS;Initial Catalog=Skills_International;Integrated Security=True");

        private void populatempid()
        {
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter("select empid from Emp_rg", con);
                adapter.Fill(dt);
                con.Close();
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "empid";
                comboBox1.ValueMember = "empid";
            }
            catch (Exception)
            {
                MessageBox.Show("Combobox error");
            }
            finally
            {
                con.Close();
            }
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            populatempid();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            String user, password;

            try
            {
                //checking current pswd.....
                String q = "SELECT * FROM Emp_rg WHERE empid = '" + comboBox1.Text + "'AND pswd = '" + textBox1.Text + "'";
                SqlDataAdapter sda = new SqlDataAdapter(q, con);

                DataTable dtable = new DataTable();
                sda.Fill(dtable);
                if (dtable.Rows.Count > 0)
                {
                    user = comboBox1.Text;
                    password = textBox1.Text;

                    //checking new pswd1 and pswd2 are eaqual........
                    string ps1 = textBox2.Text;
                    string ps2 = textBox3.Text;
                    if (ps1 == ps2)
                    {
                        //update new pswd and username...........
                        string commandstring = @"UPDATE [dbo].[Emp_rg] SET[empid] = @empid , [pswd] = @pswd WHERE empid = @empid";

                        SqlCommand cmd = new SqlCommand(commandstring, con);

                        cmd.Parameters.AddWithValue("@empid", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@pswd", textBox3.Text);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Password Updated Successfully", "UPDATE", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        
                    }
                    else
                    {
                        MessageBox.Show("deka galpenne na ");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Current Password", "Error!!!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
            }
            catch
            {
                MessageBox.Show("errrrrrrrrrr");
            }
         
            finally
            {
                con.Close();
            }
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
                this.ActiveControl = textBox3;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.ActiveControl = button1;
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.ActiveControl = textBox1;
            }
        }

        private void Form8_Activated(object sender, EventArgs e)
        {
            this.ActiveControl = comboBox1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form10 f = new Form10(memlogin);
            f.ShowDialog();
            this.Close();
        }
    }
}

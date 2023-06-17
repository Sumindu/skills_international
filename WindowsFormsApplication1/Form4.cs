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
    public partial class Form4 : Form
    {
        private string memlogin;

        public Form4(string memlog)
        {
            memlogin = memlog;
            InitializeComponent();
            
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SMK4S43\SQLEXPRESS;Initial Catalog=Skills_International;Integrated Security=True");

        private void populatestregnum()
        {
            try
            {
                con.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter("select regnum from St_reg", con);
                adapter.Fill(dt);
                con.Close();
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "regnum";
                comboBox1.ValueMember = "regnum";
                comboBox1.Text = "";
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

        private void populatecontrols(int regnum)
        {
            DataTable dt = new DataTable();

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * From St_reg where regnum=@regnum", con);
            cmd.Parameters.AddWithValue("@regnum", regnum);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            con.Close();

            string fname = dt.Rows[0]["f_nm"].ToString();
            string lname = dt.Rows[0]["l_nm"].ToString();
            textBox3.Text = fname + " " + lname;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            populatestregnum();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stcted = comboBox1.SelectedValue.ToString();
            int regnm;
            if (int.TryParse(stcted, out regnm))
            {
                regnm = Convert.ToInt32(stcted);
                populatecontrols(regnm);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Diploma in IT
            //Diploma In English
            //HND in Computing
            //HND in Manegement
            if (comboBox3.Text == "Diploma in IT")
                textBox2.Text = "60,000 LKR";
            else if (comboBox3.Text == "Diploma in English")
                textBox2.Text = "45,000 LKR";
            else if (comboBox3.Text == "HND in Computing")
                textBox2.Text = "500,000 LKR";
            else if (comboBox3.Text == "HND in Manegement")
                textBox2.Text = "450,000 LKR";
            else
                textBox2.Text = "15,000 LKR";
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string cmdstring = @"UPDATE[dbo].[St_reg]
   SET[regnum]=@regnum
      ,[course] = @course
      ,[price] = @price
      ,[pm] = @pm
  Where regnum=@regnum";

            SqlCommand cmd = new SqlCommand(cmdstring, con);

            cmd.Parameters.AddWithValue("@regnum",comboBox1.Text);
            cmd.Parameters.AddWithValue("@course", comboBox3.Text);
            cmd.Parameters.AddWithValue("@price", textBox2.Text);
            cmd.Parameters.AddWithValue("@pm", comboBox2.Text);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Course Added Succesfully", "Update Student Course", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.clear();


        }
        public void clear()
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form10 f = new Form10(memlogin);
            f.ShowDialog();
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.clear();
        }
    }
}


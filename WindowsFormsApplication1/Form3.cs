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
    public partial class Form3 : Form
    {
        private string memlogin;

        public Form3(string memlog)
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
            textBox1.Text = fname +" "+ lname;
            label3.Text = dt.Rows[0]["email"].ToString();
            label4.Text = dt.Rows[0]["m_ph"].ToString();
            label5.Text = dt.Rows[0]["course"].ToString();
            label6.Text = dt.Rows[0]["pm"].ToString();
        }

            private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form10 f = new Form10(memlogin);
            f.ShowDialog();
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            populatestregnum();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label3.Show();
            label4.Show();
            label5.Show();
            label6.Show();
            string stcted = comboBox1.SelectedValue.ToString();
            int regnm;
            if (int.TryParse(stcted, out regnm))
            {
                regnm = Convert.ToInt32(stcted);
                populatecontrols(regnm);
            }
        }
    }
}

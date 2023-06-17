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
    public partial class Form5 : Form
    {
        private string memlogin;

        public Form5(string memlog)
        {
            memlogin = memlog;
            InitializeComponent();

            if (radioButton1.Checked == true)
            {
                radioButton2.Checked = false;
            }
            else if (radioButton2.Checked == true)
            {
                radioButton1.Checked = false;
            }
            if (radioButton3.Checked == true)
            {
                radioButton4.Checked = false;
            }
            else if (radioButton4.Checked == true)
            {
                radioButton3.Checked = false;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form10 f = new Form10(memlogin);
            f.ShowDialog();
            this.Close();
        }

        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-SMK4S43\SQLEXPRESS;Initial Catalog=Skills_International;Integrated Security=True");

        private void populateempid()
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

        private void populatecontrols(int empid)
        {
            DataTable dt = new DataTable();

            con.Open();
            SqlCommand cmd = new SqlCommand("Select * From Emp_rg where empid=@empid", con);
            cmd.Parameters.AddWithValue("@empid", empid);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            con.Close();

            textBox2.Text = dt.Rows[0]["f_nm"].ToString();
            textBox3.Text = dt.Rows[0]["l_nm"].ToString();
            dateTimePicker1.Text = dt.Rows[0]["dob"].ToString();
            textBox4.Text = dt.Rows[0]["address"].ToString();
            textBox5.Text = dt.Rows[0]["email"].ToString();
            textBox6.Text = dt.Rows[0]["m_ph"].ToString();
            textBox9.Text = dt.Rows[0]["pswd"].ToString();
            textBox10.Text = dt.Rows[0]["pswd"].ToString();
            string gen = dt.Rows[0]["gnd"].ToString();

            if (gen == "Male")
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
            else if (gen =="Female")
            {
                radioButton2.Checked = true;
                radioButton1.Checked = false;
            }

            string typ = dt.Rows[0]["type"].ToString();

            if (typ == "yes")
            {
                radioButton3.Checked = true;
                radioButton4.Checked = false;
            }
            else if (typ=="no")
            {
                radioButton4.Checked = true;
                radioButton3.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string cmdString = @"INSERT INTO [dbo].[Emp_rg]
           ([empid]
           ,[f_nm]
           ,[l_nm]
           ,[dob]
           ,[gnd]
           ,[address]
           ,[email]
           ,[m_ph]
           ,[type]
           ,[pswd])
     VALUES
           (@empid,@f_nm,@l_nm,@dob,@gnd,@address,@email,@m_ph,@type,@pswd)";

                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.Parameters.AddWithValue("@empid", comboBox1.Text);
                cmd.Parameters.AddWithValue("@f_nm", textBox2.Text);
                cmd.Parameters.AddWithValue("@l_nm", textBox3.Text);
                cmd.Parameters.AddWithValue("@dob", Convert.ToDateTime(dateTimePicker1.Text));
                cmd.Parameters.AddWithValue("@address", textBox4.Text);
                cmd.Parameters.AddWithValue("@email", textBox5.Text);
                cmd.Parameters.AddWithValue("@m_ph", textBox6.Text);

                string gnd = "Unkown";
                if (radioButton1.Checked == true && radioButton2.Checked == false)
                {
                    gnd = "Male";
                }
                else if (radioButton2.Checked == true && radioButton1.Checked == false)
                {
                    gnd = "Female";
                }
                cmd.Parameters.AddWithValue("@gnd", gnd);

                string typ = "Unkown";
                if (radioButton3.Checked == true && radioButton4.Checked == false)
                {
                    typ = "yes";
                }
                else if (radioButton4.Checked == true && radioButton3.Checked == false)
                {
                    typ = "no";
                }
                cmd.Parameters.AddWithValue("@type", typ);

                string ps1 = textBox9.Text;
                string ps2 = textBox10.Text;
                if (ps1 == ps2)
                {
                    cmd.Parameters.AddWithValue("@pswd", textBox9.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Record Added Succesfully.", "Register Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.clear();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Password isn't equal, please enter the same password !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Fucked up");
            }
            finally
            {
                con.Close();
            }
        }

        public void clear()
        {
            comboBox1.Text = "";
            textBox2.Clear();
            textBox3.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox9.Clear();
            textBox10.Clear();
            dateTimePicker1.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.ShowDialog();
            this.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            populateempid();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stcted = comboBox1.SelectedValue.ToString();
            int empid;
            if (int.TryParse(stcted, out empid))
            {
                empid = Convert.ToInt32(stcted);
                populatecontrols(empid);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string cmdstring = @"UPDATE[dbo].[Emp_rg]
   SET[empid] = @empid
      ,[f_nm] = @f_nm
      ,[l_nm] = @l_nm
      ,[dob] = @dob
      ,[gnd] = @gnd
      ,[address] = @address
      ,[email] = @email
      ,[m_ph] = @m_ph
      ,[type] = @type
      ,[pswd] = @pswd
  WHERE empid=@empid";

            SqlCommand cmd = new SqlCommand(cmdstring, con);

            cmd.Parameters.AddWithValue("@empid", comboBox1.Text);
            cmd.Parameters.AddWithValue("@f_nm", textBox2.Text);
            cmd.Parameters.AddWithValue("@l_nm", textBox3.Text);
            cmd.Parameters.AddWithValue("@dob", Convert.ToDateTime(dateTimePicker1.Text));
            cmd.Parameters.AddWithValue("@address", textBox4.Text);
            cmd.Parameters.AddWithValue("@email", textBox5.Text);
            cmd.Parameters.AddWithValue("@m_ph", textBox6.Text);

            string gnd = "Unkown";
            if (radioButton1.Checked == true && radioButton2.Checked == false)
            {
                gnd = "Male";
            }
            else if (radioButton2.Checked == true && radioButton1.Checked == false)
            {
                gnd = "Female";
            }
            cmd.Parameters.AddWithValue("@gnd", gnd);

            string typ = "Unkown";
            if (radioButton3.Checked == true && radioButton4.Checked == false)
            {
                typ = "yes";
            }
            else if (radioButton4.Checked == true && radioButton3.Checked == false)
            {
                typ = "no";
            }
            cmd.Parameters.AddWithValue("@type", typ);

            string ps1 = textBox9.Text;
            string ps2 = textBox10.Text;
            if (ps1 == ps2)
            {
                cmd.Parameters.AddWithValue("@pswd", textBox9.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated Succesfully.", "UPDATE Employee", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.clear();
                con.Close();
            }
            else
            {
                MessageBox.Show("Password isn't equal, please enter the same password !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are You sure, Do you really want to Delete this record..?", "DELETE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                string cmdstring = @"DELETE FROM Emp_rg Where empid=@empid";
                SqlCommand cmd = new SqlCommand(cmdstring, con);

                cmd.Parameters.AddWithValue(@"empid", (comboBox1.Text));
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted", "DELETE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
                this.clear();
            }
            else
            {
                this.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.clear();
        }
    }
}

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
    public partial class Form2 : Form
    {
        private string memlogin;

        public Form2(string memlog)
        {
            InitializeComponent();
            
            
            memlogin = memlog;
            if(radioButton1.Checked==true)
            {
                radioButton2.Checked = false;
            }
            else if(radioButton2.Checked==true)
            {
                radioButton1.Checked = false;
            }
        }
        public void clear()
        {
            comboBox1.Text="";
            textBox2.Clear();
            textBox3.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            dateTimePicker1.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
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

            textBox2.Text = dt.Rows[0]["f_nm"].ToString();
            textBox3.Text = dt.Rows[0]["l_nm"].ToString();
            dateTimePicker1.Text = dt.Rows[0]["dob"].ToString();
            textBox4.Text = dt.Rows[0]["address"].ToString();
            textBox5.Text = dt.Rows[0]["email"].ToString();
            textBox6.Text = dt.Rows[0]["m_ph"].ToString();
            textBox7.Text = dt.Rows[0]["h_ph"].ToString();
            textBox8.Text = dt.Rows[0]["p_nm"].ToString();
            textBox9.Text = dt.Rows[0]["nic"].ToString();
            textBox10.Text = dt.Rows[0]["c_ph"].ToString();
            string gen = dt.Rows[0]["gnd"].ToString();

            if (gen == "Male")
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
            else
            {
                radioButton2.Checked = true;
                radioButton1.Checked = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form1 f = new Form1();
            f.ShowDialog();
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string cmdString = @"INSERT INTO [dbo].[St_reg]
           ([regnum]
           ,[f_nm]
           ,[l_nm]
           ,[dob]
           ,[gnd]
           ,[address]
           ,[email]
           ,[m_ph]
           ,[h_ph]
           ,[p_nm]
           ,[nic]
           ,[c_ph])
            VALUES
           (@regnum,@f_nm,@l_nm,@dob,@gnd,@address,@email,@m_ph,@h_ph,@p_nm,@nic,@c_ph)";

                SqlCommand cmd = new SqlCommand(cmdString, con);

                cmd.Parameters.AddWithValue("@regnum", comboBox1.Text);
                cmd.Parameters.AddWithValue("@f_nm", textBox2.Text);
                cmd.Parameters.AddWithValue("@l_nm", textBox3.Text);
                cmd.Parameters.AddWithValue("@dob", Convert.ToDateTime(dateTimePicker1.Text));
                cmd.Parameters.AddWithValue("@address", textBox4.Text);
                cmd.Parameters.AddWithValue("@email", textBox5.Text);
                cmd.Parameters.AddWithValue("@m_ph", textBox6.Text);
                cmd.Parameters.AddWithValue("@h_ph", textBox7.Text);
                cmd.Parameters.AddWithValue("@p_nm", textBox8.Text);
                cmd.Parameters.AddWithValue("@nic", textBox9.Text);
                cmd.Parameters.AddWithValue("@c_ph", textBox10.Text);

                string gnd = "Unkown";
                if (radioButton1.Checked == true)
                {
                    gnd = "Male";
                }
                else if (radioButton2.Checked == true)
                {
                    gnd = "Female";
                }
                cmd.Parameters.AddWithValue("@gnd", gnd);

                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Added Succesfully.", "Register Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.clear();

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

        private void Form2_Load(object sender, EventArgs e)
        {
            //int regno = Convert.ToInt32(comboBox1.SelectedValue);
            //populatecontrols(regno);
            populatestregnum();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
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
                this.ActiveControl = dateTimePicker1;
            }
        }

        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.ActiveControl = radioButton1;
            }
        }

        private void radioButton1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.ActiveControl = textBox4;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.ActiveControl = textBox5;
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.ActiveControl = textBox6;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.ActiveControl = textBox7;
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.ActiveControl = textBox8;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.ActiveControl = textBox9;
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.ActiveControl = textBox10;
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.ActiveControl = button1;
            }
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 37)
            {
                this.ActiveControl = button2;
            }
            else if (e.KeyChar == 39)
            {
                this.ActiveControl = linkLabel1;
            }
        }

        private void button2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 37)
            {
                this.ActiveControl = button3;
            }
            else if (e.KeyChar == 39)
            {
                this.ActiveControl = button1;
            }
        }

        private void button3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 37)
            {
                this.ActiveControl = button4;
            }
            else if (e.KeyChar == 39)
            {
                this.ActiveControl = button3;
            }
        }

        private void button4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 37)
            {
                this.ActiveControl = button3;
            }
            else if (e.KeyChar == 39)
            {
                this.ActiveControl = linkLabel2;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string stcted = comboBox1.SelectedValue.ToString();
            int regnm;
            if(int.TryParse(stcted,out regnm))
            {
                regnm = Convert.ToInt32(stcted);
                populatecontrols(regnm);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.clear();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Form10 f = new Form10(memlogin);
            f.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are You sure, Do you really want to Delete this record..?","DELETE",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (res == DialogResult.Yes)
            {
                string cmdstring = @"DELETE FROM St_reg Where regnum=@regnum";
                SqlCommand cmd = new SqlCommand(cmdstring, con);

                cmd.Parameters.AddWithValue(@"regnum", (comboBox1.Text));
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

        private void button2_Click(object sender, EventArgs e)
        {
            string cmdstring = @"UPDATE[dbo].[St_reg]
   SET[regnum] = @regnum
      ,[f_nm] = @f_nm
      ,[l_nm] = @l_nm
      ,[dob] = @dob
      ,[gnd] = @gnd
      ,[address] = @address
      ,[email] = @email
      ,[m_ph] = @m_ph
      ,[h_ph] = @h_ph
      ,[p_nm] = @p_nm
      ,[nic] = @nic
      ,[c_ph] = @c_ph
  Where regnum=@regnum";

            SqlCommand cmd = new SqlCommand(cmdstring,con);

            cmd.Parameters.AddWithValue("@regnum", comboBox1.Text);
            cmd.Parameters.AddWithValue("@f_nm", textBox2.Text);
            cmd.Parameters.AddWithValue("@l_nm", textBox3.Text);
            cmd.Parameters.AddWithValue("@dob", Convert.ToDateTime(dateTimePicker1.Text));
            cmd.Parameters.AddWithValue("@address", textBox4.Text);
            cmd.Parameters.AddWithValue("@email", textBox5.Text);
            cmd.Parameters.AddWithValue("@m_ph", textBox6.Text);
            cmd.Parameters.AddWithValue("@h_ph", textBox7.Text);
            cmd.Parameters.AddWithValue("@p_nm", textBox8.Text);
            cmd.Parameters.AddWithValue("@nic", textBox9.Text);
            cmd.Parameters.AddWithValue("@c_ph", textBox10.Text);

            string gnd = "Unkown";
            if (radioButton1.Checked == true)
            {
                gnd = "Male";
            }
            else if (radioButton2.Checked == true)
            {
                gnd = "Female";
            }
            cmd.Parameters.AddWithValue("@gnd", gnd);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Record Updated Succesfully", "Update Student", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.clear();

        }
    }
}

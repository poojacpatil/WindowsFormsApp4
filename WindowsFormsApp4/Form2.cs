using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp4
{
    //connection configuration
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form2()
        {
            InitializeComponent();
            con = new SqlConnection(@"Server=DESKTOP-AFRADQ0\SQLEXPRESS;database=TQ;integrated Security=True");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // while writing query follow the col sequence
                string qry = "insert into Students values(@id,@name,@branch,@percentage)";
                // this configuration is to assign query & connection details to commad
                // so that qry will be executed on the given connection
                cmd = new SqlCommand(qry, con);
                // assign values to the parameter
                // no need to follow the sequence
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@branch", txtBranch.Text);
                cmd.Parameters.AddWithValue("@percentage", Convert.ToSingle(txtPercentage.Text));
                // open DB connection
                con.Open();
                // fire the query
                int res = cmd.ExecuteNonQuery();
                if (res == 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Students where Id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtName.Text = dr["Name"].ToString(); //["Name"]should match col name
                        txtBranch.Text = dr["Branch"].ToString();
                        txtPercentage.Text = dr["Percentage"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnDelet_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "delete from Students where Id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@branch", txtBranch.Text);
                cmd.Parameters.AddWithValue("@percentage", Convert.ToInt32(txtPercentage.Text));
                con.Open();
                int res = cmd.ExecuteNonQuery();
                if (res == 1)
                {
                    MessageBox.Show("Record delet");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }


        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select max(Roll No) from Students";
                cmd = new SqlCommand(qry, con);
                con.Open();
                object obj = cmd.ExecuteScalar();
                if (obj == DBNull.Value)//when obj is null or obj does not have value
                {
                    txtId.Text = "4";
                }
                else
                {
                    int RollNo = Convert.ToInt32(obj);
                    RollNo++;
                    txtId.Text = RollNo.ToString();
                }
                txtId.Enabled = false;
                txtName.Clear();
                txtBranch.Clear();
                txtPercentage.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

       
    }
}
    

    


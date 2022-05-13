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
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
            con=new SqlConnection(@"Server=DESKTOP-AFRADQ0\SQLEXPRESS;database=TQ;integrated Security=True");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // while writing query follow the col sequence
                string qry = "insert into Employeee values(@id,@name,@designation,@salary)";
                // this configuration is to assign query & connection details to commad
                // so that qry will be executed on the given connection
                cmd = new SqlCommand(qry, con);
                // assign values to the parameter
                // no need to follow the sequence
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@designation", txtDesignation.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToInt32(txtSalary.Text));
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
                string qry = "select * from Employeee where Id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtId.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtName.Text = dr["Name"].ToString(); //["Name"]should match col name
                        txtDesignation.Text = dr["Designation"].ToString();
                        txtSalary.Text = dr["Salary"].ToString();
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
                string qry = "delete from Employeee where Id=@id";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id",Convert.ToInt32(txtId.Text));
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@designation",txtDesignation.Text);
                cmd.Parameters.AddWithValue("@salary",Convert.ToInt32(txtSalary.Text));
                con.Open();
                int res=cmd.ExecuteNonQuery();
                if(res==1)
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
                string qry = "select max(Id) from Employeee";
                cmd = new SqlCommand(qry, con);
                con.Open();
                object obj = cmd.ExecuteScalar();
                if (obj == DBNull.Value)//when obj is null or obj does not have value
                {
                    txtId.Text = "2";
                }
                else
                {
                    int id = Convert.ToInt32(obj);
                    id++;
                    txtId.Text = id.ToString();
                }
                txtId.Enabled = false;
                txtName.Clear();
                txtDesignation.Clear();
                txtSalary.Clear();
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select max(Id) from Employeee";
                cmd = new SqlCommand(qry, con);
                con.Open();
                object obj = cmd.ExecuteScalar();
                if (obj == DBNull.Value)//when obj is null or obj does not have value
                {
                    txtId.Text = "2";
                }
                else
                {
                    int id = Convert.ToInt32(obj);
                    id++;
                    txtId.Text = id.ToString();
                }
                txtId.Enabled = false;
                txtName.Clear();
                txtDesignation.Clear();
                txtSalary.Clear();
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

        private void btnShowAllEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Employeee";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    DataTable table = new DataTable();
                    table.Load(dr);
                    dataGridView1.DataSource = table;
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            txtId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtDesignation.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtSalary.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }
    }
    
}



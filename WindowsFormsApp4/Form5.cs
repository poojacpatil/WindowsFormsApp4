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
using System.Configuration;

namespace WindowsFormsApp4
{
    //Disconnection configuration
    public partial class Form5 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public Form5()
        {
            InitializeComponent();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }
        public DataSet GetStuds()
        {
            da = new SqlDataAdapter("select * from Stud", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "stud");
            return ds;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            ds = GetStuds();
            DataRow row = ds.Tables["stud"].NewRow();
            row["Name"] = txtName.Text;
            row["Branch"] = txtBranch.Text;
            row["Percentage"] = txtPercentage.Text;
            ds.Tables["stud"].Rows.Add(row);
            int res = da.Update(ds.Tables["stud"]);
            if (res == 1)
                MessageBox.Show("Record saved");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ds = GetStuds();
            DataRow row = ds.Tables["stud"].Rows.Find(Convert.ToInt32(txtId.Text));
            if (row != null)
            {
                row.Delete();
                int res = da.Update(ds.Tables["stud"]);
                if (res == 1)
                    MessageBox.Show("record deleted");
                else
                    MessageBox.Show("Not able to delete");
            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            ds = GetStuds();
            DataRow row = ds.Tables["stud"].Rows.Find(Convert.ToInt32(txtId.Text));
            if (row != null)
            {

                txtName.Text = row["Name"].ToString();
                txtBranch.Text = row["Branch"].ToString();
                txtPercentage.Text = row["Percentage"].ToString();
                int res = da.Update(ds.Tables["stud"]);
                if (res == 1)
                    MessageBox.Show("record updated");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ds = GetStuds();
            // Find() method only work with PK col in the dataset
            DataRow row = ds.Tables["stud"].Rows.Find(Convert.ToInt32(txtId.Text));
            if (row != null)
            {
                txtName.Text = row["Name"].ToString();
                txtBranch.Text = row["Branch"].ToString();
                txtPercentage.Text = row["Percentage"].ToString();
            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }
    }
}


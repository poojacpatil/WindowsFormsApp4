using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using WindowsFormsApp4.DAL;

namespace WindowsFormsApp4
{
    //connection configuration(DAL,Model)
    public partial class Form3 : Form
    {
        EmpDal empdal = new EmpDal();
        public Form3()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Emp emp = new Emp();
            emp.Name = txtName.Text;
            emp.Salary = Convert.ToDouble(txtSalary.Text);
            emp.DeptName = txtDeptName.Text;
            int res = empdal.Save(emp);
            if (res == 1)
                MessageBox.Show("Inserted the record");
        }

        private void btnDelet_Click(object sender, EventArgs e)
        {
            int res = empdal.Delete(Convert.ToInt32(txtId.Text));
            if (res == 1)
                MessageBox.Show("deleted the record");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Emp emp = empdal.GetEmpById(Convert.ToInt32(txtId.Text));
            if (emp.Id > 0)
            {
                txtName.Text = emp.Name;
                txtSalary.Text = emp.Salary.ToString();
                txtDeptName.Text = emp.DeptName;
            }
            else
            {
                MessageBox.Show("Record not found");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Emp emp = new Emp();
            emp.Id = Convert.ToInt32(txtId.Text);
            emp.Name = txtName.Text;
            emp.Salary = Convert.ToDouble(txtSalary.Text);
            emp.DeptName= txtDeptName.Text;
            int res = empdal.Upate(emp);
            if (res == 1)
                MessageBox.Show("updated the record");
        }

        private void lblId_Click(object sender, EventArgs e)
        {

        }
    }
}

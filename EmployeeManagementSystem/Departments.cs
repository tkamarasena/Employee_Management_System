using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class Departments : Form
    {
        Functions Con;

        public Departments()
        {
            InitializeComponent();
            Con = new Functions();
            ShowDepartments();

        }

        public void ShowDepartments()
        {
            string Query = "SELECT * FROM DepartmentTbl";
            dgvDepartmentList.DataSource = Con.GetData(Query);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDepartment.Text=="")
                {
                    MessageBox.Show("Missing Data!!");
                }
                else
                {
                    string Dep=txtDepartment.Text;
                    string Query = "INSERT INTO DepartmentTbl VALUES('{0}')";
                    Query=string.Format(Query,txtDepartment.Text);
                    Con.SetData(Query);
                    ShowDepartments();
                    MessageBox.Show("Department Is Added!!");
                    txtDepartment.Text = "";
                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDepartment.Text == "")
                {
                    MessageBox.Show("Missing Data!!");
                }
                else
                {
                    string Dep = txtDepartment.Text;
                    string Query = "UPDATE DepartmentTbl SET  DepName = '{0}' WHERE DepId = {1}";
                    Query = string.Format(Query, txtDepartment.Text, key);
                    Con.SetData(Query);
                    ShowDepartments();
                    MessageBox.Show("Department Is Updated!!");
                    txtDepartment.Text = "";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        int key = 0;


       /* private void dgvDepartmentList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDepartment.Text = dgvDepartmentList.SelectedRows[0].Cells[1].Value.ToString();
            if (txtDepartment.Text == "")
            {
                key = 0;

            }
            else
            {
                key = Convert.ToInt32(dgvDepartmentList.SelectedRows[0].Cells[0].Value.ToString());

            }

        }*/

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDepartment.Text == "")
                {
                    MessageBox.Show("Missing Data!!");
                }
                else
                {
                    string Dep = txtDepartment.Text;
                    string Query = "DELETE FROM DepartmentTbl WHERE DepId = {0}";
                    Query = string.Format(Query, key);
                    Con.SetData(Query);
                    ShowDepartments();
                    MessageBox.Show("Department Is Deleted!!");
                    txtDepartment.Text = "";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lblEmployee_Click(object sender, EventArgs e)
        {
            Employees obj = new Employees();
            obj.Show();
            this.Hide();
        }

        private void lblDepartments_Click(object sender, EventArgs e)
        {
            Departments obj= new Departments();
            obj.Show();
            this.Hide();
        }

        private void lblSalary_Click(object sender, EventArgs e)
        {
            Salaries obj = new Salaries();
            obj.Show();
            this.Hide();
        }

        private void dgvDepartmentList_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //============================================

            txtDepartment.Text = dgvDepartmentList.SelectedRows[0].Cells[1].Value.ToString();
            if (txtDepartment.Text == "")
            {
                key = 0;

            }
            else
            {
                key = Convert.ToInt32(dgvDepartmentList.SelectedRows[0].Cells[0].Value.ToString());

            }

        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
    }
}

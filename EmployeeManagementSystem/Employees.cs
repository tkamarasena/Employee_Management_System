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
    public partial class Employees : Form
    {
        Functions Con;
        public Employees()
        {
            InitializeComponent();
            Con = new Functions();
            ShowEmployees();
            GetDepartments();

        }

        public void ShowEmployees()
        {
            try
            {
                txtEmpName.Focus();
                string Query = "SELECT * FROM EmpTbl";
                dgvEmployeeDetails.DataSource = Con.GetData(Query);
            }
            catch {

                throw;
            }
            
        }

        private void GetDepartments()
        {
            string Query = "SELECT * FROM DepartmentTbl";
            cmbDepartment.DisplayMember = Con.GetData(Query).Columns["DepName"].ToString();
            cmbDepartment.ValueMember = Con.GetData(Query).Columns["DepId"].ToString();
            cmbDepartment.DataSource = Con.GetData(Query);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmpName.Text == "" || txtEmail.Text == "" || cmbGender.SelectedIndex == -1 || cmbDepartment.SelectedIndex == -1 
                    || txtDailySalary.Text == "")
                {
                    MessageBox.Show("Missing Data!!");
                }
                else
                {
                    string name = txtEmpName.Text;                       
                    string gender = cmbGender.SelectedItem.ToString();
                    string email = txtEmail.Text;
                    int department = Convert.ToInt32(cmbDepartment.SelectedValue.ToString());
                    string dob = dateTpDob.Value.ToString();
                    string joinDate = dateTpJoinDate.Value.ToString();
                    int salary = Convert.ToInt32(txtDailySalary.Text);

                    string Query = "INSERT INTO  EmpTbl VALUES('{0}','{1}','{2}',{3},'{4}','{5}',{6})";
                    Query = string.Format(Query, name,email,gender,department,dob,joinDate,salary);
                    Con.SetData(Query);
                    ShowEmployees();
                    MessageBox.Show("Eployee Is Added!!");
                    txtEmpName.Text = "";
                    cmbGender.SelectedIndex = -1;
                    txtEmail.Text = "";
                    cmbDepartment.SelectedIndex = -1;
                    txtDailySalary.Text = "";
                    txtEmpName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Employees obj=new Employees();
            obj.Show();
            this.Hide();
        }

        private void dgvEmployeeDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)  //Delete employees
        {
            try
            {
                if (key == 0)
                {
                    MessageBox.Show("Missing Data!!");
                }
                else
                {
                    string Query = "DELETE FROM EmpTbl WHERE EmpId = {0}";
                    Query = string.Format(Query, key);
                    Con.SetData(Query);
                    ShowEmployees();
                    MessageBox.Show("Eployee Is Deleted!!");
                    txtEmpName.Text = "";
                    cmbGender.SelectedIndex = -1;
                    cmbDepartment.SelectedIndex = -1;
                    txtDailySalary.Text = "";
                    txtEmpName.Focus();
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
                if (txtEmpName.Text == "" || cmbGender.SelectedIndex == -1 || cmbDepartment.SelectedIndex == -1
                    || txtDailySalary.Text == "")
                {
                    MessageBox.Show("Missing Data!!");
                }
                else
                {
                    string name = txtEmpName.Text;
                    string gender = cmbGender.SelectedItem.ToString();
                    int department = Convert.ToInt32(cmbDepartment.SelectedValue.ToString());
                    string dob = dateTpDob.Value.ToString();
                    string joinDate = dateTpJoinDate.Value.ToString();
                    int salary = Convert.ToInt32(txtDailySalary.Text);

                    string Query = "UPDATE EmpTbl SET EmpName='{0}', EmpGen='{1}', EmpDep={2}, EmpDOB='{3}', EmpDate='{4}', EmpSal={5} WHERE EmpId={6}";
                    Query = string.Format(Query, name, gender, department, dob, joinDate, salary,key);
                    Con.SetData(Query);
                    ShowEmployees();
                    MessageBox.Show("Eployee Details Are Updated!!");
                    txtEmpName.Text = "";
                    cmbGender.SelectedIndex = -1;
                    cmbDepartment.SelectedIndex = -1;
                    txtDailySalary.Text = "";
                    txtEmpName.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        int key = 0;
        private void dgvEmployeeDetails_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtEmpName.Text = dgvEmployeeDetails.SelectedRows[0].Cells[1].Value.ToString();
            cmbGender.Text = dgvEmployeeDetails.SelectedRows[0].Cells[2].Value.ToString();
            cmbDepartment.SelectedValue = dgvEmployeeDetails.SelectedRows[0].Cells[3].Value.ToString();
            dateTpDob.Text = dgvEmployeeDetails.SelectedRows[0].Cells[4].Value.ToString();
            dateTpJoinDate.Text = dgvEmployeeDetails.SelectedRows[0].Cells[5].Value.ToString();
            txtDailySalary.Text = dgvEmployeeDetails.SelectedRows[0].Cells[6].Value.ToString();
            

            if (txtEmpName.Text == "")
            {
                key = 0;

            }
            else
            {
                key = Convert.ToInt32(dgvEmployeeDetails.SelectedRows[0].Cells[0].Value.ToString());

            }
        }

        private void lblDepartments_Click(object sender, EventArgs e)
        {
            Departments obj=new Departments();
            obj.Show();
            this.Hide();
        }

        private void lblSalary_Click(object sender, EventArgs e)
        {
            Salaries obj=new Salaries();
            obj.Show(); 
            this.Hide();
        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

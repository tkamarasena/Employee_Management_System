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
    public partial class SalaryUpdate : Form
    {
        Functions Con;
        public SalaryUpdate()
        {
            InitializeComponent();
            Con = new Functions();
            ShowSalaries();
            GetEmployees();
        }

        private void ShowSalaries()
        {
            try
            {
                string Query = "SELECT * FROM SalaryTbl";
                dgvUpdateSalary.DataSource = Con.GetData(Query);
            }
            catch
            {

                throw;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void GetEmployees()
        {
            string Query = "SELECT * FROM EmpTbl";
            txtEmpName.SelectedText = Con.GetData(Query).Columns["EmpName"].ToString();
            txtAttendentDays.SelectedText = Con.GetData(Query).Columns["EmpId"].ToString();
            
        }


        private void GetDate()
        {
            string Query = "SELECT * FROM SalaryTbl";
            dateTpPeriod.Format = DateTimePickerFormat.Custom;
            dateTpPeriod.CustomFormat = "mm/dd/yyyy";
        }

    }
}

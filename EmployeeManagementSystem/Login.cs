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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            
        }

        private void lblReset_Click(object sender, EventArgs e)
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            /*allow user to enter three times theire user name and password
             using loops*/

            if (txtUserName.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Missing Data");
            }
            else if(txtUserName.Text == "1234" && txtPassword.Text == "1234")
            {
                Employees obj= new Employees(); 
                obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Incorrect UserName Or Password!!");
                txtUserName.Text = "";
                txtPassword.Text = "";
                txtUserName.Focus();
            }
        }
    }
}

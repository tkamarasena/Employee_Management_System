using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EmployeeManagementSystem
{
    public partial class Salaries : Form
    {
        NetworkCredential login; //related to email
        SmtpClient client;
        MailMessage message;//***

        Functions Con;
        public Salaries()
        {
            InitializeComponent();
            Con = new Functions();
            ShowSalaries();
            GetEmployees();
        }

        public void ShowSalaries()
        {
            try
            {
                string Query = "SELECT * FROM SalaryTbl";
                dgvSalary.DataSource = Con.GetData(Query);
            }
            catch
            {

                throw;
            }
        }


        private void GetEmployees()
        {
            string Query = "SELECT * FROM EmpTbl";
            cmbEmployee.DisplayMember = Con.GetData(Query).Columns["EmpName"].ToString();
            cmbEmployee.ValueMember = Con.GetData(Query).Columns["EmpId"].ToString();
            cmbEmployee.DataSource = Con.GetData(Query);
        }


        int dSal = 0;
        string period = "";

        private void GetSalary()
        {
            string Query = "SELECT * FROM EmpTbl WHERE EmpId={0}";
            Query=string.Format(Query, cmbEmployee.SelectedValue.ToString());

           foreach (DataRow dr in Con.GetData(Query).Rows)
            {
                dSal = Convert.ToInt32(dr["EmpSal"].ToString());
            }
            //MessageBox.Show("" + dSal);
            //cmbEmployee.DataSource = Con.GetData(Query);

            if (txtAttendentDays.Text == "")
            {
                txtAmount.Text = "Rs. " + (d * dSal);
            }
            else if (Convert.ToInt32(txtAttendentDays.Text) > 31 )
            {
                MessageBox.Show("Days Can Not Be Greater Than 31!!");
            }
            else if (Convert.ToInt32(txtAttendentDays.Text) < 0)
            {
                MessageBox.Show("Days Can Not Be Less Than 0!!");
            }
            else
            {
                d=Convert.ToInt32(txtAttendentDays.Text);
                txtAmount.Text = "Rs. " + (d * dSal);
            }


        }

        private void dgvSalary_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {
            Employees obj=new Employees();
            obj.Show();
            this.Hide();
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


        int d = 1;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if(cmbEmployee.SelectedIndex == -1 || txtAttendentDays.Text == "" || dateTpPeriod.Text == "")
                {
                    MessageBox.Show("Missing Data!!");
                }
                else
                {
                    period = dateTpPeriod.Value.Date.Month.ToString() + "-" + dateTpPeriod.Value.Date.Year.ToString();
                    int amount = dSal * Convert.ToInt32(txtAttendentDays.Text);

                    int days = Convert.ToInt32(txtAttendentDays.Text);

                    string Query = "INSERT INTO  SalaryTbl VALUES({0},{1},'{2}',{3},'{4}')";
                    Query = string.Format(Query, cmbEmployee.SelectedValue.ToString(), days, period, amount, DateTime.Today.Date);
                    Con.SetData(Query);
                    ShowSalaries();

                    //Send salary informations to employee through the email

                    string name = cmbEmployee.SelectedValue.ToString();  //*************************************************
                    string email = "tharindakaushalya778@gmail.com";  //mail reciever
                    //string mobile = txtMobileNumber.Text;
                    string userName = "tharindakaushalya98"; //mail sender
                    string password = "app pasword"; // enter app pasword for outhentication
                    string smtp = "smtp.gmail.com";

                    login = new NetworkCredential(userName, password); // txtusername, txtpasswrd
                    client = new SmtpClient(smtp);//txtsmtp
                    client.Port = 587;
                    client.EnableSsl = true;
                    client.Credentials = login;


                    //start send mail
                    message = new MailMessage { From = new MailAddress(userName + smtp.Replace("smtp.", "@"), "Employee Management System", Encoding.UTF8) }; //txtusrname+txtsmtp.repalace
                    message.To.Add(new MailAddress(email));
                    message.Subject = "Monthly Salary Details";
                    message.Body = "Monthly salary of September "+ amount;
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    message.Priority = MailPriority.Normal;
                    message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallBack);
                    string userState = "Sending...";
                    client.SendAsync(message, userState);
                    //end send mail



                    // end mail



                    MessageBox.Show("Salary Paid!!");
                    txtAttendentDays.Text = "";
                    cmbEmployee.SelectedValue = -1;
                    
                }
                
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
            }
           

        }

        private void cmbEmployee_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetSalary();
        }

        private void lblLogout_Click(object sender, EventArgs e)
        {
            Login obj= new Login();
            obj.Show();
            this.Hide();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {

        }

        private static void SendCompletedCallBack(object sender, AsyncCompletedEventArgs e)  //**************************************************
        {
            if (e.Cancelled)
            {
                MessageBox.Show(string.Format("{0} send cancelled", e.UserState), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (e.Error != null)
            {
                MessageBox.Show(string.Format("{0} {1}", e.UserState, e.Error), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Your message has been successfully sent!!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

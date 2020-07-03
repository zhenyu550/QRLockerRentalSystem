using LockerRentalManagementSystem.Controller;
using LockerRentalManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockerRentalManagementSystem.View
{
    public partial class MasterKeyForm : Form
    {
        public MasterKeyForm(Employee employee)
        {
            InitializeComponent();

            // Display detail of employee
            labelMasterKeyEmployee.Text = String.Format("{0} ({1})", employee.Name, employee.Username);

            // Get the master key of the employee
            EmployeeController employeeController = new EmployeeController();
            Bitmap masterKeyQr = employeeController.GenerateQR(employee.MasterKey);

            // Display the master key
            pictureBoxMasterKeyQr.Image = masterKeyQr;
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

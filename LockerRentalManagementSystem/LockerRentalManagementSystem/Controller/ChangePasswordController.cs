using LockerRentalManagementSystem.Core;
using LockerRentalManagementSystem.Exceptions;
using LockerRentalManagementSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockerRentalManagementSystem.Controller
{
    public class ChangePasswordController
    {
        // Private attributes
        private string _hashedNewPw = "";
        private string _hashedReconfirmPw = "";
        private string _hashedOldPw = "";
        Employee _employee = new Employee();

        // Constructor
        public ChangePasswordController(Employee employee)
        {
            _employee = employee;
        }

        // Methods
        public void ValidatePassword(string oldPw, string newPw, string reconfrimPw)
        {
            _hashedOldPw = Security.SHA256Hash(oldPw);
            _hashedNewPw = Security.SHA256Hash(newPw);
            _hashedReconfirmPw = Security.SHA256Hash(reconfrimPw);

            if (!newPw.Equals(reconfrimPw))
                throw new InvalidChangePasswordException("Reconfirm Fail");
            if (!_hashedOldPw.Equals(_employee.Password))
                throw new InvalidChangePasswordException("Incorrrect Old Password");
            if (oldPw.Equals(newPw))
                throw new InvalidChangePasswordException("Old and New Same");
        }

        public void ChangePassword()
        {
            _employee.Password = _hashedNewPw;

            // Save the new employee data
            EmployeeController employeeController = new EmployeeController();
            employeeController.SetEmployeeData(_employee);
            employeeController.SaveEmployeeData();
        }

        public void ActivateAccount()
        {
            // Set status to Active
            _employee.Restore(); 
        }
    }
}

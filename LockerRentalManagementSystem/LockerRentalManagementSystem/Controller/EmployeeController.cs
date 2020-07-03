using ClosedXML.Excel;
using LockerRentalManagementSystem.Core;
using LockerRentalManagementSystem.Exceptions;
using LockerRentalManagementSystem.Model;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockerRentalManagementSystem.Controller
{
    public class EmployeeController
    {
        // Private Attributes
        Employee _employee = new Employee();

        // Getter & Setters
        public Employee GetEmployee()
        {
            return _employee;
        }

        // Constructor
        public EmployeeController() { }

        // Methods
        // Method for Set Employee Data (for Add New Employee)
        public void SetEmployeeData(string name, string icPassport, string gender, string phoneNo, string email,
            string address, string username, string password, string permission, string position)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(icPassport) ||
                 string.IsNullOrWhiteSpace(gender) || string.IsNullOrWhiteSpace(phoneNo) ||
                 string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(address) ||
                 string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(permission) ||
                 string.IsNullOrWhiteSpace(position))
                throw new InvalidUserInputException("Empty Field");
            else if ((!gender.Equals("Male") && !gender.Equals("Female")) ||
                (!position.Equals("Staff") && !position.Equals("Manager") && !position.Equals("Owner")) ||
                (!permission.Equals("Normal") && !permission.Equals("Admin")))
                throw new InvalidUserInputException("Invalid ComboBox Input - Employee");
            else
            {
                if (!Database.CheckUnique("employee", "ic_passport", icPassport))
                    throw new InvalidUserInputException("Duplicate Detected", "IC / Passport No.", icPassport, "Employee");
                if (!Database.CheckUnique("employee", "name", name))
                    throw new InvalidUserInputException("Duplicate Detected", "Name", name, "Employee");
                if (!Database.CheckUnique("employee", "username", username))
                    throw new InvalidUserInputException("Duplicate Detected", "Username", username, "Employee");

                string employeeMasterKey = Security.SHA256Hash( username + DateTime.Now.ToString("fffffffssmmHHddMMyyyy" + 
                    icPassport + DateTime.Now.ToString("ddddMMMM")));

                _employee.Name = name;
                _employee.IcPassport = icPassport;
                _employee.Gender = gender;
                _employee.PhoneNo = phoneNo;
                _employee.Email = email;
                _employee.Address = address;
                _employee.Username = username;
                _employee.Password = Security.SHA256Hash(password);
                _employee.Permission = permission;
                _employee.Position = position;
                _employee.MasterKey = employeeMasterKey;
            }
        }

        // Overloading Method for Set Employee Data (for Edit Employe)
        public void SetEmployeeData(Employee employee, string phoneNo, string email, string address,
            string permission, string position)
        {
            if (string.IsNullOrWhiteSpace(phoneNo) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(permission) ||
                string.IsNullOrWhiteSpace(position))
                throw new InvalidUserInputException("Empty Field");
            else if ((!position.Equals("Staff") && !position.Equals("Manager") && !position.Equals("Owner")) ||
                (!permission.Equals("Normal") && !permission.Equals("Admin")))
                throw new InvalidUserInputException("Invalid ComboBox Input - Employee");
            else if (employee.Permission.Equals("Admin") && permission.Equals("Normal") &&
                Employee.Count("permission = 'Admin'") <= 1)
            {
                // Check if number of adminstrators are more than one before changing the permission
                throw new InvalidUserInputException("Last Admin");
            }
            else
            {
                _employee = employee;
                _employee.PhoneNo = phoneNo;
                _employee.Email = email;
                _employee.Address = address;
                _employee.Permission = permission;
                _employee.Position = position;
            }
        }

        // Overloading Method for Set Employee Data (for Change Password)
        public void SetEmployeeData(Employee employee)
        {
            _employee = employee;
        }

        public void SaveEmployeeData()
        {
            _employee.Save();
        }

        public void DeleteEmployeeData(int id)
        {
            Employee emp = Employee.Get(id);

            // Check if Employee involve in any rental
            int noOfRental = Rental.Count(String.Format("employee_id = {0} AND status <> 'Ended'", id));
            if (noOfRental > 0)
                throw new InvalidUserInputException("Delete Error - Employee Rental");

            // Check if the selected account the only admin in database
            if (emp.IsAdmin() && Employee.Count("permission = 'Admin'") <= 1)
                throw new InvalidUserInputException("Last Admin - Delete");
            else
                emp.TempDelete();
        }

        public void RestoreEmployeeData(int id)
        {
            Employee employee = Employee.Get(id);
            employee.Restore();
        }

        public void ExportEmployeeData(int id)
        {
            if (id > 0)
            {
                var deletedEmp = Employee.Where(String.Format("id = {0}", id), 0, 1);
                string defaultFileName = String.Format("EXPORT_EMPLOYEE_{0}_{1}", deletedEmp[0].Id, DateTime.Now.ToString("ddMMyyyy_HHmmss"));

                var workbook = new XLWorkbook();
                var ws = workbook.AddWorksheet("DeletedEmployee");
                ws.Cell(1, 1).Value = "Employee";
                ws.Cell(2, 1).InsertTable(deletedEmp);

                SaveFileDialog sf = new SaveFileDialog
                {
                    FileName = defaultFileName,
                    Filter = "Excel Workbook (.xlsx) |*.xlsx",
                    Title = "Export Employee as",
                    FilterIndex = 1
                };
                if (sf.ShowDialog() == DialogResult.OK)
                {
                    string savePath = Path.GetDirectoryName(sf.FileName);
                    string fileName = Path.GetFileName(sf.FileName);
                    string saveFile = Path.Combine(savePath, fileName);
                    try
                    {
                        workbook.SaveAs(saveFile); //Save the file                            
                        deletedEmp[0].Delete();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                        throw new InvalidUserInputException("Export Fail", "", "", "employee");
                    }
                }
                else
                    throw new InvalidUserInputException("Export Fail", "", "", "employee");
            }
            else
            {
                var delEmpFirst = Employee.Where("id = (SELECT id FROM EMPLOYEE WHERE status = 'Disabled' LIMIT 0,1)", 0, 1);
                var delEmpLast = Employee.Where("id = (SELECT MAX(id) FROM EMPLOYEE WHERE status = 'Disabled' LIMIT 0,1)", 0, 1);

                if (!delEmpFirst.Any() || !delEmpLast.Any())
                    throw new InvalidUserInputException("Empty Records", "", "", "employee");

                string defaultFileName = String.Format("EXPORT_EMPLOYEE_{0}~{1}_{2}", delEmpFirst[0].Id, delEmpLast[0].Id, DateTime.Now.ToString("ddMMyyyy_HHmmss"));

                var deletedEmpList = Employee.Where("status = 'Disabled'", 0, 2147483647);

                var workbook = new XLWorkbook();
                var ws = workbook.AddWorksheet("DeletedEmployee");
                ws.Cell(1, 1).Value = "Employee";
                ws.Cell(2, 1).InsertTable(deletedEmpList);

                SaveFileDialog sf = new SaveFileDialog
                {
                    FileName = defaultFileName,
                    Filter = "Excel Workbook (.xlsx) |*.xlsx",
                    Title = "Export Employee as",
                    FilterIndex = 1
                };

                if (sf.ShowDialog() == DialogResult.OK)
                {
                    string savePath = Path.GetDirectoryName(sf.FileName);
                    string fileName = Path.GetFileName(sf.FileName);
                    string saveFile = Path.Combine(savePath, fileName);
                    try
                    {
                        workbook.SaveAs(saveFile); //Save the file

                        foreach (Employee item in deletedEmpList)
                            item.Delete();

                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                        throw new InvalidUserInputException("Export Fail", "", "", "employee");
                    }
                }
                else
                    throw new InvalidUserInputException("Export Fail", "", "", "employee");
            }
        }

        public Bitmap GenerateQR(string input)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(input, QRCodeGenerator.ECCLevel.Q);

            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            return qrCodeImage;
        }
    }
}

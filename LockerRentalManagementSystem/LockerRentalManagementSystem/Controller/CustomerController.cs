using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LockerRentalManagementSystem.Core;
using LockerRentalManagementSystem.Model;
using LockerRentalManagementSystem.Exceptions;
using System.Windows.Forms;
using System.IO;
using ClosedXML.Excel;

namespace LockerRentalManagementSystem.Controller
{
    public class CustomerController
    {
        //  Private Attributes
        private Customer _customer = new Customer();

        // Getter
        public Customer GetCustomer()
        {
            return _customer;
        }

        //  Constructor
        public CustomerController() { }

        //  Methods
        // Method for Set Customer Data (Add Customer)
        public void SetCustomerData(string name, string icPassport, string gender, string phoneNo, string email, string address)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(icPassport) ||
                string.IsNullOrWhiteSpace(gender) || string.IsNullOrWhiteSpace(phoneNo) ||
                string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(address))
                throw new InvalidUserInputException("Empty Field");
            else if (!gender.Equals("Male") && !gender.Equals("Female"))
                throw new InvalidUserInputException("Invalid ComboBox Input - Customer");
            else
            {
                if (!Database.CheckUnique("customer", "ic_passport", icPassport))
                    throw new InvalidUserInputException("Duplicate Detected", "IC / Passport No.", icPassport, "Customer");
                if (!Database.CheckUnique("customer", "name", name))
                    throw new InvalidUserInputException("Duplicate Detected", "Name", name, "Customer");

                _customer.Name = name;
                _customer.IcPassport = icPassport;
                _customer.Gender = gender;
                _customer.PhoneNo = phoneNo;
                _customer.Email = email;
                _customer.Address = address;
            }
        }

        // Overloading Method for Set Customer (Edit Customer)
        public void SetCustomerData(Customer customer, string phoneNo, string email, string address)
        {
            if (string.IsNullOrWhiteSpace(phoneNo) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(address))
                throw new InvalidUserInputException("Empty Field");
            else
            {
                _customer = customer;
                _customer.PhoneNo = phoneNo;
                _customer.Email = email;
                _customer.Address = address;
            }
        }

        public void SaveCustomerData()
        {
            _customer.Save();
        }

        public void DeleteCustomerData(int id)
        {
            //Check if Customer involve in any rental
            int noOfRental = Rental.Count(String.Format("customer_id = {0} AND status <> 'Ended'", id));
            if (noOfRental > 0)
                throw new InvalidUserInputException("Delete Error - Customer Rental");

            Customer customer = Customer.Get(id);
            customer.TempDelete();
        }

        public void RestoreCustomerData(int id)
        {
            Customer customer = Customer.Get(id);
            customer.Restore();
        }

        public void ExportCustomerData(int id)
        {
            if (id > 0)
            {
                var deletedCus = Customer.Where(String.Format("id = {0}", id), 0, 1);
                string defaultFileName = String.Format("EXPORT_CUSTOMER_{0}_{1}", deletedCus[0].Id, DateTime.Now.ToString("ddMMyyyy_HHmmss"));

                var workbook = new XLWorkbook();
                var ws = workbook.AddWorksheet("DeletedCustomer");
                ws.Cell(1, 1).Value = "Customer";
                ws.Cell(2, 1).InsertTable(deletedCus);

                SaveFileDialog sf = new SaveFileDialog
                {
                    FileName = defaultFileName,
                    Filter = "Excel Workbook (.xlsx) |*.xlsx",
                    Title = "Export Customer as",
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
                        deletedCus[0].Delete();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                        throw new InvalidUserInputException("Export Fail", "", "", "customer");
                    }
                }
                else
                    throw new InvalidUserInputException("Export Fail", "", "", "customer");

            }
            else
            {
                var delCusFirst = Customer.Where("id = (SELECT id FROM CUSTOMER WHERE status = 'Disabled' LIMIT 0,1)", 0, 1);
                var delCusLast = Customer.Where("id = (SELECT MAX(id) FROM CUSTOMER WHERE status = 'Disabled' LIMIT 0,1)", 0, 1);

                if (!delCusFirst.Any() || !delCusLast.Any())
                    throw new InvalidUserInputException("Empty Records", "", "", "customer");

                string defaultFileName = String.Format("EXPORT_CUSTOMER_{0}~{1}_{2}", delCusFirst[0].Id, delCusLast[0].Id, DateTime.Now.ToString("ddMMyyyy_HHmmss"));

                var deletedCusList = Customer.Where("status = 'Disabled'", 0, 2147483647);

                var workbook = new XLWorkbook();
                var ws = workbook.AddWorksheet("DeletedCustomer");
                ws.Cell(1, 1).Value = "Customer";
                ws.Cell(2, 1).InsertTable(deletedCusList);

                SaveFileDialog sf = new SaveFileDialog
                {
                    FileName = defaultFileName,
                    Filter = "Excel Workbook (.xlsx) |*.xlsx",
                    Title = "Export Customers as",
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

                        foreach (Customer item in deletedCusList)
                            item.Delete();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                        throw new InvalidUserInputException("Export Fail", "", "", "customer");
                    }
                }
                else
                    throw new InvalidUserInputException("Export Fail", "", "", "customer");
            }
        }
    }
}

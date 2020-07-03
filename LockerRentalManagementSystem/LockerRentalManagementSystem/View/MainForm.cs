using LockerRentalManagementSystem.Controller;
using LockerRentalManagementSystem.Core;
using LockerRentalManagementSystem.Exceptions;
using LockerRentalManagementSystem.Model;
using LockerRentalManagementSystem.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockerRentalManagementSystem
{
    public partial class MainForm : Form
    {
        // Private Attributes
        private Employee _employee = new Employee();
        private bool _search = false;
        private string _searchCondition = "";
        private int _sortColumn = -1;

        private List<LockerType> _lockerTypes = new List<LockerType>();
        private Dictionary<int, string> _lockerTypeDictonary = new Dictionary<int, string>();
        private string _cabinetStatus = "";
        private string _cabinetSize = "";
        private string _oldCabinetStatus = "";
        private string _oldCabinetSize = "";
        private bool _pageFlip = false;
        private int _cabinetId;

        // Pages
        private Page _employeePage = new Page();
        private Page _customerPage = new Page();
        private Page _lockerTypePage = new Page();
        private Page _cabinetPage = new Page();
        private Page _lockerCabinetPage = new Page();
        private Page _lockerPage = new Page();
        private Page _rentalPage = new Page();
        private Page _deletedEmployeePage = new Page();
        private Page _deletedCustomerPage = new Page();
        private Page _deletedLockerTypePage = new Page();
        private Page _deletedCabinetPage = new Page();
        private Page _endedRentalPage = new Page();

        // Getter & Setters
        public Employee GetEmployee()
        {
            return _employee;
        }

        public void SetEmployee(Employee employee)
        {
            _employee = employee;
        }

        // Constructor
        public MainForm()
        {
            InitializeComponent();
        }

        /* 
         * Methods
         */
        /*
         * List View Table Reload Methods
         */
        private void ReloadEmployeeList(int count, int offset, string condition)
        {
            listViewEmployee.Items.Clear();
            List<Employee> items = Employee.Where(condition, count, offset);
            foreach (Employee e in items)
            {
                ListViewItem lvi = new ListViewItem(e.Id.ToString());
                lvi.SubItems.Add(e.Name);
                lvi.SubItems.Add(e.IcPassport);
                lvi.SubItems.Add(e.Position);
                lvi.SubItems.Add(e.Username);
                lvi.SubItems.Add(e.Permission);

                listViewEmployee.Items.Add(lvi);
            }
        }

        private void ReloadCustomerList(int offset, int count, string condition)
        {
            listViewCustomer.Items.Clear();
            List<Customer> items = Customer.Where(condition, offset, count);
            foreach (Customer c in items)
            {
                ListViewItem lvi = new ListViewItem(c.Id.ToString());
                lvi.SubItems.Add(c.Name);
                lvi.SubItems.Add(c.IcPassport);

                listViewCustomer.Items.Add(lvi);
            }
        }

        private void ReloadLockerTypeList(int count, int offset, string condition)
        {
            listViewLockerType.Items.Clear();
            List<LockerType> lockerTypes = LockerType.Where(condition, count, offset);

            foreach (LockerType t in lockerTypes)
            {
                int cabinetAmount = Cabinet.Count(String.Format("locker_type_id = {0} AND status <> 'Disabled'", t.Id));
                ListViewItem lvi = new ListViewItem(t.Id.ToString());
                lvi.SubItems.Add(t.Name);
                lvi.SubItems.Add(t.Code);
                lvi.SubItems.Add(t.Rate.ToString("#0.00"));
                lvi.SubItems.Add(cabinetAmount.ToString());

                listViewLockerType.Items.Add(lvi);
            }
        }

        private void ReloadCabinetList(int count, int offset, string condition)
        {
            List<LockerType> lockerTypes = LockerType.Where("status <> 'Disabled'", 0, 100);

            listViewCabinet.Items.Clear();
            List<Cabinet> items = Cabinet.Where(condition, count, offset);
            foreach (Cabinet cab in items)
            {
                ListViewItem lvi = new ListViewItem(cab.Id.ToString());
                lvi.SubItems.Add(cab.Code);

                var selectedLockerType = from selected in lockerTypes
                                         where selected.Id.Equals(cab.LockerTypeId)
                                         select selected.Name;

                string lockerTypeName = selectedLockerType.First();

                lvi.SubItems.Add(lockerTypeName);
                lvi.SubItems.Add(cab.Row.ToString());
                lvi.SubItems.Add(cab.Column.ToString());
                lvi.SubItems.Add(cab.Status);

                listViewCabinet.Items.Add(lvi);
            }
        }

        private void ReloadLockerCabinetList(int count, int offset, string condition)
        {
            listViewLockerCabinet.Items.Clear();

            List<Cabinet> items = Cabinet.Where(condition, count, offset);
            foreach (Cabinet cab in items)
            {
                ListViewItem lvi = new ListViewItem(cab.Id.ToString());
                lvi.SubItems.Add(cab.Code);
                lvi.SubItems.Add(cab.Status);

                listViewLockerCabinet.Items.Add(lvi);
            }
        }

        private void ReloadLockerList(int count, int offset, string condition)
        {
            listViewLocker.Items.Clear();
            List<Locker> items = Locker.Where(condition, count, offset);
            foreach (Locker locker in items)
            {
                ListViewItem lvi = new ListViewItem(locker.Code);
                lvi.SubItems.Add(locker.Status);
                lvi.SubItems.Add(locker.DoorStatus);
                if (locker.Status == "Available")
                {
                    lvi.ImageIndex = 0;
                }
                else if (locker.Status == "Occupied")
                {
                    lvi.ImageIndex = 1;
                }
                else if (locker.Status == "Not Available")
                {
                    lvi.ImageIndex = 2;
                }
                else
                {
                    lvi.ImageIndex = 3;
                }

                listViewLocker.Items.Add(lvi);
            }
        }

        private void ReloadRentalList(int count, int offset, string condition)
        {
            listViewRental.Items.Clear();

            List<Rental> items = Rental.Where(condition, count, offset);
            foreach (Rental r in items)
            {
                ListViewItem lvi = new ListViewItem(r.Id.ToString());

                DateTime startDate = r.StartDate;
                lvi.SubItems.Add(startDate.ToString("dd-MM-yyyy"));

                DateTime endDate = r.EndDate;
                lvi.SubItems.Add(endDate.ToString("dd-MM-yyyy"));

                var customer = new Customer();
                customer = Customer.Get(r.CustomerId);

                var locker = new Locker();
                locker = Locker.Get(r.LockerId);

                string timeLeft = "";
                int timeLeftValue = 0;
                DateTime currentDateTime = DateTime.Now;

                if (r.IsNotStarted())
                {
                    // If the start date equals or earlier than current date
                    // startDate Later than currentDate will give value of 1
                    if (startDate.Date.CompareTo(currentDateTime.Date) <= 0)
                    {
                        // Check if the Locker was occupied or overdues, if it is, end the rental that occupies it
                        if (locker.IsOccupied() || locker.IsOverdued())
                        {
                            List<Rental> blockingRentals = Rental.Where(String.Format("locker_id = {0} AND status <> 'Ended'", locker.Id), 0, 1);
                            foreach(Rental blockingRental in blockingRentals)
                            {
                                // Check if the starting rental start date is equal or later than
                                // blocking rental end date.
                                // If yes, end the rental and update in database
                                if (startDate.Date.CompareTo(blockingRental.EndDate.Date) >= 0)
                                {
                                    blockingRental.ReturnDateTime = DateTime.Now;
                                    blockingRental.End();
                                }
                            }
                        }

                        // Set the Rental Status to Started
                        r.Start();

                        // Set the Locker belongs to it to Occupied
                        locker.Occupied();

                        // Check if the Cabinet is Full, if it is, set cabinet to full
                        string lockerSearchCondition = String.Format("cabinet_id = {0} AND status = 'Available'", locker.CabinetId);
                        int noOfEmptyLocker = Locker.Count(lockerSearchCondition);
                        if (noOfEmptyLocker <= 0)
                        {
                            Cabinet cab = Cabinet.Get(locker.CabinetId);
                            cab.Full();
                        }

                        TimeSpan timeSpan = endDate.Date.Subtract(DateTime.Now.Date);
                        timeLeftValue = Convert.ToInt32(timeSpan.Days);
                        timeLeft = timeLeftValue.ToString();
                    }
                }
                else
                {
                    TimeSpan timeSpan = endDate.Date.Subtract(DateTime.Now.Date);
                    timeLeftValue = Convert.ToInt32(timeSpan.Days);
                    timeLeft = timeLeftValue.ToString();
                }
                lvi.SubItems.Add(timeLeft);

                if (timeLeftValue < 0)
                {
                    lvi.ImageIndex = 1;
                    if (!r.IsOverdue())
                    {
                        r.Overdue();
                        locker.Overdued();
                    }
                }
                else if (r.IsNotStarted())
                    lvi.ImageIndex = 2;
                else
                    lvi.ImageIndex = 0;

                lvi.SubItems.Add(customer.IcPassport);
                lvi.SubItems.Add(locker.Code);
                lvi.SubItems.Add(r.Status);

                listViewRental.Items.Add(lvi);
            }
        }

        private void ReloadDeletedEmployeeList(int count, int offset, string condition)
        {
            listViewDeletedEmployee.Items.Clear();
            List<Employee> items = Employee.Where(condition, count, offset);
            foreach (Employee e in items)
            {
                ListViewItem lvi = new ListViewItem(e.Id.ToString());
                lvi.SubItems.Add(e.Name);
                lvi.SubItems.Add(e.IcPassport);
                lvi.SubItems.Add(e.Position);
                lvi.SubItems.Add(e.Username);
                lvi.SubItems.Add(e.Permission);

                listViewDeletedEmployee.Items.Add(lvi);
            }
        }

        private void ReloadDeletedCustomerList(int count, int offset, string condition)
        {
            listViewDeletedCustomer.Items.Clear();
            List<Customer> items = Customer.Where(condition, count, offset);
            foreach (Customer c in items)
            {
                ListViewItem lvi = new ListViewItem(c.Id.ToString());
                lvi.SubItems.Add(c.Name);
                lvi.SubItems.Add(c.IcPassport);

                listViewDeletedCustomer.Items.Add(lvi);
            }
        }

        private void ReloadDeletedLockerTypeList(int count, int offset, string condition)
        {
            listViewDeletedLockerType.Items.Clear();
            List<LockerType> items = LockerType.Where(condition, count, offset);
            foreach (LockerType t in items)
            {
                var cab = new Cabinet();
                ListViewItem lvi = new ListViewItem(t.Id.ToString());
                lvi.SubItems.Add(t.Name);
                lvi.SubItems.Add(t.Code);
                lvi.SubItems.Add(t.Rate.ToString("#0.00"));

                listViewDeletedLockerType.Items.Add(lvi);
            }
        }

        private void ReloadDeletedCabinetList(int count, int offset, string condition)
        {
            List<LockerType> lockerTypes = LockerType.Where("status IS NOT NULL", 0, 100);

            listViewDeletedCabinet.Items.Clear();
            List<Cabinet> items = Cabinet.Where(condition, count, offset);

            foreach (Cabinet cab in items)
            {
                ListViewItem lvi = new ListViewItem(cab.Id.ToString());
                lvi.SubItems.Add(cab.Code);

                var cabinetSize = from selected in lockerTypes
                                  where selected.Id.Equals(cab.LockerTypeId)
                                  select selected.Code;
                string sizeCode = cabinetSize.First();

                lvi.SubItems.Add(sizeCode);
                lvi.SubItems.Add(cab.Row.ToString());
                lvi.SubItems.Add(cab.Column.ToString());

                listViewDeletedCabinet.Items.Add(lvi);
            }
        }

        private void ReloadEndedRentalList(int count, int offset, string condition)
        {
            listViewEndedRental.Items.Clear();

            List<Rental> items = Rental.Where(condition, count, offset);
            foreach (Rental r in items)
            {
                ListViewItem lvi = new ListViewItem(r.Id.ToString());
                lvi.SubItems.Add(r.Code);

                DateTime bookingdateTime = r.BookingDateTime;
                lvi.SubItems.Add(bookingdateTime.ToString("dd-MM-yyyy HH:mm:ss"));

                DateTime startDate = r.StartDate;
                lvi.SubItems.Add(startDate.ToString("dd-MM-yyyy"));

                DateTime endDate = r.EndDate;
                lvi.SubItems.Add(endDate.ToString("dd-MM-yyyy"));

                lvi.SubItems.Add(r.Duration.ToString());
                lvi.SubItems.Add(r.ReturnDateTime.ToString("dd-MM-yyyy HH:mm:ss"));

                var customer = new Customer();
                customer = Customer.Get(r.CustomerId);
                lvi.SubItems.Add(customer.IcPassport);

                var employee = new Employee();
                employee = Employee.Get(r.EmployeeId);
                lvi.SubItems.Add(employee.Username);

                var locker = new Locker();
                locker = Locker.Get(r.LockerId);
                lvi.SubItems.Add(locker.Code);

                listViewEndedRental.Items.Add(lvi);
            }
        }

        /*
         * Page Methods
         */
        private void EmployeePage()
        {
            string condition = "Status <> 'Disabled'";

            _employeePage.FinalIndex = Convert.ToDouble(Employee.Count(condition));
            _employeePage.LastPage = Convert.ToInt32(Math.Ceiling(_employeePage.FinalIndex / _employeePage.MaxItems));
            _employeePage.PageSetting();
            if (_employeePage.FinalIndex == 0)
            {
                _employeePage.PageReset();
            }
            if (_employeePage.PageNumber == _employeePage.LastPage)
            { _employeePage.LastIndex = (int)_employeePage.FinalIndex; }

            toolStripLabelPageNoEmployee.Text = String.Format("Page {0} / {1}", _employeePage.PageNumber, _employeePage.LastPage);
            toolStripLabelResultEmployee.Text = String.Format("Showing result {0}~{1}", _employeePage.FirstIndex, _employeePage.LastIndex);
            ReloadEmployeeList(_employeePage.IndexLimit, _employeePage.MaxItems, condition);
        }

        private void EmployeePage(string condition)
        {
            _employeePage.FinalIndex = Convert.ToDouble(Employee.Count(condition));
            _employeePage.LastPage = Convert.ToInt32(Math.Ceiling(_employeePage.FinalIndex / _employeePage.MaxItems));
            _employeePage.PageSetting();
            if (_employeePage.FinalIndex == 0)
            {
                _employeePage.PageReset();
            }
            if (_employeePage.PageNumber == _employeePage.LastPage)
            { _employeePage.LastIndex = (int)_employeePage.FinalIndex; }

            toolStripLabelPageNoEmployee.Text = String.Format("Page {0} / {1}", _employeePage.PageNumber, _employeePage.LastPage);
            toolStripLabelResultEmployee.Text = String.Format("Showing result {0}~{1}", _employeePage.FirstIndex, _employeePage.LastIndex);
            ReloadEmployeeList(_employeePage.IndexLimit, _employeePage.MaxItems, condition);
        }

        private void CustomerPage()
        {
            string condition = "status <> 'Disabled'";

            _customerPage.FinalIndex = Convert.ToDouble(Customer.Count(condition));
            _customerPage.LastPage = Convert.ToInt32(Math.Ceiling(_customerPage.FinalIndex / _customerPage.MaxItems));
            _customerPage.PageSetting();
            if (_customerPage.FinalIndex == 0)
            {
                _customerPage.PageReset();
            }
            if (_customerPage.PageNumber == _customerPage.LastPage)
            { _customerPage.LastIndex = (int)_customerPage.FinalIndex; }
            toolStripLabelPageNoCustomer.Text = String.Format("Page {0} / {1}", _customerPage.PageNumber, _customerPage.LastPage);
            toolStripLabelResultCustomer.Text = String.Format("Showing result {0}~{1}", _customerPage.FirstIndex, _customerPage.LastIndex);
            ReloadCustomerList(_customerPage.IndexLimit, _customerPage.MaxItems, condition);
        }

        private void CustomerPage(string condition)
        {
            _customerPage.FinalIndex = Convert.ToDouble(Customer.Count(condition));

            _customerPage.LastPage = Convert.ToInt32(Math.Ceiling(_customerPage.FinalIndex / _customerPage.MaxItems));
            _customerPage.PageSetting();
            if (_customerPage.FinalIndex == 0)
            {
                _customerPage.PageReset();
            }
            if (_customerPage.PageNumber == _customerPage.LastPage)
            { _customerPage.LastIndex = (int)_customerPage.FinalIndex; }
            toolStripLabelPageNoCustomer.Text = String.Format("Page {0} / {1}", _customerPage.PageNumber, _customerPage.LastPage);
            toolStripLabelResultCustomer.Text = String.Format("Showing result {0}~{1}", _customerPage.FirstIndex, _customerPage.LastIndex);
            ReloadCustomerList(_customerPage.IndexLimit, _customerPage.MaxItems, condition);
        }

        private void LockerTypePage()
        {
            string condition = "status <> 'Disabled'";
            _lockerTypePage.FinalIndex = Convert.ToDouble(LockerType.Count(condition));
            _lockerTypePage.LastPage = Convert.ToInt32(Math.Ceiling(_lockerTypePage.FinalIndex / _lockerTypePage.MaxItems));
            _lockerTypePage.PageSetting();
            if (_lockerTypePage.FinalIndex == 0)
                _lockerTypePage.PageReset();
            
            if (_lockerTypePage.PageNumber == _lockerTypePage.LastPage)
                _lockerTypePage.LastIndex = (int)_lockerTypePage.FinalIndex;

            toolStripLabelPageNoLockerType.Text = String.Format("Page {0} / {1}", _lockerTypePage.PageNumber, _lockerTypePage.LastPage);
            ReloadLockerTypeList(_lockerTypePage.IndexLimit, _lockerTypePage.MaxItems, condition);

        }

        private void CabinetPage()
        {
            string condition = "status <> 'Disabled'";
            _cabinetPage.FinalIndex = Convert.ToDouble(Cabinet.Count(condition));
            _cabinetPage.LastPage = Convert.ToInt32(Math.Ceiling(_cabinetPage.FinalIndex / _cabinetPage.MaxItems));
            _cabinetPage.PageSetting();
            if (_cabinetPage.FinalIndex == 0)
                _cabinetPage.PageReset();
            
            if (_cabinetPage.PageNumber == _cabinetPage.LastPage)
                _cabinetPage.LastIndex = (int)_cabinetPage.FinalIndex;

            toolStripLabelPageNoCabinet.Text = String.Format("Page {0} / {1}", _cabinetPage.PageNumber, _cabinetPage.LastPage);
            toolStripLabelResultCabinet.Text = String.Format("Showing result {0}~{1}", _cabinetPage.FirstIndex, _cabinetPage.LastIndex);
            ReloadCabinetList(_cabinetPage.IndexLimit, _cabinetPage.MaxItems, condition);
        }

        private void CabinetPage(string condition)
        {
            _cabinetPage.FinalIndex = Convert.ToDouble(Cabinet.Count(condition));
            _cabinetPage.LastPage = Convert.ToInt32(Math.Ceiling(_cabinetPage.FinalIndex / _cabinetPage.MaxItems));
            _cabinetPage.PageSetting();
            if (_cabinetPage.FinalIndex == 0)
                _cabinetPage.PageReset();
            
            if (_cabinetPage.PageNumber == _cabinetPage.LastPage)
                _cabinetPage.LastIndex = (int)_cabinetPage.FinalIndex;

            toolStripLabelPageNoCabinet.Text = String.Format("Page {0} / {1}", _cabinetPage.PageNumber, _cabinetPage.LastPage);
            toolStripLabelResultCabinet.Text = String.Format("Showing result {0}~{1}", _cabinetPage.FirstIndex, _cabinetPage.LastIndex);
            ReloadCabinetList(_cabinetPage.IndexLimit, _cabinetPage.MaxItems, condition);
        }

        private void LockerCabinetPage()
        {
            if (!_pageFlip)
            {
                //Buffer to filter and reduce refresh rounds for the locker cabinet list.
                //This is due to event raidoButton_CheckedChanged will call this function 2 times for each change 
                //(1 time before change and 1 time after change).
                //For comboBoxLockerTypeLockerCabinet_SelectedIndexChanged, if cabinet_status is empty, exit this function to prevent errors.

                //Return if cabinet_status OR cabient_size is empty
                if (String.IsNullOrWhiteSpace(_cabinetStatus) || String.IsNullOrWhiteSpace(_cabinetSize))
                    return;
                //Return if new cabinet status == old cabinet status AND new cabinet size == old cabinet size
                if (_cabinetStatus == _oldCabinetStatus && _cabinetSize == _oldCabinetSize)
                    return;

                _oldCabinetStatus = _cabinetStatus;
                _oldCabinetSize = _cabinetSize;
            }

            string condition = "status {0} AND locker_type_id {1}";
            condition = String.Format(condition, _cabinetStatus, _cabinetSize);

            _lockerCabinetPage.FinalIndex = Cabinet.Count(condition);
            _lockerCabinetPage.LastPage = Convert.ToInt32(Math.Ceiling(_lockerCabinetPage.FinalIndex / _lockerCabinetPage.MaxItems));
            _lockerCabinetPage.PageSetting();

            if (_lockerCabinetPage.FinalIndex == 0)
                _lockerCabinetPage.PageReset();
            

            if (_lockerCabinetPage.PageNumber == _lockerCabinetPage.LastPage)
                _lockerCabinetPage.LastIndex = (int)_lockerCabinetPage.FinalIndex; 

            toolStripLabelPageNoLockerCabinet.Text = String.Format("Page {0} / {1}", _lockerCabinetPage.PageNumber, _lockerCabinetPage.LastPage);

            ReloadLockerCabinetList(_lockerCabinetPage.IndexLimit, _lockerCabinetPage.MaxItems, condition);
            _pageFlip = false;
        }

        private void LockerPage(int cabinetId)
        {
            string condition = String.Format("cabinet_id = {0}", cabinetId);

            _lockerPage.FinalIndex = Convert.ToDouble(Locker.Count(condition));
            _lockerPage.LastPage = Convert.ToInt32(Math.Ceiling(_lockerPage.FinalIndex / _lockerPage.MaxItems));
            _lockerPage.PageSetting();

            if (_lockerPage.FinalIndex == 0)
                _lockerPage.PageReset();
            
            if (_lockerPage.PageNumber == _lockerPage.LastPage)
                _lockerPage.LastIndex = (int)_lockerPage.FinalIndex; 

            toolStripLabelPageNoLocker.Text = String.Format("Page {0} / {1}", _lockerPage.PageNumber, _lockerPage.LastPage);
            toolStripLabelResultLocker.Text = String.Format("Showing result {0}~{1}", _lockerPage.FirstIndex, _lockerPage.LastIndex);
            ReloadLockerList(_lockerPage.IndexLimit, _lockerPage.MaxItems, condition);
        }

        private void RentalPage()
        {
            string condition = "status <> 'Ended'";

            _rentalPage.FinalIndex = Convert.ToDouble(Rental.Count(condition));
            _rentalPage.LastPage = Convert.ToInt32(Math.Ceiling(_rentalPage.FinalIndex / _rentalPage.MaxItems));
            _rentalPage.PageSetting();

            if (_rentalPage.FinalIndex == 0)
                _rentalPage.PageReset();
            
            if (_rentalPage.PageNumber == _rentalPage.LastPage)
                _rentalPage.LastIndex = (int)_rentalPage.FinalIndex; 

            toolStripLabelPageNoRental.Text = String.Format("Page {0} / {1}", _rentalPage.PageNumber, _rentalPage.LastPage);
            toolStripLabelResultRental.Text = String.Format("Showing result {0}~{1}", _rentalPage.FirstIndex, _rentalPage.LastIndex);

            // Reload the Rental List and update the rental data in database
            ReloadRentalList(_rentalPage.IndexLimit, _rentalPage.MaxItems, condition);

            // Reload the Rental List and display the updated data (sync with database)
            ReloadRentalList(_rentalPage.IndexLimit, _rentalPage.MaxItems, condition);

        }

        private void RentalPage(string condition)
        {
            _rentalPage.FinalIndex = Convert.ToDouble(Rental.Count(condition));
            _rentalPage.LastPage = Convert.ToInt32(Math.Ceiling(_rentalPage.FinalIndex / _rentalPage.MaxItems));
            _rentalPage.PageSetting();

            if (_rentalPage.FinalIndex == 0)
                _rentalPage.PageReset();
            
            if (_rentalPage.PageNumber == _rentalPage.LastPage)
                _rentalPage.LastIndex = (int)_rentalPage.FinalIndex;

            toolStripLabelPageNoRental.Text = String.Format("Page {0} / {1}", _rentalPage.PageNumber, _rentalPage.LastPage);
            toolStripLabelResultRental.Text = String.Format("Showing result {0}~{1}", _rentalPage.FirstIndex, _rentalPage.LastIndex);
            
            // Reload the Rental List and update the rental data in database
            ReloadRentalList(_rentalPage.IndexLimit, _rentalPage.MaxItems, condition);

            // Reload the Rental List and display the updated data (sync with database)
            ReloadRentalList(_rentalPage.IndexLimit, _rentalPage.MaxItems, condition);
        }

        private void DeletedEmployeePage()
        {
            string condition = "status = 'Disabled'";
            _deletedEmployeePage.FinalIndex = Convert.ToDouble(Employee.Count(condition));
            _deletedEmployeePage.LastPage = Convert.ToInt32(Math.Ceiling(_deletedEmployeePage.FinalIndex / _deletedEmployeePage.MaxItems));
            _deletedEmployeePage.PageSetting();

            if (_deletedEmployeePage.FinalIndex == 0)
                _deletedEmployeePage.PageReset();
            
            if (_deletedEmployeePage.PageNumber == _deletedEmployeePage.LastPage)
                _deletedEmployeePage.LastIndex = (int)_deletedEmployeePage.FinalIndex;

            toolStripLabelPageNoDeletedEmployee.Text = String.Format("Page {0} / {1}", _deletedEmployeePage.PageNumber, _deletedEmployeePage.LastPage);
            toolStripLabelResultDeletedEmployee.Text = String.Format("Showing result {0}~{1}", _deletedEmployeePage.FirstIndex, _deletedEmployeePage.LastIndex);
            ReloadDeletedEmployeeList(_deletedEmployeePage.IndexLimit, _deletedEmployeePage.MaxItems, condition);
        }

        private void DeletedEmployeePage(string condition)
        {
            _deletedEmployeePage.FinalIndex = Convert.ToDouble(Employee.Count(condition));
            _deletedEmployeePage.LastPage = Convert.ToInt32(Math.Ceiling(_deletedEmployeePage.FinalIndex / _deletedEmployeePage.MaxItems));
            _deletedEmployeePage.PageSetting();

            if (_deletedEmployeePage.FinalIndex == 0)
                _deletedEmployeePage.PageReset();

            if (_deletedEmployeePage.PageNumber == _deletedEmployeePage.LastPage)
                _deletedEmployeePage.LastIndex = (int)_deletedEmployeePage.FinalIndex;

            toolStripLabelPageNoDeletedEmployee.Text = String.Format("Page {0} / {1}", _deletedEmployeePage.PageNumber, _deletedEmployeePage.LastPage);
            toolStripLabelResultDeletedEmployee.Text = String.Format("Showing result {0}~{1}", _deletedEmployeePage.FirstIndex, _deletedEmployeePage.LastIndex);
            ReloadDeletedEmployeeList(_deletedEmployeePage.IndexLimit, _deletedEmployeePage.MaxItems, condition);
        }

        private void DeletedCustomerPage()
        {
            string condition = "status = 'Disabled'";
            _deletedCustomerPage.FinalIndex = Customer.Count(condition);
            _deletedCustomerPage.LastPage = Convert.ToInt32(Math.Ceiling(_deletedCustomerPage.FinalIndex / _deletedCustomerPage.MaxItems));
            _deletedCustomerPage.PageSetting();

            if (_deletedCustomerPage.FinalIndex == 0)
                _deletedCustomerPage.PageReset();

            if (_deletedCustomerPage.PageNumber == _deletedCustomerPage.LastPage)
                _deletedCustomerPage.LastIndex = (int)_deletedCustomerPage.FinalIndex;

            toolStripLabelPageNoDeletedCustomer.Text = String.Format("Page {0} / {1}", _deletedCustomerPage.PageNumber, _deletedCustomerPage.LastPage);
            toolStripLabelResultDeletedCustomer.Text = String.Format("Showing result {0}~{1}", _deletedCustomerPage.FirstIndex, _deletedCustomerPage.LastIndex);
            ReloadDeletedCustomerList(_deletedCustomerPage.IndexLimit, _deletedCustomerPage.MaxItems, condition);
        }

        private void DeletedCustomerPage(string condition)
        {
            _deletedCustomerPage.FinalIndex = Customer.Count(condition);
            _deletedCustomerPage.LastPage = Convert.ToInt32(Math.Ceiling(_deletedCustomerPage.FinalIndex / _deletedCustomerPage.MaxItems));
            _deletedCustomerPage.PageSetting();

            if (_deletedCustomerPage.FinalIndex == 0)
                _deletedCustomerPage.PageReset();
            
            if (_deletedCustomerPage.PageNumber == _deletedCustomerPage.LastPage)
                _deletedCustomerPage.LastIndex = (int)_deletedCustomerPage.FinalIndex;

            toolStripLabelPageNoDeletedCustomer.Text = String.Format("Page {0} / {1}", _deletedCustomerPage.PageNumber, _deletedCustomerPage.LastPage);
            toolStripLabelResultDeletedCustomer.Text = String.Format("Showing result {0}~{1}", _deletedCustomerPage.FirstIndex, _deletedCustomerPage.LastIndex);
            ReloadDeletedCustomerList(_deletedCustomerPage.IndexLimit, _deletedCustomerPage.MaxItems, condition);
        }

        private void DeletedLockerTypePage()
        {
            string condition = "status = 'Disabled'";
            _deletedLockerTypePage.FinalIndex = Convert.ToDouble(LockerType.Count(condition));
            _deletedLockerTypePage.LastPage = Convert.ToInt32(Math.Ceiling(_deletedLockerTypePage.FinalIndex / _deletedLockerTypePage.MaxItems));
            _deletedLockerTypePage.PageSetting();
            if (_deletedLockerTypePage.FinalIndex == 0)
                _deletedLockerTypePage.PageReset();
            
            if (_deletedLockerTypePage.PageNumber == _deletedLockerTypePage.LastPage)
                _deletedLockerTypePage.LastIndex = (int)_deletedLockerTypePage.FinalIndex; 

            toolStripLabelPageNoDeletedLockerType.Text = String.Format("Page {0} / {1}", _deletedLockerTypePage.PageNumber, _deletedLockerTypePage.LastPage);
            ReloadDeletedLockerTypeList(_deletedLockerTypePage.IndexLimit, _deletedLockerTypePage.MaxItems, condition);
        }

        private void DeletedCabinetPage()
        {
            string condition = "status = 'Disabled'";
            _deletedCabinetPage.FinalIndex = Convert.ToDouble(Cabinet.Count(condition));
            _deletedCabinetPage.LastPage = Convert.ToInt32(Math.Ceiling(_deletedCabinetPage.FinalIndex / _deletedCabinetPage.MaxItems));
            _deletedCabinetPage.PageSetting();
            if (_deletedCabinetPage.FinalIndex == 0)
                _deletedCabinetPage.PageReset();

            if (_deletedCabinetPage.PageNumber == _deletedCabinetPage.LastPage)
                _deletedCabinetPage.LastIndex = (int)_deletedCabinetPage.FinalIndex;

            toolStripLabelPageNoDeletedCabinet.Text = String.Format("Page {0} / {1}", _deletedCabinetPage.PageNumber, _deletedCabinetPage.LastPage);
            toolStripLabelResultDeletedCabinet.Text = String.Format("Showing result {0}~{1}", _deletedCabinetPage.FirstIndex, _deletedCabinetPage.LastIndex);
            ReloadDeletedCabinetList(_deletedCabinetPage.IndexLimit, _deletedCabinetPage.MaxItems, condition);
        }

        private void DeletedCabinetPage(string condition)
        {
            var cab = new Cabinet();
            _deletedCabinetPage.FinalIndex = Convert.ToDouble(Cabinet.Count(condition));
            _deletedCabinetPage.LastPage = Convert.ToInt32(Math.Ceiling(_deletedCabinetPage.FinalIndex / _deletedCabinetPage.MaxItems));
            _deletedCabinetPage.PageSetting();
            if (_deletedCabinetPage.FinalIndex == 0)
                _deletedCabinetPage.PageReset();

            if (_deletedCabinetPage.PageNumber == _deletedCabinetPage.LastPage)
                _deletedCabinetPage.LastIndex = (int)_deletedCabinetPage.FinalIndex;

            toolStripLabelPageNoDeletedCabinet.Text = String.Format("Page {0} / {1}", _deletedCabinetPage.PageNumber, _deletedCabinetPage.LastPage);
            toolStripLabelResultDeletedCabinet.Text = String.Format("Showing result {0}~{1}", _deletedCabinetPage.FirstIndex, _deletedCabinetPage.LastIndex);
            ReloadDeletedCabinetList(_deletedCabinetPage.IndexLimit, _deletedCabinetPage.MaxItems, condition);
        }

        private void EndedRentalPage()
        {
            string condition = "status = 'Ended'";

            _endedRentalPage.FinalIndex = Convert.ToDouble(Rental.Count(condition));
            _endedRentalPage.LastPage = Convert.ToInt32(Math.Ceiling(_endedRentalPage.FinalIndex / _endedRentalPage.MaxItems));
            _endedRentalPage.PageSetting();

            if (_endedRentalPage.FinalIndex == 0)
                _endedRentalPage.PageReset();

            if (_endedRentalPage.PageNumber == _endedRentalPage.LastPage)
                _endedRentalPage.LastIndex = (int)_endedRentalPage.FinalIndex;

            toolStripLabelPageNoEndedRental.Text = String.Format("Page {0} / {1}", _endedRentalPage.PageNumber, _endedRentalPage.LastPage);
            toolStripLabelResultEndedRental.Text = String.Format("Showing result {0}~{1}", _endedRentalPage.FirstIndex, _endedRentalPage.LastIndex);

            ReloadEndedRentalList(_rentalPage.IndexLimit, _rentalPage.MaxItems, condition);
        }

        private void EndedRentalPage(string condition)
        {
            _endedRentalPage.FinalIndex = Convert.ToDouble(Rental.Count(condition));
            _endedRentalPage.LastPage = Convert.ToInt32(Math.Ceiling(_endedRentalPage.FinalIndex / _endedRentalPage.MaxItems));
            _endedRentalPage.PageSetting();

            if (_endedRentalPage.FinalIndex == 0)
                _endedRentalPage.PageReset();

            if (_endedRentalPage.PageNumber == _endedRentalPage.LastPage)
                _endedRentalPage.LastIndex = (int)_endedRentalPage.FinalIndex;

            toolStripLabelPageNoEndedRental.Text = String.Format("Page {0} / {1}", _endedRentalPage.PageNumber, _endedRentalPage.LastPage);
            toolStripLabelResultEndedRental.Text = String.Format("Showing result {0}~{1}", _endedRentalPage.FirstIndex, _endedRentalPage.LastIndex);

            ReloadEndedRentalList(_endedRentalPage.IndexLimit, _endedRentalPage.MaxItems, condition);

        }

        /*
         * Event Handlers
         */
        // Main Form Loading Event Handler
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Controls.Remove(tabControlMainForm);
            this.Controls.Add(panelMainBasePanel);
            panelMainCenter.Controls.Remove(tabControlMainPanel);
            panelMainCenter.Controls.Add(panelRentalBasePanel);

            // Set the Employee Name and Username in the welcome message
            labelLoginUser.Text = String.Format("{0} ({1})", _employee.Name, _employee.Username);

            _search = false;
            RentalPage();

            //Clear combo box  & locker type dictonary to avoid error
            _lockerTypeDictonary.Clear();

            //Load Locker Type Name into Combo Box 1
            _lockerTypes = LockerType.Where("status <> 'Disabled'", 0, 2147483467);

            // Add default value (select ALL) into locker type dictonary
            _lockerTypeDictonary.Add(0, "All");

            // Add locker types into locker type dictonary
            foreach (LockerType lockerType in _lockerTypes)
            { _lockerTypeDictonary.Add(lockerType.Id, lockerType.Name); }

            // Bind locker type dictonary onto combo box locker type locker cabinet (In Locker Module)
            comboBoxLockerTypeLockerCabinet.DataSource = new BindingSource(_lockerTypeDictonary, null);

            // Display the Locker Type Name and Set the Locker Type Id as ValueMember
            comboBoxLockerTypeLockerCabinet.DisplayMember = "Value";
            comboBoxLockerTypeLockerCabinet.ValueMember = "Key";

            // Set default select all cabinet (ignore status)
            radioButtonAllLockerCabinet.Checked = true;

            // manually trigger the LockerCabinet table update
            _lockerCabinetPage.PageNumber = 1;
            LockerCabinetPage();
        }

        /*
         * Big Button Event Handlers
         */
        // Main Panel Module
        private void ButtonAdminPanel_Click(object sender, EventArgs e)
        {
            if (_employee.IsAdmin())
            {
                // Add Admin Base Panel
                Controls.Add(panelAdminBase);

                // Remove Main Panel
                Controls.Remove(panelMainBasePanel);

                // Remove the Admin Tab Control and Add the Employee Base Panel
                panelAdminCenter.Controls.Remove(tabControlAdminPanel);
                panelAdminCenter.Controls.Add(panelEmployeeBase);

                // Reset Employee page to Page 1
                _search = false;
                _searchCondition = "";

                EmployeePage();

                // Reload all tables in deleted records that will be changed in Main Panel
                DeletedCustomerPage();
                EndedRentalPage();
            }
            else
            {
                MessageBox.Show("Access Error: Acccess Denied. " + Environment.NewLine +
                    "Only administrators can access the admin panel.", "Access Denied", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ButtonChangePassword_Click(object sender, EventArgs e)
        {
            ChangePasswordForm changePasswordForm = new ChangePasswordForm(false, _employee);
            changePasswordForm.ShowDialog();

            if (changePasswordForm.IsPasswordChanged)
            {
                MessageBox.Show("Password Changed Successfully." +
                    "\nYou need to login again. ", "Password Changed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
        }

        private void ButtonViewMasterKey_Click(object sender, EventArgs e)
        {
            MasterKeyForm viewMasterKeyForm = new MasterKeyForm(_employee);
            viewMasterKeyForm.ShowDialog();
        }

        private void ButtonLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Do you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                LoginForm LoginForm = new LoginForm();
                LoginForm.Show();
                this.Close();
            }
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Admin Panel Module
        private void ButtonMainPanel_Click(object sender, EventArgs e)
        {
            Controls.Add(panelMainBasePanel);
            Controls.Remove(panelAdminBase);
            _search = false;

            // Reload all tables in Main Panel that will be affected by Admin Panel
            CustomerPage();
            RentalPage();

            // Clear combo box  & locker type dictonary to avoid error
            _lockerTypeDictonary.Clear();

            // Get Locker Type data from Database
            _lockerTypes = LockerType.Where("status <> 'Disabled'", 0, 2147483467);

            // Add default value (select ALL) into locker type dictonary
            _lockerTypeDictonary.Add(0, "All");

            // Add locker types into locker type dictonary
            foreach (LockerType lockerType in _lockerTypes)
            { _lockerTypeDictonary.Add(lockerType.Id, lockerType.Name); }

            // Bind locker type dictonary onto combo box locker type locker cabinet (In Locker Module)
            comboBoxLockerTypeLockerCabinet.DataSource = new BindingSource(_lockerTypeDictonary, null);

            // Display the Locker Type Name and Set the Locker Type Id as ValueMember
            comboBoxLockerTypeLockerCabinet.DisplayMember = "Value";
            comboBoxLockerTypeLockerCabinet.ValueMember = "Key";

            // manually trigger the LockerCabinet table update
            _lockerCabinetPage.PageNumber = 1;
            _pageFlip = true;
            LockerCabinetPage();
        }
    
        // Employee Module        
        private void ButtonEmployee_Click(object sender, EventArgs e)
        {
            // Remove all panels that are not Employee Panel
            panelAdminCenter.Controls.Remove(panelCabinetBasePanel);
            panelAdminCenter.Controls.Remove(panelDeletedRecordsBasePanel);

            // Add Employee Base Panel
            panelAdminCenter.Controls.Add(panelEmployeeBase);

            //Reset Employee page to Page 1
            _search = false;
            _employeePage.PageReset();
            EmployeePage();
        }

        private void ButtonAddEmployee_Click(object sender, EventArgs e)
        {
            var addNewEmployeeForm = new EmployeeForm(false);
            addNewEmployeeForm.ShowDialog();
            if (!addNewEmployeeForm.IsInsertComplete())
                return;
            else
            {
                if (!_search)
                {
                    _employeePage.FinalIndex = Convert.ToDouble(Employee.Count("status <> 'Disabled'"));
                    _employeePage.LastPage = Convert.ToInt32(Math.Ceiling(_employeePage.FinalIndex / _employeePage.MaxItems));
                    _employeePage.PageNumber = _employeePage.LastPage;
                    EmployeePage();
                }
                else
                {
                    _employeePage.FinalIndex = Convert.ToDouble(Employee.Count(_searchCondition));
                    _employeePage.LastPage = Convert.ToInt32(Math.Ceiling(_employeePage.FinalIndex / _employeePage.MaxItems));
                    _employeePage.PageNumber = _employeePage.LastPage;
                    EmployeePage(_searchCondition);
                }
            }
        }

        private void ButtonViewEmployee_Click(object sender, EventArgs e)
        {
            if (listViewEmployee.SelectedItems.Count <= 0)
                return;

            ListViewItem lvi = listViewEmployee.SelectedItems[0];
            int id = Convert.ToInt32(lvi.Text);
            var ViewEmployeeFrom = new EmployeeForm(id);
            ViewEmployeeFrom.ShowDialog();
            if (!_search)
                EmployeePage();
            else
                EmployeePage(_searchCondition);
        }

        private void ButtonDeleteEmployee_Click(object sender, EventArgs e)
        {
            if (listViewEmployee.SelectedItems.Count <= 0)
                return;

            var result = MessageBox.Show("Do you want to delete this employee?", "Delete Employee", MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                ListViewItem lvi = listViewEmployee.SelectedItems[0];
                int id = Convert.ToInt32(lvi.Text);

                try
                {
                    EmployeeController employeeController = new EmployeeController();
                    employeeController.DeleteEmployeeData(id);

                    if (!_search)
                        EmployeePage();
                    else
                        EmployeePage(_searchCondition);
                }
                catch (InvalidUserInputException exception)
                {
                    exception.ShowErrorMessage();
                }
            }           
        }

        // Customer Module
        private void ButtonCustomer_Click(object sender, EventArgs e)
        {
            panelMainCenter.Controls.Add(panelCustomerBasePanel);       //Customer
            panelMainCenter.Controls.Remove(panelLockerBasePanel);    //Rental
            panelMainCenter.Controls.Remove(panelRentalBasePanel);    //Locker
            
            // Reset Customer page to page 1
            _search = false;
            _customerPage.PageReset();
            CustomerPage();
        }

        private void ButtonAddCustomer_Click(object sender, EventArgs e)
        {
            CustomerForm addCustomerForm = new CustomerForm();
            addCustomerForm.ShowDialog();

            if (!addCustomerForm.IsInsertComplete())
                return;

            if (!_search)
            {
                _customerPage.FinalIndex = Convert.ToDouble(Customer.Count("status <> 'Disabled'"));
                _customerPage.LastPage = Convert.ToInt32(Math.Ceiling(_customerPage.FinalIndex / _customerPage.MaxItems));
                _customerPage.PageNumber = _customerPage.LastPage;
                CustomerPage();
            }
            else
            {
                _customerPage.FinalIndex = Convert.ToDouble(Customer.Count(_searchCondition));
                _customerPage.LastPage = Convert.ToInt32(Math.Ceiling(_customerPage.FinalIndex / _customerPage.MaxItems));
                _customerPage.PageNumber = _customerPage.LastPage;
                CustomerPage(_searchCondition);
            }
        }

        private void ButtonViewCustomer_Click(object sender, EventArgs e)
        {
            if (listViewCustomer.SelectedItems.Count <= 0)
                return;

            ListViewItem lvi = listViewCustomer.SelectedItems[0];
            int id = Convert.ToInt32(lvi.Text);
            CustomerForm viewCustomerFrom = new CustomerForm(id);
            viewCustomerFrom.ShowDialog();
            if (!_search)
                CustomerPage();
            else
                CustomerPage(_searchCondition);

        }

        private void ButtonDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (listViewCustomer.SelectedItems.Count <= 0)
                return;
            var result = MessageBox.Show("Do you want to delete this customer?", "Delete Customer", MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                ListViewItem lvi = listViewCustomer.SelectedItems[0];
                int id = Convert.ToInt32(lvi.Text);

                try
                {
                    CustomerController customerController= new CustomerController();
                    customerController.DeleteCustomerData(id);

                    if (!_search)
                        CustomerPage();
                    else
                        CustomerPage(_searchCondition);
                }
                catch (InvalidUserInputException exception)
                {
                    exception.ShowErrorMessage();
                }
            }
        }

        // Locker Type Module
        private void ButtonAddLockerType_Click(object sender, EventArgs e)
        {
            LockerTypeForm addLockerTypeForm = new LockerTypeForm();
            addLockerTypeForm.ShowDialog();

            if (!addLockerTypeForm.IsInsertComplete())
                return;

            _lockerTypePage.FinalIndex = Convert.ToDouble(LockerType.Count("status <> 'Disabled'"));
            _lockerTypePage.LastPage = Convert.ToInt32(Math.Ceiling(_lockerTypePage.FinalIndex / _lockerTypePage.MaxItems));
            _lockerTypePage.PageNumber = _lockerTypePage.LastPage;

            LockerTypePage();
        }

        private void ButtonEditLockerType_Click(object sender, EventArgs e)
        {
            if (listViewLockerType.SelectedItems.Count <= 0)
                return;

            ListViewItem lvi = listViewLockerType.SelectedItems[0];
            int id = Convert.ToInt32(lvi.Text);

            LockerTypeForm editRateForm = new LockerTypeForm(id);
            editRateForm.ShowDialog();
            LockerTypePage();
        }

        private void ButtonDeleteLockerType_Click(object sender, EventArgs e)
        {
            if (listViewLockerType.SelectedItems.Count <= 0)
                return;

            var result = MessageBox.Show("Do you want to delete this locker type?", "Delete Locker Type",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                ListViewItem lvi = listViewLockerType.SelectedItems[0];
                int id = Convert.ToInt32(lvi.Text);

                try
                {
                    LockerTypeController lockerTypeController = new LockerTypeController();
                    lockerTypeController.DeleteLockerTypeData(id);

                    // Reload the Locker Type List in Cabinet Module
                    LockerTypePage();
                }
                catch (InvalidUserInputException exception)
                {
                    exception.ShowErrorMessage();
                }
            }
        }

        // Cabinet Module
        private void ButtonCabinet_Click(object sender, EventArgs e)
        {
            panelAdminCenter.Controls.Remove(panelEmployeeBase);
            panelAdminCenter.Controls.Add(panelCabinetBasePanel);
            panelAdminCenter.Controls.Remove(panelDeletedRecordsBasePanel);

            _search = false;
            LockerTypePage();
            _cabinetPage.PageReset();
            CabinetPage();
        }

        private void ButtonAddCabinet_Click(object sender, EventArgs e)
        {
            CabinetForm addCabinetForm = new CabinetForm();
            addCabinetForm.ShowDialog();

            if (!addCabinetForm.IsInsertComplete())
                return;

            // Reload the Locker Type List
            LockerTypePage();

            // Reload the Cabinet List in Cabinet Module
            if (!_search)
            {
                _cabinetPage.FinalIndex = Convert.ToDouble(Cabinet.Count("status <> 'Disabled'"));
                _cabinetPage.LastPage = Convert.ToInt32(Math.Ceiling(_cabinetPage.FinalIndex / _cabinetPage.MaxItems));
                _cabinetPage.PageNumber = _cabinetPage.LastPage;
                CabinetPage();
            }
            else
            {
                _cabinetPage.FinalIndex = Convert.ToDouble(Cabinet.Count(_searchCondition));
                _cabinetPage.LastPage = Convert.ToInt32(Math.Ceiling(_cabinetPage.FinalIndex / _cabinetPage.MaxItems));
                _cabinetPage.PageNumber = _cabinetPage.LastPage;
                CabinetPage(_searchCondition);
            }

            // Reload the Cabinet List in Locker Module
            /*
            _pageFlip = true;
            SmallCabinetPage();
            */

        }

        private void ButtonDeleteCabinet_Click(object sender, EventArgs e)
        {
            
            if (listViewCabinet.SelectedItems.Count <= 0)
                return;

            var result = MessageBox.Show("Do you want to delete this cabinet?", "Delete Cabinet",
               MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                ListViewItem lvi = listViewCabinet.SelectedItems[0];
                int id = Convert.ToInt32(lvi.Text);

                try
                {
                    CabinetLockerController cabinetController = new CabinetLockerController();
                    cabinetController.DeleteCabinetData(id);

                    // Reload the Locker Type list
                    LockerTypePage();

                    // Reload the Cabinet List in Cabinet Module
                    if (!_search)
                        CabinetPage();
                    else
                        CabinetPage(_searchCondition);
                }
                catch (InvalidUserInputException exception)
                {
                    exception.ShowErrorMessage();
                }
            }
        }

        private void ButtonFilterCabinet_Click(object sender, EventArgs e)
        {
            CabinetForm filterCabinetForm = new CabinetForm(false);
            filterCabinetForm.ShowDialog();

            if (!String.IsNullOrWhiteSpace(filterCabinetForm.Condition))
            {
                _searchCondition = filterCabinetForm.Condition;
                _search = true;
                _cabinetPage.PageNumber = 1;
                CabinetPage(_searchCondition);
            }
        }

        // Locker Module
        private void ButtonLocker_Click(object sender, EventArgs e)
        {
            panelMainCenter.Controls.Remove(panelRentalBasePanel);
            panelMainCenter.Controls.Remove(panelCustomerBasePanel);
            panelMainCenter.Controls.Add(panelLockerBasePanel);

            _search = false;

            //Clear combo box  & locker type dictonary to avoid error
            _lockerTypeDictonary.Clear();

            //Load Locker Type Name into Combo Box 1
            _lockerTypes = LockerType.Where("status <> 'Disabled'", 0, 2147483467);

            // Add default value (select ALL) into locker type dictonary
            _lockerTypeDictonary.Add(0, "All");

            // Add locker types into locker type dictonary
            foreach (LockerType lockerType in _lockerTypes)
            { _lockerTypeDictonary.Add(lockerType.Id, lockerType.Name); }

            // Bind locker type dictonary onto combo box locker type locker cabinet (In Locker Module)
            comboBoxLockerTypeLockerCabinet.DataSource = new BindingSource(_lockerTypeDictonary, null);

            // Display the Locker Type Name and Set the Locker Type Id as ValueMember
            comboBoxLockerTypeLockerCabinet.DisplayMember = "Value";
            comboBoxLockerTypeLockerCabinet.ValueMember = "Key";

            // Default select all cabinets (ignore cabinet condition)
            radioButtonAllLockerCabinet.Checked = true;

            // manually trigger the LockerCabinet table update
            _lockerCabinetPage.PageNumber = 1;
            LockerCabinetPage();

            // Default select the first cabinet to load
            List<Cabinet> cabinets = Cabinet.Where("status <> 'Disabled'", 0, 1);

            //If no cabinet in list, return
            if (!cabinets.Any()) 
                return;

            _cabinetId = cabinets[0].Id;
            textBoxCabinetCode.Text = cabinets[0].Code;

            _lockerPage.PageNumber = 1;
            LockerPage(_cabinetId);
        }

        private void ButtonSelectCabinet_Click(object sender, EventArgs e)
        {
            if (listViewLockerCabinet.SelectedItems.Count <= 0)
                return;

            ListViewItem lvi = listViewLockerCabinet.SelectedItems[0];
            _cabinetId = Convert.ToInt32(lvi.Text);
            textBoxCabinetCode.Text = lvi.SubItems[1].Text;

            _lockerPage.PageNumber = 1;
            LockerPage(_cabinetId);
        }

        private void ButtonDisableLocker_Click(object sender, EventArgs e)
        {
            if (listViewLocker.SelectedItems.Count <= 0)
                return;

            var result = MessageBox.Show("Do you want to disable this locker?", "Disable Locker",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                ListViewItem lvi = listViewLocker.SelectedItems[0];
                string lockerCode = lvi.Text;

                try
                {
                    CabinetLockerController lockerController = new CabinetLockerController();
                    lockerController.DisableLocker(lockerCode);

                    _pageFlip = true;
                    LockerCabinetPage();
                    LockerPage(_cabinetId);
                }
                catch (InvalidUserInputException exception)
                {
                    exception.ShowErrorMessage();
                }
            }
        }

        private void ButtonResetLocker_Click(object sender, EventArgs e)
        {
            if (listViewLocker.SelectedItems.Count <= 0)
                return;

            if (!_employee.IsAdmin())
            {
                MessageBox.Show("Access Error: Acccess Denied. " + Environment.NewLine +
                    "Only administrators can reset the locker status.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                var result = MessageBox.Show("Do you want to reset this locker?", "Reset Locker",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    ListViewItem lvi = listViewLocker.SelectedItems[0];
                    string lockerCode = lvi.Text;

                    try
                    {
                        CabinetLockerController lockerController = new CabinetLockerController();
                        lockerController.ResetLocker(lockerCode);

                        _pageFlip = true;
                        LockerCabinetPage();
                        LockerPage(_cabinetId);
                    }
                    catch (InvalidUserInputException exception)
                    {
                        exception.ShowErrorMessage();
                    }
                }
            }
        }

        private void ComboBoxLockerTypeLockerCabinet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxLockerTypeLockerCabinet.SelectedIndex < 0)
                return;

            int lockerTypeId = Int32.Parse(comboBoxLockerTypeLockerCabinet.SelectedValue.ToString());
            if (lockerTypeId == 0)
                _cabinetSize = "IS NOT NULL";
            else
                _cabinetSize = String.Format("= {0}", lockerTypeId);
            
            LockerCabinetPage();
        }

        private void RadioButtonAllLockerCabinet_CheckedChanged(object sender, EventArgs e)
        {
            _cabinetStatus = "<> 'Disabled'";
            _lockerCabinetPage.PageNumber = 1;
            LockerCabinetPage();
        }

        private void RadioButtonAvailableLockerCabinet_CheckedChanged(object sender, EventArgs e)
        {
            _cabinetStatus = "= 'Available'";
            _lockerCabinetPage.PageNumber = 1;
            LockerCabinetPage();
        }

        private void RadioButtonFullLockerCabinet_CheckedChanged(object sender, EventArgs e)
        {
            _cabinetStatus = "= 'Full'";
            _lockerCabinetPage.PageNumber = 1;
            LockerCabinetPage();
        }

        // Rental Module
        private void ButtonRental_Click(object sender, EventArgs e)
        {
            panelMainCenter.Controls.Add(panelRentalBasePanel);
            panelMainCenter.Controls.Remove(panelCustomerBasePanel);
            panelMainCenter.Controls.Remove(panelLockerBasePanel);

            _search = false;
            _rentalPage.PageReset();
            RentalPage();
        }

        private void ButtonAddRental_Click(object sender, EventArgs e)
        {
            RentalForm addRentalForm = new RentalForm(_employee);
            addRentalForm.ShowDialog();

            if (!addRentalForm.IsInsertComplete())
                return;
            else
            {
                if (!_search)
                {
                    _rentalPage.FinalIndex = Convert.ToDouble(Rental.Count("status <> 'Ended'"));
                    _rentalPage.LastPage = Convert.ToInt32(Math.Ceiling(_rentalPage.FinalIndex / _rentalPage.MaxItems));
                    _rentalPage.PageNumber = _rentalPage.LastPage;
                    RentalPage();
                }
                else
                {
                    _rentalPage.FinalIndex = Convert.ToDouble(Rental.Count(_searchCondition));
                    _rentalPage.LastPage = Convert.ToInt32(Math.Ceiling(_rentalPage.FinalIndex / _rentalPage.MaxItems));
                    _rentalPage.PageNumber = _rentalPage.LastPage;
                    RentalPage(_searchCondition);
                }
            }

        }

        private void ButtonViewRental_Click(object sender, EventArgs e)
        {
            if (listViewRental.SelectedItems.Count <= 0)
                return;

            ListViewItem lvi = listViewRental.SelectedItems[0];
            int id = Convert.ToInt32(lvi.Text);

            RentalForm viewRentalForm = new RentalForm(id, false);
            viewRentalForm.ShowDialog();

            if (viewRentalForm.IsInsertComplete())
            {
                if (!_search)
                    RentalPage();
                else
                    RentalPage(_searchCondition);
            }
        }

        private void ButtonEndRental_Click(object sender, EventArgs e)
        {
            if (listViewRental.SelectedItems.Count <= 0)
                return;

            ListViewItem lvi = listViewRental.SelectedItems[0];
            int id = Convert.ToInt32(lvi.Text);

            Rental rental = Rental.Get(id);

            if (rental.IsNotStarted())
            {
                MessageBox.Show("Error: Rental Not Started." +
                    "\nYou cannot end a rental that is not started.", "Rental Not Started", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RentalForm endRentalForm = new RentalForm(rental);
            endRentalForm.ShowDialog();

            if (endRentalForm.IsInsertComplete())
            {
                if (!_search)
                    RentalPage();
                else
                    RentalPage(_searchCondition);
            }
        }

        // Deleted Records
        private void ButtonDeletedRecords_Click(object sender, EventArgs e)
        {
            panelAdminCenter.Controls.Remove(panelEmployeeBase);
            panelAdminCenter.Controls.Remove(panelCabinetBasePanel);
            panelAdminCenter.Controls.Add(panelDeletedRecordsBasePanel);

            // Reset all page number in deleted records
            _deletedEmployeePage.PageReset();
            _deletedCustomerPage.PageReset();
            _deletedLockerTypePage.PageReset();
            _deletedCabinetPage.PageReset();
            _endedRentalPage.PageReset();

            // Reload all tables in deleted records
            _search = false;
            DeletedEmployeePage();
            DeletedCustomerPage();
            DeletedLockerTypePage();
            DeletedCabinetPage();
            EndedRentalPage();
        }

        // Deleted Employee
        private void ButtonRestoreEmployee_Click(object sender, EventArgs e)
        {
            if (listViewDeletedEmployee.SelectedItems.Count <= 0)
                return;

            var result = MessageBox.Show("Do you want to restore this employee?", "Restore Employee", MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                ListViewItem lvi = listViewDeletedEmployee.SelectedItems[0];
                int id = Convert.ToInt32(lvi.Text);

                EmployeeController employeeController = new EmployeeController();
                employeeController.RestoreEmployeeData(id);

                if (!_search)
                    DeletedEmployeePage();
                else
                    DeletedEmployeePage(_searchCondition);
            }
        }

        private void ButtonExportSelectedEmployee_Click(object sender, EventArgs e)
        {
            if (listViewDeletedEmployee.SelectedItems.Count <= 0)
                return;

            var result = MessageBox.Show("Do you want to export this employee from the database?\n" + Environment.NewLine +
                "Note: Exported employee will be deleted from the database.",
                "Export Deleted Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {

                ListViewItem lvi = listViewDeletedEmployee.SelectedItems[0];
                int id = Convert.ToInt32(lvi.Text);

                try
                {
                    EmployeeController employeeController = new EmployeeController();
                    employeeController.ExportEmployeeData(id);

                    //Reload Deleted Employee List
                    if (!_search)
                        DeletedEmployeePage();
                    else
                        DeletedEmployeePage(_searchCondition);
                }
                catch (InvalidUserInputException exception)
                {
                    exception.ShowErrorMessage();
                }
            }
        }

        private void ButtonExportAllEmployee_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Do you want to export all deleted employee from the database?" + Environment.NewLine +
                "Note: All exported employee will be deleted from the database.",
                "Export All Deleted Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    EmployeeController employeeController = new EmployeeController();
                    employeeController.ExportEmployeeData(0);

                    //Reload Deleted Employee List
                    if (!_search)
                        DeletedEmployeePage();
                    else
                        DeletedEmployeePage(_searchCondition);
                }
                catch (InvalidUserInputException exception)
                {
                    exception.ShowErrorMessage();
                }
            }
        }

        // Deleted Customer
        private void ButtonRestoreCustomer_Click(object sender, EventArgs e)
        {
            if (listViewDeletedCustomer.SelectedItems.Count <= 0)
                return;

            var result = MessageBox.Show("Do you want to restore this customer?", "Restore Customer", MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                ListViewItem lvi = listViewDeletedCustomer.SelectedItems[0];
                int id = Convert.ToInt32(lvi.Text);

                CustomerController customerController = new CustomerController();
                customerController.RestoreCustomerData(id);

                if (!_search)
                    DeletedCustomerPage();
                else
                    DeletedCustomerPage(_searchCondition);
            }
        }

        private void ButtonExportSelectedCustomer_Click(object sender, EventArgs e)
        {
            if (listViewDeletedCustomer.SelectedItems.Count <= 0)
                return;
            var result = MessageBox.Show("Do you want to export this customer from the database?\n" + Environment.NewLine +
                "Note: Exported customer will be deleted from the database.",
                "Export Deleted Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                ListViewItem lvi = listViewDeletedCustomer.SelectedItems[0];
                int id = Convert.ToInt32(lvi.Text);

                try
                {
                    CustomerController customerController = new CustomerController();
                    customerController.ExportCustomerData(id);

                    //Reload Deleted Customer List
                    if (!_search)
                        DeletedCustomerPage();
                    else
                        DeletedCustomerPage(_searchCondition);

                }
                catch (InvalidUserInputException exception)
                {
                    exception.ShowErrorMessage();
                }
            }
        }

        private void ButtonExportAllCustomer_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Do you want to export all deleted customer from the database?\n" + Environment.NewLine +
                "Note: All exported customer will be deleted from the database.",
                "Export All Deleted Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    CustomerController customerController = new CustomerController();
                    customerController.ExportCustomerData(0);

                    //Reload Deleted Customer List
                    if (!_search)
                        DeletedCustomerPage();
                    else
                        DeletedCustomerPage(_searchCondition);
                }
                catch (InvalidUserInputException exception)
                {
                    exception.ShowErrorMessage();
                }
            }
        }

        // Deleted Cabinet
        private void ButtonRestoreLockerType_Click(object sender, EventArgs e)
        {
            if (listViewDeletedLockerType.SelectedItems.Count <= 0)
                return;

            var result = MessageBox.Show("Do you want to restore this locker type?", "Restore Locker Type", MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                ListViewItem lvi = listViewDeletedLockerType.SelectedItems[0];
                int id = Convert.ToInt32(lvi.Text);

                LockerTypeController lockerTypeController = new LockerTypeController();
                lockerTypeController.RestoreLockerTypeData(id);

                DeletedLockerTypePage();
            }
        }

        private void ButtonExportSelectedLockerType_Click(object sender, EventArgs e)
        {
            if (listViewDeletedLockerType.SelectedItems.Count <= 0)
                return;

            var result = MessageBox.Show("Do you want to export this locker type from the database?\n" + Environment.NewLine +
                "Note: " + Environment.NewLine + "1. Exported locker type will be deleted from the database." + Environment.NewLine +
                "2. The locker type cannot be exported if existing cabinets for this locker type was not exported from the database.",
                "Export Deleted Locker Type", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                ListViewItem lvi = listViewDeletedLockerType.SelectedItems[0];
                int id = Convert.ToInt32(lvi.Text);

                try
                {
                    LockerTypeController lockerTypeController = new LockerTypeController();
                    lockerTypeController.ExportLockerTypeData(id);

                    DeletedLockerTypePage();
                }
                catch (InvalidUserInputException exception)
                {
                    exception.ShowErrorMessage();
                }
            }
        }

        private void ButtonRestoreCabinet_Click(object sender, EventArgs e)
        {
            if (listViewDeletedCabinet.SelectedItems.Count <= 0)
                return;

            var result = MessageBox.Show("Do you want to restore this cabinet?", "Restore Cabinet", MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                ListViewItem lvi = listViewDeletedCabinet.SelectedItems[0];
                int id = Convert.ToInt32(lvi.Text);

                try
                {
                    CabinetLockerController cabinetController = new CabinetLockerController();
                    cabinetController.RestoreCabinetData(id);

                    if (!_search)
                        DeletedCabinetPage();
                    else
                        DeletedCabinetPage(_searchCondition);
                }
                catch (InvalidUserInputException exception)
                {
                    exception.ShowErrorMessage();
                }
            }
        }

        private void ButtonFilterDeletedCabinet_Click(object sender, EventArgs e)
        {
            CabinetForm filterDeletedCabinetForm = new CabinetForm(true);
            filterDeletedCabinetForm.ShowDialog();

            if (!String.IsNullOrWhiteSpace(filterDeletedCabinetForm.Condition))
            {
                _searchCondition = filterDeletedCabinetForm.Condition;
                _search = true;
                _deletedCabinetPage.PageNumber = 1;
                DeletedCabinetPage(_searchCondition);
            }
        }

        private void ButtonExportSelectedCabinet_Click(object sender, EventArgs e)
        {
            if (listViewDeletedCabinet.SelectedItems.Count <= 0)
                return;

            var result = MessageBox.Show("Do you want to export this cabinet from the database?\n" + Environment.NewLine +
                "Note:\n" + "1. The assoicated lockers for this cabinet will also be exported.\n" +
                "2. Exported cabinets and lockers will be deleted from the database.",
                "Export Deleted Cabinet", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {

                ListViewItem lvi = listViewDeletedCabinet.SelectedItems[0];
                int id = Convert.ToInt32(lvi.Text);

                try
                {
                    CabinetLockerController cabinetController = new CabinetLockerController();
                    cabinetController.ExportCabinetData(id);

                    if (!_search)
                        DeletedCabinetPage();
                    else
                        DeletedCabinetPage(_searchCondition);
                }
                catch (InvalidUserInputException exception)
                {
                    exception.ShowErrorMessage();
                }
            }
        }

        // Ended Rental
        private void ButtonViewEndedRental_Click(object sender, EventArgs e)
        {
            if (listViewEndedRental.SelectedItems.Count <= 0)
                return;

            ListViewItem lvi = listViewEndedRental.SelectedItems[0];
            int id = Convert.ToInt32(lvi.Text);

            RentalForm viewEndedRentalForm = new RentalForm(id, true);
            viewEndedRentalForm.ShowDialog();
        }

        private void ButtonExportEndedRentals_Click(object sender, EventArgs e)
        {
            var exportRentalForm = new RentalForm(true);
            exportRentalForm.ShowDialog();

            if (exportRentalForm.IsInsertComplete())
            {
                if (!_search)
                    EndedRentalPage();
                else
                    EndedRentalPage(_searchCondition);
            }
        }

        /*
         * ToolStrip Button Event Handlers
         */
        // Employee Module
        private void ToolStripButtonSearchEmployee_Click(object sender, EventArgs e)
        {
            string item, searchValue;

            if (toolStripComboBoxColumnEmployee.Text == "IC Number")
            { item = "ic_passport"; }
            else if (toolStripComboBoxColumnEmployee.Text == "Name")
            { item = "name"; }
            else if (toolStripComboBoxColumnEmployee.Text == "Username")
            { item = "username"; }
            else
            { return; }

            if (toolStripComboBoxStringEmployee.Text == "Start with")
            { searchValue = "'{0}%'"; }
            else if (toolStripComboBoxStringEmployee.Text == "End with")
            { searchValue = "'%{0}'"; }
            else if (toolStripComboBoxStringEmployee.Text == "Contains")
            { searchValue = "'%{0}%'"; }
            else
            { return; }

            if (string.IsNullOrWhiteSpace(toolStripTextBoxEmployee.Text))
            { return; }

            searchValue = String.Format(searchValue, toolStripTextBoxEmployee.Text);

            _searchCondition = "{0} LIKE {1} AND status <> 'Disabled'";
            _searchCondition = String.Format(_searchCondition, item, searchValue);

            _search = true;
            _employeePage.PageNumber = 1;
            EmployeePage(_searchCondition);
        }

        private void ToolStripButtonResetEmployee_Click(object sender, EventArgs e)
        {
            _search = false;
            _searchCondition = "";
            toolStripComboBoxColumnEmployee.SelectedIndex = -1;
            toolStripComboBoxStringEmployee.SelectedIndex = -1;
            toolStripTextBoxEmployee.Text = "";
            _employeePage.PageNumber = 1;
            EmployeePage();
        }

        private void ToolStripButtonFirstPageEmployee_Click(object sender, EventArgs e)
        {
            if (_employeePage.PageNumber == 1)
                return;
            else
            {
                _employeePage.PageNumber = 1;
                if (!_search)
                    EmployeePage();
                else
                    EmployeePage(_searchCondition);
            }
        }

        private void ToolStripButtonPreviousPageEmployee_Click(object sender, EventArgs e)
        {
            if (_employeePage.PageNumber == 1)
                return;
            else
            {
                _employeePage.PageNumber -= 1;
                if (!_search)
                    EmployeePage();
                else
                    EmployeePage(_searchCondition);
            }

        }

        private void ToolStripButtonNextPageEmployee_Click(object sender, EventArgs e)
        {

            if (_employeePage.PageNumber == _employeePage.LastPage)
                 return;
            else
            {
                _employeePage.PageNumber += 1;
                if (!_search)
                    EmployeePage();
                else
                    EmployeePage(_searchCondition); 
            }
        }

        private void ToolStripButtonLastPageEmployee_Click(object sender, EventArgs e)
        {
            if (_employeePage.PageNumber == _employeePage.LastPage)
                return;
            else
            {
                _employeePage.PageNumber = _employeePage.LastPage;
                if (!_search)
                    EmployeePage();
                else
                    EmployeePage(_searchCondition);
            }
        }

        // Customer Module
        private void ToolStripButtonSearchCustomer_Click(object sender, EventArgs e)
        {
            string item, searchValue;

            if (toolStripComboBoxColumnCustomer.Text == "IC Number")
            { item = "ic_passport"; }
            else if (toolStripComboBoxColumnCustomer.Text == "Name")
            { item = "name"; }
            else
            { return; }

            if (toolStripComboBoxStringCustomer.Text == "Start with")
            { searchValue = "'{0}%'"; }
            else if (toolStripComboBoxStringCustomer.Text == "End with")
            { searchValue = "'%{0}'"; }
            else if (toolStripComboBoxStringCustomer.Text == "Contains")
            { searchValue = "'%{0}%'"; }
            else
            { return; }

            if (string.IsNullOrWhiteSpace(toolStripTextBoxCustomer.Text))
            { return; }

            searchValue = String.Format(searchValue, toolStripTextBoxCustomer.Text);

            _searchCondition = "{0} LIKE {1} AND status <> 'Disabled'";
            _searchCondition = String.Format(_searchCondition, item, searchValue);

            _search = true;
            _customerPage.PageNumber = 1;
            CustomerPage(_searchCondition);

        }

        private void ToolStripButtonResetCustomer_Click(object sender, EventArgs e)
        {
            _search = false;
            _searchCondition = "";
            toolStripComboBoxColumnCustomer.SelectedIndex = -1;
            toolStripComboBoxStringCustomer.SelectedIndex = -1;
            toolStripTextBoxCustomer.Text = "";
            _customerPage.PageNumber = 1;
            CustomerPage();
        }

        private void ToolStripButtonFirstPageCustomer_Click(object sender, EventArgs e)
        {
            if (_customerPage.PageNumber == 1)
                return;
            else
            {
                _customerPage.PageNumber = 1;
                if (!_search)
                    CustomerPage();
                else
                    CustomerPage(_searchCondition);
            }
        }

        private void ToolStripButtonPreviousPageCustomer_Click(object sender, EventArgs e)
        {
            if (_customerPage.PageNumber == 1)
                return;
            else
            {
                _customerPage.PageNumber -= 1;
                if (!_search)
                    CustomerPage();
                else
                    CustomerPage(_searchCondition);
            }


        }

        private void ToolStripButtonNextPageCustomer_Click(object sender, EventArgs e)
        {
            if (_customerPage.PageNumber == _customerPage.LastPage)
                return;
            else
            {
                _customerPage.PageNumber += 1;
                if (!_search)
                    CustomerPage();
                else
                    CustomerPage(_searchCondition);
            }

        }

        private void ToolStripButtonLastPageCustomer_Click(object sender, EventArgs e)
        {
            if (_customerPage.PageNumber == _customerPage.LastPage)
                return;
            else
            {
                _customerPage.PageNumber = _customerPage.LastPage;
                if (!_search)
                    CustomerPage();
                else
                    CustomerPage(_searchCondition);
            }
        }

        // Locker Type Module
        private void ToolStripButtonFirstPageLockerType_Click(object sender, EventArgs e)
        {
            if (_lockerTypePage.PageNumber == 1)
                return;
            else
            {
                _lockerTypePage.PageNumber = 1;
                LockerTypePage();
            }

        }

        private void ToolStripButtonPreviousPageLockerType_Click(object sender, EventArgs e)
        {
            if (_lockerTypePage.PageNumber == 1)
                return;
            else
            {
                _lockerTypePage.PageNumber -= 1;
                LockerTypePage();
            }
        }

        private void ToolStripButtonNextPageLockerType_Click(object sender, EventArgs e)
        {
            if (_lockerTypePage.PageNumber == _lockerTypePage.LastPage)
                return;
            else
            {
                _lockerTypePage.PageNumber += 1;
                LockerTypePage();
            }
        }

        private void ToolStripButtonLastPageLockerType_Click(object sender, EventArgs e)
        {
            if (_lockerTypePage.PageNumber == _lockerTypePage.LastPage)
                return;
            else
            {
                _lockerTypePage.PageNumber = _lockerTypePage.LastPage
;
                LockerTypePage();
            }
        }

        // Cabinet Module
        private void ToolStripButtonFirstPageCabinet_Click(object sender, EventArgs e)
        {
            if (_cabinetPage.PageNumber == 1)
                return;
            else
            {
                _cabinetPage.PageNumber = 1;
                if (!_search)
                    CabinetPage();
                else
                    CabinetPage(_searchCondition);
            }
        }

        private void ToolStripButtonPreviousPageCabinet_Click(object sender, EventArgs e)
        {
            if (_cabinetPage.PageNumber == 1)
                return;
            else
            {
                _cabinetPage.PageNumber = -1;
                if (!_search)
                    CabinetPage();
                else
                    CabinetPage(_searchCondition);
            }
        }

        private void ToolStripButtonNextPageCabinet_Click(object sender, EventArgs e)
        {
            if (_cabinetPage.PageNumber == _cabinetPage.LastPage)
                return;
            else
            {
                _cabinetPage.PageNumber += 1;
                if (!_search)
                    CabinetPage();
                else
                    CabinetPage(_searchCondition);
            }
        }

        private void ToolStripButtonLastPageCabinet_Click(object sender, EventArgs e)
        {
            if (_cabinetPage.PageNumber == _cabinetPage.LastPage)
                return;
            else
            {
                _cabinetPage.PageNumber = _cabinetPage.LastPage;
                if (!_search)
                    CabinetPage();
                else
                    CabinetPage(_searchCondition);
            }
        }

        // Locker Module
        // Locker Cabinet
        private void ToolStripButtonFirstPageLockerCabinet_Click(object sender, EventArgs e)
        {
            if (_lockerCabinetPage.PageNumber == 1)
                return;
            else
            {
                _pageFlip = true;
                _lockerCabinetPage.PageNumber = 1;
                LockerCabinetPage();
            }
        }

        private void ToolStripButtonPreviousPageLockerCabinet_Click(object sender, EventArgs e)
        {
            if (_lockerCabinetPage.PageNumber == 1)
                return;
            else
            {
                _pageFlip = true;
                _lockerCabinetPage.PageNumber -= 1;
                LockerCabinetPage();
            }
        }

        private void ToolStripButtonNextPageLockerCabinet_Click(object sender, EventArgs e)
        {
            if (_lockerCabinetPage.PageNumber == _lockerCabinetPage.LastPage)
                return;
            else
            {
                _pageFlip = true;
                _lockerCabinetPage.PageNumber += 1;
                LockerCabinetPage();
            }
        }

        private void ToolStripButtonLastPageLockerCabinet_Click(object sender, EventArgs e)
        {
            if (_lockerCabinetPage.PageNumber == _lockerCabinetPage.LastPage)
                return;
            else
            {
                _pageFlip = true;
                _lockerCabinetPage.PageNumber = _lockerCabinetPage.LastPage;
                LockerCabinetPage();
            }
        }

        // Locker
        private void ToolStripButtonFirstPageLocker_Click(object sender, EventArgs e)
        {
            if (_lockerPage.PageNumber == 1)
                return;
            else
            {
                _lockerPage.PageNumber = 1;
                LockerPage(_cabinetId);
            }
        }

        private void ToolStripButtonPreviousPageLocker_Click(object sender, EventArgs e)
        {
            if (_lockerPage.PageNumber == 1)
                return;
            else
            {
                _lockerPage.PageNumber -= 1;
                LockerPage(_cabinetId);
            }
        }

        private void ToolStripButtonNextPageLocker_Click(object sender, EventArgs e)
        {
            if (_lockerPage.PageNumber == _lockerPage.LastPage)
                return;
            else
            {
                _lockerPage.PageNumber += 1;
                LockerPage(_cabinetId);
            }
        }

        private void ToolStripButtonLastPageLocker_Click(object sender, EventArgs e)
        {
            if (_lockerPage.PageNumber == _lockerPage.LastPage)
                return;
            else
            {
                _lockerPage.PageNumber = _lockerPage.LastPage;
                LockerPage(_cabinetId);
            }
        }

        // Rental
        private void ToolStripButtonSearchRental_Click(object sender, EventArgs e)
        {
            string innerSqlItem;
            string outerSqlItem;
            string searchValue;
            string table;

            if (toolStripComboBoxColumnRental.Text == "Customer IC")
            {
                innerSqlItem = "ic_passport";
                outerSqlItem = "customer_id";
                table = "Customer";
            }
            else if (toolStripComboBoxColumnRental.Text == "Locker Code")
            {
                innerSqlItem = "code";
                outerSqlItem = "locker_id";
                table = "Locker";
            }
            else
            { return; }

            if (toolStripComboBoxStringRental.Text == "Start with")
            { searchValue = "'{0}%'"; }
            else if (toolStripComboBoxStringRental.Text == "End with")
            { searchValue = "'%{0}'"; }
            else if (toolStripComboBoxStringRental.Text == "Contains")
            { searchValue = "'%{0}%'"; }
            else
                return;

            if (string.IsNullOrWhiteSpace(toolStripTextBoxRental.Text))
                return;

            searchValue = String.Format(searchValue, toolStripTextBoxRental.Text);

            string condition = "{0} IN (SELECT id FROM {1} WHERE {2} LIKE {3}) AND status <> 'Ended'";
            _searchCondition = String.Format(condition, outerSqlItem, table, innerSqlItem, searchValue);

            _search = true;
            _rentalPage.PageNumber = 1;
            RentalPage(_searchCondition);
        }

        private void ToolStripButtonResetRental_Click(object sender, EventArgs e)
        {
            _search = false;
            _searchCondition = "";
            toolStripComboBoxColumnRental.SelectedIndex = -1;
            toolStripComboBoxStringRental.SelectedIndex = -1;
            toolStripTextBoxRental.Text = "";
            _rentalPage.PageNumber = 1;
            RentalPage();
        }

        private void ToolStripButtonFirstPageRental_Click(object sender, EventArgs e)
        {
            if (_rentalPage.PageNumber == 1)
                return;
            else
            {
                _rentalPage.PageNumber = 1;
                if (!_search)
                    RentalPage();
                else
                    RentalPage(_searchCondition);
            }

        }

        private void ToolStripButtonPreviousPageRental_Click(object sender, EventArgs e)
        {
            if (_rentalPage.PageNumber == 1)
                return;
            else
            {
                _rentalPage.PageNumber -= 1;
                if (!_search)
                    RentalPage();
                else
                    RentalPage(_searchCondition);
            }
        }

        private void ToolStripButtonNextPageRental_Click(object sender, EventArgs e)
        {
            if (_rentalPage.PageNumber == _rentalPage.LastPage)
                return;
            else
            {
                _rentalPage.PageNumber += 1;
                if (!_search)
                    RentalPage();
                else
                    RentalPage(_searchCondition);
            }
        }

        private void ToolStripButtonLastPageRental_Click(object sender, EventArgs e)
        {
            if (_rentalPage.PageNumber == _rentalPage.LastPage)
                return;
            else
            {
                _rentalPage.PageNumber = _rentalPage.LastPage;
                if (!_search)
                    RentalPage();
                else
                    RentalPage(_searchCondition);
            }

        }

        // Deleted Records
        // Deleted Employee
        private void ToolStripButtonSearchDeletedEmployee_Click(object sender, EventArgs e)
        {
            string item, searchValue;

            if (toolStripComboBoxColumnDeletedEmployee.Text == "IC Number")
            { item = "ic_passport"; }
            else if (toolStripComboBoxColumnDeletedEmployee.Text == "Name")
            { item = "name"; }
            else if (toolStripComboBoxColumnDeletedEmployee.Text == "Username")
            { item = "username"; }
            else
            { return; }

            if (toolStripComboBoxStringDeletedEmployee.Text == "Start with")
            { searchValue = "'{0}%'"; }
            else if (toolStripComboBoxStringDeletedEmployee.Text == "End with")
            { searchValue = "'%{0}'"; }
            else if (toolStripComboBoxStringDeletedEmployee.Text == "Contains")
            { searchValue = "'%{0}%'"; }
            else
            { return; }

            if (string.IsNullOrWhiteSpace(toolStripTextBoxDeletedEmployee.Text))
            { return; }

            searchValue = String.Format(searchValue, toolStripTextBoxDeletedEmployee.Text);

            _searchCondition = "{0} LIKE {1} AND status = 'Disabled'";
            _searchCondition = String.Format(_searchCondition, item, searchValue);

            _search = true;
            _deletedEmployeePage.PageNumber = 1;
            DeletedEmployeePage(_searchCondition);
        }

        private void ToolStripButtonResetDeletedEmployee_Click(object sender, EventArgs e)
        {
            _search = false;
            _searchCondition = "";
            toolStripComboBoxColumnDeletedEmployee.SelectedIndex = -1;
            toolStripComboBoxStringDeletedEmployee.SelectedIndex = -1;
            toolStripTextBoxDeletedEmployee.Text = "";
            _deletedEmployeePage.PageNumber = 1;
            DeletedEmployeePage();

        }

        private void ToolStripButtonFirstPageDeletedEmployee_Click(object sender, EventArgs e)
        {
            if (_deletedEmployeePage.PageNumber == 1)
                return;
            else
            {
                _deletedEmployeePage.PageNumber = 1;
                if (!_search)
                    DeletedEmployeePage();
                else
                    DeletedEmployeePage(_searchCondition);
            }
        }

        private void ToolStripButtonPreviousPageDeletedEmployee_Click(object sender, EventArgs e)
        {
            if (_deletedEmployeePage.PageNumber == 1)
                return;
            else
            {
                _deletedEmployeePage.PageNumber -= 1;
                if (!_search)
                    DeletedEmployeePage();
                else
                    DeletedEmployeePage(_searchCondition);
            }
        }

        private void ToolStripButtonNextPageDeletedEmployee_Click(object sender, EventArgs e)
        {
            if (_deletedEmployeePage.PageNumber == _deletedEmployeePage.LastPage)
                return;
            else
            {
                _deletedEmployeePage.PageNumber += 1;
                if (!_search)
                    DeletedEmployeePage();
                else
                    DeletedEmployeePage(_searchCondition);
            }
        }

        private void ToolStripButtonLastPageDeletedEmployee_Click(object sender, EventArgs e)
        {
            if (_deletedEmployeePage.PageNumber == _deletedEmployeePage.LastPage)
                return;
            else
            {
                _deletedEmployeePage.PageNumber = _deletedEmployeePage.LastPage;
                if (!_search)
                    DeletedEmployeePage();
                else
                    DeletedEmployeePage(_searchCondition);
            }
        }

        // Deleted Customer
        private void ToolStripButtonSearchDeletedCustomer_Click(object sender, EventArgs e)
        {
            string item, searchValue;

            if (toolStripComboBoxColumnDeletedCustomer.Text == "IC Number")
            { item = "ic_passport"; }
            else if (toolStripComboBoxColumnDeletedCustomer.Text == "Name")
            { item = "name"; }
            else
            { return; }

            if (toolStripComboBoxStringDeletedCustomer.Text == "Start with")
            { searchValue = "'{0}%'"; }
            else if (toolStripComboBoxStringDeletedCustomer.Text == "End with")
            { searchValue = "'%{0}'"; }
            else if (toolStripComboBoxStringDeletedCustomer.Text == "Contains")
            { searchValue = "'%{0}%'"; }
            else
            { return; }

            if (string.IsNullOrWhiteSpace(toolStripTextBoxDeletedCustomer.Text))
            { return; }

            searchValue = String.Format(searchValue, toolStripTextBoxDeletedCustomer.Text);

            _searchCondition = "{0} LIKE {1} AND status = 'Disabled'";
            _searchCondition = String.Format(_searchCondition, item, searchValue);

            _search = true;
            _deletedCustomerPage.PageNumber = 1;
            DeletedCustomerPage(_searchCondition);
        }

        private void ToolStripButtonResetDeletedCustomer_Click(object sender, EventArgs e)
        {
            _search = false;
            _searchCondition = "";
            toolStripComboBoxColumnDeletedCustomer.SelectedIndex = -1;
            toolStripComboBoxStringDeletedCustomer.SelectedIndex = -1;
            toolStripTextBoxDeletedCustomer.Text = "";
            _deletedCustomerPage.PageNumber = 1;
            DeletedCustomerPage();
        }

        private void ToolStripButtonFirstPageDeletedCustomer_Click(object sender, EventArgs e)
        {
            if (_deletedCustomerPage.PageNumber == 1)
                return;
            else
            {
                _deletedCustomerPage.PageNumber = 1;
                if (!_search)
                    DeletedCustomerPage();
                else
                    DeletedCustomerPage(_searchCondition);
            }
        }

        private void ToolStripButtonPreviousPageDeletedCustomer_Click(object sender, EventArgs e)
        {
            if (_deletedCustomerPage.PageNumber == 1)
                return;
            else
            {
                _deletedCustomerPage.PageNumber -= 1;
                if (!_search)
                    DeletedCustomerPage();
                else
                    DeletedCustomerPage(_searchCondition);
            }
        }

        private void ToolStripButtonNextPageDeletedCustomer_Click(object sender, EventArgs e)
        {
            if (_deletedCustomerPage.PageNumber == _deletedCustomerPage.LastPage)
                return;
            else
            {
                _deletedCustomerPage.PageNumber += 1;
                if (!_search)
                    DeletedCustomerPage();
                else
                    DeletedCustomerPage(_searchCondition);
            }
        }

        private void ToolStripButtonLastPageDeletedCustomer_Click(object sender, EventArgs e)
        {
            if (_deletedCustomerPage.PageNumber == _deletedCustomerPage.LastPage)
                return;
            else
            {
                _deletedCustomerPage.PageNumber = _deletedCustomerPage.LastPage;
                if (!_search)
                    DeletedCustomerPage();
                else
                    DeletedCustomerPage(_searchCondition);
            }
        }

        // Ended Rental
        private void ToolStripButtonSearchEndedRental_Click(object sender, EventArgs e)
        {
            string innerSqlItem;
            string outerSqlItem;
            string searchValue;
            string table;

            if (toolStripComboBoxColumnEndedRental.Text == "Customer IC")
            {
                innerSqlItem = "ic";
                outerSqlItem = "customer_id";
                table = "Customer";
            }
            else if (toolStripComboBoxColumnEndedRental.Text == "Locker Code")
            {
                innerSqlItem = "code";
                outerSqlItem = "locker_id";
                table = "Locker";
            }
            else if (toolStripComboBoxColumnEndedRental.Text == "Employee Username")
            {
                innerSqlItem = "username";
                outerSqlItem = "employee_id";
                table = "Employee";
            }
            else
                return; 

            if (toolStripComboBoxStringEndedRental.Text == "Start with")
                searchValue = "'{0}%'";
            else if (toolStripComboBoxStringEndedRental.Text == "End with")
                searchValue = "'%{0}'";
            else if (toolStripComboBoxStringEndedRental.Text == "Contains")
                searchValue = "'%{0}%'";
            else
                return;

            if (string.IsNullOrWhiteSpace(toolStripTextBoxEndedRental.Text))
                return;

            searchValue = String.Format(searchValue, toolStripTextBoxEndedRental.Text);

            string condition = "{0} IN (SELECT id FROM {1} WHERE {2} LIKE {3}) AND status = 'Ended'";
            _searchCondition = String.Format(condition, outerSqlItem, table, innerSqlItem, searchValue);

            _search = true;
            _endedRentalPage.PageNumber = 1;
            EndedRentalPage(_searchCondition);
        }

        private void ToolStripButtonResetEndedRental_Click(object sender, EventArgs e)
        {
            _search = false;
            _searchCondition = "";
            toolStripComboBoxColumnEndedRental.SelectedIndex = -1;
            toolStripComboBoxStringEndedRental.SelectedIndex = -1;
            toolStripTextBoxEndedRental.Text = "";
            _endedRentalPage.PageNumber = 1;
            EndedRentalPage();
        }

        private void ToolStripButtonFirstPageEndedRental_Click(object sender, EventArgs e)
        {
            if (_endedRentalPage.PageNumber == 1)
                return;
            else
            {
                _endedRentalPage.PageNumber = 1;
                if (!_search)
                    EndedRentalPage();
                else
                    EndedRentalPage(_searchCondition);
            }
        }

        private void ToolStripButtonPreviousPageEndedRental_Click(object sender, EventArgs e)
        {
            if (_endedRentalPage.PageNumber == 1)
                return;
            else
            {
                _endedRentalPage.PageNumber -= 1;
                if (!_search)
                    EndedRentalPage();
                else
                    EndedRentalPage(_searchCondition);
            }
        }

        private void ToolStripButtonNextPageEndedRental_Click(object sender, EventArgs e)
        {
            if (_endedRentalPage.PageNumber == _endedRentalPage.LastPage)
                return;
            else
            {
                _endedRentalPage.PageNumber += 1;
                if (!_search)
                    EndedRentalPage();
                else
                    EndedRentalPage(_searchCondition);
            }
        }

        private void ToolStripButtonLastPageEndedRental_Click(object sender, EventArgs e)
        {
            if (_endedRentalPage.PageNumber == _endedRentalPage.LastPage)
                return;
            else
            {
                _endedRentalPage.PageNumber = _endedRentalPage.LastPage;
                if (!_search)
                    EndedRentalPage();
                else
                    EndedRentalPage(_searchCondition);
            }

        }

        /*
         * List View Sorting Event Handlers
         */
        private void ListViewEmployee_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != _sortColumn)
            {
                _sortColumn = e.Column;
                listViewEmployee.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (listViewEmployee.Sorting == SortOrder.Ascending)
                    listViewEmployee.Sorting = SortOrder.Descending;
                else
                    listViewEmployee.Sorting = SortOrder.Ascending;
            }

            listViewEmployee.Sort();
            this.listViewEmployee.ListViewItemSorter = new ListViewItemComparer(e.Column, listViewEmployee.Sorting);

        }

        private void ListViewCustomer_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != _sortColumn)
            {
                _sortColumn = e.Column;
                listViewCustomer.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (listViewCustomer.Sorting == SortOrder.Ascending)
                    listViewCustomer.Sorting = SortOrder.Descending;
                else
                    listViewCustomer.Sorting = SortOrder.Ascending;
            }

            listViewCustomer.Sort();
            this.listViewCustomer.ListViewItemSorter = new ListViewItemComparer(e.Column, listViewCustomer.Sorting);
        }

        private void ListViewLockerType_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != _sortColumn)
            {
                _sortColumn = e.Column;
                listViewLockerType.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (listViewLockerType.Sorting == SortOrder.Ascending)
                    listViewLockerType.Sorting = SortOrder.Descending;
                else
                    listViewLockerType.Sorting = SortOrder.Ascending;
            }

            listViewLockerType.Sort();
            this.listViewLockerType.ListViewItemSorter = new ListViewItemComparer(e.Column, listViewLockerType.Sorting);

        }

        private void ListViewCabinet_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != _sortColumn)
            {
                // Set the sort column to the new column.
                _sortColumn = e.Column;

                // Set the sort order to ascending by default.
                listViewCabinet.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (listViewCabinet.Sorting == SortOrder.Ascending)
                    listViewCabinet.Sorting = SortOrder.Descending;
                else
                    listViewCabinet.Sorting = SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            listViewCabinet.Sort();

            // Set the ListViewItemSorter property to a new ListViewItemComparer object.
            this.listViewCabinet.ListViewItemSorter = new ListViewItemComparer(e.Column, listViewCabinet.Sorting);
        }

        private void ListViewLockerCabinet_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != _sortColumn)
            {
                // Set the sort column to the new column.
                _sortColumn = e.Column;

                // Set the sort order to ascending by default.
                listViewLockerCabinet.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (listViewLockerCabinet.Sorting == SortOrder.Ascending)
                    listViewLockerCabinet.Sorting = SortOrder.Descending;
                else
                    listViewLockerCabinet.Sorting = SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            listViewLockerCabinet.Sort();

            // Set the ListViewItemSorter property to a new ListViewItemComparer object.
            this.listViewLockerCabinet.ListViewItemSorter = new ListViewItemComparer(e.Column, listViewLockerCabinet.Sorting);
        }

        private void ListViewRental_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != _sortColumn)
            {
                // Set the sort column to the new column.
                _sortColumn = e.Column;

                // Set the sort order to ascending by default.
                listViewRental.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (listViewRental.Sorting == SortOrder.Ascending)
                    listViewRental.Sorting = SortOrder.Descending;
                else
                    listViewRental.Sorting = SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            listViewRental.Sort();

            // Set the ListViewItemSorter property to a new ListViewItemComparer object.
            this.listViewRental.ListViewItemSorter = new ListViewItemComparer(e.Column, listViewRental.Sorting);

        }

        private void ListViewDeletedEmployee_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != _sortColumn)
            {
                _sortColumn = e.Column;
                listViewDeletedEmployee.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (listViewDeletedEmployee.Sorting == SortOrder.Ascending)
                    listViewDeletedEmployee.Sorting = SortOrder.Descending;
                else
                    listViewDeletedEmployee.Sorting = SortOrder.Ascending;
            }

            listViewDeletedEmployee.Sort();
            this.listViewDeletedEmployee.ListViewItemSorter = new ListViewItemComparer(e.Column, listViewDeletedEmployee.Sorting);

        }

        private void ListViewDeletedCustomer_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != _sortColumn)
            {
                _sortColumn = e.Column;
                listViewDeletedCustomer.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (listViewDeletedCustomer.Sorting == SortOrder.Ascending)
                    listViewDeletedCustomer.Sorting = SortOrder.Descending;
                else
                    listViewDeletedCustomer.Sorting = SortOrder.Ascending;
            }

            listViewDeletedCustomer.Sort();
            this.listViewDeletedCustomer.ListViewItemSorter = new ListViewItemComparer(e.Column, listViewDeletedCustomer.Sorting);
        }

        private void ListViewDeletedLockerType_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != _sortColumn)
            {
                _sortColumn = e.Column;
                listViewDeletedLockerType.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (listViewDeletedLockerType.Sorting == SortOrder.Ascending)
                    listViewDeletedLockerType.Sorting = SortOrder.Descending;
                else
                    listViewDeletedLockerType.Sorting = SortOrder.Ascending;
            }

            listViewDeletedLockerType.Sort();
            this.listViewDeletedLockerType.ListViewItemSorter = new ListViewItemComparer(e.Column, listViewDeletedLockerType.Sorting);

        }

        private void ListViewDeletedCabinet_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != _sortColumn)
            {
                // Set the sort column to the new column.
                _sortColumn = e.Column;

                // Set the sort order to ascending by default.
                listViewDeletedCabinet.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (listViewDeletedCabinet.Sorting == SortOrder.Ascending)
                    listViewDeletedCabinet.Sorting = SortOrder.Descending;
                else
                    listViewDeletedCabinet.Sorting = SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            listViewDeletedCabinet.Sort();

            // Set the ListViewItemSorter property to a new ListViewItemComparer object.
            this.listViewDeletedCabinet.ListViewItemSorter = new ListViewItemComparer(e.Column, listViewDeletedCabinet.Sorting);
        }

        private void ListViewEndedRental_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != _sortColumn)
            {
                // Set the sort column to the new column.
                _sortColumn = e.Column;

                // Set the sort order to ascending by default.
                listViewEndedRental.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (listViewEndedRental.Sorting == SortOrder.Ascending)
                    listViewEndedRental.Sorting = SortOrder.Descending;
                else
                    listViewEndedRental.Sorting = SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            listViewEndedRental.Sort();

            // Set the ListViewItemSorter property to a new ListViewItemComparer object.
            this.listViewEndedRental.ListViewItemSorter = new ListViewItemComparer(e.Column, listViewEndedRental.Sorting);

        }

    }
}

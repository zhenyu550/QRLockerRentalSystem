using LockerRentalManagementSystem.Core;
using LockerRentalManagementSystem.Controller;
using LockerRentalManagementSystem.Exceptions;
using LockerRentalManagementSystem.Model;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace LockerRentalManagementSystem.View
{
    public partial class RentalForm : Form
    {
        // Private Attributes
        private bool _isInsertComplete = false;
        private Rental _rental = new Rental();
        private Employee _employee = new Employee();
        private Customer _customer = new Customer();
        private Locker _locker = new Locker();
        private Cabinet _cabinet = new Cabinet();
        private LockerType _lockerType = new LockerType();
        private RentalController _rentalController = new RentalController();

        // Getter and Setters
        public bool IsInsertComplete()
        {
            return _isInsertComplete;
        }

        // Cosntructor for Add Rental
        public RentalForm(Employee employee)
        {
            InitializeComponent();

            // Hide all tabs not related to Add Rental
            this.Controls.Remove(tabControlRental);
            this.Controls.Add(panelAddRental);

            // Set the rental start date and end date minimum value as today date
            dateTimePickerStartDateAddRental.MinDate = DateTime.Now.Date;
            dateTimePickerEndDateAddRental.MinDate = DateTime.Now.Date;

            // Set the employee data
            _employee = employee;
        }

        // Constructor for View Rental
        public RentalForm(int id, bool isEndedRental)
        {
            InitializeComponent();

            // Hide all tabs not related to view rental
            this.Controls.Remove(tabControlRental);
            this.Controls.Add(panelViewRental);

            // If view ended rental, hide the change locker button
            if (isEndedRental)
                buttonChangeLocker.Hide();

            // Get data related to the rental
            _rental = Rental.Get(id);
            _customer = Customer.Get(_rental.CustomerId);
            _employee = Employee.Get(_rental.EmployeeId);
            _locker = Locker.Get(_rental.LockerId);
            _cabinet = Cabinet.Get(_locker.CabinetId);
            _lockerType = LockerType.Get(_cabinet.LockerTypeId);

            // Insert the data into display fields
            ViewRentalLoadRentalData();

        }

        // Constructor for End Rental
        public RentalForm(Rental rental)
        {
            InitializeComponent();

            // Hide all tabs not related to End Rental
            this.Controls.Remove(tabControlRental);
            this.Controls.Add(panelEndRental);

            // Set rental data
            _rental = rental;

            // Get data related to the rental
            _customer = Customer.Get(_rental.CustomerId);
            _locker = Locker.Get(_rental.LockerId);
            _cabinet = Cabinet.Get(_locker.CabinetId);
            _lockerType = LockerType.Get(_cabinet.LockerTypeId);

            // Insert the data into display fields
            EndRentalLoadRentalData();
        }

        // Constructor for Export Rental 
        public RentalForm(bool isExport)
        {
            InitializeComponent();

            // Hide all tabs not related to End Rental
            this.Controls.Remove(tabControlRental);
            this.Controls.Add(panelExportRental);

            // Rezie the form
            this.Width = 460;
            this.Height = 250;

            // Get The Booking Date of the first ended rental record
            List<Rental> rentals = Rental.Where("status = 'Ended'", 0, 1);

            // If there is a ended rental in the list
            if (rentals.Any())
            {
                DateTime firstEndedRentaBookinglDate = rentals[0].BookingDateTime;

                if (!rentals[0].BookingDateTime.Equals(DateTime.Now.Date))
                {
                    // Set the minium value of booking date for From & Until Day to the booking date of first ended rental
                    dateTimePickerFrom.MinDate = firstEndedRentaBookinglDate.Date;
                    dateTimePickerUntil.MinDate = firstEndedRentaBookinglDate.Date;

                    // Set the assigned value of booking date for From & Until Day to the booking date of first ended rental
                    dateTimePickerFrom.Value = firstEndedRentaBookinglDate.Date;
                    dateTimePickerUntil.Value = firstEndedRentaBookinglDate.Date;

                    // Set the maximum value of booking date for From & Until Day before today
                    dateTimePickerFrom.MaxDate = DateTime.Now.Date.AddDays(-1);
                    dateTimePickerUntil.MaxDate = DateTime.Now.Date.AddDays(-1);
                }
            }
        }


        // Methods
        private void AddRentalLoadCustomerData()
        {
            textBoxCustomerNameAddRental.Text = _customer.Name;
            textBoxCustomerIcAddRental.Text = _customer.IcPassport;
        }

        private void PayRentalLoadRentalData()
        {
            // Customer Data
            textBoxCustomerNamePayRental.Text = _customer.Name;
            textBoxCustomerIcPayRental.Text = _customer.IcPassport;

            // Locker Data
            textBoxLockerCodePayRental.Text = _locker.Code;
            textBoxLockerCabinetPayRental.Text = _cabinet.Code;
            textBoxLockerSizePayRental.Text = _lockerType.Name;
            numericUpDownLockerRatePayRental.Value = _lockerType.Rate;

            // Rental Data
            textBoxStartDatePayRental.Text = dateTimePickerStartDateAddRental.Value.ToString("dd-MM-yyyy");
            textBoxEndDatePayRental.Text = dateTimePickerEndDateAddRental.Value.ToString("dd-MM-yyyy");
            textBoxDurationPayRental.Text = numericUpDownDurationAddRental.Value.ToString();
            numericUpDownTotalPricePayRental.Value = _lockerType.Rate * numericUpDownDurationAddRental.Value;
        }

        private void ViewRentalLoadRentalData()
        {
            // Rental
            textBoxRentalCodeViewRental.Text = _rental.Code;
            textBoxBookingDateViewRental.Text = _rental.BookingDateTime.ToString("dd-MM-yyyy");
            textBoxBookingTimeViewRental.Text = _rental.BookingDateTime.ToString("HH:mm:ss");
            textBoxStartDateViewRental.Text = _rental.StartDate.ToString("dd-MM-yyyy");
            textBoxEndDateViewRental.Text = _rental.EndDate.ToString("dd-MM-yyyy");

            textBoxDurationViewRental.Text = _rental.Duration.ToString();
            textBoxStatusViewRental.Text = _rental.Status;

            if (!_rental.ReturnDateTime.Date.ToString("dd-MM-yyyy").Equals("01-01-0001"))
            {
                textBoxReturnDateViewRental.Text = _rental.ReturnDateTime.ToString("dd-MM-yyyy");
                textBoxReturnTimeViewRental.Text = _rental.ReturnDateTime.ToString("HH:mm:ss");
            }
            pictureBoxQRCodeViewRental.Image = _rentalController.GenerateQR(_rental.Key);

            // Customer
            textBoxCustomerNameViewRental.Text = _customer.Name;
            textBoxCustomerIcViewRental.Text = _customer.IcPassport;

            // Employee
            textBoxEmployeeNameViewRental.Text = _employee.Name;
            textBoxEmployeeIcViewRental.Text = _employee.IcPassport;

            // Locker
            textBoxLockerCodeViewRental.Text = _locker.Code;
            textBoxLockerCabinetViewRental.Text = _cabinet.Code;
            textBoxLockerSizeViewRental.Text = _lockerType.Name;
            textBoxLockerRateViewRental.Text = _lockerType.Rate.ToString("F2");

            // Payment
            textBoxTotalPriceViewRental.Text = (_lockerType.Rate * _rental.Duration).ToString("F2");
        }

        private void EndRentalLoadRentalData()
        {
            // Customer
            textBoxCustomerNameEndRental.Text = _customer.Name;
            textBoxCustomerIcEndRental.Text = _customer.IcPassport;

            // Locker
            textBoxLockerCodeEndRental.Text = _locker.Code;
            textBoxLockerCabinetEndRental.Text = _cabinet.Code;
            textBoxLockerSizeEndRental.Text = _lockerType.Name;
            textBoxLockerRateEndRental.Text = String.Format("{0:.##}", _lockerType.Rate);

            // Rental Data
            textBoxRentalCodeEndRental.Text = _rental.Code;
            textBoxBookingDateEndRental.Text = _rental.BookingDateTime.ToString("dd-MM-yyyy");
            textBoxBookingTimeEndRental.Text = _rental.BookingDateTime.ToString("HH:mm:ss");
            textBoxStartDateEndRental.Text = _rental.StartDate.ToString("dd-MM-yyyy");
            textBoxDurationEndRental.Text = _rental.Duration.ToString();
            textBoxEndDateEndRental.Text = _rental.EndDate.ToString("dd-MM-yyyy");

            TimeSpan timeSpan = _rental.EndDate.Date.Subtract(DateTime.Now.Date);
            int overdueDays = timeSpan.Days;

            if (overdueDays < 0)
                textBoxOverdueDays.Text = (overdueDays * -1).ToString();
            else
                textBoxOverdueDays.Text = "0";
            
        }

        // Event Handlers
        private void ButtonSelectCustomer_Click(object sender, EventArgs e)
        {
            SelectCustomerForm selectCustomerForm = new SelectCustomerForm();
            selectCustomerForm.ShowDialog();

            if (!selectCustomerForm.IsSelected())
                return;

            _customer = selectCustomerForm.SelectedCustomer;
            AddRentalLoadCustomerData();
        }

        private void ButtonNextAddRental_Click(object sender, EventArgs e)
        {
            try
            {
                _rentalController.SetAddRentalData(_customer, _employee,
                    dateTimePickerStartDateAddRental.Value, dateTimePickerEndDateAddRental.Value,
                    (int)numericUpDownDurationAddRental.Value);

                _rentalController.CheckRentalDuration((int)numericUpDownDurationAddRental.Value);

                DateTime startdate = dateTimePickerStartDateAddRental.Value;
                DateTime endDate = dateTimePickerEndDateAddRental.Value;

                SelectLockerForm selectLockerForm = new SelectLockerForm(startdate, endDate);
                selectLockerForm.ShowDialog();

                if (!selectLockerForm.IsSelected())
                    return;

                _rentalController.SetAddRentalLockerData(selectLockerForm.SelectedLocker);

                // Get data of the selected locker
                _locker = selectLockerForm.SelectedLocker;
                _cabinet = Cabinet.Get(_locker.CabinetId);
                _lockerType = LockerType.Get(_cabinet.LockerTypeId);

                PayRentalLoadRentalData();

                // Show Pay Rental
                this.Controls.Remove(panelAddRental);
                this.Controls.Add(panelPayRental);

                // Hide OK button and show only Confrim Button
                buttonOKPayRental.Hide();
            }
            catch (InvalidUserInputException exception)
            {
                exception.ShowErrorMessage();
            }
        }

        private void ButtonCancelAddRental_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonConfirmPayRental_Click(object sender, EventArgs e)
        {
            try
            {
                Decimal change = _rentalController.CalculateChange(numericUpDownTotalPricePayRental.Value,
                    numericUpDownPayment.Value);

                // Set the booking date time, rental code & rental key 
                DateTime bookingDateTime = DateTime.Now;
                string rentalCode = bookingDateTime.ToString("yyyyMMddHHmmssffffff");
                string rentalKey = Security.SHA256Hash(rentalCode + _locker.Code + _customer.IcPassport + 
                    _rental.Duration.ToString() + _rental.StartDate.ToString("ddMMyyyy") + _employee.IcPassport);

                // Insert the above data into the rental
                _rentalController.SetPayRentalData(rentalCode, bookingDateTime, rentalKey);

                // Save the rental
                _rentalController.SaveRentalData();

                // Display the payment result
                numericUpDownChange.Value = change;

                // Generate the QRCode
                Bitmap qrCodeImage = _rentalController.GenerateQR(rentalKey);
                pictureBoxQRCodePayRental.Image = qrCodeImage;

                // Set Rental Inserted as true
                _isInsertComplete = true;

                // Hide Confirm & back button
                buttonConfirmPayRental.Hide();
                buttonBackPayRental.Hide();

                // Show OK button
                buttonOKPayRental.Show();
            }
            catch (InvalidUserInputException exception)
            {
                exception.ShowErrorMessage();
            }
        }

        private void ButtonBackPayRental_Click(object sender, EventArgs e)
        {
            // Show Add Rental
            this.Controls.Add(panelAddRental);
            this.Controls.Remove(panelPayRental);
        }

        private void ButtonOKPayRental_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonOKViewRental_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonOKEndRental_Click(object sender, EventArgs e)
        {
            _rental.ReturnDateTime = DateTime.Now;
            _rentalController.SetEndRentalData(_rental);
            _rentalController.EndRental();
            _isInsertComplete = true;

            this.Close();
        }

        private void ButtonExportRental_Click(object sender, EventArgs e)
        {
            try
            {
                _rentalController.CheckExportDate(dateTimePickerFrom.Value, dateTimePickerUntil.Value);

                string startDate = dateTimePickerFrom.Value.ToString("dd-MM-yyyy");
                string endDate = dateTimePickerUntil.Value.ToString("dd-MM-yyyy");

                var result = MessageBox.Show("Do you want to export rentals from " + startDate + " until " + endDate + "?\n\n" +
                    "Note: Exported access log will be deleted from the database.",
                    "Export Access Log", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    _rentalController.ExportRentalData(dateTimePickerFrom.Value, dateTimePickerUntil.Value);
                    _isInsertComplete = true;
                    this.Close();
                }
            }
            catch (InvalidUserInputException exception)
            {
                exception.ShowErrorMessage();
            }
        }

        private void ButtonCancelEndRental_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonCancelExportRental_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonChangeLocker_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Do you want to change the locker for this rental?", "Change Locker",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                //Check if the rental overdue. If yes, show error message and return.

                TimeSpan timeSpan = _rental.EndDate.Date.Subtract(DateTime.Now.Date);
                int daysLeft = Convert.ToInt32(timeSpan.Days);
                if (daysLeft < 0)
                {
                    MessageBox.Show("Access Error: Rental Overdued." + Environment.NewLine +
                        "You cannot change details for an overdued rental.", "Access Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SelectLockerForm selectLockerForm = new SelectLockerForm(_rental.Id);
                selectLockerForm.ShowDialog();

                if (selectLockerForm.IsSelected())
                {
                    // Assign the original cabinet data into a temporary variable
                    Locker previousLocker = _locker;

                    // Get the new locker type, cabinet and locker data for the selected locker
                    _locker = selectLockerForm.SelectedLocker;
                    _cabinet = Cabinet.Get(_locker.CabinetId);
                    _lockerType = LockerType.Get(_cabinet.LockerTypeId);
                    _rental.LockerId = _locker.Id;

                    // Save the rental
                    RentalController rentalController = new RentalController();
                    rentalController.ChangeBookedLocker(_rental, previousLocker);
                    _isInsertComplete = true;

                    // Load the new data into locker display
                    ViewRentalLoadRentalData();
                }
            }
        }

        private void DateTimePickerStartDateAddRental_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan timeSpan = dateTimePickerEndDateAddRental.Value.Date.Subtract(dateTimePickerStartDateAddRental.Value.Date);
            decimal duration = Convert.ToDecimal(timeSpan.Days);
            if(duration < 0)
                numericUpDownDurationAddRental.Value = 0;
            else
                numericUpDownDurationAddRental.Value = duration;

        }

        private void DateTimePickerEndDateAddRental_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan timeSpan = dateTimePickerEndDateAddRental.Value.Date.Subtract(dateTimePickerStartDateAddRental.Value.Date);
            decimal duration = Convert.ToDecimal(timeSpan.Days);
            if (duration < 0)
                numericUpDownDurationAddRental.Value = 0;
            else
                numericUpDownDurationAddRental.Value = duration;
        }

        private void NumericUpDownDurationAddRental_ValueChanged(object sender, EventArgs e)
        {
            dateTimePickerEndDateAddRental.Value = dateTimePickerStartDateAddRental.Value.AddDays(
                Convert.ToDouble(numericUpDownDurationAddRental.Value));
        }

    }
}

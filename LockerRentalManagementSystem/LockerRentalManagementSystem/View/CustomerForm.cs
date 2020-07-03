using LockerRentalManagementSystem.Controller;
using LockerRentalManagementSystem.Exceptions;
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
    public partial class CustomerForm : Form
    {
        // Private Attributes
        private bool _isInsertComplete = false;
        private Customer _customer = new Customer();

        // Getter
        public bool IsInsertComplete()
        {
            return _isInsertComplete;
        }

        // Constructor (Add Customer)
        public CustomerForm()
        {
            InitializeComponent();

            // Hide all labels and buttons that are not related to Add Customer
            buttonBack.Hide();
            buttonEdit.Hide();
            buttonClose.Hide();
            buttonSave.Hide();
            labelViewCustomer.Hide();
            labelEditCustomer.Hide();

        }

        // Overloading Constructor (View Customer)
        public CustomerForm(int id)
        {
            InitializeComponent();

            // Hide all labels and buttons that are not related to View Employee
            buttonCancel.Hide();
            buttonConfirm.Hide();
            buttonBack.Hide();
            buttonSave.Hide();
            labelAddCustomer.Hide();
            labelEditCustomer.Hide();

            // Get the employee data for using the id
            _customer = Customer.Get(id);
            LoadCustomerData(_customer);
            LockInput();

        }

        // Methods
        // Method to load customer data into input fields
        private void LoadCustomerData(Customer customerData)
        {
            textBoxName.Text = customerData.Name;
            textBoxIcPassport.Text = customerData.IcPassport;
            comboBoxGender.Text = customerData.Gender;
            textBoxPhoneNo.Text = customerData.PhoneNo;
            textBoxEmail.Text = customerData.Email;
            textBoxHomeAddress.Text = customerData.Address;
        }

        // Method to lock input fields
        private void LockInput()
        {
            textBoxName.ReadOnly = true;
            textBoxIcPassport.ReadOnly = true;
            comboBoxGender.Enabled = false;
            textBoxPhoneNo.ReadOnly = true;
            textBoxEmail.ReadOnly = true;
            textBoxHomeAddress.ReadOnly = true;
        }

        // Method to unlock input fields
        private void UnlockInput()
        {
            textBoxPhoneNo.ReadOnly = false;
            textBoxEmail.ReadOnly = false;
            textBoxHomeAddress.ReadOnly = false;
        }


        // Event Handlers
        // Cancel Button Event Handler
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Confirm Button Event Handler
        private void ButtonConfirm_Click(object sender, EventArgs e)
        {
            CustomerController customerController = new CustomerController();
            try
            {
                customerController.SetCustomerData(textBoxName.Text, textBoxIcPassport.Text, comboBoxGender.Text, textBoxPhoneNo.Text,
                    textBoxEmail.Text, textBoxHomeAddress.Text);
        
                customerController.SaveCustomerData();
                _isInsertComplete = true;
                this.Close();
            }
            catch (InvalidUserInputException exception)
            {
                exception.ShowErrorMessage();
            }

        }

        // Close Button Event Handler
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Edit Button Event Handler
        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            // Hide all labels & buttons related to View Customer
            buttonEdit.Hide();
            buttonClose.Hide();
            labelViewCustomer.Hide();

            // Show all label & buttons related to Edit Customer
            buttonBack.Show();
            buttonSave.Show();
            labelEditCustomer.Show();

            // Unlock User Input for certain fields
            UnlockInput();
        }
        
        // Back Button Event Handler
        private void ButtonBack_Click(object sender, EventArgs e)
        {
            // Hide all labels & buttons related to Edit Customer
            buttonBack.Hide();
            buttonSave.Hide();
            labelEditCustomer.Hide();

            // Show all label & buttons related to View Customer
            buttonEdit.Show();
            buttonClose.Show();
            labelViewCustomer.Show();

            // Lock User Input for all fields
            LockInput();

            // Discard Unsaved Data
            LoadCustomerData(_customer);
        }

        // Save Button Event Handler
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            CustomerController customerController = new CustomerController();
            try
            {
                customerController.SetCustomerData(_customer, textBoxPhoneNo.Text, textBoxEmail.Text, textBoxHomeAddress.Text);
                customerController.SaveCustomerData();
                _customer = customerController.GetCustomer();

                // Load Fields with new data
                LoadCustomerData(_customer);

                // Lock the user input
                LockInput();

                // Show Label and Buttons related to View Employee
                buttonClose.Show();
                buttonEdit.Show();
                labelViewCustomer.Show();

                // Hide Label and Buttons related to Edit Employee
                buttonSave.Hide();
                buttonBack.Hide();
                labelEditCustomer.Hide();
            }
            catch (InvalidUserInputException exception)
            {
                exception.ShowErrorMessage();
            }
        }
    }
}

using LockerRentalManagementSystem.Controller;
using LockerRentalManagementSystem.Exceptions;
using LockerRentalManagementSystem.Model;
using System;
using System.Windows.Forms;

namespace LockerRentalManagementSystem.View
{
    public partial class EmployeeForm : Form
    {
        // Private variables
        private bool _isFirstAdmin;
        private bool _isInsertComplete;
        private Employee _employee = new Employee();

        // Getter
        public bool IsInsertComplete()
        {
            return _isInsertComplete;
        }

        // Constructors
        // Constructor for Add Employee / Create Admin Account
        public EmployeeForm(bool isFirstAdmin)
        {
            InitializeComponent();

            // Hide all labels and buttons that are not related to Add Employee / Create Admin
            buttonBack.Hide();
            buttonEdit.Hide();
            buttonClose.Hide();
            buttonSave.Hide();
            labelViewEmployee.Hide();
            labelEditEmployee.Hide();

            _isFirstAdmin = isFirstAdmin;

            // boolean firstAdmin determines Create Admin Account or Add Employee
            if (isFirstAdmin)
            {
                // Hide Add Employee Label
                labelAddEmployee.Hide();
            }
            else
            {
                // Hide Create Admin Account Label
                labelCreateAdmin.Hide();
            }

            // Set the Default Password
            textBoxPassword.Text = "123456";
            textBoxPassword.PasswordChar = '\0';
            textBoxPassword.UseSystemPasswordChar = false;
        }

        // Constructor for View Employee
        public EmployeeForm(int id)
        {
            InitializeComponent();

            // Hide all labels and buttons that are not related to View Employee
            buttonCancel.Hide();
            buttonConfirm.Hide();
            buttonBack.Hide();
            buttonSave.Hide();
            labelAddEmployee.Hide();
            labelCreateAdmin.Hide();
            labelEditEmployee.Hide();

            // Get the employee data for using the id
            _employee = Employee.Get(id);
            LoadEmployeeData(_employee);
            LockInput();

        }

        // Methods
        // Method to load employee data into input fields
        private void LoadEmployeeData(Employee employeeData)
        {
            textBoxName.Text = employeeData.Name;
            textBoxIcPassport.Text = employeeData.IcPassport;
            comboBoxGender.Text = employeeData.Gender;
            textBoxPhoneNo.Text = employeeData.PhoneNo;
            textBoxEmail.Text = employeeData.Email;
            textBoxHomeAddress.Text = employeeData.Address;
            textBoxUsername.Text = employeeData.Username;
            textBoxPassword.Text = "";
            comboBoxPermission.SelectedIndex = comboBoxPermission.FindStringExact(employeeData.Permission);
            comboBoxPosition.SelectedIndex = comboBoxPosition.FindStringExact(employeeData.Position);
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
            textBoxUsername.ReadOnly = true;
            textBoxPassword.ReadOnly = true;
            comboBoxPermission.Enabled = false;
            comboBoxPosition.Enabled = false;
        }

        // Method to unlock input fields
        private void UnlockInput()
        {
            textBoxPhoneNo.ReadOnly = false;
            textBoxEmail.ReadOnly = false;
            textBoxHomeAddress.ReadOnly = false;
            comboBoxPermission.Enabled = true;
            comboBoxPosition.Enabled = true;
        }

        // Event Handlers
        // Confirm Button Event handler (For Add Employee / Create Admin Account)
        private void ButtonConfirm_Click(object sender, EventArgs e)
        {
            EmployeeController employeeController = new EmployeeController();
            try
            {
                employeeController.SetEmployeeData(textBoxName.Text, textBoxIcPassport.Text, comboBoxGender.Text,
                  textBoxPhoneNo.Text, textBoxEmail.Text, textBoxHomeAddress.Text, textBoxUsername.Text,
                  textBoxPassword.Text, comboBoxPermission.Text, comboBoxPosition.Text);

                if (_isFirstAdmin)
                {
                    if (!comboBoxPermission.Text.Equals("Admin"))
                    {
                        MessageBox.Show("Initialization Error: No Admin Account." +
                           "\nThe account to be created must be an admin account. ", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                employeeController.SaveEmployeeData();
                _isInsertComplete = true;
                this.Close();
            }
            catch (InvalidUserInputException exception)
            {
                exception.ShowErrorMessage();
            }
        }

        // Cancel Button Event Handler
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Edit Button Event Handler
        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            // Hide Label and Buttons related to View Employee
            buttonClose.Hide();
            buttonEdit.Hide();
            labelViewEmployee.Hide();

            // Show Label and Buttons related to Edit Employee
            buttonSave.Show();
            buttonBack.Show();
            labelEditEmployee.Show();

            // Unlock the user input for certain fields
            UnlockInput();
        }

        // Close Button Event Handler
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        // Save Button Event Handler
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            EmployeeController employeeController = new EmployeeController();
            try
            {
                employeeController.SetEmployeeData(_employee, textBoxPhoneNo.Text, textBoxEmail.Text,
                    textBoxHomeAddress.Text, comboBoxPermission.Text, comboBoxPosition.Text);
                employeeController.SaveEmployeeData();
                _employee = employeeController.GetEmployee();

                // Load Fields with new data
                LoadEmployeeData(_employee);

                // Lock the user input
                LockInput();

                // Show Label and Buttons related to View Employee
                buttonClose.Show();
                buttonEdit.Show();
                labelViewEmployee.Show();

                // Hide Label and Buttons related to Edit Employee
                buttonSave.Hide();
                buttonBack.Hide();
                labelEditEmployee.Hide();
            }
            catch (InvalidUserInputException exception)
            {
                exception.ShowErrorMessage();
            }
        }

        // Back button Event Handler
        private void ButtonBack_Click(object sender, EventArgs e)
        {
            // Show Label and Buttons related to View Employee
            buttonClose.Show();
            buttonEdit.Show();
            labelViewEmployee.Show();

            // Hide Label and Buttons related to Edit Employee
            buttonSave.Hide();
            buttonBack.Hide();
            labelEditEmployee.Hide();

            // Lock the user input for certain fields
            LockInput();

            // Discard Unsaved Data
            LoadEmployeeData(_employee);
        }
    }
}

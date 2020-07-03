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
    public partial class CabinetForm : Form
    {
        // Private Attributes
        private List<LockerType> _lockerTypes = new List<LockerType>();
        private Dictionary<int, string> _lockerTypeDictionary = new Dictionary<int, string>();
        private string _lockerTypeCode;
        private bool _isInsertComplete = false;
        private string _condition = "";
        private string _cabinetLockerType = "";
        private string _cabinetStatus = "";
        public string Condition { get { return _condition; } set { _condition = value; } }

        // Getter
        public bool IsInsertComplete()
        {
            return _isInsertComplete;
        }

        // Constructors
        // Constructor for Add Cabinet
        public CabinetForm()
        {
            InitializeComponent();

            // Remove the Tab Control
            Controls.Remove(tabControlCabinet);

            // Add the Add Cabinet Panel
            Controls.Add(panelAddCabinet);

            // Get the list of Locker Types in the database
            _lockerTypes = LockerType.Where("status <> 'Disabled'", 0, 2147483467);

            // Insert Locker Types into the Locker Type Dictionary
            foreach (LockerType lockerType in _lockerTypes)
                _lockerTypeDictionary.Add(lockerType.Id, lockerType.Name);

            // Exit the data binding if no data inside the dictionary
            if(!_lockerTypeDictionary.Any())
                return;

            // Bind Locker Type Dictonary onto Combo Box Locker Type
            comboBoxLockerType.DataSource = new BindingSource(_lockerTypeDictionary, null);

            // Display the Locker Type Name and Set the Locker Type Id as Value
            comboBoxLockerType.DisplayMember = "Value";
            comboBoxLockerType.ValueMember = "Key";

            // Set the selected index to -1
            comboBoxLockerType.SelectedIndex = -1;
        }
        
        // Constructor for Filter Cabinet / Filter Deleted Cabinet
        public CabinetForm(bool isDisabled)
        {
            InitializeComponent();

            // Remove the Tab Control
            Controls.Remove(tabControlCabinet);

            // Determine the list to be filtered is cabinet or deleted (disabled) cabinet 
            if (isDisabled)
            {
                Controls.Add(panelFilterDeletedCabinet);
                this.Height = 150;

                // Get the list of all locker types, including the ones disabled
                _lockerTypes = LockerType.All(0, 2147483467);

                // Add default element (Select ALL locker types) into the Locker Type Dictonary
                _lockerTypeDictionary.Add(0, "All");

                // Insert Locker Types into the Locker Type Dictonary
                foreach (LockerType lockerType in _lockerTypes)
                    _lockerTypeDictionary.Add(lockerType.Id, lockerType.Name);

                // Exit the data binding if no data inside the dictionary
                if (!_lockerTypeDictionary.Any())
                    return;

                // Bind Locker Type Dictonary onto Combo Box Locker Type for Filter Cabinet
                comboBoxLockerTypeDeletedCabinet.DataSource = new BindingSource(_lockerTypeDictionary, null);

                // Display the Locker Type Name and Set the Locker Type Id as Value
                comboBoxLockerTypeDeletedCabinet.DisplayMember = "Value";
                comboBoxLockerTypeDeletedCabinet.ValueMember = "Key";

                // Set the selected index to 0 (Select ALL locker types)
                comboBoxLockerTypeDeletedCabinet.SelectedIndex = 0;
            }
            else
            {
                Controls.Add(panelFilterCabinet);
                this.Height = 260;

                // Get the list of available locker types
                _lockerTypes = LockerType.Where("status <> 'Disabled'", 0, 2147483467);

                // Add default element (Select ALL locker types) into the Locker Type Dictonary
                _lockerTypeDictionary.Add(0, "All");

                // Insert Locker Types into the Locker Type Dictonary
                foreach (LockerType lockerType in _lockerTypes)
                    _lockerTypeDictionary.Add(lockerType.Id, lockerType.Name);

                // Exit the data binding if no data inside the dictionary
                if (!_lockerTypeDictionary.Any())
                    return;

                // Bind Locker Type Dictonary onto Combo Box Locker Type for Filter Cabinet
                comboBoxLockerTypeFilterCabinet.DataSource = new BindingSource(_lockerTypeDictionary, null);

                // Display the Locker Type Name and Set the Locker Type Id as Value
                comboBoxLockerTypeFilterCabinet.DisplayMember = "Value";
                comboBoxLockerTypeFilterCabinet.ValueMember = "Key";

                // Set the selected index to 0 (Select ALL locker types)
                comboBoxLockerTypeFilterCabinet.SelectedIndex = 0;

                // Default select ALL cabinets (Ignore cabinet status)
                radioButtonAll.Checked = true;
            }
        }

        // Methods

        // Event Handlers
        // Add Cabinet
        private void ComboBoxLockerType_TextChanged(object sender, EventArgs e)
        {
            // Ignore the event if no index was selected
            if (comboBoxLockerType.SelectedIndex < 0)
                return;

            //Select the Locker Type Code for the selected Locker Type
            var item = from selected in _lockerTypes
                       where selected.Name.Contains(comboBoxLockerType.Text)
                       select selected;
            _lockerTypeCode = item.First().Code;

            // Auto generate Cabinet Code
            int newCabinetCodeNumber = 0;

            // Select the cabinet that have the Largest Id (Most recent) for the selected locker type
            string cabinetCondition = "id = (SELECT MAX(id) FROM cabinet WHERE locker_type_id = {0})";
            List<Cabinet> cabinets = Cabinet.Where(String.Format(cabinetCondition, item.First().Id), 0, 1);

            if (!cabinets.Any())
            {
                // Set value to 1 if no cabinet for that locker type
                newCabinetCodeNumber = 1;
            }
            else
            {
                // Get the Cabinet Code
                string currentCabinetCode = cabinets[0].Code;
                string currentCabinetCodeNumber = String.Empty;

                // Extract Digit characters (Number only) from the Cabinet Code
                for (int i = 0; i < currentCabinetCode.Length; i++)
                {
                    if (Char.IsDigit(currentCabinetCode[i]))
                        currentCabinetCodeNumber += currentCabinetCode[i];
                }

                // Convert the extract code into int and add 1 onto it
                newCabinetCodeNumber = Int32.Parse(currentCabinetCodeNumber) + 1;
            }

            // Create the code of the new cabinet and fix to 2 digits
            textBoxCabinetCode.Text = String.Format("{0}-{1}", _lockerTypeCode,
                newCabinetCodeNumber.ToString("D2"));
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            // Return error if no valid items in Combo Box Locker Type is selected
            if (comboBoxLockerType.SelectedIndex < 0)
            {
                MessageBox.Show("Input Error: Invalid input detected!" + Environment.NewLine +
                  "Please ensure that field 'Locker Type' was filled with provided items. ",
                  "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the Locker Type Id of the cabinet using the value of comboBoxLockerType, and parse to int
            int selectedLockerTypeId = Int32.Parse(comboBoxLockerType.SelectedValue.ToString());

            // Set the Cabinet Data and Save it
            CabinetLockerController cabinetController = new CabinetLockerController();
            cabinetController.SetCabinetData(textBoxCabinetCode.Text, selectedLockerTypeId, 
                (int)numericUpDownRow.Value, (int)numericUpDownColumn.Value);
            cabinetController.SaveCabinetData();

            // Set the insert complete boolean as true
            _isInsertComplete = true;

            // Exit this form
            this.Close();
        }

        // Filter Cabinet
        private void ButtonOKFilterCabinet_Click(object sender, EventArgs e)
        {
            // Return error if no valid items in Combo Box Locker Type is selected
            if (comboBoxLockerTypeFilterCabinet.SelectedIndex < 0)
            {
                MessageBox.Show("Input Error: Invalid input detected!" + Environment.NewLine +
                  "Please ensure that field 'Locker Type' was filled with provided items. ",
                  "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the Locker Type Id of the cabinet using the value of comboBoxLockerType, and parse to int
            int selectedLockerTypeId = Int32.Parse(comboBoxLockerTypeFilterCabinet.SelectedValue.ToString());
            if (selectedLockerTypeId == 0)
                _cabinetLockerType = "IS NOT NULL";
            else
                _cabinetLockerType = String.Format(" = {0}", selectedLockerTypeId);

            // Create the search condition string
            _condition = "locker_type_id {0} AND status {1}";
            _condition = String.Format(_condition, _cabinetLockerType, _cabinetStatus);

            // Close this form
            this.Close();
        }

        private void ButtonCloseFilterCabinet_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RadioButtonAll_CheckedChanged(object sender, EventArgs e)
        {
            _cabinetStatus = "<> 'Disabled'";
        }

        private void RadioButtonAvailable_CheckedChanged(object sender, EventArgs e)
        {
            _cabinetStatus = "= 'Available'";
        }

        private void RadioButtonFull_CheckedChanged(object sender, EventArgs e)
        {
            _cabinetStatus = "= 'Full'";
        }

        // Filter Deleted Cabinet
        private void ButtonCloseFilterDeletedCabinet_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonOKFilterDeletedCabinet_Click(object sender, EventArgs e)
        {
            // Set cabinet Status to Disabled
            _cabinetStatus = " = 'Disabled'";

            // Return error if no valid items in Combo Box Locker Type is selected
            if (comboBoxLockerTypeDeletedCabinet.SelectedIndex < 0)
            {
                MessageBox.Show("Input Error: Invalid input detected!" + Environment.NewLine +
                  "Please ensure that field 'Locker Type' was filled with provided items. ",
                  "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Get the Locker Type Id of the cabinet using the value of comboBoxLockerType, and parse to int
            int selectedLockerTypeId = Int32.Parse(comboBoxLockerTypeDeletedCabinet.SelectedValue.ToString());
            if (selectedLockerTypeId == 0)
                _cabinetLockerType = "IS NOT NULL";
            else
                _cabinetLockerType = String.Format(" = {0}", selectedLockerTypeId);

            // Create the search condition string
            _condition = "locker_type_id {0} AND status {1}";
            _condition = String.Format(_condition, _cabinetLockerType, _cabinetStatus);

            // Close this form
            this.Close();
        }
    }
}

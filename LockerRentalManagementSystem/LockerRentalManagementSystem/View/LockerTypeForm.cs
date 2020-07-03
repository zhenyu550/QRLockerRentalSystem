using LockerRentalManagementSystem.Controller;
using LockerRentalManagementSystem.Exceptions;
using LockerRentalManagementSystem.Model;
using System;
using System.Windows.Forms;

namespace LockerRentalManagementSystem.View
{
    public partial class LockerTypeForm : Form
    {
        // Private Attributes
        private bool _isInsertComplete = false;
        private bool _isNewInsert;
        private LockerType _lockerType = new LockerType();

        // Getter
        public bool IsInsertComplete()
        {
            return _isInsertComplete;
        }

        // Constructor for Add Locker Type
        public LockerTypeForm()
        {
            InitializeComponent();

            // Hide labels not related to Add Locker Type
            labelEditLockerType.Hide();

            // Declare this operation is insert a new locker type
            _isNewInsert = true;
        }

        // Constructor for Edit Locker Type
        public LockerTypeForm(int id)
        {
            InitializeComponent();

            // Hide labels not related to Edit Locker Type
            labelAddLockerType.Hide();

            // Lock Name and Code input
            textBoxName.ReadOnly = true;
            textBoxCode.ReadOnly = true;

            // Declare this operation is not insert new locker
            _isNewInsert = false;

            // Get the Locker Type Data for this id
            _lockerType = LockerType.Get(id);

            // Load data into Input Field
            textBoxName.Text = _lockerType.Name;
            textBoxCode.Text = _lockerType.Code;
            numericUpDownRentalRate.Value = _lockerType.Rate;
        }


        // Event Handlers
        // Save Button Event Handler
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            LockerTypeController lockerTypeController = new LockerTypeController();
            try
            {
                if (_isNewInsert)
                    lockerTypeController.SetLockerTypeData(textBoxName.Text, textBoxCode.Text, numericUpDownRentalRate.Value);
                else
                    lockerTypeController.SetLockerTypeData(_lockerType, numericUpDownRentalRate.Value);

                lockerTypeController.SaveLockerTypeData();
                _isInsertComplete = true;
                this.Close();
            }
            catch (InvalidUserInputException exception)
            {
                exception.ShowErrorMessage();
            }
        }

        //  Cancel Button Event Handler
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

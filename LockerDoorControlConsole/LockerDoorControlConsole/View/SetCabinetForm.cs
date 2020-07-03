using LockerDoorControlConsole.Controller;
using LockerDoorControlConsole.Exceptions;
using LockerDoorControlConsole.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LockerDoorControlConsole.View
{
    public partial class SetCabinetForm : Form
    {
        // Private Attributes
        private bool _isCabinetSelected = false;
        private DatabaseController _dbController;

        // Getters
        public bool IsCabinetSelected()
        {
            return _isCabinetSelected;
        }

        public string GetCabinetCode()
        {
            return _dbController.CabinetCode;
        }

        // Constructor
        public SetCabinetForm(DatabaseController databaseController)
        {
            InitializeComponent();
            _dbController = databaseController;
            LoadCabinetCode();
        }

        public void LoadCabinetCode()
        {
            Dictionary<int,string> cabinetDictionary = new Dictionary<int,string>();

            // Clear combo box  & cabinet dictonary to avoid error
            cabinetDictionary.Clear();

            // Get list of cabinets from database
            List<Cabinet> cabinets = _dbController.GetAllCabinets();

            // Add cabinet into cabinet dictonary
            foreach (Cabinet cabinet in cabinets)
                cabinetDictionary.Add(cabinet.Id, cabinet.Code); 

            // Bind cabinet dictonary onto combo box cabinet code
            comboBoxCabinetCode.DataSource = new BindingSource(cabinetDictionary, null);

            // Display the Cabinet Code and Set the Cabinet Id as ValueMember
            comboBoxCabinetCode.DisplayMember = "Value";
            comboBoxCabinetCode.ValueMember = "Key";

        }

        // Methods
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonConfirm_Click(object sender, EventArgs e)
        {
            string cabinetCode = comboBoxCabinetCode.Text;

            try
            {
                _dbController.CabinetCode = cabinetCode;
                _dbController.CheckCabinet();
                _dbController.SaveIniFile();
                _isCabinetSelected = true;
                this.Close();
            }
            catch (InvalidCabinetException exception)
            {
                exception.ShowErrorMessage();
            }
            catch (InvalidDatabaseConnectionException exception)
            {
                exception.ShowErrorMessage();
            }

        }
    }
}

using LockerRentalManagementSystem.Controller;
using LockerRentalManagementSystem.Core;
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
    public partial class SelectLockerForm : Form
    {
        // Private Attributes
        private Locker _selectedLocker = new Locker();
        private bool _isSelected = false;
        private List<LockerType> _lockerTypes = new List<LockerType>();
        private Dictionary<int, string> _lockerTypeDictonary = new Dictionary<int, string>();
        private string _cabinetStatus = "";
        private string _cabinetSize = "";

        private Rental _rental = new Rental();
        private Locker _locker = new Locker();
        private Cabinet _cabinet = new Cabinet();
        private LockerType _lockerType = new LockerType();

        private DateTime _startDate = new DateTime();
        private DateTime _endDate = new DateTime();

        private Page _lockerCabinetPage = new Page();
        private Page _lockerPage = new Page();

        // Getter
        public Locker SelectedLocker { get { return _selectedLocker; } }
        public bool IsSelected() { return _isSelected; }

        // Constructor for Add Rental
        public SelectLockerForm(DateTime newRentalStartDate, DateTime newRentalEndDate)
        {
            InitializeComponent();

            // Change the label on buttonCancel to Back
            buttonCancel.Text = "Back";

            // Set the startDate and endDate to be checked
            _startDate = newRentalStartDate;
            _endDate = newRentalEndDate;

            // Clear combo box  & locker type dictonary to avoid error
            _lockerTypeDictonary.Clear();

            // Load Locker Type Name into Combo Box 1
            _lockerTypes = LockerType.Where("status <> 'Disabled'", 0, 2147483467);

            // Add default value (select ALL) into locker type dictonary
            _lockerTypeDictonary.Add(0, "All");

            // Add locker types into locker type dictonary
            foreach (LockerType lockerType in _lockerTypes)
                _lockerTypeDictonary.Add(lockerType.Id, lockerType.Name);

            // Bind locker type dictonary onto combo box locker type locker cabinet (In Locker Module)
            comboBoxLockerTypeLockerCabinet.DataSource = new BindingSource(_lockerTypeDictonary, null);

            // Display the Locker Type Name and Set the Locker Type Id as ValueMember
            comboBoxLockerTypeLockerCabinet.DisplayMember = "Value";
            comboBoxLockerTypeLockerCabinet.ValueMember = "Key";

            // Trigger SelectedIndexChanged event
            comboBoxLockerTypeLockerCabinet.SelectedIndex = -1;

            // Default Select All Cabinets
            comboBoxLockerTypeLockerCabinet.SelectedIndex = 0;

            // Load all cabinet list
            _lockerCabinetPage.PageNumber = 1;
            LockerCabinetPage();

            //Default select the first cabinet to load
            List<Cabinet> cabinets = Cabinet.Where("status <> 'Disabled'", 0, 1);

            //If no cabinet in list, return
            if (!cabinets.Any())
                return;

            // Get the available lockers
            CabinetLockerController cabinetLockerController = new CabinetLockerController();
            List<Locker> availableLockers = cabinetLockerController.GetAvailableLockers(cabinets[0].Id, newRentalStartDate, newRentalEndDate);

            textBoxCabinetCode.Text = cabinets[0].Code;
            textBoxEmptyLockerNo.Text = availableLockers.Count.ToString();

            _lockerPage.PageNumber = 1;
            LockerPage(availableLockers);
        }

        // Constructor for View Rental
        public SelectLockerForm(int rentalId)
        {
            InitializeComponent();

            // Get all data related to the rental
            _rental = Rental.Get(rentalId);
            _locker = Locker.Get(_rental.LockerId);
            _cabinet = Cabinet.Get(_locker.CabinetId);
            _lockerType = LockerType.Get(_cabinet.LockerTypeId);

            // Set the start date and end date of rental
            _startDate = _rental.StartDate;
            _endDate = _rental.EndDate;

            // Clear combo box  & locker type dictonary to avoid error
            _lockerTypeDictonary.Clear();

            // Only add the involved locker type into the directonary
            _lockerTypeDictonary.Add(_lockerType.Id, _lockerType.Name);
        
            // Bind locker type dictonary onto combo box locker type locker cabinet (In Locker Module)
            comboBoxLockerTypeLockerCabinet.DataSource = new BindingSource(_lockerTypeDictonary, null);

            // Display the Locker Type Name and Set the Locker Type Id as ValueMember
            comboBoxLockerTypeLockerCabinet.DisplayMember = "Value";
            comboBoxLockerTypeLockerCabinet.ValueMember = "Key";

            // Trigger SelectedIndexChanged event
            comboBoxLockerTypeLockerCabinet.SelectedIndex = -1;

            // Select the invloved locker type
            comboBoxLockerTypeLockerCabinet.SelectedIndex = 0;

            // Disable the comboBox 
            comboBoxLockerTypeLockerCabinet.Enabled = false;
            
            // Load all cabinet list
            _lockerCabinetPage.PageNumber = 1;
            LockerCabinetPage();

            //Default select the involved cabinet to load
            textBoxCabinetCode.Text = _cabinet.Code;

            // Get the available lockers for the selected cabinet
            CabinetLockerController cabinetLockerController = new CabinetLockerController();
            List<Locker> availableLockers = cabinetLockerController.GetAvailableLockers(_cabinet.Id, _startDate, _endDate);

            textBoxEmptyLockerNo.Text = availableLockers.Count.ToString();

            _lockerPage.PageNumber = 1;
            LockerPage(availableLockers);

        }

        // Methods
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

        private void ReloadLockerList(List<Locker> lockers)
        {
            listViewLocker.Items.Clear();
            foreach (Locker locker in lockers)
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

        private void LockerCabinetPage()
        {
            _cabinetStatus = "<> 'Disabled'";

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
        }

        private void LockerPage(List<Locker> lockers)
        {
            _lockerPage.FinalIndex = Convert.ToDouble(lockers.Count);
            _lockerPage.LastPage = Convert.ToInt32(Math.Ceiling(_lockerPage.FinalIndex / _lockerPage.MaxItems));
            _lockerPage.PageSetting();

            if (_lockerPage.FinalIndex == 0)
                _lockerPage.PageReset();

            if (_lockerPage.PageNumber == _lockerPage.LastPage)
                _lockerPage.LastIndex = (int)_lockerPage.FinalIndex;

            toolStripLabelPageNoLocker.Text = String.Format("Page {0} / {1}", _lockerPage.PageNumber, _lockerPage.LastPage);
            toolStripLabelResultLocker.Text = String.Format("Showing result {0}~{1}", _lockerPage.FirstIndex, _lockerPage.LastIndex);
            ReloadLockerList(lockers);
        }


        // Event Handler
        private void ButtonSelectLocker_Click(object sender, EventArgs e)
        {
            if (listViewLocker.SelectedItems.Count <= 0)
                return;

            ListViewItem lvi = listViewLocker.SelectedItems[0];

            string lockerCodeCondition = String.Format("code = '{0}'", lvi.Text);

            List<Locker> lockers = Locker.Where(lockerCodeCondition, 0, 1);
            var locker = lockers[0];

            try
            {
                CabinetLockerController cabinetLockerController = new CabinetLockerController();

                if(_rental.Id > 0)
                    cabinetLockerController.CheckLockerAvailability(locker, _rental);

                _selectedLocker = locker;
                _isSelected = true;

                this.Close();
            }
            catch (InvalidUserInputException exception)
            {
                exception.ShowErrorMessage();
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonSelectCabinet_Click(object sender, EventArgs e)
        {
            if (listViewLockerCabinet.SelectedItems.Count <= 0)
                return;

            ListViewItem lvi = listViewLockerCabinet.SelectedItems[0];
            _cabinet.Id = Convert.ToInt32(lvi.Text);
            textBoxCabinetCode.Text = lvi.SubItems[1].Text;

            // Get the available lockers
            CabinetLockerController cabinetLockerController = new CabinetLockerController();
            List<Locker> availableLockers = cabinetLockerController.GetAvailableLockers(_cabinet.Id, _startDate, _endDate);

            textBoxEmptyLockerNo.Text = availableLockers.Count.ToString();

            _lockerPage.PageNumber = 1;
            LockerPage(availableLockers);
        }

        private void ComboBoxLockerTypeLockerCabinet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxLockerTypeLockerCabinet.SelectedIndex < 0)
                return;

            // Get the real locker type Id from the locker type dictonary
            var dictValue = from selected in _lockerTypeDictonary
                            where selected.Value.Contains(comboBoxLockerTypeLockerCabinet.Text)
                            select selected;

            int lockerTypeId = dictValue.First().Key;

            if (lockerTypeId == 0)
                _cabinetSize = "IS NOT NULL";
            else
                _cabinetSize = String.Format("= {0}", lockerTypeId);

            _lockerCabinetPage.PageNumber = 1;
            LockerCabinetPage();
        }
    }
}

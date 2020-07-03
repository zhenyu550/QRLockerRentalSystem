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
    public partial class SelectLockerForm : Form
    {
        // Private variables
        private bool _isSelected = false;
        private LockerDoorController _lockerDoorController = new LockerDoorController();
        private List<Locker> _lockers = new List<Locker>();
        private Locker _selectedLocker = new Locker();
        private string _cabinetCode = "";

        // Getter & Setters
        public Locker SelectedLocker { get { return _selectedLocker; } }

        public bool IsSelected()
        {
            return _isSelected;
        }

        // Constructor
        public SelectLockerForm(string cabinetCode)
        {
            InitializeComponent();
            _cabinetCode = cabinetCode;
            labelCabinetCode.Text = _cabinetCode;
            GetAccessibleLockerData();
            ReloadLockerTable();
        }

        // Methods
        public void GetAccessibleLockerData()
        {
            _lockers = _lockerDoorController.GetLockers(_cabinetCode);

            // Select to display lockers with status "Not Available" and "Overdue" only
            foreach (Locker locker in _lockers.ToList())
            {
                // Remove lockers with status "Occupied" and "Disabled" from the list
                if (locker.IsOccupied() || locker.IsDisabled())
                    _lockers.Remove(locker);
            }
        }

        public void ReloadLockerTable()
        {
            listViewLocker.Items.Clear();
            foreach (Locker locker in _lockers)
            {
                ListViewItem lvi = new ListViewItem(locker.Code);
                lvi.SubItems.Add(locker.Status);
                lvi.SubItems.Add(locker.DoorStatus);

                if (locker.IsAvailable())
                {
                    if (locker.IsOpened())
                        lvi.ImageIndex = 4;
                    else
                        lvi.ImageIndex = 0;
                }
                else if (locker.IsOccupied())
                {
                    if (locker.IsOpened())
                        lvi.ImageIndex = 6;
                    else
                        lvi.ImageIndex = 2;
                }
                else if (locker.IsNotAvailable())
                {
                    if (locker.IsOpened())
                        lvi.ImageIndex = 5;
                    else
                        lvi.ImageIndex = 1;
                }
                else
                {
                    // Overdued
                    if (locker.IsOpened())
                        lvi.ImageIndex = 7;
                    else
                        lvi.ImageIndex = 3;
                }
                listViewLocker.Items.Add(lvi);
            }
        }

        // Event Handlers
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ButtonSelect_Click(object sender, EventArgs e)
        {
            // Ignore operation if nothing selected
            if (listViewLocker.SelectedItems.Count <= 0)
                return;

            // Get the locker code
            ListViewItem lvi = listViewLocker.SelectedItems[0];
            string lockerCodeCondition = String.Format("code = '{0}'", lvi.Text);

            // Get the locker data from database using the code
            List<Locker> lockers = Locker.Where(lockerCodeCondition, 0, 1);

            // Assign the first result to selectedLocker
            _selectedLocker = lockers[0];

            // Set flag IsSelected as true
            _isSelected = true;

            // Close this form
            this.Close();
        }
    }
}

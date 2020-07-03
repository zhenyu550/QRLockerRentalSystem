using LockerRentalManagementSystem.Core;
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
    public partial class SelectCustomerForm : Form
    {
        // Private Attributes
        private Page _customerPage = new Page();
        private Customer _selectedCustomer = new Customer();
        private bool _isSelected = false;
        private bool _search = false;
        private string _searchCondition = "";
        private int _sortColumn = -1;


        // Getter
        public Customer SelectedCustomer { get { return _selectedCustomer; } }
        public bool IsSelected() { return _isSelected; }

        // Constructor
        public SelectCustomerForm()
        {
            InitializeComponent();

            // Load the Customer List
            _search = false;
            _customerPage.PageReset();
            CustomerPage();
        }

        // Methods
        private void ReloadCustomerList(int offset, int count, string condition)
        {
            listViewSelectCustomer.Items.Clear();
            List<Customer> items = Customer.Where(condition, offset, count);
            foreach (Customer c in items)
            {
                ListViewItem lvi = new ListViewItem(c.Id.ToString());
                lvi.SubItems.Add(c.Name);
                lvi.SubItems.Add(c.IcPassport);

                listViewSelectCustomer.Items.Add(lvi);
            }
        }


        private void CustomerPage()
        {
            string condition = "status <> 'Disabled'";

            _customerPage.FinalIndex = Convert.ToDouble(Customer.Count(condition));
            _customerPage.LastPage = Convert.ToInt32(Math.Ceiling(_customerPage.FinalIndex / _customerPage.MaxItems));
            _customerPage.PageSetting();
            if (_customerPage.FinalIndex == 0)
                _customerPage.PageReset();
            
            if (_customerPage.PageNumber == _customerPage.LastPage)
                _customerPage.LastIndex = (int)_customerPage.FinalIndex;

            toolStripLabelPageNo.Text = String.Format("Page {0} / {1}", _customerPage.PageNumber, _customerPage.LastPage);
            toolStripLabelResult.Text = String.Format("Showing result {0}~{1}", _customerPage.FirstIndex, _customerPage.LastIndex);
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
            toolStripLabelPageNo.Text = String.Format("Page {0} / {1}", _customerPage.PageNumber, _customerPage.LastPage);
            toolStripLabelResult.Text = String.Format("Showing result {0}~{1}", _customerPage.FirstIndex, _customerPage.LastIndex);
            ReloadCustomerList(_customerPage.IndexLimit, _customerPage.MaxItems, condition);
        }
        
        // Event Handlers
        private void ButtonSelect_Click(object sender, EventArgs e)
        {
            if (listViewSelectCustomer.SelectedItems.Count <= 0)
                return;

            ListViewItem lvi = listViewSelectCustomer.SelectedItems[0];
            int id = Convert.ToInt32(lvi.Text);

            _selectedCustomer = Customer.Get(id);
            _isSelected = true;

            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolStripButtonSearch_Click(object sender, EventArgs e)
        {
            string item, searchValue;

            if (toolStripComboBoxColumn.Text == "IC Number")
            { item = "ic_passport"; }
            else if (toolStripComboBoxColumn.Text == "Name")
            { item = "name"; }
            else
            { return; }

            if (toolStripComboBoxString.Text == "Start with")
            { searchValue = "'{0}%'"; }
            else if (toolStripComboBoxString.Text == "End with")
            { searchValue = "'%{0}'"; }
            else if (toolStripComboBoxString.Text == "Contains")
            { searchValue = "'%{0}%'"; }
            else
            { return; }

            if (string.IsNullOrWhiteSpace(toolStripTextBoxValue.Text))
            { return; }

            searchValue = String.Format(searchValue, toolStripTextBoxValue.Text);

            _searchCondition = "{0} LIKE {1} AND status <> 'Disabled'";
            _searchCondition = String.Format(_searchCondition, item, searchValue);

            _search = true;
            _customerPage.PageNumber = 1;
            CustomerPage(_searchCondition);
        }

        private void ToolStripButtonReset_Click(object sender, EventArgs e)
        {
            _search = false;
            _searchCondition = "";
            toolStripComboBoxColumn.SelectedIndex = -1;
            toolStripComboBoxString.SelectedIndex = -1;
            toolStripTextBoxValue.Text = "";
            _customerPage.PageNumber = 1;
            CustomerPage();
        }

        private void ToolStripButtonFirstPage_Click(object sender, EventArgs e)
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

        private void ToolStripButtonPreviousPage_Click(object sender, EventArgs e)
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

        private void ToolStripButtonNextPage_Click(object sender, EventArgs e)
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

        private void ToolStripButtonLastPage_Click(object sender, EventArgs e)
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

        private void ListViewSelectCustomer_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column != _sortColumn)
            {
                _sortColumn = e.Column;
                listViewSelectCustomer.Sorting = SortOrder.Ascending;
            }
            else
            {
                if (listViewSelectCustomer.Sorting == SortOrder.Ascending)
                    listViewSelectCustomer.Sorting = SortOrder.Descending;
                else
                    listViewSelectCustomer.Sorting = SortOrder.Ascending;
            }

            listViewSelectCustomer.Sort();
            this.listViewSelectCustomer.ListViewItemSorter = new ListViewItemComparer(e.Column, listViewSelectCustomer.Sorting);
        }

    }
}

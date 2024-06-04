using QLQuanCaPhe.BUS;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanCaPhe.View
{
    public partial class GUI_BillInfomation : Form
    {
        public GUI_BillInfomation()
        {
            InitializeComponent();
        }

        public GUI_BillInfomation(string username, int customerID, int billID)
        {
            InitializeComponent();
            this.username = username;
            this.customerID = customerID;
            this.billID = billID;
        }

        string username;
        int customerID;
        int billID;

        private void frmBillInfomation_Load(object sender, EventArgs e)
        {
            loadStaffList();
            loadCustomerList();
            Location = new Point(700, 500);
            SelectedComboboxStaff(username);
            if (customerID != 0)
            {
                selectedComboboxCustomer(customerID);
            }
        }

        void loadStaffList()
        {
            List<AccountDTO> list = BUS_Account.Instance.GetAccountList();
            cboStaffName.DataSource = list;
            cboStaffName.DisplayMember = "displayName";
        }

        void SelectedComboboxStaff(string username)
        {
            int i = 0;
            foreach (AccountDTO account in cboStaffName.Items)
            {
                if (account.UserName != username)
                {
                    i++;
                }
                else
                {
                    break;
                }
            }

            cboStaffName.SelectedIndex = i;

        }

        void selectedComboboxCustomer(int custoemrID)
        {
            int i = 0;
            foreach (CustomerDTO customer in cboCustomer.Items)
            {
                if (customer.CustomerID != custoemrID)
                {
                    i++;
                }
                else
                {
                    break;
                }
            }

            cboCustomer.SelectedIndex = i;
        }

        public void loadCustomerList()
        {
            List<CustomerDTO> customerDTOs = BUS_Customer.Instance.getCustomerList();
            cboCustomer.DataSource = customerDTOs;
            cboCustomer.DisplayMember = "customerName";
        }

        private void txtFindStaff_TextChanged(object sender, EventArgs e)
        {
            /*DataTable dt = StaffDAO.Instance.staffFind("", txtFindStaff.Text);
            cboStaffName.DataSource = dt;
            cboStaffName.DisplayMember = "Tên nhân viên";*/
        }

        private void picExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        string staffName = "";
        private void cboStaffName_KeyDown(object sender, KeyEventArgs e)
        {
            if (cboStaffName.Text != "")
            {
                staffName = cboStaffName.Text;
                List<AccountDTO> list = BUS_Account.Instance.FindAccount("", cboStaffName.Text);
                cboStaffName.DataSource = list;
                cboStaffName.DisplayMember = "displayName";
                cboStaffName.Text = staffName;
                cboStaffName.SelectionStart = staffName.Length + 1;
            }
            else
            {
                staffName = "";
                loadStaffList();
            }
        }

        private void cboStaffName_Click(object sender, EventArgs e)
        {
            cboStaffName.DroppedDown = true;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            frmAddCustomer_Bill frmAddCustomer_Bill = new frmAddCustomer_Bill();
            frmAddCustomer_Bill.Show();
            loadCustomerList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (billID != -1)
            {
                AccountDTO account = cboStaffName.SelectedItem as AccountDTO;
                CustomerDTO customer = cboCustomer.SelectedItem as CustomerDTO;
                if (cboCustomer.Text == "")
                {
                    BUS_Bill.Instance.UpdateAddInformationBill(billID, account.UserName);
                }
                else
                {
                    BUS_Bill.Instance.UpdateAddInformationBill(billID, account.UserName, customer.CustomerID);
                }
            }
            Close();
        }
    }
}

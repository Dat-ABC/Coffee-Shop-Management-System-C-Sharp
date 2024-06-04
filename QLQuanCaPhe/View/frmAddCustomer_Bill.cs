using QLQuanCaPhe.BUS;
using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanCaPhe.View
{
    public partial class frmAddCustomer_Bill : Form
    {
        public frmAddCustomer_Bill()
        {
            InitializeComponent();
        }

        private void frmAddCustomer_Bill_Load(object sender, EventArgs e)
        {
            Location = new Point(1460, 500);
            loadComboboxSex();
            loadComboxCustomerType();
        }

        private void picCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        void reset()
        {
            txtCustomerName.Text = "";
            cboSex.SelectedIndex = 0;
            mtxtBirthday.Mask = "00/00/0000";
            txtAddress.Text = "";
            mtxtPhone.Mask = "0000-000-000";
            txtEmail.Text = "";
            cboType.SelectedIndex = 0;
        }

        void loadComboboxSex()
        {
            cboSex.Items.Add("Không xác định");
            cboSex.Items.Add("Nam");
            cboSex.Items.Add("Nữ");
            cboSex.SelectedIndex = 0;
        }

        void loadComboxCustomerType()
        {
            cboType.Items.Add("Khách hàng thường");
            cboType.Items.Add("Khách hàng bạc");
            cboType.Items.Add("Khách hàng vàng");
            cboType.Items.Add("Khách hàng kim cương");
            cboType.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtCustomerName.Text;
            if (MessageBox.Show($"Bạn có chắc chắn muốn thêm khách hàng tên là: {name} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                bool checkEmail = true;
                bool checkPhone = true;
                if (txtEmail.Text != "")
                {
                    checkEmail = CheckFormatDAO.Instance.checkEmail(txtEmail.Text);
                }

                string phone = "";

                if (mtxtPhone.Text != "    -   -")
                {
                    checkPhone = CheckFormatDAO.Instance.checkPhone(mtxtPhone.Text);
                    phone = mtxtPhone.Text;
                }

                bool checkDate = true;

                if (!DateTime.TryParseExact(mtxtBirthday.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date) && DateTime.Now.Year - date.Year < 10 && DateTime.Now.Year - date.Year > 100)
                {
                    checkDate = false;
                }


                if (checkDate && txtCustomerName.Text.Trim() != "" && BUS_Customer.Instance.InsertCustomer(name, cboSex.Text, cboType.Text, phone, mtxtBirthday.Text, txtAddress.Text) && checkEmail && checkPhone)
                {
                    MessageBox.Show($"Thêm khách hàng {name} thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("Thông tin không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                reset();
            }
        }
    }
}

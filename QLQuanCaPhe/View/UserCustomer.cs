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
using System.Windows.Forms.DataVisualization.Charting;

namespace QLQuanCaPhe.View
{
    public partial class UserCustomer : UserControl
    {
        public UserCustomer()
        {
            InitializeComponent();
        }

        private void UserHome_Load(object sender, EventArgs e)
        {
            loadCboSex();
            loadCboType();
            loadDgvCustomer();

            btnFix.Enabled = false;
            btnDelete.Enabled = false;
            btnFix.BackColor = Color.DarkGray;
            btnDelete.BackColor = Color.DarkGray;
        }

        private void UserCustomer_Paint(object sender, PaintEventArgs e)
        {
            int borderWidth = 1;
            Color borderColor = Color.White;

            UserControl userControl = sender as UserControl;

            int borderX = 0;
            int borderY = 2;
            int borderXEnd = userControl.Width;

            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawLine(pen, borderX, borderY, borderXEnd, borderY);
            }
        }

        void loadDgvCustomer()
        {
            dgvCustomer.DataSource = BUS_Customer.Instance.getCustomerList();
            dgvCustomer.Columns["CustomerID"].Visible = false;
            dgvCustomer.Columns["CustomerName"].HeaderText = "Tên khách hàng";
            dgvCustomer.Columns["Phone"].HeaderText = "Số điện thoại";
            dgvCustomer.Columns["Birthday"].HeaderText = "Ngày sinh";
            dgvCustomer.Columns["address"].HeaderText = "Địa chỉ";
            dgvCustomer.Columns["Sex"].HeaderText = "Giới tính";
            dgvCustomer.Columns["TypeName"].HeaderText = "Thành viên";
        }

        void loadCboSex()
        {
            cboSex.Items.Add("Không xác định");
            cboSex.Items.Add("Nam");
            cboSex.Items.Add("Nữ");
            cboSex.SelectedIndex = 0;
        }

        void loadCboType()
        {
            cboCustomerType.Items.Add("Khách hàng thường");
            cboCustomerType.Items.Add("Khách hàng bạc");
            cboCustomerType.Items.Add("Khách hàng vàng");
            cboCustomerType.Items.Add("Khách hàng kim cương");
            cboCustomerType.SelectedIndex = 0;
        }

        void loadCboFind()
        {

        }

        void clean()
        {
            txtCustomerName.Text = "";
            txtAddress.Text = "";
            txtFind.Text = "";
            mtxtBirthday.Mask = "00/00/0000";
            mtxtPhone.Mask = "0000-000-000";
            cboCustomerType.SelectedIndex = 0;
            cboSex.SelectedIndex = 0;
            //cboFind.SelectedIndex = 0;
            btnDelete.Enabled = false;
            btnFix.Enabled = false;
            btnDelete.BackColor = Color.DarkGray;
            btnFix.BackColor = Color.DarkGray;
            customerID = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtCustomerName.Text.Trim();
            if (MessageBox.Show("Bạn có chắc chắn muốn thêm khách hàng " + name + " không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                bool chkPhone = true;
                string phone = "";
                if (mtxtPhone.Text != "    -   -")
                {
                    chkPhone = BUS_CheckFormat.Instance.checkPhone(mtxtPhone.Text);
                    phone = mtxtPhone.Text;
                }

                bool chkBirhtday = true;

                DateTime date;

                if (!DateTime.TryParseExact(mtxtBirthday.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date) && DateTime.Now.Year - date.Year < 10 && DateTime.Now.Year - date.Year > 100)
                {
                    chkBirhtday = false;
                }
                
                if (name != "" && chkPhone && chkBirhtday)
                {
                    if (BUS_Customer.Instance.InsertCustomer(name, cboSex.Text, cboCustomerType.Text, phone, mtxtBirthday.Text, txtAddress.Text))
                    {
                        MessageBox.Show("Thêm khách hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clean();
                        loadDgvCustomer();
                    }
                    else
                    {
                        MessageBox.Show("Khách hàng này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Thông tin không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            string name = txtCustomerName.Text.Trim();
            if (MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin khách hàng " + name + " không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                bool chkPhone = true;
                string phone = "";
                if (mtxtPhone.Text != "    -   -")
                {
                    chkPhone = BUS_CheckFormat.Instance.checkPhone(mtxtPhone.Text);
                    phone = mtxtPhone.Text;
                }

                bool chkBirhtday = true;

                DateTime date;

                if (!DateTime.TryParseExact(mtxtBirthday.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date) && DateTime.Now.Year - date.Year < 10 && DateTime.Now.Year - date.Year > 100)
                {
                    chkBirhtday = false;
                }

                if (name != "" && chkPhone && chkBirhtday)
                {
                    if (BUS_Customer.Instance.UpdateCustomer(customerID, name, cboSex.Text, cboCustomerType.Text, phone, mtxtBirthday.Text, txtAddress.Text))
                    {
                        MessageBox.Show("Sửa thông tin khách hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clean();
                        loadDgvCustomer();
                    }
                    else
                    {
                        MessageBox.Show("Khách hàng này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Thông tin không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        int customerID;

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0 && rowIndex < dgvCustomer.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvCustomer.Rows[rowIndex];
                customerID = (int)selectedRow.Cells["customerID"].Value;
                txtCustomerName.Text = selectedRow.Cells["CustomerName"].Value.ToString();
                txtAddress.Text = selectedRow.Cells["address"].Value.ToString();
                object birthdayValue = selectedRow.Cells["Birthday"].Value;
                if (birthdayValue != null && birthdayValue != DBNull.Value)
                {
                    mtxtBirthday.Text = birthdayValue.ToString();
                }
                mtxtPhone.Text = selectedRow.Cells["phone"].Value.ToString();

                btnFix.Enabled = true;
                btnDelete.Enabled = true;
                btnFix.BackColor = Color.FromArgb(53, 41, 123);
                btnDelete.BackColor = Color.FromArgb(53, 41, 123);

                int i = 0;
                string s = selectedRow.Cells["Sex"].Value.ToString();
                foreach (string sex in cboSex.Items)
                {
                    if (sex != s && s != "")
                    {
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }

                cboSex.SelectedIndex = i;

                int j = 0;
                string t = selectedRow.Cells["typeName"].Value.ToString();

                foreach (string type in cboCustomerType.Items)
                {
                    if (type != t && t != "")
                    {
                        j++;
                    }
                    else
                    {
                        break;
                    }
                }

                cboCustomerType.SelectedIndex = j;
            }
            else
            {
                btnFix.Enabled = false;
                btnDelete.Enabled = false;
                btnFix.BackColor = Color.DarkGray;
                btnDelete.BackColor = Color.DarkGray;
                clean();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string name = txtCustomerName.Text.Trim();
            if (MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin khách hàng " + name + " không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (BUS_Customer.Instance.DeleteCustomer(customerID))
                {
                    MessageBox.Show("Xóa thông tin khách hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clean();
                    loadDgvCustomer();
                }
                else
                {
                    MessageBox.Show("Khách hàng này không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            if (txtFind.Text != "")
            {
                dgvCustomer.DataSource = BUS_Customer.Instance.FindCustomer(txtFind.Text);
                dgvCustomer.Columns["CustomerID"].Visible = false;
                dgvCustomer.Columns["CustomerName"].HeaderText = "Tên khách hàng";
                dgvCustomer.Columns["Phone"].HeaderText = "Số điện thoại";
                dgvCustomer.Columns["Birthday"].HeaderText = "Ngày sinh";
                dgvCustomer.Columns["address"].HeaderText = "Địa chỉ";
                dgvCustomer.Columns["Sex"].HeaderText = "Giới tính";
                dgvCustomer.Columns["TypeName"].HeaderText = "Thành viên";
            }
            else
            {
                loadDgvCustomer();
            }
        }
    }
}

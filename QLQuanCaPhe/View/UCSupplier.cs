using QLQuanCaPhe.BUS;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace QLQuanCaPhe.View
{
    public partial class UCSupplier : UserControl
    {
        public UCSupplier()
        {
            InitializeComponent();
        }

        private void UCSupplier_Load(object sender, EventArgs e)
        {
            loadCboFind();
            loadDgvSupplier();
        }

        void loadCboFind()
        {
            cboFind.Items.Clear();
            cboFind.Items.Add("Tên nhà cung cấp");
            cboFind.Items.Add("Địa chỉ");
            cboFind.Items.Add("Số điện thoại");
            cboFind.SelectedIndex = 0;
        }

        void loadDgvSupplier(List<SupplierDTO> list = null)
        {
            if (list == null)
            {
                list = BUS_Supplier.Instance.GetSupplierList();
            }

            dgvSupplier.DataSource = list;
            dgvSupplier.Columns["SupplierID"].Visible = false;
            dgvSupplier.Columns["SupplierName"].HeaderText = "Nhà cung cấp";
            dgvSupplier.Columns["address"].HeaderText = "Địa chỉ";
            dgvSupplier.Columns["phone"].HeaderText = "Số điện thoại";
            dgvSupplier.Columns["email"].HeaderText = "Email";
        }

        private void UCSupplier_Paint(object sender, PaintEventArgs e)
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

        void clean()
        {
            txtName.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
            mtxtPhone.Mask = "0000-000-000";
            cboFind.SelectedIndex = 0;
            txtFind.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            if (MessageBox.Show("Bạn có chắc chắn muốn thêm nhà cung cấp " + name + " không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                bool chkPhone = true;
                string phone = "";
                if (mtxtPhone.Text != "    -   -")
                {
                    chkPhone = BUS_CheckFormat.Instance.checkPhone(mtxtPhone.Text);
                    phone = mtxtPhone.Text;
                }

                bool chkEmail = true;
                if (txtEmail.Text != "")
                {
                    chkEmail = BUS_CheckFormat.Instance.checkEmail(txtEmail.Text);
                }


                if (name != "" && chkPhone && chkEmail)
                {
                    if (BUS_Supplier.Instance.InsertSupplier(name, txtAddress.Text, phone, txtEmail.Text))
                    {
                        MessageBox.Show("Thêm nhà cung cấp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clean();
                        loadDgvSupplier();
                    }
                    else
                    {
                        MessageBox.Show("Nhà cung cấp này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            string name = txtName.Text.Trim();
            if (MessageBox.Show("Bạn có chắc chắn muốn sửa thông tin nhà cung cấp không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                bool chkPhone = true;
                string phone = "";
                if (mtxtPhone.Text != "    -   -")
                {
                    chkPhone = BUS_CheckFormat.Instance.checkPhone(mtxtPhone.Text);
                    phone = mtxtPhone.Text;
                }

                bool chkEmail = true;
                if (txtEmail.Text != "")
                {
                    chkEmail = BUS_CheckFormat.Instance.checkEmail(txtEmail.Text);
                }


                if (name != "" && chkPhone && chkEmail)
                {
                    if (BUS_Supplier.Instance.UpdateSupplier(supplierID, name, txtAddress.Text, phone, txtEmail.Text))
                    {
                        MessageBox.Show("Sửa thông tin nhà cung cấp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clean();
                        loadDgvSupplier();
                    }
                    else
                    {
                        MessageBox.Show("Nhà cung cấp này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Thông tin không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        int supplierID;

        private void dgvSupplier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0 && rowIndex < dgvSupplier.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvSupplier.Rows[rowIndex];
                txtName.Text = selectedRow.Cells["supplierName"].Value.ToString();
                txtAddress.Text = selectedRow.Cells["address"].Value.ToString();
                mtxtPhone.Text = selectedRow.Cells["phone"].Value.ToString();
                txtEmail.Text = selectedRow.Cells["email"].Value.ToString();

                supplierID = (int)selectedRow.Cells["SupplierID"].Value;

                btnFix.Enabled = true;
                btnDelete.Enabled = true;
                btnFix.BackColor = Color.FromArgb(53, 41, 123);
                btnDelete.BackColor = Color.FromArgb(53, 41, 123);
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
            string name = txtName.Text.Trim();
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa thông tin {name} không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {

                if (BUS_Supplier.Instance.DeleteSupplier(supplierID))
                {
                    MessageBox.Show("Xóa thông tin nhà cung cấp thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clean();
                    loadDgvSupplier();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            if (txtFind.Text != "")
            {
                if (cboFind.SelectedIndex == 0)
                {
                    List<SupplierDTO> list = BUS_Supplier.Instance.findSupplier(txtFind.Text);
                    loadDgvSupplier(list);
                }
                else if (cboFind.SelectedIndex == 1)
                {
                    List<SupplierDTO> list = BUS_Supplier.Instance.findSupplier("", txtFind.Text);
                    loadDgvSupplier(list);
                }
                else if (cboFind.SelectedIndex == 2)
                {
                    List<SupplierDTO> list = BUS_Supplier.Instance.findSupplier("", "", txtFind.Text);
                    loadDgvSupplier(list);
                }
            }
            else
            {
                loadDgvSupplier();
            }
        }
    }
}

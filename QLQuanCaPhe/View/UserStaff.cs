using QLQuanCaPhe.BUS;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QLQuanCaPhe.View
{
    public partial class UserStaff : UserControl
    {
        public UserStaff()
        {
            InitializeComponent();
        }

        private void UserStaff_Load(object sender, EventArgs e)
        {
            loadComboBoxType();
            loadComboBoxFind();
            loadAccountList();
            btnFix.Enabled = false;
            btnDelete.Enabled = false;
            btnFix.BackColor = Color.DarkGray;
            btnDelete.BackColor = Color.DarkGray;
            loadCheckBox();
        }

        void loadComboBoxFind()
        {
            cboFilter.Items.Add("Tên tài khoản");
            cboFilter.Items.Add("Tên người dùng");
            cboFilter.SelectedIndex = 0;
        }

        void loadComboBoxType()
        {
            cboType.DataSource = BUS_Account.Instance.GetAccountTypeList();
            cboType.DisplayMember = "TypeName";
        }
        

        void loadCheckBox()
        {
            CheckBox[] functionAdmin = new CheckBox[]
            {
                chkSellAdmin, chkMenuAdmin, chkAreaAdmin, chkCustomerAdmin, chkBillAdmin,
                chkSupplierAdmin, chkImportAdmin, chkStatisticalAdmin, chkShiftAdmin, chkDiaryAdmin, chkQLUserAdmin
            };

            CheckBox[] functionQL = new CheckBox[]
            {
                chkSellQL, chkMenuQL, chkAreaQL, chkCustomerQL, chkBillQL,
                chkSupplierQL, chkImportQL, chkStatisticalQL, chkShiftQL, chkDiaryQL, chkQLUserQL
            };

            CheckBox[] functionStaff = new CheckBox[]
            {
                chkSellStaff, chkMenuStaff, chkAreaStaff, chkCustomerStaff, chkBillStaff,
                chkSupplierStaff, chkImportStaff, chkStatisticalStaff, chkShiftStaff, chkDiaryStaff, chkQLUserStaff
            };

            List<DecentralizaionDTO> admin = BUS_Decentralizaion.Instance.GetDecentralization(3);
            List<DecentralizaionDTO> ql = BUS_Decentralizaion.Instance.GetDecentralization(2);
            List<DecentralizaionDTO> staff = BUS_Decentralizaion.Instance.GetDecentralization(1);

            int i = 0;

            foreach (DecentralizaionDTO dto in admin)
            {
                if (dto.Status == 1)
                {
                    functionAdmin[i].Checked = true;
                }
                else
                {
                    functionAdmin[i].Checked = false;
                }
                i++;
            }

            i = 0;

            foreach (DecentralizaionDTO dto in ql)
            {
                if (dto.Status == 1)
                {
                    functionQL[i].Checked = true;
                }
                else
                {
                    functionQL[i].Checked = false;
                }
                i++;
            }

            i = 0;

            foreach (DecentralizaionDTO dto in staff)
            {
                if (dto.Status == 1)
                {
                    functionStaff[i].Checked = true;
                }
                else
                {
                    functionStaff[i].Checked= false;
                }
                i++;
            }
        }

        void loadAccountList()
        {
            dgvUser.Rows.Clear();
            List<AccountDTO> list = BUS_Account.Instance.GetAccountList();

            foreach (AccountDTO account in list)
            {
                int rowIndex = dgvUser.Rows.Add();

                // Lấy hàng vừa thêm vào
                DataGridViewRow row = dgvUser.Rows[rowIndex];

                // Gán giá trị từ đối tượng FoodDTO vào các ô tương ứng trong hàng
                row.Cells["AccountName"].Value = account.UserName;
                row.Cells["Password"].Value = account.Password;
                row.Cells["Username"].Value = account.DisplayName;
                row.Cells["userPhoto"].Value = account.OwnerPhoto;
                row.Cells["Email"].Value = account.Email;
                row.Cells["phone"].Value = account.Phone;
                row.Cells["TypeAccount"].Value = account.TypeName;
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            TableLayoutPanel tlp = sender as TableLayoutPanel;
            if (tlp != null)
            {
                foreach (Control control in tlp.Controls)
                {
                    if (control is CheckBox)
                    {
                        CheckBox checkBox = control as CheckBox;
                        Rectangle rect = tlp.RectangleToScreen(checkBox.Bounds);
                        rect.Location = tlpCheckBox.PointToClient(rect.Location);
                        rect.Inflate(1, 1);
                        ControlPaint.DrawBorder(e.Graphics, rect, Color.Black, ButtonBorderStyle.Solid);
                    }
                }
            }
        }

        private void UserStaff_Paint(object sender, PaintEventArgs e)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtUserName.Text;
            if (name != "" && txtDisplayName.Text != "" && txtEmail.Text != "")
            {
                if (MessageBox.Show($"Bạn có chắc chắn muốn thêm người dùng {name} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (txtPassword1.Text == txtPassword2.Text && txtPassword1.Text != "")
                    {
                        int type = cboType.SelectedIndex;
                        string phone = mtxtPhone.Text;

                        bool checkPhone = true;
                        if (phone != "    -   -")
                        {
                            checkPhone = BUS_CheckFormat.Instance.checkPhone(phone);
                        }
                        else
                        {
                            phone = "";
                        }

                        if (checkPhone && BUS_CheckFormat.Instance.checkEmail(txtEmail.Text))
                        {
                            if (BUS_Account.Instance.InsertAccount(name, txtDisplayName.Text, picUser.Image, txtEmail.Text, phone, txtPassword1.Text, type + 1))
                            {
                                MessageBox.Show("Thêm thông tin người dùng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loadAccountList();
                            }
                            else
                            {
                                MessageBox.Show("Tên tài khoản này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Thông tin không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hai mật khẩu không giống nhau", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc tên người dùng hoặc email không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                txtUserName.Enabled = true;
            }
            try
            {
                if (dgvUser.SelectedCells[0].OwningRow != null && dgvUser.SelectedCells[0].OwningRow.Cells[0].Value != null)
                {
                    string type = dgvUser.SelectedCells[0].OwningRow.Cells["TypeAccount"].Value.ToString();

                    int index = -1;
                    int i = 0;
                    foreach (AccountTypeDTO item in cboType.Items)
                    {
                        if (item.TypeName == type)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }

                    cboType.SelectedIndex = index;
                }
            }
            catch { }
        }

        private void dgvUser_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvUser.Columns[e.ColumnIndex].Name == "Password" && e.RowIndex >= 0)
            {
                string password = e.Value as string;

                if (!string.IsNullOrEmpty(password))
                {
                    e.Value = new string('●', password.Length);
                }
                else
                {
                    e.Value = string.Empty;
                }
            }
        }

        void refesh()
        {
            txtUserName.Text = "";
            txtPassword1.Text = "";
            txtPassword2.Text = "";
            txtDisplayName.Text = "";
            picUser.Image = null;
            mtxtPhone.Text = "";
            txtEmail.Text = "";
            cboType.SelectedIndex = 0;
        }

        private void btnFix_Click_1(object sender, EventArgs e)
        {
            string name = txtUserName.Text;
            if (txtDisplayName.Text != "" && txtEmail.Text != "")
            {
                if (MessageBox.Show($"Bạn có chắc chắn muốn sửa thông tin người dùng {name} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    AccountTypeDTO accountType = cboType.SelectedItem as AccountTypeDTO;
                    if (txtUserName.Text == "admin" && accountType.TypeID != 3)
                    {
                        MessageBox.Show("Tài khoản admin phải có quyền quản trị", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (txtPassword1.Text == txtPassword2.Text && txtPassword1.Text != "")
                        {
                            int type = cboType.SelectedIndex;
                            string phone = mtxtPhone.Text;

                            bool checkPhone = true;
                            if (phone != "    -   -")
                            {
                                checkPhone = BUS_CheckFormat.Instance.checkPhone(phone);
                            }
                            else
                            {
                                phone = "";
                            }

                            if (checkPhone && BUS_CheckFormat.Instance.checkEmail(txtEmail.Text) && BUS_Account.Instance.UpdateAccount(name, txtDisplayName.Text, picUser.Image, txtEmail.Text, phone, txtPassword1.Text, type + 1))
                            {

                                MessageBox.Show("Sửa thông tin người dùng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                loadAccountList();
                                refesh();
                            }
                            else
                            {
                                MessageBox.Show("Thông tin không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Hai mật khẩu không giống nhau", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Tên người dùng hoặc email không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void picUser_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png; *.jpg; *.jpeg; *.gif; *.bmp)|*.png; *.jpg; *.jpeg; *.gif; *.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                picUser.Image = Image.FromFile(filePath);
            }
            else
            {
                picUser.Image = null;
            }

        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0 && rowIndex < dgvUser.Rows.Count)
            {
                txtUserName.Enabled = false;
                DataGridViewRow selectedRow = dgvUser.Rows[rowIndex];
                txtUserName.Text = selectedRow.Cells["AccountName"].Value.ToString();
                txtPassword1.Text = selectedRow.Cells["Password"].Value.ToString();
                txtPassword2.Text = selectedRow.Cells["Password"].Value.ToString();
                txtDisplayName.Text = selectedRow.Cells["username"].Value.ToString();
                picUser.Image = (Image)selectedRow.Cells["userPhoto"].Value;
                txtEmail.Text = selectedRow.Cells["Email"].Value.ToString();
                mtxtPhone.Text = selectedRow.Cells["Phone"].Value.ToString();
                btnFix.Enabled = true;
                btnDelete.Enabled = true;
                btnFix.BackColor = Color.FromArgb(53, 41, 123);
                btnDelete.BackColor = Color.FromArgb(53, 41, 123);
            }
            else
            {
                txtUserName.Enabled = true;
                btnFix.Enabled = false;
                btnDelete.Enabled = false;
                btnFix.BackColor = Color.DarkGray;
                btnDelete.BackColor = Color.DarkGray;
                refesh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refesh();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            string name = txtUserName.Text;
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa thông tin người dùng {name} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (txtUserName.Text != "admin")
                {
                    if (BUS_Account.Instance.DeleteAccount(name))
                    {

                        MessageBox.Show("Xóa thông tin người dùng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadAccountList();
                        refesh();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi khi xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Không thể xóa tài khoản admin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtFind_TextChanged_1(object sender, EventArgs e)
        {
            if (txtFind.Text != "")
            {
                string searchText = txtFind.Text.ToLower();
                string selectedColumn;

                if (cboFilter.SelectedIndex == 0)
                {
                    selectedColumn = "UserName";
                }
                else
                {
                    selectedColumn = "DisplayName";

                }

                foreach (DataGridViewRow row in dgvUser.Rows)
                {
                    object cellValue = row.Cells[selectedColumn].Value;
                    if (cellValue != null && cellValue != DBNull.Value && cellValue.ToString().ToLower().Contains(searchText))
                    {
                        row.Selected = true;
                        dgvUser.CurrentCell = row.Cells[0];
                        dgvUser.FirstDisplayedScrollingRowIndex = row.Index; // Di chuyển để hiển thị hàng trên màn hình
                        break;
                    }
                }
            }
            else
            {
                dgvUser.FirstDisplayedScrollingRowIndex = 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string[] functionAdmin = new string[]
            {
                chkSellAdmin.Name, chkMenuAdmin.Name, chkAreaAdmin.Name, chkCustomerAdmin.Name, chkBillAdmin.Name,
                chkSupplierAdmin.Name, chkImportAdmin.Name, chkStatisticalAdmin.Name, chkShiftAdmin.Name, chkDiaryAdmin.Name, chkQLUserAdmin.Name
            };

            string[] functionQL = new string[]
            {
                chkSellQL.Name, chkMenuQL.Name, chkAreaQL.Name, chkCustomerQL.Name, chkBillQL.Name,
                chkSupplierQL.Name, chkImportQL.Name, chkStatisticalQL.Name, chkShiftQL.Name, chkDiaryQL.Name, chkQLUserQL.Name
            };

            string[] functionStaff = new string[]
            {
                chkSellStaff.Name, chkMenuStaff.Name, chkAreaStaff.Name, chkCustomerStaff.Name, chkBillStaff.Name,
                chkSupplierStaff.Name, chkImportStaff.Name, chkStatisticalStaff.Name, chkShiftStaff.Name, chkDiaryStaff.Name, chkQLUserStaff.Name
            };

            DataTable dt = new DataTable();
            dt.Columns.Add("TypeID", typeof(int));
            dt.Columns.Add("FunctionID", typeof(int));
            dt.Columns.Add("Status", typeof(int));

            int i = 12;
            int j = 12;
            int k = 12;

            foreach (Control control in tlpCheckBox.Controls)
            {
                CheckBox chk = control as CheckBox;
                if (functionAdmin.Contains(chk.Name))
                {
                    i--;
                    int status = chk.Checked ? 1 : 0;
                    dt.Rows.Add(3, i, status);
                }
                else if (functionQL.Contains(chk.Name))
                {
                    j--;
                    int status = chk.Checked ? 1 : 0;
                    dt.Rows.Add(2, j, status);
                }
                else if (functionStaff.Contains(chk.Name))
                {
                    k--;
                    int status = chk.Checked ? 1 : 0;
                    dt.Rows.Add(1, k, status);
                }
            }

            if (BUS_Decentralizaion.Instance.UpdateDecentralization(dt))
            {
                MessageBox.Show("Cập nhập thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadCheckBox();
            }
            else
            {
                MessageBox.Show("Cập nhập thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

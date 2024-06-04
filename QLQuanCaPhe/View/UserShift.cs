using QLQuanCaPhe.BUS;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanCaPhe.View
{
    public partial class UserShift : UserControl
    {
        public UserShift()
        {
            InitializeComponent();
        }

        private void UserShift_Load(object sender, EventArgs e)
        {
            loadDgvShift();
            loadCboFind();
            loadDgvShiftDivision();
            loadCboShift();
            loadCboStaff();
        }

        #region Shift
        void loadDgvShift(List<ShiftDTO> list = null)
        {
            if (list == null)
            {
                list = BUS_Shift.Instance.GetShiftList();
            }
            dgvShift.DataSource = list;
            dgvShift.Columns["ShiftID"].Visible = false;
            dgvShift.Columns["ShiftName"].HeaderText = "Ca làm";
            dgvShift.Columns["StartTime"].HeaderText = "Thời gian bắt đầu";
            dgvShift.Columns["EndTime"].HeaderText = "Thời gian kết thúc";
        }

        void cleanShift()
        {
            txtShiftName.Text = "";
            txtShiftName.Text = "";
            dtpStart.Value = DateTime.Now;
            dtpEnd.Value = DateTime.Now;

            dtpEnd.Value = dtpEnd.Value.AddHours(1);

        }

        private void UserShift_Paint(object sender, PaintEventArgs e)
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

        private void btnAddShift_Click(object sender, EventArgs e)
        {
            string name = txtShiftName.Text;
            if (name.Trim() != "")
            {
                if (MessageBox.Show($"Bạn có muốn thêm ca làm việc là: {name} không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    if (BUS_Shift.Instance.InsertShift(name, dtpStart.Value, dtpEnd.Value))
                    {
                        MessageBox.Show("Thêm ca làm việc thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDgvShift();
                        cleanShift();
                        loadCboShift();
                    }
                    else
                    {
                        MessageBox.Show("Tên ca làm việc này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Tên không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnFixShift_Click(object sender, EventArgs e)
        {
            string name = txtShiftName.Text.Trim();
            if (name != "")
            {
                if (MessageBox.Show($"Bạn có muốn sửa thông tin ca làm việc không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    ShiftDTO shift = BUS_Shift.Instance.GetShiftByName(shiftName);
                    if (BUS_Shift.Instance.UpdateShift(shiftID, name, dtpStart.Value, dtpEnd.Value))
                    {
                        if (shift.ShiftName != name)
                        {
                            BUS_Diary.Instance.InsertDiary(DateTime.Now, "Quản lý ca làm việc", $"Sửa ca làm việc {shift.ShiftName} thành {name}", BUS_Account.DisplayName);
                        }
                        MessageBox.Show("Sửa thông tin ca làm việc thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDgvShift();
                        cleanShift();
                        loadCboShift();
                    }
                    else
                    {
                        MessageBox.Show("Tên ca làm việc này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Tên không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeleteShift_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Bạn có muốn xóa thông tin ca làm việc không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                ShiftDTO shift = BUS_Shift.Instance.GetShiftByName(shiftName);
                if (BUS_Shift.Instance.DeleteShift(shiftID))
                {
                    BUS_Diary.Instance.InsertDiary(DateTime.Now, "Quản lý ca làm việc", $"Xóa ca làm việc {shift.ShiftName}", BUS_Account.DisplayName);
                    MessageBox.Show("Xóa thông tin ca làm việc thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDgvShift();
                    cleanShift();
                    loadCboShift();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCleanShift_Click(object sender, EventArgs e)
        {
            cleanShift();
        }

        int shiftID;
        string shiftName;

        private void dgvShift_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0 && rowIndex < dgvShift.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvShift.Rows[rowIndex];
                shiftName = txtShiftName.Text = selectedRow.Cells["shiftName"].Value.ToString();
                TimeSpan startTime = (TimeSpan)selectedRow.Cells["startTime"].Value;
                TimeSpan endTime = (TimeSpan)selectedRow.Cells["endTime"].Value;

                // Tạo đối tượng DateTime mới với ngày hiện tại và thời gian từ TimeSpan
                DateTime currentDate = DateTime.Now.Date; // Chỉ lấy phần ngày, bỏ qua phần thời gian

                // Gán giá trị mới cho DateTimePicker
                dtpStart.Value = currentDate + startTime;
                dtpEnd.Value = currentDate + endTime;


                shiftID = (int)dgvShift.Rows[rowIndex].Cells["shiftID"].Value;

                btnFixShift.Enabled = true;
                btnDeleteShift.Enabled = true;
                btnFixShift.BackColor = Color.FromArgb(53, 41, 123);
                btnDeleteShift.BackColor = Color.FromArgb(53, 41, 123);
            }
            else
            {
                btnFixShift.Enabled = false;
                btnDeleteShift.Enabled = false;
                btnFixShift.BackColor = Color.DarkGray;
                btnDeleteShift.BackColor = Color.DarkGray;
                cleanShift();
            }
        }

        #endregion

        void loadCboStaff()
        {
            cboStaff.DataSource = BUS_Account.Instance.GetAccountList();
            cboStaff.DisplayMember = "DisplayName";
            if (cboStaff.Items.Count < 1)
            {
                btnAdd.Enabled = false;
                btnAdd.BackColor = Color.DarkGray;
            }
            else
            {
                btnAdd.Enabled = true;
                btnAdd.BackColor = Color.FromArgb(53, 41, 123);
            }
        }

        void loadCboShift()
        {
            cboShift.DataSource = BUS_Shift.Instance.GetShiftList();
            cboShift.DisplayMember = "ShiftName";
            if (cboStaff.Items.Count < 1)
            {
                btnAdd.Enabled = false;
                btnAdd.BackColor = Color.DarkGray;
            }
            else
            {
                btnAdd.Enabled = true;
                btnAdd.BackColor = Color.FromArgb(53, 41, 123);
            }
        }

        void loadDgvShiftDivision(List<ShiftDivisionDTO> list = null)
        {
            if (list == null)
            {
                list = BUS_ShiftDivision.Instance.GetShiftDivisionList();
            }
            dgvShiftDivision.DataSource = list;
            dgvShiftDivision.Columns["shiftID"].Visible = false;
            dgvShiftDivision.Columns["username"].Visible = false;
            dgvShiftDivision.Columns["shiftID"].HeaderText = "Mã ca làm";
            dgvShiftDivision.Columns["username"].HeaderText = "Tài khoản";

            dgvShiftDivision.Columns["workday"].HeaderText = "Ngày";
            dgvShiftDivision.Columns["shiftName"].HeaderText = "Ca làm";
            dgvShiftDivision.Columns["displayName"].HeaderText = "Nhân viên";
        }

        void loadCboFind()
        {
            cboFind.Items.Clear();
            cboFind.Items.Add("Ngày làm");
            cboFind.Items.Add("Nhân viên");
            cboFind.SelectedIndex = 0;
        }

        void cleanShiftDivision()
        {
            cboStaff.SelectedIndex = 0;
            cboShift.SelectedIndex = 0;
            dtpWorkDay.Value = DateTime.Now;
        }

        private void btnCleanShiftDivision_Click(object sender, EventArgs e)
        {
            cleanShiftDivision();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Bạn có muốn thêm thông tin phân ca làm việc không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                AccountDTO account = cboStaff.SelectedItem as AccountDTO;
                ShiftDTO shift = cboShift.SelectedItem as ShiftDTO;
                if (dtpWorkDay.Value <= DateTime.Now.AddMonths(3) && dtpWorkDay.Value >= DateTime.Now.AddMonths(-3))
                {
                    if (BUS_ShiftDivision.Instance.InsertShiftDivision(shift.ShiftID, account.UserName, dtpWorkDay.Value))
                    {
                        MessageBox.Show("Thêm thông tin phân ca làm việc thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDgvShiftDivision();
                        cleanShiftDivision();
                    }
                    else
                    {
                        MessageBox.Show($"Nhân viên {account.DisplayName} đã làm ca {shift.ShiftName} ngày {dtpWorkDay.Value.ToString("dd/MM/yyyy")}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Ngày làm việc phải lớn hơn 3 tháng trước và nhỏ 3 tháng sau", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Bạn có muốn sửa thông tin phân ca làm việc không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                AccountDTO account = cboStaff.SelectedItem as AccountDTO;
                ShiftDTO shift = cboShift.SelectedItem as ShiftDTO;
                if (dtpWorkDay.Value < DateTime.Now.AddDays(101) && dtpWorkDay.Value > DateTime.Now.AddDays(-101))
                {
                    if (BUS_ShiftDivision.Instance.UpdateShiftDivision(shift.ShiftID, account.UserName, dtpWorkDay.Value))
                    {
                        MessageBox.Show("Sửa thông tin phân ca làm việc thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDgvShiftDivision();
                        cleanShiftDivision();
                    }
                    else
                    {
                        MessageBox.Show($"Nhân viên {account.DisplayName} đã làm ca {shift.ShiftName} ngày {dtpWorkDay.Value.Date}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Ngày làm việc phải lớn hơn 3 tháng trước và nhỏ 3 tháng sau", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Bạn có muốn xóa thông tin phân ca làm việc không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                AccountDTO account = cboStaff.SelectedItem as AccountDTO;
                ShiftDTO shift = cboShift.SelectedItem as ShiftDTO;
                if (BUS_ShiftDivision.Instance.UpdateShiftDivision(shift.ShiftID, account.UserName, dtpWorkDay.Value))
                {
                    MessageBox.Show("Xóa thông tin phân ca làm việc thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDgvShiftDivision();
                    cleanShiftDivision();
                }
                else
                {
                    MessageBox.Show($"Xóa thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        int shiftID2;
        string username;

        private void dgvShiftDivision_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0 && rowIndex < dgvShiftDivision.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvShiftDivision.Rows[rowIndex];
                dtpWorkDay.Value = (DateTime)selectedRow.Cells["workday"].Value;



                shiftID2 = (int)selectedRow.Cells["shiftID"].Value;
                int index3 = -1;
                int i = 0;
                foreach (ShiftDTO shift in cboShift.Items)
                {
                    if (shift.ShiftID == shiftID2)
                    {
                        index3 = i;
                    }
                    i++;
                }

                cboShift.SelectedIndex = index3;

                username = selectedRow.Cells["username"].Value.ToString();

                index3 = -1;
                i = 0;
                foreach (ShiftDTO shift in cboShift.Items)
                {
                    if (shift.ShiftID == shiftID2)
                    {
                        index3 = i;
                    }
                    i++;
                }

                cboStaff.SelectedIndex = index3;

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
                cleanShiftDivision();
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(Type.Missing);

            for (int i = 1; i < dgvShiftDivision.Columns.Count + 1; i++)
            {
                excel.Cells[1, i] = dgvShiftDivision.Columns[i - 1].HeaderText;
            }

            for (int i = 0; i < dgvShiftDivision.Rows.Count; i++)
            {
                for (int j = 1; j < dgvShiftDivision.Columns.Count + 1; j++)
                {
                    excel.Cells[i + 2, j] = dgvShiftDivision.Rows[i].Cells[j - 1].Value.ToString();
                }
            }

            excel.Columns.AutoFit();

            // Đặt đường dẫn và tên tệp Excel mới
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx;*.xls |All Files|*.*";
            saveFileDialog.FileName = "Tên_Tệp_Mới.xlsx";

            // Mở hộp thoại để người dùng chọn vị trí và tên tệp Excel mới
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                excel.ActiveWorkbook.SaveAs(saveFileDialog.FileName);
                BUS_Diary.Instance.InsertDiary(DateTime.Now, "Xuất phân ca làm việc", $"Xuất danh sách phân ca làm việc", BUS_Account.DisplayName);
                MessageBox.Show("Xuất Excel thành công!");
            }
            excel.Quit();
        }

        private void btnImportToExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("shiftDivison", typeof(int));
            dt.Columns.Add("username", typeof(string));
            dt.Columns.Add("Workday", typeof(DateTime));

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application App = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook workbook = App.Workbooks.Open(openFileDialog.FileName);
                    Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.Worksheets["Sheet1"];
                    Microsoft.Office.Interop.Excel.Range range = worksheet.UsedRange;

                    for (int i = 2; i < range.Rows.Count + 1; i++)
                    {
                        int shiftID = (int)range.Cells[i, 1].Value;
                        string username = range.Cells[i, 2].Value.ToString();
                        DateTime workday = (DateTime)range.Cells[i, 3].Value;

                        username = Encryption.Instance.Encrypt(username);

                        dt.Rows.Add(new object[] { shiftID, username, workday });
                    }
                    workbook.Close();
                    App.Quit();
                }
            }
            catch { }
            if (dt.Rows.Count > 0 && BUS_ShiftDivision.Instance.InsertShiftDivisionToExcel(dt))
            {
                BUS_Diary.Instance.InsertDiary(DateTime.Now, "Nhập phân ca làm việc", "Nhập danh sách phân ca làm việc", BUS_Account.DisplayName);
                MessageBox.Show("Nhập dữ liệu từ excel thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDgvShiftDivision();
            }
            else
            {
                MessageBox.Show("Có lỗi khi nhập dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            if (txtFind.Text.Length > 0)
            {
                List<ShiftDTO> list = BUS_Shift.Instance.FindShift(txtFind.Text);
                loadDgvShift(list);
            }
            else
            {
                loadDgvShift();
            }
        }

        private void txtFindShiftDivision_TextChanged(object sender, EventArgs e)
        {
            if (txtFindShiftDivision.Text.Length > 0)
            {
                if (cboFind.SelectedIndex == 0)
                {
                    List<ShiftDivisionDTO> list = BUS_ShiftDivision.Instance.FindShiftDivision(txtFindShiftDivision.Text);
                    loadDgvShiftDivision(list);
                }
                else if (cboFind.SelectedIndex == 1)
                {
                    List<ShiftDivisionDTO> list = BUS_ShiftDivision.Instance.FindShiftDivision("", txtFindShiftDivision.Text);
                    loadDgvShiftDivision(list);
                }
            }
            else
            {
                loadDgvShiftDivision();
            }
        }
    }
}

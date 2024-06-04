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
    public partial class UserDiary : UserControl
    {
        public UserDiary()
        {
            InitializeComponent();
        }

        private void UserDiary_Load(object sender, EventArgs e)
        {
            loadCboActivity();
            loadCboStaff();
        }

        private void UserDiary_Paint(object sender, PaintEventArgs e)
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

        void loadCboActivity()
        {
            cboType.Items.Add("Loại hoạt động");
            cboType.Items.Add("Xem thống kê");
            cboType.Items.Add("Xuất báo cáo");
            cboType.Items.Add("Quản lý món ăn");
            cboType.Items.Add("Xuất hóa đơn nhập");
            cboType.Items.Add("Xuất hóa đơn xuất");
            cboType.Items.Add("Xuất danh sách mặt hàng");
            cboType.Items.Add("Xuất danh sách hàng tồn kho");
            cboType.Items.Add("Xuất danh sách món ăn");
            cboType.Items.Add("Nhập danh sách món ăn");
            cboType.Items.Add("Xuất phân ca làm việc");
            cboType.Items.Add("Nhập phân ca làm việc");
            cboType.Items.Add("Quản lý ca làm việc");
            cboType.SelectedIndex = 0;
        }

        void loadCboStaff()
        {
            AccountDTO account = new AccountDTO() { DisplayName = "Nhân viên" };
            List<AccountDTO> list = new List<AccountDTO>();
            List<AccountDTO> accounts = BUS_Account.Instance.GetAccountList();
            list.Add(account);
            list.AddRange(accounts);
            cboStaff.DataSource = list;
            cboStaff.DisplayMember = "DisplayName";
            cboStaff.SelectedIndex = 0;
        }

        void loadDgvDiary(DateTime start, DateTime end, string activity, string implementer)
        {
            if (activity == "Loại hoạt động" && implementer == "Nhân viên")
            {
                dgvDiary.DataSource = BUS_Diary.Instance.GetDiaryList(start, end.AddDays(1));
            }
            else if (activity != "Loại hoạt động" && implementer == "Nhân viên")
            {
                dgvDiary.DataSource = BUS_Diary.Instance.GetDiaryList(start, end.AddDays(1), activity);
            }
            else if (activity == "Loại hoạt động" && implementer != "Nhân viên")
            {
                dgvDiary.DataSource = BUS_Diary.Instance.GetDiaryList(start, end.AddDays(1), "", implementer);
            }
            else
            {
                dgvDiary.DataSource = BUS_Diary.Instance.GetDiaryList(start, end.AddDays(1), activity, implementer);
            }

            dgvDiary.Columns["DiaryID"].Visible = false;
            dgvDiary.Columns["executionTime"].HeaderText = "Thời gian";
            dgvDiary.Columns["executionTime"].FillWeight = 25;
            dgvDiary.Columns["activity"].HeaderText = "Hoạt động";
            dgvDiary.Columns["activity"].FillWeight = 40;
            dgvDiary.Columns["detail"].HeaderText = "Chi tiết";
            dgvDiary.Columns["implementer"].HeaderText = "Thực hiện";
            dgvDiary.Columns["implementer"].FillWeight = 30;
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDgvDiary(dtpStart.Value, dtpEnd.Value, cboType.Text, cboStaff.Text);
        }

        private void cboStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDgvDiary(dtpStart.Value, dtpEnd.Value, cboType.Text, cboStaff.Text);
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            loadDgvDiary(dtpStart.Value, dtpEnd.Value, cboType.Text, cboStaff.Text);
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            loadDgvDiary(dtpStart.Value, dtpEnd.Value, cboType.Text, cboStaff.Text);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(Type.Missing);

            for (int i = 1; i < dgvDiary.Columns.Count; i++)
            {
                excel.Cells[1, i] = dgvDiary.Columns[i].HeaderText;
            }

            for (int i = 0; i < dgvDiary.Rows.Count; i++)
            {
                for (int j = 1; j < dgvDiary.Columns.Count; j++)
                {
                    excel.Cells[i + 2, j] = dgvDiary.Rows[i].Cells[j].Value.ToString();
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

                MessageBox.Show("Xuất Excel thành công!");
            }
            excel.Quit();
        }
    }
}

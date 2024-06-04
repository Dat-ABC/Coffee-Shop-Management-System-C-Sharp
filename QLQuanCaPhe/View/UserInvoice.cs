using QLQuanCaPhe.BUS;
using QLQuanCaPhe.DTO;
using QLQuanCaPhe.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanCaPhe
{
    public partial class UserInvoice : UserControl
    {
        public UserInvoice()
        {
            InitializeComponent();
        }

        private void UserInvoice_Load(object sender, EventArgs e)
        {
            loadDateTimePicker();
            loadDataForDatagridview(1);
        }

        void loadDateTimePicker()
        {
            DateTime today = DateTime.Now;
            dtpFisrt.Value = new DateTime(today.Year, today.Month, 1);
            dtpEnd.Value = dtpFisrt.Value.AddMonths(1).AddDays(-1);
        }

        void loadDataForDatagridview(int page)
        {
            if (dtpFisrt.Value <= dtpEnd.Value)
            {
                dgvBill.Columns.Clear();
                dgvBill.DataSource =  BUS_BillInformation.Instance.GetBillListByDateAndPage(dtpFisrt.Value, dtpEnd.Value, page);
                dgvBill.Columns["billid"].Visible = false;
                dgvBill.Columns["tableName"].HeaderText = "Bàn";
                dgvBill.Columns["datecheckin"].HeaderText = "Thời gian vào";
                dgvBill.Columns["datecheckOut"].HeaderText = "Thời gian ra";
                dgvBill.Columns["discount"].HeaderText = "Giảm giá";
                dgvBill.Columns["totalMoney"].HeaderText = "Thành tiền";
                dgvBill.Columns["customerName"].HeaderText = "Khách hàng";
                dgvBill.Columns["displayname"].HeaderText = "Nhân viên";
                dgvBill.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9);
            }
        }

        private void txtPage_TextChanged(object sender, EventArgs e)
        {
            loadDataForDatagridview(int.Parse(txtPage.Text));
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            loadDataForDatagridview(1);
        }

        private void UserInvoice_Paint(object sender, PaintEventArgs e)
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

        private void btnFirst_Click_1(object sender, EventArgs e)
        {
            txtPage.Text = "1";
        }

        private void btnPrevious_Click_1(object sender, EventArgs e)
        {
            int page = int.Parse(txtPage.Text);

            if (page > 1)
            {
                page--;
            }

            txtPage.Text = page.ToString();
        }

        private void btnNext_Click_1(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txtPage.Text);
            int sumRecord = BUS_BillInformation.Instance.GetNumBillListByDate(dtpFisrt.Value, dtpEnd.Value);

            int totalRecord = sumRecord / 14;
            if (sumRecord % 14 != 0) 
                totalRecord++;

            if (page < totalRecord)
            {
                page++;
            }

            txtPage.Text = page.ToString();
        }

        private void btnLastPage_Click_1(object sender, EventArgs e)
        {
            int record = BUS_BillInformation.Instance.GetNumBillListByDate(dtpFisrt.Value, dtpEnd.Value);

            int lastPage = record / 14;

            if (record % 14 != 0)
                lastPage++;

            txtPage.Text = lastPage.ToString();
        }

        private void pnBillInfo_Paint(object sender, PaintEventArgs e)
        {
            int borderWidth = 2;
            Color borderColor = Color.White;

            Panel pn = sender as Panel;

            int borderX = 0;
            int borderY = 2;
            int borderXEnd = pn.Width;

            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawLine(pen, borderX, borderY, borderXEnd, borderY);
            }
        }


        int BillID;
        int Discount;
        double totalMoney;
        DateTime date;
        private void dgvBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0 && rowIndex < dgvBill.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvBill.Rows[rowIndex];
                BillID = (int)selectedRow.Cells["BillID"].Value;
                Discount = (int)selectedRow.Cells["Discount"].Value;
                totalMoney = Convert.ToDouble(selectedRow.Cells["totalMoney"].Value);
                date = (DateTime)selectedRow.Cells["datecheckOut"].Value;

                loadDgvBillInfo(BillID);

            }
        }

        void loadDgvBillInfo(int BillID)
        {
            dgvBillInfo.DataSource = BUS_BillInfo.Instance.getBillDetailList(BillID);
            dgvBillInfo.Columns["FoodName"].HeaderText = "Tên món";
            dgvBillInfo.Columns["Quantity"].HeaderText = "Số lượng";
            dgvBillInfo.Columns["Discount"].HeaderText = "Giảm giá (Phần trăm)";
            dgvBillInfo.Columns["price"].HeaderText = "Giá";
            dgvBillInfo.Columns["TotalMoney"].HeaderText = "Thành tiền";
            dgvBillInfo.Columns["note"].HeaderText = "Ghi chú";
            dgvBillInfo.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(Type.Missing);

            for (int i = 2; i < dgvBill.Columns.Count + 1; i++)
            {
                excel.Cells[1, i - 1] = dgvBill.Columns[i - 1].HeaderText;
            }

            for (int i = 0; i < dgvBill.Rows.Count; i++)
            {
                for (int j = 1; j < dgvBill.Columns.Count; j++)
                {
                    excel.Cells[i + 2, j] = dgvBill.Rows[i].Cells[j].Value.ToString();
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
                BUS_Diary.Instance.InsertDiary(DateTime.Now, "Xuất hóa đơn bán", $"Xuất danh hóa đơn bán từ {dtpFisrt.Value.Date} đến {dtpEnd.Value.Date}", BUS_Account.DisplayName);
                MessageBox.Show("Xuất Excel thành công!");
            }
            excel.Quit();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (BillID > 0)
            {
                List<MenuDTO> list = BUS_Menu.Instance.GetMenuToPrint(BillID);

                double totalMoney2 = totalMoney / ((100 - Discount) * 0.01);

                frmBill frmBill = new frmBill(list, totalMoney, Discount, totalMoney2, date);
                frmBill.ShowDialog();
            }
        }
    }
}

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

namespace QLQuanCaPhe
{
    public partial class UserStatistical : UserControl
    {
        public UserStatistical()
        {
            InitializeComponent();
        }

        private void UserStatistical_Load(object sender, EventArgs e)
        {
            /*dtpStartFood.Value = DateTime.Now.AddMonths(-1);
            loadDgvQuantity(dtpStartFood.Value, dtpEndFood.Value);
            loadItemTop5(dtpStartFood.Value, dtpEndFood.Value);

            dtpStart.Value = DateTime.Now.AddMonths(-1);
            loadRevenueFromStaff(dtpStart.Value, dtpEnd.Value);
            loadWarehouseList(dtpStart.Value, dtpEnd.Value);

            dtpRevenueStart.Value = DateTime.Now.AddMonths(-1);
            loadDgvRevenue(dtpRevenueStart.Value, dtpRevenueEnd.Value);
            loadItemTop5(dtpRevenueStart.Value, dtpRevenueEnd.Value);*/

            loadCboOverview();
            loadCboStaff_Customer();
            loadCboFood_Warehouse();
        }

        void loadDgvQuantity(DateTime start, DateTime end)
        {
            dgvQuantity.DataSource = BUS_Revenue.Instance.getOverview(start, end.AddDays(1));
            dgvQuantity.Columns["foodName"].HeaderText = "Tên sản phẩm";
            dgvQuantity.Columns["quantity"].HeaderText = "Số lượng";
            dgvQuantity.Columns["totalMoney"].HeaderText = "Doanh thu";

            decimal totalMoney = 0;

            foreach (DataGridViewRow row in dgvQuantity.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["totalMoney"].Value != null)
                {
                    if (decimal.TryParse(row.Cells["totalMoney"].Value.ToString(), out decimal value))
                    {
                        totalMoney += value;
                    }
                }
            }

            CultureInfo cultureInfo = new CultureInfo("vi-VN");

            string total = totalMoney.ToString("c", cultureInfo);

            lblFood.Text = "Tổng doanh thu: " + total;

        }

        void loadCboOverview()
        {
            cboOverview.Items.Clear();
            cboOverview.Items.Add("Tháng này");
            cboOverview.Items.Add("1 tháng trước");
            cboOverview.Items.Add("3 tháng trước");
            cboOverview.Items.Add("1 năm trước");
            cboOverview.SelectedIndex = 0;
            DateTime today = DateTime.Now;
            dtpRevenueStart.Value = new DateTime(today.Year, today.Month, 1);
            dtpRevenueEnd.Value = dtpRevenueStart.Value.AddMonths(1).AddDays(-1);
        }

        void loadCboStaff_Customer()
        {
            cboStaff_Customer.Items.Clear();
            cboStaff_Customer.Items.Add("Tháng này");
            cboStaff_Customer.Items.Add("1 tháng trước");
            cboStaff_Customer.Items.Add("3 tháng trước");
            cboStaff_Customer.Items.Add("1 năm trước");
            cboStaff_Customer.SelectedIndex = 0;
            DateTime today = DateTime.Now;
            dtpStart.Value = new DateTime(today.Year, today.Month, 1);
            dtpEnd.Value = dtpStart.Value.AddMonths(1).AddDays(-1);
        }

        void loadCboFood_Warehouse()
        {
            cboFood_Warehouse.Items.Clear();
            cboFood_Warehouse.Items.Add("Tháng này");
            cboFood_Warehouse.Items.Add("1 tháng trước");
            cboFood_Warehouse.Items.Add("3 tháng trước");
            cboFood_Warehouse.Items.Add("1 năm trước");
            cboFood_Warehouse.SelectedIndex = 0;
            DateTime today = DateTime.Now;
            dtpStartFood.Value = new DateTime(today.Year, today.Month, 1);
            dtpEndFood.Value = dtpStartFood.Value.AddMonths(1).AddDays(-1);
        }

        private void UserStatistical_Paint(object sender, PaintEventArgs e)
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

        void loadDgvRevenue(DateTime start, DateTime end)
        {
            dgvRevenue.DataSource = BUS_Revenue.Instance.getRevenueList(start, end.AddDays(1));

            decimal totalMoney = 0;

            foreach (DataGridViewRow row in dgvRevenue.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["Doanh thu (đ)"].Value != null)
                {
                    if (decimal.TryParse(row.Cells["Doanh thu (đ)"].Value.ToString(), out decimal value))
                    {
                        totalMoney += value;
                    }
                }
            }

            CultureInfo cultureInfo = new CultureInfo("vi-VN");

            string total = totalMoney.ToString("c", cultureInfo);

            lblRevenue.Text = "Tổng doanh thu: " + total;
        }

        void loadDgvCustomer(DateTime start, DateTime end)
        {
            dgvCustomer.DataSource = BUS_Revenue.Instance.getRevenueToCustomer(start, end.AddDays(1));
            dgvCustomer.Columns["customerID"].Visible = false;

            decimal totalMoney = 0;

            foreach (DataGridViewRow row in dgvCustomer.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["Tổng chi (đ)"].Value != null)
                {
                    if (decimal.TryParse(row.Cells["Tổng chi (đ)"].Value.ToString(), out decimal value))
                    {
                        totalMoney += value;
                    }
                }
            }

            CultureInfo cultureInfo = new CultureInfo("vi-VN");

            string total = totalMoney.ToString("c", cultureInfo);

            lblCustomer.Text = "Tổng chi: " + total;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            BUS_Diary.Instance.InsertDiary(DateTime.Now, "Xem thống kê", "Xem báo cáo doanh thu theo thời gian và từ khách hàng", BUS_Account.DisplayName);
            loadDgvRevenue(dtpRevenueStart.Value, dtpRevenueEnd.Value);
            loadDgvSpend(dtpRevenueStart.Value, dtpRevenueEnd.Value);
            try
            {
                string revenueText = lblRevenue.Text.Split(' ')[3].Split(',')[0].Trim();

                string spendText = lblSpend.Text.Split(' ')[2].Split(',')[0].Trim();

                decimal revenue = decimal.Parse(revenueText);

                decimal spend = decimal.Parse(spendText);

                decimal profit = revenue - spend;

                CultureInfo cultureInfo = new CultureInfo("vi-VN");

                string total = profit.ToString("c", cultureInfo);

                lblProfit.Text = "Lợi nhuận: " + total;
            }
            catch { }
        }


        void loadRevenueFromStaff(DateTime start, DateTime end)
        {
            dgvStaff.DataSource = BUS_Revenue.Instance.GetRevenueFromStaff(start, end.AddDays(1));
            dgvStaff.Columns["USername"].Visible = false;

            decimal totalMoney = 0;

            foreach (DataGridViewRow row in dgvStaff.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["Doanh thu (đ)"].Value != null)
                {
                    if (decimal.TryParse(row.Cells["Doanh thu (đ)"].Value.ToString(), out decimal value))
                    {
                        totalMoney += value;
                    }
                }

            }

            CultureInfo cultureInfo = new CultureInfo("vi-VN");

            string total = totalMoney.ToString("c", cultureInfo);

            lblStaff.Text = "Tổng doanh thu: " + total;
        }

        void loadWarehouseList(DateTime start, DateTime end)
        {
            dgvWarehouse.DataSource = BUS_Revenue.Instance.GetWarehouseList(start, end.AddDays(1));
            dgvWarehouse.Columns["itemID"].Visible = false;

            decimal totalMoney = 0;

            foreach (DataGridViewRow row in dgvWarehouse.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["Thành tiền"].Value != null)
                {
                    if (decimal.TryParse(row.Cells["Thành tiền"].Value.ToString(), out decimal value))
                    {
                        totalMoney += value;
                    }
                }
            }

            CultureInfo cultureInfo = new CultureInfo("vi-VN");

            string total = totalMoney.ToString("c", cultureInfo);

            lblWarehouse.Text = "Tổng tồn kho: " + total;
        }

        void loadDgvSpend(DateTime start, DateTime end)
        {
            dgvSpend.DataSource = BUS_Revenue.Instance.GetExpense(start, end.AddDays(1));

            decimal totalMoney = 0;

            foreach (DataGridViewRow row in dgvSpend.Rows)
            {
                if (row.IsNewRow) continue;

                if (row.Cells["Chi (đ)"].Value != null)
                {
                    if (decimal.TryParse(row.Cells["Chi (đ)"].Value.ToString(), out decimal value))
                    {
                        totalMoney += value;
                    }
                }
            }

            CultureInfo cultureInfo = new CultureInfo("vi-VN");

            string total = totalMoney.ToString("c", cultureInfo);

            lblSpend.Text = "Tổng chi: " + total;
        }

        private void btnView_Staff_Shift_Click(object sender, EventArgs e)
        {
            BUS_Diary.Instance.InsertDiary(DateTime.Now,"Xem thống kê", "Xem báo cáo doanh thu theo nhân viên và tồn kho", BUS_Account.DisplayName);
            loadRevenueFromStaff(dtpStart.Value, dtpEnd.Value);
            loadDgvCustomer(dtpStart.Value, dtpEnd.Value);
        }

        private void btnViewFood_Click_1(object sender, EventArgs e)
        {
            BUS_Diary.Instance.InsertDiary(DateTime.Now, "Xem thống kê", "Xem báo cáo doanh thu theo các món ăn và tồn kho", BUS_Account.DisplayName);
            loadDgvQuantity(dtpStartFood.Value, dtpEndFood.Value);
            loadWarehouseList(dtpStartFood.Value, dtpEndFood.Value);
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(Type.Missing);

            for (int i = 2; i < dgvStaff.Columns.Count + 1; i++)
            {
                excel.Cells[1, i - 1] = dgvStaff.Columns[i - 1].HeaderText;
            }

            for (int i = 0; i < dgvStaff.Rows.Count - 1; i++)
            {
                for (int j = 2; j < dgvStaff.Columns.Count + 1; j++)
                {
                    excel.Cells[i + 2, j] = dgvStaff.Rows[i].Cells[j - 1].Value.ToString();
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

                BUS_Diary.Instance.InsertDiary(DateTime.Now, "Xuất báo cáo", $"Xuất file báo cáo doanh thu theo nhân viên từ {dtpStart.Value.Date}  đến  {dtpEnd.Value.Date}", BUS_Account.DisplayName);
                MessageBox.Show("Xuất Excel thành công!");
            }
            excel.Quit();
        }

        private void btnExcelFood_Click_1(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(Type.Missing);

            for (int i = 1; i < dgvQuantity.Columns.Count + 1; i++)
            {
                excel.Cells[1, i] = dgvQuantity.Columns[i - 1].HeaderText;
            }

            for (int i = 0; i < dgvQuantity.Rows.Count; i++)
            {
                for (int j = 1; j < dgvQuantity.Columns.Count + 1; j++)
                {
                    excel.Cells[i + 2, j] = dgvQuantity.Rows[i].Cells[j - 1].Value.ToString();
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

                BUS_Diary.Instance.InsertDiary(DateTime.Now, "Xuất báo cáo", $"Xuất file báo cáo doanh thu theo món ăn từ {dtpStartFood.Value.Date}  đến  {dtpEndFood.Value.Date}", BUS_Account.DisplayName);
                MessageBox.Show("Xuất Excel thành công!");
            }
            excel.Quit();
        }

        private void btnCustomer_Click_1(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(Type.Missing);

            for (int i = 2; i < dgvCustomer.Columns.Count + 1; i++)
            {
                excel.Cells[1, i - 1] = dgvCustomer.Columns[i - 1].HeaderText;
            }

            for (int i = 0; i < dgvCustomer.Rows.Count - 1; i++)
            {
                for (int j = 1; j < dgvCustomer.Columns.Count + 1; j++)
                {
                    excel.Cells[i + 2, j] = dgvCustomer.Rows[i].Cells[j - 1].Value.ToString();
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
                BUS_Diary.Instance.InsertDiary(DateTime.Now, "Xuất báo cáo", $"Xuất file báo cáo doanh thu từ khách hàng từ {dtpStart.Value.Date}  đến  {dtpEnd.Value.Date}", BUS_Account.DisplayName);
                MessageBox.Show("Xuất Excel thành công!");
            }
            excel.Quit();
        }

        private void btnWarehouse_Click_1(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(Type.Missing);

            for (int i = 2; i < dgvWarehouse.Columns.Count + 1; i++)
            {
                excel.Cells[1, i - 1] = dgvWarehouse.Columns[i - 1].HeaderText;
            }

            for (int i = 0; i < dgvWarehouse.Rows.Count - 1; i++)
            {
                for (int j = 2; j < dgvWarehouse.Columns.Count + 1; j++)
                {
                    excel.Cells[i + 2, j] = dgvWarehouse.Rows[i].Cells[j - 1].Value.ToString();
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

                BUS_Diary.Instance.InsertDiary(DateTime.Now, "Xuất báo cáo", $"Xuất file báo cáo hàng tồn kho từ {dtpStartFood.Value.Date} đến {dtpEndFood.Value.Date}", BUS_Account.DisplayName);
                MessageBox.Show("Xuất Excel thành công!");
            }
            excel.Quit();
        }

        private void btnRevenue_Click_1(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(Type.Missing);

            for (int i = 1; i < dgvRevenue.Columns.Count + 1; i++)
            {
                excel.Cells[1, i] = dgvRevenue.Columns[i - 1].HeaderText;
            }

            for (int i = 0; i < dgvRevenue.Rows.Count - 1; i++)
            {
                for (int j = 1; j < dgvRevenue.Columns.Count + 1; j++)
                {
                    excel.Cells[i + 2, j] = dgvRevenue.Rows[i].Cells[j - 1].Value.ToString();
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

                BUS_Diary.Instance.InsertDiary(DateTime.Now, "Xuất báo cáo", $"Xuất file báo cáo doanh thu theo thời gian từ {dtpRevenueStart.Value.Date}  đến  {dtpRevenueEnd.Value.Date}", BUS_Account.DisplayName);
                MessageBox.Show("Xuất Excel thành công!");
            }
            excel.Quit();
        }

        private void btnSpend_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(Type.Missing);

            for (int i = 1; i < dgvSpend.Columns.Count + 1; i++)
            {
                excel.Cells[1, i] = dgvSpend.Columns[i - 1].HeaderText;
            }

            for (int i = 0; i < dgvSpend.Rows.Count - 1; i++)
            {
                for (int j = 1; j < dgvSpend.Columns.Count + 1; j++)
                {
                    excel.Cells[i + 2, j] = dgvSpend.Rows[i].Cells[j - 1].Value.ToString();
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

                BUS_Diary.Instance.InsertDiary(DateTime.Now, "Xuất báo cáo", $"Xuất file báo cáo khoản chi từ {dtpRevenueStart.Value.Date}  đến  {dtpRevenueEnd.Value.Date}", BUS_Account.DisplayName);
                MessageBox.Show("Xuất Excel thành công!");
            }
            excel.Quit();
        }

        private void cboStaff_Customer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStaff_Customer.SelectedIndex == 0)
            {
                DateTime today = DateTime.Now;
                dtpStart.Value = new DateTime(today.Year, today.Month, 1);
                dtpEnd.Value = dtpStart.Value.AddMonths(1).AddDays(-1);

            }
            if (cboStaff_Customer.SelectedIndex == 1)
            {
                dtpStart.Value = DateTime.Now.AddMonths(-1).AddDays(1);
                dtpEnd.Value = DateTime.Now;
            }
            else if (cboStaff_Customer.SelectedIndex == 2)
            {
                dtpStart.Value = DateTime.Now.AddMonths(-3).AddDays(1);
                dtpEnd.Value = DateTime.Now;
            }
            else if (cboStaff_Customer.SelectedIndex == 3)
            {
                dtpStart.Value = DateTime.Now.AddMonths(-12).AddDays(1);
                dtpEnd.Value = DateTime.Now;
            }
        }

        private void cboOverview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOverview.SelectedIndex == 0)
            {
                DateTime today = DateTime.Now;
                dtpRevenueStart.Value = new DateTime(today.Year, today.Month, 1);
                dtpRevenueEnd.Value = dtpRevenueStart.Value.AddMonths(1).AddDays(-1);

            }
            if (cboOverview.SelectedIndex == 1)
            {
                dtpRevenueStart.Value = DateTime.Now.AddMonths(-1).AddDays(1);
                dtpRevenueEnd.Value = DateTime.Now;
            }
            else if (cboOverview.SelectedIndex == 2)
            {
                dtpRevenueStart.Value = DateTime.Now.AddMonths(-3).AddDays(1);
                dtpRevenueEnd.Value = DateTime.Now;
            }
            else if (cboOverview.SelectedIndex == 3)
            {
                dtpRevenueStart.Value = DateTime.Now.AddMonths(-12).AddDays(1);
                dtpRevenueEnd.Value = DateTime.Now;
            }
        }

        private void cboFood_Warehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFood_Warehouse.SelectedIndex == 0)
            {
                DateTime today = DateTime.Now;
                dtpStartFood.Value = new DateTime(today.Year, today.Month, 1);
                dtpEndFood.Value = dtpStartFood.Value.AddMonths(1).AddDays(-1);

            }
            if (cboFood_Warehouse.SelectedIndex == 1)
            {
                dtpStartFood.Value = DateTime.Now.AddMonths(-1).AddDays(1);
                dtpEndFood.Value = DateTime.Now;
            }
            else if (cboFood_Warehouse.SelectedIndex == 2)
            {
                dtpStartFood.Value = DateTime.Now.AddMonths(-3).AddDays(1);
                dtpEndFood.Value = DateTime.Now;
            }
            else if (cboFood_Warehouse.SelectedIndex == 3)
            {
                dtpStartFood.Value = DateTime.Now.AddMonths(-12).AddDays(1);
                dtpEndFood.Value = DateTime.Now;
            }
        }
    }
}

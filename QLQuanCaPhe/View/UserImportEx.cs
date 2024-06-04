using Microsoft.Office.Interop.Excel;
using QLQuanCaPhe.BUS;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanCaPhe.View
{
    public partial class UserImportEx : UserControl
    {
        public UserImportEx()
        {
            InitializeComponent();
        }

        private void UserImportEx_Load(object sender, EventArgs e)
        {
            loadCboItemType();
            loadDgvItem();
            dtpStartImport.Value = DateTime.Now.AddDays(-6);
            loadCboStaffImport();
            loadCboItemTypeImport();
            loadCboSupplierImport();
            btnAddDetailImport.Enabled = false;
            btnAddDetailImport.BackColor = Color.DarkGray;

            btnAddDetailEx.Enabled = false;
            btnAddDetailEx.BackColor = Color.DarkGray;

            dtpStartEx.Value = DateTime.Now.AddDays(-6);
            loadCboStaffExport();
            loadCboItemTypeEx();
            loadDgvWarehouse();
        }

        private void UserImportEx_Paint(object sender, PaintEventArgs e)
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

        private void pnImportDetail_Paint(object sender, PaintEventArgs e)
        {
            int borderWidth = 2;
            Color borderColor = Color.White;

            Panel panel = sender as Panel;

            int borderX = 0;
            int borderY = 0;
            int borderHeightWithPanel = panel.Height;

            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawLine(pen, borderX, borderY, borderX, borderHeightWithPanel);
            }
        }

        private void pnExportDetail_Paint(object sender, PaintEventArgs e)
        {
            int borderWidth = 2;
            Color borderColor = Color.White;

            Panel panel = sender as Panel;

            int borderX = 0;
            int borderY = 0;
            int borderHeightWithPanel = panel.Height;

            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawLine(pen, borderX, borderY, borderX, borderHeightWithPanel);
            }
        }

        #region mặt hàng

        void loadDgvItem()
        {
            dgvItem.DataSource = BUS_Items.Instance.GetItemList();

            dgvItem.Columns["ItemID"].Visible = false;
            dgvItem.Columns["ItemName"].HeaderText = "Mặt hàng";
            dgvItem.Columns["Unit"].HeaderText = "Đơn vị tính";
            dgvItem.Columns["Price"].HeaderText = "Giá";
            dgvItem.Columns["itemType"].HeaderText = "Loại mặt hàng";
        }

        void loadCboItemType()
        {
            cboItemType.Items.Clear();
            cboItemType.Items.Add("Nguyên liệu");
            cboItemType.Items.Add("Sản phẩm");
            cboItemType.SelectedIndex = 0;
        }

        void clean()
        {
            txtName.Text = "";
            txtPrice.Text = "";
            txtDVT.Text = "";
            cboItemType.SelectedIndex = 0;
            txtFind.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            if (MessageBox.Show($"Bạn có chắc chắn muốn thêm mặt hàng {name} không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (name.Trim() != "" && double.TryParse(txtPrice.Text, out double price) && txtDVT.Text.Trim() != "")
                {
                    if (BUS_Items.Instance.InsertItem(name, txtDVT.Text, price, cboItemType.Text))
                    {
                        MessageBox.Show($"Thêm mặt hàng thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDgvItem();
                        loadCboItemTypeImport();
                        loadCboItemTypeEx();
                        clean();
                    }
                    else
                    {
                        MessageBox.Show($"Tên mặt hàng này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                else
                {
                    MessageBox.Show($"Các thông tin không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            clean();
        }

        private void dgvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index >= 0 &&  index < dgvItem.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvItem.Rows[index];
                txtName.Text = selectedRow.Cells["itemName"].Value.ToString();
                txtPrice.Text = selectedRow.Cells["price"].Value.ToString();
                txtDVT.Text = selectedRow.Cells["Unit"].Value.ToString();

                string itemType = selectedRow.Cells["itemType"].Value.ToString();

                int index3 = -1;
                int i = 0;
                foreach (string item in cboItemType.Items)
                {
                    if (item == itemType)
                    {
                        index3 = i;
                        break;
                    }
                    i++;
                }

                cboItemType.SelectedIndex = index3;
            }
            else
            {
                clean();
            }
        }

        private void btnExExcel_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(Type.Missing);

            for (int i = 1; i < dgvItem.Columns.Count; i++)
            {
                excel.Cells[1, i] = dgvItem.Columns[i].HeaderText;
            }

            for (int i = 0; i < dgvItem.Rows.Count; i++)
            {

                for (int j = 1; j < dgvItem.Columns.Count; j++)
                {
                    excel.Cells[i + 2, j] = dgvItem.Rows[i].Cells[j].Value.ToString();
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
                BUS_Diary.Instance.InsertDiary(DateTime.Now, "Xuất danh sách mặt hàng", "Xuất danh sách mặt hàng", BUS_Account.DisplayName);
                MessageBox.Show("Xuất Excel thành công!");
            }
            excel.Quit();
        }

        #endregion

        #region Import

        void loadDgvImport(DateTime start, DateTime end)
        {
            dgvImport.DataSource = BUS_ImportOrder.Instance.GetImportOrderList(start, end.AddDays(1));
            dgvImport.Columns["ImportID"].Visible = false;
            dgvImport.Columns["Username"].Visible = false;
            dgvImport.Columns["DateIn"].HeaderText = "Ngày nhập";
            dgvImport.Columns["TotalMoney"].HeaderText = "Tổng tiền";
            dgvImport.Columns["DisplayName"].HeaderText = "Nhân viên";
            dgvImportDetail.DataSource = null;
            btnAddDetailImport.Enabled = false;
            btnAddDetailImport.BackColor = Color.DarkGray;
        }

        void loadCboStaffImport()
        {
            List<AccountDTO> list = BUS_Account.Instance.GetAccountList();
            cboStaffImport.DataSource = BUS_Account.Instance.GetAccountList();
            cboStaffImport.DisplayMember = "displayname";
            int i = 0;
            string username = BUS_Account.Username;

            foreach (AccountDTO account in list)
            {
                if (account.UserName == username)
                {
                    break;
                }
                i++;
            }
            cboStaffImport.SelectedIndex = i;
        }

        private void btnAddImport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thêm hóa đơn nhập không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                AccountDTO account = cboStaffImport.SelectedItem as AccountDTO;
                dtpImport.Value = DateTime.Now;
                if (BUS_ImportOrder.Instance.InsertImportOrder(dtpImport.Value, account.UserName))
                {
                    MessageBox.Show("Thêm hóa đơn nhập thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cleanImport();
                    dtpStartImport.Value = DateTime.Now.AddDays(-6);
                    dtpEndImport.Value = DateTime.Now;
                }

                else
                {
                    MessageBox.Show("Có lỗi khi thêm hóa đơn nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        void cleanImport()
        {
            dtpStartImport.Value = DateTime.Now.AddDays(-6);
            dtpEndImport.Value = DateTime.Now;

            loadCboStaffImport();
            dtpImport.Value = DateTime.Now;
            txtTotalMoney.Text = "";
            
        }

        private void btnCleanImport_Click(object sender, EventArgs e)
        {
            cleanImport();
        }

        private void btnExExcelImport_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(Type.Missing);

            for (int i = 3; i < dgvImport.Columns.Count + 1; i++)
            {
                excel.Cells[1, i - 2] = dgvImport.Columns[i - 1].HeaderText;
            }

            for (int i = 0; i < dgvImport.Rows.Count; i++)
            {
                for (int j = 2; j < dgvImport.Columns.Count; j++)
                {
                    excel.Cells[i + 2, j - 1] = dgvImport.Rows[i].Cells[j].Value.ToString();
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
                BUS_Diary.Instance.InsertDiary(DateTime.Now, "Xuất hóa đơn nhập", "Xuất danh sách hóa đơn nhập", BUS_Account.DisplayName);
                MessageBox.Show("Xuất Excel thành công!");
            }
            excel.Quit();
        }

        int importID;

        private void dgvImport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index >= 0 && index < dgvImport.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvImport.Rows[index];

                importID = (int)selectedRow.Cells["importID"].Value;
                dtpImport.Value = (DateTime)selectedRow.Cells["dateIn"].Value;
                txtTotalMoneyImport.Text = selectedRow.Cells["totalMoney"].Value.ToString();

                btnAddDetailImport.Enabled = true;
                btnAddDetailImport.BackColor = Color.FromArgb(53, 41, 123);

                string username = selectedRow.Cells["Username"].Value.ToString();

                int i = 0;
                foreach (AccountDTO account in cboStaffImport.Items)
                {
                    if (account.UserName == username)
                    {
                        break;
                    }
                    i++;
                }

                cboStaffImport.SelectedIndex = i;

                loadDgvImportDetail(importID);
            }
            else
            {
                cleanImport();

                btnAddDetailImport.Enabled = false;
                btnAddDetailImport.BackColor = Color.DarkGray;
            }
        }

        private void dtpStartImport_ValueChanged(object sender, EventArgs e)
        {

            loadDgvImport(dtpStartImport.Value, dtpEndImport.Value);
        }

        private void dtpEndImport_ValueChanged(object sender, EventArgs e)
        {
            loadDgvImport(dtpStartImport.Value, dtpEndImport.Value);
        }

        #endregion

        #region importOrder Detail

        private void btnCleanDetailImport_Click(object sender, EventArgs e)
        {
            clean_import_detail();
        }

        void loadDgvImportDetail(int importID = 0)
        {
            if (importID != 0)
            {
                dgvImportDetail.DataSource = BUS_ImportDetail.Instance.GetImportDetailList(importID);
                dgvImportDetail.Columns["importDetailID"].Visible = false;
                dgvImportDetail.Columns["importID"].Visible = false;
                dgvImportDetail.Columns["itemID"].Visible = false;
                dgvImportDetail.Columns["supplierID"].Visible = false;
                dgvImportDetail.Columns["itemName"].HeaderText = "Sản phẩm";
                dgvImportDetail.Columns["quantity"].HeaderText = "Số lượng";
                dgvImportDetail.Columns["quantity"].FillWeight = 70;
                dgvImportDetail.Columns["price"].HeaderText = "Giá";
                dgvImportDetail.Columns["discount"].HeaderText = "Giảm giá";
                dgvImportDetail.Columns["discount"].FillWeight = 70;
                dgvImportDetail.Columns["Unit"].HeaderText = "Đơn vị tính";
                dgvImportDetail.Columns["Unit"].FillWeight = 70;
                dgvImportDetail.Columns["totalMoney"].HeaderText = "Thành tiền";
                dgvImportDetail.Columns["supplierName"].HeaderText = "Nhà cung cấp";
            }
            else
            {
                dgvImportDetail.DataSource = null;
            }
        }

        void loadCboItemTypeImport()
        {
            cboItemTypeImport.Items.Clear();
            cboItemTypeImport.Items.Add("Nguyên liệu");
            cboItemTypeImport.Items.Add("Sản phẩm");
            cboItemTypeImport.SelectedIndex = 0;
        }

        void loadCboNameImport(string itemType)
        {
            cboNameImport.DataSource = BUS_Items.Instance.GetItemListByItemType(itemType);
            ItemsDTO item = cboNameImport.SelectedItem as ItemsDTO;
            if (item != null)
            {
                txtPriceImport.Text = item.Price.ToString();
                txtDVTImport.Text = item.Unit.ToString();
            }

            cboNameImport.DisplayMember = "ItemName";
        }

        void loadCboSupplierImport()
        {
            SupplierDTO supplier = new SupplierDTO () { SupplierName = "Không xác định"};
            List<SupplierDTO> suppliers = new List<SupplierDTO>();
            List<SupplierDTO> list = BUS_Supplier.Instance.GetSupplierList();
            suppliers.Add(supplier);
            suppliers.AddRange(list);
            cboSupplierImport.DataSource = suppliers;
            cboSupplierImport.DisplayMember = "SupplierName";
            cboSupplierImport.SelectedIndex = 0;
        }

        void clean_import_detail()
        {
            txtQuantityImport.Text = "";
            txtPriceImport.Text = "";
            txtDVTImport.Text = "";
            txtDiscountImport.Text = "";
            txtTotalMoney.Text = "";
            loadCboNameImport("Nguyên liệu");
            cboSupplierImport.SelectedIndex = 0;
            cboItemTypeImport.SelectedIndex = 0;
        }

        private void cboItemTypeImport_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            loadCboNameImport(cboItemTypeImport.Text);
        }

        private void cboNameImport_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ItemsDTO item = cboNameImport.SelectedItem as ItemsDTO;
            if (item != null)
            {
                txtPriceImport.Text = item.Price.ToString();
                txtDVTImport.Text = item.Unit.ToString();
            }
        }

        private void btnAddDetailImport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thêm chi tiết hóa đơn không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (txtQuantityImport.Text.Trim() != "" && txtPriceImport.Text.Trim() != "" && txtDVTImport.Text.Trim() != "" && cboNameImport.Text != "")
                {
                    float quantity;
                    bool chkQuantity = false;
                    if (float.TryParse(txtQuantityImport.Text, out quantity) && quantity > 0 && quantity < 100000)
                    {
                        chkQuantity = true;
                    }

                    double price;
                    bool chkPrice = false;
                    if (double.TryParse(txtPriceImport.Text, out price) && price > 0 && price < 100000000)
                    {
                        chkPrice = true;
                    }

                    float discount = 0;
                    bool chkDiscount = true;
                    if (txtDiscountImport.Text != "" && !float.TryParse(txtDiscountImport.Text, out discount) && discount < 0 && discount > 100)
                    {
                        chkDiscount = false;
                    }

                    ItemsDTO items = cboNameImport.SelectedItem as ItemsDTO;

                    SupplierDTO supplier = cboSupplierImport.SelectedItem as SupplierDTO;
                    int supplierID = 0;
                    if (supplier.SupplierName != "Không xác định")
                    {
                        supplierID = supplier.SupplierID;
                    }

                    if (chkQuantity && chkPrice && chkDiscount)
                    {
                        if (BUS_ImportDetail.Instance.InsertImportDetail(quantity, price, double.Parse(txtTotalMoney.Text), importID, items.ItemID, txtDVTImport.Text, supplierID, discount))
                        {
                            MessageBox.Show("Thêm chi tiết hóa đơn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadDgvWarehouse();
                            loadDgvImportDetail(importID);
                            loadDgvImport(dtpStartImport.Value, dtpEndImport.Value);
                        }
                        else
                        {
                            MessageBox.Show("Thêm chi tiết hóa đơn thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Các ô số lượng, giá, giảm giá (Nếu có phạm vi 0 đến 100) phải là một số lớn hơn 0 và không quá lớn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Không được để trống các ô số lượng, giá, ĐVT, mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtQuantityImport_TextChanged(object sender, EventArgs e)
        {
            try
            {


                float quantity;
                float.TryParse(txtQuantityImport.Text, out quantity);

                double price;
                double.TryParse(txtPriceImport.Text, out price);

                float discount;
                float.TryParse(txtDiscountImport.Text, out discount);

                txtTotalMoney.Text = ((price * quantity) - (price * quantity * discount * 0.01)).ToString();
            }
            catch { }
        }

        private void txtDiscountImport_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float quantity;
                float.TryParse(txtQuantityImport.Text, out quantity);

                double price;
                double.TryParse(txtPriceImport.Text, out price);

                float discount;
                float.TryParse(txtDiscountImport.Text, out discount);

                txtTotalMoney.Text = ((price * quantity) - (price * quantity * discount * 0.01)).ToString();
            }
            catch { }
        }

        private void txtPriceImport_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float quantity;
                float.TryParse(txtQuantityImport.Text, out quantity);

                double price;
                double.TryParse(txtPriceImport.Text, out price);

                float discount;
                float.TryParse(txtDiscountImport.Text, out discount);

                txtTotalMoney.Text = ((price * quantity) - (price * quantity * discount * 0.01)).ToString();
            }
            catch { }
        }

        private void dgvImportDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index >= 0 && index < dgvImportDetail.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvImportDetail.Rows[index];
                txtQuantityImport.Text = selectedRow.Cells["quantity"].Value.ToString();
                txtPriceImport.Text = selectedRow.Cells["price"].Value.ToString();
                txtDVTImport.Text = selectedRow.Cells["unit"].Value.ToString();
                txtDiscountImport.Text = selectedRow.Cells["discount"].Value.ToString();
                txtTotalMoney.Text = selectedRow.Cells["totalMoney"].Value.ToString();

                int itemID = (int)selectedRow.Cells["itemID"].Value;

                ItemsDTO item = BUS_Items.Instance.GetItemByItemID(itemID);

                int i = 0;
                foreach (string items in cboItemTypeImport.Items)
                {
                    if (items == item.ItemType)
                    {
                        break;
                    }
                    i++;
                }

                cboNameImport.SelectedIndex = i;

                i = 0;
                foreach (ItemsDTO items in cboNameImport.Items)
                {
                    if (items.ItemID == itemID)
                    {
                        break;
                    }
                    i++;
                }

                cboNameImport.SelectedIndex = i;

                int supplierID = (int)selectedRow.Cells["supplierID"].Value;

                i = 0;
                foreach (SupplierDTO supplier in cboSupplierImport.Items)
                {
                    if (supplier.SupplierID == supplierID)
                    {
                        break;
                    }
                    i++;
                }

                cboSupplierImport.SelectedIndex = i;


            }
            else
            {
                clean_import_detail();
            }
        }

        #endregion

        #region Export Order

        void loadDgvExport(DateTime start, DateTime end)
        {
            dgvExport.DataSource = BUS_ExportOrder.Instance.GetExportOrderList(start, end.AddDays(1));
            dgvExport.Columns["ExportID"].Visible = false;
            dgvExport.Columns["Username"].Visible = false;
            dgvExport.Columns["DateOut"].HeaderText = "Ngày xuất";
            dgvExport.Columns["TotalMoney"].HeaderText = "Tổng tiền";
            dgvExport.Columns["DisplayName"].HeaderText = "Nhân viên";
            dgvExportDetail.DataSource = null;
            btnAddDetailEx.Enabled = false;
            btnAddDetailEx.BackColor = Color.DarkGray;
        }

        void loadCboStaffExport()
        {
            List<AccountDTO> list = BUS_Account.Instance.GetAccountList();
            cboStaffEx.DataSource = BUS_Account.Instance.GetAccountList();
            cboStaffEx.DisplayMember = "displayname";
            int i = 0;
            string username = BUS_Account.Username;

            foreach (AccountDTO account in list)
            {
                if (account.UserName == username)
                {
                    break;
                }
                i++;
            }
            cboStaffEx.SelectedIndex = i;
        }

        void cleanExport()
        {
            dtpStartEx.Value = DateTime.Now.AddDays(-6);
            dtpEndEx.Value = DateTime.Now;

            loadCboStaffExport();
            dtpExport.Value = DateTime.Now;
            txtTotalMoneyEx.Text = "";

        }

        private void btnCleanExport_Click(object sender, EventArgs e)
        {
            cleanExport();
        }

        private void btnAddEx_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thêm hóa đơn xuất không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                AccountDTO account = cboStaffEx.SelectedItem as AccountDTO;
                dtpExport.Value = DateTime.Now;
                if (BUS_ExportOrder.Instance.InsertExportOrder(dtpExport.Value, account.UserName))
                {
                    MessageBox.Show("Thêm hóa đơn xuất thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cleanExport();
                    dtpStartEx.Value = DateTime.Now.AddDays(-6);
                    dtpEndEx.Value = DateTime.Now;
                    loadDgvExportDetail();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi thêm hóa đơn xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        int exportID;

        private void dgvExport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index >= 0 && index < dgvExport.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvExport.Rows[index];

                exportID = (int)selectedRow.Cells["exportID"].Value;
                dtpExport.Value = (DateTime)selectedRow.Cells["dateOut"].Value;
                txtTotalMoneyEx.Text = selectedRow.Cells["totalMoney"].Value.ToString();

                btnAddDetailEx.Enabled = true;
                btnAddDetailEx.BackColor = Color.FromArgb(53, 41, 123);

                string username = selectedRow.Cells["Username"].Value.ToString();

                int i = 0;
                foreach (AccountDTO account in cboStaffEx.Items)
                {
                    if (account.UserName == username)
                    {
                        break;
                    }
                    i++;
                }

                cboStaffEx.SelectedIndex = i;

                loadDgvExportDetail(exportID);
            }
            else
            {
                cleanExport();

                btnAddDetailEx.Enabled = false;
                btnAddDetailEx.BackColor = Color.DarkGray;
                loadDgvExportDetail();
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(Type.Missing);

            for (int i = 3; i < dgvExport.Columns.Count + 1; i++)
            {
                excel.Cells[1, i - 2] = dgvExport.Columns[i - 1].HeaderText;
            }

            for (int i = 0; i < dgvExport.Rows.Count; i++)
            {
                for (int j = 2; j < dgvExport.Columns.Count; j++)
                {
                    excel.Cells[i + 2, j - 1] = dgvExport.Rows[i].Cells[j].Value.ToString();
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
                BUS_Diary.Instance.InsertDiary(DateTime.Now, "Xuất hóa đơn xuất", "Xuất danh sách hóa đơn xuất", BUS_Account.DisplayName);
                MessageBox.Show("Xuất Excel thành công!");
            }
            excel.Quit();
        }

        private void dtpStartEx_ValueChanged(object sender, EventArgs e)
        {
            loadDgvExport(dtpStartEx.Value, dtpEndEx.Value);
        }

        private void dtpEndEx_ValueChanged(object sender, EventArgs e)
        {
            loadDgvExport(dtpStartEx.Value, dtpEndEx.Value);
        }

        #endregion

        #region Export Detail

        void loadDgvExportDetail(int exportID = 0)
        {
            if (exportID != 0)
            {
                dgvExportDetail.DataSource = BUS_ExportDetail.Instance.GetExportDetailList(exportID);
                dgvExportDetail.Columns["ExportDetailID"].Visible = false;
                dgvExportDetail.Columns["exportID"].Visible = false;
                dgvExportDetail.Columns["itemID"].Visible = false;

                dgvExportDetail.Columns["itemName"].HeaderText = "Sản phẩm";
                dgvExportDetail.Columns["quantity"].HeaderText = "Số lượng";
                dgvExportDetail.Columns["price"].HeaderText = "Giá";
                dgvExportDetail.Columns["Unit"].HeaderText = "Đơn vị tính";
                dgvExportDetail.Columns["totalMoney"].HeaderText = "Thành tiền";
            }
            else
            {
                dgvExportDetail.DataSource = null;
            }
        }

        void loadCboItemTypeEx()
        {
            cboItemTypeEx.Items.Clear();
            cboItemTypeEx.Items.Add("Nguyên liệu");
            cboItemTypeEx.Items.Add("Sản phẩm");
            cboItemTypeEx.SelectedIndex = 0;
        }

        void loadCboNameEx(string itemType)
        {
            cboNameEx.DataSource = BUS_Items.Instance.GetItemListByItemType(itemType);
            ItemsDTO item = cboNameEx.SelectedItem as ItemsDTO;
            if (item != null)
            {
                txtPriceEx.Text = item.Price.ToString();
                txtDVTEx.Text = item.Unit.ToString();
            }

            cboNameEx.DisplayMember = "ItemName";
        }

        void clean_export_detail()
        {
            txtQuantityEx.Text = "";
            txtPriceEx.Text = "";
            txtDVTEx.Text = "";
            txtTotalMoneyEx.Text = "";
            loadCboNameEx("Nguyên liệu");
            cboItemTypeEx.SelectedIndex = 0;
        }

        private void cboItemTypeEx_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadCboNameEx(cboItemTypeEx.Text);
        }

        private void cboNameEx_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemsDTO item = cboNameEx.SelectedItem as ItemsDTO;
            if (item != null)
            {
                txtPriceEx.Text = item.Price.ToString();
                txtDVTEx.Text = item.Unit.ToString();
            }
        }

        private void btnCleanDetailEx_Click(object sender, EventArgs e)
        {
            clean_export_detail();
        }

        private void btnAddDetailEx_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thêm chi tiết hóa đơn xuất không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (txtQuantityEx.Text.Trim() != "" && txtPriceEx.Text.Trim() != "" && cboNameEx.Text != "")
                {
                    ItemsDTO items = cboNameEx.SelectedItem as ItemsDTO;

                    InventoryDTO inventory = BUS_Inventory.Instance.GetInventoryMax(items.ItemID);

                    if(inventory != null)
                    {
                        float quantity;
                        bool chkQuantity = false;
                        if (float.TryParse(txtQuantityEx.Text, out quantity) && quantity > 0 && quantity < 1000000)
                        {
                            chkQuantity = true;
                        }

                        if (inventory.ActualQuantity - quantity >= 0)
                        {
                            double price;
                            bool chkPrice = false;
                            if (double.TryParse(txtPriceEx.Text, out price) && price > 0 && price < 100000000)
                            {
                                chkPrice = true;
                            }

                            if (chkQuantity && chkPrice)
                            {
                                if (BUS_ExportDetail.Instance.InsertExportDetail(quantity, price, double.Parse(txtTotalMoneyDetailEx.Text), exportID, items.ItemID, txtDVTEx.Text))
                                {
                                    MessageBox.Show("Thêm chi tiết hóa đơn xuất thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    loadDgvWarehouse();
                                    loadDgvExportDetail(exportID);
                                    loadDgvExport(dtpStartEx.Value, dtpEndEx.Value);
                                }
                                else
                                {
                                    MessageBox.Show("Thêm chi tiết hóa đơn xuất thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Các ô số lượng, giá  phải là một số lớn hơn 0 và không quá lớn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Số lượng xuất đang lớn hơn ở trong kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sản phẩm này chưa được nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Không được để trống các ô số lượng, giá, mặt hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void dgvExportDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;

            if (index >= 0 && index < dgvExportDetail.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvExportDetail.Rows[index];

                txtQuantityEx.Text = selectedRow.Cells["quantity"].Value.ToString();
                txtPriceEx.Text = selectedRow.Cells["price"].Value.ToString();
                txtDVTEx.Text = selectedRow.Cells["unit"].Value.ToString();
                txtTotalMoneyEx.Text = selectedRow.Cells["totalMoney"].Value.ToString();

                int itemID = (int)selectedRow.Cells["itemID"].Value;

                ItemsDTO item = BUS_Items.Instance.GetItemByItemID(itemID);

                int i = 0;
                foreach (string items in cboItemTypeEx.Items)
                {
                    if (items == item.ItemType)
                    {
                        break;
                    }
                    i++;
                }

                cboNameEx.SelectedIndex = i;


                i = 0;
                foreach (ItemsDTO items in cboNameEx.Items)
                {
                    if (items.ItemID == itemID)
                    {
                        break;
                    }
                    i++;
                }

                cboNameEx.SelectedIndex = i;
            }
            else
            {
                clean_export_detail();
            }
        }

        private void txtQuantityEx_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float quantity;
                float.TryParse(txtQuantityEx.Text, out quantity);

                double price;
                double.TryParse(txtPriceEx.Text, out price);



                txtTotalMoneyDetailEx.Text = (price * quantity).ToString();
            }
            catch { }
        }

        private void txtPriceEx_TextChanged(object sender, EventArgs e)
        {
            try
            {


                float quantity;
                float.TryParse(txtQuantityEx.Text, out quantity);

                double price;
                double.TryParse(txtPriceEx.Text, out price);

                txtTotalMoneyDetailEx.Text = (price * quantity).ToString();
            }
            catch { }
        }

        #endregion

        #region Kho
        void loadDgvWarehouse()
        {
            dgvWarehouse.DataSource = BUS_Warehouse.Instance.GetWarehouseList();
            dgvWarehouse.Columns["itemID"].Visible = false;

            dgvWarehouse.Columns["itemName"].HeaderText = "Sản phẩm";
            dgvWarehouse.Columns["quantity"].HeaderText = "Số lượng";
            dgvWarehouse.Columns["unit"].HeaderText = "Đơn vị tính";
            dgvWarehouse.Columns["price"].HeaderText = "Giá";
            dgvWarehouse.Columns["ItemType"].HeaderText = "Loại sản phẩm";
        }

        void loadDgvWarehouseHistory(int itemID)
        {
            dgvWarehouseHistory.DataSource = BUS_Inventory.Instance.GetInventoryList(itemID);

            dgvWarehouseHistory.Columns["inventoryID"].Visible = false;
            dgvWarehouseHistory.Columns["username"].Visible = false;
            dgvWarehouseHistory.Columns["itemID"].Visible = false;

            dgvWarehouseHistory.Columns["date"].HeaderText = "Ngày";
            dgvWarehouseHistory.Columns["date"].FillWeight = 30;
            dgvWarehouseHistory.Columns["quantity"].HeaderText = "Số lượng";
            dgvWarehouseHistory.Columns["quantity"].FillWeight = 60;

            dgvWarehouseHistory.Columns["actualQuantity"].HeaderText = "Số lượng thực tế";
            dgvWarehouseHistory.Columns["actualQuantity"].FillWeight = 60;
            dgvWarehouseHistory.Columns["note"].HeaderText = "Ghi chú";
            dgvWarehouseHistory.Columns["loss"].HeaderText = "Hao hụt";
            dgvWarehouseHistory.Columns["loss"].FillWeight = 40;
            dgvWarehouseHistory.Columns["displayName"].HeaderText = "Nhân viên";
            dgvWarehouseHistory.Columns["displayName"].FillWeight = 60;
        }

        int itemID3;

        private void dgvWarehouse_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0 && index < dgvWarehouse.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvWarehouse.Rows[index];

                itemID3 = (int)selectedRow.Cells["itemID"].Value;


                txtItemName.Text = selectedRow.Cells["itemName"].Value.ToString();
                txtQuantity.Text = selectedRow.Cells["quantity"].Value.ToString();
                txtActualQuantity.Text = selectedRow.Cells["quantity"].Value.ToString();
                txtUnit.Text = selectedRow.Cells["Unit"].Value.ToString();
                txtPrice_warehouse.Text = selectedRow.Cells["price"].Value.ToString();

                loadDgvWarehouseHistory(itemID3);
            }
        }

        private void btnCleanInventory_Click(object sender, EventArgs e)
        {
            cleanInventory();
        }

        void cleanInventory()
        {
            txtItemName.Text = "";
            txtQuantity.Text = "";
            txtActualQuantity.Text = "";
            txtPrice_warehouse.Text = "";
            txtUnit.Text = "";
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có thêm lịch sử cập nhập hàng tồn không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (float.TryParse(txtActualQuantity.Text, out float actualQuantity) && actualQuantity >= 0)
                {
                    if (BUS_Inventory.Instance.InsertInventory(float.Parse(txtQuantity.Text), actualQuantity, itemID3))
                    {
                        MessageBox.Show("Thêm lịch sử thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDgvWarehouseHistory(itemID3);
                        loadDgvWarehouse();
                        cleanInventory();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi khi thêm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Số lượng thực tế phải lớn hơn hoặc bằng 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnExcelWarehouse_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(Type.Missing);

            for (int i = 1; i < dgvWarehouse.Columns.Count; i++)
            {
                excel.Cells[1, i] = dgvWarehouse.Columns[i].HeaderText;
            }

            for (int i = 0; i < dgvWarehouse.Rows.Count; i++)
            {
                for (int j = 1; j < dgvWarehouse.Columns.Count; j++)
                {
                    excel.Cells[i + 2, j] = dgvWarehouse.Rows[i].Cells[j].Value.ToString();
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
                BUS_Diary.Instance.InsertDiary(DateTime.Now, "Xuất danh sách hàng tồn kho", "Xuất danh sách hàng tồn kho", BUS_Account.DisplayName);
                MessageBox.Show("Xuất Excel thành công!");
            }
            excel.Quit();
        }

        #endregion
    }
}

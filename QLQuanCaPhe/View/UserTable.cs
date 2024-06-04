using Microsoft.Reporting.WinForms;
using QLQuanCaPhe.BUS;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanCaPhe.View
{
    public partial class UserTable : UserControl
    {
        public UserTable()
        {
            InitializeComponent();
        }

        private void UserTable_Load(object sender, EventArgs e)
        {
            loadTable();
            loadFoodCategory();
            loadComboboxArea();
        }

        #region Menthod
        void loadFoodCategory()
        {
            List<CategoryDTO> list = BUS_Category.Instance.GetCategoryList();
            cboCategory.DataSource = list;
            cboCategory.DisplayMember = "categoryName";
        }

        void loadFood(int CategoryID)
        {
            flpFood.Controls.Clear();
            List<FoodDTO> foods = BUS_Food.Instance.GetFoodListByCategoryID(CategoryID);
            foreach (FoodDTO foodDTO in foods)
            {
                FlowLayoutPanel pn = new FlowLayoutPanel () { Width = BUS_Food.Width, Height = BUS_Food.Height, Font = new Font("Segoe", 9), BorderStyle = BorderStyle.FixedSingle };
                pn.Click += Pn_Click;
                PictureBox pictureBox = new PictureBox() { Width = BUS_Food.Width - 10, Height = BUS_Food.Height - 130, Font = new Font("Segoe", 9) };
                Label lb = new Label() { Width = BUS_Food.Width - 10, Height = BUS_Food.Height - 190, Font = new Font("Segoe", 9), ForeColor = Color.White };
                pictureBox.Image = foodDTO.FoodPhoto;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Click += Pn_Click;
                lb.Text = foodDTO.FoodName + "\n" + foodDTO.Price + "đ";
                lb.TextAlign = ContentAlignment.MiddleCenter;
                lb.Click += Pn_Click;
                pictureBox.Tag = foodDTO;
                pn.Controls.Add(pictureBox);
                lb.Tag = foodDTO;
                pn.Tag = foodDTO;
                pn.Controls.Add(lb);

                flpFood.Controls.Add(pn);
            }
        }

        void loadComboboxArea()
        {
            List<AreaDTO> list = BUS_Area.Instance.GetAreaList();
            CboAreaList.DataSource = list;
            CboAreaList.DisplayMember = "AreaName";
            if (list != null && list.Count > 0)
                loadComboboxTable(list[0].AreaID);
        }

        void loadComboboxTable(int AreaID)
        {
            cboTableList.DataSource = BUS_Table.Instance.GetTableList(AreaID);
            cboTableList.DisplayMember = "TableName";
        }


        bool checkClickTable = false;
        private void Pn_Click(object sender, EventArgs e)
        {
            if (checkClickTable)
            {
                FoodDTO foodDTO = (sender as Control).Tag as FoodDTO;
                if (foodNumber.ContainsKey(foodDTO.FoodName))
                {
                    GUI_Keyboard frmKeyboard = new GUI_Keyboard(tableDTO.TableID ,tableDTO.TableName, foodNumber[foodDTO.FoodName], foodDTO);
                    frmKeyboard.ShowDialog();
                }
                else
                {
                    GUI_Keyboard frmKeyboard = new GUI_Keyboard(tableDTO.TableID ,tableDTO.TableName, new List<string> { "", "" }, foodDTO);
                    frmKeyboard.ShowDialog();
                }
                showBill(tableDTO.TableID);
                loadTable();
            }
        }

        void loadTable()
        {
            flpTable.Controls.Clear();
            List<AreaDTO> areaList = BUS_Area.Instance.GetAreaList();
            foreach (AreaDTO area in areaList)
            {
                List<TableDTO> tableList = BUS_Table.Instance.GetTableList(area.AreaID);

                int heightFlp;
                if (tableList.Count % 3 == 0)
                {
                    heightFlp = tableList.Count / 3 * 135 + 60;
                }
                else
                {
                    heightFlp = (tableList.Count / 3 + 1) * 135 + 60;
                }

                FlowLayoutPanel flp = new FlowLayoutPanel() { Width = flpTable.Width - 5, Height =  heightFlp };
                Label lbl = new Label() { Width = flpTable.Width - 6, Height = BUS_Area.Height };
                lbl.Text = area.AreaName;
                lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                lbl.BackColor = Color.LightSalmon;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                flp.Controls.Add(lbl);

                foreach (TableDTO table in tableList)
                {
                    Button btn = new Button() { Width = BUS_Table.Width, Height = BUS_Table.Height };
                    btn.Text = table.TableName + "\n" + table.Status;
                    btn.Click += Btn_Click;
                    btn.Font = new Font("Segoe UI", 9);
                    btn.ForeColor = Color.Black;
                    btn.Tag = table;

                    switch (table.Status)
                    {
                        case "Trống":
                            btn.BackColor = Color.White;
                            break;
                        default:
                            btn.BackColor = Color.Pink;
                            break;
                    }
                    flp.Controls.Add(btn);
                }
                flpTable.Controls.Add(flp);
            }
        }

        Dictionary<string, List<string>> foodNumber = new Dictionary<string, List<string>>();
        int customerID;
        string username;
        void showBill(int id)
        {
            lsvBill.Items.Clear();
            List<MenuDTO> billInfo = BUS_Menu.Instance.GetMenuByTableID(id);
            double total = 0;
            lblTableName.Text = tableDTO.TableName;
            lblTimeIn.Text = "Giờ đến:  ";
            lblStatus.Text = "Tình trạng:  ";
            lblCustomerName.Text = "Khách:  ";
            lblStaffName.Text = "Yêu cầu:  ";
            
            foodNumber = new Dictionary<string, List<string>>();

            
            if (billInfo.Count > 0)
            {
                lblTableName.Text = billInfo[0].TableName;
                lblTimeIn.Text += billInfo[0].DateCheckIN.ToString("HH:mm");
                
                lblStatus.Text += billInfo[0].Status;
                lblCustomerName.Text += billInfo[0].CustomerName;
                username = Encryption.Instance.Decrypt(billInfo[0].Username); 
                lblStaffName.Text += BUS_Account.Instance.GetAccountByUserName(username).DisplayName;
                customerID = billInfo[0].CustomerID;
            }
            else
            {
                customerID = 0;
                username = "";
            }

            foreach (MenuDTO menu in billInfo)
            {
                ListViewItem list = new ListViewItem(new string[] { menu.FoodName, menu.Number.ToString(), menu.Price.ToString(), menu.Discount.ToString(), menu.TotalPrice.ToString(), menu.Note });
                list.Tag = menu;
                total += menu.TotalPrice;
                if (!foodNumber.ContainsKey(menu.FoodName))
                {
                    foodNumber[menu.FoodName] = new List<string>();
                }
                foodNumber[menu.FoodName].Add(menu.Number.ToString());
                foodNumber[menu.FoodName].Add(menu.Discount.ToString());
                foodNumber[menu.FoodName].Add(menu.Note);

                lsvBill.Items.Add(list);
            }
            lsvBill.Tag = tableDTO;
            CultureInfo cultureInfo = new CultureInfo("vi-VN");
            txtTotal.Text = total.ToString("c", cultureInfo);
        }

        TableDTO tableDTO;
        private void Btn_Click(object sender, EventArgs e)
        {
            checkClickTable = true;
            tableDTO = (sender as Button).Tag as TableDTO;
            showBill(tableDTO.TableID);
        }
        #endregion

        private void cboCategory_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem == null)
                return;

            CategoryDTO category = comboBox.SelectedItem as CategoryDTO;

           loadFood(category.CategoryID);
        }

        private void PnBillInformation_Click(object sender, EventArgs e)
        {
            if (checkClickTable)
            {
                int billID = BUS_Bill.Instance.GetUncheckBillIDByTableID(tableDTO.TableID);
                if (billID != -1)
                {
                    GUI_BillInfomation frmBillInfomation = new GUI_BillInfomation(username, customerID, billID);
                    frmBillInfomation.ShowDialog();
                }
            }
        }

        private void UserTable_Paint(object sender, PaintEventArgs e)
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

        private void flpTable_Paint(object sender, PaintEventArgs e)
        {
            int borderWidth = 2;
            Color borderColor = Color.White;

            FlowLayoutPanel flpanel = sender as FlowLayoutPanel;

            int borderX = flpanel.Location.X + flpanel.Width;
            int borderY = flpanel.Location.Y;
            int borderHeightWithPanel = flpanel.Height;

            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawLine(pen, borderX, borderY, borderX, borderY + borderHeightWithPanel);
            }
        }

        private void pnFoodList_Paint(object sender, PaintEventArgs e)
        {
            int borderWidth = 2;
            Color borderColor = Color.White;

            Panel panel = sender as Panel;

            int borderX = 0;
            int borderY = 0;
            int borderHeightWithPanel = panel.Height;

            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawLine(pen, borderX, borderY, borderX, borderY + borderHeightWithPanel);
            }
        }

        private void flpFood_Paint(object sender, PaintEventArgs e)
        {
            int borderWidth = 2;
            Color borderColor = Color.White;

            FlowLayoutPanel flpanel = sender as FlowLayoutPanel;

            int borderX = 0;
            int borderY = 0;
            int borderXEnd = flpanel.Width;

            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawLine(pen, borderX, borderY, borderXEnd, borderY);
            }
        }

        private void lsvBill_Click(object sender, EventArgs e)
        {
            if (lsvBill.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lsvBill.SelectedItems[0];

                MenuDTO menu = selectedItem.Tag as MenuDTO;

                GUI_Keyboard frmKeyboard = new GUI_Keyboard(tableDTO.TableID, tableDTO.TableName, menu);
                frmKeyboard.ShowDialog();
                showBill(tableDTO.TableID);
            }
        }

        private void CboAreaList_SelectedIndexChanged(object sender, EventArgs e)
        {
            AreaDTO area = CboAreaList.SelectedItem as AreaDTO;

            loadComboboxTable(area.AreaID);
        }

        private void btnChangeTable_Click(object sender, EventArgs e)
        {
            if (checkClickTable)
            {
                TableDTO table = lsvBill.Tag as TableDTO;
                TableDTO table1 = cboTableList.SelectedItem as TableDTO;
                int id1 = table.TableID;
                int id2 = table1.TableID;
                string status = table.Status;

                if (status != "Trống")
                {
                    if (MessageBox.Show(string.Format("Bạn có thật sự muốn chuyển {0} sang {1} không?", (lsvBill.Tag as TableDTO).TableName.ToLower(), (cboTableList.SelectedItem as TableDTO).TableName.ToLower()), "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        if (customerID == 0)
                        {
                            BUS_Table.Instance.SwitchTable(id1, id2, BUS_Account.Username, table.TableName, table1.TableName);
                            loadTable();
                        }
                        else
                        {
                            BUS_Table.Instance.SwitchTable(id1, id2, BUS_Account.Username, table.TableName, table1.TableName, customerID);
                            loadTable();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Bạn không thể chuyển một bàn trống sang bàn khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Bạn phải chọn một bàn trước khi muốn chuyển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            TableDTO table = lsvBill.Tag as TableDTO;
            if (table != null)
            {
                int billID = BUS_Bill.Instance.GetUncheckBillIDByTableID(table.TableID);
                int discount;
                int.TryParse(nmDiscount.Value.ToString(), out discount);
                double totalMoney = Convert.ToDouble(txtTotal.Text.Split(',')[0]);

                if (billID != -1)
                {
                    if (MessageBox.Show("Bạn có chắc chắn muốn thanh toán cho " + table.TableName.ToLower() + $" không?\n Tổng số tiền là: {totalMoney}", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        if (chkIn.Checked == true)
                        {
                            if (totalMoney2 <= totalMoney)
                            {
                                totalMoney2 = totalMoney;
                            }
                            List<MenuDTO> list = BUS_Menu.Instance.GetMenuByTableID(table.TableID);
                            frmBill frmBill = new frmBill(list, totalMoney, discount, totalMoney2);
                            frmBill.ShowDialog();
                        }

                        BUS_Bill.Instance.UpdateBill(billID, discount, totalMoney);
                        showBill(table.TableID);
                        loadTable();
                        chkIn.Checked = false;
                    }
                }
            }
            nmDiscount.Value = 0;
        }

        double totalMoney2;

        private void nmDiscount_ValueChanged(object sender, EventArgs e)
        {
            TableDTO table = lsvBill.Tag as TableDTO;
            if (table != null)
            {
                List<MenuDTO> billInfo = BUS_Menu.Instance.GetMenuByTableID(table.TableID);
                double total = billInfo.Sum(item => item.TotalPrice);
                totalMoney2 = total;
                int discount = (int)nmDiscount.Value;
                double totalMoney = total - (total / 100 * discount);
                CultureInfo cultureInfo = new CultureInfo("vi-VN");
                txtTotal.Text = totalMoney.ToString("c", cultureInfo);
            }
        }

    }
}

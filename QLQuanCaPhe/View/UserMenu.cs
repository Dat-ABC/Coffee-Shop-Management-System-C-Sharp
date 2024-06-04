using QLQuanCaPhe.BUS;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QLQuanCaPhe
{
    public partial class UserMenu : UserControl
    {
        public UserMenu()
        {
            InitializeComponent();
        }

        private void UserItems_Load(object sender, EventArgs e)
        {
            loadDgvCategory();
            loadFood();
            loadComboboxCategory();

            btnDeleteCategory.BackColor = Color.DarkGray;
            btnFixCategory.BackColor = Color.DarkGray;
            btnFixFood.BackColor = Color.DarkGray;
            btnDeleteFood.BackColor = Color.DarkGray;
        }

        void loadFood(List<FoodDTO> list = null)
        {
            dgvFood.Rows.Clear();
            if (list == null)
            {
                list = BUS_Food.Instance.GetFoodList();
            }

            foreach (FoodDTO food in list)
            {
                int rowIndex = dgvFood.Rows.Add();

                // Lấy hàng vừa thêm vào
                DataGridViewRow row = dgvFood.Rows[rowIndex];

                // Gán giá trị từ đối tượng FoodDTO vào các ô tương ứng trong hàng
                row.Cells["Category"].Value = food.CategoryName;
                row.Cells["FoodName"].Value = food.FoodName;
                row.Cells["FoodPhoto"].Value = food.FoodPhoto;
                row.Cells["Price"].Value = food.Price;
                row.Cells["Unit"].Value = food.Unit;
                row.Cells["Food_ID"].Value = food.FoodID;
                row.Cells["Category_ID"].Value = food.CategoryID;
            }
        }

        void loadDgvCategory()
        {
            flpCategory.Controls.Clear();
            List<CategoryDTO> categoryList = BUS_Category.Instance.GetCategoryList();

            foreach (CategoryDTO category in categoryList)
            {
                Button btn = new Button() { Height = 50 };
                btn.Text = category.CategoryName;
                btn.Click += Btn_Click;
                btn.Font = new Font("Segoe UI", 9);
                btn.Tag = category;

                Size textSize = TextRenderer.MeasureText(category.CategoryName, btn.Font);
                int width = textSize.Width + 20;


                btn.Width = Math.Max(width, 100);

                flpCategory.Controls.Add(btn);
            }
            loadComboboxCategory();
        }

        void loadComboboxCategory()
        {
            cboCategory.DataSource = BUS_Category.Instance.GetCategoryList();
            cboCategory.DisplayMember = "CategoryName";
        }

        int categoryID;

        private void Btn_Click(object sender, EventArgs e)
        {
            CategoryDTO Category = (sender as Button).Tag as CategoryDTO;
            categoryID = Category.CategoryID;
            txtCategoryName.Text = Category.CategoryName;
            btnFixCategory.Enabled = true;
            btnDeleteCategory.Enabled = true;
            btnDeleteCategory.BackColor = Color.FromArgb(53, 41, 123);
            btnFixCategory.BackColor = Color.FromArgb(53, 41, 123);
            showFoodList(categoryID);
            refesh();
        }

        private void flpCategory_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs;
            if (me != null)
            {
                Control control = ((FlowLayoutPanel)sender).GetChildAtPoint(me.Location);
                if (!(control is Button))
                {
                    btnFixCategory.Enabled = false;
                    btnDeleteCategory.Enabled = false;
                    btnDeleteCategory.BackColor = Color.DarkGray;
                    btnFixCategory.BackColor = Color.DarkGray;
                    loadFood();
                }
            }
        }

        void showFoodList(int categoryID)
        {
            List<FoodDTO> list = BUS_Food.Instance.GetFoodListByCategoryID(categoryID);
            loadFood(list);
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            if (txtFind.Text != "")
            {
                foreach (DataGridViewRow row in dgvFood.Rows)
                {
                    object cellValue = row.Cells["FoodName"].Value;
                    if (cellValue != null && cellValue != DBNull.Value && cellValue.ToString().ToLower().Contains(txtFind.Text.ToLower()))
                    {
                        row.Selected = true;
                        dgvFood.CurrentCell = row.Cells[0];
                        dgvFood.FirstDisplayedScrollingRowIndex = row.Index; // Di chuyển để hiển thị hàng trên màn hình
                        break;
                    }
                }
            }
            else
            {
                dgvFood.FirstDisplayedScrollingRowIndex = 0;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtFind.Text = "";
            txtCategoryName.Text = "";
            txtPrice.Text = "";
            txtFoodName.Text = "";
            cboCategory.SelectedIndex = 0;
        }

        private void pnCategoryList_Paint(object sender, PaintEventArgs e)
        {
            int borderWidth = 2;
            Color borderColor = Color.White;

            Panel panel = sender as Panel;

            int borderX = 0;
            int borderY = panel.Height;
            int borderHeightWithPanel = panel.Width;

            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawLine(pen, borderX, borderY, borderX + borderHeightWithPanel, borderY);
            }
        }

        private void UserMenu_Paint(object sender, PaintEventArgs e)
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

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txtCategoryName.Text;
            if (MessageBox.Show($"Bạn có chắc chắn muốn thêm danh mục {name} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (name.Trim() != "")
                {
                    if (BUS_Category.Instance.InsertCategory(name))
                    {
                        MessageBox.Show("Thêm danh mục thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDgvCategory();
                        txtCategoryName.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Tên danh mục này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Tên danh mục không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnFixCategory_Click(object sender, EventArgs e)
        {
            string name = txtCategoryName.Text;
            if (MessageBox.Show($"Bạn có chắc chắn muốn sửa thông tin danh mục không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (name.Trim() != "")
                {
                    if (BUS_Category.Instance.UpdateCategory(categoryID, name))
                    {
                        MessageBox.Show("Sửa thông tin danh mục thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDgvCategory();
                        txtCategoryName.Text = "";
                        loadFood();
                    }
                    else
                    {
                        MessageBox.Show("Tên danh mục này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Tên danh mục không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            string name = txtCategoryName.Text;
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa thông tin danh mục {name} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (BUS_Category.Instance.DeleteCategory(categoryID))
                {
                    MessageBox.Show("Xóa thông tin danh mục thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDgvCategory();
                    txtCategoryName.Text = "";
                    loadFood();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi xóa danh mục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txtFoodName.Text;
            if (MessageBox.Show($"Bạn có chắc chắn muốn thêm món {name} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (txtFoodName.Text.Trim() != "" && txtPrice.Text.Trim() != "")
                {
                    int CategoryID = (cboCategory.SelectedItem as CategoryDTO).CategoryID;
                    if (float.TryParse(txtPrice.Text, out float price) && price > 0 && price < 100000000)
                    {
                        if (BUS_Food.Instance.InsertFood(name, price, CategoryID, picFoodImage.Image, txtUnit.Text))
                        {
                            BUS_Diary.Instance.InsertDiary(DateTime.Now, "Quản lý món ăn", $"Thêm món {name}", BUS_Account.DisplayName);
                            MessageBox.Show("Thêm món thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadFood();
                            refesh();
                        }
                        else
                        {
                            MessageBox.Show("Tên món ăn này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Giá phải lớn hơn 0 và nhỏ hơn 100 triệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Tên món ăn hoặc giá không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnFixFood_Click(object sender, EventArgs e)
        {
            string name = txtFoodName.Text;
            if (MessageBox.Show($"Bạn có chắc chắn muốn sửa thông tin món không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (txtFoodName.Text.Trim() != "" && txtPrice.Text.Trim() != "")
                {
                    int CategoryID = (cboCategory.SelectedItem as CategoryDTO).CategoryID;
                    if (float.TryParse(txtPrice.Text, out float price) && price > 0 && price < 100000000)
                    {
                        if (BUS_Food.Instance.UpdateFood(FoodID, name, price, CategoryID, picFoodImage.Image, txtUnit.Text))
                        {
                            BUS_Diary.Instance.InsertDiary(DateTime.Now, "Quản lý món ăn", $"Sửa thông tin món ăn là: {foodName} -> {name}, giá {price2} -> {price}", BUS_Account.DisplayName);
                            MessageBox.Show("Sửa món thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadFood();
                            refesh();
                        }
                        else
                        {
                            MessageBox.Show("Tên món ăn này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Giá phải lớn hơn 0 và nhỏ hơn 100 triệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Tên món ăn hoặc giá không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            string name = txtFoodName.Text;
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa thông tin món {name} không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (BUS_Food.Instance.DeleteFood(FoodID))
                {
                    BUS_Diary.Instance.InsertDiary(DateTime.Now, "Quản lý món ăn", $"Xóa thông tin món ăn là: {foodName}", BUS_Account.DisplayName);
                    MessageBox.Show("Xóa món thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadFood();
                    refesh();
                }
                else
                {
                    MessageBox.Show("Xóa không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        int FoodID;
        string foodName;
        string price2;

        private void dgvFood_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0 && rowIndex < dgvFood.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvFood.Rows[rowIndex];
                foodName = txtFoodName.Text = selectedRow.Cells["FoodName"].Value.ToString();
                picFoodImage.Image = (Image)selectedRow.Cells["foodPhoto"].Value;
                price2 = txtPrice.Text = selectedRow.Cells["price"].Value.ToString();
                txtUnit.Text = selectedRow.Cells["Unit"].Value.ToString();



                FoodID = (int)dgvFood.Rows[rowIndex].Cells["Food_ID"].Value;

                btnFixFood.Enabled = true;
                btnDeleteFood.Enabled = true;
                btnFixFood.BackColor = Color.FromArgb(53, 41, 123);
                btnDeleteFood.BackColor = Color.FromArgb(53, 41, 123);
            }
            else
            {
                btnFixFood.Enabled = false;
                btnDeleteFood.Enabled = false;
                btnFixFood.BackColor = Color.DarkGray;
                btnDeleteFood.BackColor = Color.DarkGray;
                refesh();
            }
        }

        void refesh()
        {
            txtFoodName.Text = "";
            txtPrice.Text = "";
            txtUnit.Text = "";
            cboCategory.SelectedIndex = 0;
            picFoodImage.Image = null;
        }

        private void btnCleanFood_Click(object sender, EventArgs e)
        {
            refesh();
        }

        private void btnCleanCategory_Click(object sender, EventArgs e)
        {
            txtCategoryName.Text = "";
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add(Type.Missing);

            for (int i = 2; i < dgvFood.Columns.Count + 1; i++)
            {
                excel.Cells[1, i - 1] = dgvFood.Columns[i - 1].HeaderText.Trim();
            }

            for (int i = 0; i < dgvFood.Rows.Count; i++)
            {
                for (int j = 1; j < dgvFood.Columns.Count; j++)
                {
                    if (dgvFood.Rows[i].Cells[j].Value is Image)
                    {
                        byte[] b = ImageEncryption.Instance.ImageToByteArray((Image)dgvFood.Rows[i].Cells[j].Value);

                        string base64String = Convert.ToBase64String(b);

                        excel.Cells[i + 2, j] = base64String;
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(Convert.ToString(dgvFood.Rows[i].Cells[j].Value)))
                        {
                            excel.Cells[i + 2, j] = dgvFood.Rows[i].Cells[j].Value.ToString();
                        }
                        else
                        {
                            excel.Cells[i + 2, j] = "";
                        }
                    }
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

                BUS_Diary.Instance.InsertDiary(DateTime.Now, "Xuất danh sách món ăn", "Xuất danh sách món ăn", BUS_Account.DisplayName);

                MessageBox.Show("Xuất Excel thành công!");
            }
            excel.Quit();
        }

        private void picFoodImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png; *.jpg; *.jpeg; *.gif; *.bmp)|*.png; *.jpg; *.jpeg; *.gif; *.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                Console.WriteLine(filePath);

                picFoodImage.Image = Image.FromFile(filePath);
            }
            else
            {
                picFoodImage.Image = null;
            }
        }

        private void txtFoodName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvFood.SelectedCells[0].OwningRow != null && dgvFood.SelectedCells[0].OwningRow.Cells[0].Value != null)
                {
                    int id = (int)dgvFood.SelectedCells[0].OwningRow.Cells["Category_ID"].Value;

                    int index = -1;
                    int i = 0;
                    foreach (CategoryDTO item in cboCategory.Items)
                    {
                        if (item.CategoryID == id)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }

                    cboCategory.SelectedIndex = index;
                }
            }
            catch { }
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FoodName", typeof(string));
            dt.Columns.Add("FoodPhoto", typeof(byte[]));
            dt.Columns.Add("Price", typeof(float));
            dt.Columns.Add("CategoryID", typeof(int));
            dt.Columns.Add("Unit", typeof(string));
            dt.Columns.Add("IsDeleteFood", typeof(int));
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


                    List<FoodDTO> list = BUS_Food.Instance.GetFoodList();

                    for (int i = 2; i < range.Rows.Count + 1; i++)
                    {
                        string foodName = range.Cells[i, 2].Value.ToString();

                        bool exists = list.Any(food => food.FoodName == foodName);

                        if (!exists)
                        {
                            string categoryName = range.Cells[i, 1].Value.ToString();
                            byte[] b = null;
                            if (!string.IsNullOrWhiteSpace(Convert.ToString(range.Cells[i, 3].Value)))
                            {
                                b = Convert.FromBase64String(range.Cells[i, 3].Value.ToString() + "=");
                            }
                            float price = (float)range.Cells[i, 4].Value;
                            string unit = range.Cells[i, 5].Value.ToString();
                            int categoryID = (int)range.Cells[i, 6].Value;

                            dt.Rows.Add(new object[] { foodName, b, price, categoryID, unit, 1 });
                        }
                    }
                    workbook.Close();
                    App.Quit();
                }
            }
            catch { }
            if (dt.Rows.Count > 0)
            {
                if (BUS_Food.Instance.InsertFoodToExcel(dt))
                {
                    BUS_Diary.Instance.InsertDiary(DateTime.Now, "Nhập danh sách món ăn", "Nhập danh sách món ăn", BUS_Account.DisplayName);
                    MessageBox.Show("Nhập dữ liệu từ excel thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadFood();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi nhập dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}

using QLQuanCaPhe.BUS;
using QLQuanCaPhe.DAO;
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
using System.Xml.Linq;

namespace QLQuanCaPhe
{
    public partial class UserArea : UserControl
    {
        BindingSource categoryList = new BindingSource();
        public UserArea()
        {
            InitializeComponent();
        }

        private void UserCategory_Load(object sender, EventArgs e)
        {
            btnAddTable.BackColor = Color.DarkGray;
            btnFixTable.BackColor = Color.DarkGray;
            btnDeleteTable.BackColor = Color.DarkGray;
            btnFixArea.BackColor = Color.DarkGray;
            btnDeleteArea.BackColor = Color.DarkGray;
            loadDgvArea();
        }

        void loadDgvArea()
        {
            dgvArea.DataSource = BUS_Area.Instance.GetAreaList();
            dgvArea.Columns["AreaID"].Visible = false;
            dgvArea.Columns["AreaName"].HeaderText = "Tên khu vực";
        }

        void loadDgvTable()
        {
            dgvTable.Columns.Clear();
            dgvTable.DataSource = BUS_Table.Instance.GetTableList(AreaID);
            dgvTable.Columns["tableID"].Visible = false;
            dgvTable.Columns["Status"].Visible = false;
            dgvTable.Columns["AreaID"].Visible = false;
            dgvTable.Columns["TableName"].HeaderText = "Tên bàn";
        }

        private void pnTable_Paint(object sender, PaintEventArgs e)
        {
            int borderWidth = 1;
            Color borderColor = Color.White;

            Panel pn = sender as Panel;

            int borderX = 0;
            int borderY = 0;
            int borderXEnd = pn.Height;

            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawLine(pen, borderX, borderY, borderX, borderXEnd);
            }
        }

        private void UserArea_Paint(object sender, PaintEventArgs e)
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

        private void btnAddArea_Click(object sender, EventArgs e)
        {
            string name = txtAreName.Text;
            if (name.Trim() != "")
            {
                if (MessageBox.Show($"Bạn có chắc chắn muốn thêm khu vực {name} không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    if (BUS_Area.Instance.InsertArea(name))
                    {
                        MessageBox.Show("Thêm khu vực thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDgvArea();
                        clean();
                    }
                    else
                    {
                        MessageBox.Show("Tên khu vực này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Tên không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnFixArea_Click(object sender, EventArgs e)
        {
            string name = txtAreName.Text;
            if (name.Trim() != "")
            {
                if (MessageBox.Show($"Bạn có chắc chắn muốn sửa thông tin khu vực {name} không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    if (BUS_Area.Instance.UpdateArea(AreaID, name))
                    {
                        MessageBox.Show("Sửa thông tin khu vực thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDgvArea();
                        clean();
                    }
                    else
                    {
                        MessageBox.Show("Tên khu vực này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Tên không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCleanArea_Click(object sender, EventArgs e)
        {
            clean();
        }

        void clean()
        {
            txtAreName.Text = "";
            txtFindArea.Text = "";

            btnFixArea.BackColor = Color.DarkGray;
            btnDeleteArea.BackColor = Color.DarkGray;
            btnFixArea.Enabled = false;
            btnDeleteArea.Enabled = false;
        }

        int AreaID;

        private void dgvArea_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0 && rowIndex < dgvArea.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvArea.Rows[rowIndex];
                txtAreName.Text = selectedRow.Cells["AreaName"].Value.ToString();
                AreaID = (int)selectedRow.Cells["AreaID"].Value;
                btnFixArea.Enabled = true;
                btnDeleteArea.Enabled = true;
                btnAddTable.Enabled = true;
                btnAddTable.BackColor = Color.FromArgb(53, 41, 123);
                btnFixArea.BackColor = Color.FromArgb(53, 41, 123);
                btnDeleteArea.BackColor = Color.FromArgb(53, 41, 123);
                loadDgvTable();
            }
            else
            {
                btnFixArea.Enabled = false;
                btnDeleteArea.Enabled = false;
                btnFixArea.BackColor = Color.DarkGray;
                btnDeleteArea.BackColor = Color.DarkGray;
                btnAddTable.Enabled = false;
                btnAddTable.BackColor = Color.DarkGray;
                clean();
            }
        }

        private void btnDeleteArea_Click(object sender, EventArgs e)
        {
            string name = txtAreName.Text;
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa thông tin khu vực {name} không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (BUS_Area.Instance.DeleteArea(AreaID))
                {
                    MessageBox.Show("Xóa thông tin khu vực thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDgvArea();
                    clean();
                }
                else
                {
                    MessageBox.Show("Có lỗi khi xóa khu vực", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void txtFindArea_TextChanged(object sender, EventArgs e)
        {
            if (txtFindArea.Text != "")
            {
                foreach (DataGridViewRow row in dgvArea.Rows)
                {
                    object cellValue = row.Cells["AreaName"].Value;
                    if (cellValue != null && cellValue != DBNull.Value && cellValue.ToString().ToLower().Contains(txtFindArea.Text.ToLower()))
                    {
                        row.Selected = true;
                        dgvArea.CurrentCell = row.Cells[0];
                        dgvArea.FirstDisplayedScrollingRowIndex = row.Index; // Di chuyển để hiển thị hàng trên màn hình
                        break;
                    }
                }
            }
            else
            {
                dgvArea.FirstDisplayedScrollingRowIndex = 0;
            }
        }



        private void dgvTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0 && rowIndex < dgvTable.Rows.Count)
            {
                DataGridViewRow selectedRow = dgvTable.Rows[rowIndex];
                txtTableName.Text = selectedRow.Cells["TableName"].Value.ToString();
                TableID = (int)selectedRow.Cells["TableID"].Value;
                btnFixTable.Enabled = true;
                btnDeleteTable.Enabled = true;
                btnFixTable.BackColor = Color.FromArgb(53, 41, 123);
                btnDeleteTable.BackColor = Color.FromArgb(53, 41, 123);
            }
            else
            {
                btnFixTable.Enabled = false;
                btnDeleteTable.Enabled = false;
                btnFixTable.BackColor = Color.DarkGray;
                btnDeleteTable.BackColor = Color.DarkGray;
                cleanTable();
            }
        }

        void cleanTable()
        {
            txtFindTable.Text = "";
            txtTableName.Text = "";
            btnFixTable.BackColor = Color.DarkGray;
            btnDeleteTable.BackColor = Color.DarkGray;
            btnFixTable.Enabled = false;
            btnDeleteTable.Enabled = false;
        }
        private void btnCleanTable_Click(object sender, EventArgs e)
        {
            cleanTable();
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txtTableName.Text;
            if (name.Trim() != "")
            {
                if (MessageBox.Show($"Bạn có chắc chắn muốn thêm bàn {name} không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    if (BUS_Table.Instance.InsertTable(name, AreaID))
                    {
                        MessageBox.Show($"Thêm bàn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDgvTable();
                        cleanTable();
                    }
                    else
                    {
                        MessageBox.Show($"Tên bàn {name} này đã tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
            }
            else
            {
                MessageBox.Show($"Tên bàn không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnFixTable_Click(object sender, EventArgs e)
        {
            string name = txtTableName.Text;
            if (name.Trim() != "")
            {
                if (MessageBox.Show($"Bạn có chắc chắn muốn sửa thông tin bàn không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    if (BUS_Table.Instance.UpdateTable(TableID, name))
                    {
                        MessageBox.Show($"Sửa thông tin bàn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDgvTable();
                        cleanTable();
                    }
                }
            }
            else
            {
                MessageBox.Show($"Tên bàn không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtFindTable_TextChanged(object sender, EventArgs e)
        {
            if (txtFindTable.Text != "")
            {
                foreach (DataGridViewRow row in dgvTable.Rows)
                {
                    object cellValue = row.Cells["TableName"].Value;
                    if (cellValue != null && cellValue != DBNull.Value && cellValue.ToString().ToLower().Contains(txtFindTable.Text.ToLower()))
                    {
                        row.Selected = true;
                        dgvTable.CurrentCell = row.Cells[0];
                        dgvTable.FirstDisplayedScrollingRowIndex = row.Index; // Di chuyển để hiển thị hàng trên màn hình
                        break;
                    }
                }
            }
            else
            {
                dgvTable.FirstDisplayedScrollingRowIndex = 0;
            }
        }

        int TableID;

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            string name = txtTableName.Text;
            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa thông tin bàn {name} không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                if (BUS_Table.Instance.DeleteTable(TableID))
                {
                    MessageBox.Show($"Xóa thông tin bàn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadDgvTable();
                    cleanTable();
                }
            }
        }
    }
}

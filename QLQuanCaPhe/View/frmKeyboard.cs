using QLQuanCaPhe.BUS;
using QLQuanCaPhe.DAO;
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
    public partial class GUI_Keyboard : Form
    {
        public GUI_Keyboard()
        {
            InitializeComponent();
        }

        public GUI_Keyboard(int tableID, string tableName, List<string> number, FoodDTO food)
        {
            InitializeComponent();
            lblTableName.Text = tableName;
            lblNumber.Text = number[0];
            number2 = number[0];
            lblFoodName.Text = food.FoodName;
            if (number[0] == "")
            {
                txtNumber.Text = "1";
            }

            price2 = food.Price;

            txtPrice.Text = food.Price.ToString();
            txtDiscount.Text = number[1];
            TableID = tableID;
            foodID = food.FoodID;
            if (number.Count < 3)
            {
                txtRequest.Text = "";
            }
            else
            {
                txtRequest.Text = number[2];
            }
        }

        public GUI_Keyboard(int tableID, string tableName, MenuDTO menu)
        {
            InitializeComponent();
            lblTableName.Text = tableName;
            TableID = tableID;
            lblNumber.Text = menu.Number.ToString();
            number2 = menu.Number.ToString();
            lblFoodName.Text = menu.FoodName;
            if (menu.Number.ToString() == "")
            {
                txtNumber.Text = "1";
            }

            price2 = menu.Price;

            txtPrice.Text = menu.Price.ToString();
            txtDiscount.Text = menu.Discount.ToString();
            foodID = menu.FoodID;
            txtRequest.Text = menu.Note;
        }

        string number2;
        int TableID;
        int foodID;
        float price2;

        private void picExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmKeyboard_Load(object sender, EventArgs e)
        {

        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {
            
        }

        byte count = 1;

        void calculate(string number)
        {
            if (count == 1)
            {
                txtNumber.Text = number;
                count = 0;
            }
            else
            {
                if (txtNumber.Text.Length < 6)
                {
                    txtNumber.Text += number;
                }
            }

            int b = int.Parse(txtNumber.Text);
            if (int.TryParse(number2, out int a) && a < b)
            {
                lblNumber.Text = number2 + " + " + (b - a).ToString();
            }
            else if (a > b)
            {
                int c = a - b;
                lblNumber.Text = number2 + " - " + c.ToString();
                txtNumber.Text = b.ToString();
            }
            else
            {
                lblNumber.Text = txtNumber.Text;
            }
        }

        private void btnFour_Click(object sender, EventArgs e)
        {
            calculate("4");
        }

        private void btnTwo_Click(object sender, EventArgs e)
        {
            calculate("2");
        }

        private void btnThree_Click(object sender, EventArgs e)
        {
            calculate("3");
        }

        private void btnFive_Click(object sender, EventArgs e)
        {
            calculate("5");
        }

        private void btnSix_Click(object sender, EventArgs e)
        {
            calculate("6");
        }

        private void btnSeven_Click(object sender, EventArgs e)
        {
            calculate("7");
        }

        private void btnOne_Click(object sender, EventArgs e)
        {
            calculate("1");
        }

        private void btnNine_Click(object sender, EventArgs e)
        {
            calculate("9");
        }

        private void btnZero_Click(object sender, EventArgs e)
        {
            if (txtNumber.Text.Length > 0)
            {
                count = 2;
                calculate("0");
            }
            else
            {
                txtNumber.Text = "0";
                lblNumber.Text = "Hủy yêu cầu!";
                count = 1;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtNumber.Text = "0";
            lblNumber.Text = "Hủy yêu cầu!";
            count = 1;
        }

        private void btnIncrease_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtNumber.Text, out int a) && a < 99999)
            {
                txtNumber.Text = (int.Parse(txtNumber.Text) + 1).ToString();
                lblNumber.Text = txtNumber.Text;
            }
        }

        private void btnReduce_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtNumber.Text, out int a) && a > 0)
            {
                txtNumber.Text = (int.Parse(txtNumber.Text) - 1).ToString();
                lblNumber.Text = txtNumber.Text;
            }
        }

        private void btnLittle_Click(object sender, EventArgs e)
        {
            txtRequest.Text += "Ít ";
        }

        private void btnStone_Click(object sender, EventArgs e)
        {
            txtRequest.Text += "Đá ";
        }

        private void btnHot_Click(object sender, EventArgs e)
        {
            txtRequest.Text += "Nóng ";
        }

        private void btnMuch_Click(object sender, EventArgs e)
        {
            txtRequest.Text += "Nhiều ";
        }

        private void btnMilk_Click(object sender, EventArgs e)
        {
            txtRequest.Text += "Sữa ";
        }

        private void btnCold_Click(object sender, EventArgs e)
        {
            txtRequest.Text += "Lạnh ";
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            txtRequest.Text += "Không ";
        }

        private void btnSugar_Click(object sender, EventArgs e)
        {
            txtRequest.Text += "Đường ";
        }

        private void btnSweet_Click(object sender, EventArgs e)
        {
            txtRequest.Text += "Ngọt ";
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            txtRequest.Text += "Có ";
        }

        private void btnGinger_Click(object sender, EventArgs e)
        {
            txtRequest.Text += "Gừng ";
        }

        private void btnSour_Click(object sender, EventArgs e)
        {
            txtRequest.Text += "Chua ";
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtRequest.Text += "1 ";
        }

        private void btnComma_Click(object sender, EventArgs e)
        {
            txtRequest.Text += ", ";
        }

        private void btnX_Click(object sender, EventArgs e)
        {
            txtRequest.Text = "";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            int billID = BillDAO.Instance.GetUncheckBillIDByTableID(TableID);
            float price = (float)Convert.ToDouble(txtPrice.Text);
            int count;
            if (txtNumber.Text == "")
            {
                count = Convert.ToInt32(lblNumber.Text);
            }
            else
            {
                count = Convert.ToInt32(txtNumber.Text);
            }
            
            float totalMoney = price * count;

            if (billID == -1)
            {
                if (count > 0)
                {
                    BUS_Bill.Instance.InsertBill(TableID);
                    if (int.TryParse(txtDiscount.Text, out int discount))
                    {
                        BUS_BillInfo.Instance.InsertBillInfo(BUS_Bill.Instance.getMaxIDBill(), foodID, count, totalMoney, txtRequest.Text, discount);
                    }
                    else
                    {
                        BUS_BillInfo.Instance.InsertBillInfo(BUS_Bill.Instance.getMaxIDBill(), foodID, count, totalMoney, txtRequest.Text);
                    }
                }
            }
            else
            {
                if (int.TryParse(txtDiscount.Text, out int discount))
                {
                    BUS_BillInfo.Instance.InsertBillInfo(billID, foodID, count, totalMoney, txtRequest.Text, discount);
                }
                else
                {
                    BUS_BillInfo.Instance.InsertBillInfo(billID, foodID, count, totalMoney, txtRequest.Text);
                }
            }
            Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chkDonate_CheckedChanged(object sender, EventArgs e)
        {
            txtPrice.Text = "0";
        }



        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            if (!chkDonate.Checked)
            {
                if (float.TryParse(txtDiscount.Text, out float discount))
                {
                    float price3 = price2;
                    price3 -= price3 * discount * 0.01f;
                    txtPrice.Text = price3.ToString();
                }
                else
                {
                    txtPrice.Text = price2.ToString();
                }
            }
        }
    }
}

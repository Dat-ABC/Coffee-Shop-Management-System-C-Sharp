using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class BillInfoDTO
    {
        private int billInfoID;
        private int billID;
        private int foodID;
        private int number;
        private int discount;
        private double totalMoney;
        private string note;

        public int BillInfoID { get => billInfoID; set => billInfoID = value; }
        public int BillID { get => billID; set => billID = value; }
        public int FoodID { get => foodID; set => foodID = value; }
        public int Number { get => number; set => number = value; }
        public int Discount { get => discount; set => discount = value; }
        public double TotalMoney { get => totalMoney; set => totalMoney = value; }
        public string Note { get => note; set => note = value; }

        public BillInfoDTO(int id, int billID, int foodID, int number, double totalMoney, int discount, string note) 
        { 
            this.BillInfoID = id;
            this.BillID = billID;
            this.FoodID = foodID;
            this.Number = number;
            this.TotalMoney = totalMoney;
            this.Discount = discount;
            this.Note = note;
        }

        public BillInfoDTO(DataRow row) 
        {
            this.BillInfoID = (int)row["BillInfoID"];
            this.BillID = (int)row["billID"];
            this.FoodID = (int)row["foodID"];
            this.Number = (int)row["quantity"];
            this.TotalMoney = Convert.ToDouble(totalMoney);
            var discount = row["Discount"];
            if (discount.ToString() != "")
            {
                this.Discount = (int)discount;
            }
            this.Note = row["note"].ToString();
        }
    }
}

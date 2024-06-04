using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class BillDetailDTO
    {
        string foodName;
        int quantity;
        int discount;
        float price;
        double totalMoney;
        string note;


        public string FoodName { get => foodName; set => foodName = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public int Discount { get => discount; set => discount = value; }
        public float Price { get => price; set => price = value; }
        public double TotalMoney { get => totalMoney; set => totalMoney = value; }
        public string Note { get => note; set => note = value; }

        public BillDetailDTO() { }

        public BillDetailDTO(string foodName, int quantity, int discount, float price, double totalMoney, string note)
        {
            FoodName = foodName;
            Quantity = quantity;
            Discount = discount;
            TotalMoney = totalMoney;
            Note = note;
            Price = price;
        }

        public BillDetailDTO(DataRow row)
        {
            FoodName = row["foodName"].ToString();
            Quantity = (int)row["quantity"];
            var discount = row["discount"];
            if (discount.ToString() != "")
            {
                Discount = (int)discount; 
            }
            TotalMoney = (double)row["totalMoney"];
            Note = row["note"].ToString();
            Price = (float)Convert.ToDouble(row["price"]);
        }
    }
}

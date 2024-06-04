using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class RevenueDTO
    {
        private string foodName;
        private int quantity;
        private double totalMoney;

        public string FoodName { get => foodName; set => foodName = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public double TotalMoney { get => totalMoney; set => totalMoney = value; }

        public RevenueDTO(string foodName, int quantity,double totalMoney) 
        {
            this.FoodName = foodName;
            this.Quantity = quantity;
            this.TotalMoney = totalMoney;
        }

        public RevenueDTO(DataRow row)
        {
            this.FoodName = row["foodName"].ToString();
            this.Quantity = (int)row["quantity"];
            this.TotalMoney = Convert.ToDouble(row["totalMoney"]);
        }
    }
}

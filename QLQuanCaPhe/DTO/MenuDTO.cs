using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class MenuDTO
    {
        private int foodID;
        private string foodName;
        private int number;
        private float price;
        private float totalPrice;
        private DateTime dateCheckIN;
        private string status;
        private string customerName;
        private string tableName;
        private string username;
        private string note;
        private int customerID;
        private int billID;
        private int discount;

        public int FoodID { get => foodID; set => foodID = value; }
        public string FoodName { get => foodName; set => foodName = value; }
        public int Number { get => number; set => number = value; }
        public float Price { get => price; set => price = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
        public DateTime DateCheckIN { get => dateCheckIN; set => dateCheckIN = value; }
        public string Status { get => status; set => status = value; }
        public string CustomerName { get => customerName; set => customerName = value; }
        public string TableName { get => tableName; set => tableName = value; }
        public string Username { get => username; set => username = value; }
        public string Note { get => note; set => note = value; }
        public int CustomerID { get => customerID; set => customerID = value; }
        public int BillID { get => billID; set => billID = value; }
        public int Discount { get => discount; set => discount = value; }

        public MenuDTO(string name, int number, float price, float totalPirce, DateTime dateCheckIn, string status, string customerName, string tableName, string username, int foodID, string note, int customerID, int billID, int discount) 
        {
            this.FoodName = name;
            this.Number = number;
            this.Price = price;
            this.TotalPrice = totalPirce;
            this.DateCheckIN = dateCheckIn;
            this.Status = status;
            this.CustomerName = customerName;
            this.TableName = tableName;
            this.Username = username;
            this.FoodID = foodID;
            this.Note = note;
            this.CustomerID = customerID;
            this.BillID = billID;
            this.Discount = discount;
        }

        public MenuDTO(DataRow row) 
        {
            this.FoodName = row["foodName"].ToString();
            this.Number = (int)row["Quantity"];
            this.Price = (float)Convert.ToDouble(row["price"]);
            var total = row["totalMoney"];
            if (total.ToString() != "")
                this.totalPrice = (float)Convert.ToDouble(total);
            var dateCheckIn = row["dateCheckIn"];
            if (dateCheckIN.ToString() != "")
                this.DateCheckIN = (DateTime)dateCheckIn;
            this.Status = row["status"].ToString();
            var customerName = row["CustomerName"];
            if (customerName.ToString() != "")
                this.CustomerName = customerName.ToString();
            this.TableName = row["tableName"].ToString();
            this.Username = row["UserName"].ToString();
            this.FoodID = (int)row["foodID"];
            this.Note = row["note"].ToString();

            var customerID = row["CustomerID"];
            if (customerID.ToString() != "")
                this.CustomerID = (int)customerID;
            this.BillID = (int)row["billID"];
            var discount = row["discount"];
            if (discount.ToString() != "")
                this.Discount = (int)discount;
        }
    }
}

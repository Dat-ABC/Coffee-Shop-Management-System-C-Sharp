using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class BillDTO
    {
        private int billID;
        private DateTime? dateIn;
        private DateTime? dateOut;
        private int status;
        private int discount;
        private double totalMoney;
        private int tableID;
        private string username;
        private int customerID;

        public int BillID { get => billID; set => billID = value; }
        public DateTime? DateIn { get => dateIn; set => dateIn = value; }
        public DateTime? DateOut { get => dateOut; set => dateOut = value; }
        public int Status { get => status; set => status = value; }
        public int Discount { get => discount; set => discount = value; }
        public double TotalMoney { get => totalMoney; set => totalMoney = value; }
        public int TableID { get => tableID; set => tableID = value; }
        public string Username { get => username; set => username = value; }
        public int CustomerID { get => customerID; set => customerID = value; }

        public BillDTO(int id, DateTime? dateIn, DateTime? dateOut, int status, int discount, double totalMoney, int tableID, string username, int customerID)
        {
            this.BillID = id;
            this.DateIn = dateIn;
            this.DateOut = dateOut;
            this.Status = status;
            this.Discount = discount;
            this.TotalMoney = totalMoney;
            this.TableID = tableID;
            this.Username = username;
            this.CustomerID = customerID;
        }

        public BillDTO(DataRow row) 
        {
            this.BillID = (int)row["BillID"];
            this.DateIn = (DateTime?)row["dateCheckIn"];
            var dateOut = row["DateCheckOut"];
            if (dateOut.ToString() != "")
                this.DateOut = (DateTime?)dateOut;
            this.Status = (int)row["status"];
            var discount = row["discount"];
            if (discount.ToString() != "")
            {
                this.Discount = (int)discount;
            }
            var total = row["totalMoney"];
            if (total.ToString() != "")
                this.TotalMoney = Convert.ToDouble(total);
            this.TableID = (int)row["tableID"];
            this.Username = row["username"].ToString();
            var customerID = row["CustomerID"];
            if (customerID.ToString() != "")
                this.CustomerID = (int)customerID;
        }
    }
}

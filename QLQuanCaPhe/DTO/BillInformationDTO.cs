using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class BillInformationDTO
    {
        int billID;
        string tableName;
        DateTime dateCheckIn;
        DateTime dateCheckOut;
        int discount;
        float totalMoney;
        string customerName;
        string displayName;

        public int BillID { get => billID; set => billID = value; }
        public string TableName { get => tableName; set => tableName = value; }
        public DateTime DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int Discount { get => discount; set => discount = value; }
        public float TotalMoney { get => totalMoney; set => totalMoney = value; }
        public string CustomerName { get => customerName; set => customerName = value; }
        public string DisplayName { get => displayName; set => displayName = value; }

        public BillInformationDTO() { }

        public BillInformationDTO(int billID, string tableName, DateTime dateCheckIn, DateTime dateCheckOut, int discount, float totalMoney, string customerName, string displayName)
        {
            BillID = billID;
            TableName = tableName;
            DateCheckIn = dateCheckIn;
            DateCheckOut = dateCheckOut;
            Discount = discount;
            TotalMoney = totalMoney;
            CustomerName = customerName;
            DisplayName = displayName;
        }

        public BillInformationDTO(DataRow row)
        {
            BillID = (int)row["billID"];
            TableName = row["tableName"].ToString();
            DateCheckIn = (DateTime)row["dateCheckIn"];
            DateCheckOut = (DateTime)row["dateCheckOut"];
            Discount = (int)row["discount"];
            TotalMoney = (float)Convert.ToDouble(row["totalMoney"]);
            CustomerName = row["customerName"].ToString();
            DisplayName = row["displayName"].ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class ExportDetailDTO
    {
        int exportDetailID;
        string itemName;
        float quantity;
        double price;
        double totalMoney;
        int exportID;
        int itemID;
        string unit;

        public int ExportDetailID { get => exportDetailID; set => exportDetailID = value; }
        public string ItemName { get => itemName; set => itemName = value; }
        public float Quantity { get => quantity; set => quantity = value; }
        public string Unit { get => unit; set => unit = value; }
        public double Price { get => price; set => price = value; }
        public double TotalMoney { get => totalMoney; set => totalMoney = value; }
        public int ExportID { get => exportID; set => exportID = value; }
        public int ItemID { get => itemID; set => itemID = value; }

        public ExportDetailDTO() { }

        public ExportDetailDTO(int exportDetailID, string itemName, float quantity, double price, double totalMoney, int exportID, int itemID, string unit)
        {
            ExportDetailID = exportDetailID;
            ItemName = itemName;
            Quantity = quantity;
            Price = price;
            TotalMoney = totalMoney;
            ExportID = exportID;
            ItemID = itemID;
            Unit = unit;
        }

        public ExportDetailDTO(DataRow row)
        {
            ExportDetailID = (int)row["EDetailID"];
            ItemName = row["itemName"].ToString();
            Quantity = (float)Convert.ToDouble(row["quantity"]);
            Price = (double)row["price"];
            TotalMoney = (double)row["totalMoney"];
            ExportID = (int)row["exportID"];
            ItemID = (int)row["itemID"];
            Unit = row["unit"].ToString();
        }
    }
}

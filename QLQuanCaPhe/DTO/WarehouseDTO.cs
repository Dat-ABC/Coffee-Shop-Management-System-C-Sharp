using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class WarehouseDTO
    {
        int itemID;
        string itemName;
        float quantity;
        string unit;
        double price;
        string itemType;

        public int ItemID { get => itemID; set => itemID = value; }
        public string ItemName { get => itemName; set => itemName = value; }
        public float Quantity { get => quantity; set => quantity = value; }
        public string Unit { get => unit; set => unit = value; }
        public double Price { get => price; set => price = value; }
        public string ItemType { get => itemType; set => itemType = value; }

        public WarehouseDTO() { }

        public WarehouseDTO(int itemID, string itemName, float quantity, string unit, double price, string itemType)
        {
            ItemID = itemID;
            ItemName = itemName;
            Quantity = quantity;
            Unit = unit;
            Price = price;
            ItemType = itemType;
        }

        public WarehouseDTO(DataRow row)
        {
            ItemID = (int)row["itemID"];
            ItemName = row["itemName"].ToString();
            Quantity = (float)Convert.ToDouble(row["actualQuantity"]);
            Unit = row["unit"].ToString();
            Price = (double)row["price"];
            ItemType = row["itemType"].ToString();
        }
    }
}

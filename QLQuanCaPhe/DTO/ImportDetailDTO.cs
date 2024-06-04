using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class ImportDetailDTO
    {
        int importDetailID;
        string itemName;
        float quantity;
        double price;
        float discount;
        double totalMoney;
        int importID;
        int itemID;
        int supplierID;
        string unit;
        string supplierName;

        public int ImportDetailID { get => importDetailID; set => importDetailID = value; }
        public string ItemName { get => itemName; set => itemName = value; }
        public float Quantity { get => quantity; set => quantity = value; }
        public string Unit { get => unit; set => unit = value; }
        public double Price { get => price; set => price = value; }
        public float Discount { get => discount; set => discount = value; }
        public double TotalMoney { get => totalMoney; set => totalMoney = value; }
        public int ImportID { get => importID; set => importID = value; }
        public int ItemID { get => itemID; set => itemID = value; }
        public int SupplierID { get => supplierID; set => supplierID = value; }
        public string SupplierName { get => supplierName; set => supplierName = value; }

        public ImportDetailDTO() { }

        public ImportDetailDTO(int importDetailID, string itemName, float quantity, double price, float discount, double totalMoney, int importID, int itemID, int supplierID, string unit, string supplierName)
        {
            ImportDetailID = importDetailID;
            ItemName = itemName;
            Quantity = quantity;
            Price = price;
            Discount = discount;
            TotalMoney = totalMoney;
            ImportID = importID;
            ItemID = itemID;
            SupplierID = supplierID;
            Unit = unit;
            SupplierName = supplierName;
        }

        public ImportDetailDTO(DataRow row)
        {
            ImportDetailID = (int)row["IDetailID"];
            ItemName = row["itemName"].ToString();
            Quantity = (float)Convert.ToDouble(row["quantity"]);
            Price = (double)row["price"];
            var discount = row["discount"];
            if (discount.ToString() != "")
                Discount = (float)Convert.ToDouble(discount);
            TotalMoney = (double)row["totalMoney"];
            ImportID = (int)row["importID"];
            ItemID = (int)row["itemID"];
            var supplierID = row["supplierID"];
            if (supplierID.ToString() != "")
                SupplierID = (int)supplierID;
            Unit = row["unit"].ToString();
            SupplierName = row["supplierName"].ToString();
        }
    }
}

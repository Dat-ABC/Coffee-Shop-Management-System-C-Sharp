using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class InventoryDTO
    {
        int inventoryID;
        DateTime date;
        float quantity;
        float actualQuantity;
        string note;
        float loss;
        string displayName;
        string username;
        int itemID;

        public int InventoryID { get => inventoryID; set => inventoryID = value; }
        public DateTime Date { get => date; set => date = value; }
        public float Quantity { get => quantity; set => quantity = value; }
        public float ActualQuantity { get => actualQuantity; set => actualQuantity = value; }
        public string Note { get => note; set => note = value; }
        public float Loss { get => loss; set => loss = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public string Username { get => username; set => username = value; }
        public int ItemID { get => itemID; set => itemID = value; }

        public InventoryDTO() { }

        public InventoryDTO(int inventoryID, DateTime date, float quantity, float actualQuantity, string note, float loss, string displayName, string username, int itemID)
        {
            InventoryID = inventoryID;
            Date = date;
            Quantity = quantity;
            ActualQuantity = actualQuantity;
            Note = note;
            Loss = loss;
            DisplayName = displayName;
            Username = username;
            ItemID = itemID;
        }

        public InventoryDTO(DataRow row)
        {
            InventoryID = (int)row["iventoryID"];
            Date = (DateTime)row["date"];
            Quantity = (float)Convert.ToDouble(row["quantity"]);
            ActualQuantity = (float)Convert.ToDouble(row["actualQuantity"]);
            Note = row["note"].ToString();
            Loss = (float)Convert.ToDouble(row["loss"]);
            DisplayName = row["displayName"].ToString();
            Username = row["username"].ToString();
            ItemID = (int)row["itemID"];
        }
    }
}

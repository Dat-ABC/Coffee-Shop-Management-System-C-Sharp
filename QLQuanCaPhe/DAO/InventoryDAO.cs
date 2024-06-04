using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QLQuanCaPhe.DAO
{
    public class InventoryDAO
    {
        private static InventoryDAO instance;

        public static InventoryDAO Instance 
        { 
            get 
            { 
                if (instance == null) 
                    instance = new InventoryDAO(); 
                return instance; 
            } 
        }

        private InventoryDAO() { }

        public List<InventoryDTO> GetInventoryList(int itemID)
        {
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_GetInventoryList @ItemID", new object[] { itemID });

            List<InventoryDTO> list = new List<InventoryDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                InventoryDTO inventory = new InventoryDTO(dr);
                list.Add(inventory);
            }

            return list;
        }

        public bool InsertInventory(float quantity, float actualQuantity, string username, int itemID)
        {
            username = Encryption.Instance.Encrypt(username);
            string note = $"Trước khi cập nhập, số lượng: {quantity}, số lượng thực tế: {actualQuantity}";
            float loss = Math.Abs(actualQuantity - quantity);
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertInventory @Date , @quantity , @actualQuantity , @username , @itemID , @note , @loss", new object[] { DateTime.Now, actualQuantity, actualQuantity, username, itemID, note, loss });

            return result > 0;
        }

        public void InsertInventoryFromImportDetail(float quantity, string username, int itemID, string unit)
        {
            username = Encryption.Instance.Encrypt(username);
            string note = $"Nhập {quantity} {unit}";
            float loss = 0;
            clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertInventoryFromImportDetail @Date , @quantity , @actualQuantity , @username , @itemID , @note , @loss", new object[] { DateTime.Now, quantity, quantity, username, itemID, note, loss });
        }

        public void InsertInventoryFromExportDetail(float quantity, string username, int itemID, string unit)
        {
            username = Encryption.Instance.Encrypt(username);
            string note = $"Xuất {quantity} {unit}";
            float loss = 0;

            clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertInventoryFromExportDetail @Date , @quantity , @actualQuantity , @username , @itemID , @note , @loss", new object[] { DateTime.Now, quantity, quantity, username, itemID, note, loss });
        }

        public InventoryDTO GetInventoryMax(int itemID)
        {
            InventoryDTO inventory = null;
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_MAX_Inventory @ITEMID", new object[] { itemID });
            if (dt.Rows.Count > 0)
            {
                inventory = new InventoryDTO(dt.Rows[0]);
            }

            return inventory;
        }
    }
}

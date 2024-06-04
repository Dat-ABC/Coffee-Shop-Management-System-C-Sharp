using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.BUS
{
    public class BUS_Inventory
    {
        private static BUS_Inventory instance;

        public static BUS_Inventory Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Inventory();
                return instance;
            }
        }

        private BUS_Inventory() { }

        public List<InventoryDTO> GetInventoryList(int itemID)
        {
            return InventoryDAO.Instance.GetInventoryList(itemID);
        }

        public bool InsertInventory(float quantity, float actualQuantity, int itemID)
        {
            return InventoryDAO.Instance.InsertInventory(quantity, actualQuantity, BUS_Account.Username, itemID);
        }

        public void InsertInventoryFromImportDetail(float quantity, int itemID, string unit)
        {
            InventoryDAO.Instance.InsertInventoryFromImportDetail(quantity, BUS_Account.Username, itemID, unit);
        }

        public void InsertInventoryFromExportDetail(float quantity, int itemID, string unit)
        {
            InventoryDAO.Instance.InsertInventoryFromExportDetail(quantity, BUS_Account.Username, itemID, unit);
        }

        public InventoryDTO GetInventoryMax(int itemID)
        {
            return InventoryDAO.Instance.GetInventoryMax(itemID);
        }
    }
}

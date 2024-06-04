using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.BUS
{
    public class BUS_Items
    {
        private static BUS_Items instance;

        public static BUS_Items Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Items();
                return instance;
            }
        }

        private BUS_Items() { }

        public List<ItemsDTO> GetItemList()
        {
            return ItemsDAO.Instance.GetItemList();
        }

        public List<ItemsDTO> GetItemListByItemType(string itemType)
        {
            return ItemsDAO.Instance.GetItemListByItemType(itemType);
        }

        public ItemsDTO GetItemByName(string name, string itemType)
        {
            return ItemsDAO.Instance.GetItemByName(name, itemType);
        }

        public ItemsDTO GetItemByItemID(int itemID)
        { 
            return ItemsDAO.Instance.GetItemByItemID(itemID);
        }

        public bool InsertItem(string itemName, string unit, double price, string itemType)
        {
            if (GetItemByName(itemName, itemType) == null)
            {
                return ItemsDAO.Instance.InsertItem(itemName, unit, price, itemType);
            }
            return false;
        }

        public bool UpdateItem(int itemID, string itemName, string unit, double price, string itemType)
        {
            ItemsDTO items = GetItemByName(itemName, itemType);
            if (items == null || items.ItemID == itemID)
            {
                return ItemsDAO.Instance.UpdateItem(itemID, itemName, unit, price, itemType);
            }
            return false;
        }
    }
}

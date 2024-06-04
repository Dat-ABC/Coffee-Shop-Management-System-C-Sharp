using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DAO
{
    public class ItemsDAO
    {
        private static ItemsDAO instance;

        public static ItemsDAO Instance 
        {
            get
            {
                if (instance == null)
                    instance = new ItemsDAO();
                return instance;
            }
        }

        private ItemsDAO() { }

        public List<ItemsDTO> GetItemList()
        {
            DataTable table = clsDatabaseDAO.Instance.excuteQuery("Select * from Item");
            List<ItemsDTO> list = new List<ItemsDTO>();

            foreach (DataRow row in table.Rows)
            {
                ItemsDTO item = new ItemsDTO(row);
                list.Add(item);
            }

            list.Sort((x, y) => string.Compare(x.ItemType, y.ItemType));

            return list;
        }

        public List<ItemsDTO> GetItemListByItemType(string itemType)
        {
            DataTable table = clsDatabaseDAO.Instance.excuteQuery($"Select * from Item where itemtype = N'{itemType}'");
            List<ItemsDTO> list = new List<ItemsDTO>();

            foreach (DataRow row in table.Rows)
            {
                ItemsDTO item = new ItemsDTO(row);
                list.Add(item);
            }

            list.Sort((x, y) => string.Compare(x.ItemType, y.ItemType));

            return list;
        }

        public ItemsDTO GetItemByName(string name, string itemType)
        {
            ItemsDTO item = null;
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery($"select * from item where itemName = N'{name}' and itemType = N'{itemType}'");
            if (dt.Rows.Count > 0)
            {
                item = new ItemsDTO(dt.Rows[0]);
            }

            return item;
        }

        public ItemsDTO GetItemByItemID(int itemID)
        {
            ItemsDTO item = null;
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery($"select * from item where itemID =" + itemID);
            if (dt.Rows.Count > 0)
            {
                item = new ItemsDTO(dt.Rows[0]);
            }

            return item;
        }

        public bool InsertItem(string itemName, string unit, double price, string itemType)
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertItem @itemName , @unit , @price , @itemType", new object[] { itemName, unit, price, itemType });

            return result > 0;
        }

        public bool UpdateItem(int itemID, string itemName, string unit, double price, string itemType)
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_UpdateItem @itemID , @itemName , @unit , @price , @itemType", new object[] { itemID, itemName, unit, price, itemType });

            return result > 0;
        }
    }
}

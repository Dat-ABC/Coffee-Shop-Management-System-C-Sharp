using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.BUS
{
    public class BUS_Menu
    {
        private static BUS_Menu instance;

        public static BUS_Menu Instance 
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Menu();
                return instance;
            }
        }

        private BUS_Menu() { }

        public List<MenuDTO> GetMenuByTableID(int TableID)
        {
            return MenuDAO.Instance.getMenuByTable(TableID);
        }

        public List<MenuDTO> GetMenuToPrint(int BillID)
        {
            return MenuDAO.Instance.GetMenuToPrint(BillID);
        }
    }
}

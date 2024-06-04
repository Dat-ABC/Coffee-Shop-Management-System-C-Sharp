using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.BUS
{
    public class BUS_ImportOrder
    {
        private static BUS_ImportOrder instance;

        public static BUS_ImportOrder Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_ImportOrder();
                return instance;
            }
        }

        private BUS_ImportOrder() { }

        public List<ImportOrderDTO> GetImportOrderList(DateTime start, DateTime end)
        {
            return ImportOrderDAO.Instance.GetImportOrderList(start, end);
        }

        public bool InsertImportOrder(DateTime date, string username, double totalMoney = 0)
        {
            return ImportOrderDAO.Instance.InsertImportOrder(date, username, totalMoney);
        }

        public bool UpdateImportOrder(int importID, string username, double totalMoney = 0)
        { 
            return ImportOrderDAO.Instance.UpdateImportOrder(importID, username, totalMoney);
        }
    }
}

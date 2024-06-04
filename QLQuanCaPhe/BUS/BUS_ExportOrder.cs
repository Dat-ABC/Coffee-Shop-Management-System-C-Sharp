using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.BUS
{
    public class BUS_ExportOrder
    {
        private static BUS_ExportOrder instance;

        public static BUS_ExportOrder Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_ExportOrder();
                return instance;
            }
        }

        private BUS_ExportOrder() { }

        public List<ExportOrderDTO> GetExportOrderList(DateTime start, DateTime end)
        {
            return ExportOrderDAO.Instance.GetExportOrderList(start, end);
        }

        public bool InsertExportOrder(DateTime date, string username, double totalMoney = 0)
        {
            return ExportOrderDAO.Instance.InsertExportOrder(date, username, totalMoney);
        }

        public bool UpdateExportOrder(int importID, string username, double totalMoney = 0)
        {
            return ExportOrderDAO.Instance.UpdateExportOrder(importID, username, totalMoney);
        }
    }
}

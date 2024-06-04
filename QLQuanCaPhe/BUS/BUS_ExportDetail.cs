using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.BUS
{
    public class BUS_ExportDetail
    {
        private static BUS_ExportDetail instance;

        public static BUS_ExportDetail Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_ExportDetail();
                return instance;
            }
        }

        private BUS_ExportDetail() { }

        public List<ExportDetailDTO> GetExportDetailList(int exportID)
        {
            return ExportDetailDAO.Instance.GetExportDetailList(exportID);
        }

        public bool InsertExportDetail(float quatity, double price, double totalMoney, int exportID, int itemID, string unit)
        {
            BUS_Inventory.Instance.InsertInventoryFromExportDetail(quatity, itemID, unit);
            return ExportDetailDAO.Instance.InsertExportDetail(quatity, price, totalMoney, exportID, itemID, unit);
        }
    }
}

using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace QLQuanCaPhe.BUS
{
    public class BUS_ImportDetail
    {
        private static BUS_ImportDetail instance;

        public static BUS_ImportDetail Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_ImportDetail();
                return instance;
            }
        }

        private BUS_ImportDetail() { }

        public List<ImportDetailDTO> GetImportDetailList(int importID)
        {
            return ImportDetailDAO.Instance.GetImportDetailList(importID);
        }

        public bool InsertImportDetail(float quatity, double price, double totalMoney, int importID, int itemID, string unit, int supplierID = 0, float discount = 0)
        {
            BUS_Inventory.Instance.InsertInventoryFromImportDetail(quatity, itemID, unit);
            return ImportDetailDAO.Instance.InsertImportDetail(quatity, price, totalMoney, importID, itemID, unit, supplierID, discount);
        }
    }
}

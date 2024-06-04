using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DAO
{
    public class ImportDetailDAO
    {
        private static ImportDetailDAO instance;

        public static ImportDetailDAO Instance 
        {
            get
            {
                if (instance == null)
                    instance = new ImportDetailDAO();
                return instance;
            }
        }

        private ImportDetailDAO() { }

        public List<ImportDetailDTO> GetImportDetailList(int ImportID)
        {
            List<ImportDetailDTO> list = new List<ImportDetailDTO>();

            DataTable data = clsDatabaseDAO.Instance.excuteQuery("SP_GetImportDetailList @ImportID", new object[] { ImportID });

            foreach (DataRow row in data.Rows)
            {
                ImportDetailDTO importDetail = new ImportDetailDTO(row);
                list.Add(importDetail);
            }

            return list;
        }

        public bool InsertImportDetail(float quatity, double price, double totalMoney, int importID, int itemID, string unit, int supplierID = 0, float discount = 0)
        {
            int result;
            if(supplierID == 0 && discount == 0)
            {
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertImportDetal @Quantity , @price , @totalMoney , @importID , @itemID , @unit", new object[] { quatity, price, totalMoney, importID, itemID, unit });
            }
            else if (supplierID != 0 && discount == 0)
            {
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertImportDetal @Quantity , @price , @totalMoney , @importID , @itemID , @unit , @supplierID", new object[] { quatity, price, totalMoney, importID, itemID, unit, supplierID });
            }
            else if (supplierID == 0 && discount != 0)
            {
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertImportDetal @Quantity , @price , @totalMoney , @importID , @itemID , @unit , @discount", new object[] { quatity, price, totalMoney, importID, itemID, unit, discount });
            }
            else
            {
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertImportDetal @Quantity , @price , @totalMoney , @importID , @itemID , @unit , @supplierID , @discount", new object[] { quatity, price, totalMoney, importID, itemID, unit, supplierID, discount });
            }

            return result > 0;
        }
    }
}

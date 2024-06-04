using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DAO
{
    public class ExportDetailDAO
    {
        private static ExportDetailDAO instance;

        public static ExportDetailDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new ExportDetailDAO();
                return instance;
            }
        }

        private ExportDetailDAO() { }

        public List<ExportDetailDTO> GetExportDetailList(int ExportID)
        {
            List<ExportDetailDTO> list = new List<ExportDetailDTO>();

            DataTable data = clsDatabaseDAO.Instance.excuteQuery("SP_GetExportDetailList @ExportID", new object[] { ExportID });

            foreach (DataRow row in data.Rows)
            {
                ExportDetailDTO exportDetail = new ExportDetailDTO(row);
                list.Add(exportDetail);
            }

            return list;
        }

        public bool InsertExportDetail(float quatity, double price, double totalMoney, int exportID, int itemID, string unit)
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertExportDetail @Quantity , @price , @totalMoney , @exportID , @itemID , @unit", new object[] { quatity, price, totalMoney, exportID, itemID, unit });

            return result > 0;
        }
    }
}

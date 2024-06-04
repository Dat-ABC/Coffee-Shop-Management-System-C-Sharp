using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DAO
{
    public class ExportOrderDAO
    {
        private static ExportOrderDAO instance;

        public static ExportOrderDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new ExportOrderDAO();
                return instance;
            }
        }

        private ExportOrderDAO() { }

        public List<ExportOrderDTO> GetExportOrderList(DateTime start, DateTime end)
        {
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_GetExportOrder @start , @end", new object[] { start, end });

            List<ExportOrderDTO> list = new List<ExportOrderDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                ExportOrderDTO dto = new ExportOrderDTO(dr);
                list.Add(dto);
            }

            return list;
        }

        public bool InsertExportOrder(DateTime date, string username, double totalMoney = 0)
        {
            username = Encryption.Instance.Encrypt(username);
            int result;
            if (totalMoney == 0)
            {
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertExportOrder @date , @username", new object[] { date, username });
            }
            else
            {
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertExportOrder @date , @username , @totalMoney", new object[] { date, username, totalMoney });
            }

            return result > 0;
        }

        public bool UpdateExportOrder(int exportID, string username, double totalMoney = 0)
        {
            username = Encryption.Instance.Encrypt(username);
            int result;
            if (totalMoney == 0)
            {
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_UpdateExportOrder @ExportID , @username", new object[] { exportID, username });
            }
            else
            {
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_UpdateExportOrder @ExportID , @username , @totalMoney", new object[] { exportID, username, totalMoney });
            }

            return result > 0;
        }
    }
}

using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DAO
{
    public class ImportOrderDAO
    {
        private static ImportOrderDAO instance;

        public static ImportOrderDAO Instance 
        {
            get
            {
                if (instance == null)
                    instance = new ImportOrderDAO();
                return instance;
            }
        }

        private ImportOrderDAO() { }

        public List<ImportOrderDTO> GetImportOrderList(DateTime start, DateTime end) 
        {
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_GetImportOrder @start , @end", new object[] { start, end });

            List<ImportOrderDTO> list = new List<ImportOrderDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                ImportOrderDTO dto = new ImportOrderDTO(dr);
                list.Add(dto);
            }

            return list;
        }

        public bool InsertImportOrder(DateTime date, string username, double totalMoney = 0)
        {
            username = Encryption.Instance.Encrypt(username);
            int result;
            if (totalMoney == 0)
            {
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertImportOrder @date , @username", new object[] { date, username });
            }
            else
            {
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertImportOrder @date , @username , @totalMoney", new object[] { date, username, totalMoney });
            }

            return result > 0;
        }

        public bool UpdateImportOrder(int importID, string username, double totalMoney = 0)
        {
            username = Encryption.Instance.Encrypt(username);
            int result;
            if (totalMoney == 0)
            {
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_UpdateImportOrder @importID , @username", new object[] { importID, username });
            }
            else
            {
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_UpdateImportOrder @importID , @username , @totalMoney", new object[] { importID, username, totalMoney });
            }

            return result > 0;
        }
    }
}

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
    public class BillInformationDAO
    {
        private static BillInformationDAO instance;

        public static BillInformationDAO Instance 
        {
            get
            {
                if (instance == null)
                    instance = new BillInformationDAO();
                return instance;
            }
        }

        private BillInformationDAO() { }

        public List<BillInformationDTO> GetBillListByDateAndPage(DateTime checkIn, DateTime checkOut, int pageNum)
        {
            List<BillInformationDTO> list = new List<BillInformationDTO>();
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_GetListBillByDateAndPage @checkIn , @checkOut , @page", new object[] { checkIn, checkOut, pageNum });

            foreach (DataRow row in dt.Rows)
            {
                BillInformationDTO billInformation = new BillInformationDTO(row);
                list.Add(billInformation);
            }
            return list;
        }

        public int GetNumBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            return (int)clsDatabaseDAO.Instance.excuteScaler("SP_GetNumBillByDate @checkIn , @checkOut", new object[] { checkIn, checkOut });
        }
    }
}

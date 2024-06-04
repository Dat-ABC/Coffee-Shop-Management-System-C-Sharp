using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.BUS
{
    public class BUS_BillInformation
    {
        private static BUS_BillInformation instance;

        public static BUS_BillInformation Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_BillInformation();
                return instance;
            }
        }

        private BUS_BillInformation() { }

        public List<BillInformationDTO> GetBillListByDateAndPage(DateTime checkIn, DateTime checkOut, int pageNum)
        {
            return BillInformationDAO.Instance.GetBillListByDateAndPage(checkIn, checkOut, pageNum);
        }

        public int GetNumBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            return BillInformationDAO.Instance.GetNumBillListByDate(checkIn, checkOut);
        }
    }
}

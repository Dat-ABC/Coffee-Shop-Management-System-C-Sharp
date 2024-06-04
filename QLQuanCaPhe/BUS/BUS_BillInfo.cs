using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.BUS
{
    public class BUS_BillInfo
    {
        private static BUS_BillInfo instance;

        public static BUS_BillInfo Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_BillInfo();
                return instance;
            }
        }

        private BUS_BillInfo() { }

        public List<BillInfoDTO> GetListBillInfo(int BillID)
        {
            return BillInfoDAO.Instance.GetListBillInfo(BillID);
        }

        public void InsertBillInfo(int billID, int foodID, int number, double totalMoney, string note, int discount = 0)
        {
            BillInfoDAO.Instance.insertBillInfo(billID, foodID, number, totalMoney, note, discount);
        }

        public void deleteBillInfo(int BillID)
        {
            BillInfoDAO.Instance.deleteBillInfo(BillID);
        }

        public List<BillDetailDTO> getBillDetailList(int BillID)
        {
            return BillInfoDAO.Instance.getBillDetailList(BillID);
        }
    }
}

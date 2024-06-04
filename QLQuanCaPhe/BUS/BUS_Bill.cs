using QLQuanCaPhe.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.BUS
{
    public class BUS_Bill
    {
        private static BUS_Bill instance;

        public static BUS_Bill Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Bill();
                return instance;
            }
        }

        private BUS_Bill() { }

        public int GetUncheckBillIDByTableID(int TableID)
        {
            return BillDAO.Instance.GetUncheckBillIDByTableID(TableID);
        }

        public void InsertBill(int tableID)
        {
            BillDAO.Instance.insertBill(tableID, BUS_Account.Username);
        }

        public void UpdateBill(int id, int discount, double totalMoney)
        {
            BillDAO.Instance.UpdateBill(id, discount, totalMoney);
        }

        public void Delete(int tableID)
        {
            BillDAO.Instance.deleteBill(tableID);
        }

        public int getMaxIDBill()
        {
            return BillDAO.Instance.getMaxIDBill();
        }

        public void UpdateAddInformationBill(int billID, string username, int CustomerID = 0)
        {
            BillDAO.Instance.UpdateAddInformationBill(billID, username, CustomerID);
        }
    }
}


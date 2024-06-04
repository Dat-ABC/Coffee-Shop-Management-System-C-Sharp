using QLQuanCaPhe.BUS;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanCaPhe.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return instance; }
        }

        public static int width1 = 100;
        public static int height1 = 20;

        public static int width2 = 60;
        public static int height2 = 20;

        private BillDAO() { }

        /// <summary>
        /// lấy bill id
        /// không có: -1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetUncheckBillIDByTableID(int TableID)
        {
            DataTable data = clsDatabaseDAO.Instance.excuteQuery("SP_GetUncheckBillIDByTableID @tableID", new object[] { TableID });

            if (data.Rows.Count > 0)
            {
                BillDTO bill = new BillDTO(data.Rows[0]);
                return bill.BillID;
            }

            return -1;
        }

        public void insertBill(int tableID, string username)
        {
            username = Encryption.Instance.Encrypt(username);
            clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertBill @tableID , @username", new object[] { tableID, username });
        }

        public void deleteBill(int BillID)
        {
            BillInfoDAO.Instance.deleteBillInfo(BillID);
            clsDatabaseDAO.Instance.excuteNonQuery("delete bill where id = " + BillID);
        }

        public int getMaxIDBill()
        {
            try
            {
                return (int)clsDatabaseDAO.Instance.excuteScaler("select max(BillID) from Bill");
            }
            catch
            {
                return 1;
            }
        }

        public void UpdateBill(int id, int discount, double totalMoney)
        {
            clsDatabaseDAO.Instance.excuteNonQuery($"SP_UpdateBill @discount , @totalMoney , @billID", new object[] { discount, totalMoney, id });
        }

        public void UpdateAddInformationBill(int billID, string username, int CustomerID = 0)
        {
            username = Encryption.Instance.Encrypt(username);
            if (CustomerID == 0)
            {
                clsDatabaseDAO.Instance.excuteNonQuery($"SP_AddBillinformation @username , @billID", new object[] { username, billID });
            }
            else
            {
                clsDatabaseDAO.Instance.excuteNonQuery($"SP_AddBillinformation @username , @billID , @customerID", new object[] { username, billID, CustomerID });
            }
        }
    }
}

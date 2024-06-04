using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return instance; }
        }

        private BillInfoDAO() { }

        public List<BillInfoDTO> GetListBillInfo(int BillID)
        {
            List<BillInfoDTO> listBillInfo = new List<BillInfoDTO>();

            DataTable data = clsDatabaseDAO.Instance.excuteQuery("SELECT * FROM dbo.BillInfo WHERE billID = " + BillID);

            foreach (DataRow item in data.Rows)
            {
                BillInfoDTO info = new BillInfoDTO(item);
                listBillInfo.Add(info);
            }

            return listBillInfo;
        }

        public void insertBillInfo(int billID, int foodID, int number, double totalMoney, string note, int discount = 0)
        {
            if (discount == 0)
            {
                clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertBillInfo @billID , @foodID , @number , @totalMoney , @note", new object[] { billID, foodID, number, totalMoney, note });
            }
            else
            {
                clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertBillInfo @billID , @foodID , @number , @totalMoney , @note , @discount", new object[] { billID, foodID, number, totalMoney, note, discount });
            }
        }

        public DataTable getItemNumber()
        {
            string query = "select foodName, sum(number) as soLuongSP from BillInfo as bi inner join Food as f on f.id = bi.FoodID inner join bill as b on b.id = bi.billID where status = 1 group by foodName";
            return clsDatabaseDAO.Instance.excuteQuery(query);
        }

        public void deleteBillInfo(int BillID)
        {
            string query = "delete BillInfo where billID = " + BillID;
            clsDatabaseDAO.Instance.excuteNonQuery(query);
        }

        public List<BillDetailDTO> getBillDetailList(int BillID)
        {
            List<BillDetailDTO> listBillInfo = new List<BillDetailDTO>();

            DataTable data = clsDatabaseDAO.Instance.excuteQuery("SP_GetBillDetailList @BillID", new object[] { BillID });

            foreach (DataRow item in data.Rows)
            {
                BillDetailDTO info = new BillDetailDTO(item);
                listBillInfo.Add(info);
            }

            return listBillInfo;
        }
    }
}

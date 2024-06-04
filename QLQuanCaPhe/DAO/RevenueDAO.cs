using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DAO
{
    public class RevenueDAO
    {
        private static RevenueDAO instances;

        public static RevenueDAO Instances { get { if (instances == null) instances = new RevenueDAO(); return instances; } }

        private RevenueDAO() { }

        public List<RevenueDTO> getOverview(DateTime start, DateTime end)
        {
            List<RevenueDTO> revenueDTOs = new List<RevenueDTO>();
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_QuantityForProduct @start , @end", new object[] { start, end });

            foreach (DataRow dr in dt.Rows)
            {
                RevenueDTO revenue = new RevenueDTO(dr);
                revenueDTOs.Add(revenue);
            }
            return revenueDTOs;
        }

        public DataTable getCalculateRenevue()
        {
            return clsDatabaseDAO.Instance.excuteQuery("SP_CalculateRevenue");
        }

        public DataTable getItemTop5(DateTime start, DateTime end)
        {
            return clsDatabaseDAO.Instance.excuteQuery("SP_GetTop5Items @start , @end", new object[] { start, end });
        }

        public DataTable getStaffTop1()
        {
            return clsDatabaseDAO.Instance.excuteQuery("SP_Top1Staff");
        }

        public DataTable getRevenueList(DateTime start, DateTime end)
        {
            return clsDatabaseDAO.Instance.excuteQuery("SP_RevenueList @start , @end", new object[] { start, end });
        }

        public DataTable getRevenueToCustomer(DateTime start, DateTime end)
        {
            return clsDatabaseDAO.Instance.excuteQuery("SP_RevenueToCustomer @start , @end", new object[] { start, end });
        }

        public DataTable GetRevenueFromStaff(DateTime start, DateTime end)
        {
            return clsDatabaseDAO.Instance.excuteQuery("SP_RevenueFromStaff @start , @end", new object[] { start, end });
        }

        public DataTable GetWarehouseList(DateTime start, DateTime end)
        {
            return clsDatabaseDAO.Instance.excuteQuery("SP_GetWarehouseList_Statistical @start , @end", new object[] { start, end });
        }

        public DataTable getExpense(DateTime start, DateTime end)
        {
            return clsDatabaseDAO.Instance.excuteQuery("SP_Expense @start , @end", new object[] { start, end });
        }
    }
}

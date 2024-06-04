using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QLQuanCaPhe.BUS
{
    public class BUS_Revenue
    {
        private static BUS_Revenue instance;

        public static BUS_Revenue Instance 
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Revenue();
                return instance;
            }
        }

        private BUS_Revenue() { }

        public List<RevenueDTO> getOverview(DateTime start, DateTime end)
        {
            return RevenueDAO.Instances.getOverview(start, end);
        }

        public DataTable getCalculateRenevue()
        {
            return RevenueDAO.Instances.getCalculateRenevue();
        }

        public DataTable getItemTop5(DateTime start, DateTime end)
        {
            return RevenueDAO.Instances.getItemTop5(start, end);
        }

        public DataTable getStaffTop1()
        {
            return RevenueDAO.Instances.getStaffTop1();
        }

        public DataTable getRevenueList(DateTime start, DateTime end)
        {
            return RevenueDAO.Instances.getRevenueList(start, end);
        }

        public DataTable getRevenueToCustomer(DateTime start, DateTime end)
        {
            return RevenueDAO.Instances.getRevenueToCustomer(start, end);
        }

        public DataTable GetRevenueFromStaff(DateTime start, DateTime end)
        {
            return RevenueDAO.Instances.GetRevenueFromStaff(start, end);
        }

        public DataTable GetWarehouseList(DateTime start, DateTime end)
        {
            return RevenueDAO.Instances.GetWarehouseList(start, end);
        }

        public DataTable GetExpense(DateTime start, DateTime end)
        {
            return RevenueDAO.Instances.getExpense(start, end);
        }
    }
}

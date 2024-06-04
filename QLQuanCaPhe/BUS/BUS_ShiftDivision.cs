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
    public class BUS_ShiftDivision
    {
        private static BUS_ShiftDivision instance;

        public static BUS_ShiftDivision Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_ShiftDivision();
                return instance;
            }
        }

        private BUS_ShiftDivision() { }

        public List<ShiftDivisionDTO> GetShiftDivisionList()
        {
            return ShiftDivisionDAO.Instance.GetShiftDivisionList();
        }

        public ShiftDivisionDTO GetShiftDivision(int shiftID, string username, DateTime workday)
        {
            return ShiftDivisionDAO.Instance.GetShiftDivision(shiftID, username, workday);
        }

        public bool InsertShiftDivision(int shiftID, string username, DateTime workday)
        {
            if (GetShiftDivision(shiftID, username, workday) == null)
            {
                return ShiftDivisionDAO.Instance.InsertShiftDivision(shiftID, username, workday);
            }

            return false;
        }

        public bool UpdateShiftDivision(int shiftID, string username, DateTime workday)
        {
            ShiftDivisionDTO shiftDivision = GetShiftDivision(shiftID, username, workday);
            if (shiftDivision == null || (shiftDivision.ShiftID == shiftID && shiftDivision.Username == username && shiftDivision.Workday.Date == workday.Date))
            {
                return ShiftDivisionDAO.Instance.UpdateShiftDivision(shiftID, username, workday);
            }

            return false;
        }

        public bool DeleteShiftDivision(int shiftID, string username, DateTime workday)
        {
            return ShiftDivisionDAO.Instance.DeleteShiftDivision(shiftID, username, workday);
        }

        public bool InsertShiftDivisionToExcel(DataTable dt)
        {
            return ShiftDivisionDAO.Instance.InsertShiftDivisionToExcel(dt);
        }

        public List<ShiftDivisionDTO> FindShiftDivision(string date = "",  string displayName = "")
        {
            return ShiftDivisionDAO.Instance.findShiftDivision(date, displayName);
        }
    }
}

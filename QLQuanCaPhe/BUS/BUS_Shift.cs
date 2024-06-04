using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.BUS
{
    public class BUS_Shift
    {
        private static BUS_Shift instance;

        public static BUS_Shift Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Shift();
                return instance;
            }
        }

        private BUS_Shift() { }

        public List<ShiftDTO> GetShiftList()
        {
            return ShiftDAO.Instance.GetShiftList();
        }

        public ShiftDTO GetShiftByName(string shiftName)
        {
            return ShiftDAO.Instance.GetShiftByName(shiftName);
        }

        public bool InsertShift(string shiftName, DateTime startTime, DateTime endTime)
        {
            if (GetShiftByName(shiftName) == null)
            {
                return ShiftDAO.Instance.InsertShift(shiftName, startTime, endTime);
            }

            return false;
        }

        public bool UpdateShift(int shiftID, string shiftName, DateTime startTime, DateTime endTime)
        {
            ShiftDTO shift = GetShiftByName(shiftName);
            if (shift == null || shift.ShiftID == shiftID)
            {
                return ShiftDAO.Instance.UpdateShift(shiftID, shiftName, startTime, endTime);
            }

            return false;
        }

        public bool DeleteShift(int shiftID)
        {
            return ShiftDAO.Instance.DeleteShift(shiftID);
        }

        public List<ShiftDTO> FindShift(string shiftName)
        {
            return ShiftDAO.Instance.findShift(shiftName);
        }
    }
}

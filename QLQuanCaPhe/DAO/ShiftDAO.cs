using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DAO
{
    public class ShiftDAO
    {
        private static ShiftDAO instance;

        public static ShiftDAO Instance 
        {
            get
            {
                if (instance == null)
                    instance = new ShiftDAO();
                return instance;
            }
        }

        private ShiftDAO() { }

        public List<ShiftDTO> GetShiftList()
        {
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("select * from shiftt where IsDeleteShift = 1");

            List<ShiftDTO> list = new List<ShiftDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                ShiftDTO shift = new ShiftDTO(dr);
                list.Add(shift);
            }

            return list;
        }

        public ShiftDTO GetShiftByName(string shiftName)
        {
            ShiftDTO shift = null;
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_GetShiftByName @shiftName", new object[] { shiftName });

            if (dt.Rows.Count > 0)
            {
                shift = new ShiftDTO(dt.Rows[0]);
            }

            return shift;
        }

        public bool InsertShift(string shiftName, DateTime startTime, DateTime endTime)
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertShift @ShiftName , @startTime , @endTime", new object[] { shiftName, startTime.TimeOfDay, endTime.TimeOfDay });

            return result > 0;
        }

        public bool UpdateShift(int shiftID, string shiftName, DateTime startTime, DateTime endTime)
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_UpdateShift @shiftID , @ShiftName , @startTime , @endTime", new object[] { shiftID, shiftName, startTime.TimeOfDay, endTime.TimeOfDay });

            return result > 0;
        }

        public bool DeleteShift(int shiftID)
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_DeleteShift @shiftID", new object[] { shiftID });

            return result > 0;
        }

        public List<ShiftDTO> findShift(string shiftName)
        {
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_FindShift @ShiftName", new object[] { shiftName });

            List<ShiftDTO> list = new List<ShiftDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                ShiftDTO shift = new ShiftDTO(dr);
                list.Add(shift);
            }

            return list;
        }
    }
}

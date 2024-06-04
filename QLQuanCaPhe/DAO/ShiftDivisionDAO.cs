using QLQuanCaPhe.BUS;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DAO
{
    public class ShiftDivisionDAO
    {
        private static ShiftDivisionDAO instance;

        public static ShiftDivisionDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new ShiftDivisionDAO();
                return instance;
            }
        }

        private ShiftDivisionDAO() { }

        public List<ShiftDivisionDTO> GetShiftDivisionList()
        {
            DataTable table = clsDatabaseDAO.Instance.excuteQuery("SP_GetShiftDivision");
            List<ShiftDivisionDTO> list = new List<ShiftDivisionDTO>();

            foreach (DataRow row in table.Rows)
            {
                ShiftDivisionDTO shiftDivision = new ShiftDivisionDTO(row);
                list.Add(shiftDivision);
            }

            list.Sort((x, y) => DateTime.Compare(x.Workday, y.Workday));

            return list;
        }

        public ShiftDivisionDTO GetShiftDivision(int shiftID, string username, DateTime workday)
        {
            ShiftDivisionDTO shiftDivision = null;
            username = Encryption.Instance.Encrypt(username);

            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_GetShiftDivisionOne @shiftID , @username , @date", new object[] { shiftID, username, workday });

            if (dt.Rows.Count > 0)
            {
                shiftDivision = new ShiftDivisionDTO(dt.Rows[0]);
            }

            return shiftDivision;
        }

        public bool InsertShiftDivision(int shiftID, string username, DateTime workday)
        {
            username = Encryption.Instance.Encrypt(username);
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertShiftDivision @shiftID , @username , @date", new object[] { shiftID,  username, workday});

            return result > 0;
        }

        public bool UpdateShiftDivision(int shiftID, string username, DateTime workday)
        {
            username = Encryption.Instance.Encrypt(username);
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_UpdateShiftDivision @shiftID , @username , @date", new object[] { shiftID, username, workday });

            return result > 0;
        }

        public bool DeleteShiftDivision(int shiftID, string username, DateTime workday)
        {
            username = Encryption.Instance.Encrypt(username);
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_DeleteShiftDivision @shiftID , @username , @date", new object[] { shiftID, username, workday });

            return result > 0;
        }

        public bool InsertShiftDivisionToExcel(DataTable dt)
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery2("SP_InsertShiftDivisonToExcel", "@temp", "TempShiftDivision", new object[] { dt });
            return result > 0;
        }

        public List<ShiftDivisionDTO> findShiftDivision(string date = "", string displayName = "")
        {
            DataTable table = clsDatabaseDAO.Instance.excuteQuery("SP_FindShiftDivison @date , @displayName", new object[] { date, displayName });
            List<ShiftDivisionDTO> list = new List<ShiftDivisionDTO>();

            foreach (DataRow row in table.Rows)
            {
                ShiftDivisionDTO shiftDivision = new ShiftDivisionDTO(row);
                list.Add(shiftDivision);
            }

            list.Sort((x, y) => DateTime.Compare(x.Workday, y.Workday));

            return list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class ShiftDivisionDTO
    {
        int shiftID;
        string username;
        DateTime workday;
        string shiftName;
        string displayName;

        public int ShiftID { get => shiftID; set => shiftID = value; }
        public string Username { get => username; set => username = value; }
        public DateTime Workday { get => workday; set => workday = value; }
        public string ShiftName { get => shiftName; set => shiftName = value; }
        public string DisplayName { get => displayName; set => displayName = value; }

        public ShiftDivisionDTO() { }

        public ShiftDivisionDTO(int shiftID, string username, DateTime workday, string shiftName, string displayName)
        {
            ShiftID = shiftID;
            Username = username;
            Workday = workday;
            ShiftName = shiftName;
            DisplayName = displayName;
        }

        public ShiftDivisionDTO(DataRow row)
        {
            ShiftID = (int)row["shiftID"];
            Username = Encryption.Instance.Decrypt(row["username"].ToString());
            Workday = (DateTime)row["workday"];
            ShiftName = row["shiftName"].ToString();
            DisplayName = row["displayName"].ToString();
        }
    }
}

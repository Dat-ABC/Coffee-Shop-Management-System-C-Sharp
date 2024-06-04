using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class ShiftDTO
    {
        int shiftID;
        string shiftName;
        TimeSpan startTime;
        TimeSpan endTime;

        public int ShiftID { get => shiftID; set => shiftID = value; }
        public string ShiftName { get => shiftName; set => shiftName = value; }
        public TimeSpan StartTime { get => startTime; set => startTime = value; }
        public TimeSpan EndTime { get => endTime; set => endTime = value; }

        public ShiftDTO() { }

        public ShiftDTO(int shiftID, string shiftName, TimeSpan startTime, TimeSpan endTime)
        {
            ShiftID = shiftID;
            ShiftName = shiftName;
            StartTime = startTime;
            EndTime = endTime;
        }

        public ShiftDTO(DataRow row)
        {
            ShiftID = (int)row["shiftID"];
            ShiftName = row["shiftName"].ToString();
            StartTime = (TimeSpan)row["startTime"];
            EndTime = (TimeSpan)row["endTime"];
        }
    }
}

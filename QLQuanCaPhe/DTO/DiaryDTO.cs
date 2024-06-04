using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanCaPhe.DTO
{
    public class DiaryDTO
    {
        int diaryID;
        DateTime executionTime;
        string activity;
        string detail;
        string implementer;

        public int DiaryID { get => diaryID; set => diaryID = value; }
        public DateTime ExecutionTime { get => executionTime; set => executionTime = value; }
        public string Activity { get => activity; set => activity = value; }
        public string Detail { get => detail; set => detail = value; }
        public string Implementer { get => implementer; set => implementer = value; }

        public DiaryDTO() { }

        public DiaryDTO(int diaryID, DateTime executionTime, string activity, string detail, string implementer)
        {
            DiaryID = diaryID;
            ExecutionTime = executionTime;
            Activity = activity;
            Detail = detail;
            Implementer = implementer;
        }

        public DiaryDTO(DataRow row)
        {
            DiaryID = (int)row["diaryID"];
            ExecutionTime = (DateTime)row["executionTime"];
            Activity = row["activity"].ToString();
            Detail = row["detail"].ToString();
            Implementer = row["implementer"].ToString();
        }
    }
}

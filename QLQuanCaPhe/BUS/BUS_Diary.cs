using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.BUS
{
    public class BUS_Diary
    {
        private static BUS_Diary instance;

        public static BUS_Diary Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Diary();
                return instance;
            }
        }

        private BUS_Diary() { }

        public List<DiaryDTO> GetDiaryList(DateTime start, DateTime end, string activity = "", string implementer = "")
        {
            return DiaryDAO.Instance.GetDiaryList(start, end, activity, implementer);
        }

        public void InsertDiary(DateTime exectionTime, string activity, string detail, string implementer)
        {
            DiaryDAO.Instance.InsertDiary(exectionTime, activity, detail, implementer);
        }
    }
}

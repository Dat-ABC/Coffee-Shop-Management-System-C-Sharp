using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DAO
{
    public class DiaryDAO
    {
        private static DiaryDAO instance;

        public static DiaryDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new DiaryDAO();
                return instance;
            }
        }

        private DiaryDAO() { }

        public List<DiaryDTO> GetDiaryList(DateTime start, DateTime end, string activity = "", string implementer = "")
        {
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_GetDiaryList @start , @end , @activity , @implementer", new object[] { start, end, activity, implementer });

            List<DiaryDTO> list = new List<DiaryDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new DiaryDTO(dr));
            }

            return list;
        }

        public void InsertDiary(DateTime exectionTime, string activity, string detail, string implementer)
        {
            clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertDiary @executionTime , @activtiy , @detail , @implementer", new object[] { exectionTime, activity, detail, implementer });
        }
    }
}

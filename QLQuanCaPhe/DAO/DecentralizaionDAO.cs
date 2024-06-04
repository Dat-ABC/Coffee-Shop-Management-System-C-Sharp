using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DAO
{
    public class DecentralizaionDAO
    {
        private static DecentralizaionDAO instance;

        public static DecentralizaionDAO Instance 
        {
            get
            {
                if (instance == null)
                    instance = new DecentralizaionDAO();
                return instance;
            }
        }

        private DecentralizaionDAO() { }

        public List<DecentralizaionDTO> GetDecentralization(int typeID)
        {
            List<DecentralizaionDTO> list = new List<DecentralizaionDTO>();

            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_GetDecentralizationList @typeID", new object[] { typeID });

            foreach (DataRow dr in dt.Rows)
            {
                DecentralizaionDTO decentralizaion = new DecentralizaionDTO(dr);
                list.Add(decentralizaion);
            }

            return list;
        }

        public bool UpdateDecentralization(DataTable dt)
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery2("SP_UpdateDecentralization", "@Updates", "DecentralizationUpdateType", new object[] { dt });

            return result > 0;
        }
    }
}

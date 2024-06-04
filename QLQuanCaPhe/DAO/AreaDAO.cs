using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DAO
{
    public class AreaDAO
    {
        private static AreaDAO instance;

        public static AreaDAO Instance 
        {
            get
            {
                if (instance == null)
                    instance = new AreaDAO();
                return instance;
            }
        }

        private AreaDAO() { }

        public List<AreaDTO> GetAreaList()
        {
            List<AreaDTO> areaList = new List<AreaDTO>();

            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("select * from area");

            foreach (DataRow dr in dt.Rows)
            {
                AreaDTO areaDTO = new AreaDTO(dr);
                areaList.Add(areaDTO);
            }

            areaList.Sort((x, y) => string.Compare(x.AreaName, y.AreaName));

            return areaList;
        }

        public static int height = 60;

        public AreaDTO GetAreaByName(string name)
        {
            AreaDTO AreaName = null;
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery($"select * from area where areaName = N'{name}'");
            if (dt.Rows.Count > 0)
            {
                AreaName = new AreaDTO(dt.Rows[0]);
            }
            return AreaName;
        }

        public bool InsertArea(string name)
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery($"insert into area values (N'{name}')");
            return result > 0;
        }

        public bool UpdateArea(int areaID, string areaName)
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery($"update area set areaName = N'{areaName}' where areaID = {areaID}");
            return result > 0;
        }

        public bool DeleteArea(int areaID)
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery($"delete area where areaID = " + areaID);
            return result > 0;
        }
    }
}

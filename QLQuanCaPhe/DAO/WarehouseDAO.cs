using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DAO
{
    public class WarehouseDAO
    {
        private static WarehouseDAO instance;

        public static WarehouseDAO Instance
        {
            get
            {
                if (instance == null) 
                    instance = new WarehouseDAO(); 
                return instance;
            }
        }

        private WarehouseDAO() { }

        public List<WarehouseDTO> GetWarehouseList()
        {
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_GetWarehouseList");

            List<WarehouseDTO> list = new List<WarehouseDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                WarehouseDTO w = new WarehouseDTO(dr);
                list.Add(w);
            }

            return list;
        }
    }
}

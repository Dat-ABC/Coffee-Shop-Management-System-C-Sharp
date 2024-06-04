using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.BUS
{
    public class BUS_Warehouse
    {
        private static BUS_Warehouse instance;

        public static BUS_Warehouse Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Warehouse();
                return instance;
            }
        }

        private BUS_Warehouse() { }

        public List<WarehouseDTO> GetWarehouseList()
        {
            return WarehouseDAO.Instance.GetWarehouseList();
        }
    }
}

using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.BUS
{
    public class BUS_Area
    {
        private static BUS_Area instance;

        public static BUS_Area Instance 
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Area();
                return instance;
            }
        }

        private BUS_Area() { }

        public List<AreaDTO> GetAreaList()
        {
            return AreaDAO.Instance.GetAreaList();
        }

        public bool InsertArea(string name)
        {
            if (AreaDAO.Instance.GetAreaByName(name) == null)
            {
                return AreaDAO.Instance.InsertArea(name);
            }
            return false;
        }

        public static int Height = AreaDAO.height;

        public bool UpdateArea(int AreaID, string name)
        {
            AreaDTO area = AreaDAO.Instance.GetAreaByName(name);
            if (area == null || area.AreaID == AreaID)
            {
                return AreaDAO.Instance.UpdateArea(AreaID, name);
            }
            return false;
        }

        public bool DeleteArea(int AreaID)
        {
            return AreaDAO.Instance.DeleteArea(AreaID);
        }
    }
}

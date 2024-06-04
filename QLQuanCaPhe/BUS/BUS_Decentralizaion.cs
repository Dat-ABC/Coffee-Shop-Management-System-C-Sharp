using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.BUS
{
    public class BUS_Decentralizaion
    {
        private static BUS_Decentralizaion instance;

        public static BUS_Decentralizaion Instance 
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Decentralizaion();
                return instance;
            }
        }

        private BUS_Decentralizaion() { }

        public List<DecentralizaionDTO> GetDecentralization(int typeID)
        {
            return DecentralizaionDAO.Instance.GetDecentralization(typeID);
        }

        public bool UpdateDecentralization(DataTable dt)
        {
            return DecentralizaionDAO.Instance.UpdateDecentralization(dt);
        }
    }

}

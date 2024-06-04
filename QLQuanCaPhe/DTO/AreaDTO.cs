using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class AreaDTO
    {
        private int areaID;
        private string areaName;

        public int AreaID { get => areaID; set => areaID = value; }
        public string AreaName { get => areaName; set => areaName = value; }

        public AreaDTO(int areaID, string areaName) 
        {
            AreaID = areaID;
            AreaName = areaName;
        }

        public AreaDTO(DataRow row) 
        {
            AreaID = (int)row["AreaID"];
            AreaName = row["AreaName"].ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class DecentralizaionDTO
    {
        private int typeID;
        private int functionID;
        private int status;

        public int TypeID { get => typeID; set => typeID = value; }
        public int FunctionID { get => functionID; set => functionID = value; }
        public int Status { get => status; set => status = value; }

        public DecentralizaionDTO() { }

        public DecentralizaionDTO(int typeID, int functionID, int status)
        {
            TypeID = typeID;
            FunctionID = functionID;
            Status = status;
        }

        public DecentralizaionDTO(DataRow row)
        {
            TypeID = (int)row["typeID"];
            FunctionID = (int)row["functionID"];
            Status = (int)row["status"];
        }
    }
}

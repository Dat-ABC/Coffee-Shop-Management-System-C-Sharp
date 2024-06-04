using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class AccountTypeDTO
    {
        private int typeID;
        private string typeName;

        public int TypeID { get => typeID; set => typeID = value; }
        public string TypeName { get => typeName; set => typeName = value; }

        public AccountTypeDTO(int typeID, string typeName)
        {
            TypeID = typeID;
            TypeName = typeName;
        }

        public AccountTypeDTO(DataRow row)
        {
            TypeID = (int)row["typeID"];
            TypeName = row["typeName"].ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class TableDTO
    {
        int tableID;
        string tableName;
        string status;
        int areaID;

        public int TableID { get => tableID; set => tableID = value; }
        public string TableName { get => tableName; set => tableName = value; }
        public string Status { get => status; set => status = value; }
        public int AreaID { get => areaID; set => areaID = value; }

        public TableDTO(int id, string name, int areaID, string status) 
        {
            this.TableID = id;
            this.TableName = name;
            this.Status = status;
            this.AreaID = areaID;
        }

        public TableDTO(DataRow row) 
        {
            this.TableID = (int)row["TableID"];
            this.TableName = row["TableName"].ToString();
            this.Status = row["status"].ToString();
            this.AreaID = (int)row["AreaID"];
        }
    }
}

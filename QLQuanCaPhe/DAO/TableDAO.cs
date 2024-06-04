using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace QLQuanCaPhe.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance 
        {
            get 
            { 
                if (instance == null) instance = new TableDAO(); 
                return instance; 
            } 
        }

        public static int width = 135;
        public static int height = 135;

        private TableDAO() { }

        public void switchTable(int id1, int id2, string username, int customerID = 0)
        {
            username = Encryption.Instance.Encrypt(username);
            if (customerID == 0)
            {
                clsDatabaseDAO.Instance.excuteQuery("sp_SwitchTable @idTable1 , @idTable2 , @username", new object[] { id1, id2, username});
            }
            else
            {
                clsDatabaseDAO.Instance.excuteQuery("sp_SwitchTable @idTable1 , @idTable2 , @username , @customer", new object[] { id1, id2, username, customerID});
            }
        }

        public List<TableDTO> loadTableList(int AreaID)
        {
            List<TableDTO> listTable = new List<TableDTO>();

            DataTable dataTable = clsDatabaseDAO.Instance.excuteQuery("SP_GetTableList @areaID", new object[] { AreaID });

            foreach (DataRow item in dataTable.Rows)
            {
                TableDTO table = new TableDTO(item);
                listTable.Add(table);
            }

            listTable.Sort((x, y) => string.Compare(x.TableName, y.TableName));

            return listTable;
        }

        public TableDTO GetTableByName(string Name)
        {
            TableDTO table = null;
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_GetTableByName @tableName", new object[] { Name });

            if (dt.Rows.Count > 0)
            {
                table = new TableDTO(dt.Rows[0]);
            }

            return table;
        }

        public bool insertTable(string name, int AreaID)
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertTable @TableName , @areaID", new object[] { name, AreaID });
            return result > 0;
        }

        public bool updateTable(int id, string name)
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_UpdateTable @tableID , @tableName", new object[] { id, name });
            return result > 0;
        }

        public bool deleteTable(int id)
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_DeleteTable @tableID", new object[] { id });
            return result > 0;
        }
    }
}

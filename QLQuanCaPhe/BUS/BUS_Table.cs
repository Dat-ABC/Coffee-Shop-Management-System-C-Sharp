using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QLQuanCaPhe.BUS
{
    public class BUS_Table
    {
        private static BUS_Table instance;

        public static BUS_Table Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Table();
                return instance;
            }
        }

        private BUS_Table() { }

        public List<TableDTO> GetTableList(int AreaID)
        {
            return TableDAO.Instance.loadTableList(AreaID);
        }

        public bool InsertTable(string name, int AreaID)
        {
            if (TableDAO.Instance.GetTableByName(name) == null)
            {
                return TableDAO.Instance.insertTable(name, AreaID);
            }

            return false;
        }

        public static int Height = TableDAO.height;
        public static int Width = TableDAO.width;

        public bool UpdateTable(int TableID, string name)
        {
            TableDTO table = TableDAO.Instance.GetTableByName(name);
            if (table == null || table.TableID == TableID)
            {
                return TableDAO.Instance.updateTable(TableID, name);
            }

            return false;
        }

        public bool DeleteTable(int TableID)
        {
            return TableDAO.Instance.deleteTable(TableID);
        }

        public void SwitchTable(int tableID1, int tableID2, string username, string tableName1, string tableName2, int customerID = 0)
        {
            TableDAO.Instance.switchTable(tableID1, tableID2, username, customerID);
            string detail = $"Chuyển từ bàn {tableName1} sang bàn {tableName2}";
            BUS_Diary.Instance.InsertDiary(DateTime.Now, "Chuyển bàn", detail, BUS_Account.DisplayName);
        }
    }
}

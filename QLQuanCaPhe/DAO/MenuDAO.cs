using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QLQuanCaPhe.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance { get { if (instance == null) instance = new MenuDAO(); return instance; } }

        private MenuDAO() { }

        public List<MenuDTO> getMenuByTable(int id)
        {
            DataTable table = clsDatabaseDAO.Instance.excuteQuery("SP_GetMenuList @tableID", new object[] {id});
            List<MenuDTO> list = new List<MenuDTO>();
            
            foreach (DataRow row in table.Rows)
            {
                MenuDTO menu = new MenuDTO(row);
                list.Add(menu);
            }
            return list;
        }

        public List<MenuDTO> GetMenuToPrint(int BillID)
        {
            List<MenuDTO> list = new List<MenuDTO>();
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_GetMenuToPrint @BillID", new object[] { BillID });

            foreach (DataRow row in dt.Rows)
            {
                MenuDTO billInformation = new MenuDTO(row);
                list.Add(billInformation);
            }
            return list;
        }
    }
}

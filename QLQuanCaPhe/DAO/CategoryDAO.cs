using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DAO
{
    public class CategoryDAO
    {
        private static CategoryDAO instance;

        public static CategoryDAO Instance 
        {
            get 
            { 
                if (instance == null) instance = new CategoryDAO(); 
                return instance; 
            }
        }

        private CategoryDAO() { }

        public List<CategoryDTO> getCategoryList()
        {
            List<CategoryDTO> list = new List<CategoryDTO>();
            string query = "SELECT CategoryID, CategoryName FROM FoodCategory WHERE IsDeleteCategory = 1";

            DataTable data = clsDatabaseDAO.Instance.excuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                CategoryDTO category = new CategoryDTO(row);
                list.Add(category);
            }

            list.Sort((x, y) => string.Compare(x.CategoryName, y.CategoryName));

            return list;
        }

        public CategoryDTO getCategoryByName(string categoryName)
        {
            CategoryDTO category = null;
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery($"SELECT CategoryID, CategoryName FROM FoodCategory where CategoryName = N'{categoryName}' AND IsDeleteCategory = 1");

            if (dt.Rows.Count > 0)
            {
                category = new CategoryDTO(dt.Rows[0]);
            }
            return category;
        }

        public bool InsertCategory(string name)
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertCategory @CategoryName", new object[] { name });
            return result > 0;
        }

        public bool UpdateCategory(int id, string name)
        {
            string query = string.Format("UPDATE FoodCategory SET CategoryName = N'{0}' WHERE CategoryID = {1}", name, id);
            int result = clsDatabaseDAO.Instance.excuteNonQuery(query);
            return result > 0;
        }

        public bool DeleteCategory(int id)
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_DeleteCategory @CategoryID", new object[] { id });
            return result > 0;
        }
    }
}

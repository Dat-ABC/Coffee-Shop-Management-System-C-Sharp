using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.BUS
{
    public class BUS_Category
    {
        private static BUS_Category instance;

        public static BUS_Category Instance 
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Category();
                return instance;
            }
        }
        private BUS_Category() { }

        public List<CategoryDTO> GetCategoryList()
        {
            return CategoryDAO.Instance.getCategoryList();
        }

        public bool InsertCategory(string categoryName)
        {
            if (CategoryDAO.Instance.getCategoryByName(categoryName) == null)
            {
                return CategoryDAO.Instance.InsertCategory(categoryName);
            }
            return false;
        }

        public bool UpdateCategory(int categoryId, string CategoryName)
        {
            CategoryDTO categoryDTO = CategoryDAO.Instance.getCategoryByName(CategoryName);
            if (categoryDTO == null || categoryDTO.CategoryID == categoryId)
            {
                return CategoryDAO.Instance.UpdateCategory(categoryId, CategoryName);
            }
            return false;
        }

        public bool DeleteCategory(int CategoryID)
        {
            return CategoryDAO.Instance.DeleteCategory(CategoryID);
        }
    }
}

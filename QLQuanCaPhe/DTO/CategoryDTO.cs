using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class CategoryDTO
    {
        private int categoryID;
        private string categoryName;

        public string CategoryName { get => categoryName; set => categoryName = value; }
        public int CategoryID { get => categoryID; set => categoryID = value; }

        public CategoryDTO(int id, string category) 
        { 
            this.CategoryID = id;
            this.CategoryName = category;
        }

        public CategoryDTO(DataRow row) 
        {
            this.CategoryID = (int)row["CategoryID"];
            this.CategoryName = row["categoryName"].ToString();
        }
    }
}

using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new FoodDAO();
                return instance;
            }
        }

        public List<FoodDTO> GetFoodList()
        {
            List<FoodDTO> list = new List<FoodDTO>();

            DataTable data = clsDatabaseDAO.Instance.excuteQuery("SP_GetFoodList");

            foreach (DataRow row in data.Rows)
            {
                FoodDTO food = new FoodDTO(row);
                list.Add(food);
            }

            list.Sort((x, y) => string.Compare(x.FoodName, y.FoodName));

            return list;
        }

        public static int height = 320;
        public static int width = 190;

        public List<FoodDTO> getFoodListByCategoryID(int categoryID)
        {
            List<FoodDTO> list = new List<FoodDTO>();
            DataTable data = clsDatabaseDAO.Instance.excuteQuery("SP_GetFoodListByCategoryID @categoryID", new object[] { categoryID });

            foreach (DataRow row in data.Rows)
            {
                FoodDTO food = new FoodDTO(row);
                list.Add(food);
            }

            list.Sort((x, y) => string.Compare(x.FoodName, y.FoodName));

            return list;
        }

        public FoodDTO GetFoodByName(string name)
        {
            FoodDTO foodDTO = null;
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery($"SP_GetFoodByName @FoodName", new object[] { name });
            if (dt.Rows.Count > 0)
            {
                foodDTO = new FoodDTO(dt.Rows[0]);
            }

            return foodDTO;
        }

        public bool InsertFood(string name, float price, int CategoryID, Image image, string unit)
        {
            int result;
            if (image == null)
            {
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertFood @FoodName , @price , @CategoryID , @Unit", new object[] { name, price, CategoryID, unit });
            }
            else
            {
                byte[] b = ImageEncryption.Instance.ImageToByteArray(image);
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertFood @FoodName , @price , @CategoryID , @Unit , @FoodPhoto", new object[] { name, price, CategoryID, unit, b });
            }

            return result > 0;
        }

        public bool UpdateFood(int foodID, string name, float price, int CategoryID, Image image, string unit)
        {
            int result;
            if (image == null)
            {
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_UpdateFood @FoodID , @FoodName , @price , @CategoryID , @Unit", new object[] { foodID, name, price, CategoryID, unit });
            }
            else
            {
                byte[] b = ImageEncryption.Instance.ImageToByteArray(image);
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_UpdateFood @FoodID , @FoodName , @price , @CategoryID , @Unit , @FoodPhoto", new object[] { foodID, name, price, CategoryID, unit, b });
            }

            return result > 0;
        }

        public bool DeleteFood(int FoodID)
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_DeleteFood @foodID", new object[] { FoodID });
            return result > 0;
        }

        public bool InsertFoodToExcel(DataTable dt)
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery2("SP_InsertFoodToExcel", "@tabletemp", "tabletemp", new object[] { dt });
            return result > 0;
        }
    }
}

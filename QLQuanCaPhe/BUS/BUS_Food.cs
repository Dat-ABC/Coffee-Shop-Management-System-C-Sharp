using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.BUS
{
    public class BUS_Food
    {
        private static BUS_Food instance;

        public static BUS_Food Instance 
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Food();
                return instance;
            }
        }

        private BUS_Food() { }

        public List<FoodDTO> GetFoodList()
        {
            return FoodDAO.Instance.GetFoodList();
        }

        public List<FoodDTO> GetFoodListByCategoryID(int categoryID)
        {
            return FoodDAO.Instance.getFoodListByCategoryID(categoryID);
        }

        public static int Height = FoodDAO.height;
        public static int Width = FoodDAO.width;

        public bool InsertFood(string name, float price, int CategoryID, Image image, string unit)
        {
            if (FoodDAO.Instance.GetFoodByName(name) == null)
            {
                return FoodDAO.Instance.InsertFood(name, price, CategoryID,image, unit);
            }

            return false;
        }

        public bool UpdateFood(int foodID, string name, float price, int CategoryID, Image image, string unit)
        {
            FoodDTO food = FoodDAO.Instance.GetFoodByName(name);
            if (food == null || food.FoodID == foodID)
            {
                return FoodDAO.Instance.UpdateFood(foodID, name, price, CategoryID, image, unit);
            }

            return false;
        }

        public bool DeleteFood(int foodID)
        {
            return FoodDAO.Instance.DeleteFood(foodID);
        }

        public bool InsertFoodToExcel(DataTable dt)
        {
            return FoodDAO.Instance.InsertFoodToExcel(dt);
        }
    }
}

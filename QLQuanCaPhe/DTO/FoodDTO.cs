using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class FoodDTO
    {
        private int foodID;
        private string foodName;
        private int categoryID;
        private float price;
        private Image foodPhoto;
        private string unit;
        private string categoryName;

        public int FoodID { get => foodID; set => foodID = value; }
        public string CategoryName { get => categoryName; set => categoryName = value; }
        public string FoodName { get => foodName; set => foodName = value; }
        public Image FoodPhoto { get => foodPhoto; set => foodPhoto = value; }
        public float Price { get => price; set => price = value; }
        public string Unit { get => unit; set => unit = value; }
        public int CategoryID { get => categoryID; set => categoryID = value; }

        public FoodDTO(string foodName, int categoryID, float price, Image image, string unit, string categoryName) 
        {
            this.FoodName = foodName;
            this.CategoryID = categoryID;
            this.Price = price;
            this.FoodPhoto = image;
            this.Unit = unit;
            this.CategoryName = categoryName;
        }

        public FoodDTO(DataRow row) 
        {
            this.FoodID = (int)row["FoodID"];
            this.CategoryName = row["CategoryName"].ToString();
            this.FoodName = row["foodName"].ToString();
            var image = row["FoodPhoto"];
            if (image.ToString() != "")
            {
                this.FoodPhoto = ImageEncryption.Instance.ByteArrayToImage((byte[])image);
            }
            this.Price = (float)Convert.ToDouble(row["price"]);
            this.Unit = row["unit"].ToString();
            this.CategoryID = (int)row["categoryID"];
        }
    }
}

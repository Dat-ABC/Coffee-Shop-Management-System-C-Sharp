using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QLQuanCaPhe.BUS
{
    public class BUS_CheckFormat
    {
        private static BUS_CheckFormat instance;
        public static BUS_CheckFormat Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_CheckFormat();
                return instance;
            }
        }

        private BUS_CheckFormat() { }

        public bool checkEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(email, pattern);
        }

        public bool checkPhone(string phone)
        {
            string pattern = @"^0[0-9]{3}-d{3}-d{3}$";

            return Regex.IsMatch(phone, pattern);
        }

        public string formatName(string name)
        {
            TextInfo textInfo = new CultureInfo("vn-VN").TextInfo;
            return textInfo.ToTitleCase(name.ToLower());
        }
    }
}
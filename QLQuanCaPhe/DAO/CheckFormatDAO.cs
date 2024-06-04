using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DAO
{
    public class CheckFormatDAO
    {
        private static CheckFormatDAO instance;

        public static CheckFormatDAO Instance 
        {
            get
            {
                if (instance == null)
                    instance = new CheckFormatDAO();
                return instance;
            }
        }

        public bool checkEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(email, pattern);
        }

        public bool checkPhone(string phone)
        {
            string pattern = @"^0[1-9]\d{8}$";

            return Regex.IsMatch(phone, pattern);
        }

       /* public string formatName(string name)
        {
            TextInfo textInfo = new CultureInfo("vn-VN").TextInfo;
            return textInfo.ToTitleCase(name.ToLower());
        }*/
    }
}

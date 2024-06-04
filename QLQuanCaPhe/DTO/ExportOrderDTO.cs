using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class ExportOrderDTO
    {
        int exportID;
        string username;
        DateTime? dateOut;
        double totalMoney;
        string displayName;

        public int ExportID { get => exportID; set => exportID = value; }
        public string Username { get => username; set => username = value; }
        public DateTime? DateOut { get => dateOut; set => dateOut = value; }
        public double TotalMoney { get => totalMoney; set => totalMoney = value; }
        public string DisplayName { get => displayName; set => displayName = value; }

        public ExportOrderDTO() { }

        public ExportOrderDTO(int exportID, DateTime dateOut, double totalMoney, string username, string displayName)
        {
            ExportID = exportID;
            DateOut = dateOut;
            TotalMoney = totalMoney;
            Username = username;
            DisplayName = displayName;
        }

        public ExportOrderDTO(DataRow row)
        {
            ExportID = (int)row["ExportID"];
            var dateIn = row["DateOut"];
            if (dateIn.ToString() != "")
                DateOut = (DateTime)dateIn;
            var totalMoney = row["totalMoney"];
            if (totalMoney.ToString() != "")
                TotalMoney = (double)totalMoney;
            Username = Encryption.Instance.Decrypt(row["username"].ToString());
            DisplayName = row["displayName"].ToString();
        }
    }
}

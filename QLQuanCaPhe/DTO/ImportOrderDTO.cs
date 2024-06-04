using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class ImportOrderDTO
    {
        int importID;
        DateTime? dateIn;
        double totalMoney;
        string username;
        string displayName;

        public int ImportID { get => importID; set => importID = value; }
        public string Username { get => username; set => username = value; }
        public DateTime? DateIn { get => dateIn; set => dateIn = value; }
        public double TotalMoney { get => totalMoney; set => totalMoney = value; }
        public string DisplayName { get => displayName; set => displayName = value; }

        public ImportOrderDTO() { }

        public ImportOrderDTO(int importID, DateTime dateIn, double totalMoney, string username, string displayName)
        {
            ImportID = importID;
            DateIn = dateIn;
            TotalMoney = totalMoney;
            Username = username;
            DisplayName = displayName;
        }

        public ImportOrderDTO(DataRow row)
        {
            ImportID = (int)row["importID"];
            var dateIn = row["dateIn"];
            if (dateIn.ToString() != "")
                DateIn = (DateTime)dateIn;
            var totalMoney = row["totalMoney"];
            if (totalMoney.ToString() != "")
                TotalMoney = (double)totalMoney;
            Username = Encryption.Instance.Decrypt(row["username"].ToString());
            DisplayName = row["displayName"].ToString();
        }
    }
}

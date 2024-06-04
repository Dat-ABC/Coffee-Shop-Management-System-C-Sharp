using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class SupplierDTO
    {
        int supplierID;
        string supplierName;
        string address;
        string phone;
        string email;

        public int SupplierID { get => supplierID; set => supplierID = value; }
        public string SupplierName { get => supplierName; set => supplierName = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Email { get => email; set => email = value; }
        public string Address { get => address; set => address = value; }

        public SupplierDTO() { }

        public SupplierDTO(int supplierID, string supplierName, string address, string phone, string email)
        {
            SupplierID = supplierID;
            SupplierName = supplierName;
            Address = address;
            Phone = phone;
            Email = email;
        }

        public SupplierDTO(DataRow row)
        {
            SupplierID = (int)row["supplierID"];
            SupplierName = row["supplierName"].ToString();
            Address = row["addresss"].ToString();
            Phone = row["phone"].ToString();
            Email = row["email"].ToString();
        }
    }
}

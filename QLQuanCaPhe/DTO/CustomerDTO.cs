using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class CustomerDTO
    {
        private int customerID;
        private string customerName;
        private string sex;
        private DateTime? birthday;
        private string phone;
        private string address;
        private string typeName;

        public int CustomerID { get => customerID; set => customerID = value; }
        public string CustomerName { get => customerName; set => customerName = value; }
        public string Sex { get => sex; set => sex = value; }
        public DateTime? Birthday { get => birthday; set => birthday = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public string TypeName { get => typeName; set => typeName = value; }

        public CustomerDTO(int customerID, string customerName, string phone, DateTime? birthday, string address, string typeName, string sex) 
        {
            this.CustomerID = customerID;
            this.CustomerName = customerName;
            this.Phone = phone;
            this.Birthday = birthday;
            this.Address = address;
            this.TypeName = typeName;
            this.Sex = sex;
        }

        public CustomerDTO() { }

        public CustomerDTO(DataRow row) 
        {
            this.CustomerID = (int)row["CustomerID"];
            this.CustomerName = row["CustomerName"].ToString();
            this.Phone = row["phone"].ToString();
            var birthday = row["birthday"];
            if (birthday.ToString() != "")
                this.Birthday = (DateTime)birthday;
            this.Address = row["address"].ToString();
            this.TypeName = row["CustomerType"].ToString();
            this.Sex = row["sex"].ToString();
        }
    }
}

using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DAO
{
    public class CustomerDAO
    {
        private static CustomerDAO instance;

        public static CustomerDAO Instance 
        {
            get
            {
                if (instance == null)
                    instance = new CustomerDAO();
                return instance;
            } 
        }

        private CustomerDAO() { }

        public List<CustomerDTO> getCustomerList()
        {
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_GetCustomerList");
            List<CustomerDTO> list = new List<CustomerDTO>();

            foreach (DataRow dr in dt.Rows)
            {
                CustomerDTO customerDTO = new CustomerDTO(dr);
                list.Add(customerDTO);
            }

            list.Sort((x, y) => string.Compare(x.CustomerName, y.CustomerName));

            return list;
        }

        public CustomerDTO GetCustomerByPhone(string phone)
        {
            CustomerDTO customer = null;
            if (phone != "")
            {
                DataTable dt = clsDatabaseDAO.Instance.excuteQuery($"select * from customer where phone = '{phone}'");
                if (dt.Rows.Count > 0)
                {
                    customer = new CustomerDTO(dt.Rows[0]);
                }
            }

            return customer;
        }

        public List<CustomerDTO> findCustomer(string name)
        {
            List<CustomerDTO> list = new List<CustomerDTO>();
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_FindCustomer @customername", new object[] { name });

            foreach (DataRow dr in dt.Rows)
            {
                CustomerDTO customerDTO = new CustomerDTO(dr);
                list.Add(customerDTO);
            }

            list.Sort((x, y) => string.Compare(x.CustomerName, y.CustomerName));

            return list;
        }

        public bool InsertCustomer(string customerName, string sex = "", string customerType = "", string phone = "", string birthday = "", string address = "")
        {
            int result;
            bool chkBirthday = false;
            DateTime date;
            if (DateTime.TryParseExact(birthday, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                chkBirthday = true;
            }
            

            if (chkBirthday)
            {
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertCustomer @customerName , @phone , @address , @sex , @customerType , @birthday", new object[] { customerName, phone, address, sex, customerType, date });
            }

            else
            {
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertCustomer @customerName , @phone , @address , @sex , @customerType", new object[] { customerName, phone, address, sex, customerType });
            }
            
            return result > 0;
        }

        public bool UpdateCustomer(int customerID, string customerName, string sex = "", string customerType = "", string phone = "", string birthday = "", string address = "")
        {
            int result;
            bool chkBirthday = false;
            DateTime date;
            if (DateTime.TryParseExact(birthday, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                chkBirthday = true;
            }


            if (chkBirthday)
            {
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_UpdateCustomer @customerID , @customerName , @phone , @address , @sex , @customerType , @birthday", new object[] { customerID, customerName, phone, address, sex, customerType, date });
            }

            else
            {
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_UpdateCustomer @customerID , @customerName , @phone , @address , @sex , @customerType", new object[] { customerID, customerName, phone, address, sex, customerType });
            }

            return result > 0;
        }

        public bool DeleteCustomer(int customerID)
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_DeleteCustomer @customerID", new object[] {customerID});
            return result > 0;
        }
    }
}

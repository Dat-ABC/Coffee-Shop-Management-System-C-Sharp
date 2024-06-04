using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.BUS
{
    public class BUS_Customer
    {
        private static BUS_Customer instance;

        public static BUS_Customer Instance 
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Customer();
                return instance;
            }
        }

        private BUS_Customer() { }

        public List<CustomerDTO> getCustomerList()
        {
            return CustomerDAO.Instance.getCustomerList();
        }

        public List<CustomerDTO> FindCustomer(string name)
        {
            return CustomerDAO.Instance.findCustomer(name);
        }

        public bool InsertCustomer(string customerName, string sex, string customerType, string phone = "", string birthday = "", string address = "")
        {
            if (CustomerDAO.Instance.GetCustomerByPhone(phone) == null)
            {
                return CustomerDAO.Instance.InsertCustomer(customerName, sex, customerType, phone, birthday, address);
            }
            return false;
        }

        public bool UpdateCustomer(int customerID,string customerName, string sex, string customerType, string phone = "", string birthday = "", string address = "")
        {
            CustomerDTO customer = CustomerDAO.Instance.GetCustomerByPhone(phone);
            if (customer == null || customer.CustomerID == customerID)
            {
                return CustomerDAO.Instance.UpdateCustomer(customerID, customerName, sex, customerType, phone, birthday, address);
            }
            return false;
        }

        public bool DeleteCustomer(int customerID)
        {
            return CustomerDAO.Instance.DeleteCustomer(customerID);
        }
    }
}

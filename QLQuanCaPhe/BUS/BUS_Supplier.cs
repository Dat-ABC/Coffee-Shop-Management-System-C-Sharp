using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.BUS
{
    public class BUS_Supplier
    {
        private static BUS_Supplier instance;

        public static BUS_Supplier Instance 
        {
            get
            { 
                if (instance == null)
                    instance = new BUS_Supplier();
                return instance; 
            }
        }

        private BUS_Supplier() { }

        public List<SupplierDTO> GetSupplierList()
        {
            return SupplierDAO.Instance.GetSupplierList();
        }

        public SupplierDTO GetSupplierByNameOrPhoneOrEmail(string name = "", string phone = "", string email = "")
        {
            return SupplierDAO.Instance.GetSupplierByNameOrPhoneOrEmail(name, phone, email);
        }

        public bool InsertSupplier(string name, string adress, string phone,  string email)
        {
            if (GetSupplierByNameOrPhoneOrEmail(name, phone, email) == null)
            {
                return SupplierDAO.Instance.InsertSupplier(name, adress, phone, email);
            }
            return false;
        }

        public bool UpdateSupplier(int supplierID, string name, string adress, string phone, string email)
        {
            SupplierDTO supplier = GetSupplierByNameOrPhoneOrEmail(name, phone, email);
            if (supplier == null || supplier.SupplierID == supplierID)
            {
                return SupplierDAO.Instance.UpdateSupplier(supplierID, name, adress, phone, email);
            }
            return false;
        }

        public bool DeleteSupplier(int supplierID)
        {
            return SupplierDAO.Instance.DeleteSupplier(supplierID);
        }

        public List<SupplierDTO> findSupplier(string supplierName = "", string address = "", string phone = "")
        {
            return SupplierDAO.Instance.findSupplier(supplierName, address, phone);
        }
    }
}

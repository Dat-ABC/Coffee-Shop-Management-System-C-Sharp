using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DAO
{
    public class SupplierDAO
    {
        private static SupplierDAO instance;

        public static SupplierDAO Instance 
        {
            get
            {
                if (instance == null)
                    instance = new SupplierDAO();
                return instance;
            }
        }

        private SupplierDAO() { }

        public List<SupplierDTO> GetSupplierList()
        {
            DataTable table = clsDatabaseDAO.Instance.excuteQuery("SP_GetSupplierList");
            List<SupplierDTO> list = new List<SupplierDTO>();

            foreach (DataRow row in table.Rows)
            {
                SupplierDTO supplier = new SupplierDTO(row);
                list.Add(supplier);
            }

            list.Sort((x, y) => string.Compare(x.SupplierName, y.SupplierName));

            return list;
        }

        public SupplierDTO GetSupplierByNameOrPhoneOrEmail(string name = "", string phone = "", string email = "")
        {
            SupplierDTO supplier = null;

            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_GetSupplierByName_Phone_Email @suppliername , @phone , @email", new object[] { name, phone, email });

            if (dt.Rows.Count > 0)
            {
                supplier = new SupplierDTO(dt.Rows[0]);
            }

            return supplier;
        }

        public bool InsertSupplier(string supplierName, string address = "", string phone = "", string email = "")
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertSupplier @SupplierName , @address , @phone , @email", new object[] { supplierName, address, phone, email });

            return result > 0;
        }

        public bool UpdateSupplier(int supplierID, string supplierName, string address = "", string phone = "", string email = "")
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_UpdateSupplier @SupplierID , @SupplierName , @address , @phone , @email", new object[] { supplierID, supplierName, address, phone, email });

            return result > 0;
        }

        public bool DeleteSupplier(int supplierID)
        {
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_DeleteSupplier @SupplierID", new object[] { supplierID });

            return result > 0;
        }

        public List<SupplierDTO> findSupplier(string supplierName = "", string address = "", string phone = "")
        {
            DataTable table = clsDatabaseDAO.Instance.excuteQuery("SP_FindSupplier @SupplierName , @address , @phone", new object[] { supplierName, address, phone });
            List<SupplierDTO> list = new List<SupplierDTO>();

            foreach (DataRow row in table.Rows)
            {
                SupplierDTO supplier = new SupplierDTO(row);
                list.Add(supplier);
            }

            list.Sort((x, y) => string.Compare(x.SupplierName, y.SupplierName));

            return list;
        }
    }
}

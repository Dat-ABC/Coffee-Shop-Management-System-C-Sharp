using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanCaPhe.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance 
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; } 
            private set => instance = value; 
        }

        private AccountDAO() { }

        public bool login(string username, string password)
        {
            string query = "SP_Login @username , @password";

            username = Encryption.Instance.Encrypt(username);
            password = Encryption.Instance.Encrypt(password);

            DataTable result = clsDatabaseDAO.Instance.excuteQuery(query, new object[] {username, password});

            return result.Rows.Count > 0;
        }

        public List<AccountDTO> GetAccountList()
        {
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_GetAccountList");
            List<AccountDTO> list = new List<AccountDTO>();

            foreach (DataRow row in dt.Rows)
            {
                AccountDTO accountDTO = new AccountDTO(row);
                list.Add(accountDTO);
            }

            list.Sort((x, y) => string.Compare(x.UserName, y.UserName));

            return list;
        }
        
        public List<AccountTypeDTO> GetAccountTypeList()
        {
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("select * from accounttype");
            List<AccountTypeDTO> list = new List<AccountTypeDTO>();

            foreach(DataRow row in dt.Rows)
            {
                AccountTypeDTO accountTypeDTO = new AccountTypeDTO(row);
                list.Add(accountTypeDTO);
            }

            return list;
        }

        public List<AccountDTO> findAccount(string username = "", string displayName = "")
        {
            string user = Encryption.Instance.Encrypt(username);
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_FindAccount @username , @displayname", new object[] { user, displayName });
            List<AccountDTO> list = new List<AccountDTO>();

            foreach (DataRow row in dt.Rows)
            {
                AccountDTO accountTypeDTO = new AccountDTO(row);
                list.Add(accountTypeDTO);
            }
            return list;
        }

        public AccountTypeDTO GetAccountTypeByID(int id)
        {
            AccountTypeDTO accountTypeDTO = null;

            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("select * from accounttype where typeid =" + id);

            foreach (DataRow row in dt.Rows)
            {
                accountTypeDTO = new AccountTypeDTO(row);
                break;
            }
            
            return accountTypeDTO;
        }

        public AccountDTO getAccountByUserName(string username)
        {
            string user = Encryption.Instance.Encrypt(username);
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_GetAccountByUsername @username", new object[] { user });

            AccountDTO accountDTO = null;
            if (dt.Rows.Count > 0)
                accountDTO = new AccountDTO(dt.Rows[0]);
            return accountDTO;
        }

        public AccountDTO getAccountByUserName2(string username)
        {
            string user = Encryption.Instance.Encrypt(username);
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery("SP_GetAccountByUsername2 @username", new object[] { user });

            AccountDTO accountDTO = null;
            if (dt.Rows.Count > 0)
                accountDTO = new AccountDTO(dt.Rows[0]);
            return accountDTO;
        }

        public AccountDTO getAccountTypeById(int id)
        {
            DataTable dt = clsDatabaseDAO.Instance.excuteQuery($"select typeid, typename as N'Loại tài khoản' from accounttype where typeid = N'{id}'");

            AccountDTO accountDTO = new AccountDTO();
            if (dt.Rows.Count > 0)
                accountDTO = new AccountDTO(dt.Rows[0]);
            return accountDTO;
        }

        public bool insertAccount(string username, string displayName, Image image, string email, string phone, string password, int typeid)
        {
            int result;

            string passw = Encryption.Instance.Encrypt(password);
            string user = Encryption.Instance.Encrypt(username);
            if (image == null)
            {
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertAccount @username , @displayname , @email , @phone , @password , @typeid", new object[] { user, displayName, email, phone, passw, typeid });
            }
            else
            {
                byte[] b = ImageEncryption.Instance.ImageToByteArray(image);
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_InsertAccount @username , @displayname , @email , @phone , @password , @typeid , @ownerphoto", new object[] { user, displayName, email, phone, passw, typeid, b });
            }
            
            return result > 0;
        }

        public bool updateAccount(string username, string displayName, Image image, string email, string phone, string password, int typeid)
        {
            int result;
            string passw = Encryption.Instance.Encrypt(password);
            string user = Encryption.Instance.Encrypt(username);
            if (image == null)
            {
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_UpdateAccount @username , @displayname , @email , @phone , @password , @typeid", new object[] { user, displayName, email, phone, passw, typeid });
            }
            else
            {
                byte[] b = ImageEncryption.Instance.ImageToByteArray(image);
                result = clsDatabaseDAO.Instance.excuteNonQuery("SP_UpdateAccount @username , @displayname , @email , @phone , @password , @typeid , @ownerphoto", new object[] { user, displayName, email, phone, passw, typeid, b });
            }
            return result > 0;
        }
        public bool deleteAccount(string username)
        {
            string user = Encryption.Instance.Encrypt(username);
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_DeleteAccount @username", new object[] { user });
            return result > 0;
        }

        public bool changePassword(string username, string password)
        {
            username = Encryption.Instance.Encrypt(username);
            password = Encryption.Instance.Encrypt(password);
            int result = clsDatabaseDAO.Instance.excuteNonQuery("SP_ChangePassword @username , @password", new object[] {username, password});
            return result > 0;
        }
    }
}

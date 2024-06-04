using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanCaPhe.BUS
{
    public class BUS_Account
    {
        private static BUS_Account instance;

        public static BUS_Account Instance
        {
            get
            {
                if (instance == null)
                    instance = new BUS_Account();
                return instance;
            }

        }

        private BUS_Account() { }

        public List<AccountDTO> GetAccountList()
        {
            return AccountDAO.Instance.GetAccountList();
        }

        public List<AccountTypeDTO> GetAccountTypeList()
        {
            return AccountDAO.Instance.GetAccountTypeList();
        }

        public bool Login(string username, string password)
        {
            Username = username;
            AccountDTO account = GetAccountByUserName(username);
            DisplayName = account.DisplayName;
            return AccountDAO.Instance.login(username, password);
        }

        public static string Username;

        public static string DisplayName;

        public AccountDTO GetAccountByUserName(string username)
        {
            return AccountDAO.Instance.getAccountByUserName(username);
        }

        public AccountDTO GetAccountByUserName2(string username)
        {
            return AccountDAO.Instance.getAccountByUserName2(username);
        }

        public AccountTypeDTO GetAccountTypeByID(int id)
        {
            return AccountDAO.Instance.GetAccountTypeByID(id);
        }

        public bool InsertAccount(string username, string displayName, Image image, string email, string phone, string password, int type)
        {
            AccountDTO account = GetAccountByUserName2(username);
            if (account == null || (account.IsDeleteAccount == 0 && account.UserName == username))
            {
                return AccountDAO.Instance.insertAccount(username, displayName, image, email, phone, password, type);
            }

            return false;
        }

        public bool UpdateAccount(string username, string displayName, Image image, string email, string phone, string password, int type)
        {
            AccountDTO account = GetAccountByUserName(username);
            if (account == null || account.UserName == username)
            {
                return AccountDAO.Instance.updateAccount(username, displayName, image, email, phone, password, type);
            }
            return false;
        }

        public bool DeleteAccount(string username)
        {
            return AccountDAO.Instance.deleteAccount(username);
        }

        public List<AccountDTO> FindAccount(string username = "", string displayName = "")
        {
            return AccountDAO.Instance.findAccount(username, displayName);
        }

        public bool ChangePassword(string username, string password)
        {
            return AccountDAO.Instance.changePassword(username, password);
        }
    }
}

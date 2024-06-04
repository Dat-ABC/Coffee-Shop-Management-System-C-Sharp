using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QLQuanCaPhe.DTO
{
    public class AccountDTO
    {
        private string userName;
        private string password;
        private string displayName;
        private Image ownerPhoto;
        private string email;
        private string phone;
        private string typeName;
        private int type;
        private int isDeleteAccount;

        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public Image OwnerPhoto { get => ownerPhoto; set => ownerPhoto = value; }
        public string Email { get => email; set => email = value; }
        public string Phone { get => phone; set => phone = value; }
        public string TypeName { get => typeName; set => typeName = value; }
        public int Type { get => type; set => type = value; }
        public int IsDeleteAccount { get => isDeleteAccount; set => isDeleteAccount = value; }


        public AccountDTO(string userName, string displayName, Image ownerPhoto, string email, string phone, string password, int type, string typeName, int isDeleteAccount)
        {
            UserName = userName;
            Password = password;
            DisplayName = displayName;
            OwnerPhoto = ownerPhoto;
            Email = email;
            Phone = phone;
            TypeName = typeName;
            Type = type;
            IsDeleteAccount = isDeleteAccount;
        }

        public AccountDTO() { }

        public AccountDTO(DataRow row)
        {
            UserName = Encryption.Instance.Decrypt(row["UserName"].ToString());
            Password = Encryption.Instance.Decrypt(row["Password"].ToString());
            DisplayName = row["DisplayName"].ToString();
            var image = row["OwnerPhoto"];
            if (image.ToString().Trim() != "")
            {
                OwnerPhoto = ImageEncryption.Instance.ByteArrayToImage((byte[])image);
            }
            Email = row["email"].ToString();
            Phone = row["phone"].ToString();
            TypeName = row["typeName"].ToString();
            Type = (int)row["typeid"];
            IsDeleteAccount = (int)row["isDeleteAccount"];
        }
    }
}

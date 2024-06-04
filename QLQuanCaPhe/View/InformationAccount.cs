using QLQuanCaPhe.BUS;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanCaPhe.View
{
    public partial class InformationAccount : Form
    {
        public InformationAccount()
        {
            InitializeComponent();
        }

        private void InformationAccount_Load(object sender, EventArgs e)
        {
            account = BUS_Account.Instance.GetAccountByUserName(BUS_Account.Username);
            txtUsername.Text = account.UserName;
            txtDisplayname.Text = account.DisplayName;
            txtEmail.Text = account.Email;
            if (account.Phone != "")
            {
                mtxtPhone.Text = account.Phone;
            }
            txtPosition.Text = account.TypeName;
            picUser.Image = account.OwnerPhoto;
        }

        AccountDTO account;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void picMinimiezed_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text.Trim() != "")
            {
                if (BUS_CheckFormat.Instance.checkEmail(txtEmail.Text))
                {
                    bool chkPhone = true;
                    string phone = "";
                    if (mtxtPhone.Text != "    -   -" && mtxtPhone.Text != "")
                    {
                        chkPhone = BUS_CheckFormat.Instance.checkPhone(mtxtPhone.Text);
                        phone = mtxtPhone.Text;
                    }
                    if (chkPhone)
                    {
                        BUS_Account.Instance.UpdateAccount(account.UserName, account.DisplayName, account.OwnerPhoto, account.Email, phone, account.Password, account.Type);
                    }
                    else
                    {
                        MessageBox.Show("Số điện thoại không chính xác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Địa chỉ email không chính xác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Email không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword changePassword = new frmChangePassword(account);
            changePassword.ShowDialog();
        }
    }
}

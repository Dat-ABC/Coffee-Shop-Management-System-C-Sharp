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
    public partial class frmChangePassword : Form
    {
        public frmChangePassword()
        {
            InitializeComponent();
        }

        public frmChangePassword(AccountDTO account)
        {
            InitializeComponent();
            Account = account;
        }

        AccountDTO Account;

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
            if (Account.Password == txtOldPassword.Text)
            {
                if (txtNew.Text == txtOldPassword.Text)
                {
                    BUS_Account.Instance.ChangePassword(Account.UserName, txtNew.Text);
                }
                else 
                {
                    MessageBox.Show("Mật khẩu không khớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Mật khẩu cũ không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

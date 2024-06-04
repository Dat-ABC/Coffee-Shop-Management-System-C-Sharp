using QLQuanCaPhe.BUS;
using QLQuanCaPhe.DAO;
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
    public partial class GUI_ForgetPass : Form
    {
        public GUI_ForgetPass()
        {
            InitializeComponent();
        }

        public GUI_ForgetPass(string Username)
        {
            InitializeComponent();
            username = Username;
        }

        string username;

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (txtConfirm.Text == txtNew.Text)
            {
                if (txtConfirm.Text.Trim() != "")
                {  
                    if(BUS_Account.Instance.ChangePassword(username, txtConfirm.Text))
                    {
                        MessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Show();
                        txtNew.Text = "";
                        txtConfirm.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Đổi mật khẩu không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Mật khẩu không được để trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Show();
                }
            }
            else
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp với mật khẩu mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void picMinimiezed_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        public static bool frmExitForgetPass = true;

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmForgetPass_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}

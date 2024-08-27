using QLQuanCaPhe.BUS;
using QLQuanCaPhe.DAO;
using QLQuanCaPhe.DTO;
using QLQuanCaPhe.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanCaPhe
{
    public partial class GUI_Account : Form
    {
        public GUI_Account()
        {
            InitializeComponent();
        }
        Random random = new Random();
        int otp;
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnResend_Click(object sender, EventArgs e)
        {
            AccountDTO account = BUS_Account.Instance.GetAccountByUserName(txtUserName.Text);

            if (account != null)
            {
                if (account.Email == txtEmail.Text)
                {
                    try
                    {
                        otp = random.Next(100000, 1000000);

                        var fromAddress = new MailAddress("nguyendat6789999@gmail.com");
                        var toAddress = new MailAddress(txtEmail.Text.ToString());
                        const string frompass = ""; # password từ gmail
                        const string subject = "Mã OTP từ Coffee Shop Management";
                        string body = otp.ToString();
                        using (var msg = new MailMessage(fromAddress, toAddress))
                        {
                            msg.Subject = subject;
                            msg.Body = "Mã otp là: " + body;

                            using (var smtp = new SmtpClient
                            {
                                Host = "smtp.gmail.com",
                                Port = 587,
                                EnableSsl = true,
                                DeliveryMethod = SmtpDeliveryMethod.Network,
                                UseDefaultCredentials = false,
                                Credentials = new NetworkCredential(fromAddress.Address, frompass),
                                Timeout = 200000
                            })
                            {
                                smtp.Send(msg);
                                MessageBox.Show("Đã gửi OTP qua email");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show($"Tài khoản {txtUserName.Text} không được đăng ký bởi email: {txtEmail.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show($"Tài khoản {txtUserName.Text} không được đăng ký bởi email: {txtEmail.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        public static string username;

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            AccountDTO account = BUS_Account.Instance.GetAccountByUserName(txtUserName.Text);

            if (account != null)
            {
                if (account.Email == txtEmail.Text)
                {
                    if (otp.ToString().Equals(txtOTP.Text))
                    {
                        username = txtUserName.Text;
                        otp = random.Next(100000, 1000000);
                        txtOTP.Text = "";
                        GUI_ForgetPass frmForgetPass = new GUI_ForgetPass(txtUserName.Text);
                        frmForgetPass.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        MessageBox.Show("Mã xác nhận không chính xác");
                    }
                }
                else
                {
                    MessageBox.Show($"Tài khoản {txtUserName.Text} không được đăng ký bởi email: {txtEmail.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show($"Tài khoản {txtUserName.Text} không được đăng ký bởi email: {txtEmail.Text}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void frmAccount_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }

        private void picMinimized_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}

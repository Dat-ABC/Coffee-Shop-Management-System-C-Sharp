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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanCaPhe
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            Button[] btn = new Button[]
            {
                btnOrder, btnMenu, btnArea, btnCustomer, btnInvoice,
                btnSupplier, btnImportExport, btnStatistical, btnShift, btnDiary, btnDecentralization
            };

            UserControl[] userControls = new UserControl[]
            {
                new UserTable(), new UserMenu(), new UserArea(), new UserCustomer(), new UserInvoice(),
                new UCSupplier(), new UserImportEx(), new UserStatistical(), new UserShift(), new UserDiary(), new UserStaff()
            };

            AccountDTO account = BUS_Account.Instance.GetAccountByUserName(BUS_Account.Username);
            lblDisplayName.Text = account.DisplayName;

            List<DecentralizaionDTO> decentralizaions = BUS_Decentralizaion.Instance.GetDecentralization(account.Type);

            int i = 0;
            int n = 0;
            Button tick = new Button();
            int j = 0;

            Point point = new Point(-2, 1140);
            foreach (DecentralizaionDTO decentralizaion in decentralizaions)
            {
                if (decentralizaion.Status == 1)
                {
                    btn[i].Visible = true;

                    for (int z = n; z < btn.Length; z++)
                    {
                        if (!btn[z].Visible)
                        {
                            if (btn[i].Location.Y > btn[z].Location.Y)
                            {
                                Point p = btn[z].Location;
                                Point p2 = btn[i].Location;
                                btn[z].Location = point;
                                btn[i].Location = p;
                                btn[z].Location = p2;

                                Button button = btn[z];
                                btn[z] = btn[i];
                                btn[i] = button;

                                if (n == 0)
                                {
                                    tick = btn[z];
                                    j = i;
                                }
                            }
                            n++;
                            break;
                        }
                    }
                }
                else
                {
                    btn[i].Visible = false;
                }
                i++;
            }

            tick.BackColor = Color.LimeGreen;
            previous = tick;
            addUserControl(userControls[j]);
            lblTitle.Text = tick.Text.Trim();



            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();

            gp.AddEllipse(0, 0, picUser.Width - 3, picUser.Height - 3);
            Region rg = new Region(gp);
            picUser.Region = rg;
            if (account.OwnerPhoto != null)
            {
                picUser.Image = account.OwnerPhoto;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            System.Drawing.Rectangle rectangle = Screen.PrimaryScreen.WorkingArea;
            this.Size = new System.Drawing.Size(Convert.ToInt32(rectangle.Width * 0.9), Convert.ToInt32(rectangle.Height * 0.9));
            this.Location = new System.Drawing.Point(130, 75);
        }

        private void addUserControl(UserControl uc)
        {
            pnControl.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            uc.BringToFront();
            pnControl.Controls.Add(uc);
            pnControl.Invalidate();
        }

        Button previous;


        public static bool frmMainExit = true;

        private void picClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đóng ứng dụng không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                frmMainExit = false;
                Application.Exit();
            }
        }

        private void picMinimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pnFunction_Paint(object sender, PaintEventArgs e)
        {
            int borderWidth = 2;
            Color borderColor = Color.White;

            int borderX = pnFunction.Location.X + pnFunction.Width;
            int borderY = pnFunction.Location.Y;
            int borderHeightWithPanel = pnFunction.Height;

            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawLine(pen, borderX, borderY, borderX, borderY + borderHeightWithPanel);
            }
        }

        private void pnControl_Paint(object sender, PaintEventArgs e)
        {
            int borderWidth = 1;
            Color borderColor = Color.White;

            int borderX = 0;
            int borderY = 2;
            int borderXEnd = pnControl.Width;

            using (Pen pen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawLine(pen, borderX, borderY, borderXEnd, borderY);
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            UserTable userTable = new UserTable();
            previous.BackColor = Color.FromArgb(53, 41, 123);
            previous = btnOrder;
            btnOrder.BackColor = Color.LimeGreen;
            addUserControl(userTable);
            lblTitle.Text = btnOrder.Text.Trim();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            UserMenu userMenu = new UserMenu();
            previous.BackColor = Color.FromArgb(53, 41, 123);
            previous = btnMenu;
            btnMenu.BackColor = Color.LimeGreen;
            addUserControl(userMenu);
            lblTitle.Text = btnMenu.Text.Trim();
        }

        private void btnArea_Click(object sender, EventArgs e)
        {
            UserArea userArea = new UserArea();
            previous.BackColor = Color.FromArgb(53, 41, 123);
            previous = btnArea;
            btnArea.BackColor = Color.LimeGreen;
            addUserControl(userArea);
            lblTitle.Text = btnArea.Text.Trim();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            UserCustomer userCustomer = new UserCustomer();
            previous.BackColor = Color.FromArgb(53, 41, 123);
            previous = btnCustomer;
            btnCustomer.BackColor = Color.LimeGreen;
            addUserControl(userCustomer);
            lblTitle.Text = btnCustomer.Text.Trim();
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            UserInvoice userInvoice = new UserInvoice();
            previous.BackColor = Color.FromArgb(53, 41, 123);
            previous = btnInvoice;
            btnInvoice.BackColor = Color.LimeGreen;
            addUserControl(userInvoice);
            lblTitle.Text = btnInvoice.Text.Trim();
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            UCSupplier uCSupplier = new UCSupplier();
            previous.BackColor = Color.FromArgb(53, 41, 123);
            previous = btnSupplier;
            btnSupplier.BackColor = Color.LimeGreen;
            addUserControl(uCSupplier);
            lblTitle.Text = btnSupplier.Text.Trim();
        }

        private void btnImportExport_Click(object sender, EventArgs e)
        {
            UserImportEx userImportEx = new UserImportEx();
            previous.BackColor = Color.FromArgb(53, 41, 123);
            previous = btnImportExport;
            btnImportExport.BackColor = Color.LimeGreen;
            addUserControl(userImportEx);
            lblTitle.Text = btnImportExport.Text.Trim();
        }

        private void btnStatistical_Click(object sender, EventArgs e)
        {
            UserStatistical userStatistical = new UserStatistical();
            previous.BackColor = Color.FromArgb(53, 41, 123);
            previous = btnStatistical;
            btnStatistical.BackColor = Color.LimeGreen;
            addUserControl(userStatistical);
            lblTitle.Text = btnStatistical.Text.Trim();
        }

        private void btnShift_Click(object sender, EventArgs e)
        {
            UserShift userShift = new UserShift();
            previous.BackColor = Color.FromArgb(53, 41, 123);
            previous = btnShift;
            btnShift.BackColor = Color.LimeGreen;
            addUserControl(userShift);
            lblTitle.Text = btnShift.Text.Trim();
        }

        private void btnDiary_Click(object sender, EventArgs e)
        {
            UserDiary userDiary = new UserDiary();
            previous.BackColor = Color.FromArgb(53, 41, 123);
            previous = btnDiary;
            btnDiary.BackColor = Color.LimeGreen;
            addUserControl(userDiary);
            lblTitle.Text = btnDiary.Text.Trim();
        }

        private void btnDecentralization_Click(object sender, EventArgs e)
        {
            UserStaff userStaff = new UserStaff();
            previous.BackColor = Color.FromArgb(53, 41, 123);
            previous = btnDecentralization;
            btnDecentralization.BackColor = Color.LimeGreen;
            addUserControl(userStaff);
            lblTitle.Text = btnDecentralization.Text.Trim();
            AccountDTO account = BUS_Account.Instance.GetAccountByUserName(BUS_Account.Username);
            lblDisplayName.Text = account.DisplayName;
        }

        private void picUser_Click(object sender, EventArgs e)
        {
            InformationAccount account = new InformationAccount();
            account.ShowDialog();
        }

        private void picBack_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

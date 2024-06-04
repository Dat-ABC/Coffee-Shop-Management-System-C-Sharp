using Microsoft.Reporting.WinForms;
using QLQuanCaPhe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanCaPhe.View
{
    public partial class frmBill : Form
    {
        public frmBill()
        {
            InitializeComponent();
        }

        public frmBill(List<MenuDTO> list, double totalMoney, int discount, double totalMoney2, DateTime? date = null)
        {
            InitializeComponent();
            this.list = list;
            TotalMoney = totalMoney.ToString();
            Discount = discount;
            TotalMoney2 = totalMoney2.ToString();
            dateTime = date ?? DateTime.Now;
        }

        List<MenuDTO> list;
        string TotalMoney;
        int Discount;
        string TotalMoney2;

        DateTime? dateTime;

        private void frmBill_Load(object sender, EventArgs e)
        {
            string date = dateTime?.ToString("dd.MM.yyyy");
            string timeOut = dateTime?.ToString("HH.mm");
            string timeIn = list[0].DateCheckIN.ToString("HH.mm");

            foreach (MenuDTO item in list)
            {
                if (item.Discount > 0)
                {
                    item.FoodName += " -" + item.Discount.ToString() + "%";
                }
            }

            string sDiscount = "";
            string discount = "";
            if (Discount > 0)
            {
                sDiscount = "Giảm giá:";
                discount = Discount.ToString() + "%";
            }

            string shift = "Ca sáng";
            if (list[0].DateCheckIN.Hour > 12 && list[0].DateCheckIN.Hour <= 18)
            {
                shift = "Ca chiều";
            }
            else if (list[0].DateCheckIN.Hour > 18 && list[0].DateCheckIN.Hour < 24)
            {
                shift = "Ca tối";
            }


            Microsoft.Reporting.WinForms.ReportParameter[] para = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
                new Microsoft.Reporting.WinForms.ReportParameter("totalMoney", TotalMoney),
                new Microsoft.Reporting.WinForms.ReportParameter("dateOut", date),
                new Microsoft.Reporting.WinForms.ReportParameter("timeIn", timeIn),
                new Microsoft.Reporting.WinForms.ReportParameter("timeOut", timeOut),
                new Microsoft.Reporting.WinForms.ReportParameter("shift", shift),
                new Microsoft.Reporting.WinForms.ReportParameter("Discount", discount),
                new Microsoft.Reporting.WinForms.ReportParameter("str_discount", sDiscount),
                new Microsoft.Reporting.WinForms.ReportParameter("totalMoney2", TotalMoney2)
            };

            reportBill.LocalReport.ReportEmbeddedResource = "QLQuanCaPhe.View.Bill.rdlc";
            var source = new ReportDataSource("BillList", list);
            reportBill.LocalReport.DataSources.Clear();
            reportBill.LocalReport.DataSources.Add(source);
            reportBill.LocalReport.SetParameters(para);

            this.reportBill.RefreshReport();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void picMinimiezed_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}

namespace QLQuanCaPhe.View
{
    partial class frmBill
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBill));
            this.reportBill = new Microsoft.Reporting.WinForms.ReportViewer();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.picMinimiezed = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMinimiezed)).BeginInit();
            this.SuspendLayout();
            // 
            // reportBill
            // 
            this.reportBill.AutoScroll = true;
            this.reportBill.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.reportBill.Location = new System.Drawing.Point(0, 66);
            this.reportBill.Name = "reportBill";
            this.reportBill.ServerReport.BearerToken = null;
            this.reportBill.Size = new System.Drawing.Size(974, 968);
            this.reportBill.TabIndex = 0;
            // 
            // picClose
            // 
            this.picClose.Image = ((System.Drawing.Image)(resources.GetObject("picClose.Image")));
            this.picClose.Location = new System.Drawing.Point(928, 12);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(34, 34);
            this.picClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picClose.TabIndex = 27;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // picMinimiezed
            // 
            this.picMinimiezed.Image = ((System.Drawing.Image)(resources.GetObject("picMinimiezed.Image")));
            this.picMinimiezed.Location = new System.Drawing.Point(888, 12);
            this.picMinimiezed.Name = "picMinimiezed";
            this.picMinimiezed.Size = new System.Drawing.Size(34, 34);
            this.picMinimiezed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picMinimiezed.TabIndex = 26;
            this.picMinimiezed.TabStop = false;
            this.picMinimiezed.Click += new System.EventHandler(this.picMinimiezed_Click);
            // 
            // frmBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(974, 1034);
            this.ControlBox = false;
            this.Controls.Add(this.picClose);
            this.Controls.Add(this.picMinimiezed);
            this.Controls.Add(this.reportBill);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmBill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmBill_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMinimiezed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportBill;
        private System.Windows.Forms.PictureBox picClose;
        private System.Windows.Forms.PictureBox picMinimiezed;
    }
}
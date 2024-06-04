namespace QLQuanCaPhe
{
    partial class UserMenu
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserMenu));
            this.txtFind = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvFood = new System.Windows.Forms.DataGridView();
            this.Food_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FoodName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.foodPhoto = new System.Windows.Forms.DataGridViewImageColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.txtFoodName = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.pnMenuFunction = new System.Windows.Forms.Panel();
            this.btnCleanFood = new System.Windows.Forms.Button();
            this.btnImportExcel = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnDeleteFood = new System.Windows.Forms.Button();
            this.btnFixFood = new System.Windows.Forms.Button();
            this.btnAddFood = new System.Windows.Forms.Button();
            this.picFoodImage = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pnCategoryFunction = new System.Windows.Forms.Panel();
            this.btnCleanCategory = new System.Windows.Forms.Button();
            this.btnDeleteCategory = new System.Windows.Forms.Button();
            this.btnFixCategory = new System.Windows.Forms.Button();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.pnCategoryList = new System.Windows.Forms.Panel();
            this.flpCategory = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFood)).BeginInit();
            this.pnMenuFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoodImage)).BeginInit();
            this.pnCategoryFunction.SuspendLayout();
            this.pnCategoryList.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtFind
            // 
            this.txtFind.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFind.Location = new System.Drawing.Point(1115, 300);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(330, 39);
            this.txtFind.TabIndex = 1;
            this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(11, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 32);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tên danh mục";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(984, 303);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 32);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tìm kiếm";
            // 
            // dgvFood
            // 
            this.dgvFood.AllowUserToAddRows = false;
            this.dgvFood.AllowUserToDeleteRows = false;
            this.dgvFood.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFood.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFood.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFood.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFood.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Food_ID,
            this.Category,
            this.FoodName,
            this.foodPhoto,
            this.price,
            this.Unit,
            this.Category_ID});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFood.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvFood.Location = new System.Drawing.Point(0, 366);
            this.dgvFood.Name = "dgvFood";
            this.dgvFood.RowHeadersWidth = 82;
            this.dgvFood.RowTemplate.Height = 100;
            this.dgvFood.Size = new System.Drawing.Size(1445, 836);
            this.dgvFood.TabIndex = 7;
            this.dgvFood.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFood_CellClick);
            // 
            // Food_ID
            // 
            this.Food_ID.HeaderText = "Mã món ăn";
            this.Food_ID.MinimumWidth = 10;
            this.Food_ID.Name = "Food_ID";
            this.Food_ID.Visible = false;
            // 
            // Category
            // 
            this.Category.HeaderText = "            Danh mục";
            this.Category.MinimumWidth = 10;
            this.Category.Name = "Category";
            // 
            // FoodName
            // 
            this.FoodName.HeaderText = "           Tên món ăn";
            this.FoodName.MinimumWidth = 10;
            this.FoodName.Name = "FoodName";
            // 
            // foodPhoto
            // 
            this.foodPhoto.FillWeight = 50F;
            this.foodPhoto.HeaderText = "       Ảnh";
            this.foodPhoto.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.foodPhoto.MinimumWidth = 10;
            this.foodPhoto.Name = "foodPhoto";
            // 
            // price
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.price.DefaultCellStyle = dataGridViewCellStyle2;
            this.price.HeaderText = "                 Giá";
            this.price.MinimumWidth = 10;
            this.price.Name = "price";
            // 
            // Unit
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Unit.DefaultCellStyle = dataGridViewCellStyle3;
            this.Unit.HeaderText = "               Đơn vị";
            this.Unit.MinimumWidth = 10;
            this.Unit.Name = "Unit";
            this.Unit.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Category_ID
            // 
            this.Category_ID.HeaderText = "Mã danh mục";
            this.Category_ID.MinimumWidth = 10;
            this.Category_ID.Name = "Category_ID";
            this.Category_ID.Visible = false;
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCategoryName.Location = new System.Drawing.Point(187, 33);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(348, 39);
            this.txtCategoryName.TabIndex = 0;
            // 
            // txtFoodName
            // 
            this.txtFoodName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFoodName.Location = new System.Drawing.Point(189, 93);
            this.txtFoodName.Name = "txtFoodName";
            this.txtFoodName.Size = new System.Drawing.Size(348, 39);
            this.txtFoodName.TabIndex = 0;
            this.txtFoodName.TextChanged += new System.EventHandler(this.txtFoodName_TextChanged);
            // 
            // txtPrice
            // 
            this.txtPrice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrice.Location = new System.Drawing.Point(187, 157);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(348, 39);
            this.txtPrice.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(18, 466);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 32);
            this.label4.TabIndex = 11;
            this.label4.Text = "Ảnh";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(18, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 32);
            this.label5.TabIndex = 12;
            this.label5.Text = "Tên món ăn";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(17, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 32);
            this.label6.TabIndex = 13;
            this.label6.Text = "Giá";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(18, 292);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 32);
            this.label7.TabIndex = 15;
            this.label7.Text = "Danh mục";
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(187, 285);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(348, 40);
            this.cboCategory.TabIndex = 3;
            // 
            // pnMenuFunction
            // 
            this.pnMenuFunction.Controls.Add(this.btnCleanFood);
            this.pnMenuFunction.Controls.Add(this.btnImportExcel);
            this.pnMenuFunction.Controls.Add(this.btnExportExcel);
            this.pnMenuFunction.Controls.Add(this.txtUnit);
            this.pnMenuFunction.Controls.Add(this.label8);
            this.pnMenuFunction.Controls.Add(this.btnDeleteFood);
            this.pnMenuFunction.Controls.Add(this.btnFixFood);
            this.pnMenuFunction.Controls.Add(this.btnAddFood);
            this.pnMenuFunction.Controls.Add(this.picFoodImage);
            this.pnMenuFunction.Controls.Add(this.txtFoodName);
            this.pnMenuFunction.Controls.Add(this.txtPrice);
            this.pnMenuFunction.Controls.Add(this.cboCategory);
            this.pnMenuFunction.Controls.Add(this.label7);
            this.pnMenuFunction.Controls.Add(this.label5);
            this.pnMenuFunction.Controls.Add(this.label4);
            this.pnMenuFunction.Controls.Add(this.label6);
            this.pnMenuFunction.Location = new System.Drawing.Point(1451, 279);
            this.pnMenuFunction.Name = "pnMenuFunction";
            this.pnMenuFunction.Size = new System.Drawing.Size(560, 923);
            this.pnMenuFunction.TabIndex = 0;
            // 
            // btnCleanFood
            // 
            this.btnCleanFood.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(41)))), ((int)(((byte)(123)))));
            this.btnCleanFood.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCleanFood.ForeColor = System.Drawing.Color.White;
            this.btnCleanFood.Image = ((System.Drawing.Image)(resources.GetObject("btnCleanFood.Image")));
            this.btnCleanFood.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCleanFood.Location = new System.Drawing.Point(33, 677);
            this.btnCleanFood.Name = "btnCleanFood";
            this.btnCleanFood.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnCleanFood.Size = new System.Drawing.Size(157, 53);
            this.btnCleanFood.TabIndex = 5;
            this.btnCleanFood.Text = "    Làm mới";
            this.btnCleanFood.UseVisualStyleBackColor = false;
            this.btnCleanFood.Click += new System.EventHandler(this.btnCleanFood_Click);
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(41)))), ((int)(((byte)(123)))));
            this.btnImportExcel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportExcel.ForeColor = System.Drawing.Color.White;
            this.btnImportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnImportExcel.Image")));
            this.btnImportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportExcel.Location = new System.Drawing.Point(314, 828);
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnImportExcel.Size = new System.Drawing.Size(221, 53);
            this.btnImportExcel.TabIndex = 4;
            this.btnImportExcel.Text = "    Nhập từ excel";
            this.btnImportExcel.UseVisualStyleBackColor = false;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(41)))), ((int)(((byte)(123)))));
            this.btnExportExcel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportExcel.ForeColor = System.Drawing.Color.White;
            this.btnExportExcel.Image = ((System.Drawing.Image)(resources.GetObject("btnExportExcel.Image")));
            this.btnExportExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportExcel.Location = new System.Drawing.Point(33, 828);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnExportExcel.Size = new System.Drawing.Size(221, 53);
            this.btnExportExcel.TabIndex = 3;
            this.btnExportExcel.Text = "    Xuất excel";
            this.btnExportExcel.UseVisualStyleBackColor = false;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // txtUnit
            // 
            this.txtUnit.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUnit.Location = new System.Drawing.Point(187, 221);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(348, 39);
            this.txtUnit.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(16, 227);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 32);
            this.label8.TabIndex = 21;
            this.label8.Text = "Đơn vị tính";
            // 
            // btnDeleteFood
            // 
            this.btnDeleteFood.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(41)))), ((int)(((byte)(123)))));
            this.btnDeleteFood.Enabled = false;
            this.btnDeleteFood.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteFood.ForeColor = System.Drawing.Color.White;
            this.btnDeleteFood.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteFood.Image")));
            this.btnDeleteFood.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteFood.Location = new System.Drawing.Point(386, 752);
            this.btnDeleteFood.Name = "btnDeleteFood";
            this.btnDeleteFood.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnDeleteFood.Size = new System.Drawing.Size(148, 53);
            this.btnDeleteFood.TabIndex = 2;
            this.btnDeleteFood.Text = " Xóa";
            this.btnDeleteFood.UseVisualStyleBackColor = false;
            this.btnDeleteFood.Click += new System.EventHandler(this.btnDeleteFood_Click);
            // 
            // btnFixFood
            // 
            this.btnFixFood.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(41)))), ((int)(((byte)(123)))));
            this.btnFixFood.Enabled = false;
            this.btnFixFood.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFixFood.ForeColor = System.Drawing.Color.White;
            this.btnFixFood.Image = ((System.Drawing.Image)(resources.GetObject("btnFixFood.Image")));
            this.btnFixFood.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFixFood.Location = new System.Drawing.Point(33, 752);
            this.btnFixFood.Name = "btnFixFood";
            this.btnFixFood.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnFixFood.Size = new System.Drawing.Size(148, 53);
            this.btnFixFood.TabIndex = 1;
            this.btnFixFood.Text = " Sửa";
            this.btnFixFood.UseVisualStyleBackColor = false;
            this.btnFixFood.Click += new System.EventHandler(this.btnFixFood_Click);
            // 
            // btnAddFood
            // 
            this.btnAddFood.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(41)))), ((int)(((byte)(123)))));
            this.btnAddFood.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddFood.ForeColor = System.Drawing.Color.White;
            this.btnAddFood.Image = ((System.Drawing.Image)(resources.GetObject("btnAddFood.Image")));
            this.btnAddFood.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddFood.Location = new System.Drawing.Point(386, 677);
            this.btnAddFood.Name = "btnAddFood";
            this.btnAddFood.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnAddFood.Size = new System.Drawing.Size(148, 53);
            this.btnAddFood.TabIndex = 0;
            this.btnAddFood.Text = "    Thêm";
            this.btnAddFood.UseVisualStyleBackColor = false;
            this.btnAddFood.Click += new System.EventHandler(this.btnAddFood_Click);
            // 
            // picFoodImage
            // 
            this.picFoodImage.BackColor = System.Drawing.Color.White;
            this.picFoodImage.Location = new System.Drawing.Point(187, 350);
            this.picFoodImage.Name = "picFoodImage";
            this.picFoodImage.Size = new System.Drawing.Size(264, 265);
            this.picFoodImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFoodImage.TabIndex = 16;
            this.picFoodImage.TabStop = false;
            this.picFoodImage.Click += new System.EventHandler(this.picFoodImage_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 37);
            this.label1.TabIndex = 24;
            this.label1.Text = "DANH MỤC";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(12, 299);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(131, 37);
            this.label9.TabIndex = 25;
            this.label9.Text = "MÓN ĂN";
            // 
            // pnCategoryFunction
            // 
            this.pnCategoryFunction.Controls.Add(this.btnCleanCategory);
            this.pnCategoryFunction.Controls.Add(this.btnDeleteCategory);
            this.pnCategoryFunction.Controls.Add(this.btnFixCategory);
            this.pnCategoryFunction.Controls.Add(this.btnAddCategory);
            this.pnCategoryFunction.Controls.Add(this.txtCategoryName);
            this.pnCategoryFunction.Controls.Add(this.label2);
            this.pnCategoryFunction.Location = new System.Drawing.Point(1451, 3);
            this.pnCategoryFunction.Name = "pnCategoryFunction";
            this.pnCategoryFunction.Size = new System.Drawing.Size(554, 258);
            this.pnCategoryFunction.TabIndex = 1;
            // 
            // btnCleanCategory
            // 
            this.btnCleanCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(41)))), ((int)(((byte)(123)))));
            this.btnCleanCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCleanCategory.ForeColor = System.Drawing.Color.White;
            this.btnCleanCategory.Image = ((System.Drawing.Image)(resources.GetObject("btnCleanCategory.Image")));
            this.btnCleanCategory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCleanCategory.Location = new System.Drawing.Point(22, 122);
            this.btnCleanCategory.Name = "btnCleanCategory";
            this.btnCleanCategory.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.btnCleanCategory.Size = new System.Drawing.Size(157, 53);
            this.btnCleanCategory.TabIndex = 4;
            this.btnCleanCategory.Text = "    Làm mới";
            this.btnCleanCategory.UseVisualStyleBackColor = false;
            this.btnCleanCategory.Click += new System.EventHandler(this.btnCleanCategory_Click);
            // 
            // btnDeleteCategory
            // 
            this.btnDeleteCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(41)))), ((int)(((byte)(123)))));
            this.btnDeleteCategory.Enabled = false;
            this.btnDeleteCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteCategory.ForeColor = System.Drawing.Color.White;
            this.btnDeleteCategory.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteCategory.Image")));
            this.btnDeleteCategory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDeleteCategory.Location = new System.Drawing.Point(369, 201);
            this.btnDeleteCategory.Name = "btnDeleteCategory";
            this.btnDeleteCategory.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnDeleteCategory.Size = new System.Drawing.Size(148, 53);
            this.btnDeleteCategory.TabIndex = 3;
            this.btnDeleteCategory.Text = " Xóa";
            this.btnDeleteCategory.UseVisualStyleBackColor = false;
            this.btnDeleteCategory.Click += new System.EventHandler(this.btnDeleteCategory_Click);
            // 
            // btnFixCategory
            // 
            this.btnFixCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(41)))), ((int)(((byte)(123)))));
            this.btnFixCategory.Enabled = false;
            this.btnFixCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFixCategory.ForeColor = System.Drawing.Color.White;
            this.btnFixCategory.Image = ((System.Drawing.Image)(resources.GetObject("btnFixCategory.Image")));
            this.btnFixCategory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFixCategory.Location = new System.Drawing.Point(22, 201);
            this.btnFixCategory.Name = "btnFixCategory";
            this.btnFixCategory.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnFixCategory.Size = new System.Drawing.Size(148, 53);
            this.btnFixCategory.TabIndex = 2;
            this.btnFixCategory.Text = " Sửa";
            this.btnFixCategory.UseVisualStyleBackColor = false;
            this.btnFixCategory.Click += new System.EventHandler(this.btnFixCategory_Click);
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(41)))), ((int)(((byte)(123)))));
            this.btnAddCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddCategory.ForeColor = System.Drawing.Color.White;
            this.btnAddCategory.Image = ((System.Drawing.Image)(resources.GetObject("btnAddCategory.Image")));
            this.btnAddCategory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddCategory.Location = new System.Drawing.Point(369, 122);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnAddCategory.Size = new System.Drawing.Size(148, 53);
            this.btnAddCategory.TabIndex = 1;
            this.btnAddCategory.Text = "    Thêm";
            this.btnAddCategory.UseVisualStyleBackColor = false;
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // pnCategoryList
            // 
            this.pnCategoryList.Controls.Add(this.flpCategory);
            this.pnCategoryList.Controls.Add(this.pnCategoryFunction);
            this.pnCategoryList.Controls.Add(this.label1);
            this.pnCategoryList.Location = new System.Drawing.Point(0, 3);
            this.pnCategoryList.Name = "pnCategoryList";
            this.pnCategoryList.Size = new System.Drawing.Size(2008, 270);
            this.pnCategoryList.TabIndex = 26;
            this.pnCategoryList.Paint += new System.Windows.Forms.PaintEventHandler(this.pnCategoryList_Paint);
            // 
            // flpCategory
            // 
            this.flpCategory.AutoScroll = true;
            this.flpCategory.BackColor = System.Drawing.Color.White;
            this.flpCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flpCategory.Location = new System.Drawing.Point(0, 71);
            this.flpCategory.Name = "flpCategory";
            this.flpCategory.Size = new System.Drawing.Size(1445, 186);
            this.flpCategory.TabIndex = 25;
            this.flpCategory.Click += new System.EventHandler(this.flpCategory_Click);
            // 
            // UserMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(41)))), ((int)(((byte)(123)))));
            this.Controls.Add(this.pnCategoryList);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.pnMenuFunction);
            this.Controls.Add(this.dgvFood);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFind);
            this.Name = "UserMenu";
            this.Size = new System.Drawing.Size(2011, 1202);
            this.Load += new System.EventHandler(this.UserItems_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UserMenu_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFood)).EndInit();
            this.pnMenuFunction.ResumeLayout(false);
            this.pnMenuFunction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFoodImage)).EndInit();
            this.pnCategoryFunction.ResumeLayout(false);
            this.pnCategoryFunction.PerformLayout();
            this.pnCategoryList.ResumeLayout(false);
            this.pnCategoryList.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvFood;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.TextBox txtFoodName;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Panel pnMenuFunction;
        private System.Windows.Forms.PictureBox picFoodImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel pnCategoryFunction;
        private System.Windows.Forms.Panel pnCategoryList;
        private System.Windows.Forms.Button btnDeleteFood;
        private System.Windows.Forms.Button btnFixFood;
        private System.Windows.Forms.Button btnAddFood;
        private System.Windows.Forms.Button btnDeleteCategory;
        private System.Windows.Forms.Button btnFixCategory;
        private System.Windows.Forms.Button btnAddCategory;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnImportExcel;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.FlowLayoutPanel flpCategory;
        private System.Windows.Forms.Button btnCleanFood;
        private System.Windows.Forms.Button btnCleanCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn Food_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn FoodName;
        private System.Windows.Forms.DataGridViewImageColumn foodPhoto;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category_ID;
    }
}

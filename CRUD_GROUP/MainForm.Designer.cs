namespace CRUD_GROUP
{
    partial class MainForm
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

        public bool isUser = true;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        /// 
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.MainControl = new System.Windows.Forms.TabControl();
            this.Tab1 = new System.Windows.Forms.TabPage();
            this.FieldsPanel = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.CancelAddButton = new System.Windows.Forms.Button();
            this.txtID = new System.Windows.Forms.TextBox();
            this.lblID = new System.Windows.Forms.Label();
            this.txtReceipt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.txtPrev = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtCurrent = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.dgvRecords = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.customerNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.previousKWHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.currentKWHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ratePerKWHDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receiptNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recordTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.recordsDataSet = new CRUD_GROUP.RecordsDataSet();
            this.AddButton = new System.Windows.Forms.Button();
            this.Tab2 = new System.Windows.Forms.TabPage();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.picGraph = new System.Windows.Forms.PictureBox();
            this.strMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.changeAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitAltF4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SearchComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UserLabel = new System.Windows.Forms.Label();
            this.recordTableTableAdapter = new CRUD_GROUP.RecordsDataSetTableAdapters.RecordTableTableAdapter();
            this.PaidCheckBox = new System.Windows.Forms.CheckBox();
            this.chkUnpaid = new System.Windows.Forms.CheckBox();
            this.recordsDataSet1 = new CRUD_GROUP.RecordsDataSet1();
            this.recordTableBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.recordTableTableAdapter1 = new CRUD_GROUP.RecordsDataSet1TableAdapters.RecordTableTableAdapter();
            this.Paid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MainControl.SuspendLayout();
            this.Tab1.SuspendLayout();
            this.FieldsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recordTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recordsDataSet)).BeginInit();
            this.Tab2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGraph)).BeginInit();
            this.strMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recordsDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.recordTableBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // MainControl
            // 
            this.MainControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainControl.Controls.Add(this.Tab1);
            this.MainControl.Controls.Add(this.Tab2);
            this.MainControl.Location = new System.Drawing.Point(9, 38);
            this.MainControl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MainControl.MinimumSize = new System.Drawing.Size(962, 352);
            this.MainControl.Name = "MainControl";
            this.MainControl.SelectedIndex = 0;
            this.MainControl.Size = new System.Drawing.Size(962, 352);
            this.MainControl.TabIndex = 0;
            this.MainControl.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // Tab1
            // 
            this.Tab1.Controls.Add(this.chkUnpaid);
            this.Tab1.Controls.Add(this.FieldsPanel);
            this.Tab1.Controls.Add(this.dgvRecords);
            this.Tab1.Controls.Add(this.AddButton);
            this.Tab1.Location = new System.Drawing.Point(4, 22);
            this.Tab1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Tab1.Name = "Tab1";
            this.Tab1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Tab1.Size = new System.Drawing.Size(954, 326);
            this.Tab1.TabIndex = 0;
            this.Tab1.Text = "Records";
            this.Tab1.UseVisualStyleBackColor = true;
            // 
            // FieldsPanel
            // 
            this.FieldsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldsPanel.Controls.Add(this.PaidCheckBox);
            this.FieldsPanel.Controls.Add(this.btnDelete);
            this.FieldsPanel.Controls.Add(this.CancelAddButton);
            this.FieldsPanel.Controls.Add(this.txtID);
            this.FieldsPanel.Controls.Add(this.lblID);
            this.FieldsPanel.Controls.Add(this.txtReceipt);
            this.FieldsPanel.Controls.Add(this.label2);
            this.FieldsPanel.Controls.Add(this.txtRate);
            this.FieldsPanel.Controls.Add(this.txtPrev);
            this.FieldsPanel.Controls.Add(this.label6);
            this.FieldsPanel.Controls.Add(this.label5);
            this.FieldsPanel.Controls.Add(this.label4);
            this.FieldsPanel.Controls.Add(this.lblName);
            this.FieldsPanel.Controls.Add(this.txtCurrent);
            this.FieldsPanel.Controls.Add(this.txtName);
            this.FieldsPanel.Controls.Add(this.btnSave);
            this.FieldsPanel.Controls.Add(this.btnUpdate);
            this.FieldsPanel.Enabled = false;
            this.FieldsPanel.Location = new System.Drawing.Point(4, 5);
            this.FieldsPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.FieldsPanel.Name = "FieldsPanel";
            this.FieldsPanel.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.FieldsPanel.Size = new System.Drawing.Size(946, 318);
            this.FieldsPanel.TabIndex = 6;
            this.FieldsPanel.TabStop = false;
            this.FieldsPanel.Text = "Input";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(264, 278);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(128, 36);
            this.btnDelete.TabIndex = 21;
            this.btnDelete.Text = "Delete...";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // CancelAddButton
            // 
            this.CancelAddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelAddButton.Location = new System.Drawing.Point(844, 278);
            this.CancelAddButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CancelAddButton.Name = "CancelAddButton";
            this.CancelAddButton.Size = new System.Drawing.Size(98, 36);
            this.CancelAddButton.TabIndex = 20;
            this.CancelAddButton.Text = "Cancel";
            this.CancelAddButton.UseVisualStyleBackColor = true;
            this.CancelAddButton.Click += new System.EventHandler(this.CancelAddButton_Click);
            // 
            // txtID
            // 
            this.txtID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID.Location = new System.Drawing.Point(118, 24);
            this.txtID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(804, 20);
            this.txtID.TabIndex = 19;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.Location = new System.Drawing.Point(24, 24);
            this.lblID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(21, 15);
            this.lblID.TabIndex = 18;
            this.lblID.Text = "ID:";
            // 
            // txtReceipt
            // 
            this.txtReceipt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceipt.Location = new System.Drawing.Point(118, 200);
            this.txtReceipt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtReceipt.Name = "txtReceipt";
            this.txtReceipt.Size = new System.Drawing.Size(804, 20);
            this.txtReceipt.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 202);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 16;
            this.label2.Text = "Receipt No.:";
            // 
            // txtRate
            // 
            this.txtRate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRate.Location = new System.Drawing.Point(118, 164);
            this.txtRate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(804, 20);
            this.txtRate.TabIndex = 15;
            // 
            // txtPrev
            // 
            this.txtPrev.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrev.Location = new System.Drawing.Point(118, 92);
            this.txtPrev.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtPrev.Name = "txtPrev";
            this.txtPrev.Size = new System.Drawing.Size(804, 20);
            this.txtPrev.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(24, 166);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Rate per KWH:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(24, 128);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "Current KWH:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(24, 92);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 15);
            this.label4.TabIndex = 11;
            this.label4.Text = "Previous KWH:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(24, 59);
            this.lblName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(42, 15);
            this.lblName.TabIndex = 10;
            this.lblName.Text = "Name:";
            // 
            // txtCurrent
            // 
            this.txtCurrent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurrent.Location = new System.Drawing.Point(118, 128);
            this.txtCurrent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCurrent.Name = "txtCurrent";
            this.txtCurrent.Size = new System.Drawing.Size(804, 20);
            this.txtCurrent.TabIndex = 8;
            this.txtCurrent.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(118, 59);
            this.txtName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(804, 20);
            this.txtName.TabIndex = 7;
            this.txtName.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(4, 278);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(124, 36);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save/New Record...";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdate.Location = new System.Drawing.Point(132, 278);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(128, 36);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Update...";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // dgvRecords
            // 
            this.dgvRecords.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecords.AutoGenerateColumns = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecords.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.customerNameDataGridViewTextBoxColumn,
            this.previousKWHDataGridViewTextBoxColumn,
            this.currentKWHDataGridViewTextBoxColumn,
            this.ratePerKWHDataGridViewTextBoxColumn,
            this.priceDataGridViewTextBoxColumn,
            this.receiptNoDataGridViewTextBoxColumn,
            this.Paid});
            this.dgvRecords.DataSource = this.recordTableBindingSource1;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecords.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvRecords.Location = new System.Drawing.Point(274, 5);
            this.dgvRecords.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dgvRecords.MultiSelect = false;
            this.dgvRecords.Name = "dgvRecords";
            this.dgvRecords.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecords.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvRecords.RowHeadersWidth = 51;
            this.dgvRecords.RowTemplate.Height = 24;
            this.dgvRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecords.Size = new System.Drawing.Size(676, 318);
            this.dgvRecords.TabIndex = 0;
            this.dgvRecords.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.Width = 125;
            // 
            // customerNameDataGridViewTextBoxColumn
            // 
            this.customerNameDataGridViewTextBoxColumn.DataPropertyName = "CustomerName";
            this.customerNameDataGridViewTextBoxColumn.HeaderText = "CustomerName";
            this.customerNameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.customerNameDataGridViewTextBoxColumn.Name = "customerNameDataGridViewTextBoxColumn";
            this.customerNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.customerNameDataGridViewTextBoxColumn.Width = 125;
            // 
            // previousKWHDataGridViewTextBoxColumn
            // 
            this.previousKWHDataGridViewTextBoxColumn.DataPropertyName = "PreviousKWH";
            this.previousKWHDataGridViewTextBoxColumn.HeaderText = "PreviousKWH";
            this.previousKWHDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.previousKWHDataGridViewTextBoxColumn.Name = "previousKWHDataGridViewTextBoxColumn";
            this.previousKWHDataGridViewTextBoxColumn.ReadOnly = true;
            this.previousKWHDataGridViewTextBoxColumn.Width = 125;
            // 
            // currentKWHDataGridViewTextBoxColumn
            // 
            this.currentKWHDataGridViewTextBoxColumn.DataPropertyName = "CurrentKWH";
            this.currentKWHDataGridViewTextBoxColumn.HeaderText = "CurrentKWH";
            this.currentKWHDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.currentKWHDataGridViewTextBoxColumn.Name = "currentKWHDataGridViewTextBoxColumn";
            this.currentKWHDataGridViewTextBoxColumn.ReadOnly = true;
            this.currentKWHDataGridViewTextBoxColumn.Width = 125;
            // 
            // ratePerKWHDataGridViewTextBoxColumn
            // 
            this.ratePerKWHDataGridViewTextBoxColumn.DataPropertyName = "RatePerKWH";
            this.ratePerKWHDataGridViewTextBoxColumn.HeaderText = "RatePerKWH";
            this.ratePerKWHDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.ratePerKWHDataGridViewTextBoxColumn.Name = "ratePerKWHDataGridViewTextBoxColumn";
            this.ratePerKWHDataGridViewTextBoxColumn.ReadOnly = true;
            this.ratePerKWHDataGridViewTextBoxColumn.Width = 125;
            // 
            // priceDataGridViewTextBoxColumn
            // 
            this.priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
            this.priceDataGridViewTextBoxColumn.HeaderText = "Price";
            this.priceDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
            this.priceDataGridViewTextBoxColumn.ReadOnly = true;
            this.priceDataGridViewTextBoxColumn.Width = 125;
            // 
            // receiptNoDataGridViewTextBoxColumn
            // 
            this.receiptNoDataGridViewTextBoxColumn.DataPropertyName = "ReceiptNo";
            this.receiptNoDataGridViewTextBoxColumn.HeaderText = "ReceiptNo";
            this.receiptNoDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.receiptNoDataGridViewTextBoxColumn.Name = "receiptNoDataGridViewTextBoxColumn";
            this.receiptNoDataGridViewTextBoxColumn.ReadOnly = true;
            this.receiptNoDataGridViewTextBoxColumn.Width = 125;
            // 
            // recordTableBindingSource
            // 
            this.recordTableBindingSource.DataMember = "RecordTable";
            this.recordTableBindingSource.DataSource = this.recordsDataSet;
            // 
            // recordsDataSet
            // 
            this.recordsDataSet.DataSetName = "RecordsDataSet";
            this.recordsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(9, 7);
            this.AddButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(256, 36);
            this.AddButton.TabIndex = 1;
            this.AddButton.Text = "Add/Edit...";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // Tab2
            // 
            this.Tab2.Controls.Add(this.button6);
            this.Tab2.Controls.Add(this.button5);
            this.Tab2.Controls.Add(this.button4);
            this.Tab2.Controls.Add(this.picGraph);
            this.Tab2.Location = new System.Drawing.Point(4, 22);
            this.Tab2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Tab2.Name = "Tab2";
            this.Tab2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Tab2.Size = new System.Drawing.Size(954, 326);
            this.Tab2.TabIndex = 1;
            this.Tab2.Text = "Graph";
            this.Tab2.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button6.Location = new System.Drawing.Point(162, 232);
            this.button6.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(57, 33);
            this.button6.TabIndex = 3;
            this.button6.Text = "Print...";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button5.Location = new System.Drawing.Point(14, 270);
            this.button5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(144, 33);
            this.button5.TabIndex = 2;
            this.button5.Text = "Save as CSV...";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.Location = new System.Drawing.Point(14, 232);
            this.button4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(144, 33);
            this.button4.TabIndex = 1;
            this.button4.Text = "Save as PNG...";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // picGraph
            // 
            this.picGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picGraph.Location = new System.Drawing.Point(4, 5);
            this.picGraph.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picGraph.Name = "picGraph";
            this.picGraph.Size = new System.Drawing.Size(946, 212);
            this.picGraph.TabIndex = 0;
            this.picGraph.TabStop = false;
            // 
            // strMain
            // 
            this.strMain.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.strMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.strMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.graphToolStripMenuItem,
            this.SearchComboBox,
            this.searchToolStripMenuItem});
            this.strMain.Location = new System.Drawing.Point(0, 0);
            this.strMain.Name = "strMain";
            this.strMain.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.strMain.Size = new System.Drawing.Size(980, 27);
            this.strMain.TabIndex = 1;
            this.strMain.Text = "MainMenuStrip";
            this.strMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountInformationToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitAltF4ToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 23);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // accountInformationToolStripMenuItem
            // 
            this.accountInformationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infoToolStripMenuItem,
            this.toolStripSeparator1,
            this.changeAccountToolStripMenuItem,
            this.logOutToolStripMenuItem});
            this.accountInformationToolStripMenuItem.Name = "accountInformationToolStripMenuItem";
            this.accountInformationToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.accountInformationToolStripMenuItem.Text = "Account";
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.infoToolStripMenuItem.Text = "New Account...";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(167, 6);
            // 
            // changeAccountToolStripMenuItem
            // 
            this.changeAccountToolStripMenuItem.Name = "changeAccountToolStripMenuItem";
            this.changeAccountToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.changeAccountToolStripMenuItem.Text = "Change account...";
            // 
            // logOutToolStripMenuItem
            // 
            this.logOutToolStripMenuItem.Name = "logOutToolStripMenuItem";
            this.logOutToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.logOutToolStripMenuItem.Text = "Log out";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(136, 6);
            // 
            // exitAltF4ToolStripMenuItem
            // 
            this.exitAltF4ToolStripMenuItem.Name = "exitAltF4ToolStripMenuItem";
            this.exitAltF4ToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.exitAltF4ToolStripMenuItem.Text = "Exit (Alt+F4)";
            // 
            // graphToolStripMenuItem
            // 
            this.graphToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem,
            this.saveAsToolStripMenuItem});
            this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            this.graphToolStripMenuItem.Size = new System.Drawing.Size(51, 23);
            this.graphToolStripMenuItem.Text = "Graph";
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            // 
            // SearchComboBox
            // 
            this.SearchComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SearchComboBox.Items.AddRange(new object[] {
            "All"});
            this.SearchComboBox.Name = "SearchComboBox";
            this.SearchComboBox.Size = new System.Drawing.Size(92, 23);
            this.SearchComboBox.Click += new System.EventHandler(this.SearchComboBox_Click);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(54, 23);
            this.searchToolStripMenuItem.Text = "Search";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // UserLabel
            // 
            this.UserLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UserLabel.AutoSize = true;
            this.UserLabel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.UserLabel.Location = new System.Drawing.Point(886, 7);
            this.UserLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(88, 13);
            this.UserLabel.TabIndex = 3;
            this.UserLabel.Text = "admin@localhost";
            this.UserLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // recordTableTableAdapter
            // 
            this.recordTableTableAdapter.ClearBeforeFill = true;
            // 
            // PaidCheckBox
            // 
            this.PaidCheckBox.AutoSize = true;
            this.PaidCheckBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PaidCheckBox.Location = new System.Drawing.Point(27, 240);
            this.PaidCheckBox.Name = "PaidCheckBox";
            this.PaidCheckBox.Size = new System.Drawing.Size(87, 19);
            this.PaidCheckBox.TabIndex = 22;
            this.PaidCheckBox.Text = "Is this paid?";
            this.PaidCheckBox.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.PaidCheckBox.UseVisualStyleBackColor = true;
            this.PaidCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // chkUnpaid
            // 
            this.chkUnpaid.AutoSize = true;
            this.chkUnpaid.Location = new System.Drawing.Point(9, 62);
            this.chkUnpaid.Name = "chkUnpaid";
            this.chkUnpaid.Size = new System.Drawing.Size(126, 17);
            this.chkUnpaid.TabIndex = 6;
            this.chkUnpaid.Text = "Show unpaid records";
            this.chkUnpaid.UseVisualStyleBackColor = true;
            this.chkUnpaid.CheckedChanged += new System.EventHandler(this.chkUnpaid_CheckedChanged);
            // 
            // recordsDataSet1
            // 
            this.recordsDataSet1.DataSetName = "RecordsDataSet1";
            this.recordsDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // recordTableBindingSource1
            // 
            this.recordTableBindingSource1.DataMember = "RecordTable";
            this.recordTableBindingSource1.DataSource = this.recordsDataSet1;
            // 
            // recordTableTableAdapter1
            // 
            this.recordTableTableAdapter1.ClearBeforeFill = true;
            // 
            // Paid
            // 
            this.Paid.DataPropertyName = "Paid";
            this.Paid.HeaderText = "Paid";
            this.Paid.Name = "Paid";
            this.Paid.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(980, 409);
            this.Controls.Add(this.UserLabel);
            this.Controls.Add(this.MainControl);
            this.Controls.Add(this.strMain);
            this.MainMenuStrip = this.strMain;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Barangay Electricity File Management";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainControl.ResumeLayout(false);
            this.Tab1.ResumeLayout(false);
            this.Tab1.PerformLayout();
            this.FieldsPanel.ResumeLayout(false);
            this.FieldsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecords)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recordTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recordsDataSet)).EndInit();
            this.Tab2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picGraph)).EndInit();
            this.strMain.ResumeLayout(false);
            this.strMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recordsDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.recordTableBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl MainControl;
        private System.Windows.Forms.TabPage Tab1;
        private System.Windows.Forms.TabPage Tab2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.DataGridView dgvRecords;
        private System.Windows.Forms.MenuStrip strMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox SearchComboBox;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.PictureBox picGraph;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.ToolStripMenuItem accountInformationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem changeAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitAltF4ToolStripMenuItem;
        private System.Windows.Forms.GroupBox FieldsPanel;
        private System.Windows.Forms.Button btnUpdate;
        private RecordsDataSet recordsDataSet;
        private System.Windows.Forms.BindingSource recordTableBindingSource;
        private RecordsDataSetTableAdapters.RecordTableTableAdapter recordTableTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn previousKWHDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn currentKWHDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ratePerKWHDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn receiptNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.TextBox txtCurrent;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPrev;
        private System.Windows.Forms.TextBox txtRate;
        private System.Windows.Forms.TextBox txtReceipt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.Button CancelAddButton;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.CheckBox PaidCheckBox;
        private System.Windows.Forms.CheckBox chkUnpaid;
        private RecordsDataSet1 recordsDataSet1;
        private System.Windows.Forms.BindingSource recordTableBindingSource1;
        private RecordsDataSet1TableAdapters.RecordTableTableAdapter recordTableTableAdapter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Paid;
    }
}


namespace CRUD_GROUP
{
    partial class DisplayUnrecord
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
            this.MainList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Clip = new System.Windows.Forms.Label();
            this.lstDetails = new System.Windows.Forms.ListView();
            this.Property = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.CopyID = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MainList
            // 
            this.MainList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainList.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainList.FormattingEnabled = true;
            this.MainList.ItemHeight = 25;
            this.MainList.Location = new System.Drawing.Point(30, 119);
            this.MainList.Name = "MainList";
            this.MainList.Size = new System.Drawing.Size(1251, 352);
            this.MainList.TabIndex = 0;
            this.MainList.SelectedIndexChanged += new System.EventHandler(this.MainList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semilight", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label1.Location = new System.Drawing.Point(19, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(657, 62);
            this.label1.TabIndex = 1;
            this.label1.Text = "List of unclaimed Reference IDs";
            // 
            // Clip
            // 
            this.Clip.AutoSize = true;
            this.Clip.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.Clip.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.Clip.Location = new System.Drawing.Point(877, 54);
            this.Clip.Name = "Clip";
            this.Clip.Size = new System.Drawing.Size(76, 25);
            this.Clip.TabIndex = 2;
            this.Clip.Text = "Copied:";
            // 
            // lstDetails
            // 
            this.lstDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDetails.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.lstDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Property,
            this.Value});
            this.lstDetails.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDetails.FullRowSelect = true;
            this.lstDetails.GridLines = true;
            this.lstDetails.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstDetails.HideSelection = false;
            this.lstDetails.Location = new System.Drawing.Point(30, 545);
            this.lstDetails.Name = "lstDetails";
            this.lstDetails.Size = new System.Drawing.Size(1045, 189);
            this.lstDetails.TabIndex = 3;
            this.lstDetails.UseCompatibleStateImageBehavior = false;
            this.lstDetails.View = System.Windows.Forms.View.Details;
            // 
            // Property
            // 
            this.Property.Text = "Property";
            this.Property.Width = 147;
            // 
            // Value
            // 
            this.Value.Text = "Value";
            this.Value.Width = 386;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label2.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.label2.Location = new System.Drawing.Point(1095, 495);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Actions";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnDelete.FlatAppearance.BorderSize = 3;
            this.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrchid;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDelete.Location = new System.Drawing.Point(1100, 617);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(181, 44);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete...";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F);
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(25, 495);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Properties";
            // 
            // CopyID
            // 
            this.CopyID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CopyID.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.CopyID.FlatAppearance.BorderSize = 3;
            this.CopyID.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.CopyID.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrchid;
            this.CopyID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CopyID.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CopyID.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CopyID.Location = new System.Drawing.Point(1100, 545);
            this.CopyID.Name = "CopyID";
            this.CopyID.Size = new System.Drawing.Size(181, 44);
            this.CopyID.TabIndex = 7;
            this.CopyID.Text = "Copy ID";
            this.CopyID.UseVisualStyleBackColor = true;
            this.CopyID.Click += new System.EventHandler(this.CopyID_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.CloseButton.FlatAppearance.BorderSize = 3;
            this.CloseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.CloseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkOrchid;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.CloseButton.Location = new System.Drawing.Point(1100, 690);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(181, 44);
            this.CloseButton.TabIndex = 8;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // DisplayUnrecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Indigo;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(1312, 783);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.CopyID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lstDetails);
            this.Controls.Add(this.Clip);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MainList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1312, 783);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1312, 783);
            this.Name = "DisplayUnrecord";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.DisplayUnrecord_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox MainList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Clip;
        private System.Windows.Forms.ListView lstDetails;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ColumnHeader Property;
        private System.Windows.Forms.ColumnHeader Value;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CopyID;
        private System.Windows.Forms.Button CloseButton;
    }
}
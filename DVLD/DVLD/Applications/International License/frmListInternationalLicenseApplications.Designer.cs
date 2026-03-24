namespace DVLD.Applications.International_License
{
    partial class frmListInternationalLicenseApplications
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
            this.components = new System.ComponentModel.Container();
            this.cbIsActive = new System.Windows.Forms.ComboBox();
            this.btnAddNewInternationalLicenseApplication = new System.Windows.Forms.Button();
            this.txtSearchInput = new System.Windows.Forms.TextBox();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.lblRecord = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dgvInternationalLicenseApplicationsList = new System.Windows.Forms.DataGridView();
            this.cmsInternationalLicenseApplicationOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowPersonDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowLicenseDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowPersonLicenseHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicenseApplicationsList)).BeginInit();
            this.cmsInternationalLicenseApplicationOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btcClose
            // 
            this.btcClose.Location = new System.Drawing.Point(763, 572);
            // 
            // cbIsActive
            // 
            this.cbIsActive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIsActive.FormattingEnabled = true;
            this.cbIsActive.Location = new System.Drawing.Point(250, 240);
            this.cbIsActive.Name = "cbIsActive";
            this.cbIsActive.Size = new System.Drawing.Size(136, 21);
            this.cbIsActive.TabIndex = 29;
            this.cbIsActive.Visible = false;
            this.cbIsActive.SelectedIndexChanged += new System.EventHandler(this.cbIsActive_SelectedIndexChanged);
            // 
            // btnAddNewInternationalLicenseApplication
            // 
            this.btnAddNewInternationalLicenseApplication.AllowDrop = true;
            this.btnAddNewInternationalLicenseApplication.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddNewInternationalLicenseApplication.BackgroundImage = global::DVLD.Properties.Resources.New_Application_64;
            this.btnAddNewInternationalLicenseApplication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddNewInternationalLicenseApplication.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewInternationalLicenseApplication.Location = new System.Drawing.Point(813, 213);
            this.btnAddNewInternationalLicenseApplication.Name = "btnAddNewInternationalLicenseApplication";
            this.btnAddNewInternationalLicenseApplication.Size = new System.Drawing.Size(48, 47);
            this.btnAddNewInternationalLicenseApplication.TabIndex = 20;
            this.btnAddNewInternationalLicenseApplication.UseVisualStyleBackColor = true;
            this.btnAddNewInternationalLicenseApplication.Click += new System.EventHandler(this.btnAddNewInternationalLicenseApplication_Click);
            // 
            // txtSearchInput
            // 
            this.txtSearchInput.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSearchInput.Location = new System.Drawing.Point(250, 238);
            this.txtSearchInput.Name = "txtSearchInput";
            this.txtSearchInput.Size = new System.Drawing.Size(180, 24);
            this.txtSearchInput.TabIndex = 24;
            this.txtSearchInput.Visible = false;
            this.txtSearchInput.TextChanged += new System.EventHandler(this.txtSearchInput_TextChanged);
            this.txtSearchInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchInput_KeyPress);
            // 
            // cbFilter
            // 
            this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Location = new System.Drawing.Point(91, 240);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(136, 21);
            this.cbFilter.TabIndex = 22;
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            // 
            // lblRecord
            // 
            this.lblRecord.AutoSize = true;
            this.lblRecord.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecord.Location = new System.Drawing.Point(96, 575);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(18, 19);
            this.lblRecord.TabIndex = 26;
            this.lblRecord.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 575);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 19);
            this.label3.TabIndex = 25;
            this.label3.Text = "# Records: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 19);
            this.label2.TabIndex = 23;
            this.label2.Text = "Filter By:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Firebrick;
            this.label1.Location = new System.Drawing.Point(219, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(530, 36);
            this.label1.TabIndex = 21;
            this.label1.Text = "International License Applciations";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Applications;
            this.pictureBox1.Location = new System.Drawing.Point(400, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // dgvInternationalLicenseApplicationsList
            // 
            this.dgvInternationalLicenseApplicationsList.AllowUserToAddRows = false;
            this.dgvInternationalLicenseApplicationsList.AllowUserToDeleteRows = false;
            this.dgvInternationalLicenseApplicationsList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dgvInternationalLicenseApplicationsList.BackgroundColor = System.Drawing.Color.White;
            this.dgvInternationalLicenseApplicationsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInternationalLicenseApplicationsList.ContextMenuStrip = this.cmsInternationalLicenseApplicationOptions;
            this.dgvInternationalLicenseApplicationsList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvInternationalLicenseApplicationsList.Location = new System.Drawing.Point(7, 273);
            this.dgvInternationalLicenseApplicationsList.MultiSelect = false;
            this.dgvInternationalLicenseApplicationsList.Name = "dgvInternationalLicenseApplicationsList";
            this.dgvInternationalLicenseApplicationsList.ReadOnly = true;
            this.dgvInternationalLicenseApplicationsList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.dgvInternationalLicenseApplicationsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInternationalLicenseApplicationsList.Size = new System.Drawing.Size(854, 293);
            this.dgvInternationalLicenseApplicationsList.TabIndex = 27;
            this.dgvInternationalLicenseApplicationsList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvInternationalLicenseApplicationsList_MouseDown);
            // 
            // cmsInternationalLicenseApplicationOptions
            // 
            this.cmsInternationalLicenseApplicationOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowPersonDetails,
            this.ShowLicenseDetails,
            this.ShowPersonLicenseHistory});
            this.cmsInternationalLicenseApplicationOptions.Name = "contextMenuStrip1";
            this.cmsInternationalLicenseApplicationOptions.Size = new System.Drawing.Size(226, 70);
            // 
            // ShowPersonDetails
            // 
            this.ShowPersonDetails.Image = global::DVLD.Properties.Resources.PersonDetails_32;
            this.ShowPersonDetails.Name = "ShowPersonDetails";
            this.ShowPersonDetails.Size = new System.Drawing.Size(225, 22);
            this.ShowPersonDetails.Text = "Show Person Details";
            this.ShowPersonDetails.Click += new System.EventHandler(this.ShowPersonDetails_Click);
            // 
            // ShowLicenseDetails
            // 
            this.ShowLicenseDetails.Image = global::DVLD.Properties.Resources.License_View_32;
            this.ShowLicenseDetails.Name = "ShowLicenseDetails";
            this.ShowLicenseDetails.Size = new System.Drawing.Size(225, 22);
            this.ShowLicenseDetails.Text = "Show License Details";
            this.ShowLicenseDetails.Click += new System.EventHandler(this.ShowLicenseDetails_Click);
            // 
            // ShowPersonLicenseHistory
            // 
            this.ShowPersonLicenseHistory.Image = global::DVLD.Properties.Resources.PersonLicenseHistory_32;
            this.ShowPersonLicenseHistory.Name = "ShowPersonLicenseHistory";
            this.ShowPersonLicenseHistory.Size = new System.Drawing.Size(225, 22);
            this.ShowPersonLicenseHistory.Text = "Show Person License History";
            this.ShowPersonLicenseHistory.Click += new System.EventHandler(this.ShowPersonLicenseHistory_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD.Properties.Resources.International_32;
            this.pictureBox2.Location = new System.Drawing.Point(529, 68);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 40);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 28;
            this.pictureBox2.TabStop = false;
            // 
            // frmListInternationalLicenseApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(871, 617);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.cbIsActive);
            this.Controls.Add(this.btnAddNewInternationalLicenseApplication);
            this.Controls.Add(this.txtSearchInput);
            this.Controls.Add(this.cbFilter);
            this.Controls.Add(this.lblRecord);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dgvInternationalLicenseApplicationsList);
            this.Name = "frmListInternationalLicenseApplications";
            this.Text = "International License Applications";
            this.Load += new System.EventHandler(this.frmListInternationalLicenseApplications_Load);
            this.Controls.SetChildIndex(this.btcClose, 0);
            this.Controls.SetChildIndex(this.dgvInternationalLicenseApplicationsList, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.lblRecord, 0);
            this.Controls.SetChildIndex(this.cbFilter, 0);
            this.Controls.SetChildIndex(this.txtSearchInput, 0);
            this.Controls.SetChildIndex(this.btnAddNewInternationalLicenseApplication, 0);
            this.Controls.SetChildIndex(this.cbIsActive, 0);
            this.Controls.SetChildIndex(this.pictureBox2, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicenseApplicationsList)).EndInit();
            this.cmsInternationalLicenseApplicationOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbIsActive;
        private System.Windows.Forms.Button btnAddNewInternationalLicenseApplication;
        private System.Windows.Forms.TextBox txtSearchInput;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.Label lblRecord;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dgvInternationalLicenseApplicationsList;
        private System.Windows.Forms.ContextMenuStrip cmsInternationalLicenseApplicationOptions;
        private System.Windows.Forms.ToolStripMenuItem ShowPersonDetails;
        private System.Windows.Forms.ToolStripMenuItem ShowLicenseDetails;
        private System.Windows.Forms.ToolStripMenuItem ShowPersonLicenseHistory;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}
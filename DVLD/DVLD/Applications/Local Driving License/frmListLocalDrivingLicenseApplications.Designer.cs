namespace DVLD.Applications.Local_Driving_License
{
    partial class frmListLocalDrivingLicenseApplications
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
            this.CancelApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.DeleteApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.EditApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ShowApplicationDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.SechduleTests = new System.Windows.Forms.ToolStripMenuItem();
            this.SechduleVisionTest = new System.Windows.Forms.ToolStripMenuItem();
            this.SechduleWrittenTest = new System.Windows.Forms.ToolStripMenuItem();
            this.SechduleDrivingTest = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsManageLocalLicenseApplicationOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.IssueLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.ShowLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.ShowPersonLicenseHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAddNewLocalLicenseApplication = new System.Windows.Forms.Button();
            this.txtSearchInput = new System.Windows.Forms.TextBox();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.lblRecord = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dgvLocalLicenseApplicationsList = new System.Windows.Forms.DataGridView();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.cmsManageLocalLicenseApplicationOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicenseApplicationsList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // btcClose
            // 
            this.btcClose.Location = new System.Drawing.Point(985, 575);
            // 
            // CancelApplication
            // 
            this.CancelApplication.Image = global::DVLD.Properties.Resources.Delete_32;
            this.CancelApplication.Name = "CancelApplication";
            this.CancelApplication.Size = new System.Drawing.Size(245, 22);
            this.CancelApplication.Text = "Cancel Application";
            this.CancelApplication.Click += new System.EventHandler(this.CancelApplication_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(242, 6);
            // 
            // DeleteApplication
            // 
            this.DeleteApplication.Image = global::DVLD.Properties.Resources.Delete_32_2;
            this.DeleteApplication.Name = "DeleteApplication";
            this.DeleteApplication.Size = new System.Drawing.Size(245, 22);
            this.DeleteApplication.Text = "Delete Application";
            this.DeleteApplication.Click += new System.EventHandler(this.DeleteApplication_Click);
            // 
            // EditApplication
            // 
            this.EditApplication.Image = global::DVLD.Properties.Resources.edit_32;
            this.EditApplication.Name = "EditApplication";
            this.EditApplication.Size = new System.Drawing.Size(245, 22);
            this.EditApplication.Text = "Edit Application";
            this.EditApplication.Click += new System.EventHandler(this.EditApplication_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(242, 6);
            // 
            // ShowApplicationDetails
            // 
            this.ShowApplicationDetails.Image = global::DVLD.Properties.Resources.PersonDetails_32;
            this.ShowApplicationDetails.Name = "ShowApplicationDetails";
            this.ShowApplicationDetails.Size = new System.Drawing.Size(245, 22);
            this.ShowApplicationDetails.Text = "Show Application Details";
            this.ShowApplicationDetails.Click += new System.EventHandler(this.ShowApplicationDetails_Click);
            // 
            // SechduleTests
            // 
            this.SechduleTests.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SechduleVisionTest,
            this.SechduleWrittenTest,
            this.SechduleDrivingTest});
            this.SechduleTests.Image = global::DVLD.Properties.Resources.Schedule_Test_32;
            this.SechduleTests.Name = "SechduleTests";
            this.SechduleTests.Size = new System.Drawing.Size(245, 22);
            this.SechduleTests.Text = "Sechdule Tests";
            // 
            // SechduleVisionTest
            // 
            this.SechduleVisionTest.Image = global::DVLD.Properties.Resources.Vision_Test_32;
            this.SechduleVisionTest.Name = "SechduleVisionTest";
            this.SechduleVisionTest.Size = new System.Drawing.Size(187, 22);
            this.SechduleVisionTest.Text = "Sechdule Vision Test";
            this.SechduleVisionTest.Click += new System.EventHandler(this.SechduleVisionTest_Click);
            // 
            // SechduleWrittenTest
            // 
            this.SechduleWrittenTest.Image = global::DVLD.Properties.Resources.Written_Test_32;
            this.SechduleWrittenTest.Name = "SechduleWrittenTest";
            this.SechduleWrittenTest.Size = new System.Drawing.Size(187, 22);
            this.SechduleWrittenTest.Text = "Sechdule Written Test";
            this.SechduleWrittenTest.Click += new System.EventHandler(this.SechduleWrittenTest_Click);
            // 
            // SechduleDrivingTest
            // 
            this.SechduleDrivingTest.Image = global::DVLD.Properties.Resources.Street_Test_32;
            this.SechduleDrivingTest.Name = "SechduleDrivingTest";
            this.SechduleDrivingTest.Size = new System.Drawing.Size(187, 22);
            this.SechduleDrivingTest.Text = "Sechdule Street Test";
            this.SechduleDrivingTest.Click += new System.EventHandler(this.SechduleStreetTest_Click);
            // 
            // cmsManageLocalLicenseApplicationOptions
            // 
            this.cmsManageLocalLicenseApplicationOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowApplicationDetails,
            this.toolStripSeparator1,
            this.EditApplication,
            this.DeleteApplication,
            this.toolStripSeparator2,
            this.CancelApplication,
            this.toolStripSeparator3,
            this.SechduleTests,
            this.toolStripSeparator4,
            this.IssueLicense,
            this.toolStripSeparator5,
            this.ShowLicense,
            this.toolStripSeparator6,
            this.ShowPersonLicenseHistory});
            this.cmsManageLocalLicenseApplicationOptions.Name = "contextMenuStrip1";
            this.cmsManageLocalLicenseApplicationOptions.Size = new System.Drawing.Size(246, 216);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(242, 6);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(242, 6);
            // 
            // IssueLicense
            // 
            this.IssueLicense.Image = global::DVLD.Properties.Resources.IssueDrivingLicense_32;
            this.IssueLicense.Name = "IssueLicense";
            this.IssueLicense.Size = new System.Drawing.Size(245, 22);
            this.IssueLicense.Text = "Issue Driving License (First Time)";
            this.IssueLicense.Click += new System.EventHandler(this.IssueLicense_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(242, 6);
            // 
            // ShowLicense
            // 
            this.ShowLicense.Image = global::DVLD.Properties.Resources.License_View_32;
            this.ShowLicense.Name = "ShowLicense";
            this.ShowLicense.Size = new System.Drawing.Size(245, 22);
            this.ShowLicense.Text = "Show License";
            this.ShowLicense.Click += new System.EventHandler(this.ShowLicense_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(242, 6);
            // 
            // ShowPersonLicenseHistory
            // 
            this.ShowPersonLicenseHistory.Image = global::DVLD.Properties.Resources.PersonLicenseHistory_32;
            this.ShowPersonLicenseHistory.Name = "ShowPersonLicenseHistory";
            this.ShowPersonLicenseHistory.Size = new System.Drawing.Size(245, 22);
            this.ShowPersonLicenseHistory.Text = "Show Person License History";
            this.ShowPersonLicenseHistory.Click += new System.EventHandler(this.ShowPersonLicenseHistory_Click);
            // 
            // btnAddNewLocalLicenseApplication
            // 
            this.btnAddNewLocalLicenseApplication.AllowDrop = true;
            this.btnAddNewLocalLicenseApplication.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddNewLocalLicenseApplication.BackgroundImage = global::DVLD.Properties.Resources.New_Application_64;
            this.btnAddNewLocalLicenseApplication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddNewLocalLicenseApplication.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewLocalLicenseApplication.Location = new System.Drawing.Point(1038, 222);
            this.btnAddNewLocalLicenseApplication.Name = "btnAddNewLocalLicenseApplication";
            this.btnAddNewLocalLicenseApplication.Size = new System.Drawing.Size(45, 45);
            this.btnAddNewLocalLicenseApplication.TabIndex = 9;
            this.btnAddNewLocalLicenseApplication.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddNewLocalLicenseApplication.UseVisualStyleBackColor = true;
            this.btnAddNewLocalLicenseApplication.Click += new System.EventHandler(this.btnAddNewLocalLicenseApplication_Click);
            // 
            // txtSearchInput
            // 
            this.txtSearchInput.Font = new System.Drawing.Font("Tahoma", 10F);
            this.txtSearchInput.Location = new System.Drawing.Point(245, 238);
            this.txtSearchInput.Name = "txtSearchInput";
            this.txtSearchInput.Size = new System.Drawing.Size(180, 24);
            this.txtSearchInput.TabIndex = 13;
            this.txtSearchInput.Visible = false;
            this.txtSearchInput.TextChanged += new System.EventHandler(this.txtSearchInput_TextChanged);
            this.txtSearchInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchInput_KeyPress);
            // 
            // cbFilter
            // 
            this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Location = new System.Drawing.Point(86, 240);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(136, 21);
            this.cbFilter.TabIndex = 11;
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            // 
            // lblRecord
            // 
            this.lblRecord.AutoSize = true;
            this.lblRecord.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecord.Location = new System.Drawing.Point(91, 575);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(18, 19);
            this.lblRecord.TabIndex = 15;
            this.lblRecord.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 575);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 19);
            this.label3.TabIndex = 14;
            this.label3.Text = "# Records: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 19);
            this.label2.TabIndex = 12;
            this.label2.Text = "Filter By:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Firebrick;
            this.label1.Location = new System.Drawing.Point(284, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(527, 36);
            this.label1.TabIndex = 10;
            this.label1.Text = "Local Driving License Applciations";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD.Properties.Resources.Applications;
            this.pictureBox1.Location = new System.Drawing.Point(454, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(150, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // dgvLocalLicenseApplicationsList
            // 
            this.dgvLocalLicenseApplicationsList.AllowUserToAddRows = false;
            this.dgvLocalLicenseApplicationsList.AllowUserToDeleteRows = false;
            this.dgvLocalLicenseApplicationsList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dgvLocalLicenseApplicationsList.BackgroundColor = System.Drawing.Color.White;
            this.dgvLocalLicenseApplicationsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalLicenseApplicationsList.ContextMenuStrip = this.cmsManageLocalLicenseApplicationOptions;
            this.dgvLocalLicenseApplicationsList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLocalLicenseApplicationsList.Location = new System.Drawing.Point(2, 273);
            this.dgvLocalLicenseApplicationsList.MultiSelect = false;
            this.dgvLocalLicenseApplicationsList.Name = "dgvLocalLicenseApplicationsList";
            this.dgvLocalLicenseApplicationsList.ReadOnly = true;
            this.dgvLocalLicenseApplicationsList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.dgvLocalLicenseApplicationsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLocalLicenseApplicationsList.Size = new System.Drawing.Size(1081, 293);
            this.dgvLocalLicenseApplicationsList.TabIndex = 16;
            this.dgvLocalLicenseApplicationsList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvLocalLicenseApplicationsList_MouseDown);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD.Properties.Resources.Local_32;
            this.pictureBox2.Location = new System.Drawing.Point(589, 69);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 40);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Location = new System.Drawing.Point(245, 240);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(136, 21);
            this.cbStatus.TabIndex = 18;
            this.cbStatus.Visible = false;
            this.cbStatus.SelectedIndexChanged += new System.EventHandler(this.cbStatus_SelectedIndexChanged);
            // 
            // frmListLocalDrivingLicenseApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(1095, 617);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnAddNewLocalLicenseApplication);
            this.Controls.Add(this.txtSearchInput);
            this.Controls.Add(this.cbFilter);
            this.Controls.Add(this.lblRecord);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dgvLocalLicenseApplicationsList);
            this.Name = "frmListLocalDrivingLicenseApplications";
            this.Text = "Local Driving License Applications";
            this.Load += new System.EventHandler(this.frmListLocalDrivingLicenseApplications_Load);
            this.Controls.SetChildIndex(this.dgvLocalLicenseApplicationsList, 0);
            this.Controls.SetChildIndex(this.btcClose, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.lblRecord, 0);
            this.Controls.SetChildIndex(this.cbFilter, 0);
            this.Controls.SetChildIndex(this.txtSearchInput, 0);
            this.Controls.SetChildIndex(this.btnAddNewLocalLicenseApplication, 0);
            this.Controls.SetChildIndex(this.pictureBox2, 0);
            this.Controls.SetChildIndex(this.cbStatus, 0);
            this.cmsManageLocalLicenseApplicationOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicenseApplicationsList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAddNewLocalLicenseApplication;
        private System.Windows.Forms.TextBox txtSearchInput;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.Label lblRecord;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dgvLocalLicenseApplicationsList;
        private System.Windows.Forms.ContextMenuStrip cmsManageLocalLicenseApplicationOptions;
        private System.Windows.Forms.ToolStripMenuItem ShowApplicationDetails;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem EditApplication;
        private System.Windows.Forms.ToolStripMenuItem DeleteApplication;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem CancelApplication;
        private System.Windows.Forms.ToolStripMenuItem SechduleTests;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem IssueLicense;
        private System.Windows.Forms.ToolStripMenuItem ShowLicense;
        private System.Windows.Forms.ToolStripMenuItem ShowPersonLicenseHistory;
        private System.Windows.Forms.ToolStripMenuItem SechduleVisionTest;
        private System.Windows.Forms.ToolStripMenuItem SechduleWrittenTest;
        private System.Windows.Forms.ToolStripMenuItem SechduleDrivingTest;
        private System.Windows.Forms.ComboBox cbStatus;
    }
}

namespace DVLD.Licenses.Local_Licenses.Controls
{
    partial class ctrlDriverLocalLicenseInfoWithFilter
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
            this.btnFind = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.grbSearch = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlDriverLocalLicenseInfo1 = new DVLD.Licenses.Local_Licenses.Controls.ctrlDriverLocalLicenseInfo();
            this.grbSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFind
            // 
            this.btnFind.AutoSize = true;
            this.btnFind.BackgroundImage = global::DVLD.Properties.Resources.License_View_32;
            this.btnFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFind.Location = new System.Drawing.Point(269, 17);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(35, 35);
            this.btnFind.TabIndex = 1;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(107, 23);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(144, 22);
            this.txtFilter.TabIndex = 2;
            this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilter_KeyPress);
            // 
            // grbFilter
            // 
            this.grbSearch.Controls.Add(this.label1);
            this.grbSearch.Controls.Add(this.btnFind);
            this.grbSearch.Controls.Add(this.txtFilter);
            this.grbSearch.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.grbSearch.Location = new System.Drawing.Point(9, 3);
            this.grbSearch.Name = "grbFilter";
            this.grbSearch.Size = new System.Drawing.Size(340, 63);
            this.grbSearch.TabIndex = 3;
            this.grbSearch.TabStop = false;
            this.grbSearch.Text = "Filter";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "License ID:";
            // 
            // ctrlDriverLocalLicenseInfo1
            // 
            this.ctrlDriverLocalLicenseInfo1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlDriverLocalLicenseInfo1.Location = new System.Drawing.Point(5, 66);
            this.ctrlDriverLocalLicenseInfo1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ctrlDriverLocalLicenseInfo1.Name = "ctrlDriverLocalLicenseInfo1";
            this.ctrlDriverLocalLicenseInfo1.Size = new System.Drawing.Size(816, 273);
            this.ctrlDriverLocalLicenseInfo1.TabIndex = 0;
            // 
            // ctrlDriverLocalLicenseInfoWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.grbSearch);
            this.Controls.Add(this.ctrlDriverLocalLicenseInfo1);
            this.Name = "ctrlDriverLocalLicenseInfoWithFilter";
            this.Size = new System.Drawing.Size(825, 340);
            this.grbSearch.ResumeLayout(false);
            this.grbSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlDriverLocalLicenseInfo ctrlDriverLocalLicenseInfo1;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.GroupBox grbSearch;
        private System.Windows.Forms.Label label1;
    }
}

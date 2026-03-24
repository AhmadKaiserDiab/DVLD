namespace DVLD
{
    partial class frmBaseForm
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
            this.btcClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btcClose
            // 
            this.btcClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btcClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btcClose.Font = new System.Drawing.Font("Tahoma", 10F);
            this.btcClose.Image = global::DVLD.Properties.Resources.Close_32;
            this.btcClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btcClose.Location = new System.Drawing.Point(717, 434);
            this.btcClose.Name = "btcClose";
            this.btcClose.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.btcClose.Size = new System.Drawing.Size(98, 33);
            this.btcClose.TabIndex = 0;
            this.btcClose.Text = "Close";
            this.btcClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btcClose.UseVisualStyleBackColor = true;
            this.btcClose.Click += new System.EventHandler(this.btcClose_Click);
            // 
            // frmBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btcClose;
            this.ClientSize = new System.Drawing.Size(827, 479);
            this.Controls.Add(this.btcClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmBaseForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmBaseForm";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Button btcClose;
    }
}
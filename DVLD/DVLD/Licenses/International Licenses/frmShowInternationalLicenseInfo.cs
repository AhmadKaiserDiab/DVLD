using System;
using DVLD_Business_Layer;
using System.Windows.Forms;

namespace DVLD.Licenses.International_Licenses
{
    public partial class frmShowInternationalLicenseInfo : DVLD.frmBaseForm
    {
        private int LicenseID;

        public frmShowInternationalLicenseInfo(int LicenseID)
        {
            InitializeComponent();
            this.LicenseID = LicenseID;
        }
        
        private void frmShowInternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            clsInternationalLicense License = clsInternationalLicense.Find(LicenseID);

            if (License != null)
            {
                ctrlDriverInternationalLicenseInfo1.Load(License);
            }
            else
            {
                MessageBox.Show(
                    text: $"License With ID = {LicenseID} Not Found!!",
                    caption: "Error",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error);
            }
        }
    }
}
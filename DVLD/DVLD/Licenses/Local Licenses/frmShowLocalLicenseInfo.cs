using System;
using DVLD_Business_Layer;

namespace DVLD.Licenses.Local_Licenses
{
    public partial class frmShowLocalLicenseInfo : DVLD.frmBaseForm
    {
        private int LicenseID;
        private clsLicense NewLicense;

        public frmShowLocalLicenseInfo(int LicenseID)
        {
            InitializeComponent();
            this.LicenseID = LicenseID;
        }

        public frmShowLocalLicenseInfo(clsLicense NewLicense)
        {
            InitializeComponent();
            this.NewLicense = NewLicense;
        }

        private void frmShowLocalLicenseInfo_Load(object sender, EventArgs e)
        {
            if (NewLicense == null)
            {
                NewLicense = clsLicense.Find(LicenseID);
            }
            ctrlDriverLocalLicenseInfo1.Load(NewLicense);
        }
    }
}

using System.Windows.Forms;
using DVLD_Business_Layer;
using DVLD.Licenses.Local_Licenses;

namespace DVLD.Applications.Local_Driving_License
{
    public partial class ctrlDrivingLicenseApplicationInfo : UserControl
    {
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public ctrlDrivingLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int LicenseID = _LocalDrivingLicenseApplication.GetLicenseID();
            frmShowLocalLicenseInfo frm = new frmShowLocalLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void _LoadApplicationData()
        {
            lblDLAppID.Text = this._LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblLicenseClassName.Text = this._LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblPassedTests.Text = _LocalDrivingLicenseApplication.GetPassedTestsCount().ToString() + "/3";
            ctrlApplicationBasicInfo1.Load(this._LocalDrivingLicenseApplication);

            llShowLicenseInfo.Enabled = this._LocalDrivingLicenseApplication.StatusText == "Completed";
        }

        private void _ResetLocalDrivingLicenseApplicationInfo()
        {
            _LocalDrivingLicenseApplication = null;
            ctrlApplicationBasicInfo1.Load(null);
            lblDLAppID.Text = "[????]";
            lblLicenseClassName.Text = "[????]";
            lblPassedTests.Text = "0/3";
        }

        public new void Load(int LocalDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();
                MessageBox.Show($"Application with ApplicationID = {LocalDrivingLicenseApplicationID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadApplicationData();
        }

        public new void Load(clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication)
        {
            _LocalDrivingLicenseApplication = LocalDrivingLicenseApplication;
            if (_LocalDrivingLicenseApplication == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();
                MessageBox.Show("Application Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadApplicationData();
        }

    }
}

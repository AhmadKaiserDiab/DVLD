using System;
using System.Windows.Forms;
using DVLD.Global_Classes;
using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
using DVLD_Business_Layer;

namespace DVLD.Applications.Release_Detained_License
{
    public partial class frmReleaseDetainedLicenseApplication : frmBaseForm
    {
        int _LicenseID = -1;
        clsDetainedLicense _DetainedLicense;
        static float ApplicationFees = clsApplicationType.FindApplicationType((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).Fees;

        public frmReleaseDetainedLicenseApplication()
        {
            InitializeComponent();
            ctrlDriverLocalLicenseInfoWithFilter1.OnLicenseFound += _IsLicenseFound;
        }

        public frmReleaseDetainedLicenseApplication(int LicenseID)
        {
            InitializeComponent();
            this._LicenseID = LicenseID;
        }

        private void frmReleaseDetainedLicenseApplication_Load(object sender, EventArgs e)
        {
            _SetDefaultValues();
            if (_LicenseID != -1)
            {
                ctrlDriverLocalLicenseInfoWithFilter1.LoadLicenseData(_LicenseID);
                ctrlDriverLocalLicenseInfoWithFilter1.EnableSearch = false;
                _IsLicenseFound(true);
            }
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLocalLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLocalLicenseInfo frm = new frmShowLocalLicenseInfo(ctrlDriverLocalLicenseInfoWithFilter1.SelectedLicenseID);
            frm.ShowDialog();
        }
   
        private void btnRelease_Click(object sender, EventArgs e)
        {
            clsLicense SelectedLicense = ctrlDriverLocalLicenseInfoWithFilter1.SelectedLicenseInfo;
            
            if (!SelectedLicense.IsActive)
            {
                MessageBox.Show(
                       text: "Can't Release The Detain for This License,\n The Selected License Not Active!!",
                       caption: "Error",
                       icon: MessageBoxIcon.Error,
                       buttons: MessageBoxButtons.OK);
                return;
            }

            if (!SelectedLicense.IsDetained)
            {
                MessageBox.Show(
                    text: "Selected License Is Not Detained\nChoose Another One",
                    caption: "Not Allowed",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error
                    );
                return;
            }

            if (_DetainedLicense.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID))
            {
                MessageBox.Show(
                    text: $"Detained License Released Successfully ",
                    caption: "Done",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Information);

                frmReleaseDetainedLicenseApplication_Load(null,null);
                btnRelease.Enabled = false;
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void _IsLicenseFound(bool isLicenseFound)
        {
            _LicenseID = ctrlDriverLocalLicenseInfoWithFilter1.SelectedLicenseID;

            lblLicenseID.Text = _LicenseID.ToString();
            llShowLicenseInfo.Enabled = isLicenseFound;
            llShowLicensesHistory.Enabled = isLicenseFound;
            btnRelease.Enabled = isLicenseFound;

            if (isLicenseFound)
            {
                _LoadDetainedLicenseData();
            }
            else
            {
                _SetDefaultValues();
            }
        }

        private void _SetDefaultValues()
        {
            lblDetainDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblApplicationFees.Text = ApplicationFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblDetainID.Text = "[?????]";
            lblTotalFees.Text = "[$$$$]";
            lblLicenseID.Text = (_LicenseID == -1) ? "No License": _LicenseID.ToString();
            lblFineFees.Text = "[$$$$]";
            lblApplicationID.Text = "[?????]";
        }

        private void _LoadDetainedLicenseData()
        {
            _DetainedLicense = clsDetainedLicense.FindByLicenseID(_LicenseID);
            lblDetainID.Text = _DetainedLicense.DetainID.ToString();
            lblDetainDate.Text = clsFormat.DateToShort(_DetainedLicense.DetainDate);
            lblFineFees.Text = _DetainedLicense.FineFees.ToString();
            lblTotalFees.Text = (ApplicationFees + _DetainedLicense.FineFees).ToString();
            lblApplicationID.Text = _DetainedLicense.ReleaseApplicationID == -1 ? "" : _DetainedLicense.ReleaseApplicationID.ToString();
        }

    }
}
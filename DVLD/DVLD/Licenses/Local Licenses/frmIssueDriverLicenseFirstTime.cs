using System;
using System.Windows.Forms;
using DVLD_Business_Layer;
using DVLD.Global_Classes;

namespace DVLD.Licenses.Local_Licenses
{
    public partial class frmIssueDriverLicenseFirstTime : DVLD.frmBaseForm
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();

        public frmIssueDriverLicenseFirstTime()
        {
            InitializeComponent();
        }

        public frmIssueDriverLicenseFirstTime(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void frmIssueDriverLicenseFirstTime_Load(object sender, EventArgs e)
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication != null)
            {
                ctrlDrivingLicenseApplicationInfo1.Load(_LocalDrivingLicenseApplication);
                btnIssue.Enabled = true;
            }
            else
            {
                MessageBox.Show("Application Not Found");
                return;
            }
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            string Notes = txtNotes.Text.Trim();
            int CreatedByUserID = clsGlobal.CurrentUser.UserID;
            int NewLicenseID = _LocalDrivingLicenseApplication.IssueLicenseForFirstTime(Notes, CreatedByUserID);
            if (NewLicenseID != -1)
            {
                MessageBox.Show(
                    text: $"New License Issued With ID = {NewLicenseID}",
                    caption: "Success",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Information
                    );
                frmIssueDriverLicenseFirstTime_Load(null,null);
                IsDataUpdated();
                btnIssue.Enabled = false;
            }
            else
            {
                MessageBox.Show(
                    text: $"New License Issued With ID = {NewLicenseID}",
                    caption: "Success",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Information
                    );
            }
        }
        
    }
}

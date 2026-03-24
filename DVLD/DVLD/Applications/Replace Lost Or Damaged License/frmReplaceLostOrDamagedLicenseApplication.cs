using DVLD.Global_Classes;
using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
using DVLD_Business_Layer;
using System;
using System.Windows.Forms;

namespace DVLD.Applications.Replace_Lost_Or_Damaged_License
{
    public partial class frmReplaceLostOrDamagedLicenseApplication : frmBaseForm
    {
        clsLicense NewLicense;

        public frmReplaceLostOrDamagedLicenseApplication()
        {
            InitializeComponent();
            ctrlDriverLocalLicenseInfoWithFilter1.OnLicenseFound += _IsLicenseFound;
        }

        private void frmReplaceLostOrDamagedLicenseApplication_Load(object sender, EventArgs e)
        {
            rbDamagedLicense.Checked = true;
            rbDamagedLicense_CheckedChanged(null,null);
            _SetDefaultValues();
        }
       
        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDamagedLicense.Checked)
            {
                this.Text = lblTitle.Text = "Replacement For Damaged License";
                lblApplicationFees.Text = clsApplicationType.FindApplicationType((int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense).Fees.ToString();
            }
            else
            {
                this.Text = lblTitle.Text = "Replacement For Lost License";
                lblApplicationFees.Text = clsApplicationType.FindApplicationType((int)clsApplication.enApplicationType.ReplaceLostDrivingLicense).Fees.ToString();
            }
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLocalLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLocalLicenseInfo frm = new frmShowLocalLicenseInfo(NewLicense);
            frm.ShowDialog();
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            clsLicense OldLicense = ctrlDriverLocalLicenseInfoWithFilter1.SelectedLicenseInfo;

            if (!OldLicense.IsActive)
            {
                MessageBox.Show(
                       text: $"Can't Issue a Replacement For Expired License",
                       caption: "Not Allowed",
                       icon: MessageBoxIcon.Error,
                       buttons: MessageBoxButtons.OK);
                return;
            }

            if (OldLicense.IsDetained)
            {
                MessageBox.Show(
                       text: $"Can't Issue a Replacement For Detained License",
                       caption: "Not Allowed",
                       icon: MessageBoxIcon.Error,
                       buttons: MessageBoxButtons.OK);
                return;
            }

            


            if (MessageBox.Show(
                       text: "Are You Sure You Want To Issue a Replacement \n for This License ??",
                       caption: "Confirm",
                       icon: MessageBoxIcon.Question,
                       buttons: MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (rbDamagedLicense.Checked)
                {
                    NewLicense = OldLicense.ReplaceDamagedLicense(clsGlobal.CurrentUser.UserID);
                }
                else
                {
                    NewLicense = OldLicense.ReplaceLostLicense(clsGlobal.CurrentUser.UserID);
                }

                if (NewLicense != null)
                {
                    MessageBox.Show(
                           text: "License Renewed Successfully!!",
                           caption: "Success",
                           icon: MessageBoxIcon.Information,
                           buttons: MessageBoxButtons.OK);

                    ctrlDriverLocalLicenseInfoWithFilter1.EnableSearch = false;
                    llShowNewLicenseInfo.Enabled = true;
                    btnIssueReplacement.Enabled = false;

                    lblNewLicenseID.Text = NewLicense.LicenseID.ToString();
                    lblLicenseReplacementApplicationID.Text = NewLicense.ApplicationID.ToString();
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void _IsLicenseFound(bool isLicenseFound)
        {
            llShowLicensesHistory.Enabled = isLicenseFound;
            btnIssueReplacement.Enabled = isLicenseFound;
            if (isLicenseFound)
            {
                lblOldLicenseID.Text = ctrlDriverLocalLicenseInfoWithFilter1.SelectedLicenseID.ToString();
            }
            else
            {
                _SetDefaultValues();
            }
        }

        private void _SetDefaultValues()
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblOldLicenseID.Text = "No License";
        }

    }
}

using System;
using System.Windows.Forms;
using DVLD.Licenses;
using DVLD.Global_Classes;
using DVLD.Licenses.Local_Licenses;
using DVLD_Business_Layer;

namespace DVLD.Applications.Renew_Local_License
{
    public partial class frmRenewLocalDrivingLicenseApplication : DVLD.frmBaseForm
    {
        static float ApplicationFees = clsApplicationType.FindApplicationType((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees;
        clsLicense NewLicense;

        public frmRenewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            ctrlDriverLocalLicenseInfoWithFilter1.OnLicenseFound += _IsLicenseFound;
        }

        private void frmRenewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
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

        private void btnRenew_Click(object sender, EventArgs e)
        {
            clsLicense OldLicense = ctrlDriverLocalLicenseInfoWithFilter1.SelectedLicenseInfo;
            int LicenseID = clsLicense.GetLicenseID(OldLicense.DriverInfo.PersonID, OldLicense.LicenseClassID);

            if (LicenseID != -1  && 
                LicenseID != OldLicense.LicenseID)
            {
                MessageBox.Show(
                       text: $"This Driver Already Had A newer License with ID = {LicenseID}\n Please Choose Anotyher License",
                       caption: "Not Allowed",
                       icon: MessageBoxIcon.Error,
                       buttons: MessageBoxButtons.OK);
                return;
            }


            if (OldLicense.IsDetained)
            {
                MessageBox.Show(
                       text: $"Can't Renew a Detained License",
                       caption: "Not Allowed",
                       icon: MessageBoxIcon.Error,
                       buttons: MessageBoxButtons.OK);
                return;
            }

            if (OldLicense.ExpirationDate >= DateTime.Now)
            {
                MessageBox.Show(
                       text: $"Selected License Is Not Expired Yet, it Will Expire On \n {clsFormat.DateToShort(OldLicense.ExpirationDate)}",
                       caption: "Not Allowed",
                       icon: MessageBoxIcon.Error,
                       buttons: MessageBoxButtons.OK);
                return;
            }


            if (MessageBox.Show(
                       text: "Are You Sure You Want To Renew This License ??",
                       caption: "Confirm",
                       icon: MessageBoxIcon.Question,
                       buttons: MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                NewLicense = OldLicense.RenewLicense(txtNotes.Text.Trim(), clsGlobal.CurrentUser.UserID);
                if (NewLicense != null)
                {
                    MessageBox.Show(
                           text: "License Renewed Successfully!!",
                           caption: "Success",
                           icon: MessageBoxIcon.Information,
                           buttons: MessageBoxButtons.OK);

                    ctrlDriverLocalLicenseInfoWithFilter1.EnableSearch = false;
                    llShowNewLicenseInfo.Enabled = true;
                    btnRenew.Enabled = false;

                    lblRenewedLicenseID.Text = NewLicense.LicenseID.ToString();
                    lblRenewLicenseApplicationID.Text = NewLicense.ApplicationID.ToString();
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
            btnRenew.Enabled = isLicenseFound;
            if (isLicenseFound)
            {
                lblLicenseFees.Text = ctrlDriverLocalLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassInfo.ClassFees.ToString();
                lblOldLicenseID.Text = ctrlDriverLocalLicenseInfoWithFilter1.SelectedLicenseID.ToString();
                lblTotalFees.Text = (ctrlDriverLocalLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassInfo.ClassFees + ApplicationFees).ToString();
            }
            else
            {
                _ResetDefaultValues();
            }
        }

        private void _ResetDefaultValues()
        {
            lblRenewLicenseApplicationID.Text = "[???]";
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblApplicationFees.Text = ApplicationFees.ToString();
            lblLicenseFees.Text = "[$$$]";
            lblRenewedLicenseID.Text = "[???]";
            lblOldLicenseID.Text = "[???]";
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now); ;
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblTotalFees.Text = "[$$$]";

        }
    }
}
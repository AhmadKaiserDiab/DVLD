using System;
using System.Windows.Forms;
using DVLD.Licenses;
using DVLD.Global_Classes;
using DVLD.Licenses.International_Licenses;
using DVLD_Business_Layer;

namespace DVLD.Applications.International_License
{
    public partial class frmNewInternationalLicenseApplication : DVLD.frmBaseForm
    {
        int _LicenseID = -1;
        clsInternationalLicense _License = new clsInternationalLicense();

        public frmNewInternationalLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmNewInternationalLicenseApplication_Load(object sender, EventArgs e)
        {
            ctrlDriverLocalLicenseInfoWithFilter1.OnLicenseFound += IsLicenseFound;
            _SetDefaultValues();
            
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            clsLicense SelectedLicense = ctrlDriverLocalLicenseInfoWithFilter1.SelectedLicenseInfo;
            int ActiveLicenseID = clsInternationalLicense.GetActiveLicenseID(ctrlDriverLocalLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID);

            if (ActiveLicenseID != -1)
            {
                MessageBox.Show(
                       text: $"Driver Already Had An Active License With ID = {ActiveLicenseID}!!",
                       caption: "Error",
                       icon: MessageBoxIcon.Error,
                       buttons: MessageBoxButtons.OK);
                return;
            }

            if (SelectedLicense.LicenseClassID != 3)
            {
                MessageBox.Show(
                       text: "You Can Issue International License Only on Local License From Class 3",
                       caption: "Error",
                       icon: MessageBoxIcon.Error,
                       buttons: MessageBoxButtons.OK);
                return;
            }

            if (!SelectedLicense.IsActive)
            {
                MessageBox.Show(
                       text: "Can't Issue The License, Because The Selected Local License Expired!!",
                       caption: "Error",
                       icon: MessageBoxIcon.Error,
                       buttons: MessageBoxButtons.OK);
                return;
            }

            if (SelectedLicense.IsDetained)
            {
                MessageBox.Show(
                       text: "Can't Issue The License, Because The Selected Local License Is Detained!!",
                       caption: "Error",
                       icon: MessageBoxIcon.Error,
                       buttons: MessageBoxButtons.OK);
                return;
            }

            if (
                MessageBox.Show(
                    text: "Are You Sure You Want To Issue The License?",
                    caption: "Confirm",
                    buttons: MessageBoxButtons.OKCancel,
                    icon: MessageBoxIcon.Question) == DialogResult.OK)
            {
                _SaveLicenseData();
                if (_License.Save())
                {
                    _LicenseID = _License.InternationalLicenseID;

                    MessageBox.Show(
                    text: $"International License Issued Successfully with ID = {_LicenseID}",
                    caption: "License Issued",
                    icon: MessageBoxIcon.Error,
                    buttons: MessageBoxButtons.OK);

                    lblApplicationID.Text = _License.ApplicationID.ToString();
                    lblInternationalLicenseID.Text = _LicenseID.ToString();
                    lblLocalLicenseID.Text = _License.IssuedUsingLocalLicenseID.ToString();

                    btnIssue.Enabled = false;
                    llShowLicenseInfo.Enabled = true;
                    ctrlDriverLocalLicenseInfoWithFilter1.EnableSearch = false;
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(ctrlDriverLocalLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(_LicenseID);
            frm.ShowDialog();
        }

        private void IsLicenseFound(bool result)
        {
            llShowLicensesHistory.Enabled = result;
            btnIssue.Enabled = result;
        }

        private void _SetDefaultValues()
        {
            lblApplicationID.Text = "[???]";
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblIssueDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblFees.Text = clsApplicationType.FindApplicationType((int)clsApplication.enApplicationType.NewInternationalLicense).Fees.ToString();
            lblInternationalLicenseID.Text = "[???]";
            lblLocalLicenseID.Text = "[???]";
            lblExpirationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            llShowLicensesHistory.Enabled = false;
            llShowLicenseInfo.Enabled = false;
            btnIssue.Enabled = false;
        }

        private void _SaveLicenseData()
        {
            _License.DriverID = ctrlDriverLocalLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID;
            _License.IssuedUsingLocalLicenseID = ctrlDriverLocalLicenseInfoWithFilter1.SelectedLicenseID;
            _License.CreatedByUserID = clsGlobal.CurrentUser.UserID;
        }

    }
}
using System;
using System.Windows.Forms;
using DVLD.Licenses.Local_Licenses;
using DVLD_Business_Layer;
using DVLD.Global_Classes;

namespace DVLD.Licenses.Detain_License
{
    public partial class frmDetainLicenseApplication : frmBaseForm
    {
        int _LicenseID = -1;
        clsDetainedLicense _DetainedLicense = new clsDetainedLicense();

        public frmDetainLicenseApplication()
        {
            InitializeComponent();
            ctrlDriverLocalLicenseInfoWithFilter1.OnLicenseFound += _IsLicenseFound;
        }

        private void frmDetainLicenseApplication_Load(object sender, EventArgs e)
        {
            _SetDefaultData();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            clsLicense SelectedLicense = ctrlDriverLocalLicenseInfoWithFilter1.SelectedLicenseInfo;

            if (txtFineFees.Text == "")
            {
                MessageBox.Show(
                       text: "Fine Fees Can't Be Empty!!!",
                       caption: "Error",
                       icon: MessageBoxIcon.Error,
                       buttons: MessageBoxButtons.OK);
                return;
            }

            if (!SelectedLicense.IsActive)
            {
                MessageBox.Show(
                       text: "Can't Detain The License, Because The Selected License Not Active!!",
                       caption: "Error",
                       icon: MessageBoxIcon.Error,
                       buttons: MessageBoxButtons.OK);
                return;
            }

            if (SelectedLicense.IsDetained)
            {
                MessageBox.Show(
                    text: "Selected License Already Detained\nChoose Another One",
                    caption: "Not Allowed",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error
                    );
                return;
            }

            _DetainedLicense.LicenseID = ctrlDriverLocalLicenseInfoWithFilter1.SelectedLicenseID;
            _DetainedLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _DetainedLicense.FineFees = float.Parse(txtFineFees.Text.ToString());

            if (_DetainedLicense.DetainLicense())
            {
                MessageBox.Show(
                    text: $"LicenseDetained With Detain ID = {_DetainedLicense.DetainID}",
                    caption: "Done",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Information);

                lblDetainID.Text = _DetainedLicense.DetainID.ToString();
                txtFineFees.Enabled = false;
                btnDetain.Enabled = false;
                ctrlDriverLocalLicenseInfoWithFilter1.LoadLicenseData(_LicenseID);
                ctrlDriverLocalLicenseInfoWithFilter1.EnableSearch = false;
                IsDataUpdated();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int PersonID = ctrlDriverLocalLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLocalLicenseInfo frm = new frmShowLocalLicenseInfo(_LicenseID);
            frm.ShowDialog();
        }

        private void _IsLicenseFound(bool isLicenseFound)
        {
            _LicenseID = ctrlDriverLocalLicenseInfoWithFilter1.SelectedLicenseID;
            lblLicenseID.Text = _LicenseID == -1 ? "No License " : _LicenseID.ToString();
            llShowLicenseInfo.Enabled = isLicenseFound;
            llShowLicensesHistory.Enabled = isLicenseFound;
            btnDetain.Enabled = isLicenseFound;
        }

        private void _SetDefaultData()
        {
            lblDetainID.Text      = "[?????]";
            lblDetainDate.Text    = clsFormat.DateToShort(DateTime.Now);
            txtFineFees.Text      = "";
            lblLicenseID.Text     = _LicenseID.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName ;
        }

    }
}
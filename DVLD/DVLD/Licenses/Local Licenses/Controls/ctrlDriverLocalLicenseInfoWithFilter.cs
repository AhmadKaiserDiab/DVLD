using System;
using System.Windows.Forms;
using DVLD_Business_Layer;

namespace DVLD.Licenses.Local_Licenses.Controls
{
    public partial class ctrlDriverLocalLicenseInfoWithFilter : UserControl
    {
        public int SelectedLicenseID { get { return ctrlDriverLocalLicenseInfo1.LicenseID; } }
        public clsLicense SelectedLicenseInfo
        {
            get
            {
                return ctrlDriverLocalLicenseInfo1.SelectedLicenseInfo;
            }
        }

        public bool EnableSearch
        {
            set
            {
                grbSearch.Enabled = value;
            }
        }

        public event Action<bool> OnLicenseFound;
        protected virtual void IsLicenseFound(bool result)
        {
            Action<bool> handler = OnLicenseFound;

            if (handler != null)
            {
                handler(result);
            }
        }

        public ctrlDriverLocalLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            _FindLicense();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (13))
            {
                btnFind.PerformClick();
                return;
            }
            e.Handled = !char.IsDigit(e.KeyChar) &&
                        !char.IsControl(e.KeyChar);
        }

        private void _FindLicense()
        {
            if (txtFilter.Text == "")
            {
                MessageBox.Show("Can't Search With Empty Fields");
                return;
            }
            int LicenseID = int.Parse(txtFilter.Text.ToString());
            clsLicense License = clsLicense.Find(LicenseID);
            ctrlDriverLocalLicenseInfo1.Load(License);

            if (License != null)
            {
                IsLicenseFound(true);
            }
            else
            {
                MessageBox.Show(
                    $"There Is No License With ID = {LicenseID}",
                    caption: "Not Found",
                    icon: MessageBoxIcon.Asterisk,
                    buttons: MessageBoxButtons.OK
                    );
                txtFilter.Focus();
                IsLicenseFound(false);

            }
        }

        public void LoadLicenseData(int LicenseID)
        {
            if (LicenseID == -1)
            {
                return;
            }
            IsLicenseFound(true);
            ctrlDriverLocalLicenseInfo1.Load(clsLicense.Find(LicenseID));
            txtFilter.Text = LicenseID.ToString();
            EnableSearch = false;
        }

    }
}
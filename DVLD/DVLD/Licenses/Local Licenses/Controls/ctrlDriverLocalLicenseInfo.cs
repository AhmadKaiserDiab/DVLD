using System.IO;
using System.Windows.Forms;
using DVLD.Global_Classes;
using DVLD_Business_Layer;

namespace DVLD.Licenses.Local_Licenses.Controls
{
    public partial class ctrlDriverLocalLicenseInfo : UserControl
    {
        int _LicenseID = -1;
        clsLicense _License;

        public int LicenseID { get { return _LicenseID; } }
        public clsLicense SelectedLicenseInfo { get { return _License; } }

        public ctrlDriverLocalLicenseInfo()
        {
            InitializeComponent();
        }

        public new void Load(clsLicense License)
        {
            _ResetDefaultValues();
            if (License != null)
            {
                 this._License = License;
                _LoadLicenseInfo();
            }
        }

        private void _ResetDefaultValues()
        {
            _LicenseID             = -1;
            _License               = null;
            lblLicenseClass.Text   = "[?????]";
            lblFullName.Text       = "[?????]";
            lblLicenseID.Text      = "[?????]";
            lblNationalNo.Text     = "[?????]";
            lblGender.Text         = "[?????]";
            lblIssueDate.Text      = "[?????]";
            lblIssueReason.Text    = "[?????]";
            lblNotes.Text          = "[?????]";
            lblIsActive.Text       = "[?????]";
            lblDateofBirth.Text    = "[?????]";
            lblDriverID.Text       = "[?????]";
            lblExpirationDate.Text = "[?????]";
            lblIsDetained.Text     = "[?????]";
            pbGender.Image         = Properties.Resources.Man_32;
            pbPersonImage.Image    = Properties.Resources.Male_512;
        }

        private void _LoadLicenseInfo()
        {
            _LicenseID             = _License.LicenseID;
            lblLicenseClass.Text   = _License.LicenseClassInfo.ClassName;
            lblFullName.Text       = _License.DriverInfo.PersonInfo.FullName;
            lblLicenseID.Text      = _License.LicenseID.ToString();
            lblNationalNo.Text     = _License.DriverInfo.PersonInfo.NationalNumber;
            lblGender.Text         = _License.DriverInfo.PersonInfo.Gender == 0 ? "Male" : "Female";
            lblIssueDate.Text      = _License.IssueDate.ToShortDateString();
            lblIssueReason.Text    = _License.IssueReasonText;
            lblNotes.Text          = _License.Notes == "" ? "No Notes" :_License.Notes ;
            lblIsActive.Text       = _License.IsActive ? "Yes" : "No" ;
            lblDateofBirth.Text    = _License.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text       = _License.DriverInfo.DriverID.ToString();
            lblExpirationDate.Text = _License.ExpirationDate.ToShortDateString();
            lblIsDetained.Text     = _License.IsDetained ? "Yes" : "No";
            pbGender.Image         = _License.DriverInfo.PersonInfo.Gender == 0 ? Properties.Resources.Man_32 : Properties.Resources.Woman_32;
            _LoadDriverImage();
        }

        private void _LoadDriverImage()
        {
            if (_License.DriverInfo.PersonInfo.ImagePath != string.Empty)
            {
                string ImagePath = Path.Combine(clsGlobal.FolderPath, _License.DriverInfo.PersonInfo.ImagePath);
                if (File.Exists(ImagePath))
                {
                    pbPersonImage.ImageLocation = ImagePath.Trim();
                }
            }
            else
            {
                pbPersonImage.Image = (_License.DriverInfo.PersonInfo.Gender == 0) ? Properties.Resources.Male_512
                    : Properties.Resources.Female_512;
            }
        }

    }
}

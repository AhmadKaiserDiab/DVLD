using System.IO;
using System.Windows.Forms;
using DVLD.Global_Classes;
using DVLD_Business_Layer;

namespace DVLD.Licenses.International_Licenses.Controls
{
    public partial class ctrlDriverInternationalLicenseInfo : UserControl
    {
        clsInternationalLicense License;

        public ctrlDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        public new void Load(clsInternationalLicense License)
        {
            this.License = License;
            _ResetDefaultValues();
            if (License != null)
            {
                _LoadLicenseInfo();
            }
        }

        private void _ResetDefaultValues()
        {
            lblFullName.Text               = "[?????]";
            lblInternationalLicenseID.Text = "[?????]";
            lblLicenseID.Text              = "[?????]";
            lblNationalNo.Text             = "[?????]";
            lblGender.Text                 = "[?????]";
            lblIssueDate.Text              = "[?????]";
            lblApplicationID.Text          = "[?????]";
            lblIsActive.Text               = "[?????]";
            lblDateOfBirth.Text            = "[?????]"; 
            lblDriverID.Text               = "[?????]";
            lblExpirationDate.Text         = "[?????]";
            pbGender.Image                 = Properties.Resources.Man_32;
            pbDriverImage.Image            = Properties.Resources.Male_512;
        }

        private void _LoadLicenseInfo()
        {
            lblFullName.Text               = License.DriverInfo.PersonInfo.FirstName;
            lblInternationalLicenseID.Text = License.InternationalLicenseID.ToString();
            lblLicenseID.Text              = License.IssuedUsingLocalLicenseID.ToString();
            lblNationalNo.Text             = License.DriverInfo.PersonInfo.NationalNumber;
            lblGender.Text                 = License.DriverInfo.PersonInfo.Gender == 0 ? "Male" : "Female";
            lblIssueDate.Text              = License.IssueDate.ToShortDateString();
            lblApplicationID.Text          = License.ApplicationID.ToString();
            lblIsActive.Text               = License.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text            = License.DriverInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text               = License.DriverID.ToString();
            lblExpirationDate.Text         = License.ExpirationDate.ToShortDateString();
            pbGender.Image                 = License.DriverInfo.PersonInfo.Gender == 0 ? Properties.Resources.Man_32 : Properties.Resources.Woman_32;
            _LoadDriverImage();
        }

        private void _LoadDriverImage()
        {
            if (License.DriverInfo.PersonInfo.ImagePath != string.Empty)
            {
                string ImagePath = Path.Combine(clsGlobal.FolderPath, License.DriverInfo.PersonInfo.ImagePath);
                if (File.Exists(ImagePath))
                {
                    pbDriverImage.ImageLocation = ImagePath.Trim();
                }
            }
            else
            {
                pbDriverImage.Image = (License.DriverInfo.PersonInfo.Gender == 0) ? Properties.Resources.Male_512
                    : Properties.Resources.Female_512;
            }
        }
    }
}
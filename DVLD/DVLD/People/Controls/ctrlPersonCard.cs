using System;
using System.IO;
using System.Windows.Forms;
using DVLD_Business_Layer;
using DVLD.Global_Classes;

namespace DVLD.People.Controls
{
    public partial class ctrlPersonCard : UserControl
    {
        private clsPerson _Person;
        private int _PersonID = -1;
        public int PersonID { get { return _PersonID; } }
        public clsPerson SelectedPersonInfo { get { return _Person; } }

        public bool EnableEdit { set { llEditPersonInfo.Enabled = value; } }

        public event Action OnDataChanged;
        protected virtual void IsDataChanged()
        {
            Action handler = OnDataChanged;

            if (handler != null)
            {
                handler?.Invoke();
            }
        }

        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);

            if (_Person == null)
            {
                _ResetCard();
                MessageBox.Show($"Person With ID = {PersonID} Not Found");
                return;
            }
            _LoadPersonInfo();
        }

        public void LoadPersonInfo(string NationalNumber)
        {
            _Person = clsPerson.Find(NationalNumber);
            
            if (_Person == null)
            {
                _ResetCard();
                MessageBox.Show($"Person With National Number = {NationalNumber} Not Found");
                return;
            }
            _LoadPersonInfo();
        }

        public void LoadPersonInfo(clsPerson Person)
        {
            if (Person == null)
            {
                _ResetCard();
                MessageBox.Show("Person Not Found");
                return;
            }
                _Person = Person;
                _LoadPersonInfo();   
        }

        private void _LoadPersonInfo()
        {
            _PersonID           = _Person.PersonID;
            lblPersonID.Text    = _Person.PersonID.ToString();
            lblNationalNo.Text  = _Person.NationalNumber;
            lblFullName.Text    = _Person.FullName;
            lblGender.Text      = _Person.Gender == 0 ? "Male" : "Female";
            lblEmail.Text       = _Person.Email;
            lblPhone.Text       = _Person.Phone;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblCountry.Text     = _Person.Country.CountryName;
            lblAddress.Text     = _Person.Address;
            pbGender.Image      = _Person.Gender == 0 ? Properties.Resources.Man_32 : Properties.Resources.Woman_32;
            _LoadProfileImage();
            llEditPersonInfo.Enabled = true;
        }

        private void _ResetCard()
        {
            _PersonID = -1;
            _Person = null;
            lblPersonID.Text    = "[?????]";
            lblFullName.Text    = "[?????]";
            lblNationalNo.Text  = "[?????]";
            lblGender.Text      = "[?????]";
            lblEmail.Text       = "[?????]";
            lblAddress.Text     = "[?????]";
            lblDateOfBirth.Text = "[?????]";
            lblPhone.Text       = "[?????]";
            lblCountry.Text     = "[?????]";
            pbPersonImage.Image = Properties.Resources.Male_512;
            pbGender.Image = Properties.Resources.Man_32;
            llEditPersonInfo.Enabled = false;
        }

        private void _LoadProfileImage()
        {
            if (_Person.ImagePath != string.Empty)
            {
                string ImagePath = Path.Combine(clsGlobal.FolderPath, _Person.ImagePath);

                if (File.Exists(ImagePath))
                {
                    pbPersonImage.ImageLocation = ImagePath.Trim();
                }
            }
            else
            {
                pbPersonImage.Image = (_Person.Gender == 0) ? Properties.Resources.Male_512
                    : Properties.Resources.Female_512;
            }
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson frm  = new frmAddUpdatePerson(_Person);
            frm.ReturnedPersonData += Frm_UpdatedPersonData;
            frm.OnDataUpdated      += Frm_OnDataChanged;
            frm.ShowDialog();
        }

        private void Frm_UpdatedPersonData(object sender, clsPerson Person)
        {
            LoadPersonInfo(Person);
        }

        private void Frm_OnDataChanged()
        {
            if (OnDataChanged != null)
            {
                IsDataChanged();
            }
        }

    }
}
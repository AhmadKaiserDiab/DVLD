using System;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using DVLD.Global_Classes;
using DVLD_Business_Layer;

namespace DVLD.People
{
    public partial class frmAddUpdatePerson : frmBaseForm
    {
        enum enMode { AddNew = 0, Update = 1 };
        enum enGender { Male = 0, FeMale = 1 }

        enMode _Mode;
        private int _PersonID = -1;
        private clsPerson _Person;

        public delegate void SendPersonDataBackEventHandler(object sender, clsPerson Person);
        public event SendPersonDataBackEventHandler ReturnedPersonData;

        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            _Mode = enMode.Update;
        }

        public frmAddUpdatePerson(clsPerson Person)
        {
            InitializeComponent();
            _Person = Person;
            _PersonID = _Person.PersonID;
            _Mode = enMode.Update;
        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadPersonData();
        }

        private void rbGender_CheckedChanged(object sender, EventArgs e)
        {
            if (_Person.ImagePath == "")
            {
                pbPersonImage.Image = (rbMale.Checked) ? Properties.Resources.Male_512
                        : Properties.Resources.Female_512;
            }
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _SetNewImage();
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _RemoveImage();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _SavePersonInfo();

            if (_Person.Save())
            {
                if (_Mode == enMode.AddNew)
                {
                    MessageBox.Show($"Pesron Added Successfully With ID = {_Person.PersonID}", "Done");
                    _Mode = enMode.Update;
                }
                else
                {
                    MessageBox.Show($"Pesron Updated Successfully", "Done");
                }
                 IsDataUpdated();
                _LoadPersonData();
            }
            else
            {
                MessageBox.Show("Save Failed", "Error");
            }
        }

        private void ValidateName(object sender, CancelEventArgs e)
        {
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This Field is Required!");
            }
            else if (!clsValidation.ValidateName(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "Please Enter a Real Name");
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }

        private void txtThirdName_Validating(object sender, CancelEventArgs e)
        {
            if (!clsValidation.ValidateName(txtThirdName.Text.Trim()) &&
                txtThirdName.Text.Trim() != "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtThirdName, "Please Enter a Real Name");
            }
            else
            {
                errorProvider1.SetError(txtThirdName, null);
            }
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This Field Is Required snd Can't Be Empty");
            }
            else if (clsPerson.IsPersonExist(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "This National Number Already Exist");
            }
            else if (!clsValidation.ValidateNationalNumber(txtNationalNo.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNationalNo, "National Number Must be N + Numbers");
            }
            else
            {
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (!clsValidation.ValidateEmail(txtEmail.Text) &&
                        txtEmail.Text.Trim() != "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address Format!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            if (txtPhone.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPhone, "This Can't Be Empty!!");
            }
            else if (!clsValidation.ValidatePhone(txtPhone.Text))
            {
                errorProvider1.SetError(txtPhone, "Invalid Phone Number!");
            }
            else
            {
                errorProvider1.SetError(txtPhone, null);
            }
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)

        {
            //if (!clsValidation.ValidateEmail(txtEmail.Text) &&
            //            txtEmail.Text.Trim() != "")
            if (txtAddress.Text == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Address Can't Be Empty!!");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }
        }

        private void frmAddUpdatePerson_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_Person != null)
            {
                ReturnedPersonData?.Invoke(this, _Person);
            }
        }

        private void _ResetDefualtValues()
        {
            _LoadCountriesInComboBox();

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                _Person = new clsPerson();
            }
            else
            {
                lblTitle.Text = "Update Person";
            }

            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);
            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);

            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
            rbMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            pbPersonImage.Image = Properties.Resources.Male_512;
            llRemoveImage.Visible = false;
            txtAddress.Text = "";
        }

        private void _LoadCountriesInComboBox()
        {
            cbCountry.DataSource = clsCountry.GetAllCountries();
            cbCountry.DisplayMember = "CountryName";
            cbCountry.ValueMember = "CountryID";
            cbCountry.SelectedIndex = cbCountry.FindString("Syria");
        }

        private void _LoadPersonData()
        {
            if (_Person == null)
            {
                _Person = clsPerson.Find(_PersonID);
                if (_Person == null)
                {
                    MessageBox.Show("No Person Found with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }
            }

            lblTitle.Text = "Edit Person";
            this.Text = "Edit Person";
            lblPersonID.Text = _Person.PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName == string.Empty ? string.Empty : _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            txtNationalNo.Text = _Person.NationalNumber;

            if (_Person.Gender == 0)
            {
                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = true;
            }

            txtEmail.Text = _Person.Email;
            txtAddress.Text = _Person.Address;
            dtpDateOfBirth.Value = Convert.ToDateTime(_Person.DateOfBirth);
            txtPhone.Text = _Person.Phone;
            cbCountry.SelectedIndex = cbCountry.FindString(_Person.Country.CountryName.Trim());
            llRemoveImage.Visible = (_Person.ImagePath != null);
            if (_Person.ImagePath != string.Empty)
            {
                string ImagePath = Path.Combine(clsGlobal.FolderPath, _Person.ImagePath);
                if (File.Exists(ImagePath))
                {
                    pbPersonImage.ImageLocation = ImagePath.Trim();
                }
            }
        }

        private void _SetNewImage()
        {
            ofdSelectImage.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            ofdSelectImage.FilterIndex = 1;
            ofdSelectImage.RestoreDirectory = true;

            if (ofdSelectImage.ShowDialog() == DialogResult.OK)
            {
                pbPersonImage.ImageLocation = ofdSelectImage.FileName;
                llRemoveImage.Visible = true;
            }
        }

        private void _RemoveImage()
        {
            pbPersonImage.ImageLocation = null;

            if (rbMale.Checked)
                pbPersonImage.Image = Properties.Resources.Male_512;
            else
                pbPersonImage.Image = Properties.Resources.Female_512;

            llRemoveImage.Visible = false;
        }

        private void _SavePersonInfo()
        {
            _Person.NationalNumber = txtNationalNo.Text;
            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.Gender = (short)(rbMale.Checked ? enGender.Male : enGender.FeMale);
            _Person.Phone = txtPhone.Text;
            _Person.Email = txtEmail.Text;
            _Person.Address = txtAddress.Text;
            _Person.NationalityCountryID = cbCountry.SelectedIndex + 1;
            _HandlePersonImage();
        }

        private void _HandlePersonImage()
        {
            if (_Person.ImagePath != pbPersonImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    File.Delete(_Person.ImagePath);
                }
                if (pbPersonImage.ImageLocation != null)
                {
                    string SourceImageFile = pbPersonImage.ImageLocation.ToString();

                    if (clsUtil.SaveImageToProjectFolder(ref SourceImageFile))
                    {
                        _Person.ImagePath = SourceImageFile;
                        pbPersonImage.ImageLocation = Path.Combine(clsGlobal.FolderPath, SourceImageFile);
                    }
                }
            }
        }
    }
}
using System;
using System.Windows.Forms;
using DVLD_Business_Layer;
using DVLD.Global_Classes;
using System.Data;

namespace DVLD.Applications.Local_Driving_License
{
    public partial class frmAddUpdateLocalDrivingLicenseApplication : DVLD.frmBaseForm
    {
        static float ApplicationTypeFees = clsApplicationType.FindApplicationType(1).Fees;
        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _LicenseApplicationID = -1;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;

        public frmAddUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            ctrlPersonCardWithFilter1.OnPersonFound += _IsPersonFound;
        }

        public frmAddUpdateLocalDrivingLicenseApplication(int LicenseApplicationID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _LicenseApplicationID = LicenseApplicationID;
            ctrlPersonCardWithFilter1.OnPersonFound += _IsPersonFound;
        }

        private void frmAddUpdateLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update)
            {
                _LoadLocalDrivingLicenseApplicationInfo();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tcApplicationInfo.SelectedTab = tcApplicationInfo.TabPages["tpApplicationInfo"];
        }

        private void tcApplicationInfo_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (_Mode == enMode.AddNew)
            {
                if (ctrlPersonCardWithFilter1.SelectedPersonInfo == null)
                {
                    MessageBox.Show(
                    "Select a Person to Proceed",
                    "Error",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
                else
                {
                    btnSave.Enabled = true;
                    tpApplicationInfo.Enabled = true;
                }
            }
        }

        private void cbLicenseClassList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _SetTotalApplicationFees();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsLicenseClass SelectedLicenseClass = clsLicenseClass.Find(cbLicenseClassList.SelectedIndex + 1);
            int SelectedPersonID = ctrlPersonCardWithFilter1.SelectedPersonID;

            int LicenseID = clsLocalDrivingLicenseApplication.GetLicenseID(SelectedPersonID, SelectedLicenseClass.LicenseClassID);
            int ActiveApplicationID = clsLocalDrivingLicenseApplication
                .GetActiveLocalDrivingLicenseApplicationID(SelectedPersonID, SelectedLicenseClass.LicenseClassID);

            if (SelectedLicenseClass.MinimumAllowedAge > _GetSelectedPersonAgeInYears())
            {
                MessageBox.Show(
                    text: @"This Person Can't Apply For This License Class Beacause of" +
                           "\n The Minimum Allowed Age For This Class" +
                           "\n Please Choose Another License Class",
                    caption: "Error",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Warning);
                return;
            }

            if (LicenseID != -1)
            {
                MessageBox.Show(
                    text: $@"This Person Already Have A License With ID = {LicenseID}" +
                            "\n From This License Class, Please Choose Another License Class",
                    caption: "Error",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Warning);
                return;
            }

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show(
                    text: $@"This Person Already Have An Active Application With ID = {ActiveApplicationID}" +
                                    "\nFrom This License Class, Please Choose Another License Class",
                    caption: "Error",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Warning);
                return;
            }

            _SaveApplicationInfo();

            if (_LocalDrivingLicenseApplication.Save())
            {
                if (_Mode == enMode.AddNew)
                {
                    MessageBox
                        .Show($@"Application Added Successfully With ID =
                        { _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString()} Done");
                    _Mode = enMode.Update;
                }
                else
                {
                    MessageBox.Show($"Application Updated Successfully", "Done");
                }

                IsDataUpdated();
                _LicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
                _LoadLocalDrivingLicenseApplicationInfo();
            }
            else
            {
                MessageBox.Show("Save Failed", "Error");
            }
        }

        private void _LoadLicenseClassesList()
        {
            cbLicenseClassList.DataSource = clsLicenseClass.GetAllLicenseClasses();
            cbLicenseClassList.DisplayMember = "ClassName";
            cbLicenseClassList.ValueMember = "LicenseClassID";
            cbLicenseClassList.SelectedIndex = 2;
        }

        private void _ResetDefaultValues()
        { 
            if (_Mode == enMode.AddNew)
            {
                this.Text = lblTitle.Text = "Add New Local Driving License Application";
                _LocalDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                tpApplicationInfo.Enabled = false;
            }
            else
            {
                this.Text = lblTitle.Text = "Update Local Driving License Application";
                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
            }
            lblApplicationID.Text = "";
            lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            lblUser.Text = clsGlobal.CurrentUser.UserName;
            _LoadLicenseClassesList();
            _SetTotalApplicationFees();
        }

        private void _LoadLocalDrivingLicenseApplicationInfo()
        {
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show(
                    text: $"No Application with ID ={_LicenseApplicationID} ",
                    caption: "Application Not Found",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonData(_LocalDrivingLicenseApplication.ApplicantPerson);
            lblApplicationID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(_LocalDrivingLicenseApplication.ApplicationDate);
            cbLicenseClassList.SelectedIndex = cbLicenseClassList.FindString(_LocalDrivingLicenseApplication.LicenseClassInfo.ClassName);
            lblApplicationFees.Text = _LocalDrivingLicenseApplication.PaidFees.ToString();
            lblUser.Text = _LocalDrivingLicenseApplication.CreatedByUserInfo.UserName.ToString();

            tpApplicationInfo.Enabled = true;
            btnSave.Enabled = true;
        }

        private void _SetTotalApplicationFees()
        {
            DataRowView row = (DataRowView)cbLicenseClassList.SelectedItem;
            float ClassFees = float.Parse(row["ClassFees"].ToString());

            float TotalFees = ApplicationTypeFees + ClassFees;

            lblApplicationFees.Text = TotalFees.ToString();
        }

        private int _GetSelectedPersonAgeInYears()
        {
            int SelectedPersonAge = ctrlPersonCardWithFilter1.SelectedPersonInfo.DateOfBirth.Year;
            return (DateTime.Now.Year - SelectedPersonAge);
        }

        private void _SaveApplicationInfo()
        {
            _LocalDrivingLicenseApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.SelectedPersonID;
            _LocalDrivingLicenseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseApplication.PaidFees = float.Parse(lblApplicationFees.Text);
            _LocalDrivingLicenseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _LocalDrivingLicenseApplication.LicenseClassID = clsLicenseClass.Find(cbLicenseClassList.Text).LicenseClassID;
        }

        private void _IsPersonFound(bool result)
        {
            btnNext.Enabled = result;
            btnSave.Enabled = result;
            tpApplicationInfo.Enabled = result;
        }

    }
}
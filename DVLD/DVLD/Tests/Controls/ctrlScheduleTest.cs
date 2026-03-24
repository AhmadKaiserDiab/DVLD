using System;
using System.Windows.Forms;
using DVLD_Business_Layer;
using DVLD.Global_Classes;

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
        float TestFees = 0;
        int _TestAppointmentID = -1;
        private int _LocalDrivingLicenseApplicationID = -1;
        clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;
        clsTestAppointment _TestAppointment;
        clsApplication RetakeTestApplication;

        public event Action OnDataUpdated;
        protected virtual void IsDataUpdated()
        {
            Action handler = OnDataUpdated;

            if (handler != null)
            {
                handler?.Invoke();
            }
        }

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (RetakeTestApplication != null)
            {
                if (!RetakeTestApplication.Save())
                {
                    MessageBox.Show(
                            text: "Error Couldn't Save Data",
                            caption: "Saved",
                            buttons: MessageBoxButtons.OK,
                            icon: MessageBoxIcon.Information);
                    return;
                }
                _TestAppointment.RetakeTestApplicationID = RetakeTestApplication.ApplicationID;
            }
            _SaveAppointmentInfo();
            if (_TestAppointment.Save())
            {
                MessageBox.Show(
                    text: "Data Saved Successfully",
                    caption: "Saved",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Information);
                IsDataUpdated();
                _TestAppointmentID = _TestAppointment.TestAppointmentID;
                _LoadAppointmentData();
            }
        }

        public new void Load(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID, int AppointmentID)
        {
            _SetDefaultValues();
            _TestTypeID = TestTypeID;
            _TestAppointmentID = AppointmentID;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _LoadAppointmentData();
        }

        private void _SetDefaultValues()
        {
            dtpTestAppointmentDate.MinDate = DateTime.Now;
            lblTitle.Text = "Schedule Test";
            lblDLAppID.Text = "[?????]";
            lblDClass.Text = "[?????]";
            lblPersonName.Text = "[?????]";
            lblFees.Text = "[$$$$$]";
            lblTrial.Text = "0";
            lblRetakeTestID.Text = "[?????]";
            lblRetakeTestFees.Text = "[$$$$$]";
            lblTotalFees.Text = "[$$$$$]";
            lblCaution.Visible = false;
        }

        private void _LoadAppointmentData()
        {
            _LoadTestTypeInfo();
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);
            TestFees = clsTestType.FindTestType(_TestTypeID).Fees;
            int Trial = _LocalDrivingLicenseApplication.GetTotalTrialsPerTest(_TestTypeID);

            lblTitle.Text = (Trial == 1) ? "Schedule Test" : "Schedule Retake Test";
            lblDLAppID.Text = _LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblPersonName.Text = _LocalDrivingLicenseApplication.ApplicantPerson.FullName;
            lblTrial.Text = Trial.ToString();
            lblFees.Text = TestFees.ToString();
            lblTotalFees.Text = TestFees.ToString();
            _HandleAppointmentData();
        }

        private void _LoadTestTypeInfo()
        {
            switch (_TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    {
                        grbTestAppointment.Text = "Vision Test";
                        pbTestImage.Image = Properties.Resources.Vision_512;
                        break;
                    }
                case clsTestType.enTestType.WrittenTest:
                    {
                        grbTestAppointment.Text = "Written Test";
                        pbTestImage.Image = Properties.Resources.Written_Test_512;
                        break;
                    }
                case clsTestType.enTestType.StreetTest:
                    {
                        grbTestAppointment.Text = "Driving Test";
                        pbTestImage.Image = Properties.Resources.driving_test_512;
                        break;
                    }
            }
        }

        private void _HandleAppointmentData()
        {
            _TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if (_TestAppointment == null)
            {
                _TestAppointment = new clsTestAppointment();
            }

            dtpTestAppointmentDate.Value = _TestAppointment.AppointmentDate;

            if (!_TestAppointment.IsLocked)
            {

                if (_LocalDrivingLicenseApplication.DoesAttendTestType(_TestTypeID) &&
                    _TestAppointment.RetakeTestApplicationInfo == null)
                {
                    RetakeTestApplication = new clsApplication();
                    RetakeTestApplication.ApplicantPersonID = this._LocalDrivingLicenseApplication.ApplicantPersonID;
                    RetakeTestApplication.ApplicationTypeID = (int)clsApplication.enApplicationType.RetakeTest;
                    RetakeTestApplication.PaidFees = clsApplicationType.FindApplicationType(RetakeTestApplication.ApplicationTypeID).Fees;
                    RetakeTestApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
                }
            }
            else
            {
                btnSave.Enabled = false;
                lblCaution.Visible = true;
                dtpTestAppointmentDate.Enabled = false;
            }

            if (_TestAppointment.RetakeTestApplicationInfo == null)
            {
                return;
            }
            lblRetakeTestID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
            lblRetakeTestFees.Text = _TestAppointment.RetakeTestApplicationInfo.ApplicationTypeInfo.Fees.ToString();
            lblTotalFees.Text = (_TestAppointment.RetakeTestApplicationInfo.PaidFees + TestFees).ToString();
        }

        private void _SaveAppointmentInfo()
        {
            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dtpTestAppointmentDate.Value;
            _TestAppointment.PaidFees = float.Parse(lblTotalFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobal.CurrentUser.UserID;
        }
    }
}

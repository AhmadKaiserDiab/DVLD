using System.Windows.Forms;
using DVLD_Business_Layer;

namespace DVLD.Tests.Controls
{
    public partial class ctrlScheduledTest : UserControl
    {
        clsTestType.enTestType _TestTypeID = clsTestType.enTestType.WrittenTest;
        clsTestAppointment _TestAppointment;

        public ctrlScheduledTest()
        {
            InitializeComponent();
        }

        public new void Load(clsTestAppointment TestAppointment)
        {
            _TestAppointment = TestAppointment;
            _TestTypeID = TestAppointment.TestTypeID;
            _LoadTestData();
        }

        private void _LoadTestData()
        {
            int TestID = _TestAppointment.GetAsscociatedTestID();
            int Trial = _TestAppointment.LocalDrivingLicenseApplicationInfo.GetTotalTrialsPerTest(_TestTypeID);

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

            lblDLAppID.Text    = _TestAppointment.LocalDrivingLicenseApplicationID.ToString();
            lblDClass.Text     = _TestAppointment.LocalDrivingLicenseApplicationInfo.LicenseClassInfo.ClassName;
            lblPersonName.Text = _TestAppointment.LocalDrivingLicenseApplicationInfo.ApplicantPerson.FullName;
            lblTrial.Text      = Trial.ToString();
            lblDate.Text       = _TestAppointment.AppointmentDate.ToString();
            lblFees.Text       = _TestAppointment.TestTypeInfo.Fees.ToString();

            if (TestID != -1)
            {
                lblTestID.Text = TestID.ToString();
            }
            else
            {
                lblTestID.Text = "Not Taken Yet";
            }
        }
        
    }
}

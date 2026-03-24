using System;
using DVLD_Business_Layer;

namespace DVLD.Tests
{
    public partial class frmScheduleTest : DVLD.frmBaseForm
    {
        private int _AppointmentID = -1;
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        
        public frmScheduleTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID, int AppointmentID = -1)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
            _AppointmentID = AppointmentID;
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.OnDataUpdated += base.IsDataUpdated;
            ctrlScheduleTest1.Load(_LocalDrivingLicenseApplicationID, _TestTypeID, _AppointmentID);
        }

    }
}

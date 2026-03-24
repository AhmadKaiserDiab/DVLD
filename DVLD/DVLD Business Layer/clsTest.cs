using DVLD_Data_Access_Layer;

namespace DVLD_Business_Layer
{
    public class clsTest
    {
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool Result { get; set; }
        public string Notes { get; set; }
        public int CreatedByUserID { get; set; }

        private bool _AddNewTest()
        {
            TestID = clsTestData.AddNewTest(TestAppointmentID,
                Result, Notes, CreatedByUserID);
            return (TestID != -1);
        }

        private clsTest(int TestID, int TestAppointmentID, bool Result,
            string Notes, int CreatedByUserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.Result = Result;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;
        }

        public clsTest()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.Result = false;
            this.Notes = "";
            this.CreatedByUserID = -1;
        }

        public bool Save()
        {
            if (clsTestAppointment.LockTestAppointment(TestAppointmentID))
            {
                return _AddNewTest();
            }
            return false;
        }
    }
}

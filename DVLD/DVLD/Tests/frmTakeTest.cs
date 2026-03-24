using System;
using System.Windows.Forms;
using DVLD.Global_Classes;
using DVLD_Business_Layer;

namespace DVLD.Tests
{
    public partial class frmTakeTest : DVLD.frmBaseForm
    {
        int _TestAppointmentID = -1;
        clsTestAppointment TestAppointment;
        clsTest Test = new clsTest();

        public frmTakeTest(int TestAppointmentID)
        {
            InitializeComponent();
            _TestAppointmentID = TestAppointmentID;
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            TestAppointment = clsTestAppointment.Find(_TestAppointmentID);
            if (TestAppointment == null)
            {
                MessageBox.Show(
                    text: $"Appointment With ID = {_TestAppointmentID} Not Found",
                    caption: "Error",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error
                    );
                btnSave.Enabled = false;
                return;
            }
            ctrlScheduledTest1.Load(TestAppointment);
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _SaveTestData();
            if (MessageBox.Show(
                    text: "Are You Sure You want To Save? After That You Cannot Change\n" +
                            "The Pass/Fail Result, Are You Sure?",
                    caption: "Confirm",
                    buttons: MessageBoxButtons.OKCancel,
                    icon: MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (Test.Save())
                {
                    btnSave.Enabled = false;
                    MessageBox.Show(
                        text: "Data Saved Successfully",
                        caption: "Saved",
                        buttons: MessageBoxButtons.OK,
                        icon: MessageBoxIcon.Information
                        );
                    IsDataUpdated();
                    ctrlScheduledTest1.Load(TestAppointment);
                }
                else
                {
                    MessageBox.Show(
                        text: "Failed Save To Data",
                        caption: "Error",
                        buttons: MessageBoxButtons.OK,
                        icon: MessageBoxIcon.Error);
                }
            }
        }

        private void _SaveTestData()
        {
            Test.TestAppointmentID = TestAppointment.TestAppointmentID;
            Test.Result = rbPass.Checked;
            Test.Notes = txtNotes.Text;
            Test.CreatedByUserID = clsGlobal.CurrentUser.UserID;
        }

    }
}

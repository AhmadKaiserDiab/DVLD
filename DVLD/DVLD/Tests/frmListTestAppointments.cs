using System;
using System.Windows.Forms;
using DVLD_Business_Layer;

namespace DVLD.Tests
{
    public partial class frmListTestAppointments : DVLD.frmBaseForm
    {
        clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        int _LocalDrivingLicenseApplicationID = -1;

        public frmListTestAppointments(int LocalLicenseApplicationTestID, clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalLicenseApplicationTestID;
            _TestTypeID = TestTypeID;
        }

        private void frmListTestAppointments_Load(object sender, EventArgs e)
        {
            _LoadData();
            _FormatDataGridColumns();
        }

        private void btnAddAppointment_Click(object sender, EventArgs e)
        {
            if (clsLocalDrivingLicenseApplication.DoesPassTestType(
                        _LocalDrivingLicenseApplicationID, _TestTypeID))
            {
                MessageBox.Show(
                    text: @"Person Already Passed this Test\n" +
                           "You Cannot Add new Appointment",
                    caption: "Not Allowed",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error);
                return;
            }

            if (clsLocalDrivingLicenseApplication
                .IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID, _TestTypeID))
            {
                MessageBox.Show(
                    text: @"Person Already had An Appointment for this Test\n" +
                           "You Cannot add new Appointment",
                    caption: "Not Allowed",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestTypeID);
            frm.OnDataUpdated += frm_OnDataUpdated;
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppoinmentID = (int)dgvTestAppointmetnsList.CurrentRow.Cells[0].Value;
            frmScheduleTest frm = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestTypeID, AppoinmentID);
            frm.OnDataUpdated += frm_OnDataUpdated;
            frm.ShowDialog();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((bool)dgvTestAppointmetnsList.CurrentRow.Cells[3].Value)
            {
                MessageBox.Show(
                    "This Test Already Taken",
                    "No",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Exclamation
                    );
                return;
            }

            int TestAppointmentID = (int)dgvTestAppointmetnsList.CurrentRow.Cells[0].Value;
            frmTakeTest frm = new frmTakeTest(TestAppointmentID);
            frm.OnDataUpdated += frm_OnDataUpdated;
            frm.ShowDialog();
        }

        private void dgvAppointmetnsList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo Location = dgvTestAppointmetnsList.HitTest(e.X, e.Y);
                if (Location.RowIndex >= 0)
                {
                    dgvTestAppointmetnsList.ClearSelection();
                    dgvTestAppointmetnsList.Rows[Location.RowIndex].Selected = true;
                }
            }
        }

        private void frm_OnDataUpdated()
        {
            _LoadData();
            IsDataUpdated();
        }

        private void _LoadTestAppointmentsList()
        {
            dgvTestAppointmetnsList.DataSource = clsTestAppointment
                .GetTestAppointmentsList( 
                    _LocalDrivingLicenseApplicationID, _TestTypeID);
            lblRecord.Text = dgvTestAppointmetnsList.RowCount.ToString();
        }

        private void _FormatDataGridColumns()
        {
            dgvTestAppointmetnsList.Columns[0].HeaderText = "Appointmennt ID";
            dgvTestAppointmetnsList.Columns[0].Width      = 120;
            dgvTestAppointmetnsList.Columns[1].HeaderText = "Appointment Date";
            dgvTestAppointmetnsList.Columns[1].Width      = 150;
            dgvTestAppointmetnsList.Columns[2].HeaderText = "Paid Fees";
            dgvTestAppointmetnsList.Columns[2].Width      = 120;
            dgvTestAppointmetnsList.Columns[3].HeaderText = "Is Locked";
            dgvTestAppointmetnsList.Columns[3].Width      = 120;
        }

        private void _LoadData()
        {
            switch (_TestTypeID)
            {
                case clsTestType.enTestType.VisionTest:
                    {
                        lblTitle.Text = "Vision Test Appointment";
                        pbTestImage.Image = Properties.Resources.Vision_Test_32;
                        break;
                    }
                case clsTestType.enTestType.WrittenTest:
                    {
                        lblTitle.Text = "Written Test Appointment";
                        pbTestImage.Image = Properties.Resources.Written_Test_32;
                        break;
                    }
                case clsTestType.enTestType.StreetTest:
                    {
                        lblTitle.Text = "Driving Test Appointment";
                        pbTestImage.Image = Properties.Resources.Street_Test_32;
                        break;
                    }
                default:
                    break;
            }
            _LoadTestAppointmentsList();
            ctrlDrivingLicenseApplicationInfo1.Load(_LocalDrivingLicenseApplicationID);
        }
    }
}

using System;
using System.Data;
using System.Windows.Forms;
using DVLD.Tests;
using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
using DVLD_Business_Layer;

namespace DVLD.Applications.Local_Driving_License
{
    public partial class frmListLocalDrivingLicenseApplications : DVLD.frmBaseForm
    {
        enum enTestTypes
        {
            VisionTest = 0,
            WrittenTest = 1,
            StreetTest = 2
        }

        private DataTable _dtLocalLicenseApplicationsData;

        public frmListLocalDrivingLicenseApplications()
        {
            InitializeComponent();
        }

        private void frmListLocalDrivingLicenseApplications_Load(object sender, EventArgs e)
        {
            _LoadLocalLicensesApplicationsData();
            _LoadFiltersOptions();
            _FormatDataGridColumns();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ToggleSearchFilters();
            _FilterData();
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStatus.Text != "All")
            {
                _dtLocalLicenseApplicationsData.DefaultView.RowFilter = $"Status = '{cbStatus.Text}'";
            }
            else
            {
                _dtLocalLicenseApplicationsData.DefaultView.RowFilter = "";
            }
            lblRecord.Text = dgvLocalLicenseApplicationsList.RowCount.ToString();
        }

        private void txtSearchInput_TextChanged(object sender, EventArgs e)
        {
            _FilterData();
        }

        private void txtSearchInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Driver ID" || cbFilter.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            else if (cbFilter.Text == "National No.")
            {
                if (txtSearchInput.Text.Length == 0)
                {
                    e.Handled = !(e.KeyChar == 'N');
                }
                else
                {
                    e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
                }
            }
            else
            {
                e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void btnAddNewLocalLicenseApplication_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication();
            frm.OnDataUpdated += frm_OnDataUpdated;
            frm.ShowDialog();
        }

        private void ShowApplicationDetails_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalLicenseApplicationsList.SelectedRows[0].Cells[0].Value;
            frmLocalDrivingLicenseApplicationInfo frm = new frmLocalDrivingLicenseApplicationInfo(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();
        }

        private void EditApplication_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalLicenseApplicationsList.SelectedRows[0].Cells[0].Value;
            frmAddUpdateLocalDrivingLicenseApplication frm = new frmAddUpdateLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);
            frm.OnDataUpdated += frm_OnDataUpdated;
            frm.ShowDialog();
        }

        private void DeleteApplication_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalLicenseApplicationsList.SelectedRows[0].Cells[0].Value;
            if (
            MessageBox.Show(
                text: $" Are You Sure You Want To Delete Application [ {LocalDrivingLicenseApplicationID} ]",
                caption: "Confirm Delete",
                icon: MessageBoxIcon.Question,
                buttons: MessageBoxButtons.YesNo
                ) == DialogResult.Yes)
            {
                if (clsLocalDrivingLicenseApplication
                    .Delete(LocalDrivingLicenseApplicationID))
                {
                    MessageBox.Show(
                        text: "Application Deleted Successfully",
                        caption: "Successful",
                        icon: MessageBoxIcon.Asterisk,
                        buttons: MessageBoxButtons.OK);
                    _LoadLocalLicensesApplicationsData();
                }
                else
                {
                    MessageBox.Show(
                        text: "Applicaton Was Not Deleted Because it Has Data Linked To It",
                        caption: "Error",
                        icon: MessageBoxIcon.Error,
                        buttons: MessageBoxButtons.OK);
                }
            }
        }

        private void CancelApplication_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalLicenseApplicationsList.SelectedRows[0].Cells[0].Value;
            if (
                MessageBox.Show(
                    text: $"Are You Sure You Want To Cancel Application [{LocalDrivingLicenseApplicationID}]",
                    caption: "Confirm Cancel",
                    icon: MessageBoxIcon.Question,
                    buttons: MessageBoxButtons.YesNo
                    ) == DialogResult.Yes)
            {
                if (clsLocalDrivingLicenseApplication
                    .Cancel(LocalDrivingLicenseApplicationID))
                {
                    MessageBox.Show(
                        text: "Application Canceled Successfully",
                        caption: "Successful",
                        icon: MessageBoxIcon.Asterisk,
                        buttons: MessageBoxButtons.OK);
                    _LoadLocalLicensesApplicationsData();
                }
            }
        }

        private void SechduleVisionTest_Click(object sender, EventArgs e)
        {
            _SechduleTest(clsTestType.enTestType.VisionTest);
        }

        private void SechduleWrittenTest_Click(object sender, EventArgs e)
        {
            _SechduleTest(clsTestType.enTestType.WrittenTest);
        }

        private void SechduleStreetTest_Click(object sender, EventArgs e)
        {
            _SechduleTest(clsTestType.enTestType.StreetTest);
        }

        private void _SechduleTest(clsTestType.enTestType TestTypeID)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalLicenseApplicationsList.SelectedRows[0].Cells[0].Value;
            frmListTestAppointments frm = new frmListTestAppointments(LocalDrivingLicenseApplicationID, TestTypeID);
            frm.OnDataUpdated += frm_OnDataUpdated;
            frm.ShowDialog();
        }

        private void IssueLicense_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalLicenseApplicationsList.SelectedRows[0].Cells[0].Value;
            frmIssueDriverLicenseFirstTime frm = new frmIssueDriverLicenseFirstTime(LocalDrivingLicenseApplicationID);
            frm.OnDataUpdated += frm_OnDataUpdated;
            frm.ShowDialog();
        }

        private void ShowLicense_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvLocalLicenseApplicationsList
                .SelectedRows[0].Cells[0].Value;

            int LicenseID = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicenseApplicationID).GetLicenseID();

            if (LicenseID != -1)
            {
                frmShowLocalLicenseInfo frm = new frmShowLocalLicenseInfo(LicenseID);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("No License Found!", "No License", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void ShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            string NationalNo = dgvLocalLicenseApplicationsList
                .SelectedRows[0].Cells[2].Value.ToString();

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(NationalNo);
            frm.ShowDialog();
        }

        private void dgvLocalLicenseApplicationsList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo Location = dgvLocalLicenseApplicationsList.HitTest(e.X, e.Y);
                if (Location.RowIndex >= 0)
                {
                    dgvLocalLicenseApplicationsList.ClearSelection();
                    dgvLocalLicenseApplicationsList.Rows[Location.RowIndex].Selected = true;
                }
                _UpdateContextMenuOptions();
            }
        }

        private void frm_OnDataUpdated()
        {
            _LoadLocalLicensesApplicationsData();
        }

        private void _LoadLocalLicensesApplicationsData()
        {
            _dtLocalLicenseApplicationsData = clsLocalDrivingLicenseApplication
                .GetAllLocalDrivingLicenseApplications();
            dgvLocalLicenseApplicationsList.DataSource = _dtLocalLicenseApplicationsData;
            lblRecord.Text = dgvLocalLicenseApplicationsList.RowCount.ToString();
        }

        private void _FormatDataGridColumns()
        {
            if (dgvLocalLicenseApplicationsList.Rows.Count != 0)
            {
                dgvLocalLicenseApplicationsList.Columns[0].HeaderText = "L.D.L.App";
                dgvLocalLicenseApplicationsList.Columns[0].Width = 110;

                dgvLocalLicenseApplicationsList.Columns[1].HeaderText = "Driving Class";
                dgvLocalLicenseApplicationsList.Columns[1].Width = 200;

                dgvLocalLicenseApplicationsList.Columns[2].HeaderText = "National No.";
                dgvLocalLicenseApplicationsList.Columns[2].Width = 100;

                dgvLocalLicenseApplicationsList.Columns[3].HeaderText = "Full Name";
                dgvLocalLicenseApplicationsList.Columns[3].Width = 250;

                dgvLocalLicenseApplicationsList.Columns[4].HeaderText = "Application Date";
                dgvLocalLicenseApplicationsList.Columns[4].Width = 130;

                dgvLocalLicenseApplicationsList.Columns[5].HeaderText = "Passed Tests";
                dgvLocalLicenseApplicationsList.Columns[5].Width = 100;

                dgvLocalLicenseApplicationsList.Columns[6].HeaderText = "Status";
                dgvLocalLicenseApplicationsList.Columns[6].Width = 140;
            }
        }

        private void _LoadFiltersOptions()
        {
            cbFilter.Items.Add("None");
            cbFilter.Items.Add("L.D.L.App");
            cbFilter.Items.Add("Driving Class");
            cbFilter.Items.Add("National No.");
            cbFilter.Items.Add("Fulll Name");
            cbFilter.Items.Add("Passed Tests");
            cbFilter.Items.Add("Status");

            cbFilter.SelectedIndex = 0;


            cbStatus.Items.Add("All");
            cbStatus.Items.Add("New");
            cbStatus.Items.Add("Cancelled");
            cbStatus.Items.Add("Completed");

            cbStatus.SelectedIndex = 0;
        }

        private void _ToggleSearchFilters()
        {
            if (cbFilter.Text == "None")
            {
                txtSearchInput.Visible = false;
                cbStatus.Visible = false;
                return;
            }

            if (cbFilter.Text == "Status")
            {
                txtSearchInput.Visible = false;
                cbStatus.Visible = true;
                cbStatus.SelectedIndex = 0;
                cbStatus.Focus();
                return;
            }

            txtSearchInput.Visible = true;
            txtSearchInput.Focus();
            txtSearchInput.Text = "";
            cbStatus.Visible = false;
        }

        private void _FilterData()
        {
            string FilterColumn = "";

            switch (cbFilter.Text)
            {
                case "L.D.L.App":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;
                case "Driving Class":
                    FilterColumn = "ClassName";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Passed Tests":
                    FilterColumn = "PassedTests";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtSearchInput.Text == "" || FilterColumn == "None")
            {
                _dtLocalLicenseApplicationsData.DefaultView.RowFilter = "";
            }
            else if (FilterColumn == "LocalDrivingLicenseApplicationID" ||
                     FilterColumn == "PassedTests")
            {
                _dtLocalLicenseApplicationsData.DefaultView.RowFilter = $"{FilterColumn} = {txtSearchInput.Text}";
            }
            else
            {
                _dtLocalLicenseApplicationsData.DefaultView.RowFilter = $"{FilterColumn} Like '%{txtSearchInput.Text}%'";
            }
            lblRecord.Text = dgvLocalLicenseApplicationsList.RowCount.ToString();
        }

        private void _UpdateContextMenuOptions()
        {
            int PassedTests = int.Parse(dgvLocalLicenseApplicationsList.SelectedRows[0].Cells[5].Value.ToString());
            string Status = dgvLocalLicenseApplicationsList.SelectedRows[0].Cells[6].Value.ToString();

            EditApplication.Enabled = (PassedTests == 0 && Status != "Cancelled");
            DeleteApplication.Enabled = (Status != "Completed");
            CancelApplication.Enabled = (Status == "New");
            IssueLicense.Enabled = (PassedTests == 3 && Status == "New");
            ShowLicense.Enabled = (Status == "Completed");

            SechduleTests.Enabled = (PassedTests < 3 && Status != "Cancelled");

            if (SechduleTests.Enabled)
            {
                SechduleVisionTest.Enabled = ((enTestTypes)PassedTests == enTestTypes.VisionTest);
                SechduleWrittenTest.Enabled = ((enTestTypes)PassedTests == enTestTypes.WrittenTest);
                SechduleDrivingTest.Enabled = ((enTestTypes)PassedTests == enTestTypes.StreetTest);
            }
        }

    }
}
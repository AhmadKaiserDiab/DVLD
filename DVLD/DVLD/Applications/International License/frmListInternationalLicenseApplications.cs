using System;
using System.Data;
using System.Windows.Forms;
using DVLD.People;
using DVLD.Licenses;
using DVLD.Licenses.International_Licenses;
using DVLD_Business_Layer;

namespace DVLD.Applications.International_License
{
    public partial class frmListInternationalLicenseApplications : frmBaseForm
    {
        DataTable _dtInternationalLicenseApplicationsData;

        public frmListInternationalLicenseApplications()
        {
            InitializeComponent();
        }

        private void frmListInternationalLicenseApplications_Load(object sender, EventArgs e)
        {
            _LoadInternationalLicenseApplicationsData();
            _FormatDataGridColumns();
            _LoadFiltersOptions();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ToggleFilters();
            _FilterData();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterValue = "";

            switch (cbIsActive.Text)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }
            if (cbIsActive.Text == "All")
            {
                _dtInternationalLicenseApplicationsData.DefaultView.RowFilter = "";
            }
            else
            {
                _dtInternationalLicenseApplicationsData.DefaultView.RowFilter = $"IsActive = {FilterValue}";
            }
            lblRecord.Text = dgvInternationalLicenseApplicationsList.RowCount.ToString();
        }

        private void txtSearchInput_TextChanged(object sender, EventArgs e)
        {
            _FilterData();
        }

        private void txtSearchInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void dgvInternationalLicenseApplicationsList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo Location = dgvInternationalLicenseApplicationsList.HitTest(e.X, e.Y);
                if (Location.RowIndex >= 0)
                {
                    dgvInternationalLicenseApplicationsList.ClearSelection();
                    dgvInternationalLicenseApplicationsList.Rows[Location.RowIndex].Selected = true;
                }
            }
        }

        private void btnAddNewInternationalLicenseApplication_Click(object sender, EventArgs e)
        {
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();
            _LoadInternationalLicenseApplicationsData();
        }

        private void ShowPersonDetails_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicenseApplicationsList.SelectedRows[0].Cells[2].Value;
            int PersonID = clsDriver.FindDriverByID(DriverID).PersonID;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void ShowLicenseDetails_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvInternationalLicenseApplicationsList.SelectedRows[0].Cells[0].Value;
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void ShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvInternationalLicenseApplicationsList.SelectedRows[0].Cells[2].Value;
            int PersonID = clsDriver.FindDriverByID(DriverID).PersonID;
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void _LoadFiltersOptions()
        {
            cbFilter.Items.Add("None");
            cbFilter.Items.Add("Int.License ID");
            cbFilter.Items.Add("Application ID");
            cbFilter.Items.Add("Driver ID");
            cbFilter.Items.Add("L.License ID");
            cbFilter.Items.Add("Is Active");

            cbFilter.SelectedIndex = 0;

            cbIsActive.Items.Add("All");
            cbIsActive.Items.Add("Yes");
            cbIsActive.Items.Add("No");

            cbIsActive.SelectedIndex = 0;
        }

        private void _LoadInternationalLicenseApplicationsData()
        {
            _dtInternationalLicenseApplicationsData = clsInternationalLicense.GetAllInternationalLicenses();
            dgvInternationalLicenseApplicationsList.DataSource = _dtInternationalLicenseApplicationsData;
            lblRecord.Text = dgvInternationalLicenseApplicationsList.RowCount.ToString();
        }

        private void _FormatDataGridColumns()
        {
            dgvInternationalLicenseApplicationsList.Columns[0].HeaderText = "Int.License ID";
            dgvInternationalLicenseApplicationsList.Columns[0].Width = 100;

            dgvInternationalLicenseApplicationsList.Columns[1].HeaderText = "Application ID";
            dgvInternationalLicenseApplicationsList.Columns[1].Width = 100;

            dgvInternationalLicenseApplicationsList.Columns[2].HeaderText = "Driver ID";
            dgvInternationalLicenseApplicationsList.Columns[2].Width = 100;

            dgvInternationalLicenseApplicationsList.Columns[3].HeaderText = "L.License ID";
            dgvInternationalLicenseApplicationsList.Columns[3].Width = 100;

            dgvInternationalLicenseApplicationsList.Columns[4].HeaderText = "Issue Date";
            dgvInternationalLicenseApplicationsList.Columns[4].Width = 150;

            dgvInternationalLicenseApplicationsList.Columns[5].HeaderText = "Expiration Date";
            dgvInternationalLicenseApplicationsList.Columns[5].Width = 150;

            dgvInternationalLicenseApplicationsList.Columns[6].HeaderText = "Is Active";
            dgvInternationalLicenseApplicationsList.Columns[6].Width = 100;
        }

        private void _ToggleFilters()
        {
            if (cbFilter.Text == "None")
            {
                txtSearchInput.Visible = false;
                cbIsActive.Visible = false;
                return;
            }

            if (cbFilter.Text == "Is Active")
            {
                txtSearchInput.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.SelectedIndex = 0;
                cbIsActive.Focus();
                return;
            }

            cbIsActive.Visible = false;
            txtSearchInput.Visible = true;
            txtSearchInput.Focus();
            txtSearchInput.Text = "";
        }

        private void _FilterData()
        {
            string FilterColumn = "";
            switch (cbFilter.Text)
            {
                case "Int.License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    FilterColumn = "ApplicationID";
                    break;
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "L.License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtSearchInput.Text == "" || FilterColumn == "None")
            {
                _dtInternationalLicenseApplicationsData.DefaultView.RowFilter = "";
            }
            else
            {
                _dtInternationalLicenseApplicationsData.DefaultView.RowFilter = $"{FilterColumn} = {txtSearchInput.Text}";
            }
            lblRecord.Text = dgvInternationalLicenseApplicationsList.RowCount.ToString();
        }

    }
}
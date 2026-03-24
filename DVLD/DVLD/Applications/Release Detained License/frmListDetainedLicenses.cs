using System;
using System.Data;
using System.Windows.Forms;
using DVLD.People;
using DVLD.Licenses;
using DVLD.Licenses.Local_Licenses;
using DVLD.Licenses.Detain_License;
using DVLD_Business_Layer;

namespace DVLD.Applications.Release_Detained_License
{
    public partial class frmListDetainedLicenses : frmBaseForm
    {
        DataTable _dtDetainedLicensesList;

        public frmListDetainedLicenses()
        {
            InitializeComponent();
        }

        private void frmListDetainedLicenses_Load(object sender, EventArgs e)
        {
            _LoadDetainedLicensesData();
            _FormatDataGridColumns();
            _LoadFiltersOptions();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ToogleFilters();
            _FilterData();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbIsReleased.Text)
            {
                case "Yes":
                    _dtDetainedLicensesList.DefaultView.RowFilter = "IsReleased = 1";
                    break;
                case "No":
                    _dtDetainedLicensesList.DefaultView.RowFilter = "IsReleased = 0";
                    break;
                default:
                    _dtDetainedLicensesList.DefaultView.RowFilter = "";
                    break;
            }
            lblRecord.Text = dgvDetainedLicensesList.RowCount.ToString();
        }

        private void txtSearchInput_TextChanged(object sender, EventArgs e)
        {
            _FilterData();
        }

        private void txtSearchInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Detain ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
                return;
            }

            if (cbFilter.Text == "Full Name")
            {
                e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar);
                return;
            }

            // Handle Nationaal Number Input
            if (txtSearchInput.Text.Length == 0)
            {
                e.Handled = e.KeyChar != 'N';
            }
            else
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void btnReleaseLicense_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.OnDataUpdated += _LoadDetainedLicensesData;
            frm.ShowDialog();
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.OnDataUpdated += _LoadDetainedLicensesData;
            frm.ShowDialog();
        }

        private void dgvDetainedLicensesList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo Location = dgvDetainedLicensesList.HitTest(e.X, e.Y);

                if (Location.RowIndex >= 0)
                {
                    dgvDetainedLicensesList.ClearSelection();
                    dgvDetainedLicensesList.Rows[Location.RowIndex].Selected = true;
                    releaseDetainedLicenseToolStripMenuItem.Enabled = !(bool)dgvDetainedLicensesList.SelectedRows[0].Cells["IsReleased"].Value;
                }
            }
        }

        private void ShowPersonDetails_Click(object sender, EventArgs e)
        {
            string PersonNationalNo = dgvDetainedLicensesList.SelectedRows[0].Cells["NationalNo"].Value.ToString();
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonNationalNo);
            frm.ShowDialog();
        }

        private void ShowLicenseDetails_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicensesList.SelectedRows[0].Cells["LicenseID"].Value;
            frmShowLocalLicenseInfo frm = new frmShowLocalLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void ShowPersonLicenseHistory_Click(object sender, EventArgs e)
        {
            string PersonNationalNo = dgvDetainedLicensesList.SelectedRows[0].Cells["NationalNo"].Value.ToString();
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonNationalNo);
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicensesList.SelectedRows[0].Cells["LicenseID"].Value;
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication(LicenseID);
            frm.OnDataUpdated += _LoadDetainedLicensesData;
            frm.ShowDialog();
        }

        private void _LoadDetainedLicensesData()
        {
            _dtDetainedLicensesList = clsDetainedLicense.GetAllDetainedLicensesApplications();
            dgvDetainedLicensesList.DataSource = _dtDetainedLicensesList;
            lblRecord.Text = dgvDetainedLicensesList.RowCount.ToString();
        }

        private void _FormatDataGridColumns()
        {
            if (dgvDetainedLicensesList.RowCount > 0)
            {
                dgvDetainedLicensesList.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicensesList.Columns[0].Width = 75;

                dgvDetainedLicensesList.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicensesList.Columns[1].Width = 75;

                dgvDetainedLicensesList.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicensesList.Columns[2].Width = 130;

                dgvDetainedLicensesList.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicensesList.Columns[3].Width = 90;

                dgvDetainedLicensesList.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicensesList.Columns[4].Width = 80;

                dgvDetainedLicensesList.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicensesList.Columns[5].Width = 130;

                dgvDetainedLicensesList.Columns[6].HeaderText = "N.No.";
                dgvDetainedLicensesList.Columns[6].Width = 80;

                dgvDetainedLicensesList.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicensesList.Columns[7].Width = 220;

                dgvDetainedLicensesList.Columns[8].HeaderText = "Release App.ID";
                dgvDetainedLicensesList.Columns[8].Width = 110;
            }
        }

        private void _LoadFiltersOptions()
        {
            cbFilter.Items.Add("None");
            cbFilter.Items.Add("Detain ID");
            cbFilter.Items.Add("Is Released");
            cbFilter.Items.Add("National No.");
            cbFilter.Items.Add("Full Name");

            cbFilter.SelectedIndex = 0;

            cbIsReleased.Items.Add("All");
            cbIsReleased.Items.Add("Yes");
            cbIsReleased.Items.Add("No");

            cbIsReleased.SelectedIndex = 0;
        }

        private void _ToogleFilters()
        {
            if (cbFilter.Text == "None")
            {
                txtSearchInput.Visible = false;
                cbIsReleased.Visible = false;
                return;
            }

            if (cbFilter.Text == "Is Released")
            {
                txtSearchInput.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;
                return;
            }
            cbIsReleased.Visible = false;
            txtSearchInput.Visible = true;
            txtSearchInput.Focus();
            txtSearchInput.Text = "";
        }

        private void _FilterData()
        {
            string FilterColumn = "";

            switch (cbFilter.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                default:
                    FilterColumn = "";
                    break;
            }

            if (FilterColumn == "" || txtSearchInput.Text == "")
            {
                _dtDetainedLicensesList.DefaultView.RowFilter = "";
            }
            else if (FilterColumn == "DetainID")
            {
                _dtDetainedLicensesList.DefaultView.RowFilter = $"{FilterColumn} = {txtSearchInput.Text}";
            }
            else
            {
                _dtDetainedLicensesList.DefaultView.RowFilter = $"{FilterColumn} Like '%{txtSearchInput.Text}%'";
            }
            lblRecord.Text = dgvDetainedLicensesList.RowCount.ToString();
        }
    }
}

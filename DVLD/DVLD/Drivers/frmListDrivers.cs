using System;
using System.Data;
using System.Windows.Forms;
using DVLD.People;
using DVLD.Licenses;
using DVLD_Business_Layer;

namespace DVLD.Drivers
{
    public partial class frmListDrivers : DVLD.frmBaseForm
    {
        private DataTable _dtDriversData;

        public frmListDrivers()
        {
            InitializeComponent();
        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            _LoadDriversData();
            _FormatDataGridColumns();
            _LoadFilterOptions();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ToggleSearchField();
        }

        private void txtSearchInput_TextChanged(object sender, EventArgs e)
        {
            _FilterData();
        }

        private void ShowPersonInfo_Click(object sender, EventArgs e)
        {
            int PersonID = int.Parse(dgvDriversList.CurrentRow.Cells[1].Value.ToString());
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = int.Parse(dgvDriversList.CurrentRow.Cells[1].Value.ToString());
            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }

        private void dgvDriversList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo Location = dgvDriversList.HitTest(e.X, e.Y);
                if (Location.RowIndex >= 0)
                {
                    dgvDriversList.ClearSelection();
                    dgvDriversList.Rows[Location.RowIndex].Selected = true;
                }
            }
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

        private void _LoadFilterOptions()
        {
            cbFilter.Items.Add("None");
            cbFilter.Items.Add("Driver ID");
            cbFilter.Items.Add("Person ID");
            cbFilter.Items.Add("National No.");
            cbFilter.Items.Add("Full Name");
            cbFilter.SelectedIndex = 0;
        }

        private void _LoadDriversData()
        {
            _dtDriversData = clsDriver.GetAllDrivers();
            dgvDriversList.DataSource = _dtDriversData;
            lblRecord.Text = dgvDriversList.RowCount.ToString();
        }

        private void _FormatDataGridColumns()
        {
            if (dgvDriversList.RowCount > 0)
            {
                dgvDriversList.Columns[0].HeaderText = "Driver ID";
                dgvDriversList.Columns[0].Width = 90;

                dgvDriversList.Columns[1].HeaderText = "Person ID";
                dgvDriversList.Columns[1].Width = 90;

                dgvDriversList.Columns[2].HeaderText = "National No.";
                dgvDriversList.Columns[2].Width = 100;

                dgvDriversList.Columns[3].HeaderText = "Full Name";
                dgvDriversList.Columns[3].Width = 250;

                dgvDriversList.Columns[4].HeaderText = "Date";
                dgvDriversList.Columns[4].Width = 130;

                dgvDriversList.Columns[5].HeaderText = "Active Licenses";
                dgvDriversList.Columns[5].Width = 110;
            }
        }

        private void _ToggleSearchField()
        {
            txtSearchInput.Visible = (cbFilter.Text != "None");
            if (txtSearchInput.Visible)
            {
                txtSearchInput.Text = "";
                txtSearchInput.Focus();
            }
        }

        private void _FilterData()
        {
            string FilterColumn = "";

            switch (cbFilter.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtSearchInput.Text == "" || FilterColumn == "None")
            {
                _dtDriversData.DefaultView.RowFilter = "";
            }
            else if (FilterColumn == "DriverID" || FilterColumn == "PersonID")
            {
                _dtDriversData.DefaultView.RowFilter = $"{FilterColumn} = {txtSearchInput.Text}";
            }
            else
            {
                _dtDriversData.DefaultView.RowFilter = $"{FilterColumn} Like '%{txtSearchInput.Text}%'";
            }

            lblRecord.Text = dgvDriversList.RowCount.ToString();
        }

    }
}
using System;
using System.Windows.Forms;
using DVLD_Business_Layer;
using DVLD.Licenses.Local_Licenses;
using DVLD.Licenses.International_Licenses;

namespace DVLD.Licenses.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {
        private clsDriver Driver;

        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }

        private void ShowLocalLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value;
            frmShowLocalLicenseInfo frm = new frmShowLocalLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void ShowInternationalLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvInternationalLicensesHistory.CurrentRow.Cells[0].Value;
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void dgv_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo Location = dgv.HitTest(e.X, e.Y);
                if (Location.RowIndex >= 0)
                {
                    dgv.ClearSelection();
                    dgv.Rows[Location.RowIndex].Selected = true;
                }
            }
        }

        public void LoadByDriverID(int DriverID)
        {
            Driver = clsDriver.FindDriverByID(DriverID);
            if (Driver != null)
            {
                _LoadData();
            }
            else
            {
                Clear();
            }
        }

        public void LoadByPersonID(int PersonID)
        {
            Driver = clsDriver.FindDriverByPersonID(PersonID);
            if (Driver != null)
            {
                _LoadData();
            }
            else
            {
                Clear();
            }
        }

        public void Clear()
        {
            dgvLocalLicensesHistory.DataSource = null;
            dgvInternationalLicensesHistory.DataSource = null;
            lblLocalRecord.Text = "0";
            lblInternationalRecord.Text = "0";
        }

        private void _LoadData()
        {
            _LoadLocalLicensesInfo();
            _LoadInternationalLicenseInfo();
        }

        private void _LoadLocalLicensesInfo()
        {
            dgvLocalLicensesHistory.DataSource = Driver.GetLocalLicenses();

            lblLocalRecord.Text = dgvLocalLicensesHistory.Rows.Count.ToString();

            dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";
            dgvLocalLicensesHistory.Columns[0].Width = 85;

            dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";
            dgvLocalLicensesHistory.Columns[1].Width = 85;

            dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
            dgvLocalLicensesHistory.Columns[2].Width = 250;

            dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
            dgvLocalLicensesHistory.Columns[3].Width = 150;

            dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
            dgvLocalLicensesHistory.Columns[4].Width = 150;

            dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
            dgvLocalLicensesHistory.Columns[5].Width = 85;
        }

        private void _LoadInternationalLicenseInfo()
        {
            dgvInternationalLicensesHistory.DataSource = Driver.GetInternationalLicenses();
            lblInternationalRecord.Text = dgvInternationalLicensesHistory.Rows.Count.ToString();

            dgvInternationalLicensesHistory.Columns[0].HeaderText = "Int.License ID";
            dgvInternationalLicensesHistory.Columns[0].Width = 110;

            dgvInternationalLicensesHistory.Columns[1].HeaderText = "Application ID";
            dgvInternationalLicensesHistory.Columns[1].Width = 110;

            dgvInternationalLicensesHistory.Columns[2].HeaderText = "L.License ID";
            dgvInternationalLicensesHistory.Columns[2].Width = 100;

            dgvInternationalLicensesHistory.Columns[3].HeaderText = "Issue Date";
            dgvInternationalLicensesHistory.Columns[3].Width = 160;

            dgvInternationalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
            dgvInternationalLicensesHistory.Columns[4].Width = 160;

            dgvInternationalLicensesHistory.Columns[5].HeaderText = "Is Active";
            dgvInternationalLicensesHistory.Columns[5].Width = 85;
        }

    }
}
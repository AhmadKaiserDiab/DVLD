using System;
using DVLD_Business_Layer;

namespace DVLD.Tests.Test_Types
{
    public partial class frmListTestTypes : DVLD.frmBaseForm
    {
        public frmListTestTypes()
        {
            InitializeComponent();
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _LoadTestTypesData();
            _FormatDataGridColumns();
        }

        private void editTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TestTypeID = (int)dgvTestTypesList.CurrentRow.Cells[0].Value;
            frmEditTestType frm = new frmEditTestType((clsTestType.enTestType)TestTypeID);
            frm.ShowDialog();
            frmListTestTypes_Load(null, null);
        }

        private void _LoadTestTypesData()
        {
            dgvTestTypesList.DataSource = clsTestType.GetAllTestTypes();
            lblRecord.Text = dgvTestTypesList.RowCount.ToString();
        }

        private void _FormatDataGridColumns()
        {
            if (dgvTestTypesList.RowCount > 0)
            {
                dgvTestTypesList.Columns[0].HeaderText = "ID";
                dgvTestTypesList.Columns[0].Width = 120;

                dgvTestTypesList.Columns[1].HeaderText = "Title";
                dgvTestTypesList.Columns[1].Width = 200;

                dgvTestTypesList.Columns[2].HeaderText = "Description";
                dgvTestTypesList.Columns[2].Width = 400;

                dgvTestTypesList.Columns[3].HeaderText = "Fees";
                dgvTestTypesList.Columns[3].Width = 100;
            }
        }
    }
}
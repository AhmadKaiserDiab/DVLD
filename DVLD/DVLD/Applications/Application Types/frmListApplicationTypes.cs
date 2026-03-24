using System;
using DVLD_Business_Layer;

namespace DVLD.Applications.Application_Types
{
    public partial class frmListApplicationTypes : DVLD.frmBaseForm
    {
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _LoadApplicationTypesData();
            _FormatDataGridView();
        }

        private void EditApplicationType_Click(object sender, EventArgs e)
        {
            int TestTypeID = (int)dgvApplicationTypesList.CurrentRow.Cells[0].Value;
            frmEditApplicationType frm = new frmEditApplicationType(TestTypeID);
            frm.ShowDialog();
        }

        private void _LoadApplicationTypesData()
        {
            dgvApplicationTypesList.DataSource = clsApplicationType.GetAllApplicationTypes();
            lblRecord.Text = dgvApplicationTypesList.RowCount.ToString();
        }

        private void _FormatDataGridView()
        {
            if (dgvApplicationTypesList.Rows.Count > 0)
            {
                dgvApplicationTypesList.Columns[0].HeaderText = "ID";
                dgvApplicationTypesList.Columns[0].Width = 110;

                dgvApplicationTypesList.Columns[1].HeaderText = "Title";
                dgvApplicationTypesList.Columns[1].Width = 400;

                dgvApplicationTypesList.Columns[2].HeaderText = "Fees";
                dgvApplicationTypesList.Columns[2].Width = 100;
            }
        }
    }
}
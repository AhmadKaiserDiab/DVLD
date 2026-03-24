using System;
using System.IO;
using System.Data;
using System.Windows.Forms;
using DVLD_Business_Layer;
using DVLD.Global_Classes;

namespace DVLD.People
{
    public partial class frmListPeople : frmBaseForm
    {
        private DataTable _dtPeopleData;

        private void _LoadPeopleData()
        {
            _dtPeopleData = clsPerson.GetAllPeople();
            dgvPeopleList.DataSource = _dtPeopleData;
            lblRecord.Text = dgvPeopleList.RowCount.ToString();
        }

        private void _FormatDataGridColumns()
        {
            if (dgvPeopleList.Rows.Count != 0)
            {
                dgvPeopleList.Columns[0].HeaderText = "Person ID";
                dgvPeopleList.Columns[0].Width = 80;

                dgvPeopleList.Columns[1].HeaderText = "National No.";
                dgvPeopleList.Columns[1].Width = 90;

                dgvPeopleList.Columns[2].HeaderText = "First Name";
                dgvPeopleList.Columns[2].Width = 100;

                dgvPeopleList.Columns[3].HeaderText = "Second Name";
                dgvPeopleList.Columns[3].Width = 110;

                dgvPeopleList.Columns[4].HeaderText = "Third Name";
                dgvPeopleList.Columns[4].Width = 90;

                dgvPeopleList.Columns[5].HeaderText = "Last Name";
                dgvPeopleList.Columns[5].Width = 100;

                dgvPeopleList.Columns[6].HeaderText = "Gender";
                dgvPeopleList.Columns[6].Width = 80;

                dgvPeopleList.Columns[7].HeaderText = "Date Of Birth";
                dgvPeopleList.Columns[7].Width = 100;

                dgvPeopleList.Columns[8].HeaderText = "Nationality";
                dgvPeopleList.Columns[8].Width = 90;

                dgvPeopleList.Columns[9].HeaderText = "Phone";
                dgvPeopleList.Columns[9].Width = 80;

                dgvPeopleList.Columns[10].HeaderText = "Email";
                dgvPeopleList.Columns[10].Width = 130;
            }
        }

        private void _LoadFilterOptions()
        {
            cbFilter.Items.Add("None");
            cbFilter.Items.Add("Person ID");
            cbFilter.Items.Add("National No.");
            cbFilter.Items.Add("First Name");
            cbFilter.Items.Add("Second Name");
            cbFilter.Items.Add("Third Name");
            cbFilter.Items.Add("Last Name");
            cbFilter.Items.Add("Phone");
            cbFilter.Items.Add("Email");

            cbFilter.SelectedIndex = 0;
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
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "National No.":
                    FilterColumn = "NationalNo";
                    break;
                case "First Name":
                    FilterColumn = "FirstName";
                    break;
                case "Second Name":
                    FilterColumn = "SecondName";
                    break;
                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;
                case "Last Name":
                    FilterColumn = "LastName";
                    break;
                case "Phone":
                    FilterColumn = "Phone";
                    break;
                case "Email":
                    FilterColumn = "Email";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtSearchInput.Text == "" || FilterColumn == "None")
            {
                _dtPeopleData.DefaultView.RowFilter = "";
            }
            else if (FilterColumn == "PersonID")
            {
                _dtPeopleData.DefaultView.RowFilter = $"{FilterColumn} = {txtSearchInput.Text}";
                //_dtPeopleData.DefaultView.RowFilter = $"Convert([PersonID] , 'System.String') like '%{txtSearchInput.Text.ToString()}%'";
            }
            else
            {
                _dtPeopleData.DefaultView.RowFilter = $"{FilterColumn} Like '%{txtSearchInput.Text}%'";
            }

            lblRecord.Text = dgvPeopleList.RowCount.ToString();
        }

        public frmListPeople()
        {
            InitializeComponent();
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _LoadPeopleData();
            _LoadFilterOptions();
            _FormatDataGridColumns();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ToggleSearchField();
        }

        private void txtSearchInput_TextChanged(object sender, EventArgs e)
        {
            _FilterData();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.OnDataUpdated += frmAddUpdatePerson_OnDataUpdated;
            frm.ShowDialog();
        }

        private void ShowPersonDetails_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeopleList.SelectedRows[0].Cells[0].Value;
            frmShowPersonInfo frm = new frmShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void AddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.OnDataUpdated += frmAddUpdatePerson_OnDataUpdated;
            frm.ShowDialog();
        }

        private void EditPerson_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeopleList.SelectedRows[0].Cells[0].Value;
            frmAddUpdatePerson frm = new frmAddUpdatePerson(PersonID);
            frm.OnDataUpdated += frmAddUpdatePerson_OnDataUpdated;
            frm.ShowDialog();
        }

        private void DeletePerson_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeopleList.SelectedRows[0].Cells[0].Value;

            if (
                MessageBox.Show(
                    text: $" Are You Sure You Want To Delete Person [ {PersonID} ]",
                    caption: "Confirm Delete",
                    icon: MessageBoxIcon.Question,
                    buttons: MessageBoxButtons.YesNo
                    ) == DialogResult.Yes)
            {
                string ImagePath = clsPerson.Find(PersonID).ImagePath;
                if (clsPerson.DeletePerson(PersonID))
                {
                    MessageBox.Show(
                        text: "Person Deleted Successfully",
                        caption: "Successful",
                        icon: MessageBoxIcon.Asterisk,
                        buttons: MessageBoxButtons.OK);

                    if (File.Exists(Path.Combine(clsGlobal.FolderPath, ImagePath)))
                    {
                        File.Delete(Path.Combine(clsGlobal.FolderPath, ImagePath));
                    }
                    _LoadPeopleData();
                }
                else
                {
                    MessageBox.Show(
                        text: "Person Was Not Deleted Because it Has Data Linked To It",
                        caption: "Error",
                        icon: MessageBoxIcon.Error,
                        buttons: MessageBoxButtons.OK);
                }
            }
        }

        private void PhoneCall_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                caption: "Not Ready",
                text: "This Feature isn't Implemented Yet!!",
                buttons: MessageBoxButtons.OK,
                icon: MessageBoxIcon.Asterisk
                );
        }

        private void SendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                   caption: "Not Ready",
                   text: "This Feature isn't Implemented Yet!!",
                   buttons: MessageBoxButtons.OK,
                   icon: MessageBoxIcon.Asterisk
                   );
        }

        private void dgvPeopleList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hit = dgvPeopleList.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    dgvPeopleList.ClearSelection();
                    dgvPeopleList.Rows[hit.RowIndex].Selected = true;

                }
            }
        }

        private void frmAddUpdatePerson_OnDataUpdated()
        {
            _LoadPeopleData();
        }

        private void txtSearchInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "Person ID")
            {
                if (!Char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            if (cbFilter.Text == "National No.")
            {
                if (cbFilter.Text.Length == 0 && e.KeyChar != 'N'
                    ||
                    cbFilter.Text.Length > 0 && !Char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }
}

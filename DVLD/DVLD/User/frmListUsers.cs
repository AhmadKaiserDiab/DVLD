using System;
using System.Data;
using System.Windows.Forms;
using DVLD_Business_Layer;

namespace DVLD.Users
{
    public partial class frmListUsers : DVLD.frmBaseForm
    {
        private DataTable _dtUserData;

        public frmListUsers()
        {
            InitializeComponent();
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _LoadUsersData();
            _FormatDataGridColumns();
            _LoadFiltersOptions();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ToggleSearchFields();
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
                _dtUserData.DefaultView.RowFilter = "";
            }
            else
            {
                _dtUserData.DefaultView.RowFilter = $"IsActive = {FilterValue}";
            }
            lblRecord.Text = dgvUsersList.RowCount.ToString();
        }

        private void txtSearchInput_TextChanged(object sender, EventArgs e)
        {
            _FilterData();
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.OnDataUpdated += _LoadUsersData;
            frm.ShowDialog();
        }

        private void ShowUserDetails_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsersList.SelectedRows[0].Cells[0].Value;
            frmUserInfo frm = new frmUserInfo(UserID);
            frm.ShowDialog();
        }

        private void AddNewUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUser frm = new frmAddUpdateUser();
            frm.OnDataUpdated += _LoadUsersData;
            frm.ShowDialog();
        }

        private void EditUser_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsersList.SelectedRows[0].Cells[0].Value;
            frmAddUpdateUser frm = new frmAddUpdateUser(UserID);
            frm.OnDataUpdated += _LoadUsersData;
            frm.ShowDialog();
        }

        private void DeleteUser_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsersList.SelectedRows[0].Cells[0].Value;

            if (
            MessageBox.Show(
                text: $" Are You Sure You Want To Delete User [{UserID}]",
                caption: "Confirm Delete",
                icon: MessageBoxIcon.Question,
                buttons: MessageBoxButtons.YesNo
                ) == DialogResult.Yes)
            {
                if (clsUser.DeleteUser(UserID))
                {
                    MessageBox.Show(
                        text: "User Deleted Successfully",
                        caption: "Successful",
                        icon: MessageBoxIcon.Asterisk,
                        buttons: MessageBoxButtons.OK);
                    _LoadUsersData();
                }
                else
                {
                    MessageBox.Show(
                        text: "User Was Not Deleted Because it Has Data Linked To It",
                        caption: "Error",
                        icon: MessageBoxIcon.Error,
                        buttons: MessageBoxButtons.OK);
                }
            }
        }

        private void ChangePassWord_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsersList.SelectedRows[0].Cells[0].Value;
            frmChangePassword frm = new frmChangePassword(UserID);
            frm.ShowDialog();
        }

        private void txtSearchInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.SelectedText == "User ID"
                || cbFilter.SelectedText == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar)
                    && !char.IsControl(e.KeyChar);
            }
        }

        private void dgvUsersList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hit = dgvUsersList.HitTest(e.X, e.Y);
                if (hit.RowIndex >= 0)
                {
                    dgvUsersList.ClearSelection();
                    dgvUsersList.Rows[hit.RowIndex].Selected = true;

                }
            }
        }

        private void _LoadUsersData()
        {
            _dtUserData = clsUser.GetAllUsers();
            dgvUsersList.DataSource = _dtUserData;
            lblRecord.Text = dgvUsersList.RowCount.ToString();
        }

        private void _FormatDataGridColumns()
        {
            if (dgvUsersList.Rows.Count != 0)
            {
                dgvUsersList.Columns[0].HeaderText = "User ID";
                dgvUsersList.Columns[0].Width = 90;

                dgvUsersList.Columns[1].HeaderText = "Person ID";
                dgvUsersList.Columns[1].Width = 90;

                dgvUsersList.Columns[2].HeaderText = "Full Name";
                dgvUsersList.Columns[2].Width = 220;

                dgvUsersList.Columns[3].HeaderText = "Username";
                dgvUsersList.Columns[3].Width = 120;

                dgvUsersList.Columns[4].HeaderText = "Is Active";
                dgvUsersList.Columns[4].Width = 60;
            }
        }

        private void _LoadFiltersOptions()
        {
            cbFilter.Items.Add("None");
            cbFilter.Items.Add("User ID");
            cbFilter.Items.Add("Person ID");
            cbFilter.Items.Add("Full Name");
            cbFilter.Items.Add("Username");
            cbFilter.Items.Add("Is Active");
            cbFilter.SelectedIndex = 0;

            cbIsActive.Items.Add("All");
            cbIsActive.Items.Add("Yes");
            cbIsActive.Items.Add("No");
            cbIsActive.SelectedIndex = 0;
        }

        private void _ToggleSearchFields()
        {
            if (cbFilter.Text == "None")
            {
                txtSearchInput.Visible = false;
                cbIsActive.Visible = false;
                return;
            }
            else
            {
                if (cbFilter.Text == "Is Active")
                {
                    txtSearchInput.Visible = false;
                    cbIsActive.Visible = true;
                    cbIsActive.Focus();
                    cbIsActive.SelectedIndex = cbIsActive.FindString("All");
                }
                else
                {
                    txtSearchInput.Visible = true;
                    txtSearchInput.Text = "";
                    txtSearchInput.Focus();
                    cbIsActive.Visible = false;
                }
            }
        }

        private void _FilterData()
        {
            string FilterColumn = "";
            switch (cbFilter.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Username":
                    FilterColumn = "Username";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtSearchInput.Text == "" || FilterColumn == "None")
            {
                _dtUserData.DefaultView.RowFilter = "";
            }
            else if (FilterColumn == "User ID" ||
                    FilterColumn == "Person ID")
            {
                _dtUserData.DefaultView.RowFilter = $"{FilterColumn} = {txtSearchInput.Text}";
            }
            else
            {
                _dtUserData.DefaultView.RowFilter = $"{FilterColumn} Like '%{txtSearchInput.Text}%'";
            }
            lblRecord.Text = dgvUsersList.RowCount.ToString();
        }

    }
}
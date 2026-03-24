using System;
using System.Windows.Forms;
using System.ComponentModel;
using DVLD_Business_Layer;

namespace DVLD.Users
{
    public partial class frmAddUpdateUser : DVLD.frmBaseForm
    {
        private enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        private int _UserID = -1;
        private clsUser _User;

        public frmAddUpdateUser()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            ctrlPersonCardWithFilter1.OnPersonFound += _IsPersonFound;
        }

        public frmAddUpdateUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _Mode = enMode.Update;
            ctrlPersonCardWithFilter1.OnPersonFound += _IsPersonFound;
        }

        private void frmAddUpdateUser_Load(object sender, EventArgs e)
        {
            _ResetDefualtValues();
            if (_Mode == enMode.Update)
            {
                _LoadUserInfo();
            }
        }

        private void tcUserInfo_Selecting(object sender, TabControlCancelEventArgs e)
        {
            int SelectedPersonID = ctrlPersonCardWithFilter1.SelectedPersonID;

            if (_Mode == enMode.AddNew)
            {
                if (SelectedPersonID == -1)
                {
                    MessageBox.Show(
                    "Select a Person to Proceed",
                    "Error",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
                else if (clsUser.IsUserExist(SelectedPersonID))
                {
                    MessageBox.Show(
                    "Selected Person Already Has a user, Select Another One.",
                    "Select Another Person",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error);
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "" || 
                txtPassWord.Text == "" || 
                txtConfirmPassWord.Text == "")
            {
                MessageBox.Show("Can't Save while There is an Empty Fields");
                return;
            }
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Can't Save while There is an Error on Input Fields");
                return;
            }
            else
            {
                _SaveUserInfo();

                if (_User.Save())
                {
                    if (_Mode == enMode.AddNew)
                    {
                        MessageBox.Show($"User Added Successfully With ID = {_User.UserID}", "Done");
                        _Mode = enMode.Update;
                    }
                    else
                    {
                        MessageBox.Show($"User Updated Successfully", "Done");
                    }
                    IsDataUpdated();
                    _UserID = _User.UserID;
                    frmAddUpdateUser_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Save Failed", "Error");
                }
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            string UserName = txtUserName.Text.Trim();
            if (UserName == "")
            {
                errorProvider1.SetError(txtUserName, "This Field Can't Be Empty");
            }
            else if (!char.IsLetter(UserName[0]))
            {
                errorProvider1.SetError(txtUserName, "UserName Should Start With Letters Only");
            }
            else if (clsUser.IsUserExist(UserName) && _User.UserName != UserName)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "UserName Already Exist, Choose Another One");
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }
        }

        private void txtPassWord_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassWord.Text == string.Empty)
            {
                errorProvider1.SetError(txtPassWord, "This Can't Be Empty");
            }
            else
            {
                errorProvider1.SetError(txtPassWord, null);
            }
            txtConfirmPassWord.Clear();
        }

        private void txtConfirmPassWord_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassWord.Text != txtConfirmPassWord.Text)
            {
                errorProvider1.SetError(txtConfirmPassWord, "Confirm PassWord Doesn't Match PassWord");
            }
            else if (txtConfirmPassWord.Text == string.Empty)
            {
                errorProvider1.SetError(txtConfirmPassWord, "This Can't Be Empty");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassWord, null);
            }
        }

        private void _ResetDefualtValues()
        {
            if (_Mode == enMode.AddNew)
            {
                this.Text = lblTitle.Text = "Add New User";
                _User = new clsUser();
                tpLoginInfo.Enabled = false;
            }
            else
            {
                this.Text = lblTitle.Text = "Update User";
                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;
            }
            txtUserName.Text = "";
            txtPassWord.Text = "";
            txtConfirmPassWord.Text = "";
            chkIsActive.Checked = true;
        }

        private void _LoadUserInfo()
        {
            _User = clsUser.FindUserByID(_UserID);
            ctrlPersonCardWithFilter1.LoadPersonData(_User.PersonInfo);
            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassWord.Text = _User.PassWord;
            txtConfirmPassWord.Text = _User.PassWord;
            chkIsActive.Checked = _User.IsActive;
            tpLoginInfo.Enabled = true;
            btnSave.Enabled = true;
        }

        private void _IsPersonFound(bool result)
        {
            tpLoginInfo.Enabled = result;
            btnSave.Enabled = result;
        }

        private void _SaveUserInfo()
        {
            _User.PersonID = ctrlPersonCardWithFilter1.SelectedPersonID;
            _User.UserName = txtUserName.Text;
            _User.PassWord = txtPassWord.Text;
            _User.IsActive = chkIsActive.Checked;
        }
    }
}

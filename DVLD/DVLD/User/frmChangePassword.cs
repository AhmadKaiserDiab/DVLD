using System;
using System.Windows.Forms;
using System.ComponentModel;
using DVLD_Business_Layer;

namespace DVLD.Users
{
    public partial class frmChangePassword : DVLD.frmBaseForm
    {
        int _UserID;
        clsUser _User;

        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            this._UserID = UserID;
        }

        public frmChangePassword(clsUser User)
        {
            InitializeComponent();
            this._User = User;
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            LoadUserData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.Validate())
            {
                MessageBox.Show("Can't Save While There Is Erorr On Input Fields.");
                return;
            }
            _User.PassWord = txtNewPassWord.Text.Trim();
            if (_User.ChangePassWord())
            {
                MessageBox.Show("PassWord Updated Succeffully");
                ResetInputFields();
            }
        }

        private void txtCurrentPassWord_Validating(object sender, CancelEventArgs e)
        {
            if (txtCurrentPassWord.Text == string.Empty)
            {
                errorProvider1.SetError(txtCurrentPassWord, "This Field Can't Be Empty!!!");
            }
            else if (txtCurrentPassWord.Text != _User.PassWord)
            {
                errorProvider1.SetError(txtCurrentPassWord, "Current Passord Is Wrong!!!");
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassWord, null);
            }
        }

        private void txtNewPassWord_Validating(object sender, CancelEventArgs e)
        {
            if (txtNewPassWord.Text == string.Empty)
            {
                errorProvider1.SetError(txtNewPassWord, "This Can't Be Empty!!!");
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassWord, null);
            }
            txtConfirmPassWord.Text = "";
        }

        private void txtConfirmPassWord_Validating(object sender, CancelEventArgs e)
        {
            if (txtNewPassWord.Text != string.Empty
                &&
                txtNewPassWord.Text != txtConfirmPassWord.Text)
            {
                errorProvider1.SetError(txtConfirmPassWord, "Confirm PassWord Doesn't Match New PassWord!!!");
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassWord, null);
            }
        }
        
        private void LoadUserData()
        {
            if (_User == null)
            {
                _User = clsUser.FindUserByID(_UserID);
            }
            ctrlUserCard1.LoadUserData(_User);
        }

        private void ResetInputFields()
        {
            txtCurrentPassWord.Focus();
            txtCurrentPassWord.Text = "";
            txtNewPassWord.Text = "";
            txtConfirmPassWord.Text = "";
        }

    }
}

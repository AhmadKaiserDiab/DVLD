using System;
using System.Windows.Forms;
using DVLD_Business_Layer;
using DVLD.Global_Classes;

namespace DVLD.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            _LoadLoginCredintials();
        }

        private void _LoadLoginCredintials()
        {
            string Username = "", Password = "";

            if (clsGlobal.GetStoredCredentials(ref Username, ref Password))
            {
                txtUsername.Text = Username;
                txtPassWord.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
            {
                _ResetLoginForm();
            }
        }

        private void _ResetLoginForm()
        {
            txtUsername.Clear();
            txtPassWord.Clear();
            txtUsername.Focus();
            chkRememberMe.Checked = false;
        }

        private void _SaveLoginCredintials()
        {
            if (chkRememberMe.Checked)
            {
                clsGlobal.RememberUsernameAndPassword(txtUsername.Text, txtPassWord.Text);
            }
            else
            {
                clsGlobal.RememberUsernameAndPassword("", "");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser User =
                    clsUser.VerifyUserLogin(txtUsername.Text.Trim(), txtPassWord.Text.Trim());
            if (User != null)
            {
                if (User.IsActive)
                {
                    clsGlobal.CurrentUser = User;
                    _SaveLoginCredintials();
                    Hide();
                    frmMain frm = new frmMain(this);
                    frm.Show();
                }
                else
                {
                    MessageBox.Show(
                        text: "This Account Is Not Active, Contact The Admin",
                        caption: "Error, Unactive Account",
                        icon: MessageBoxIcon.Error,
                        buttons: MessageBoxButtons.OK
                        );
                    _ResetLoginForm();
                }
            }
            else
            {
                MessageBox.Show(
                    text: "Invalid Username/PassWord",
                    caption: "Wrong Credintials",
                    icon: MessageBoxIcon.Error,
                    buttons: MessageBoxButtons.OK
                    );
                _ResetLoginForm();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
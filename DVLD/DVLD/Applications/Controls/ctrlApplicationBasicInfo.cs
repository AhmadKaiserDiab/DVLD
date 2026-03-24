using System.Windows.Forms;
using DVLD.People;
using DVLD_Business_Layer;

namespace DVLD.Applications.Controls
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {
        clsApplication Application;

        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonInfo frm = new frmShowPersonInfo(Application.ApplicantPerson);
            frm.ShowDialog();
        }

        public new void Load(clsApplication App)
        {
            this.Application = App;
            if (Application == null)
            {
                MessageBox.Show("Application Not Found");
                _ResetApplicationInfo();
            }
            else
            {
                _LoadApplicationInfo();
            }
        }

        private void _ResetApplicationInfo()
        {
            lblApplicationID.Text = "[????]";
            lblStatus.Text = "[????]";
            lblType.Text = "[????]";
            lblFees.Text = "[????]";
            lblApplicant.Text = "[????]";
            lblDate.Text = "[????]";
            lblStatusDate.Text = "[????]";
            lblCreatedByUser.Text = "[????]";
        }

        private void _LoadApplicationInfo()
        {
            lblApplicationID.Text = Application.ApplicationID.ToString();
            lblStatus.Text = Application.StatusText;
            lblFees.Text = Application.PaidFees.ToString();
            lblType.Text = Application.ApplicationTypeInfo.Title;
            lblApplicant.Text = Application.ApplicantPerson.FirstName;
            lblDate.Text = Application.ApplicationDate.ToShortDateString();
            lblStatusDate.Text = Application.LastStatusDate.ToShortDateString();
            lblCreatedByUser.Text = Application.CreatedByUserInfo.UserName;
        }

    }
}
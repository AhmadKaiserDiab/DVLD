using System.Windows.Forms;
using DVLD_Business_Layer;

namespace DVLD.Users
{
    public partial class ctrlUserCard : UserControl
    {
        clsUser _User;
        public ctrlUserCard()
        {
            InitializeComponent();
        }

        internal void LoadUserData(clsUser User)
        {
            _User = User;
            ctrlPersonCard1.LoadPersonInfo(_User.PersonInfo);
            //ctrlPersonCard1.OnDataChanged += IsDataChanged;
            lblUserId.Text = User.UserID.ToString();
            lblUserName.Text = User.UserName;
            lblIsActive.Text = User.IsActive ? "Yes" : "No";
        }

        private void IsDataChanged(bool result)
        {
            if(result)
            {
                LoadUserData(_User);
            }
        }
    }
}
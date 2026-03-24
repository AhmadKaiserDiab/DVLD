using System;
using DVLD_Business_Layer;

namespace DVLD.Users
{
    public partial class frmUserInfo : DVLD.frmBaseForm
    {
        private int _UserID;
        private clsUser _User;

        public frmUserInfo(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        public frmUserInfo(clsUser User)
        {
            InitializeComponent();
            _User = User;
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            if (_User == null)
            {
                _User = clsUser.FindUserByID(_UserID);
            }
            ctrlUserCard1.LoadUserData(_User);
        }
    }
}

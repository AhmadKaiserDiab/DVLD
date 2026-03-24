using DVLD_Business_Layer;

namespace DVLD.People
{
    public partial class frmShowPersonInfo : DVLD.frmBaseForm
    {
        public frmShowPersonInfo(int PersonID)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }
        
        public frmShowPersonInfo(string NationalNo)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadPersonInfo(NationalNo);
        }

        public frmShowPersonInfo(clsPerson Person)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadPersonInfo(Person);
        }
    }
}
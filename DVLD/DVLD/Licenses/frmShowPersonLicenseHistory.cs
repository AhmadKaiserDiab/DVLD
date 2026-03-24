using System;
using DVLD_Business_Layer;

namespace DVLD.Licenses
{
    public partial class frmShowPersonLicenseHistory : DVLD.frmBaseForm
    {
        clsPerson Person;

        public frmShowPersonLicenseHistory()
        {
            InitializeComponent();
            ctrlPersonCardWithFilter1.OnPersonFound += ShowPersonLicenses;
        }

        public frmShowPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();
            Person = clsPerson.Find(PersonID);
        }

        public frmShowPersonLicenseHistory(string NationalNo)
        {
            InitializeComponent();
            Person = clsPerson.Find(NationalNo);
        }

        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            _LoadForm();
        }

        private void _LoadForm()
        {
            if (Person != null)
            {
                ctrlPersonCardWithFilter1.LoadPersonData(Person);
                ctrlDriverLicenses1.LoadByPersonID(ctrlPersonCardWithFilter1.SelectedPersonID);
            }
        }

        private void ShowPersonLicenses(bool result)
        {
            if (result)
            {
                ctrlDriverLicenses1.LoadByPersonID(ctrlPersonCardWithFilter1.SelectedPersonID);
            }
            else
            {
                ctrlDriverLicenses1.Clear();
            }
        }

    }
}
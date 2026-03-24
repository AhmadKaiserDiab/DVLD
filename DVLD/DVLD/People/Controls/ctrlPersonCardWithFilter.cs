using System;
using System.Windows.Forms;
using DVLD_Business_Layer;

namespace DVLD.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public int SelectedPersonID { get { return ctrlPersonCard1.PersonID; } }
        public clsPerson SelectedPersonInfo
        {
            get
            {
                return ctrlPersonCard1.SelectedPersonInfo;
            }
        }
        public bool EnableFilter
        {
            set
            {
                grbFilter.Enabled = value;
            }
        }

        public event Action<bool> OnPersonFound;
        protected virtual void IsPersonFound(bool IsPersonFound)
        {
            Action<bool> handler = OnPersonFound;

            if (handler != null)
            {
                handler(IsPersonFound);
            }
        }

        private void _LoadComboBoxOptions()
        {
            cbFilter.Items.Add("Person ID");
            cbFilter.Items.Add("National No.");
            cbFilter.SelectedIndex = 0;
        }

        private void _FindPerson()
        {
            clsPerson P = null;
            if (txtFilterValue.Text != "")
            {
                switch (cbFilter.Text)
                {
                    case "Person ID":
                        {
                            P = clsPerson.Find(int.Parse(txtFilterValue.Text));
                            ctrlPersonCard1.LoadPersonInfo(P);
                            break;
                        }
                    case "National No.":
                        {
                            P = clsPerson.Find(txtFilterValue.Text);
                            ctrlPersonCard1.LoadPersonInfo(P);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            IsPersonFound(P != null);
            ctrlPersonCard1.EnableEdit = (P != null);
        }

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            _LoadComboBoxOptions();
            txtFilterValue.Focus();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            _FindPerson();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ReturnedPersonData += frm_GetNewPersonInfo;
            frm.ShowDialog();
        }

        private void frm_GetNewPersonInfo(object sender, clsPerson Person)
        {
            ctrlPersonCard1.LoadPersonInfo(Person);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (13))
            {
                btnFind.PerformClick();
            }
            if (cbFilter.Text == "National No."
                && txtFilterValue.Text.Length == 0)
            {
                if (e.KeyChar != 'N')
                {
                    e.Handled = true;
                }
            }
            else
            {
                if (!char.IsDigit(e.KeyChar)
                    && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        public void LoadPersonData(clsPerson Person)
        {
            if (Person == null)
            {
                return;
            }
            IsPersonFound(true);
            ctrlPersonCard1.LoadPersonInfo(Person);
            ctrlPersonCard1.EnableEdit = true;
            txtFilterValue.Text = Person.PersonID.ToString();
            cbFilter.SelectedIndex = 0;
            EnableFilter = false;
        }

    }
}
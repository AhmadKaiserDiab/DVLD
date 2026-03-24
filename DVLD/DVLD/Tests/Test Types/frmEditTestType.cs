using System;
using System.Windows.Forms;
using System.ComponentModel;
using DVLD_Business_Layer;
using DVLD.Global_Classes;

namespace DVLD.Tests.Test_Types
{
    public partial class frmEditTestType : DVLD.frmBaseForm
    {
        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;
        clsTestType _TestType;

        public frmEditTestType(clsTestType.enTestType TestTypeID)
        {
            _TestTypeID = TestTypeID;
            InitializeComponent();
        }

        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            _LoadTestTypeInfo();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show(
                    text: "Some Fields are not Valid!",
                    caption: "Validation Error",
                    buttons: MessageBoxButtons.OK, 
                    icon: MessageBoxIcon.Error);
                return;
            }

            _SaveTestType();
        }

        private void txtTestTypeTitle_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTestTypeTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTestTypeTitle, "Title Cannot be Empty!");
            }
            else
            {
                errorProvider1.SetError(txtTestTypeTitle, null);
            };
        }

        private void txtTestTypeDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTestTypeDescription.Text.Trim()))
            {
                errorProvider1.SetError(txtTestTypeDescription, "This cannot be Empty!");
            }
            else
            {
                errorProvider1.SetError(txtTestTypeDescription, null);
            }
        }

        private void txtTestTypeFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTestTypeFees.Text.Trim()))
            {
                errorProvider1.SetError(txtTestTypeFees, "This Cannot be Empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtTestTypeFees, null);
            };
            if (!clsValidation.IsNumber(txtTestTypeFees.Text))
            {
                errorProvider1.SetError(txtTestTypeFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtTestTypeFees, null);
            };
        }

        private void _LoadTestTypeInfo()
        {
            _TestType = clsTestType.FindTestType(_TestTypeID);
            if (_TestType != null)
            {
                lblTestTypeID.Text = _TestType.TestTypeID.ToString();
                txtTestTypeTitle.Text = _TestType.Title;
                txtTestTypeDescription.Text = _TestType.Description;
                txtTestTypeFees.Text = _TestType.Fees.ToString();
            }
            else
            {
                MessageBox.Show("Could not find Test Type with id = " + _TestTypeID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void _SaveTestType()
        {
            _TestType.Title = txtTestTypeTitle.Text;
            _TestType.Description = txtTestTypeDescription.Text;
            _TestType.Fees = float.Parse(txtTestTypeFees.Text);

            if (_TestType.Save())
            {
                MessageBox.Show(
                    text: "Data Saved Successfully",
                    caption: "Saved",
                    icon: MessageBoxIcon.Asterisk,
                    buttons: MessageBoxButtons.OK
                    );
            }
            else
            {
                MessageBox.Show(
                    text: "Save Failed",
                    caption: "Error",
                    icon: MessageBoxIcon.Asterisk,
                    buttons: MessageBoxButtons.OK
                    );
            }
        }
    }
}

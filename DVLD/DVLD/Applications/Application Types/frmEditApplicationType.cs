using System;
using System.Windows.Forms;
using DVLD_Business_Layer;
using DVLD.Global_Classes;

namespace DVLD.Applications.Application_Types
{
    public partial class frmEditApplicationType : DVLD.frmBaseForm
    {
        private int _ApplicationTypeID = -1;
        clsApplicationType _ApplicationType;

        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            _LoadApplicationTypeInfo();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show(
                    text: "Cant Save While There's Errors On Text Fields",
                    caption: "Error",
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error);
                return;
            }
            _SaveApplicationType();
        }

        private void _LoadApplicationTypeInfo()
        {
            _ApplicationType = clsApplicationType.FindApplicationType(_ApplicationTypeID);
            if (_ApplicationType != null)
            {
                lblApplicationTypeID.Text = _ApplicationType.ID.ToString();
                txtApplicationTypeTitle.Text = _ApplicationType.Title;
                txtApplicationTypeFees.Text = _ApplicationType.Fees.ToString();
            }
            else
            {
                MessageBox.Show("Couldn't Find The Required Application Type");
            }
        }

        private void _SaveApplicationType()
        {
            _ApplicationType.Title = txtApplicationTypeTitle.Text;
            _ApplicationType.Fees = float.Parse(txtApplicationTypeFees.Text);

            if (_ApplicationType.Save())
            {
                MessageBox.Show(
                    text: "Data Saved Successfully",
                    caption: "Saved",
                    icon: MessageBoxIcon.Asterisk,
                    buttons: MessageBoxButtons.OK
                    );
                frmEditApplicationType_Load(null, null);
            }
            else
            {
                MessageBox.Show(
                    text: "Data Save Failed",
                    caption: "Error",
                    icon: MessageBoxIcon.Asterisk,
                    buttons: MessageBoxButtons.OK
                    );
            }
        }

        private void txtApplicationTypeTitle_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtApplicationTypeTitle.Text.Trim()))
            {
                errorProvider1.SetError(txtApplicationTypeTitle, "Title cannot be empty!");
            }
            else
            {
                errorProvider1.SetError(txtApplicationTypeTitle, null);
            };
        }

        private void txtApplicationTypeFees_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtApplicationTypeFees.Text.Trim()))
            {
                errorProvider1.SetError(txtApplicationTypeFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtApplicationTypeFees, null);
            };
            if (!clsValidation.IsNumber(txtApplicationTypeFees.Text))
            {
                errorProvider1.SetError(txtApplicationTypeFees, "Invalid Number.");
            }
            else
            {
                errorProvider1.SetError(txtApplicationTypeFees, null);
            };
        }
    }
}
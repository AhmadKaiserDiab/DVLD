using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmBaseForm : Form
    {
        public event Action OnDataUpdated;
        protected virtual void IsDataUpdated()
        {
            Action handler = OnDataUpdated;

            if (handler != null)
            {
                handler?.Invoke();
            }
        }

        protected frmBaseForm()
        {
            InitializeComponent();
        }

        protected void btcClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}

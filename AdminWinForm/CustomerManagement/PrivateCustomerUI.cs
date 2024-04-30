using AdminWinForm.BusinesslogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminWinForm.CustomerManagement
{

    public partial class PrivateCustomerUI : Form
    {
        readonly PrivateCustomerLogic _privateCustomerLogic;
        public PrivateCustomerUI()
        {
            InitializeComponent();

            _privateCustomerLogic = new PrivateCustomerLogic();

            this.Load += AllPrivateCustomersUI_Load;
        }
    }
}

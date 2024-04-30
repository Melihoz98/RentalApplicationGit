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
    public partial class BusinessCustomerUI : Form
    {
        readonly BusinessCustomerLogic _businessCustomerLogic;
        public BusinessCustomerUI()
        {
            InitializeComponent();
            _businessCustomerLogic = new BusinessCustomerLogic();

            this.Load += AllBusinessCustomersUI_Load;
        }

    }
}

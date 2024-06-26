﻿using System;
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
    public partial class CustomerUI : Form
    {
        public CustomerUI()
        {
            InitializeComponent();
        }

        private void privateCustomer_Click(object sender, EventArgs e)
        {
            PrivateCustomerUI privateCustomerUI = new PrivateCustomerUI();
            privateCustomerUI.Show();
        }

        private void businessCustomer_Click(object sender, EventArgs e)
        {
            BusinessCustomerUI businessCustomerUI = new BusinessCustomerUI();
            businessCustomerUI.Show();
        }
    }
}

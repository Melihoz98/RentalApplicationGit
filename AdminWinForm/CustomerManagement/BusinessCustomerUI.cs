using AdminWinForm.BusinesslogicLayer;
using AdminWinForm.Models;
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
       

        private async void LoadBusinessCustomers()
        {
            try
            {
                List<BusinessCustomer> customers = await _businessCustomerLogic.GetAllBusinessCustomers();

                dataGridView1.Rows.Clear();
                foreach (BusinessCustomer customer in customers)
                {
                    dataGridView1.Rows.Add(customer.CustomerID, customer.CompanyName, customer.CVR, customer.PhoneNumber);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved indlæsning af kunder: {ex.Message}", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AllBusinessCustomersUI_Load(object sender, EventArgs e)
        {
            LoadBusinessCustomers();
        }
    }
}

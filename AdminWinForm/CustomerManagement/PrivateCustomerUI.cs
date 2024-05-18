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

    public partial class PrivateCustomerUI : Form
    {
        readonly PrivateCustomerLogic _privateCustomerLogic;
        public PrivateCustomerUI()
        {
            InitializeComponent();

            _privateCustomerLogic = new PrivateCustomerLogic();

            this.Load += AllPrivateCustomersUI_Load;
        }



        private async void LoadPrivateCustomers()
        {
            try
            {
                List<PrivateCustomer> customers = await _privateCustomerLogic.GetAllPrivateCustomers();

                dataGridView1.Rows.Clear();
                foreach (PrivateCustomer customer in customers)
                {
                    dataGridView1.Rows.Add(customer.CustomerID, customer.FirstName, customer.LastName, customer.PhoneNumber);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved indlæsning af kunder: {ex.Message}", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AllPrivateCustomersUI_Load(object sender, EventArgs e)
        {
            LoadPrivateCustomers();
        }
    }
}

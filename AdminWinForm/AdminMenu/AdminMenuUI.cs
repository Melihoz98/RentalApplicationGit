using AdminWinForm.CategoryManagement;
using AdminWinForm.CustomerManagement;
using AdminWinForm.ProductManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminWinForm.AdminMenu
{
    public partial class AdminMenuUI : Form
    {
        public AdminMenuUI()
        {
            InitializeComponent();
        }

        private void productManagement_Click(object sender, EventArgs e)
        {
            // Åbn AllProductsUI-vinduet
            AllProductsUI allProductsForm = new AllProductsUI();
            allProductsForm.Show();
        }

        private void categoryManagement_Click(object sender, EventArgs e)
        {
            AllCategoriesUI allCategoriesUI = new AllCategoriesUI();
            allCategoriesUI.Show();
        }

        private void customers_Click(object sender, EventArgs e)
        {
            CustomerUI customerUI = new CustomerUI();
            customerUI.Show();
        }
    }
}

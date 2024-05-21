using AdminWinForm.BusinesslogicLayer;
using AdminWinForm.Models;
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

namespace AdminWinForm.ProductCopyManagement
{
    public partial class AddProductCopyUI : Form
    {
        private readonly ProductLogic _productLogic;
        private List<Product> _products;


        public AddProductCopyUI()
        {
            InitializeComponent();
            _productLogic = new ProductLogic();
            Load += AddProductCopyUI_Load;
        }

        private void AddProductCopyUI_Load(object sender, EventArgs e)
        {
            LoadProductsAsync();
        }

        private async Task LoadProductsAsync() 
        
        {
            List<Product> products = await
                _productLogic.GetAllProducts();
            if(products != null)
            {
                foreach(Product product in products)
                
                comboBox1.DataSource = products;
                comboBox1.DisplayMember = "ProductName";
                comboBox1.ValueMember = "ProductID";
                
            }
            else
            {
                MessageBox.Show("Failed to load products.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

            foreach (Form form in Application.OpenForms)
            {
                if (form is AllProductCopiesUI)
                {
                    form.Show();
                    return;
                }
            }

            AllProductCopiesUI allProductCopiesUI = new AllProductCopiesUI();
            allProductCopiesUI.Show();
        }


        private async void saveProductCopy_Click(object sender, EventArgs e)
        {
            string serialNumber = textBox1.Text;
            int productId = (int)comboBox1.SelectedValue;

            ProductCopyLogic productCopyLogic = new ProductCopyLogic();

            int insertedProductCopyId = await productCopyLogic.AddProductCopy(serialNumber, productId);

            if (insertedProductCopyId != -1)
            {
                MessageBox.Show("Product copy added successfully with ID: " + insertedProductCopyId, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to add product copy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();

            // Show AllProductCopiesUI or AllProductsUI depending on your requirements
            foreach (Form form in Application.OpenForms)
            {
                if (form is AllProductCopiesUI)
                {
                    form.Show();
                    return;
                }
            }

            AllProductCopiesUI allProductCopiesUI = new AllProductCopiesUI();
            allProductCopiesUI.Show();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

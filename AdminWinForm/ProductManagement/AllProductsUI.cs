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

namespace AdminWinForm.ProductManagement
{
    public partial class AllProductsUI : Form
    {
        readonly ProductLogic _productLogic;
        public AllProductsUI()
        {
            InitializeComponent();

            _productLogic = new ProductLogic();

            this.Load += AllProductsUI_Load;
        }



        private readonly ProductLogic productService = new ProductLogic();

        private async void LoadProducts()
        {
            try
            {
                List<Product> products = await productService.GetAllProducts();

                dataGridView1.Rows.Clear();
                foreach (Product product in products)
                {
                    dataGridView1.Rows.Add(product.ProductID, product.ProductName, product.Description, product.HourlyPrice, product.CategoryID, product.ImagePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved indlæsning af produkter: {ex.Message}", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AllProductsUI_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }




        private void addProduct_Click(object sender, EventArgs e)
        {
            AddProductUI addProductForm = new AddProductUI();
            addProductForm.Show();
            this.Hide();
        }

        

        private void back_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private async void deleteProduct_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int productId = Convert.ToInt32(selectedRow.Cells["productIDColumn"].Value);
                DialogResult result = MessageBox.Show("Are you sure you want to delete this product?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    bool deleted = await _productLogic.DeleteProduct(productId);

                    if (deleted)
                    {
                        MessageBox.Show("Product deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadProducts();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a product to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AllProductsUI_Load_1(object sender, EventArgs e)
        {

        }
    }
}

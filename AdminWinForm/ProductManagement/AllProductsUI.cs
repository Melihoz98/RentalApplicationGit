﻿using AdminWinForm.BusinesslogicLayer;
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



        private void addProduct_Click(object sender, EventArgs e)
        {
            AddProductUI addProductForm = new AddProductUI();
            addProductForm.Show();
            this.Hide();
        }

        private void updateProduct_Click(object sender, EventArgs e)
        {
            // Check if a row is selected in the DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Extract product details from the selected row
                int productId = Convert.ToInt32(selectedRow.Cells["Column1"].Value);
                string productName = Convert.ToString(selectedRow.Cells["Column2"].Value);
                string description = Convert.ToString(selectedRow.Cells["Column3"].Value);
                decimal hourlyPrice = Convert.ToDecimal(selectedRow.Cells["Column4"].Value);
                int categoryID = Convert.ToInt32(selectedRow.Cells["Column5"].Value);
                string imagePath = Convert.ToString(selectedRow.Cells["Column6"].Value);

                // Open the UpdateProductUI form and pass the product details
                UpdateProductUI updateProductForm = new UpdateProductUI(productId, productName, description, hourlyPrice, categoryID, imagePath);
                updateProductForm.Show();
            }
            else
            {
                MessageBox.Show("Please select a product to update.");
            }
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
                int productId = Convert.ToInt32(selectedRow.Cells["Column1"].Value);
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

    }
}
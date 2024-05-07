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

namespace AdminWinForm.ProductManagement
{
    public partial class UpdateProductUI : Form
    {
        readonly ProductLogic _productLogic;
        public UpdateProductUI(int productID, string productName, string description, decimal hourlyPrice, int categoryID, string imagePath)
        {
            InitializeComponent();
            _productLogic = new ProductLogic();

            productIDBox.Text = productID.ToString();
            productNameBox.Text = productName;
            discriptionBox.Text = description;
            hourlyPriceBox.Text = hourlyPrice.ToString();
            categoryIDBox.Text = categoryID.ToString();
            imagePathBox.Text = imagePath;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void update_Click(object sender, EventArgs e)
        {
            int productID = int.Parse(productIDBox.Text);
            string productName = productNameBox.Text;
            string description = discriptionBox.Text;
            decimal hourlyPrice = decimal.Parse(hourlyPriceBox.Text);
            int categoryID = int.Parse(categoryIDBox.Text);
            string imagePath = imagePathBox.Text;


            bool success = await _productLogic.UpdateProduct(productID, productName, description, hourlyPrice, categoryID, imagePath);

            if (success)
            {
                MessageBox.Show("Product updated successfully!");
                this.Close(); 
            }
            else
            {
                MessageBox.Show("Failed to update product. Please try again.");
            }
        }

    }

}

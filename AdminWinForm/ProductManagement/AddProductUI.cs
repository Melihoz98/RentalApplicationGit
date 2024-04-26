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
    public partial class AddProductUI : Form
    {
        public AddProductUI()
        {
            InitializeComponent();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();

            foreach (Form form in Application.OpenForms)
            {
                if (form is AllProductsUI)
                {
                    form.Show();
                    return;
                }
            }

            AllProductsUI allProductsUI = new AllProductsUI();
            allProductsUI.Show();
        }

        private async void saveProduct(object sender, EventArgs e)
        {
            string productName = textBox1.Text;
            string description = textBox2.Text;
            decimal hourlyPrice = decimal.Parse(textBox3.Text);
            int categoryID = int.Parse(textBox4.Text);
            string imagepath = textBox5.Text;

            ProductLogic productLogic = new ProductLogic();

            int insertedProductId = await productLogic.AddProduct(productName, description, hourlyPrice, categoryID, imagepath);

            if (insertedProductId != -1)
            {
                MessageBox.Show("Product added successfully with ID: " + insertedProductId, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to add product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();

            foreach (Form form in Application.OpenForms)
            {
                if (form is AllProductsUI)
                {
                    form.Show();
                    return;
                }
            }

            AllProductsUI allProductsUI = new AllProductsUI();
            allProductsUI.Show();
        }

    }
}

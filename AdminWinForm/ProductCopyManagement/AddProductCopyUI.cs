using AdminWinForm.BusinesslogicLayer;
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
        public AddProductCopyUI()
        {
            InitializeComponent();
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
                int productId = int.Parse(textBox2.Text); 

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
    }
}

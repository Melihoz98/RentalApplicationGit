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
    public partial class AllProductCopiesUI : Form
    {
        readonly ProductCopyLogic _productCopyLogic;
        public AllProductCopiesUI()
        {
            InitializeComponent();

            _productCopyLogic = new ProductCopyLogic();

            this.Load += AllProductCopiesUI_Load;
        }


        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addProductCopy_Click(object sender, EventArgs e)
        {
            AddProductCopyUI addProductCopyForm = new AddProductCopyUI();
            addProductCopyForm.Show();
            this.Hide();
        }

       

        private async void deleteProductCopy_Click(object sender, EventArgs e)
        {
            // Check if a row is selected in the DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Extract product copy details from the selected row
                string serialNumber = Convert.ToString(selectedRow.Cells["Column1"].Value);


                // Display confirmation message
                DialogResult result = MessageBox.Show("Are you sure you want to delete this product copy?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // If user confirms deletion
                if (result == DialogResult.Yes)
                {
                    // Call the delete method from the business logic layer
                    bool deleted = await _productCopyLogic.DeleteProductCopy(serialNumber);

                    // Check if deletion was successful
                    if (deleted)
                    {
                        MessageBox.Show("Product copy deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Reload the product copies in the DataGridView
                        LoadProductCopies();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete product copy.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a product copy to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

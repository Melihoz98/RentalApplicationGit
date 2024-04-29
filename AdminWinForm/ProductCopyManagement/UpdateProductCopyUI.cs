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

namespace AdminWinForm.ProductCopyManagement
{
    public partial class UpdateProductCopyUI : Form
    {
        public UpdateProductCopyUI(string serialNumber, string productID, bool rented)
        {
            InitializeComponent();
            textBox1.Text = serialNumber;
            textBox2.Text = productID;
            textBox3.Text = rented.ToString();

        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void save_Click(object sender, EventArgs e)
        {
            // Indsaml oplysninger fra tekstbokse
            string serialNumber = textBox1.Text;
            int productId = int.Parse(textBox2.Text);
            bool rented = bool.Parse(textBox3.Text);

            // Opret en instans af ProductCopyLogic
            ProductCopyLogic productCopyLogic = new ProductCopyLogic();

            // Opdater produktkopien i databasen
            bool success = await productCopyLogic.UpdateProductCopy(serialNumber, productId, rented);

            if (success)
            {
                MessageBox.Show("Product copy updated successfully!");
                this.Close(); // Luk formen, når opdateringen er gennemført
            }
            else
            {
                MessageBox.Show("Failed to update product copy. Please try again.");
            }
        }

    }

}

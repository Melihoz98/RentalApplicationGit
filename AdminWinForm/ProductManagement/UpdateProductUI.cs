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
        public UpdateProductUI(int productId, string productName, string description, decimal hourlyPrice, int categoryID, string imagePath)
        {
            InitializeComponent();

            textBox6.Text = productId;
            textBox1.Text = productName;
            textBox2.Text = description;
            textBox3.Text = hourlyPrice.ToString();
            textBox4.Text = categoryID.ToString();
            textBox5.Text = imagePath.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void update_Click(object sender, EventArgs e)
        {
            // Indsaml oplysninger fra tekstbokse
            int productId = int.Parse(textBox6.Text);
            string productName = textBox1.Text;
            string description = textBox2.Text;
            decimal hourlyPrice = decimal.Parse(textBox3.Text);
            int categoryID = int.Parse(textBox4.Text);
            string imagePath = textBox5.Text;

            // Opret en instans af ProductLogic
            ProductLogic productLogic = new ProductLogic();

            // Opdater produktet i databasen
            bool success = await productLogic.UpdateProduct(productId, productName, description, hourlyPrice, categoryID, imagePath);

            if (success)
            {
                MessageBox.Show("Product updated successfully!");
                this.Close(); // Luk formen, når opdateringen er gennemført
            }
            else
            {
                MessageBox.Show("Failed to update product. Please try again.");
            }
        }

    }

}

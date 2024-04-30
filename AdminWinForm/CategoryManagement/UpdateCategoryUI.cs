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

namespace AdminWinForm.CategoryManagement
{
    public partial class UpdateCategoryUI : Form

    {
        public UpdateCategoryUI(int categoryID, string categoryName, string imagePath)
        {
            InitializeComponent();

            textBox1.Text = categoryID.ToString();
            textBox2.Text = categoryName;
            textBox3.Text = imagePath;

        }

        private void Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private async void save_Click(object sender, EventArgs e)
        {
            int categoryID = int.Parse(textBox1.Text);
            string categoryName = textBox2.Text;
            string imagePath = textBox3.Text;

            CategoryLogic categoryLogic = new CategoryLogic();

            // Update the category in the database
            bool success = await categoryLogic.UpdateCategory(categoryID, categoryName, imagePath);

            if (success)
            {
                MessageBox.Show("Category updated successfully!");
                this.Close(); // Close the form when the update is successful
            }
            else
            {
                MessageBox.Show("Failed to update category. Please try again.");
            }
        }
    }
}


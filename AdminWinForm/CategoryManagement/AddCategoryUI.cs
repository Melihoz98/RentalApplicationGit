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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AdminWinForm.CategoryManagement
{
    public partial class AddCategoryUI : Form
    {
        public AddCategoryUI()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void save_Click(object sender, EventArgs e)
        {
            string categoryName = textBox1.Text;
            string imagePath = textBox2.Text;
           

            CategoryLogic categotyLogic = new CategoryLogic();

            int insertedCategoryId = await categotyLogic.AddCategory(categoryName, imagePath);

            if (insertedCategoryId != -1)
            {
                MessageBox.Show("Category added successfully with ID: " + insertedCategoryId, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to add category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();

            foreach (Form form in Application.OpenForms)
            {
                if (form is AllCategoriesUI)
                {
                    form.Show();
                    return;
                }
            }

            AllCategoriesUI allCategoriesUI = new AllCategoriesUI();
            allCategoriesUI.Show();
        }
    }
    
}

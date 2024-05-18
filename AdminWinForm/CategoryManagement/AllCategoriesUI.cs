using AdminWinForm.BusinesslogicLayer;
using AdminWinForm.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminWinForm.CategoryManagement
{
    public partial class AllCategoriesUI : Form
    {
        readonly CategoryLogic _categoryLogic;
        public AllCategoriesUI()
        {
            InitializeComponent();
            _categoryLogic = new CategoryLogic();
            this.Load += AllCategoriesUI_Load;
        }

        private async void LoadCategories()
        {
            try
            {
                List<Category> categories = await _categoryLogic.GetAllCategories();

                if (dataGridView1.InvokeRequired)
                {
                    dataGridView1.Invoke(new Action(() =>
                    {
                        dataGridView1.Rows.Clear();
                        foreach (Category category in categories)
                        {
                            dataGridView1.Rows.Add(category.CategoryID, category.CategoryName, category.ImagePath);
                        }
                    }));
                }
                else
                {
                    dataGridView1.Rows.Clear();
                    foreach (Category category in categories)
                    {
                        dataGridView1.Rows.Add(category.CategoryID, category.CategoryName, category.ImagePath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved indlæsning af categoryer: {ex.Message}", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AllCategoriesUI_Load(object sender, EventArgs e)
        {
            LoadCategories();
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addCategory_Click(object sender, EventArgs e)
        {
            AddCategoryUI addCategory = new AddCategoryUI();
            addCategory.Show();
        }

        private async void DeleteCategory_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int categoryID = Convert.ToInt32(selectedRow.Cells["Column1"].Value);
                DialogResult result = MessageBox.Show("Are you sure you want to delete this category?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    bool deleted = await _categoryLogic.DeleteCategory(categoryID);
                    if (deleted)
                    {
                        MessageBox.Show("Category deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //LoadCategories();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a category to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

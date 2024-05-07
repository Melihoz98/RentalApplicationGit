using AdminWinForm.ServiceLayer;
using AdminWinForm.Models;
using System.Windows.Forms;

namespace AdminWinForm.CategoryManagement
{
    partial class AllCategoriesUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private readonly CategoryServiceAccess categoryService = new CategoryServiceAccess();

        private async void LoadCategories()
        {
            try
            {
                List<Category> categories = await categoryService.GetCategories();

                dataGridView1.Rows.Clear();
                foreach(Category category in categories)
                {
                    dataGridView1.Rows.Add(category.CategoryID, category.CategoryName, category.ImagePath);
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


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3 });
            dataGridView1.Location = new Point(1, 1);
            dataGridView1.Margin = new Padding(2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(996, 229);
            dataGridView1.TabIndex = 1;
            // 
            // Column1
            // 
            Column1.HeaderText = "CategoryID";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 250;
            // 
            // Column2
            // 
            Column2.HeaderText = "Category Name";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 250;
            // 
            // Column3
            // 
            Column3.HeaderText = "ImagePath";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 250;
            // 
            // button1
            // 
            button1.Location = new Point(344, 500);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(191, 34);
            button1.TabIndex = 2;
            button1.Text = "Add Category";
            button1.UseVisualStyleBackColor = true;
            button1.Click += addCategory_Click;
            // 
            // button2
            // 
            button2.Location = new Point(571, 500);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(178, 34);
            button2.TabIndex = 3;
            button2.Text = "Update Category";
            button2.UseVisualStyleBackColor = true;
            button2.Click += updateCategory_Click;
            // 
            // button3
            // 
            button3.Location = new Point(782, 500);
            button3.Margin = new Padding(2);
            button3.Name = "button3";
            button3.Size = new Size(190, 34);
            button3.TabIndex = 4;
            button3.Text = "Delete Category";
            button3.UseVisualStyleBackColor = true;
            button3.Click += DeleteCategory_Click;
            // 
            // button4
            // 
            button4.Location = new Point(14, 500);
            button4.Margin = new Padding(2);
            button4.Name = "button4";
            button4.Size = new Size(178, 34);
            button4.TabIndex = 5;
            button4.Text = "Back";
            button4.UseVisualStyleBackColor = true;
            button4.Click += back_Click;
            // 
            // AllCategoriesUI
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 562);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Margin = new Padding(4, 4, 4, 4);
            Name = "AllCategoriesUI";
            Text = "AllCategoriesUI";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button1;
        private Button button2;
        private Button button3;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private Button button4;
    }
}
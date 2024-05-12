using AdminWinForm.Models;
using AdminWinForm.ServiceLayer;

namespace AdminWinForm.ProductManagement
{
    partial class AllProductsUI
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

        private readonly ProductServiceAccess productService = new ProductServiceAccess();

        private async void LoadProducts()
        {
            try
            {
                List<Product> products = await productService.GetProducts();

                dataGridView1.Rows.Clear();
                foreach (Product product in products)
                {
                    dataGridView1.Rows.Add(product.ProductID, product.ProductName, product.Description, product.HourlyPrice, product.CategoryID, product.ImagePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved indlæsning af produkter: {ex.Message}", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AllProductsUI_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            productIDColumn = new DataGridViewTextBoxColumn();
            productNameColumn = new DataGridViewTextBoxColumn();
            descriptionColumn = new DataGridViewTextBoxColumn();
            hourlyPriceColumn = new DataGridViewTextBoxColumn();
            categoryIDColumn = new DataGridViewTextBoxColumn();
            imagePathColumn = new DataGridViewTextBoxColumn();
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
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { productIDColumn, productNameColumn, descriptionColumn, hourlyPriceColumn, categoryIDColumn, imagePathColumn });
            dataGridView1.Location = new Point(1, 1);
            dataGridView1.Margin = new Padding(2, 2, 2, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(676, 219);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // productIDColumn
            // 
            productIDColumn.HeaderText = "ProductID";
            productIDColumn.MinimumWidth = 8;
            productIDColumn.Name = "productIDColumn";
            productIDColumn.ReadOnly = true;
            productIDColumn.Width = 150;
            // 
            // productNameColumn
            // 
            productNameColumn.HeaderText = "Product Name";
            productNameColumn.MinimumWidth = 8;
            productNameColumn.Name = "productNameColumn";
            productNameColumn.ReadOnly = true;
            productNameColumn.Width = 150;
            // 
            // descriptionColumn
            // 
            descriptionColumn.HeaderText = "Discreption";
            descriptionColumn.MinimumWidth = 8;
            descriptionColumn.Name = "descriptionColumn";
            descriptionColumn.ReadOnly = true;
            descriptionColumn.Width = 150;
            // 
            // hourlyPriceColumn
            // 
            hourlyPriceColumn.HeaderText = "Hourly Price";
            hourlyPriceColumn.MinimumWidth = 8;
            hourlyPriceColumn.Name = "hourlyPriceColumn";
            hourlyPriceColumn.ReadOnly = true;
            hourlyPriceColumn.Width = 150;
            // 
            // categoryIDColumn
            // 
            categoryIDColumn.HeaderText = "CategoryID";
            categoryIDColumn.MinimumWidth = 8;
            categoryIDColumn.Name = "categoryIDColumn";
            categoryIDColumn.ReadOnly = true;
            categoryIDColumn.Width = 150;
            // 
            // imagePathColumn
            // 
            imagePathColumn.HeaderText = "Image path";
            imagePathColumn.MinimumWidth = 8;
            imagePathColumn.Name = "imagePathColumn";
            imagePathColumn.ReadOnly = true;
            imagePathColumn.Width = 150;
            // 
            // button1
            // 
            button1.Location = new Point(378, 236);
            button1.Margin = new Padding(2, 2, 2, 2);
            button1.Name = "button1";
            button1.Size = new Size(144, 20);
            button1.TabIndex = 1;
            button1.Text = "Add Product";
            button1.UseVisualStyleBackColor = true;
            button1.Click += addProduct_Click;
            // 
            // button2
            // 
            button2.Location = new Point(533, 236);
            button2.Margin = new Padding(2, 2, 2, 2);
            button2.Name = "button2";
            button2.Size = new Size(144, 20);
            button2.TabIndex = 2;
            button2.Text = "Delete Product";
            button2.UseVisualStyleBackColor = true;
            button2.Click += deleteProduct_Click;
            // 
            // button3
            // 
            button3.Location = new Point(224, 236);
            button3.Margin = new Padding(2, 2, 2, 2);
            button3.Name = "button3";
            button3.Size = new Size(144, 20);
            button3.TabIndex = 3;
            button3.Text = "Update Product";
            button3.UseVisualStyleBackColor = true;
            button3.Click += updateProduct_Click;
            // 
            // button4
            // 
            button4.Location = new Point(8, 236);
            button4.Margin = new Padding(2, 2, 2, 2);
            button4.Name = "button4";
            button4.Size = new Size(125, 20);
            button4.TabIndex = 4;
            button4.Text = "Back";
            button4.UseVisualStyleBackColor = true;
            button4.Click += back_Click;
            // 
            // AllProductsUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(698, 270);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Margin = new Padding(2, 2, 2, 2);
            Name = "AllProductsUI";
            Text = "ProductUI";
            Load += AllProductsUI_Load_1;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private DataGridViewTextBoxColumn productIDColumn;
        private DataGridViewTextBoxColumn productNameColumn;
        private DataGridViewTextBoxColumn descriptionColumn;
        private DataGridViewTextBoxColumn hourlyPriceColumn;
        private DataGridViewTextBoxColumn categoryIDColumn;
        private DataGridViewTextBoxColumn imagePathColumn;
    }
}
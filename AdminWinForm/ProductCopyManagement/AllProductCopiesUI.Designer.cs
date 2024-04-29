using AdminWinForm.ServiceLayer;
using RentalService.Models;

namespace AdminWinForm.ProductCopyManagement
{
    partial class AllProductCopiesUI
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

        private readonly ProductCopyServiceAccess productCopyService = new ProductCopyServiceAccess();

        private async void LoadProductCopies()
        {
            try
            {
                List<ProductCopy> productCopies = await productCopyService.GetProductCopies();

                dataGridView1.Rows.Clear();
                foreach (ProductCopy productCopy in productCopies)
                {
                    dataGridView1.Rows.Add(productCopy.SerialNumber, productCopy.ProductID, productCopy.Rented);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved indlæsning af produkt kopier: {ex.Message}", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AllProductCopiesUI_Load(object sender, EventArgs e)
        {
            LoadProductCopies();
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
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            button4 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3 });
            dataGridView1.Location = new Point(152, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(704, 365);
            dataGridView1.TabIndex = 1;
            // 
            // Column1
            // 
            Column1.HeaderText = "Serial Number";
            Column1.MinimumWidth = 8;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 150;
            // 
            // Column2
            // 
            Column2.HeaderText = "ProductID";
            Column2.MinimumWidth = 8;
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 150;
            // 
            // Column3
            // 
            Column3.HeaderText = "Rented";
            Column3.MinimumWidth = 8;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 150;
            // 
            // button3
            // 
            button3.Location = new Point(325, 404);
            button3.Name = "button3";
            button3.Size = new Size(205, 34);
            button3.TabIndex = 6;
            button3.Text = "Update Product";
            button3.UseVisualStyleBackColor = true;
            button3.Click += updateProductCopy_Click;
            // 
            // button2
            // 
            button2.Location = new Point(767, 404);
            button2.Name = "button2";
            button2.Size = new Size(205, 34);
            button2.TabIndex = 5;
            button2.Text = "Delete Product";
            button2.UseVisualStyleBackColor = true;
            button2.Click += deleteProductCopy_Click;
            // 
            // button1
            // 
            button1.Location = new Point(545, 404);
            button1.Name = "button1";
            button1.Size = new Size(205, 34);
            button1.TabIndex = 4;
            button1.Text = "Add Product";
            button1.UseVisualStyleBackColor = true;
            button1.Click += addProductCopy_Click;
            // 
            // button4
            // 
            button4.Location = new Point(12, 404);
            button4.Name = "button4";
            button4.Size = new Size(178, 34);
            button4.TabIndex = 7;
            button4.Text = "Back";
            button4.UseVisualStyleBackColor = true;
            button4.Click += back_Click;
            // 
            // AllProductCopiesUI
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(999, 450);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Name = "AllProductCopiesUI";
            Text = "AllProductCopiesUI";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private Button button3;
        private Button button2;
        private Button button1;
        private Button button4;
    }
}
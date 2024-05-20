

namespace AdminWinForm.ProductCopyManagement
{
    partial class AllProductCopiesUI
    {
        
        private System.ComponentModel.IContainer components = null;

       
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }




        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            button2 = new Button();
            button1 = new Button();
            button4 = new Button();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2 });
            dataGridView1.Location = new Point(151, 11);
            dataGridView1.Margin = new Padding(2, 4, 2, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(509, 365);
            dataGridView1.TabIndex = 1;
            // 
            // button2
            // 
            button2.Location = new Point(768, 404);
            button2.Margin = new Padding(2, 4, 2, 4);
            button2.Name = "button2";
            button2.Size = new Size(206, 34);
            button2.TabIndex = 5;
            button2.Text = "Delete Product";
            button2.UseVisualStyleBackColor = true;
            button2.Click += deleteProductCopy_Click;
            // 
            // button1
            // 
            button1.Location = new Point(546, 404);
            button1.Margin = new Padding(2, 4, 2, 4);
            button1.Name = "button1";
            button1.Size = new Size(206, 34);
            button1.TabIndex = 4;
            button1.Text = "Add Product";
            button1.UseVisualStyleBackColor = true;
            button1.Click += addProductCopy_Click;
            // 
            // button4
            // 
            button4.Location = new Point(11, 404);
            button4.Margin = new Padding(2, 4, 2, 4);
            button4.Name = "button4";
            button4.Size = new Size(179, 34);
            button4.TabIndex = 7;
            button4.Text = "Back";
            button4.UseVisualStyleBackColor = true;
            button4.Click += back_Click;
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
            // AllProductCopiesUI
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(999, 450);
            Controls.Add(button4);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Margin = new Padding(2, 4, 2, 4);
            Name = "AllProductCopiesUI";
            Text = "AllProductCopiesUI";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button2;
        private Button button1;
        private Button button4;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
    }
}
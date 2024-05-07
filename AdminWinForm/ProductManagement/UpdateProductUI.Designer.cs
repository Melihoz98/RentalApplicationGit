namespace AdminWinForm.ProductManagement
{
    partial class UpdateProductUI
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button2 = new Button();
            button1 = new Button();
            imagePathLabel = new Label();
            categoryIDLabel = new Label();
            hourlyPriceLabel = new Label();
            discriptionLabel = new Label();
            productNameLabel = new Label();
            productIDLabel = new Label();
            productIDBox = new TextBox();
            productNameBox = new TextBox();
            discriptionBox = new TextBox();
            hourlyPriceBox = new TextBox();
            categoryIDBox = new TextBox();
            imagePathBox = new TextBox();
            SuspendLayout();
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ActiveCaption;
            button2.Location = new Point(323, 375);
            button2.Name = "button2";
            button2.Size = new Size(152, 34);
            button2.TabIndex = 23;
            button2.Text = "Save";
            button2.UseVisualStyleBackColor = false;
            button2.Click += update_Click;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ActiveCaption;
            button1.Location = new Point(68, 375);
            button1.Name = "button1";
            button1.Size = new Size(152, 34);
            button1.TabIndex = 22;
            button1.Text = "Canncel";
            button1.UseVisualStyleBackColor = false;
            button1.Click += cancel_Click;
            // 
            // imagePathLabel
            // 
            imagePathLabel.AutoSize = true;
            imagePathLabel.Location = new Point(59, 308);
            imagePathLabel.Name = "imagePathLabel";
            imagePathLabel.Size = new Size(96, 25);
            imagePathLabel.TabIndex = 21;
            imagePathLabel.Text = "ImagePath";
            // 
            // categoryIDLabel
            // 
            categoryIDLabel.AutoSize = true;
            categoryIDLabel.Location = new Point(59, 250);
            categoryIDLabel.Name = "categoryIDLabel";
            categoryIDLabel.Size = new Size(102, 25);
            categoryIDLabel.TabIndex = 20;
            categoryIDLabel.Text = "CategoryID";
            // 
            // hourlyPriceLabel
            // 
            hourlyPriceLabel.AutoSize = true;
            hourlyPriceLabel.Location = new Point(59, 193);
            hourlyPriceLabel.Name = "hourlyPriceLabel";
            hourlyPriceLabel.Size = new Size(107, 25);
            hourlyPriceLabel.TabIndex = 19;
            hourlyPriceLabel.Text = "Hourly Price";
            // 
            // discriptionLabel
            // 
            discriptionLabel.AutoSize = true;
            discriptionLabel.Location = new Point(59, 137);
            discriptionLabel.Name = "discriptionLabel";
            discriptionLabel.Size = new Size(97, 25);
            discriptionLabel.TabIndex = 18;
            discriptionLabel.Text = "Discription";
            // 
            // productNameLabel
            // 
            productNameLabel.AutoSize = true;
            productNameLabel.Location = new Point(59, 74);
            productNameLabel.Name = "productNameLabel";
            productNameLabel.Size = new Size(126, 25);
            productNameLabel.TabIndex = 12;
            productNameLabel.Text = "Product Name";
            // 
            // productIDLabel
            // 
            productIDLabel.AutoSize = true;
            productIDLabel.Location = new Point(59, 25);
            productIDLabel.Name = "productIDLabel";
            productIDLabel.Size = new Size(92, 25);
            productIDLabel.TabIndex = 24;
            productIDLabel.Text = "ProductID";
            // 
            // productIDBox
            // 
            productIDBox.Location = new Point(284, 25);
            productIDBox.Name = "productIDBox";
            productIDBox.Size = new Size(211, 31);
            productIDBox.TabIndex = 25;
            // 
            // productNameBox
            // 
            productNameBox.Location = new Point(284, 74);
            productNameBox.Name = "productNameBox";
            productNameBox.Size = new Size(211, 31);
            productNameBox.TabIndex = 26;
            // 
            // discriptionBox
            // 
            discriptionBox.Location = new Point(284, 134);
            discriptionBox.Name = "discriptionBox";
            discriptionBox.Size = new Size(211, 31);
            discriptionBox.TabIndex = 27;
            // 
            // hourlyPriceBox
            // 
            hourlyPriceBox.Location = new Point(284, 193);
            hourlyPriceBox.Name = "hourlyPriceBox";
            hourlyPriceBox.Size = new Size(211, 31);
            hourlyPriceBox.TabIndex = 28;
            // 
            // categoryIDBox
            // 
            categoryIDBox.Location = new Point(284, 250);
            categoryIDBox.Name = "categoryIDBox";
            categoryIDBox.Size = new Size(211, 31);
            categoryIDBox.TabIndex = 29;
            // 
            // imagePathBox
            // 
            imagePathBox.Location = new Point(284, 308);
            imagePathBox.Name = "imagePathBox";
            imagePathBox.Size = new Size(211, 31);
            imagePathBox.TabIndex = 30;
            // 
            // UpdateProductUI
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(543, 450);
            Controls.Add(imagePathBox);
            Controls.Add(categoryIDBox);
            Controls.Add(hourlyPriceBox);
            Controls.Add(discriptionBox);
            Controls.Add(productNameBox);
            Controls.Add(productIDBox);
            Controls.Add(productIDLabel);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(imagePathLabel);
            Controls.Add(categoryIDLabel);
            Controls.Add(hourlyPriceLabel);
            Controls.Add(discriptionLabel);
            Controls.Add(productNameLabel);
            Name = "UpdateProductUI";
            Text = "UpdateProductUI";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button2;
        private Button button1;
        private Label imagePathLabel;
        private Label categoryIDLabel;
        private Label hourlyPriceLabel;
        private Label discriptionLabel;
        private Label productNameLabel;
        private Label productIDLabel;
        private TextBox productIDBox;
        private TextBox productNameBox;
        private TextBox discriptionBox;
        private TextBox hourlyPriceBox;
        private TextBox categoryIDBox;
        private TextBox imagePathBox;
    }
}
namespace AdminWinForm.ProductManagement
{
    partial class AddProductUI
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
            productName = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            textBox5 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // productName
            // 
            productName.AutoSize = true;
            productName.Location = new Point(44, 55);
            productName.Name = "productName";
            productName.Size = new Size(126, 25);
            productName.TabIndex = 0;
            productName.Text = "Product Name";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(203, 55);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(248, 31);
            textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(203, 112);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(248, 31);
            textBox2.TabIndex = 2;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(203, 168);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(248, 31);
            textBox3.TabIndex = 3;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(203, 225);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(248, 31);
            textBox4.TabIndex = 4;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(203, 283);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(248, 31);
            textBox5.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(44, 118);
            label1.Name = "label1";
            label1.Size = new Size(97, 25);
            label1.TabIndex = 6;
            label1.Text = "Discription";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(44, 174);
            label2.Name = "label2";
            label2.Size = new Size(107, 25);
            label2.TabIndex = 7;
            label2.Text = "Hourly Price";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(44, 231);
            label3.Name = "label3";
            label3.Size = new Size(102, 25);
            label3.TabIndex = 8;
            label3.Text = "CategoryID";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(44, 289);
            label4.Name = "label4";
            label4.Size = new Size(96, 25);
            label4.TabIndex = 9;
            label4.Text = "ImagePath";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ActiveCaption;
            button1.Location = new Point(44, 388);
            button1.Name = "button1";
            button1.Size = new Size(152, 34);
            button1.TabIndex = 10;
            button1.Text = "Canncel";
            button1.UseVisualStyleBackColor = false;
            button1.Click += cancel_Click;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ActiveCaption;
            button2.Location = new Point(299, 388);
            button2.Name = "button2";
            button2.Size = new Size(152, 34);
            button2.TabIndex = 11;
            button2.Text = "Save";
            button2.UseVisualStyleBackColor = false;
            button2.Click += saveProduct;
            // 
            // AddProductUI
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(615, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(productName);
            Name = "AddProductUI";
            Text = "AddProductUI";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label productName;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private TextBox textBox5;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button button1;
        private Button button2;
    }
}
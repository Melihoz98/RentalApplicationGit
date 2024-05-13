namespace AdminWinForm.ProductCopyManagement
{
    partial class AddProductCopyUI
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
            button2 = new Button();
            button1 = new Button();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            productName = new Label();
            SuspendLayout();
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ActiveCaption;
            button2.Location = new Point(334, 373);
            button2.Name = "button2";
            button2.Size = new Size(152, 34);
            button2.TabIndex = 23;
            button2.Text = "Save";
            button2.UseVisualStyleBackColor = false;
            button2.Click += saveProductCopy_Click;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ActiveCaption;
            button1.Location = new Point(79, 373);
            button1.Name = "button1";
            button1.Size = new Size(152, 34);
            button1.TabIndex = 22;
            button1.Text = "Canncel";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(79, 274);
            label4.Name = "label4";
            label4.Size = new Size(0, 25);
            label4.TabIndex = 21;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(79, 216);
            label3.Name = "label3";
            label3.Size = new Size(0, 25);
            label3.TabIndex = 20;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(79, 159);
            label2.Name = "label2";
            label2.Size = new Size(101, 25);
            label2.TabIndex = 19;
            label2.Text = "Image Path";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(79, 103);
            label1.Name = "label1";
            label1.Size = new Size(92, 25);
            label1.TabIndex = 18;
            label1.Text = "ProductID";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(238, 153);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(248, 31);
            textBox3.TabIndex = 15;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(238, 97);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(248, 31);
            textBox2.TabIndex = 14;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(238, 40);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(248, 31);
            textBox1.TabIndex = 13;
            // 
            // productName
            // 
            productName.AutoSize = true;
            productName.Location = new Point(79, 40);
            productName.Name = "productName";
            productName.Size = new Size(124, 25);
            productName.TabIndex = 12;
            productName.Text = "Serial Number";
            // 
            // AddProductCopyUI
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(598, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(productName);
            Name = "AddProductCopyUI";
            Text = "AddProductCopyUI";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button2;
        private Button button1;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private Label productName;
    }
}
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
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            textBox5 = new TextBox();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            productName = new Label();
            textBox6 = new TextBox();
            label5 = new Label();
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
            button1.Click += button1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(59, 308);
            label4.Name = "label4";
            label4.Size = new Size(96, 25);
            label4.TabIndex = 21;
            label4.Text = "ImagePath";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(59, 250);
            label3.Name = "label3";
            label3.Size = new Size(102, 25);
            label3.TabIndex = 20;
            label3.Text = "CategoryID";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(59, 193);
            label2.Name = "label2";
            label2.Size = new Size(107, 25);
            label2.TabIndex = 19;
            label2.Text = "Hourly Price";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(59, 137);
            label1.Name = "label1";
            label1.Size = new Size(97, 25);
            label1.TabIndex = 18;
            label1.Text = "Discription";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(218, 302);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(248, 31);
            textBox5.TabIndex = 17;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(218, 244);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(248, 31);
            textBox4.TabIndex = 16;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(218, 187);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(248, 31);
            textBox3.TabIndex = 15;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(218, 131);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(248, 31);
            textBox2.TabIndex = 14;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(218, 74);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(248, 31);
            textBox1.TabIndex = 13;
            // 
            // productName
            // 
            productName.AutoSize = true;
            productName.Location = new Point(59, 74);
            productName.Name = "productName";
            productName.Size = new Size(126, 25);
            productName.TabIndex = 12;
            productName.Text = "Product Name";
            // 
            // textBox6
            // 
            textBox6.Location = new Point(218, 25);
            textBox6.Name = "textBox6";
            textBox6.ReadOnly = true;
            textBox6.Size = new Size(248, 31);
            textBox6.TabIndex = 25;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(59, 25);
            label5.Name = "label5";
            label5.Size = new Size(92, 25);
            label5.TabIndex = 24;
            label5.Text = "ProductID";
            // 
            // UpdateProductUI
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(543, 450);
            Controls.Add(textBox6);
            Controls.Add(label5);
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
            Name = "UpdateProductUI";
            Text = "UpdateProductUI";
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
        private TextBox textBox5;
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private Label productName;
        private TextBox textBox6;
        private Label label5;
    }
}
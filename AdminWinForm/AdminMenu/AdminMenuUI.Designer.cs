﻿namespace AdminWinForm.AdminMenu
{
    partial class AdminMenuUI
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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(22, 35);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(328, 130);
            button1.TabIndex = 0;
            button1.Text = "Product Management";
            button1.UseVisualStyleBackColor = true;
            button1.Click += productManagement_Click;
            // 
            // button2
            // 
            button2.Location = new Point(432, 35);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(334, 130);
            button2.TabIndex = 1;
            button2.Text = "Category Management";
            button2.UseVisualStyleBackColor = true;
            button2.Click += categoryManagement_Click;
            // 
            // button3
            // 
            button3.Location = new Point(22, 181);
            button3.Margin = new Padding(2);
            button3.Name = "button3";
            button3.Size = new Size(328, 130);
            button3.TabIndex = 2;
            button3.Text = "Order Management";
            button3.UseVisualStyleBackColor = true;
            button3.Click += orderManagement_Click;
            // 
            // button4
            // 
            button4.Location = new Point(432, 181);
            button4.Margin = new Padding(2);
            button4.Name = "button4";
            button4.Size = new Size(334, 130);
            button4.TabIndex = 3;
            button4.Text = " Product Copy Management";
            button4.UseVisualStyleBackColor = true;
            button4.Click += productCopyManagement_Click;
            // 
            // button5
            // 
            button5.Location = new Point(22, 321);
            button5.Name = "button5";
            button5.Size = new Size(328, 130);
            button5.TabIndex = 4;
            button5.Text = "Customers";
            button5.UseVisualStyleBackColor = true;
            button5.Click += customers_Click;
            // 
            // AdminMenuUI
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 463);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Margin = new Padding(2);
            Name = "AdminMenuUI";
            Text = "AdminMenuUI";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
    }
}
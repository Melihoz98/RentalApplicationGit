namespace AdminWinForm.AdminMenu
{
    partial class AdminMenuUI
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
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(18, 64);
            button1.Margin = new Padding(2, 2, 2, 2);
            button1.Name = "button1";
            button1.Size = new Size(262, 104);
            button1.TabIndex = 0;
            button1.Text = "Product Management";
            button1.UseVisualStyleBackColor = true;
            button1.Click += productManagement_Click;
            // 
            // button2
            // 
            button2.Location = new Point(346, 64);
            button2.Margin = new Padding(2, 2, 2, 2);
            button2.Name = "button2";
            button2.Size = new Size(267, 104);
            button2.TabIndex = 1;
            button2.Text = "Category Management";
            button2.UseVisualStyleBackColor = true;
            button2.Click += categoryManagement_Click;
            // 
            // button3
            // 
            button3.Location = new Point(18, 201);
            button3.Margin = new Padding(2, 2, 2, 2);
            button3.Name = "button3";
            button3.Size = new Size(262, 104);
            button3.TabIndex = 2;
            button3.Text = "Order Management";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(346, 201);
            button4.Margin = new Padding(2, 2, 2, 2);
            button4.Name = "button4";
            button4.Size = new Size(267, 104);
            button4.TabIndex = 3;
            button4.Text = " Management";
            button4.UseVisualStyleBackColor = true;
            // 
            // AdminMenuUI
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(640, 360);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Margin = new Padding(2, 2, 2, 2);
            Name = "AdminMenuUI";
            Text = "AdminMenuUI";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}
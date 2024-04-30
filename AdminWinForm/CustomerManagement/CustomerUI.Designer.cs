namespace AdminWinForm.CustomerManagement
{
    partial class CustomerUI
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
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(44, 31);
            button1.Name = "button1";
            button1.Size = new Size(301, 370);
            button1.TabIndex = 0;
            button1.Text = "Private Customer";
            button1.UseVisualStyleBackColor = true;
            button1.Click += privateCustomer_Click;
            // 
            // button2
            // 
            button2.Location = new Point(387, 31);
            button2.Name = "button2";
            button2.Size = new Size(301, 370);
            button2.TabIndex = 1;
            button2.Text = "Business Customer";
            button2.UseVisualStyleBackColor = true;
            button2.Click += businessCustomer_Click;
            // 
            // CustomerUI
            // 
            AutoScaleDimensions = new SizeF(11F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(772, 432);
            Controls.Add(button2);
            Controls.Add(button1);
            Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "CustomerUI";
            Text = "CustomerUI";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
    }
}
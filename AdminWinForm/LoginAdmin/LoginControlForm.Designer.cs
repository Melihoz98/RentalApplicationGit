namespace AdminWinForm.LoginAdmin
{
    partial class LoginControlForm
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
            LoginTextLabel = new Label();
            UserNameLabel = new Label();
            PasswordLabel = new Label();
            UsernameBox = new TextBox();
            PasswordBox = new TextBox();
            LoginButton = new Button();
            SuspendLayout();
            // 
            // LoginTextLabel
            // 
            LoginTextLabel.AutoSize = true;
            LoginTextLabel.Font = new Font("Segoe UI", 15F);
            LoginTextLabel.Location = new Point(339, 68);
            LoginTextLabel.Name = "LoginTextLabel";
            LoginTextLabel.Size = new Size(110, 28);
            LoginTextLabel.TabIndex = 0;
            LoginTextLabel.Text = "Login page";
            // 
            // UserNameLabel
            // 
            UserNameLabel.AutoSize = true;
            UserNameLabel.Location = new Point(220, 135);
            UserNameLabel.Name = "UserNameLabel";
            UserNameLabel.Size = new Size(60, 15);
            UserNameLabel.TabIndex = 1;
            UserNameLabel.Text = "Username";
            // 
            // PasswordLabel
            // 
            PasswordLabel.AutoSize = true;
            PasswordLabel.Location = new Point(220, 177);
            PasswordLabel.Name = "PasswordLabel";
            PasswordLabel.Size = new Size(57, 15);
            PasswordLabel.TabIndex = 2;
            PasswordLabel.Text = "Password";
            // 
            // UsernameBox
            // 
            UsernameBox.Location = new Point(371, 135);
            UsernameBox.Name = "UsernameBox";
            UsernameBox.Size = new Size(100, 23);
            UsernameBox.TabIndex = 3;
            // 
            // PasswordBox
            // 
            PasswordBox.Location = new Point(371, 177);
            PasswordBox.Name = "PasswordBox";
            PasswordBox.Size = new Size(100, 23);
            PasswordBox.TabIndex = 4;
            // 
            // LoginButton
            // 
            LoginButton.Location = new Point(374, 227);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(75, 23);
            LoginButton.TabIndex = 5;
            LoginButton.Text = "Login";
            LoginButton.UseVisualStyleBackColor = true;
            // 
            // LoginControlForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(LoginButton);
            Controls.Add(PasswordBox);
            Controls.Add(UsernameBox);
            Controls.Add(PasswordLabel);
            Controls.Add(UserNameLabel);
            Controls.Add(LoginTextLabel);
            Name = "LoginControlForm";
            Text = "LoginControlForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LoginTextLabel;
        private Label UserNameLabel;
        private Label PasswordLabel;
        private TextBox UsernameBox;
        private TextBox PasswordBox;
        private Button LoginButton;
    }
}
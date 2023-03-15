namespace OTKInformationSystem
{
    partial class AuthorizationForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            authorizationLabel = new Label();
            loginLabel = new Label();
            passwordLabel = new Label();
            loginTextBox = new TextBox();
            passwordTextBox = new TextBox();
            showPasswordCheckBox = new CheckBox();
            logInButton = new Button();
            logInPanel = new Panel();
            logInPanel.SuspendLayout();
            SuspendLayout();
            // 
            // authorizationLabel
            // 
            authorizationLabel.AutoSize = true;
            authorizationLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            authorizationLabel.Location = new Point(21, 15);
            authorizationLabel.Name = "authorizationLabel";
            authorizationLabel.Size = new Size(393, 21);
            authorizationLabel.TabIndex = 0;
            authorizationLabel.Text = "Для получения доступа к системе ОТК авторизуйтесь";
            // 
            // loginLabel
            // 
            loginLabel.AutoSize = true;
            loginLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            loginLabel.Location = new Point(18, 56);
            loginLabel.Name = "loginLabel";
            loginLabel.Size = new Size(57, 21);
            loginLabel.TabIndex = 1;
            loginLabel.Text = "Логин:";
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            passwordLabel.Location = new Point(18, 87);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(66, 21);
            passwordLabel.TabIndex = 2;
            passwordLabel.Text = "Пароль:";
            // 
            // loginTextBox
            // 
            loginTextBox.Location = new Point(90, 54);
            loginTextBox.Name = "loginTextBox";
            loginTextBox.Size = new Size(207, 23);
            loginTextBox.TabIndex = 3;
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(90, 87);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(207, 23);
            passwordTextBox.TabIndex = 4;
            passwordTextBox.UseSystemPasswordChar = true;
            // 
            // showPasswordCheckBox
            // 
            showPasswordCheckBox.AutoSize = true;
            showPasswordCheckBox.Location = new Point(303, 91);
            showPasswordCheckBox.Name = "showPasswordCheckBox";
            showPasswordCheckBox.Size = new Size(119, 19);
            showPasswordCheckBox.TabIndex = 5;
            showPasswordCheckBox.Text = "Показать пароль";
            showPasswordCheckBox.UseVisualStyleBackColor = true;
            showPasswordCheckBox.CheckedChanged += ShowPassword;
            // 
            // logInButton
            // 
            logInButton.Location = new Point(173, 125);
            logInButton.Name = "logInButton";
            logInButton.Size = new Size(94, 31);
            logInButton.TabIndex = 6;
            logInButton.Text = "Войти";
            logInButton.UseVisualStyleBackColor = true;
            logInButton.Click += LogIn;
            // 
            // logInPanel
            // 
            logInPanel.Anchor = AnchorStyles.None;
            logInPanel.AutoSize = true;
            logInPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            logInPanel.Controls.Add(loginTextBox);
            logInPanel.Controls.Add(authorizationLabel);
            logInPanel.Controls.Add(passwordTextBox);
            logInPanel.Controls.Add(showPasswordCheckBox);
            logInPanel.Controls.Add(logInButton);
            logInPanel.Controls.Add(loginLabel);
            logInPanel.Controls.Add(passwordLabel);
            logInPanel.Location = new Point(2, 1);
            logInPanel.Name = "logInPanel";
            logInPanel.Size = new Size(425, 159);
            logInPanel.TabIndex = 7;
            // 
            // AuthorizationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 161);
            Controls.Add(logInPanel);
            Name = "AuthorizationForm";
            Text = "Авторизация";
            logInPanel.ResumeLayout(false);
            logInPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label authorizationLabel;
        private Label loginLabel;
        private Label passwordLabel;
        private TextBox loginTextBox;
        private TextBox passwordTextBox;
        private CheckBox showPasswordCheckBox;
        private Button logInButton;
        private Panel logInPanel;
    }
}
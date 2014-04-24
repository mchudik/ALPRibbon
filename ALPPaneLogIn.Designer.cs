namespace ALPRibbon
{
    partial class ALPPaneLogIn
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EchoLogoPictureBox = new System.Windows.Forms.PictureBox();
            this.UserNameTextBox = new System.Windows.Forms.TextBox();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.LogInButton = new System.Windows.Forms.Button();
            this.ForgoPasswordLink = new System.Windows.Forms.LinkLabel();
            this.CredentialsLabel = new System.Windows.Forms.Label();
            this.DividerPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.EchoLogoPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // EchoLogoPictureBox
            // 
            this.EchoLogoPictureBox.Image = global::ALPRibbon.Properties.Resources.echo360_logo_blue;
            this.EchoLogoPictureBox.Location = new System.Drawing.Point(23, 46);
            this.EchoLogoPictureBox.Name = "EchoLogoPictureBox";
            this.EchoLogoPictureBox.Size = new System.Drawing.Size(207, 74);
            this.EchoLogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.EchoLogoPictureBox.TabIndex = 0;
            this.EchoLogoPictureBox.TabStop = false;
            // 
            // UserNameTextBox
            // 
            this.UserNameTextBox.Location = new System.Drawing.Point(50, 244);
            this.UserNameTextBox.Name = "UserNameTextBox";
            this.UserNameTextBox.Size = new System.Drawing.Size(150, 22);
            this.UserNameTextBox.TabIndex = 1;
            this.UserNameTextBox.Text = "Email Address";
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(50, 295);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(150, 22);
            this.PasswordTextBox.TabIndex = 2;
            this.PasswordTextBox.Text = "Password";
            // 
            // LogInButton
            // 
            this.LogInButton.AutoEllipsis = true;
            this.LogInButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.LogInButton.FlatAppearance.BorderSize = 0;
            this.LogInButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DodgerBlue;
            this.LogInButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SkyBlue;
            this.LogInButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.LogInButton.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogInButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.LogInButton.Location = new System.Drawing.Point(157, 376);
            this.LogInButton.Name = "LogInButton";
            this.LogInButton.Size = new System.Drawing.Size(75, 40);
            this.LogInButton.TabIndex = 3;
            this.LogInButton.Text = "Login";
            this.LogInButton.UseVisualStyleBackColor = false;
            // 
            // ForgoPasswordLink
            // 
            this.ForgoPasswordLink.AutoSize = true;
            this.ForgoPasswordLink.Location = new System.Drawing.Point(94, 440);
            this.ForgoPasswordLink.Name = "ForgoPasswordLink";
            this.ForgoPasswordLink.Size = new System.Drawing.Size(153, 17);
            this.ForgoPasswordLink.TabIndex = 4;
            this.ForgoPasswordLink.TabStop = true;
            this.ForgoPasswordLink.Text = "Forgot your password?";
            // 
            // CredentialsLabel
            // 
            this.CredentialsLabel.AutoSize = true;
            this.CredentialsLabel.Location = new System.Drawing.Point(18, 198);
            this.CredentialsLabel.Name = "CredentialsLabel";
            this.CredentialsLabel.Size = new System.Drawing.Size(245, 17);
            this.CredentialsLabel.TabIndex = 0;
            this.CredentialsLabel.Text = "Please provide your login credentials.";
            // 
            // DividerPanel
            // 
            this.DividerPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DividerPanel.Location = new System.Drawing.Point(23, 143);
            this.DividerPanel.Name = "DividerPanel";
            this.DividerPanel.Size = new System.Drawing.Size(207, 4);
            this.DividerPanel.TabIndex = 5;
            // 
            // ALPPaneLogIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.DividerPanel);
            this.Controls.Add(this.CredentialsLabel);
            this.Controls.Add(this.ForgoPasswordLink);
            this.Controls.Add(this.LogInButton);
            this.Controls.Add(this.PasswordTextBox);
            this.Controls.Add(this.UserNameTextBox);
            this.Controls.Add(this.EchoLogoPictureBox);
            this.Name = "ALPPaneLogIn";
            this.Size = new System.Drawing.Size(278, 470);
            ((System.ComponentModel.ISupportInitialize)(this.EchoLogoPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox EchoLogoPictureBox;
        private System.Windows.Forms.TextBox UserNameTextBox;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Button LogInButton;
        private System.Windows.Forms.LinkLabel ForgoPasswordLink;
        private System.Windows.Forms.Label CredentialsLabel;
        private System.Windows.Forms.Panel DividerPanel;
    }
}

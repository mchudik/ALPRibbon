namespace ALPRibbon
{
    partial class ALPPaneFreeResponse
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
            this.QuestionLabel = new System.Windows.Forms.Label();
            this.QuestionTextBox = new System.Windows.Forms.TextBox();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.AttachFileName = new System.Windows.Forms.LinkLabel();
            this.AttachFileLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // QuestionLabel
            // 
            this.QuestionLabel.AutoSize = true;
            this.QuestionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuestionLabel.Location = new System.Drawing.Point(18, 13);
            this.QuestionLabel.Name = "QuestionLabel";
            this.QuestionLabel.Size = new System.Drawing.Size(68, 18);
            this.QuestionLabel.TabIndex = 3;
            this.QuestionLabel.Text = "Question";
            // 
            // QuestionTextBox
            // 
            this.QuestionTextBox.Location = new System.Drawing.Point(21, 39);
            this.QuestionTextBox.Multiline = true;
            this.QuestionTextBox.Name = "QuestionTextBox";
            this.QuestionTextBox.Size = new System.Drawing.Size(578, 40);
            this.QuestionTextBox.TabIndex = 4;
            this.QuestionTextBox.Text = "Define Question";
            // 
            // SubmitButton
            // 
            this.SubmitButton.Location = new System.Drawing.Point(21, 471);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(578, 29);
            this.SubmitButton.TabIndex = 20;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // AttachFileName
            // 
            this.AttachFileName.AutoSize = true;
            this.AttachFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttachFileName.Location = new System.Drawing.Point(100, 85);
            this.AttachFileName.Name = "AttachFileName";
            this.AttachFileName.Size = new System.Drawing.Size(108, 18);
            this.AttachFileName.TabIndex = 25;
            this.AttachFileName.TabStop = true;
            this.AttachFileName.Text = "Click To Select";
            this.AttachFileName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AttachFileName_LinkClicked);
            // 
            // AttachFileLabel
            // 
            this.AttachFileLabel.AutoSize = true;
            this.AttachFileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttachFileLabel.Location = new System.Drawing.Point(21, 85);
            this.AttachFileLabel.Name = "AttachFileLabel";
            this.AttachFileLabel.Size = new System.Drawing.Size(76, 18);
            this.AttachFileLabel.TabIndex = 24;
            this.AttachFileLabel.Text = "Attach File";
            // 
            // ALPPaneFreeResponse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.AttachFileName);
            this.Controls.Add(this.AttachFileLabel);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.QuestionTextBox);
            this.Controls.Add(this.QuestionLabel);
            this.Name = "ALPPaneFreeResponse";
            this.Size = new System.Drawing.Size(602, 526);
            this.Resize += new System.EventHandler(this.OnResize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label QuestionLabel;
        private System.Windows.Forms.TextBox QuestionTextBox;
        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.LinkLabel AttachFileName;
        private System.Windows.Forms.Label AttachFileLabel;
    }
}

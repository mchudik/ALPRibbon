namespace ALPRibbon
{
    partial class ALPPaneImageQuiz
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
            this.JustificationTextBox = new System.Windows.Forms.TextBox();
            this.AddJustificationCheckBox = new System.Windows.Forms.CheckBox();
            this.JustificationDescTextBox = new System.Windows.Forms.TextBox();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.ImagePictureBox = new System.Windows.Forms.PictureBox();
            this.ImageLabel = new System.Windows.Forms.Label();
            this.ImageNameLabel = new System.Windows.Forms.LinkLabel();
            this.AttachFileName = new System.Windows.Forms.LinkLabel();
            this.AttachFileLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).BeginInit();
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
            // JustificationTextBox
            // 
            this.JustificationTextBox.Location = new System.Drawing.Point(21, 461);
            this.JustificationTextBox.Multiline = true;
            this.JustificationTextBox.Name = "JustificationTextBox";
            this.JustificationTextBox.Size = new System.Drawing.Size(578, 40);
            this.JustificationTextBox.TabIndex = 7;
            this.JustificationTextBox.Text = "Briefly explain your answer.";
            // 
            // AddJustificationCheckBox
            // 
            this.AddJustificationCheckBox.AutoSize = true;
            this.AddJustificationCheckBox.Location = new System.Drawing.Point(21, 409);
            this.AddJustificationCheckBox.Name = "AddJustificationCheckBox";
            this.AddJustificationCheckBox.Size = new System.Drawing.Size(133, 21);
            this.AddJustificationCheckBox.TabIndex = 8;
            this.AddJustificationCheckBox.Text = "Add Justification";
            this.AddJustificationCheckBox.UseVisualStyleBackColor = true;
            // 
            // JustificationDescTextBox
            // 
            this.JustificationDescTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.JustificationDescTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.JustificationDescTextBox.Enabled = false;
            this.JustificationDescTextBox.Location = new System.Drawing.Point(21, 432);
            this.JustificationDescTextBox.Multiline = true;
            this.JustificationDescTextBox.Name = "JustificationDescTextBox";
            this.JustificationDescTextBox.Size = new System.Drawing.Size(578, 23);
            this.JustificationDescTextBox.TabIndex = 10;
            this.JustificationDescTextBox.Text = "Check this box to ask students to add a follow up free response.";
            // 
            // SubmitButton
            // 
            this.SubmitButton.Location = new System.Drawing.Point(21, 517);
            this.SubmitButton.Name = "SubmitButton";
            this.SubmitButton.Size = new System.Drawing.Size(578, 29);
            this.SubmitButton.TabIndex = 20;
            this.SubmitButton.Text = "Submit";
            this.SubmitButton.UseVisualStyleBackColor = true;
            this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
            // 
            // ImagePictureBox
            // 
            this.ImagePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ImagePictureBox.Location = new System.Drawing.Point(21, 110);
            this.ImagePictureBox.Name = "ImagePictureBox";
            this.ImagePictureBox.Size = new System.Drawing.Size(578, 251);
            this.ImagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImagePictureBox.TabIndex = 21;
            this.ImagePictureBox.TabStop = false;
            this.ImagePictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.ImagePictureBox_Paint);
            this.ImagePictureBox.DoubleClick += new System.EventHandler(this.ImagePictureBox_DoubleClick);
            this.ImagePictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImagePictureBox_MouseDown);
            this.ImagePictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImagePictureBox_MouseMove);
            this.ImagePictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImagePictureBox_MouseUp);
            // 
            // ImageLabel
            // 
            this.ImageLabel.AutoSize = true;
            this.ImageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImageLabel.Location = new System.Drawing.Point(18, 85);
            this.ImageLabel.Name = "ImageLabel";
            this.ImageLabel.Size = new System.Drawing.Size(48, 18);
            this.ImageLabel.TabIndex = 22;
            this.ImageLabel.Text = "Image";
            // 
            // ImageNameLabel
            // 
            this.ImageNameLabel.AutoSize = true;
            this.ImageNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImageNameLabel.Location = new System.Drawing.Point(92, 85);
            this.ImageNameLabel.Name = "ImageNameLabel";
            this.ImageNameLabel.Size = new System.Drawing.Size(108, 18);
            this.ImageNameLabel.TabIndex = 23;
            this.ImageNameLabel.TabStop = true;
            this.ImageNameLabel.Text = "Click To Select";
            this.ImageNameLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ImageNameLabel_LinkClicked);
            // 
            // AttachFileName
            // 
            this.AttachFileName.AutoSize = true;
            this.AttachFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttachFileName.Location = new System.Drawing.Point(100, 376);
            this.AttachFileName.Name = "AttachFileName";
            this.AttachFileName.Size = new System.Drawing.Size(108, 18);
            this.AttachFileName.TabIndex = 29;
            this.AttachFileName.TabStop = true;
            this.AttachFileName.Text = "Click To Select";
            this.AttachFileName.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AttachFileName_LinkClicked);
            // 
            // AttachFileLabel
            // 
            this.AttachFileLabel.AutoSize = true;
            this.AttachFileLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AttachFileLabel.Location = new System.Drawing.Point(21, 376);
            this.AttachFileLabel.Name = "AttachFileLabel";
            this.AttachFileLabel.Size = new System.Drawing.Size(76, 18);
            this.AttachFileLabel.TabIndex = 28;
            this.AttachFileLabel.Text = "Attach File";
            // 
            // ALPPaneImageQuiz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.AttachFileName);
            this.Controls.Add(this.AttachFileLabel);
            this.Controls.Add(this.ImageNameLabel);
            this.Controls.Add(this.ImageLabel);
            this.Controls.Add(this.ImagePictureBox);
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.JustificationDescTextBox);
            this.Controls.Add(this.AddJustificationCheckBox);
            this.Controls.Add(this.JustificationTextBox);
            this.Controls.Add(this.QuestionTextBox);
            this.Controls.Add(this.QuestionLabel);
            this.Name = "ALPPaneImageQuiz";
            this.Size = new System.Drawing.Size(602, 581);
            this.Resize += new System.EventHandler(this.OnResize);
            ((System.ComponentModel.ISupportInitialize)(this.ImagePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label QuestionLabel;
        private System.Windows.Forms.TextBox QuestionTextBox;
        private System.Windows.Forms.TextBox JustificationTextBox;
        private System.Windows.Forms.CheckBox AddJustificationCheckBox;
        private System.Windows.Forms.TextBox JustificationDescTextBox;
        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.PictureBox ImagePictureBox;
        private System.Windows.Forms.Label ImageLabel;
        private System.Windows.Forms.LinkLabel ImageNameLabel;
        private System.Windows.Forms.LinkLabel AttachFileName;
        private System.Windows.Forms.Label AttachFileLabel;
    }
}

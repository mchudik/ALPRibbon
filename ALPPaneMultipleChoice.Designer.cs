namespace ALPRibbon
{
    partial class ALPPaneMultipleChoice
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
            this.AnswersLabel = new System.Windows.Forms.Label();
            this.JustificationTextBox = new System.Windows.Forms.TextBox();
            this.JustificationLabel = new System.Windows.Forms.Label();
            this.AddJustificationCheckBox = new System.Windows.Forms.CheckBox();
            this.AnswerDescTextBox = new System.Windows.Forms.TextBox();
            this.JustificationDescTextBox = new System.Windows.Forms.TextBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.SubmitButton = new System.Windows.Forms.Button();
            this.Correct = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Answer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
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
            // AnswersLabel
            // 
            this.AnswersLabel.AutoSize = true;
            this.AnswersLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AnswersLabel.Location = new System.Drawing.Point(18, 103);
            this.AnswersLabel.Name = "AnswersLabel";
            this.AnswersLabel.Size = new System.Drawing.Size(65, 18);
            this.AnswersLabel.TabIndex = 5;
            this.AnswersLabel.Text = "Answers";
            // 
            // JustificationTextBox
            // 
            this.JustificationTextBox.Location = new System.Drawing.Point(21, 415);
            this.JustificationTextBox.Multiline = true;
            this.JustificationTextBox.Name = "JustificationTextBox";
            this.JustificationTextBox.Size = new System.Drawing.Size(578, 40);
            this.JustificationTextBox.TabIndex = 7;
            this.JustificationTextBox.Text = "Briefly explain your answer.";
            // 
            // JustificationLabel
            // 
            this.JustificationLabel.AutoSize = true;
            this.JustificationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JustificationLabel.Location = new System.Drawing.Point(18, 342);
            this.JustificationLabel.Name = "JustificationLabel";
            this.JustificationLabel.Size = new System.Drawing.Size(86, 18);
            this.JustificationLabel.TabIndex = 6;
            this.JustificationLabel.Text = "Justification";
            // 
            // AddJustificationCheckBox
            // 
            this.AddJustificationCheckBox.AutoSize = true;
            this.AddJustificationCheckBox.Location = new System.Drawing.Point(21, 363);
            this.AddJustificationCheckBox.Name = "AddJustificationCheckBox";
            this.AddJustificationCheckBox.Size = new System.Drawing.Size(133, 21);
            this.AddJustificationCheckBox.TabIndex = 8;
            this.AddJustificationCheckBox.Text = "Add Justification";
            this.AddJustificationCheckBox.UseVisualStyleBackColor = true;
            // 
            // AnswerDescTextBox
            // 
            this.AnswerDescTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.AnswerDescTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AnswerDescTextBox.Enabled = false;
            this.AnswerDescTextBox.Location = new System.Drawing.Point(21, 124);
            this.AnswerDescTextBox.Multiline = true;
            this.AnswerDescTextBox.Name = "AnswerDescTextBox";
            this.AnswerDescTextBox.Size = new System.Drawing.Size(578, 21);
            this.AnswerDescTextBox.TabIndex = 9;
            this.AnswerDescTextBox.Text = "You can select more than one correct answer.";
            // 
            // JustificationDescTextBox
            // 
            this.JustificationDescTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.JustificationDescTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.JustificationDescTextBox.Enabled = false;
            this.JustificationDescTextBox.Location = new System.Drawing.Point(21, 386);
            this.JustificationDescTextBox.Multiline = true;
            this.JustificationDescTextBox.Name = "JustificationDescTextBox";
            this.JustificationDescTextBox.Size = new System.Drawing.Size(578, 23);
            this.JustificationDescTextBox.TabIndex = 10;
            this.JustificationDescTextBox.Text = "Check this box to ask students to add a follow up free response.";
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Correct,
            this.Answer});
            this.dataGridView.Location = new System.Drawing.Point(21, 151);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(578, 172);
            this.dataGridView.TabIndex = 19;
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
            // Correct
            // 
            this.Correct.HeaderText = "Correct";
            this.Correct.Name = "Correct";
            this.Correct.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Correct.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Correct.Width = 70;
            // 
            // Answer
            // 
            this.Answer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Answer.HeaderText = "Answer";
            this.Answer.Name = "Answer";
            // 
            // ALPPaneMultipleChoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.SubmitButton);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.JustificationDescTextBox);
            this.Controls.Add(this.AnswerDescTextBox);
            this.Controls.Add(this.AddJustificationCheckBox);
            this.Controls.Add(this.JustificationTextBox);
            this.Controls.Add(this.JustificationLabel);
            this.Controls.Add(this.AnswersLabel);
            this.Controls.Add(this.QuestionTextBox);
            this.Controls.Add(this.QuestionLabel);
            this.Name = "ALPPaneMultipleChoice";
            this.Size = new System.Drawing.Size(602, 526);
            this.Resize += new System.EventHandler(this.OnResize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label QuestionLabel;
        private System.Windows.Forms.TextBox QuestionTextBox;
        private System.Windows.Forms.Label AnswersLabel;
        private System.Windows.Forms.TextBox JustificationTextBox;
        private System.Windows.Forms.Label JustificationLabel;
        private System.Windows.Forms.CheckBox AddJustificationCheckBox;
        private System.Windows.Forms.TextBox AnswerDescTextBox;
        private System.Windows.Forms.TextBox JustificationDescTextBox;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button SubmitButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Correct;
        private System.Windows.Forms.DataGridViewTextBoxColumn Answer;
    }
}

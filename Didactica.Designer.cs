namespace Didactica
{
    partial class Didactica
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
            taskDescriptionTextBox = new TextBox();
            textToEvaluateTextBox = new TextBox();
            sendQueryButton = new Button();
            taskDescriptionText = new TextBox();
            responseTextBox = new TextBox();
            copyToClipboardButton = new Button();
            textToEvaluateDescription = new TextBox();
            folderLoopButton = new Button();
            SuspendLayout();
            // 
            // taskDescriptionTextBox
            // 
            taskDescriptionTextBox.Location = new Point(12, 56);
            taskDescriptionTextBox.Multiline = true;
            taskDescriptionTextBox.Name = "taskDescriptionTextBox";
            taskDescriptionTextBox.ScrollBars = ScrollBars.Vertical;
            taskDescriptionTextBox.Size = new Size(501, 325);
            taskDescriptionTextBox.TabIndex = 0;
            // 
            // textToEvaluateTextBox
            // 
            textToEvaluateTextBox.Location = new Point(522, 56);
            textToEvaluateTextBox.Multiline = true;
            textToEvaluateTextBox.Name = "textToEvaluateTextBox";
            textToEvaluateTextBox.ScrollBars = ScrollBars.Vertical;
            textToEvaluateTextBox.Size = new Size(501, 325);
            textToEvaluateTextBox.TabIndex = 1;
            // 
            // sendQueryButton
            // 
            sendQueryButton.Location = new Point(458, 387);
            sendQueryButton.Name = "sendQueryButton";
            sendQueryButton.Size = new Size(116, 23);
            sendQueryButton.TabIndex = 2;
            sendQueryButton.Text = "Send spørring";
            sendQueryButton.UseVisualStyleBackColor = true;
            sendQueryButton.Click += button1_Click;
            // 
            // taskDescriptionText
            // 
            taskDescriptionText.BackColor = SystemColors.Control;
            taskDescriptionText.BorderStyle = BorderStyle.None;
            taskDescriptionText.Location = new Point(12, 34);
            taskDescriptionText.Name = "taskDescriptionText";
            taskDescriptionText.Size = new Size(371, 16);
            taskDescriptionText.TabIndex = 3;
            taskDescriptionText.Text = "Oppgavebeskrivelse, hva du ønsker at AI-en skal hjelpe deg med";
            // 
            // responseTextBox
            // 
            responseTextBox.Location = new Point(12, 416);
            responseTextBox.Multiline = true;
            responseTextBox.Name = "responseTextBox";
            responseTextBox.ScrollBars = ScrollBars.Vertical;
            responseTextBox.Size = new Size(1011, 303);
            responseTextBox.TabIndex = 4;
            // 
            // copyToClipboardButton
            // 
            copyToClipboardButton.Location = new Point(851, 387);
            copyToClipboardButton.Name = "copyToClipboardButton";
            copyToClipboardButton.Size = new Size(172, 23);
            copyToClipboardButton.TabIndex = 5;
            copyToClipboardButton.Text = "Kopier til utklippstavle";
            copyToClipboardButton.UseVisualStyleBackColor = true;
            copyToClipboardButton.Click += button2_Click;
            // 
            // textToEvaluateDescription
            // 
            textToEvaluateDescription.BackColor = SystemColors.Control;
            textToEvaluateDescription.BorderStyle = BorderStyle.None;
            textToEvaluateDescription.Location = new Point(522, 34);
            textToEvaluateDescription.Name = "textToEvaluateDescription";
            textToEvaluateDescription.Size = new Size(263, 16);
            textToEvaluateDescription.TabIndex = 6;
            textToEvaluateDescription.Text = "Tekst som skal evaluere iht. oppgavebeskrivelsen";
            // 
            // folderLoopButton
            // 
            folderLoopButton.Location = new Point(851, 27);
            folderLoopButton.Name = "folderLoopButton";
            folderLoopButton.Size = new Size(172, 23);
            folderLoopButton.TabIndex = 7;
            folderLoopButton.Text = "Angi mappe å bearbeide";
            folderLoopButton.UseVisualStyleBackColor = true;
            folderLoopButton.Click += this.button3_Click;
            // 
            // Didactica
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1035, 731);
            Controls.Add(folderLoopButton);
            Controls.Add(textToEvaluateDescription);
            Controls.Add(copyToClipboardButton);
            Controls.Add(responseTextBox);
            Controls.Add(taskDescriptionText);
            Controls.Add(sendQueryButton);
            Controls.Add(textToEvaluateTextBox);
            Controls.Add(taskDescriptionTextBox);
            Name = "Didactica";
            Text = "Didactica";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox taskDescriptionTextBox;
        private TextBox textToEvaluateTextBox;
        private Button sendQueryButton;
        private TextBox taskDescriptionText;
        private TextBox responseTextBox;
        private Button copyToClipboardButton;
        private TextBox textToEvaluateDescription;
        private Button folderLoopButton;
    }
}

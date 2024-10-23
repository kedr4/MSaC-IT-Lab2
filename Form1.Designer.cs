using System.Reflection;

namespace MSaC_IT_Lab2
{
    partial class Form1
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
            chooseFileButton = new Button();
            analyzeFileButton = new Button();
            codeRichTextBox = new RichTextBox();
            resultRichTextBox = new RichTextBox();
            SuspendLayout();
            // 
            // chooseFileButton
            // 
            chooseFileButton.Font = new Font("Bernard MT Condensed", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chooseFileButton.Location = new Point(558, 24);
            chooseFileButton.Name = "chooseFileButton";
            chooseFileButton.Size = new Size(230, 39);
            chooseFileButton.TabIndex = 0;
            chooseFileButton.Text = "Выберите файл";
            chooseFileButton.UseVisualStyleBackColor = true;
            chooseFileButton.Click += chooseFileButton_Click;
            // 
            // analyzeFileButton
            // 
            analyzeFileButton.Font = new Font("Bernard MT Condensed", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            analyzeFileButton.Location = new Point(558, 69);
            analyzeFileButton.Name = "analyzeFileButton";
            analyzeFileButton.Size = new Size(230, 38);
            analyzeFileButton.TabIndex = 2;
            analyzeFileButton.Text = "Анализировать";
            analyzeFileButton.UseVisualStyleBackColor = true;
            analyzeFileButton.Click += analyzeFileButton_Click;
            // 
            // codeRichTextBox
            // 
            codeRichTextBox.Location = new Point(12, 12);
            codeRichTextBox.Name = "codeRichTextBox";
            codeRichTextBox.Size = new Size(351, 426);
            codeRichTextBox.TabIndex = 3;
            codeRichTextBox.Text = "";
            // 
            // resultRichTextBox
            // 
            resultRichTextBox.Location = new Point(369, 124);
            resultRichTextBox.Name = "resultRichTextBox";
            resultRichTextBox.Size = new Size(419, 314);
            resultRichTextBox.TabIndex = 4;
            resultRichTextBox.Text = "";
            resultRichTextBox.TextChanged += resultRichTextBox_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(resultRichTextBox);
            Controls.Add(codeRichTextBox);
            Controls.Add(analyzeFileButton);
            Controls.Add(chooseFileButton);
            Name = "Form1";
            Text = "MSaC-IT-Lab2";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button chooseFileButton;
        private Button analyzeFileButton;
        internal RichTextBox codeRichTextBox;
        internal RichTextBox resultRichTextBox;
    }
}

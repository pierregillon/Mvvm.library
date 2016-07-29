using Mvvm.Winform.BindingToolkit.Controls;

namespace Mvvm.Example.Winform
{
    partial class CreateUserView
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.busyProgressBar = new System.Windows.Forms.ProgressBar();
            this.cancelButton = new Mvvm.Winform.BindingToolkit.Controls.CommandButton();
            this.createButton = new Mvvm.Winform.BindingToolkit.Controls.CommandButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.firstNameTextBox = new System.Windows.Forms.TextBox();
            this.lastNameTextBox = new System.Windows.Forms.TextBox();
            this.yearsOldLabel = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.birthDatePicker = new Mvvm.Winform.BindingToolkit.Controls.UltraDateTimePicker();
            this.nextValidationErrorLabel = new System.Windows.Forms.Label();
            this.showAllValidationErrorsButton = new Mvvm.Winform.BindingToolkit.Controls.CommandButton();
            this.hideValidationErrorsButton = new Mvvm.Winform.BindingToolkit.Controls.CommandButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.birthDatePicker)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.busyProgressBar);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.createButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 154);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(345, 29);
            this.panel1.TabIndex = 0;
            // 
            // busyProgressBar
            // 
            this.busyProgressBar.Location = new System.Drawing.Point(3, 3);
            this.busyProgressBar.MarqueeAnimationSpeed = 40;
            this.busyProgressBar.Name = "busyProgressBar";
            this.busyProgressBar.Size = new System.Drawing.Size(106, 23);
            this.busyProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.busyProgressBar.TabIndex = 2;
            this.busyProgressBar.Visible = false;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(186, 3);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // createButton
            // 
            this.createButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.createButton.Location = new System.Drawing.Point(267, 3);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(75, 23);
            this.createButton.TabIndex = 0;
            this.createButton.Text = "Create";
            this.createButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "First name :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Age :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Birth date :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Last name :";
            // 
            // firstNameTextBox
            // 
            this.firstNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorProvider.SetIconPadding(this.firstNameTextBox, 5);
            this.firstNameTextBox.Location = new System.Drawing.Point(78, 12);
            this.firstNameTextBox.Name = "firstNameTextBox";
            this.firstNameTextBox.Size = new System.Drawing.Size(242, 20);
            this.firstNameTextBox.TabIndex = 5;
            // 
            // lastNameTextBox
            // 
            this.lastNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorProvider.SetIconPadding(this.lastNameTextBox, 5);
            this.lastNameTextBox.Location = new System.Drawing.Point(78, 36);
            this.lastNameTextBox.Name = "lastNameTextBox";
            this.lastNameTextBox.Size = new System.Drawing.Size(242, 20);
            this.lastNameTextBox.TabIndex = 6;
            // 
            // yearsOldLabel
            // 
            this.yearsOldLabel.AutoSize = true;
            this.yearsOldLabel.Location = new System.Drawing.Point(77, 90);
            this.yearsOldLabel.Name = "yearsOldLabel";
            this.yearsOldLabel.Size = new System.Drawing.Size(13, 13);
            this.yearsOldLabel.TabIndex = 8;
            this.yearsOldLabel.Text = "5";
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // birthDatePicker
            // 
            this.birthDatePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.birthDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.errorProvider.SetIconPadding(this.birthDatePicker, 5);
            this.birthDatePicker.Location = new System.Drawing.Point(78, 60);
            this.birthDatePicker.Name = "birthDatePicker";
            this.birthDatePicker.NullText = "Choose a date";
            this.birthDatePicker.Size = new System.Drawing.Size(242, 20);
            this.birthDatePicker.TabIndex = 7;
            this.birthDatePicker.Value = null;
            // 
            // nextValidationErrorLabel
            // 
            this.nextValidationErrorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nextValidationErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.nextValidationErrorLabel.Location = new System.Drawing.Point(0, 130);
            this.nextValidationErrorLabel.Name = "nextValidationErrorLabel";
            this.nextValidationErrorLabel.Size = new System.Drawing.Size(251, 19);
            this.nextValidationErrorLabel.TabIndex = 9;
            this.nextValidationErrorLabel.Text = "validation errors";
            this.nextValidationErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // showAllValidationErrorsButton
            // 
            this.showAllValidationErrorsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showAllValidationErrorsButton.Location = new System.Drawing.Point(253, 128);
            this.showAllValidationErrorsButton.Name = "showAllValidationErrorsButton";
            this.showAllValidationErrorsButton.Size = new System.Drawing.Size(46, 23);
            this.showAllValidationErrorsButton.TabIndex = 10;
            this.showAllValidationErrorsButton.Text = "Show";
            this.showAllValidationErrorsButton.UseVisualStyleBackColor = true;
            // 
            // hideValidationErrorsButton
            // 
            this.hideValidationErrorsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.hideValidationErrorsButton.Location = new System.Drawing.Point(298, 128);
            this.hideValidationErrorsButton.Name = "hideValidationErrorsButton";
            this.hideValidationErrorsButton.Size = new System.Drawing.Size(44, 23);
            this.hideValidationErrorsButton.TabIndex = 11;
            this.hideValidationErrorsButton.Text = "Hide";
            this.hideValidationErrorsButton.UseVisualStyleBackColor = true;
            // 
            // CreateUserView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 183);
            this.Controls.Add(this.hideValidationErrorsButton);
            this.Controls.Add(this.showAllValidationErrorsButton);
            this.Controls.Add(this.nextValidationErrorLabel);
            this.Controls.Add(this.yearsOldLabel);
            this.Controls.Add(this.birthDatePicker);
            this.Controls.Add(this.lastNameTextBox);
            this.Controls.Add(this.firstNameTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "CreateUserView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CreateUserView";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.birthDatePicker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private CommandButton cancelButton;
        private CommandButton createButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox firstNameTextBox;
        private System.Windows.Forms.TextBox lastNameTextBox;
        private UltraDateTimePicker birthDatePicker;
        private System.Windows.Forms.Label yearsOldLabel;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Label nextValidationErrorLabel;
        private System.Windows.Forms.ProgressBar busyProgressBar;
        private CommandButton hideValidationErrorsButton;
        private CommandButton showAllValidationErrorsButton;
    }
}
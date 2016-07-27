using Mvvm.Example.Winform.Controls;

namespace Mvvm.Example.Winform
{
    partial class MainWindow
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
            this.displayCreateNewUserButton = new CommandButton();
            this.SuspendLayout();
            // 
            // displayCreateNewUserButton
            // 
            this.displayCreateNewUserButton.Location = new System.Drawing.Point(85, 111);
            this.displayCreateNewUserButton.Name = "displayCreateNewUserButton";
            this.displayCreateNewUserButton.Size = new System.Drawing.Size(103, 23);
            this.displayCreateNewUserButton.TabIndex = 0;
            this.displayCreateNewUserButton.Text = "Create new user";
            this.displayCreateNewUserButton.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.displayCreateNewUserButton);
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private CommandButton displayCreateNewUserButton;
    }
}


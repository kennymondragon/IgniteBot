namespace IgniteBot
{
	partial class LoginWindow
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
			this.label1 = new System.Windows.Forms.Label();
			this.accessCodeTextBox = new System.Windows.Forms.TextBox();
			this.submitButton = new System.Windows.Forms.Button();
			this.incorrectPassLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(85, 9);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(140, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Enter Access Code:";
			// 
			// accessCodeTextBox
			// 
			this.accessCodeTextBox.Location = new System.Drawing.Point(13, 41);
			this.accessCodeTextBox.Margin = new System.Windows.Forms.Padding(4);
			this.accessCodeTextBox.Name = "accessCodeTextBox";
			this.accessCodeTextBox.Size = new System.Drawing.Size(272, 24);
			this.accessCodeTextBox.TabIndex = 1;
			// 
			// submitButton
			// 
			this.submitButton.Location = new System.Drawing.Point(210, 72);
			this.submitButton.Name = "submitButton";
			this.submitButton.Size = new System.Drawing.Size(75, 33);
			this.submitButton.TabIndex = 2;
			this.submitButton.Text = "Submit";
			this.submitButton.UseVisualStyleBackColor = true;
			this.submitButton.Click += new System.EventHandler(this.SubmitButtonClick);
			// 
			// incorrectPassLabel
			// 
			this.incorrectPassLabel.AutoSize = true;
			this.incorrectPassLabel.ForeColor = System.Drawing.Color.Maroon;
			this.incorrectPassLabel.Location = new System.Drawing.Point(38, 79);
			this.incorrectPassLabel.Name = "incorrectPassLabel";
			this.incorrectPassLabel.Size = new System.Drawing.Size(141, 18);
			this.incorrectPassLabel.TabIndex = 3;
			this.incorrectPassLabel.Text = "Invalid Access Code";
			this.incorrectPassLabel.Visible = false;
			// 
			// LoginWindow
			// 
			this.AcceptButton = this.submitButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(298, 115);
			this.Controls.Add(this.incorrectPassLabel);
			this.Controls.Add(this.submitButton);
			this.Controls.Add(this.accessCodeTextBox);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "LoginWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "IgniteBot | Login";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox accessCodeTextBox;
		private System.Windows.Forms.Button submitButton;
		private System.Windows.Forms.Label incorrectPassLabel;
	}
}
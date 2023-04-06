using IgniteBot.Properties;
using System;
using System.Windows.Forms;

namespace IgniteBot
{
	public partial class LoginWindow : Form
	{
		public LoginWindow()
		{
			InitializeComponent();
		}

		private void SubmitButtonClick(object sender, EventArgs e)
		{
			CheckAccessCode(accessCodeTextBox.Text);
		}

		private void CheckAccessCode(string accessCode, bool showIncorrect = true)
		{
			if (Program.CheckAccessCode(accessCode))
			{
				Program.authorized = true;
				Settings.Default.accessCode = accessCode;
				Settings.Default.Save();

				Dispose();
			}
			else if (showIncorrect)
			{
				// show the user that their password is incorrect
				incorrectPassLabel.Visible = true;
			}
		}
	}
}

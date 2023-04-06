using IgniteBot.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Logger;

namespace IgniteBot
{
	public partial class LiveWindow : Form
	{

		private readonly Timer outputUpdateTimer = new Timer();

		List<ProgressBar> playerSpeedBars = new List<ProgressBar>();
		private string updateFilename = "";

		public LiveWindow()
		{
			InitializeComponent();

			outputUpdateTimer.Interval = 50;
			outputUpdateTimer.Tick += OutputLogUpdateTimer;
			outputUpdateTimer.Enabled = true;

			gameStateChangedCheckBox.Checked = Settings.Default.outputGameStateEvents;
			goalScoredCheckBox.Checked = Settings.Default.outputScoreEvents;
			stunCheckBox.Checked = Settings.Default.outputStunEvents;
			discThrownCheckBox.Checked = Settings.Default.outputDiscThrownEvents;
			discCaughtCheckBox.Checked = Settings.Default.outputDiscCaughtEvents;
			discStolenCheckBox.Checked = Settings.Default.outputDiscStolenEvents;
			savedCheckBox.Checked = Settings.Default.outputSaveEvents;

			accessCodeLabel.Text = "User: " + Program.currentAccessCodeUsername;
			versionLabel.Text = "v" + Application.ProductVersion;

			GenerateNewStatsId();

			for (int i = 0; i < 10; i++)
			{
				AddSpeedBar();
			}

		}

		private void AddSpeedBar()
		{
			var bar = new ColoredProgressBar();
			playerSpeedBars.Add(bar);
			bar.Height = 10;
			var margins = bar.Margin;
			margins.Top = 0;
			margins.Bottom = 0;
			bar.Margin = margins;
			bar.Width = 180;
			bar.Maximum = 200;
			statusFloxBox.Controls.Add(bar);
		}

		private void OutputLogUpdateTimer(object sender, EventArgs e)
		{
			lock (Program.logOutputWriteLock)
			{
				var newText = FilterLines(unusedFileCache.ToString());
				if (newText != string.Empty && newText != Environment.NewLine)
				{
					try
					{
						mainOutputTextBox.AppendText(newText);

						if (Program.writeToOBSHTMLFile) // TODO this file path won't work
						{
							// write to html file for overlay as well
							File.WriteAllText("html_output/events.html", @"
								<html>
								<head>
								<meta http-equiv=""refresh"" content=""1"">
								<link rel=""stylesheet"" type=""text/css"" href=""styles.css"">
								</head>
								<body>

								<div id=""info""> " +
									newText
									+ @"
								</div>

								</body>
								</html>
							");
						}
					}
					catch (Exception) { }

					ColorizeOutput("Entered state:", gameStateChangedCheckBox.ForeColor, mainOutputTextBox.Text.Length - newText.Length);
				}
				unusedFileCache.Clear();
			}


			// update the other labels in the stats box
			if (Program.lastFrame != null && Program.lastFrame.map_name != "mpl_lobby_b2")  // 'mpl_lobby_b2' may change in the future
			{
				sessionIdTextBox.Text = Program.lastFrame.sessionid;
				discSpeedText.Text = Program.lastFrame.disc.Velocity.Length() + " m/s";
				//discSpeedProgressBar.Value = (int)Program.lastFrame.disc.Velocity.Length();
				//if (Program.lastFrame.teams[0].possession)
				//{
				//	discSpeedProgressBar.ForeColor = Color.Blue;
				//} else if (Program.lastFrame.teams[1].possession)
				//{
				//	discSpeedProgressBar.ForeColor = Color.Orange;
				//} else
				//{
				//	discSpeedProgressBar.ForeColor = Color.Gray;
				//}

				string playerSpeedHTML = @"
				<html>
				<head>
				<meta http-equiv=""refresh"" content=""0.2"">
				<link rel=""stylesheet"" type=""text/css"" href=""styles.css"">
				</head>
				<body>
				<div id = ""player_speeds"">";
				bool updatedHTML = false;

				// loop through all the players and set their speed progress bars
				int i = 0;
				foreach (var team in Program.lastFrame.teams)
				{
					foreach (var player in team.players)
					{
						if (playerSpeedBars.Count > i)
						{
							playerSpeedBars[i].Visible = true;
							int speed = (int)(player.velocity.ToVector3().Length() * 10);
							if (speed > playerSpeedBars[i].Maximum) speed = playerSpeedBars[i].Maximum;
							playerSpeedBars[i].Value = speed;
							var color = team.color == equipo.Color.blue ? Color.DodgerBlue : Color.Orange;
							playerSpeedBars[i].ForeColor = color;
							playerSpeedBars[i].BackColor = color;
							i++;

							updatedHTML = true;
							playerSpeedHTML += "<div style=\"width:" + speed + "px;\" class=\"speed_bar " + team.color + "\"></div>\n";
						}
					}
				}

				if (updatedHTML && Program.writeToOBSHTMLFile)
				{
					playerSpeedHTML += "</div></body></html>";

					File.WriteAllText("html_output/player_speeds.html", playerSpeedHTML);
				}

				for (; i < playerSpeedBars.Count; i++)
				{
					playerSpeedBars[i].Visible = false;
				}
			}
			else
			{
				sessionIdTextBox.Text = "---";
				discSpeedText.Text = "--- m/s";
				//discSpeedProgressBar.Value = 0;
				//discSpeedProgressBar.ForeColor = Color.Gray;
				foreach (var bar in playerSpeedBars)
				{
					bar.Value = 0;
				}
			}

			connectedLabel.Text = Program.inGame ? "Connected" : "Not Connected";



			if (!Program.running)
			{
				outputUpdateTimer.Stop();
			}
		}

		private void LiveWindow_Load(object sender, EventArgs e)
		{
			lock (Program.logOutputWriteLock)
			{
				mainOutputTextBox.Lines = fullFileCache.ToArray();
			}

			_ = CheckForAppUpdate();
		}

		private async Task CheckForAppUpdate()
		{
			try
			{
				HttpClient updateClient = new HttpClient
				{
					BaseAddress = new Uri("https://ignitevr.gg/cgi-bin/EchoStats.cgi/")
				};
				var response = await updateClient.GetAsync("get_ignitebot_update");
				var respObj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
				string version = (string)respObj["version"];

				// if we need a new version
				if (version != Application.ProductVersion)
				{
					updateFilename = (string)respObj["filename"];
					updateButton.Visible = true;
				}
			}
			catch (HttpRequestException)
			{
				LogRow(LogType.Error, "Couldn't check for update.");
			}
		}

		private void CloseButtonClicked(object sender, EventArgs e)
		{
			outputUpdateTimer.Stop();
			Dispose();
		}

		private void SettingsButtonClicked(object sender, EventArgs e)
		{
			Program.ShowingSettings = !Program.ShowingSettings;
			if (Program.settingsWindow == null || Program.settingsWindow.IsDisposed)
			{
				Program.settingsWindow = new SettingsWindow();
				Program.settingsWindow.Show();
				Program.ShowingSettings = true;
			}
			else
			{
				Program.settingsWindow.Dispose();
				Program.ShowingSettings = false;
			}
		}

		private void QuitButtonClicked(object sender, EventArgs e)
		{
			// We must manually tidy up and remove the icon before we exit.
			// Otherwise it will be left behind until the user mouses over.
			Program.trayIcon.Visible = false;
			Program.running = false;
			Program.paused = false;
			Dispose();
			Application.Exit();
		}

		private void pauseButton_Click(object sender, EventArgs e)
		{
			Program.paused = !Program.paused;
			((Button)sender).Text = Program.paused ? "Continue" : "Pause";
		}

		private string FilterLines(string input)
		{
			string output = input;
			var lines = output
				.Split(new[] { '\r', '\n' })
				.Select(l => l.Trim())
				.Where(l =>
				{
					if (
					(!showHideLinesBox.Visible && l.Length > 0) || (
					(Settings.Default.outputGameStateEvents && l.Contains("Entered state:")) ||
					(Settings.Default.outputScoreEvents && l.Contains("scored")) ||
					(Settings.Default.outputStunEvents && l.Contains("just stunned")) ||
					(Settings.Default.outputDiscThrownEvents && l.Contains("threw the disk")) ||
					(Settings.Default.outputDiscCaughtEvents && l.Contains("caught the disk")) ||
					(Settings.Default.outputDiscStolenEvents && l.Contains("stole the disk")) ||
					(Settings.Default.outputSaveEvents && l.Contains("save"))
					))
					{
						return true;
					}
					else
					{
						return false;
					}
				});

			output = string.Join(Environment.NewLine, lines) + ((output != string.Empty) ? Environment.NewLine : string.Empty);

			return output;
		}

		private string FilterLines(List<string> input)
		{
			var lines = input
				.Select(l => l.Trim())
				.Where(l =>
				{
					if (
					(!showHideLinesBox.Visible && l.Length > 0) || (
					(Settings.Default.outputGameStateEvents && l.Contains("Entered state:")) ||
					(Settings.Default.outputScoreEvents && l.Contains("scored")) ||
					(Settings.Default.outputStunEvents && l.Contains("just stunned")) ||
					(Settings.Default.outputDiscThrownEvents && l.Contains("threw the disk")) ||
					(Settings.Default.outputDiscCaughtEvents && l.Contains("caught the disk")) ||
					(Settings.Default.outputDiscStolenEvents && l.Contains("stole the disk")) ||
					(Settings.Default.outputSaveEvents && l.Contains("save"))
					))
					{
						// Show this line
						return true;
					}
					else
					{
						// hide this line
						return false;
					}
				});

			var output = string.Join(Environment.NewLine, lines) + ((input.Count != 0 && input[0] != string.Empty) ? Environment.NewLine : string.Empty);

			return output;
		}

		private void RefilterOutput()
		{
			lock (Program.logOutputWriteLock)
			{
				mainOutputTextBox.Text = FilterLines(fullFileCache);
				ColorizeOutput("Entered state:", gameStateChangedCheckBox.ForeColor);
			}

			Settings.Default.Save();
		}

		private void ColorizeOutput(string target, Color color, int index = 0)
		{
			if (index < 0) index = 0;
			int length = mainOutputTextBox.Text.Length;
			while (index < length)
			{
				int pos = mainOutputTextBox.Text.IndexOf(target, index);
				if (pos < 0)
				{
					// Not found. Select nothing.
					mainOutputTextBox.Select(0, 0);
					break;
				}
				else
				{
					// Found the text. Select it.
					mainOutputTextBox.Select(pos, target.Length);
					mainOutputTextBox.SelectionColor = color;
				}
				index = pos + 1;
			}
			mainOutputTextBox.Select(mainOutputTextBox.Text.Length, mainOutputTextBox.Text.Length);
			mainOutputTextBox.ScrollToCaret();
		}

		private void gameStateChangedCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.outputGameStateEvents = ((CheckBox)sender).Checked;
			RefilterOutput();
		}

		private void goalScoredCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.outputScoreEvents = ((CheckBox)sender).Checked;
			RefilterOutput();

		}

		private void stunCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.outputStunEvents = ((CheckBox)sender).Checked;
			RefilterOutput();
		}

		private void discThrownCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.outputDiscThrownEvents = ((CheckBox)sender).Checked;
			RefilterOutput();
		}

		private void discCaughtCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.outputDiscCaughtEvents = ((CheckBox)sender).Checked;
			RefilterOutput();
		}

		private void discStolenCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.outputDiscStolenEvents = ((CheckBox)sender).Checked;
			RefilterOutput();
		}

		private void savedCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Settings.Default.outputSaveEvents = ((CheckBox)sender).Checked;
			RefilterOutput();
		}

		private void accessCodeLabel_Click(object sender, EventArgs e)
		{
			LoginWindow login = new LoginWindow();

			login.ShowDialog(this);

			accessCodeLabel.Text = "User: " + Program.currentAccessCodeUsername;

			login.Dispose();
		}

		private void SessionIDFocused(object sender, EventArgs e)
		{
			sessionIdTextBox.SelectAll();
		}

		private void clearButton_Click(object sender, EventArgs e)
		{
			lock (Program.logOutputWriteLock)
			{
				fullFileCache.Clear();
				mainOutputTextBox.Text = FilterLines(fullFileCache);
			}
		}

		private void customIdChanged(object sender, EventArgs e)
		{
			Program.customId = ((TextBox)sender).Text;
		}

		private void splitStatsButtonClick(object sender, EventArgs e)
		{
			GenerateNewStatsId();
		}

		private void GenerateNewStatsId()
		{
			using (SHA256 sha = SHA256.Create())
			{
				var hash = sha.ComputeHash(BitConverter.GetBytes(DateTime.Now.Ticks));
				// Convert the byte array to hexadecimal string
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < hash.Length; i++)
				{
					sb.Append(hash[i].ToString("X2"));
				}
				Program.customId = sb.ToString();
				customIdTextbox.Text = Program.customId;
			}
		}

		private void updateButton_Click(object sender, EventArgs e)
		{
			WebClient webClient = new WebClient();
			webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
			webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
			webClient.DownloadFileAsync(new Uri("https://ignitevr.gg/ignitebot_installers/" + updateFilename), Path.GetTempPath() + updateFilename);
		}

		private void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			updateProgressBar.Visible = true;
			updateProgressBar.Value = e.ProgressPercentage;
		}

		private void Completed(object sender, AsyncCompletedEventArgs e)
		{
			updateProgressBar.Visible = false;

			var result = MessageBox.Show("Download Finished. Install?", "", MessageBoxButtons.OKCancel);

			if (result == DialogResult.OK)
			{
				// Install the update
				Process installerProcess = new Process();
				ProcessStartInfo processInfo = new ProcessStartInfo();
				processInfo.FileName = Path.GetTempPath() + updateFilename;
				installerProcess.StartInfo = processInfo;
				installerProcess.Start();

				// We must manually tidy up and remove the icon before we exit.
				// Otherwise it will be left behind until the user mouses over.
				Program.trayIcon.Visible = false;
				Program.running = false;
				Program.paused = false;
				Dispose();
				Application.Exit();
			}
			else
			{
				// just do nothing
			}
			MessageBox.Show("Download completed!");
		}
	}
}

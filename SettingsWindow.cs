using IgniteBot.Properties;
using Microsoft.Win32;
using System;
using System.Threading;
using System.Windows.Forms;

namespace IgniteBot
{
	public partial class SettingsWindow : Form
	{
		public SettingsWindow()
		{
			InitializeComponent();
		}

		private void SettingsWindow_Load(object sender, EventArgs e)
		{
			startWithWindowsCheckbox.Checked = Settings.Default.startOnBoot;
			autorestartCheckbox.Checked = Settings.Default.autoRestart;

			enableStatsLogging.Checked = Settings.Default.enableStatsLogging;
			statsLoggingBox.Enabled = enableStatsLogging.Checked;
			uploadTimesComboBox.SelectedIndex = Settings.Default.whenToUploadLogs;
			uploadToFirestoreCheckBox.Checked = Settings.Default.uploadToFirestore;

			enableFullLoggingCheckbox.Checked = Settings.Default.enableFullLogging;
			currentFilenameLabel.Text = Program.fileName;
			batchWritesButton.Checked = Settings.Default.batchWrites;
			useCompressionButton.Checked = Settings.Default.useCompression;
			speedSelector.SelectedIndex = Settings.Default.targetDeltaTimeIndexFull;
			storageLocationTextBox.Text = Settings.Default.saveFolder;
			fullLoggingBox.Enabled = enableFullLoggingCheckbox.Checked;

			versionNum.Text = "v" + Application.ProductVersion.ToString();
		}

		void RestartOnCrashEvent(object sender, EventArgs e)
		{
			Program.autoRestart = ((CheckBox)sender).Checked;
			Settings.Default.autoRestart = Program.autoRestart;
			Settings.Default.Save();
		}

		private void StartWithWindowsEvent(object sender, EventArgs e)
		{
			Settings.Default.startOnBoot = ((CheckBox)sender).Checked;
			Settings.Default.Save();
			SetStartWithWindows(Settings.Default.startOnBoot);
		}

		private static void SetStartWithWindows(bool val)
		{
			RegistryKey rk = Registry.CurrentUser.OpenSubKey
						("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

			if (val)
				rk.SetValue(Resources.AppName, Application.ExecutablePath);
			else
				rk.DeleteValue(Resources.AppName, false);
		}

		private void SlowModeEvent(object sender, EventArgs e)
		{
			Program.deltaTimeIndexStats = ((CheckBox)sender).Checked ? 1 : 0;
			Settings.Default.targetDeltaTimeIndexStats = Program.deltaTimeIndexStats;
			Settings.Default.Save();
		}

		private void ShowDBLogEvent(object sender, EventArgs e)
		{
			Settings.Default.showDatabaseLog = ((CheckBox)sender).Checked;
			Settings.Default.Save();

			Program.showDatabaseLog = Settings.Default.showDatabaseLog;
		}

		private void LogToServerEvent(object sender, EventArgs e)
		{
			Settings.Default.logToServer = ((CheckBox)sender).Checked;
			Settings.Default.Save();

			Logger.enableLoggingRemote = Settings.Default.logToServer;
		}

		private void EnableStatsLoggingEvent(object sender, EventArgs e)
		{
			Program.enableStatsLogging = ((CheckBox)sender).Checked;
			Settings.Default.enableStatsLogging = Program.enableStatsLogging;
			Settings.Default.Save();

			statsLoggingBox.Enabled = Program.enableStatsLogging;
		}

		private void EnableFullLoggingEvent(object sender, EventArgs e)
		{
			Program.enableFullLogging = ((CheckBox)sender).Checked;
			Settings.Default.enableFullLogging = Program.enableFullLogging;
			Settings.Default.Save();

			fullLoggingBox.Enabled = Program.enableFullLogging;
		}

		private void CloseButtonEvent(object sender, EventArgs e)
		{
			Dispose();
		}

		private void SetStorageLocation(object sender, EventArgs e)
		{
			string selectedPath = "";
			Thread t = new Thread(() =>
			{
				FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
				if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
				{
					selectedPath = folderBrowserDialog.SelectedPath;
				}
			});

			// Run code from a thread that joins the STA Thread
			t.SetApartmentState(ApartmentState.STA);
			t.Start();
			t.Join();

			if (selectedPath != "")
			{
				SetStorageLocation(selectedPath);
				Console.WriteLine(selectedPath);
			}
		}

		private void SetStorageLocation(string path)
		{
			Program.saveFolder = path;
			Settings.Default.saveFolder = Program.saveFolder;
			Settings.Default.Save();
			storageLocationTextBox.Text = Program.saveFolder;
		}

		private void SpeedChangeEvent(object sender, EventArgs e)
		{
			int index = ((ComboBox)sender).SelectedIndex;

			Program.deltaTimeIndexFull = index;
			Settings.Default.targetDeltaTimeIndexFull = index;
			Settings.Default.Save();
		}

		private void UseCompressionEvent(object sender, EventArgs e)
		{
			Settings.Default.useCompression = ((CheckBox)sender).Checked;
			Settings.Default.Save();

			Program.useCompression = Settings.Default.useCompression;
		}

		private void BatchWritesEvent(object sender, EventArgs e)
		{
			Settings.Default.batchWrites = ((CheckBox)sender).Checked;
			Settings.Default.Save();

			Program.batchWrites = Settings.Default.batchWrites;
		}

		private void SplitFileEvent(object sender, EventArgs e)
		{
			Program.NewFilename();

			currentFilenameLabel.Text = Program.fileName;
		}

		private void ShowConsoleOnStartEvent(object sender, EventArgs e)
		{
			Settings.Default.showConsoleOnStart = ((CheckBox)sender).Checked;
			Settings.Default.Save();
		}

		private void LoggingTimeChanged(object sender, EventArgs e)
		{
			int index = ((ComboBox)sender).SelectedIndex;

			Settings.Default.whenToUploadLogs = index;
			Settings.Default.Save();
		}

		private void UploadToFirestoreChanged(object sender, EventArgs e)
		{
			Settings.Default.uploadToFirestore = ((CheckBox)sender).Checked;
			Settings.Default.Save();
		}
	}
}

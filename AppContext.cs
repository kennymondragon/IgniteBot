using System;
using System.Windows.Forms;

namespace IgniteBot
{
	class AppContext : ApplicationContext
	{

		public AppContext()
		{
			MenuItem showMainWindowMenu = new MenuItem("Show Main Output Window", new EventHandler(ShowMainWindowEvent));
			MenuItem settingsMenu = new MenuItem("Settings", new EventHandler(ShowSettingsEvent));

			// Initialize Tray Icon
			Program.trayIcon = new NotifyIcon()
			{
				Icon = Properties.Resources.vts_secondary_white_xFZ_icon,
				ContextMenu = new ContextMenu(new MenuItem[] {
					showMainWindowMenu,
					//showConsoleMenu,
					settingsMenu,
					new MenuItem("-"),
					new MenuItem("Exit", Exit)
				}),
				Visible = true
			};

			showMainWindowMenu.PerformClick();
			//showMainWindowMenu.Checked = Program.ShowingMainWindow;
		}

		#region Event Listeners

		LiveWindow mainWindow;
		private void ShowMainWindowEvent(object sender, EventArgs e)
		{
			Program.ShowingMainWindow = !Program.ShowingMainWindow;
			if (mainWindow == null || mainWindow.IsDisposed)
			{
				mainWindow = new LiveWindow();
				mainWindow.Show();
				Program.ShowingSettings = true;
			}
			else
			{
				mainWindow.Dispose();
				Program.ShowingMainWindow = false;
			}

			//((MenuItem)sender).Checked = Program.ShowingMainWindow;
		}

		private void ShowSettingsEvent(object sender, EventArgs e)
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

		void Exit(object sender, EventArgs e)
		{
			// We must manually tidy up and remove the icon before we exit.
			// Otherwise it will be left behind until the user mouses over.
			Program.trayIcon.Visible = false;
			Program.running = false;
			Application.Exit();
		}

		#endregion
	}
}
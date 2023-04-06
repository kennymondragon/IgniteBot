namespace IgniteBot
{
	partial class SettingsWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsWindow));
			this.fullLayout = new System.Windows.Forms.FlowLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.startWithWindowsCheckbox = new System.Windows.Forms.CheckBox();
			this.autorestartCheckbox = new System.Windows.Forms.CheckBox();
			this.enableStatsLogging = new System.Windows.Forms.CheckBox();
			this.statsLoggingBox = new System.Windows.Forms.GroupBox();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.uploadTimesComboBox = new System.Windows.Forms.ComboBox();
			this.uploadToFirestoreCheckBox = new System.Windows.Forms.CheckBox();
			this.enableFullLoggingCheckbox = new System.Windows.Forms.CheckBox();
			this.fullLoggingBox = new System.Windows.Forms.GroupBox();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.splitFileButton = new System.Windows.Forms.Button();
			this.currentFilenameLabel = new System.Windows.Forms.Label();
			this.batchWritesButton = new System.Windows.Forms.CheckBox();
			this.useCompressionButton = new System.Windows.Forms.CheckBox();
			this.speedSelector = new System.Windows.Forms.ComboBox();
			this.saveLocLabel = new System.Windows.Forms.Label();
			this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
			this.storageLocationTextBox = new System.Windows.Forms.TextBox();
			this.saveLocButton = new System.Windows.Forms.Button();
			this.closeButton = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.versionNum = new System.Windows.Forms.Label();
			this.fullLayout.SuspendLayout();
			this.statsLoggingBox.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.fullLoggingBox.SuspendLayout();
			this.flowLayoutPanel3.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// fullLayout
			// 
			this.fullLayout.Controls.Add(this.label1);
			this.fullLayout.Controls.Add(this.startWithWindowsCheckbox);
			this.fullLayout.Controls.Add(this.autorestartCheckbox);
			this.fullLayout.Controls.Add(this.enableStatsLogging);
			this.fullLayout.Controls.Add(this.statsLoggingBox);
			this.fullLayout.Controls.Add(this.enableFullLoggingCheckbox);
			this.fullLayout.Controls.Add(this.fullLoggingBox);
			this.fullLayout.Controls.Add(this.closeButton);
			this.fullLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fullLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.fullLayout.Location = new System.Drawing.Point(0, 0);
			this.fullLayout.Margin = new System.Windows.Forms.Padding(4);
			this.fullLayout.Name = "fullLayout";
			this.fullLayout.Padding = new System.Windows.Forms.Padding(15, 14, 15, 14);
			this.fullLayout.Size = new System.Drawing.Size(578, 594);
			this.fullLayout.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(19, 14);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(91, 26);
			this.label1.TabIndex = 1;
			this.label1.Text = "Settings";
			// 
			// startWithWindowsCheckbox
			// 
			this.startWithWindowsCheckbox.AutoSize = true;
			this.startWithWindowsCheckbox.Location = new System.Drawing.Point(19, 66);
			this.startWithWindowsCheckbox.Margin = new System.Windows.Forms.Padding(4);
			this.startWithWindowsCheckbox.Name = "startWithWindowsCheckbox";
			this.startWithWindowsCheckbox.Size = new System.Drawing.Size(154, 22);
			this.startWithWindowsCheckbox.TabIndex = 2;
			this.startWithWindowsCheckbox.Text = "Start with Windows";
			this.startWithWindowsCheckbox.UseVisualStyleBackColor = true;
			this.startWithWindowsCheckbox.CheckedChanged += new System.EventHandler(this.StartWithWindowsEvent);
			// 
			// autorestartCheckbox
			// 
			this.autorestartCheckbox.AutoSize = true;
			this.autorestartCheckbox.Location = new System.Drawing.Point(19, 96);
			this.autorestartCheckbox.Margin = new System.Windows.Forms.Padding(4);
			this.autorestartCheckbox.Name = "autorestartCheckbox";
			this.autorestartCheckbox.Size = new System.Drawing.Size(158, 22);
			this.autorestartCheckbox.TabIndex = 6;
			this.autorestartCheckbox.Text = "Autorestart EchoVR";
			this.autorestartCheckbox.UseVisualStyleBackColor = true;
			this.autorestartCheckbox.CheckedChanged += new System.EventHandler(this.RestartOnCrashEvent);
			// 
			// enableStatsLogging
			// 
			this.enableStatsLogging.AutoSize = true;
			this.enableStatsLogging.Location = new System.Drawing.Point(19, 134);
			this.enableStatsLogging.Margin = new System.Windows.Forms.Padding(4, 12, 4, 4);
			this.enableStatsLogging.Name = "enableStatsLogging";
			this.enableStatsLogging.Size = new System.Drawing.Size(166, 22);
			this.enableStatsLogging.TabIndex = 8;
			this.enableStatsLogging.Text = "Enable Stats Logging";
			this.enableStatsLogging.UseVisualStyleBackColor = true;
			this.enableStatsLogging.CheckedChanged += new System.EventHandler(this.EnableStatsLoggingEvent);
			// 
			// statsLoggingBox
			// 
			this.statsLoggingBox.Controls.Add(this.flowLayoutPanel2);
			this.statsLoggingBox.Location = new System.Drawing.Point(19, 164);
			this.statsLoggingBox.Margin = new System.Windows.Forms.Padding(4);
			this.statsLoggingBox.Name = "statsLoggingBox";
			this.statsLoggingBox.Padding = new System.Windows.Forms.Padding(4);
			this.statsLoggingBox.Size = new System.Drawing.Size(548, 111);
			this.statsLoggingBox.TabIndex = 9;
			this.statsLoggingBox.TabStop = false;
			this.statsLoggingBox.Text = "Stats Logging";
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.label2);
			this.flowLayoutPanel2.Controls.Add(this.uploadTimesComboBox);
			this.flowLayoutPanel2.Controls.Add(this.uploadToFirestoreCheckBox);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(4, 21);
			this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(4);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(540, 86);
			this.flowLayoutPanel2.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(148, 18);
			this.label2.TabIndex = 7;
			this.label2.Text = "When to upload data:";
			// 
			// uploadTimesComboBox
			// 
			this.uploadTimesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.uploadTimesComboBox.FormattingEnabled = true;
			this.uploadTimesComboBox.Items.AddRange(new object[] {
            "Only at end of matches",
            "After goals, player leave/join, and end of matches"});
			this.uploadTimesComboBox.Location = new System.Drawing.Point(3, 21);
			this.uploadTimesComboBox.Name = "uploadTimesComboBox";
			this.uploadTimesComboBox.Size = new System.Drawing.Size(419, 26);
			this.uploadTimesComboBox.TabIndex = 6;
			this.uploadTimesComboBox.SelectionChangeCommitted += new System.EventHandler(this.LoggingTimeChanged);
			// 
			// uploadToFirestoreCheckBox
			// 
			this.uploadToFirestoreCheckBox.AutoSize = true;
			this.uploadToFirestoreCheckBox.Location = new System.Drawing.Point(3, 53);
			this.uploadToFirestoreCheckBox.Name = "uploadToFirestoreCheckBox";
			this.uploadToFirestoreCheckBox.Size = new System.Drawing.Size(154, 22);
			this.uploadToFirestoreCheckBox.TabIndex = 8;
			this.uploadToFirestoreCheckBox.Text = "Upload to Firestore";
			this.uploadToFirestoreCheckBox.UseVisualStyleBackColor = true;
			this.uploadToFirestoreCheckBox.CheckedChanged += new System.EventHandler(this.UploadToFirestoreChanged);
			// 
			// enableFullLoggingCheckbox
			// 
			this.enableFullLoggingCheckbox.AutoSize = true;
			this.enableFullLoggingCheckbox.Location = new System.Drawing.Point(19, 291);
			this.enableFullLoggingCheckbox.Margin = new System.Windows.Forms.Padding(4, 12, 4, 4);
			this.enableFullLoggingCheckbox.MaximumSize = new System.Drawing.Size(1500, 1385);
			this.enableFullLoggingCheckbox.Name = "enableFullLoggingCheckbox";
			this.enableFullLoggingCheckbox.Size = new System.Drawing.Size(305, 22);
			this.enableFullLoggingCheckbox.TabIndex = 0;
			this.enableFullLoggingCheckbox.Text = "Enable Full Logging (for simulated replays)";
			this.enableFullLoggingCheckbox.UseVisualStyleBackColor = true;
			this.enableFullLoggingCheckbox.CheckedChanged += new System.EventHandler(this.EnableFullLoggingEvent);
			this.enableFullLoggingCheckbox.Click += new System.EventHandler(this.EnableFullLoggingEvent);
			// 
			// fullLoggingBox
			// 
			this.fullLoggingBox.Controls.Add(this.flowLayoutPanel3);
			this.fullLoggingBox.Location = new System.Drawing.Point(19, 321);
			this.fullLoggingBox.Margin = new System.Windows.Forms.Padding(4);
			this.fullLoggingBox.Name = "fullLoggingBox";
			this.fullLoggingBox.Padding = new System.Windows.Forms.Padding(4);
			this.fullLoggingBox.Size = new System.Drawing.Size(548, 239);
			this.fullLoggingBox.TabIndex = 10;
			this.fullLoggingBox.TabStop = false;
			this.fullLoggingBox.Text = "Full Data Logging";
			// 
			// flowLayoutPanel3
			// 
			this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel1);
			this.flowLayoutPanel3.Controls.Add(this.batchWritesButton);
			this.flowLayoutPanel3.Controls.Add(this.useCompressionButton);
			this.flowLayoutPanel3.Controls.Add(this.speedSelector);
			this.flowLayoutPanel3.Controls.Add(this.saveLocLabel);
			this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel4);
			this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel3.Location = new System.Drawing.Point(4, 21);
			this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(4);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size(540, 214);
			this.flowLayoutPanel3.TabIndex = 1;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.splitFileButton);
			this.flowLayoutPanel1.Controls.Add(this.currentFilenameLabel);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(537, 37);
			this.flowLayoutPanel1.TabIndex = 10;
			// 
			// splitFileButton
			// 
			this.splitFileButton.Location = new System.Drawing.Point(4, 4);
			this.splitFileButton.Margin = new System.Windows.Forms.Padding(4);
			this.splitFileButton.Name = "splitFileButton";
			this.splitFileButton.Size = new System.Drawing.Size(75, 32);
			this.splitFileButton.TabIndex = 3;
			this.splitFileButton.Text = "Split File";
			this.splitFileButton.UseVisualStyleBackColor = true;
			this.splitFileButton.Click += new System.EventHandler(this.SplitFileEvent);
			// 
			// currentFilenameLabel
			// 
			this.currentFilenameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.currentFilenameLabel.AutoSize = true;
			this.currentFilenameLabel.Location = new System.Drawing.Point(86, 11);
			this.currentFilenameLabel.Name = "currentFilenameLabel";
			this.currentFilenameLabel.Size = new System.Drawing.Size(18, 18);
			this.currentFilenameLabel.TabIndex = 4;
			this.currentFilenameLabel.Text = "--";
			// 
			// batchWritesButton
			// 
			this.batchWritesButton.AutoSize = true;
			this.batchWritesButton.Location = new System.Drawing.Point(4, 47);
			this.batchWritesButton.Margin = new System.Windows.Forms.Padding(4);
			this.batchWritesButton.Name = "batchWritesButton";
			this.batchWritesButton.Size = new System.Drawing.Size(112, 22);
			this.batchWritesButton.TabIndex = 1;
			this.batchWritesButton.Text = "Batch Writes";
			this.batchWritesButton.UseVisualStyleBackColor = true;
			this.batchWritesButton.CheckedChanged += new System.EventHandler(this.BatchWritesEvent);
			// 
			// useCompressionButton
			// 
			this.useCompressionButton.AutoSize = true;
			this.useCompressionButton.Location = new System.Drawing.Point(4, 77);
			this.useCompressionButton.Margin = new System.Windows.Forms.Padding(4);
			this.useCompressionButton.Name = "useCompressionButton";
			this.useCompressionButton.Size = new System.Drawing.Size(148, 22);
			this.useCompressionButton.TabIndex = 2;
			this.useCompressionButton.Text = "Use Compression";
			this.useCompressionButton.UseVisualStyleBackColor = true;
			this.useCompressionButton.CheckedChanged += new System.EventHandler(this.UseCompressionEvent);
			// 
			// speedSelector
			// 
			this.speedSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.speedSelector.FormattingEnabled = true;
			this.speedSelector.Items.AddRange(new object[] {
            "Fast (60 Hz)",
            "Med (10 Hz)",
            "Slow (1 Hz)"});
			this.speedSelector.Location = new System.Drawing.Point(4, 107);
			this.speedSelector.Margin = new System.Windows.Forms.Padding(4);
			this.speedSelector.Name = "speedSelector";
			this.speedSelector.Size = new System.Drawing.Size(180, 26);
			this.speedSelector.TabIndex = 5;
			this.speedSelector.SelectionChangeCommitted += new System.EventHandler(this.SpeedChangeEvent);
			// 
			// saveLocLabel
			// 
			this.saveLocLabel.AutoSize = true;
			this.saveLocLabel.Location = new System.Drawing.Point(4, 147);
			this.saveLocLabel.Margin = new System.Windows.Forms.Padding(4, 10, 4, 0);
			this.saveLocLabel.Name = "saveLocLabel";
			this.saveLocLabel.Size = new System.Drawing.Size(87, 18);
			this.saveLocLabel.TabIndex = 9;
			this.saveLocLabel.Text = "Save Folder";
			// 
			// flowLayoutPanel4
			// 
			this.flowLayoutPanel4.Controls.Add(this.storageLocationTextBox);
			this.flowLayoutPanel4.Controls.Add(this.saveLocButton);
			this.flowLayoutPanel4.Location = new System.Drawing.Point(4, 169);
			this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(4);
			this.flowLayoutPanel4.Name = "flowLayoutPanel4";
			this.flowLayoutPanel4.Size = new System.Drawing.Size(536, 39);
			this.flowLayoutPanel4.TabIndex = 8;
			// 
			// storageLocationTextBox
			// 
			this.storageLocationTextBox.Location = new System.Drawing.Point(4, 4);
			this.storageLocationTextBox.Margin = new System.Windows.Forms.Padding(4);
			this.storageLocationTextBox.Name = "storageLocationTextBox";
			this.storageLocationTextBox.Size = new System.Drawing.Size(414, 24);
			this.storageLocationTextBox.TabIndex = 7;
			this.storageLocationTextBox.WordWrap = false;
			// 
			// saveLocButton
			// 
			this.saveLocButton.Location = new System.Drawing.Point(426, 4);
			this.saveLocButton.Margin = new System.Windows.Forms.Padding(4);
			this.saveLocButton.Name = "saveLocButton";
			this.saveLocButton.Size = new System.Drawing.Size(75, 26);
			this.saveLocButton.TabIndex = 6;
			this.saveLocButton.Text = "Browse";
			this.saveLocButton.UseVisualStyleBackColor = true;
			this.saveLocButton.Click += new System.EventHandler(this.SetStorageLocation);
			// 
			// closeButton
			// 
			this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.closeButton.Location = new System.Drawing.Point(575, 18);
			this.closeButton.Margin = new System.Windows.Forms.Padding(4);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(75, 32);
			this.closeButton.TabIndex = 8;
			this.closeButton.Text = "Close";
			this.closeButton.UseVisualStyleBackColor = true;
			this.closeButton.Click += new System.EventHandler(this.CloseButtonEvent);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::IgniteBot.Properties.Resources.vts_icon;
			this.pictureBox1.InitialImage = global::IgniteBot.Properties.Resources.vts_icon;
			this.pictureBox1.Location = new System.Drawing.Point(440, 10);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(128, 128);
			this.pictureBox1.TabIndex = 12;
			this.pictureBox1.TabStop = false;
			// 
			// versionNum
			// 
			this.versionNum.Location = new System.Drawing.Point(440, 140);
			this.versionNum.Name = "versionNum";
			this.versionNum.Size = new System.Drawing.Size(128, 18);
			this.versionNum.TabIndex = 12;
			this.versionNum.Text = "version ?";
			this.versionNum.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// SettingsWindow
			// 
			this.AcceptButton = this.closeButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(578, 594);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.versionNum);
			this.Controls.Add(this.fullLayout);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SettingsWindow";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "IgniteBot | Settings";
			this.Load += new System.EventHandler(this.SettingsWindow_Load);
			this.fullLayout.ResumeLayout(false);
			this.fullLayout.PerformLayout();
			this.statsLoggingBox.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.fullLoggingBox.ResumeLayout(false);
			this.flowLayoutPanel3.ResumeLayout(false);
			this.flowLayoutPanel3.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.flowLayoutPanel4.ResumeLayout(false);
			this.flowLayoutPanel4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel fullLayout;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox startWithWindowsCheckbox;
		private System.Windows.Forms.CheckBox autorestartCheckbox;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.GroupBox statsLoggingBox;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.GroupBox fullLoggingBox;
		private System.Windows.Forms.CheckBox enableStatsLogging;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
		private System.Windows.Forms.CheckBox enableFullLoggingCheckbox;
		private System.Windows.Forms.Button splitFileButton;
		private System.Windows.Forms.CheckBox batchWritesButton;
		private System.Windows.Forms.CheckBox useCompressionButton;
		private System.Windows.Forms.ComboBox speedSelector;
		private System.Windows.Forms.TextBox storageLocationTextBox;
		private System.Windows.Forms.Button saveLocButton;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
		private System.Windows.Forms.Label saveLocLabel;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label currentFilenameLabel;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label versionNum;
		private System.Windows.Forms.ComboBox uploadTimesComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox uploadToFirestoreCheckBox;
	}
}
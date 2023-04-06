namespace IgniteBot
{
	partial class LiveWindow
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LiveWindow));
			this.leftFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.showHideLinesBox = new System.Windows.Forms.GroupBox();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.gameStateChangedCheckBox = new System.Windows.Forms.CheckBox();
			this.goalScoredCheckBox = new System.Windows.Forms.CheckBox();
			this.stunCheckBox = new System.Windows.Forms.CheckBox();
			this.discThrownCheckBox = new System.Windows.Forms.CheckBox();
			this.discCaughtCheckBox = new System.Windows.Forms.CheckBox();
			this.discStolenCheckBox = new System.Windows.Forms.CheckBox();
			this.savedCheckBox = new System.Windows.Forms.CheckBox();
			this.otherCheckBox = new System.Windows.Forms.CheckBox();
			this.statusBox = new System.Windows.Forms.GroupBox();
			this.statusFloxBox = new System.Windows.Forms.FlowLayoutPanel();
			this.connectedLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.sessionIdTextBox = new System.Windows.Forms.TextBox();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.label3 = new System.Windows.Forms.Label();
			this.discSpeedText = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.customIdTextbox = new System.Windows.Forms.TextBox();
			this.generateCustomId = new System.Windows.Forms.Button();
			this.closeButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.mainFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.rightFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.mainOutputTextBox = new System.Windows.Forms.RichTextBox();
			this.bottomButtonsFlow = new System.Windows.Forms.FlowLayoutPanel();
			this.quitButton = new System.Windows.Forms.Button();
			this.settingsButton = new System.Windows.Forms.Button();
			this.pauseButton = new System.Windows.Forms.Button();
			this.clearButton = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.versionLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.accessCodeLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.updateButton = new System.Windows.Forms.Button();
			this.updateProgressBar = new System.Windows.Forms.ProgressBar();
			this.leftFlowPanel.SuspendLayout();
			this.showHideLinesBox.SuspendLayout();
			this.flowLayoutPanel2.SuspendLayout();
			this.statusBox.SuspendLayout();
			this.statusFloxBox.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.mainFlowPanel.SuspendLayout();
			this.rightFlowPanel.SuspendLayout();
			this.bottomButtonsFlow.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// leftFlowPanel
			// 
			this.leftFlowPanel.AutoSize = true;
			this.leftFlowPanel.Controls.Add(this.updateButton);
			this.leftFlowPanel.Controls.Add(this.updateProgressBar);
			this.leftFlowPanel.Controls.Add(this.showHideLinesBox);
			this.leftFlowPanel.Controls.Add(this.statusBox);
			this.leftFlowPanel.Controls.Add(this.label5);
			this.leftFlowPanel.Controls.Add(this.customIdTextbox);
			this.leftFlowPanel.Controls.Add(this.generateCustomId);
			this.leftFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.leftFlowPanel.Location = new System.Drawing.Point(4, 4);
			this.leftFlowPanel.Margin = new System.Windows.Forms.Padding(4);
			this.leftFlowPanel.MaximumSize = new System.Drawing.Size(999, 1001);
			this.leftFlowPanel.MinimumSize = new System.Drawing.Size(220, 550);
			this.leftFlowPanel.Name = "leftFlowPanel";
			this.leftFlowPanel.Size = new System.Drawing.Size(220, 550);
			this.leftFlowPanel.TabIndex = 1;
			this.leftFlowPanel.WrapContents = false;
			// 
			// showHideLinesBox
			// 
			this.showHideLinesBox.Controls.Add(this.flowLayoutPanel2);
			this.showHideLinesBox.Location = new System.Drawing.Point(3, 78);
			this.showHideLinesBox.Name = "showHideLinesBox";
			this.showHideLinesBox.Padding = new System.Windows.Forms.Padding(6);
			this.showHideLinesBox.Size = new System.Drawing.Size(200, 48);
			this.showHideLinesBox.TabIndex = 12;
			this.showHideLinesBox.TabStop = false;
			this.showHideLinesBox.Text = "Show/Hide Event Types";
			this.showHideLinesBox.Visible = false;
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.gameStateChangedCheckBox);
			this.flowLayoutPanel2.Controls.Add(this.goalScoredCheckBox);
			this.flowLayoutPanel2.Controls.Add(this.stunCheckBox);
			this.flowLayoutPanel2.Controls.Add(this.discThrownCheckBox);
			this.flowLayoutPanel2.Controls.Add(this.discCaughtCheckBox);
			this.flowLayoutPanel2.Controls.Add(this.discStolenCheckBox);
			this.flowLayoutPanel2.Controls.Add(this.savedCheckBox);
			this.flowLayoutPanel2.Controls.Add(this.otherCheckBox);
			this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel2.Location = new System.Drawing.Point(6, 23);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(188, 19);
			this.flowLayoutPanel2.TabIndex = 0;
			// 
			// gameStateChangedCheckBox
			// 
			this.gameStateChangedCheckBox.AutoSize = true;
			this.gameStateChangedCheckBox.ForeColor = System.Drawing.Color.RoyalBlue;
			this.gameStateChangedCheckBox.Location = new System.Drawing.Point(3, 3);
			this.gameStateChangedCheckBox.Name = "gameStateChangedCheckBox";
			this.gameStateChangedCheckBox.Size = new System.Drawing.Size(169, 22);
			this.gameStateChangedCheckBox.TabIndex = 2;
			this.gameStateChangedCheckBox.Text = "Game State Changed";
			this.gameStateChangedCheckBox.UseVisualStyleBackColor = true;
			this.gameStateChangedCheckBox.CheckedChanged += new System.EventHandler(this.gameStateChangedCheckBox_CheckedChanged);
			// 
			// goalScoredCheckBox
			// 
			this.goalScoredCheckBox.AutoSize = true;
			this.goalScoredCheckBox.Location = new System.Drawing.Point(178, 3);
			this.goalScoredCheckBox.Name = "goalScoredCheckBox";
			this.goalScoredCheckBox.Size = new System.Drawing.Size(111, 22);
			this.goalScoredCheckBox.TabIndex = 0;
			this.goalScoredCheckBox.Text = "Goal Scored";
			this.goalScoredCheckBox.UseVisualStyleBackColor = true;
			this.goalScoredCheckBox.CheckedChanged += new System.EventHandler(this.goalScoredCheckBox_CheckedChanged);
			// 
			// stunCheckBox
			// 
			this.stunCheckBox.AutoSize = true;
			this.stunCheckBox.Location = new System.Drawing.Point(295, 3);
			this.stunCheckBox.Name = "stunCheckBox";
			this.stunCheckBox.Size = new System.Drawing.Size(57, 22);
			this.stunCheckBox.TabIndex = 1;
			this.stunCheckBox.Text = "Stun";
			this.stunCheckBox.UseVisualStyleBackColor = true;
			this.stunCheckBox.CheckedChanged += new System.EventHandler(this.stunCheckBox_CheckedChanged);
			// 
			// discThrownCheckBox
			// 
			this.discThrownCheckBox.AutoSize = true;
			this.discThrownCheckBox.Location = new System.Drawing.Point(358, 3);
			this.discThrownCheckBox.Name = "discThrownCheckBox";
			this.discThrownCheckBox.Size = new System.Drawing.Size(111, 22);
			this.discThrownCheckBox.TabIndex = 3;
			this.discThrownCheckBox.Text = "Disc Thrown";
			this.discThrownCheckBox.UseVisualStyleBackColor = true;
			this.discThrownCheckBox.CheckedChanged += new System.EventHandler(this.discThrownCheckBox_CheckedChanged);
			// 
			// discCaughtCheckBox
			// 
			this.discCaughtCheckBox.AutoSize = true;
			this.discCaughtCheckBox.Location = new System.Drawing.Point(475, 3);
			this.discCaughtCheckBox.Name = "discCaughtCheckBox";
			this.discCaughtCheckBox.Size = new System.Drawing.Size(108, 22);
			this.discCaughtCheckBox.TabIndex = 4;
			this.discCaughtCheckBox.Text = "Disc Caught";
			this.discCaughtCheckBox.UseVisualStyleBackColor = true;
			this.discCaughtCheckBox.CheckedChanged += new System.EventHandler(this.discCaughtCheckBox_CheckedChanged);
			// 
			// discStolenCheckBox
			// 
			this.discStolenCheckBox.AutoSize = true;
			this.discStolenCheckBox.Location = new System.Drawing.Point(589, 3);
			this.discStolenCheckBox.Name = "discStolenCheckBox";
			this.discStolenCheckBox.Size = new System.Drawing.Size(103, 22);
			this.discStolenCheckBox.TabIndex = 5;
			this.discStolenCheckBox.Text = "Disc Stolen";
			this.discStolenCheckBox.UseVisualStyleBackColor = true;
			this.discStolenCheckBox.CheckedChanged += new System.EventHandler(this.discStolenCheckBox_CheckedChanged);
			// 
			// savedCheckBox
			// 
			this.savedCheckBox.AutoSize = true;
			this.savedCheckBox.Location = new System.Drawing.Point(698, 3);
			this.savedCheckBox.Name = "savedCheckBox";
			this.savedCheckBox.Size = new System.Drawing.Size(60, 22);
			this.savedCheckBox.TabIndex = 6;
			this.savedCheckBox.Text = "Save";
			this.savedCheckBox.UseVisualStyleBackColor = true;
			this.savedCheckBox.CheckedChanged += new System.EventHandler(this.savedCheckBox_CheckedChanged);
			// 
			// otherCheckBox
			// 
			this.otherCheckBox.AutoSize = true;
			this.otherCheckBox.Location = new System.Drawing.Point(764, 3);
			this.otherCheckBox.Name = "otherCheckBox";
			this.otherCheckBox.Size = new System.Drawing.Size(64, 22);
			this.otherCheckBox.TabIndex = 11;
			this.otherCheckBox.Text = "Other";
			this.otherCheckBox.UseVisualStyleBackColor = true;
			// 
			// statusBox
			// 
			this.statusBox.Controls.Add(this.statusFloxBox);
			this.statusBox.Location = new System.Drawing.Point(0, 135);
			this.statusBox.Margin = new System.Windows.Forms.Padding(0, 6, 0, 0);
			this.statusBox.Name = "statusBox";
			this.statusBox.Size = new System.Drawing.Size(219, 265);
			this.statusBox.TabIndex = 9;
			this.statusBox.TabStop = false;
			this.statusBox.Text = "Status";
			// 
			// statusFloxBox
			// 
			this.statusFloxBox.Controls.Add(this.connectedLabel);
			this.statusFloxBox.Controls.Add(this.label1);
			this.statusFloxBox.Controls.Add(this.sessionIdTextBox);
			this.statusFloxBox.Controls.Add(this.flowLayoutPanel1);
			this.statusFloxBox.Controls.Add(this.label4);
			this.statusFloxBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.statusFloxBox.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.statusFloxBox.Location = new System.Drawing.Point(3, 20);
			this.statusFloxBox.Margin = new System.Windows.Forms.Padding(8);
			this.statusFloxBox.Name = "statusFloxBox";
			this.statusFloxBox.Padding = new System.Windows.Forms.Padding(8);
			this.statusFloxBox.Size = new System.Drawing.Size(213, 242);
			this.statusFloxBox.TabIndex = 1;
			// 
			// connectedLabel
			// 
			this.connectedLabel.AutoSize = true;
			this.connectedLabel.Location = new System.Drawing.Point(11, 11);
			this.connectedLabel.Margin = new System.Windows.Forms.Padding(3);
			this.connectedLabel.Name = "connectedLabel";
			this.connectedLabel.Size = new System.Drawing.Size(88, 18);
			this.connectedLabel.TabIndex = 0;
			this.connectedLabel.Text = "Connected?";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 40);
			this.label1.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 18);
			this.label1.TabIndex = 1;
			this.label1.Text = "Session ID:";
			// 
			// sessionIdTextBox
			// 
			this.sessionIdTextBox.Font = new System.Drawing.Font("Cascadia Code", 8F);
			this.sessionIdTextBox.Location = new System.Drawing.Point(8, 64);
			this.sessionIdTextBox.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.sessionIdTextBox.Name = "sessionIdTextBox";
			this.sessionIdTextBox.ReadOnly = true;
			this.sessionIdTextBox.Size = new System.Drawing.Size(194, 20);
			this.sessionIdTextBox.TabIndex = 3;
			this.sessionIdTextBox.Text = "06AC7E56-9477-4543-9407-0FD48DF2F1A2";
			this.sessionIdTextBox.Click += new System.EventHandler(this.SessionIDFocused);
			this.sessionIdTextBox.DoubleClick += new System.EventHandler(this.SessionIDFocused);
			this.sessionIdTextBox.Enter += new System.EventHandler(this.SessionIDFocused);
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.Controls.Add(this.label3);
			this.flowLayoutPanel1.Controls.Add(this.discSpeedText);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(8, 87);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 24);
			this.flowLayoutPanel1.TabIndex = 10;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 3);
			this.label3.Margin = new System.Windows.Forms.Padding(3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 18);
			this.label3.TabIndex = 5;
			this.label3.Text = "Disk Speed:";
			// 
			// discSpeedText
			// 
			this.discSpeedText.BackColor = System.Drawing.Color.Transparent;
			this.discSpeedText.Location = new System.Drawing.Point(97, 3);
			this.discSpeedText.Margin = new System.Windows.Forms.Padding(3);
			this.discSpeedText.Name = "discSpeedText";
			this.discSpeedText.Size = new System.Drawing.Size(98, 18);
			this.discSpeedText.TabIndex = 8;
			this.discSpeedText.Text = "-- m/s";
			this.discSpeedText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(11, 119);
			this.label4.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(107, 18);
			this.label4.TabIndex = 9;
			this.label4.Text = "Player Speeds:";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 400);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(64, 18);
			this.label5.TabIndex = 14;
			this.label5.Text = "Stats ID:";
			// 
			// customIdTextbox
			// 
			this.customIdTextbox.Location = new System.Drawing.Point(3, 421);
			this.customIdTextbox.Name = "customIdTextbox";
			this.customIdTextbox.ReadOnly = true;
			this.customIdTextbox.Size = new System.Drawing.Size(213, 24);
			this.customIdTextbox.TabIndex = 13;
			this.customIdTextbox.TextChanged += new System.EventHandler(this.customIdChanged);
			// 
			// generateCustomId
			// 
			this.generateCustomId.Location = new System.Drawing.Point(3, 451);
			this.generateCustomId.Name = "generateCustomId";
			this.generateCustomId.Size = new System.Drawing.Size(99, 30);
			this.generateCustomId.TabIndex = 15;
			this.generateCustomId.Text = "Split Stats";
			this.generateCustomId.UseVisualStyleBackColor = true;
			this.generateCustomId.Click += new System.EventHandler(this.splitStatsButtonClick);
			// 
			// closeButton
			// 
			this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.closeButton.Location = new System.Drawing.Point(446, 3);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(90, 36);
			this.closeButton.TabIndex = 11;
			this.closeButton.Text = "Hide";
			this.closeButton.UseVisualStyleBackColor = true;
			this.closeButton.Click += new System.EventHandler(this.CloseButtonClicked);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
			this.label2.Location = new System.Drawing.Point(4, 0);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(97, 20);
			this.label2.TabIndex = 9;
			this.label2.Text = "Data Output";
			// 
			// mainFlowPanel
			// 
			this.mainFlowPanel.Controls.Add(this.leftFlowPanel);
			this.mainFlowPanel.Controls.Add(this.rightFlowPanel);
			this.mainFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainFlowPanel.Location = new System.Drawing.Point(0, 0);
			this.mainFlowPanel.Margin = new System.Windows.Forms.Padding(4);
			this.mainFlowPanel.Name = "mainFlowPanel";
			this.mainFlowPanel.Size = new System.Drawing.Size(780, 557);
			this.mainFlowPanel.TabIndex = 12;
			this.mainFlowPanel.WrapContents = false;
			// 
			// rightFlowPanel
			// 
			this.rightFlowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rightFlowPanel.Controls.Add(this.label2);
			this.rightFlowPanel.Controls.Add(this.mainOutputTextBox);
			this.rightFlowPanel.Controls.Add(this.bottomButtonsFlow);
			this.rightFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.rightFlowPanel.Location = new System.Drawing.Point(232, 4);
			this.rightFlowPanel.Margin = new System.Windows.Forms.Padding(4);
			this.rightFlowPanel.MaximumSize = new System.Drawing.Size(999, 1001);
			this.rightFlowPanel.MinimumSize = new System.Drawing.Size(302, 550);
			this.rightFlowPanel.Name = "rightFlowPanel";
			this.rightFlowPanel.Size = new System.Drawing.Size(548, 550);
			this.rightFlowPanel.TabIndex = 12;
			// 
			// mainOutputTextBox
			// 
			this.mainOutputTextBox.HideSelection = false;
			this.mainOutputTextBox.Location = new System.Drawing.Point(3, 23);
			this.mainOutputTextBox.MinimumSize = new System.Drawing.Size(301, 101);
			this.mainOutputTextBox.Name = "mainOutputTextBox";
			this.mainOutputTextBox.ReadOnly = true;
			this.mainOutputTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
			this.mainOutputTextBox.Size = new System.Drawing.Size(533, 454);
			this.mainOutputTextBox.TabIndex = 13;
			this.mainOutputTextBox.Text = "";
			this.mainOutputTextBox.WordWrap = false;
			// 
			// bottomButtonsFlow
			// 
			this.bottomButtonsFlow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.bottomButtonsFlow.Controls.Add(this.closeButton);
			this.bottomButtonsFlow.Controls.Add(this.quitButton);
			this.bottomButtonsFlow.Controls.Add(this.settingsButton);
			this.bottomButtonsFlow.Controls.Add(this.pauseButton);
			this.bottomButtonsFlow.Controls.Add(this.clearButton);
			this.bottomButtonsFlow.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.bottomButtonsFlow.Location = new System.Drawing.Point(0, 480);
			this.bottomButtonsFlow.Margin = new System.Windows.Forms.Padding(0);
			this.bottomButtonsFlow.Name = "bottomButtonsFlow";
			this.bottomButtonsFlow.Size = new System.Drawing.Size(539, 51);
			this.bottomButtonsFlow.TabIndex = 12;
			// 
			// quitButton
			// 
			this.quitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.quitButton.Location = new System.Drawing.Point(350, 3);
			this.quitButton.Name = "quitButton";
			this.quitButton.Size = new System.Drawing.Size(90, 36);
			this.quitButton.TabIndex = 12;
			this.quitButton.Text = "Exit";
			this.quitButton.UseVisualStyleBackColor = true;
			this.quitButton.Click += new System.EventHandler(this.QuitButtonClicked);
			// 
			// settingsButton
			// 
			this.settingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.settingsButton.Location = new System.Drawing.Point(254, 3);
			this.settingsButton.Name = "settingsButton";
			this.settingsButton.Size = new System.Drawing.Size(90, 36);
			this.settingsButton.TabIndex = 13;
			this.settingsButton.Text = "Settings";
			this.settingsButton.UseVisualStyleBackColor = true;
			this.settingsButton.Click += new System.EventHandler(this.SettingsButtonClicked);
			// 
			// pauseButton
			// 
			this.pauseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.pauseButton.Location = new System.Drawing.Point(158, 3);
			this.pauseButton.Name = "pauseButton";
			this.pauseButton.Size = new System.Drawing.Size(90, 36);
			this.pauseButton.TabIndex = 14;
			this.pauseButton.Text = "Pause";
			this.pauseButton.UseVisualStyleBackColor = true;
			this.pauseButton.Visible = false;
			this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
			// 
			// clearButton
			// 
			this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.clearButton.Location = new System.Drawing.Point(62, 3);
			this.clearButton.Name = "clearButton";
			this.clearButton.Size = new System.Drawing.Size(90, 36);
			this.clearButton.TabIndex = 15;
			this.clearButton.Text = "Clear";
			this.clearButton.UseVisualStyleBackColor = true;
			this.clearButton.Visible = false;
			this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.versionLabel,
            this.accessCodeLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 535);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(780, 22);
			this.statusStrip1.TabIndex = 10;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// versionLabel
			// 
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new System.Drawing.Size(31, 17);
			this.versionLabel.Text = "v ---";
			// 
			// accessCodeLabel
			// 
			this.accessCodeLabel.Name = "accessCodeLabel";
			this.accessCodeLabel.Size = new System.Drawing.Size(83, 17);
			this.accessCodeLabel.Text = "Not Logged In";
			this.accessCodeLabel.Click += new System.EventHandler(this.accessCodeLabel_Click);
			// 
			// updateButton
			// 
			this.updateButton.Location = new System.Drawing.Point(3, 3);
			this.updateButton.Name = "updateButton";
			this.updateButton.Size = new System.Drawing.Size(213, 40);
			this.updateButton.TabIndex = 16;
			this.updateButton.Text = "Download Update";
			this.updateButton.UseVisualStyleBackColor = true;
			this.updateButton.Visible = false;
			this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
			// 
			// updateProgressBar
			// 
			this.updateProgressBar.Location = new System.Drawing.Point(3, 49);
			this.updateProgressBar.Name = "updateProgressBar";
			this.updateProgressBar.Size = new System.Drawing.Size(213, 23);
			this.updateProgressBar.TabIndex = 17;
			this.updateProgressBar.Visible = false;
			// 
			// LiveWindow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(780, 557);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.mainFlowPanel);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "LiveWindow";
			this.Text = "IgniteBot | Live Data Output";
			this.Load += new System.EventHandler(this.LiveWindow_Load);
			this.leftFlowPanel.ResumeLayout(false);
			this.leftFlowPanel.PerformLayout();
			this.showHideLinesBox.ResumeLayout(false);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.flowLayoutPanel2.PerformLayout();
			this.statusBox.ResumeLayout(false);
			this.statusFloxBox.ResumeLayout(false);
			this.statusFloxBox.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.mainFlowPanel.ResumeLayout(false);
			this.mainFlowPanel.PerformLayout();
			this.rightFlowPanel.ResumeLayout(false);
			this.rightFlowPanel.PerformLayout();
			this.bottomButtonsFlow.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.FlowLayoutPanel leftFlowPanel;
		private System.Windows.Forms.CheckBox goalScoredCheckBox;
		private System.Windows.Forms.CheckBox stunCheckBox;
		private System.Windows.Forms.CheckBox gameStateChangedCheckBox;
		private System.Windows.Forms.CheckBox discThrownCheckBox;
		private System.Windows.Forms.CheckBox discCaughtCheckBox;
		private System.Windows.Forms.CheckBox discStolenCheckBox;
		private System.Windows.Forms.CheckBox savedCheckBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.FlowLayoutPanel mainFlowPanel;
		private System.Windows.Forms.FlowLayoutPanel rightFlowPanel;
		private System.Windows.Forms.FlowLayoutPanel bottomButtonsFlow;
		private System.Windows.Forms.Button quitButton;
		private System.Windows.Forms.Button settingsButton;
		private System.Windows.Forms.Button pauseButton;
		private System.Windows.Forms.RichTextBox mainOutputTextBox;
		private System.Windows.Forms.GroupBox statusBox;
		private System.Windows.Forms.Label connectedLabel;
		private System.Windows.Forms.FlowLayoutPanel statusFloxBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox sessionIdTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel accessCodeLabel;
		private System.Windows.Forms.CheckBox otherCheckBox;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label discSpeedText;
		private System.Windows.Forms.GroupBox showHideLinesBox;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.Button clearButton;
		private System.Windows.Forms.ToolStripStatusLabel versionLabel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox customIdTextbox;
		private System.Windows.Forms.Button generateCustomId;
		private System.Windows.Forms.Button updateButton;
		private System.Windows.Forms.ProgressBar updateProgressBar;
	}
}
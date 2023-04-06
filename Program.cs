using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using static Logger;
using System.Net.Http;
using IgniteBot.Properties;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Concurrent;
using System.Globalization;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Newtonsoft.Json.Linq;

namespace IgniteBot
{
	/// <summary>
	/// Main
	/// </summary>
	class Program
	{

		/// <summary>
		/// Set this to false to finish up and close
		/// </summary>
		public static bool running = true;
		public static bool inGame = false;

		/// <summary>
		/// Whether to continue reading input right now
		/// </summary>
		public static bool paused = false;

		// READ FROM FILE
		private const bool READ_FROM_FILE = false;

		/// <summary>
		/// whether to read slower when reading file 
		/// </summary>
		private const bool realtimeWhenReadingFile = false;

		// Should only use queue when reading from file
		private const bool readIntoQueue = true;
		private static bool fileFinishedReading = false;

		//private static string readFromFolder = "S:\\git_repo\\EchoVR-Session-Grabber\\bin\\Debug\\full_session_data\\example\\";
		private static string readFromFolder = "F:\\Documents\\EchoDataStorage\\TitanV Machine";
		private static List<string> filesInFolder;
		private static int readFromFolderIndex = 0;

		public const bool uploadOnlyAtEndOfMatch = true;

		public static bool writeToOBSHTMLFile = false;

		public const string APIURL = "http://ntsfranz.blank.com/";
		//public const string APIURL = "http://127.0.0.1:5000/";
		//public const string APIURL = "https://ignitevr.gg/cgi-bin/EchoStats.cgi/";

		public static bool enableStatsLogging = true;
		public static bool enableFullLogging = true;

		public static bool authorized = false;
		public static readonly HttpClient client = new HttpClient();
		public const string APIKey = "xxxxxxxxxxxxxxxx";

		/// <summary>
		/// Dictionary of currently vaild access Hashes.
		/// This should probabbly be changed as to allow us to add and removed users easily.
		/// </summary>
		public static Dictionary<string, string> validAccessCodeHashes = new Dictionary<string, string> {
			{ "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "x1" },
			{ "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "x2" },
			{ "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "x3" },
			{ "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "x4" },
			{ "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "x5" },
			{ "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "x6" }	
		};
		public static string currentAccessCodeUsername = "";

		public static bool autoRestart = false;
		public static bool showDatabaseLog = false;

		// Debug processing flags
		public static bool onlyGoals = false;

		// declarations.
		static MatchData matchData;
		static MatchData lastMatchData;

		/// <summary>
		/// Contains the last state so that we can do a diff to determine state changes
		/// This acts like a set of flags.
		/// </summary>
		public static g_Instance lastFrame;
		public static g_Instance lastLastFrame;
		public static g_Instance lastLastLastFrame;

		private static int lastFrameSumOfStats = 0;
		private static g_Instance lastValidStatsFrame;
		private static int lastValidSumOfStatsAge = 0;

		static List<string> dataCache = new List<string>();

		class UserAtTime
		{
			public float gameClock;
			public g_Player player;
		}

		// { [stunner, stunnee], [stunner, stunnee] }
		static List<UserAtTime[]> stunningMatchedPairs = new List<UserAtTime[]>();
		static float stunMatchingTimeout = 4f;

		public static string lastDateTimeString;
		public static string lastJSON;
		public static ConcurrentQueue<string> lastJSONQueue = new ConcurrentQueue<string>();
		public static ConcurrentQueue<string> lastDateTimeStringQueue = new ConcurrentQueue<string>();
		private static bool lastJSONUsed;
		private static readonly object lastJSONLock = new object();

		private static readonly object fileWritingLock = new object();

		public static readonly object logOutputWriteLock = new object();

		static DateTime lastDataTime;
		static float minTillAutorestart = 3;

		static bool wasThrown;
		static bool inPostMatch = false;

		public static int deltaTimeIndexStats = 0;
		public static int deltaTimeIndexFull = 1;

		public static List<int> statsDeltaTimes = new List<int> { 16, 100 };
		public static List<int> fullDeltaTimes = new List<int> { 16, 100, 1000 };

		/// <summary>
		/// The folder to save all the full data logs to
		/// </summary>
		public static string saveFolder;
		public static string fileName;
		public static bool useCompression;
		public static bool batchWrites;

		static string echoPath = "";

		public static NotifyIcon trayIcon;

		public static SettingsWindow settingsWindow;
		public static bool ShowingMainWindow { get; set; }

		public static bool ShowingSettings { get; set; }

		private static float smoothDeltaTime = -1;

		public static string customId;

		private static FirestoreDb db;


		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if (!CheckAccessCode(Settings.Default.accessCode))
			{
				Application.Run(new LoginWindow());
			}
			else
			{
				authorized = true;
			}

			if (!authorized) return;

			var argsList = new List<string>(args);

			// Check for command-line flags
			if (args.Contains("-slowmode"))
			{
				deltaTimeIndexStats = 1;
				Settings.Default.targetDeltaTimeIndexStats = deltaTimeIndexStats;
			}
			if (args.Contains("-autorestart"))
			{
				autoRestart = true;
				Settings.Default.autoRestart = true;
			}
			if (args.Contains("-showdatabaselog"))
			{
				showDatabaseLog = true;
				Settings.Default.showDatabaseLog = true;
			}
			if (args.Contains("-noserverlogs"))
			{
				enableLoggingRemote = false;
				Settings.Default.logToServer = false;
			}

			Settings.Default.Save();

			ReadSettings();

			client.DefaultRequestHeaders.Add("x-api-key", APIKey);
			client.DefaultRequestHeaders.Add("version", Application.ProductVersion);

			client.BaseAddress = new Uri(APIURL);

			var builder = new FirestoreClientBuilder { JsonCredentials = @"
					{
					  ""type"": ""service_account"",
					  ""project_id"": ""ignitevr-echostats"",
					  ""private_key_id"": ""xxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"",
					  ""private_key"": ""-----BEGIN PRIVATE KEY-----\n\n-----END PRIVATE KEY-----\n"",
					  ""client_email"": ""firebase-adminsdk-nzhes@ignitevr-echostats.iam.gserviceaccount.com"",
					  ""client_id"": ""105896303393978166652"",
					  ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
					  ""token_uri"": ""https://oauth2.googleapis.com/token"",
					  ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
					  ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/firebase-adminsdk-nzhes%40ignitevr-echostats.iam.gserviceaccount.com""
					}
				" };
			db = FirestoreDb.Create("ignitevr-echostats", builder.Build());

			////create server with auto assigned port
			//HTTPServer myServer = new HTTPServer("localhost", 8001);
			//Console.WriteLine("Server is running at: localhost:8001");
			//myServer.Start();

			Thread statsThread = new Thread(new ThreadStart(StatsThread));
			statsThread.Start();

			Thread fullLogThread = new Thread(new ThreadStart(FullLogThread));
			fullLogThread.Start();

			Thread autorestartThread = new Thread(new ThreadStart(AutorestartThread));
			autorestartThread.Start();

			Thread fetchThread = new Thread(new ThreadStart(FetchThread));
			fetchThread.Start();

			Application.EnableVisualStyles();
			Application.Run(new AppContext());

			_ = KillAll(statsThread, fullLogThread, autorestartThread, fetchThread);
		}

		/// <summary>
		/// This is just a failsafe so that the program doesn't leave a dangling thread.
		/// </summary>
		async static Task KillAll(Thread statsThread, Thread fullLogThread, Thread autorestartThread, Thread fetchThread)
		{
			// give them some time to kill themselves, then kill them ourself if they couldn't manage it
			await Task.Delay(10000);
			if (statsThread.IsAlive || fullLogThread.IsAlive || autorestartThread.IsAlive || fetchThread.IsAlive)
			{
				statsThread.Abort();
				fullLogThread.Abort();
				autorestartThread.Abort();
				fetchThread.Abort();
			}
		}

		/// <summary>
		/// Thread that actually does the GET requests or reading from file. 
		/// Once a line has been used, this thread gets a new one.
		/// </summary>
		public static void FetchThread()
		{
			StreamReader fileReader = null;

			if (READ_FROM_FILE)
			{
				filesInFolder = Directory.GetFiles(readFromFolder, "*.zip").ToList();
				filesInFolder.Sort();
				fileReader = ExtractFile(fileReader, filesInFolder[readFromFolderIndex++]);
			}


			while (running && !fileFinishedReading)
			{
				while (paused && running)
				{
					Thread.Sleep(10);
				}

				if (READ_FROM_FILE)
				{
					if (fileReader != null)
					{
						string rawJSON = fileReader.ReadLine();
						if (rawJSON == null)
						{
							fileReader.Close();
							if (readFromFolderIndex >= filesInFolder.Count)
							{
								fileFinishedReading = true;
							}
							else
							{
								fileReader = ExtractFile(fileReader, filesInFolder[readFromFolderIndex++]);
							}
						}
						else
						{
							string[] splitJSON = rawJSON.Split('\t');
							string onlyJSON, onlyTime;
							if (splitJSON.Length > 1)
							{
								onlyJSON = splitJSON[1];
								onlyTime = splitJSON[0];
							}
							else
							{
								onlyJSON = splitJSON[0];
								string fileName = filesInFolder[readFromFolderIndex - 1].Split('\\').Last();
								onlyTime = fileName.Substring(4, fileName.Length - 8);
							}

							inGame = true;

							if (readIntoQueue)
							{
								lastJSONQueue.Enqueue(onlyJSON);
								lastDateTimeStringQueue.Enqueue(onlyTime);
							}
							else
							{
								lock (lastJSONLock)
								{
									lastDateTimeString = onlyTime;
									lastJSON = onlyJSON;
									lastJSONUsed = false;
								}
							}
						}
					}
					else
					{
						LogRow(LogType.Error, "File doesn't exist or something");
						return;
					}

					if (readIntoQueue)
					{
						if (lastJSONQueue.Count > 100000)
						{
							Console.WriteLine("Got 100k lines ahead");
							// sleep for 1 sec to let the other thread catch up
							Thread.Sleep(1000);
						}
					}
					else
					{
						// wait until we need to get another row
						while (!lastJSONUsed)
						{
							Thread.Sleep(1);
						}
					}
				}
				else
				{
					WebResponse response;
					StreamReader sReader;

					// Create Session.
					WebRequest request = WebRequest.Create("http://127.0.0.1:6721/session");

					// Do we get a response?
					try
					{
						response = request.GetResponse();
					}
					catch (Exception)
					{
						// Don't update so quick if we aren't in a match anyway
						Thread.Sleep(2000);
						try
						{
							Console.Clear();
						}
						catch
						{

						}

						// split file between matches
						NewFilename();
						LogRow(LogType.Info, "Not in Match");
						inGame = false;

						lock (lastJSONLock)
						{
							lastJSON = null;
						}

						continue;
					}

					lastDataTime = DateTime.Now;
					inGame = true;

					Stream dataStream = response.GetResponseStream();
					sReader = new StreamReader(dataStream);

					// Session Contents
					string rawJSON = sReader.ReadToEnd();

					// pls close (;-;)
					if (sReader != null)
						sReader.Close();
					if (response != null)
						response.Close();

					lock (lastJSONLock)
					{
						lastJSON = rawJSON;
						lastJSONUsed = false;
					}
				}
			}
		}

		/// <summary>
		/// Thread for logging only stats
		/// </summary>
		public static void StatsThread()
		{
			// TODO these times aren't used, but we could do a difference on before and after times to 
			// calculate an accurate deltaTime. Right now the execution time isn't taken into account.
			var time = DateTime.Now;
			var deltaTimeSpan = new TimeSpan(0, 0, 0, 0, statsDeltaTimes[deltaTimeIndexStats]);

			if (enableStatsLogging)
			{
				_ = InitializeDatabase();
			}

			// Session pull loop.
			while (running)
			{
				if (enableStatsLogging && inGame)
				{
					try
					{
						string json, recordedTime;
						if (READ_FROM_FILE && readIntoQueue)
						{
							if (!lastJSONQueue.TryDequeue(out json))
							{
								if (fileFinishedReading)
								{
									running = false;
								}
								Thread.Sleep(1);
								continue;
							}
							lastDateTimeStringQueue.TryDequeue(out recordedTime);
						}
						else
						{
							lock (lastJSONLock)
							{
								if (lastJSON == null) continue;

								lastJSONUsed = true;
								json = lastJSON;
								recordedTime = lastDateTimeString;
							}
						}


						// Convert session contents into game_instance class.
						g_Instance game_Instance = JsonConvert.DeserializeObject<g_Instance>(json);

						// add the recorded time
						if (recordedTime != string.Empty)
						{
							if (!DateTime.TryParse(recordedTime, out DateTime dateTime))
							{
								DateTime.TryParseExact(recordedTime, "yyyy-MM-dd_HH-mm-ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
							}
							if (dateTime != null)
							{
								game_Instance.recorded_time = dateTime;
							}
						}

						game_Instance.teams[0].color = equipo.Color.blue;
						game_Instance.teams[1].color = equipo.Color.orange;
						if (game_Instance.teams[0].players == null) game_Instance.teams[0].players = new List<g_Player>();
						if (game_Instance.teams[1].players == null) game_Instance.teams[1].players = new List<g_Player>();


						if (lastFrame == null) lastFrame = game_Instance;

						if (matchData == null)
						{
							matchData = new MatchData(game_Instance);
							UpdateStatsIngame(game_Instance);
						}

						ProcessFrame(game_Instance);
						UpdateLogger();
					}
					catch (Exception ex)
					{
						LogRow(LogType.Error, "Big oopsie. Please catch inside. " + ex);
					}

					if (READ_FROM_FILE && !realtimeWhenReadingFile)
					{
						while (running)
						{
							if (!lastJSONUsed)
							{
								break;
							}
							//Thread.Sleep(1);
						}
					}
					else
					{
						Thread.Sleep(statsDeltaTimes[deltaTimeIndexStats]);
					}
				}
				else
				{
					Thread.Sleep(1000);
				}
			}
		}

		/// <summary>
		/// Thread for logging all JSON data
		/// </summary>
		public static void FullLogThread()
		{
			lastDataTime = DateTime.Now;

			NewFilename();

			// Session pull loop.
			while (running)
			{
				if (enableFullLogging && inGame)
				{
					try
					{
						string json;
						lock (lastJSONLock)
						{
							if (lastJSON == null) continue;

							lastJSONUsed = true;
							json = lastJSON;
						}

						WriteToFile(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "\t" + json);
					}
					catch (Exception ex)
					{
						Console.WriteLine("Big oopsie. Please catch inside. " + ex);
					}

					Thread.Sleep(fullDeltaTimes[deltaTimeIndexFull]);
				}
				else
				{
					Thread.Sleep(1000);
				}
			}

			// causes a final zip if that's needed
			NewFilename();
		}

		/// <summary>
		/// Thread to detect crashes and restart EchoVR
		/// </summary>
		public static void AutorestartThread()
		{
			lastDataTime = DateTime.Now;

			if (READ_FROM_FILE) autoRestart = false;

			// Session pull loop.
			while (running)
			{
				// only start worrying once 15 seconds have passed
				if (autoRestart && DateTime.Compare(lastDataTime.AddMinutes(.25f), DateTime.Now) < 0)
				{
					LogRow(LogType.Info, "Time left until restart: " +
						(lastDataTime.AddMinutes(minTillAutorestart) - DateTime.Now).Minutes + " min " +
						(lastDataTime.AddMinutes(minTillAutorestart) - DateTime.Now).Seconds + " sec");

					// If `minTillAutorestart` minutes have passed, restart EchoVR
					if (DateTime.Compare(lastDataTime.AddMinutes(minTillAutorestart), DateTime.Now) < 0)
					{
						// Get process name
						var process = Process.GetProcessesByName("echovr");

						if (process.Length > 0)
						{
							var echo_ = process[0];
							// Get process path
							echoPath = echo_.MainModule.FileName;
							Settings.Default.echoVRPath = echoPath;
							Settings.Default.Save();
							// close client
							echo_.Kill();
							// restart client
							Process.Start(echoPath, "-spectatorstream");
						}
						else if (echoPath != null && echoPath != "")
						{
							// restart client
							Process.Start(echoPath, "-spectatorstream");
						}
						else
						{
							LogRow(LogType.Error, "Couldn't restart EchoVR because it isn't running");
						}

						// reset timer
						lastDataTime = DateTime.Now;
					}
				}

				Thread.Sleep(1000);
			}
		}

		private static StreamReader ExtractFile(StreamReader fileReader, string fileName)
		{
			string tempDir = Path.Combine(saveFolder, "temp_zip_read\\").ToString();

			if (Directory.Exists(tempDir))
			{
				while (running)
				{
					try
					{
						Directory.Delete(tempDir, true);
						break;
					}
					catch (IOException)
					{
						Thread.Sleep(10);
					}
				}
			}

			Directory.CreateDirectory(tempDir);

			using (ZipArchive archive = ZipFile.OpenRead(fileName))
			{
				foreach (ZipArchiveEntry entry in archive.Entries)
				{
					// Gets the full path to ensure that relative segments are removed.
					string destinationPath = Path.GetFullPath(Path.Combine(tempDir, entry.FullName));

					entry.ExtractToFile(destinationPath);

					fileReader = new StreamReader(destinationPath);
				}
			}

			return fileReader;
		}

		private static void ReadSettings()
		{
			showDatabaseLog = Settings.Default.showDatabaseLog;
			enableLoggingRemote = Settings.Default.logToServer;
			autoRestart = Settings.Default.autoRestart;
			deltaTimeIndexStats = Settings.Default.targetDeltaTimeIndexStats;
			useCompression = Settings.Default.useCompression;
			batchWrites = Settings.Default.batchWrites;
			saveFolder = Settings.Default.saveFolder;
			enableFullLogging = Settings.Default.enableFullLogging;
			enableStatsLogging = Settings.Default.enableStatsLogging;
			deltaTimeIndexFull = Settings.Default.targetDeltaTimeIndexFull;

			if (saveFolder == "none" || !Directory.Exists(saveFolder))
			{
				saveFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IgniteBot\\full_session_data");
				Directory.CreateDirectory(saveFolder);
				Settings.Default.saveFolder = saveFolder;
				Settings.Default.Save();
			}
		}

		/// <summary>
		/// Writes the data to the file
		/// </summary>
		/// <param name="data">The data to write</param>
		static void WriteToFile(string data)
		{
			// Fail if the folder doesn't even exist
			if (!Directory.Exists(saveFolder))
			{
				return;
			}

			if (batchWrites)
			{
				dataCache.Add(data);

				// if the time elapsed since last write is less than cutoff
				if (dataCache.Count * fullDeltaTimes[deltaTimeIndexFull] < 5000)
				{
					return;
				}
			}

			string filePath, directoryPath;

			// could combine with some other data path, such as AppData
			directoryPath = saveFolder;

			filePath = Path.Combine(directoryPath, fileName + ".json");

			lock (fileWritingLock)
			{
				StreamWriter streamWriter = new StreamWriter(filePath, true);

				if (batchWrites)
				{
					foreach (var row in dataCache)
					{
						streamWriter.WriteLine(row);
					}
					dataCache.Clear();
				}
				else
				{
					streamWriter.WriteLine(data);
				}

				streamWriter.Close();
			}
		}

		/// <summary>
		/// Goes through a "frame" (single JSON object) and generates the relevant events
		/// </summary>
		static void ProcessFrame(g_Instance frame)
		{
			// 'mpl_lobby_b2' may change in the future
			if (frame == null || frame.game_status == string.Empty || frame.map_name == "mpl_lobby_b2") return;

			// if we entered a different match
			if (frame.sessionid != lastFrame.sessionid || lastFrame == null)
			{
				// We just discard the old match and hope it was already submitted

				lastFrame = frame;  // don't detect stats changes across matches
									// TODO discard old players

				inPostMatch = false;
				matchData = new MatchData(frame);
				UpdateStatsIngame(frame);
			}

			/// <summary>
			/// The time between the current frame and last frame in seconds based on the game clock
			/// </summary>
			float deltaTime = lastFrame.game_clock - frame.game_clock;
			if (deltaTime != 0)
			{
				if (smoothDeltaTime == -1) smoothDeltaTime = deltaTime;
				float smoothingFactor = .99f;
				smoothDeltaTime = smoothDeltaTime * smoothingFactor + deltaTime * (1 - smoothingFactor);
			}


			// Did a player join or leave?

			// is a player from the current frame not in the last frame? (Player Join 🤝)
			// Loop through teams.
			foreach (var team in frame.teams)
			{
				// Loop through players on team.
				foreach (var player in team.players)
				{
					if (!lastFrame.AllPlayers.Any(p => p.userid == player.userid))
					{
						// TODO find why this is crashing
						try
						{
							matchData.Events.Add(new EventData(matchData, EventData.EventType.player_joined, frame.game_clock, player, null, player.Position, Vector3.Zero));
							LogRow(LogType.File, frame.sessionid, frame.game_clock_display + " - Player Joined: " + player.name);

							// cache this players stats so they aren't overridden if they join again
							var playerData = matchData.GetPlayerData(player);
							// if player was in this match before
							if (playerData != null)
							{
								playerData.CacheStats(player.stats);
							}

							UpdateStatsIngame(frame);
						}
						catch (Exception ex)
						{
							LogRow(LogType.Error, ex.ToString());
						}


					}
				}
			}

			// is a player from the last frame not in the current frame? (Player Leave 🚪)
			// Loop through teams.
			foreach (var team in lastFrame.teams)
			{
				// Loop through players on team.
				foreach (var player in team.players)
				{
					if (!frame.AllPlayers.Any(p => p.userid == player.userid))
					{
						matchData.Events.Add(new EventData(
							matchData,
							EventData.EventType.player_left,
							frame.game_clock,
							player,
							null,
							player.Position,
							player.Velocity)
						);

						LogRow(LogType.File, frame.sessionid, frame.game_clock_display + " - Player Left: " + player.name);

						UpdateStatsIngame(frame);
					}
				}
			}



			int currentFrameStats = 0;
			foreach (var team in frame.teams)
			{
				// Loop through players on team.
				foreach (var player in team.players)
				{
					currentFrameStats += player.stats.stuns + player.stats.points;
				}
			}

			if (currentFrameStats < lastFrameSumOfStats)
			{
				lastValidStatsFrame = lastFrame;
				lastValidSumOfStatsAge = 0;
			}

			lastValidSumOfStatsAge++;
			lastFrameSumOfStats = currentFrameStats;


			// Did the game state change?
			if (frame.game_status != lastFrame.game_status)
			{
				ProcessGameStateChange(frame, deltaTime);
			}

			// TODO remove
			//if (matchData != null && matchData.SumOfStats() == 0 && lastFrameSumOfStats != 0)
			//{
			//	LogRow(LogType.File, frame.sessionid, frame.game_clock_display + " - " + "stats were reset");
			//}
			//lastFrameSumOfStats = matchData.SumOfStats();

			// while playing and frames aren't identical
			if (frame.game_status == "playing" && deltaTime != 0)
			{
				inPostMatch = false;


				matchData.currentDiskTrajectory.Add(frame.disc.Position);

				if (frame.disc.Velocity.Equals(Vector3.Zero))
				{
					wasThrown = false;
				}

				// Generate "playing" events
				foreach (equipo team in frame.teams)
				{
					foreach (g_Player player in team.players)
					{
						var lastPlayer = lastFrame.GetPlayer(player.userid);
						if (lastPlayer == null) continue;

						MatchPlayer playerData = matchData.GetPlayerData(team, player);
						if (playerData != null)
						{
							// update player velocity
							Vector3 playerSpeed = player.Position - lastPlayer.Position;
							playerData.UpdateAverageSpeed(playerSpeed.Length() / deltaTime);

							// update hand velocities
							playerData.UpdateAverageSpeedLHand(((player.lhand.ToVector3() - lastPlayer.lhand.ToVector3()) - playerSpeed).Length() / deltaTime);
							playerData.UpdateAverageSpeedRHand(((player.rhand.ToVector3() - lastPlayer.rhand.ToVector3()) - playerSpeed).Length() / deltaTime);

							// update distance between hands
							//playerData.distanceBetweenHands.Add(Vector3.Distance(player.lhand.ToVector3(), player.rhand.ToVector3()));

							// update distance from hand to head
							float leftHandDistance = Vector3.Distance(player.Position, player.lhand.ToVector3());
							float rightHandDistance = Vector3.Distance(player.Position, player.rhand.ToVector3());
							playerData.distanceBetweenHands.Add(Math.Max(leftHandDistance, rightHandDistance));


							#region play space abuse

							// move the playspace based on reported game velocity
							playerData.playspaceLocation += player.Velocity * deltaTime;
							// move the playspace towards the current player position
							playerData.playspaceLocation += (player.Position - playerData.playspaceLocation).Normalized() * .05f * deltaTime;

							if (Math.Abs(smoothDeltaTime) < .1f && Math.Abs(deltaTime) < .1f && Vector3.Distance(player.Position, playerData.playspaceLocation) > 1.7f)
							{
								// playspace abuse happened
								matchData.Events.Add(
									new EventData(
										matchData,
										EventData.EventType.playspace_abuse,
										frame.game_clock,
										player,
										null,
										player.Position,
										player.Position - playerData.playspaceLocation));
								LogRow(LogType.File, frame.sessionid, frame.game_clock_display + " - " + player.name + " abused their playspace");
								playerData.PlayspaceAbuses++;

								// reset the playspace so we don't get extra events
								playerData.playspaceLocation = player.Position;

							}
							else if (Math.Abs(smoothDeltaTime) > .1f)
							{
								if (ENABLE_LOGGER)
								{
									Console.WriteLine("Update rate too slow to calculate playspace abuses.");
								}
							}

							#endregion

							// add time if upside down
							if (Vector3.Dot(player.up.ToVector3(), Vector3.UnitY) < 0)
							{
								playerData.InvertedTime += deltaTime;
							}


							playerData.PlayTime += deltaTime;
						}


						// check saves
						if (lastPlayer.stats.saves != player.stats.saves)
						{
							matchData.Events.Add(new EventData(matchData, EventData.EventType.save, frame.game_clock, player, null, player.Position, Vector3.Zero));
							LogRow(LogType.File, frame.sessionid, frame.game_clock_display + " - " + player.name + " made a save");
						}

						// check steals
						if (lastPlayer.stats.steals != player.stats.steals)
						{
							matchData.Events.Add(new EventData(matchData, EventData.EventType.steal, frame.game_clock, player, null, player.Position, Vector3.Zero));
							LogRow(LogType.File, frame.sessionid, frame.game_clock_display + " - " + player.name + " stole the disk");
						}

						// check stuns
						if (lastPlayer.stats.stuns != player.stats.stuns)
						{
							// try to match it to an existing stunnee

							// clean up the stun match list
							stunningMatchedPairs.RemoveAll(uat =>
							{
								if (uat[0] != null && uat[0].gameClock - frame.game_clock > stunMatchingTimeout) return true;
								else if (uat[1] != null && uat[1].gameClock - frame.game_clock > stunMatchingTimeout) return true;
								else return false;
							});

							bool added = false;
							foreach (var stunEvent in stunningMatchedPairs)
							{
								if (stunEvent[0] == null)
								{
									// if (stunEvent[1].player position is close to the stunner)
									stunningMatchedPairs.Remove(stunEvent);

									var stunner = player;
									var stunnee = stunEvent[1].player;

									matchData.Events.Add(new EventData(matchData, EventData.EventType.stun, frame.game_clock, stunner, stunnee, stunnee.Position, Vector3.Zero));
									LogRow(LogType.File, frame.sessionid, frame.game_clock_display + " - " + stunner.name + " just stunned " + stunnee.name);
									added = true;
									break;
								}
							}
							if (!added)
							{
								stunningMatchedPairs.Add(new UserAtTime[] { new UserAtTime { gameClock = frame.game_clock, player = player }, null });
							}
						}

						// check getting stunned
						if (!lastPlayer.stunned && player.stunned)
						{
							// try to match it to an existing stun

							// clean up the stun match list
							stunningMatchedPairs.RemoveAll(uat =>
							{
								if (uat[0] != null && uat[0].gameClock - frame.game_clock > stunMatchingTimeout) return true;
								else if (uat[1] != null && uat[1].gameClock - frame.game_clock > stunMatchingTimeout) return true;
								else return false;
							});
							bool added = false;
							foreach (var stunEvent in stunningMatchedPairs)
							{
								if (stunEvent[1] == null)
								{
									// if (stunEvent[0].player position is close to the stunee)
									stunningMatchedPairs.Remove(stunEvent);

									var stunner = stunEvent[0].player;
									var stunnee = player;

									matchData.Events.Add(new EventData(matchData, EventData.EventType.stun, frame.game_clock, stunner, stunnee, stunnee.Position, Vector3.Zero));
									LogRow(LogType.File, frame.sessionid, frame.game_clock_display + " - " + stunner.name + " just stunned " + stunnee.name);
									added = true;
									break;
								}
							}
							if (!added)
							{
								stunningMatchedPairs.Add(new UserAtTime[] { null, new UserAtTime { gameClock = frame.game_clock, player = player } });
							}
						}

						// check disk was caught
						if (!lastPlayer.possession && player.possession)
						{
							matchData.Events.Add(new EventData(matchData, EventData.EventType.@catch, frame.game_clock, player, null, player.Position, Vector3.Zero));
							LogRow(LogType.File, frame.sessionid, frame.game_clock_display + " - " + player.name + " made a catch");
						}

						// check if the disk was caught using stats
						if (lastPlayer.stats.catches != player.stats.catches)
						{
							LogRow(LogType.File, frame.sessionid, frame.game_clock_display + " - " + player.name + " made a catch (stat)");
						}

						// check blocks
						if (lastPlayer.stats.blocks != player.stats.blocks)
						{
							matchData.Events.Add(new EventData(matchData, EventData.EventType.block, frame.game_clock, player, null, player.Position, Vector3.Zero));
							LogRow(LogType.File, frame.sessionid, frame.game_clock_display + " - " + player.name + " just blocked");
						}

						// check shots taken
						if (lastPlayer.stats.shots_taken != player.stats.shots_taken)
						{
							matchData.Events.Add(new EventData(matchData, EventData.EventType.shot_taken, frame.game_clock, player, null, player.Position, Vector3.Zero));
							LogRow(LogType.File, frame.sessionid, frame.game_clock_display + " - " + player.name + " just took a shot");
						}

						// check disk was thrown
						if (!wasThrown && player.possession && !lastFrame.disc.Velocity.Equals(Vector3.Zero) && !frame.disc.Velocity.Equals(Vector3.Zero) &&
							(frame.disc.Velocity - player.velocity.ToVector3()).Length() > 2)
						{
							wasThrown = true;

							// find out which hand it was thrown by
							bool leftHanded = false;
							var leftHandVelocity = (lastPlayer.lhand.ToVector3() - player.lhand.ToVector3()) / deltaTime;
							var rightHandVelocity = (lastPlayer.rhand.ToVector3() - player.rhand.ToVector3()) / deltaTime;

							// based on velocity direction
							//if (Vector3.Distance(Vector3.Normalize(leftHandVelocity), Vector3.Normalize(frame.disc.Velocity)) <
							//	Vector3.Distance(Vector3.Normalize(rightHandVelocity), Vector3.Normalize(frame.disc.Velocity)))
							//{
							//	leftHanded = true;
							//}

							// based on position of hands
							if (Vector3.Distance(lastPlayer.lhand.ToVector3(), lastFrame.disc.Position) <
								Vector3.Distance(lastPlayer.rhand.ToVector3(), lastFrame.disc.Position))
							{
								leftHanded = true;
							}

							// find out underhandedness
							float underhandedness = 0;
							if (Vector3.Distance(lastPlayer.lhand.ToVector3(), lastFrame.disc.Position) <
								Vector3.Distance(lastPlayer.rhand.ToVector3(), lastFrame.disc.Position))
							{
								underhandedness = Vector3.Dot(lastPlayer.up.ToVector3(), lastPlayer.lhand.ToVector3() - lastPlayer.Position);
							}
							else
							{
								underhandedness = Vector3.Dot(lastPlayer.up.ToVector3(), lastPlayer.rhand.ToVector3() - lastPlayer.Position);
							}


							matchData.Events.Add(new EventData(matchData, EventData.EventType.@throw, frame.game_clock, player, null, player.Position, frame.disc.Velocity));
							LogRow(LogType.File, frame.sessionid, frame.game_clock_display + " - " + player.name + " threw the disk at " +
								frame.disc.Velocity.Length().ToString("N2") + " m/s with their " + (leftHanded ? "left" : "right") + " hand");
							matchData.currentDiskTrajectory.Clear();

							// add throw data type
							matchData.Throws.Add(new ThrowData(matchData, frame.game_clock, player, frame.disc.Position, frame.disc.Velocity, leftHanded, underhandedness));
						}

						// TODO check if a pass was made
						if (false)
						{
							matchData.Events.Add(new EventData(matchData, EventData.EventType.pass, frame.game_clock, player, null, player.Position, Vector3.Zero));
							LogRow(LogType.File, frame.sessionid, frame.game_clock_display + " - " + player.name + " made a pass");
						}

					}
				}
			}

			lastLastLastFrame = lastLastFrame;
			lastLastFrame = lastFrame;
			lastFrame = frame;
		}

		/// <summary>
		/// Function used to excute certain behavior based on frame given and previous frame(s).
		/// </summary>
		/// <param name="frame"></param>
		/// <param name="deltaTime"></param>
		private static void ProcessGameStateChange(g_Instance frame, float deltaTime)
		{
			LogRow(LogType.File, frame.sessionid, frame.game_clock_display + " - Entered state: " + frame.game_status);

			switch (frame.game_status)
			{
				case "pre_match":
					// if we just came from a playing state, this was a reset - requires a high enough polling rate
					if (lastFrame.game_status == "playing" || lastFrame.game_status == "round_start")
					{
						g_Instance frameToUse = lastLastFrame;
						if (lastValidSumOfStatsAge < 30)
						{
							frameToUse = lastValidStatsFrame;
						}
						EventMatchFinished(frameToUse, MatchData.FinishReason.reset, lastFrame.game_clock);
					}
					break;

				// round began
				case "round_start":
					inPostMatch = false;

					// if we just started a new 'round' (so stats haven't been reset)
					if (lastFrame.game_status == "round_over")
					{
						if (!READ_FROM_FILE)
						{
							UpdateStatsIngame(frame, false, false);
						}

						// This could cause a problem if someone is a spectator (their stats don't get reset, but they're not in the game) during the round transition
						foreach (MatchPlayer player in matchData.AllPlayers)
						{
							g_Player p = new g_Player
							{
								userid = player.Id
							};

							MatchPlayer lastPlayer = lastMatchData.GetPlayerData(p);

							if (lastPlayer != null)
							{
								player.StoreLastRoundStats(lastPlayer);
							}
							else
							{
								LogRow(LogType.Error, "Player exists in this round but not in last. Y");
							}
						}
					}

					if (!READ_FROM_FILE)
					{
						UpdateStatsIngame(frame);
					}

					break;

				// round really began
				case "playing":
					#region Started Playing
					if (!READ_FROM_FILE)
					{
						UpdateStatsIngame(frame);
					}

					// run through both teams, the point of this loop is to see who currently has the disc and where.
					// Loop through teams.	// TODO updated this
					foreach (var team in frame.teams)
					{
						// Loop through players on team.
						foreach (var player in team.players)
						{

							// reset playspace
							var playerData = matchData.GetPlayerData(team, player);
							if (playerData != null)
							{
								playerData.playspaceLocation = player.Position;
							}

							try
							{
							}
							catch (Exception ex)
							{
								LogRow(LogType.Error, ex.ToString());
							}
						}
					}


					#endregion
					break;

				// just scored
				case "score":
					#region Process Score

					ProcessScore(frame);

					#endregion
					break;

				case "round_over":
					if (frame.blue_points == frame.orange_points)
					{
						// OVERTIME
						LogRow(LogType.Info, "overtime");

					}
					else if (!frame.private_match && Math.Abs(frame.blue_points - frame.orange_points) >= 12)
					{
						ProcessScore(frame);

						EventMatchFinished(frame, MatchData.FinishReason.mercy, lastFrame.game_clock);
					}
					else if (frame.game_clock == 0 || lastFrame.game_clock < deltaTime * 10 || deltaTime < 0)
					{
						EventMatchFinished(frame, MatchData.FinishReason.game_time, 0);
					}
					else if (lastFrame.game_clock < deltaTime * 10 || lastFrame.game_status == "post_sudden_death" || deltaTime < 0)
					{
						// TODO add the score that ends an overtime
						// ProcessScore(frame); 

						// TODO find why finished and set reason
						EventMatchFinished(frame, MatchData.FinishReason.not_finished, lastFrame.game_clock);
					}
					else
					{
						EventMatchFinished(frame, MatchData.FinishReason.not_finished, lastFrame.game_clock);
					}
					break;

				// Game finished and showing scoreboard
				case "post_match":
					//EventMatchFinished(frame, MatchData.FinishReason.not_finished);
					break;

				case "pre_sudden_death":
					LogRow(LogType.Error, "idk if this is still in the api");
					break;
				case "sudden_death":
					// this happens right as the match finishes in a tie
					matchData.overtimeCount++;
					break;
				case "post_sudden_death":
					LogRow(LogType.Error, "idk if this is still in the api");
					break;


			}
		}

		/// <summary>
		/// Function used to extracted data from frame given
		/// </summary>
		/// <param name="frame"></param>
		private static void ProcessScore(g_Instance frame)
		{
			// Calculate the exact position within the goal that the disc was shot
			Vector3 discPos = frame.disc.Position;
			Vector3 discVel = lastFrame.disc.Velocity;
			Vector2 goalPos;
			bool backboard = false;
			float angleIntoGoal = 0;
			if (discVel != Vector3.Zero)
			{
				Vector3 actualGoalPos = discPos.Z < 0 ? new Vector3(0, 0, -36) : new Vector3(0, 0, 36);
				float angleIntoGoalRad = (float)(Math.Acos(Vector3.Dot(discVel, new Vector3(0, 0, 1) * (discPos.Z < 0 ? -1 : 1)) / discVel.Length()));
				angleIntoGoal = (float)(angleIntoGoalRad * (180 / Math.PI));
				float distToGoal = (float)((actualGoalPos.Z - discPos.Z) / Math.Cos(angleIntoGoalRad));
				Vector3 discDirection = discVel / discVel.Length();
				Vector3 goalPos3D = discPos + distToGoal * discDirection;
				goalPos = new Vector2(goalPos3D.X * (goalPos3D.Z < 0 ? -1 : 1), goalPos3D.Y);

				// make the angle negative if backboard
				if (angleIntoGoal > 90)
				{
					angleIntoGoal = 180 - angleIntoGoal;
					backboard = true;
				}
			}
			else
			{
				goalPos = new Vector2(frame.disc.Position.X, frame.disc.Position.Y);
			}

			// Call the Score event
			LogRow(LogType.File, frame.sessionid,
				frame.game_clock_display + " - " + frame.last_score.person_scored + " scored at " +
				frame.last_score.disc_speed.ToString("N2") + " m/s from " + frame.last_score.distance_thrown.ToString("N2") + " m away" +
				(frame.last_score.assist_scored == "[INVALID]" ? "!" : (", assisted by " + frame.last_score.assist_scored + "!")));

			g_Player scorer = frame.GetPlayer(frame.last_score.person_scored);
			var scorerPlayerData = matchData.GetPlayerData(scorer);
			if (scorerPlayerData != null)
			{
				if (frame.last_score.point_amount == 2)
				{
					scorerPlayerData.TwoPointers++;
				}
				else
				{
					scorerPlayerData.ThreePointers++;
				}
				scorerPlayerData.GoalsNum++;
			}

			// these are nullable types
			bool? leftHanded = null;
			float? underhandedness = null;
			if (matchData.Throws.Count > 0)
			{
				var lastThrow = matchData.Throws.Last();
				if (lastThrow != null)
				{
					leftHanded = lastThrow.isLeftHanded;
					underhandedness = lastThrow.underhandedness;
				}
			}

			matchData.Goals.Add(
				new GoalData(
					matchData,
					scorer,
					frame.last_score,
					frame.game_clock,
					goalPos,
					angleIntoGoal,
					backboard,
					discPos.Z < 0 ? equipo.Color.blue : equipo.Color.orange,
					leftHanded,
					underhandedness,
					matchData.currentDiskTrajectory
				));

			UpdateStatsIngame(frame);
		}

		/// <summary>
		/// Can be called often to update the ingame player stats
		/// </summary>
		/// <param name="frame">The current frame</param>
		private static void UpdateStatsIngame(g_Instance frame, bool endOfMatch = false, bool allowUpload = true)
		{
			if (inPostMatch) return;

			// team names may have changed
			matchData.teams[equipo.Color.blue].teamName = frame.teams[0].team;
			matchData.teams[equipo.Color.orange].teamName = frame.teams[1].team;

			if (frame.teams[0].stats != null)
			{
				matchData.teams[equipo.Color.blue].points = frame.blue_points;
			}
			if (frame.teams[1].stats != null)
			{
				matchData.teams[equipo.Color.orange].points = frame.orange_points;
			}

			UpdateAllPlayers(frame);

			// don't update right at the end of the match anyway
			// if end of match upload
			if (Settings.Default.whenToUploadLogs == 0 && endOfMatch) {

				UploadMatchBatch(true);
			}
			// if during-match upload
			else if (Settings.Default.whenToUploadLogs == 1 && frame.game_status != "pre_match")
			{
				UploadMatchBatch(false);
			} else
			{
				Console.WriteLine("Won't upload right now.");
			}
		}
		/// <summary>
		/// Function to wrapp up the match once we've entered post_match, restarted, or left spectate unexpectedly (crash)
		/// </summary>
		/// <param name="frame"></param>
		/// <param name="reason"></param>
		/// <param name="endTime"></param>
		private static void EventMatchFinished(g_Instance frame, MatchData.FinishReason reason, float endTime = 0)
		{
			matchData.endTime = endTime;
			matchData.finishReason = reason;

			LogRow(LogType.File, frame.sessionid, "Match Finished: " + reason.ToString());
			UpdateStatsIngame(frame, true);

			// if we here reset for public matches as well, then there would be super small files at the end of matches
			if (matchData.firstFrame.private_match)
			{
				NewFilename();
			}

			lastMatchData = matchData;
			matchData = null;

			inPostMatch = true;
		}

		public static void UploadMatchBatch(bool final = false)
		{
			BatchOutputFormat data = new BatchOutputFormat();
			data.final = final;
			data.match_data = matchData.ToDict();
			matchData.AllPlayers.ForEach(e => data.match_players.Add(e.ToDict()));
			matchData.Events.ForEach(e => { if (!e.inDB) data.events.Add(e.ToDict()); e.inDB = true; });
			matchData.Goals.ForEach(e => { if (!e.inDB) data.goals.Add(e.ToDict()); e.inDB = true; });
			matchData.Throws.ForEach(e => { if (!e.inDB) data.throws.Add(e.ToDict()); e.inDB = true; });

			_ = DoUploadMatchBatch(JsonConvert.SerializeObject(data));
			if (Settings.Default.uploadToFirestore)
			{
				_ = DoUploadMatchBatchFirebase(data);
			}
		}

		static async Task DoUploadMatchBatch(string data)
		{
			var content = new StringContent(data, Encoding.UTF8, "application/json");

			try
			{
				var response = await client.PostAsync("add_batch_generic", content);
				LogRow(LogType.Info, "[DB][Response] " + response.Content.ReadAsStringAsync().Result);
			}
			catch
			{
				LogRow(LogType.Error, "Can't connect to the DB server");
			}
		}

		static async Task DoUploadMatchBatchFirebase(BatchOutputFormat data)
		{
			WriteBatch batch = db.StartBatch();

			string season = "vrml_season_1";
			switch (currentAccessCodeUsername)
			{
				case "dev":
					season = "dev";
					break;
				case "VRML_TEST":
					season = "vrml_test";
					break;
				case "VRML_S1":
					season = "vrml_season_1";
					break;
				case "VRML_S2":
					season = "vrml_season_2";
					break;
			}

			// update the cumulative player stats
			CollectionReference playersRef = db.Collection("series/" + season + "/player_stats");
			foreach (Dictionary<string, object> p in data.match_players)
			{
				DocumentReference playerRef = playersRef.Document(p["player_name"].ToString());

				batch.Set(playerRef, p, SetOptions.MergeAll);
			}

			// update the match stats
			CollectionReference matchesRef = db.Collection("series/" + season + "/match_stats");
			DocumentReference matchDataRef = matchesRef.Document(data.match_data["match_time"] + "_" + data.match_data["session_id"]);
			batch.Set(matchDataRef, data.match_data, SetOptions.MergeAll);

			// update the match players
			foreach (var p in data.match_players)
			{
				DocumentReference matchPlayerRef = matchDataRef.Collection("players").Document(p["player_name"].ToString());
				batch.Set(matchPlayerRef, p, SetOptions.MergeAll);
			}

			await batch.CommitAsync();
		}

		// Update existing player with stats from game.
		static void UpdateSinglePlayer(g_Instance frame, equipo team, g_Player player, int won)
		{
			if (!matchData.teams.ContainsKey(team.color))
			{
				LogRow(LogType.Error, "No team. Wat."); return;
			}

			TeamData teamData = matchData.teams[team.color];

			// add a new match player if they didn't exist before
			if (!teamData.players.ContainsKey(player.userid))
			{
				teamData.players.Add(player.userid, new MatchPlayer(matchData, teamData, player));
			}

			MatchPlayer playerData = teamData.players[player.userid];

			playerData.Level = player.level;
			playerData.Number = player.number;
			playerData.PossessionTime = player.stats.possession_time;
			playerData.Points = player.stats.points;
			playerData.ShotsTaken = player.stats.shots_taken;
			playerData.Saves = player.stats.saves;
			//playerData.GoalsNum = player.stats.goals;	// disabled in favor of manual increment because the api is broken here
			playerData.Passes = player.stats.passes;
			playerData.Catches = player.stats.catches;
			playerData.Steals = player.stats.steals;
			playerData.Stuns = player.stats.stuns;
			playerData.Blocks = player.stats.blocks;
			playerData.Interceptions = player.stats.interceptions;
			playerData.Assists = player.stats.assists;
			playerData.Won = won;
		}

		static void UpdateAllPlayers(g_Instance frame)
		{
			// Loop through teams.
			foreach (var team in frame.teams)
			{
				// Loop through players on team.
				foreach (var player in team.players)
				{
					equipo.Color winningTeam =
						frame.blue_points > frame.orange_points ? equipo.Color.blue : equipo.Color.orange;
					int won = team.color == winningTeam ? 1 : 0;

					UpdateSinglePlayer(frame, team, player, won);
				}
			}
		}


		/// <summary>
		/// Generates a new filename from the current time.
		/// </summary>
		public static void NewFilename()
		{
			lock (fileWritingLock)
			{
				string lastFilename = fileName;
				fileName = DateTime.Now.ToString("rec_yyyy-MM-dd_HH-mm-ss");

				// compress the file
				if (useCompression)
				{
					if (File.Exists(Path.Combine(saveFolder, lastFilename + ".json")))
					{
						string tempDir = Path.Combine(saveFolder, "temp_zip").ToString();
						Directory.CreateDirectory(tempDir);
						File.Move(Path.Combine(saveFolder, lastFilename + ".json"), Path.Combine(saveFolder, "temp_zip", lastFilename + ".json"));      // TODO can fail because in use
						ZipFile.CreateFromDirectory(tempDir, Path.Combine(saveFolder, lastFilename + ".zip"));
						Directory.Delete(tempDir, true);
					}
				}
			}
		}

		public static bool CheckAccessCode(string accessCode)
		{
			if (accessCode == "") return false;

			using (SHA256 sha = SHA256.Create())
			{
				var hashedPW = sha.ComputeHash(Encoding.ASCII.GetBytes(accessCode));

				// Convert the byte array to hexadecimal string
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < hashedPW.Length; i++)
				{
					sb.Append(hashedPW[i].ToString("X2"));
				}
				string passHash = sb.ToString().ToLower();
				if (validAccessCodeHashes.Keys.Contains(passHash))
				{
					currentAccessCodeUsername = validAccessCodeHashes[passHash];
					client.DefaultRequestHeaders.Remove("access-code");
					client.DefaultRequestHeaders.Add("access-code", currentAccessCodeUsername);
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		private static async Task InitializeDatabase()
		{
			var values = new Dictionary<string, string> { };
			var content = new FormUrlEncodedContent(values);

			try
			{
				var response = await client.PostAsync("initDB", content);
				LogRow(LogType.Info, "[DB][Response] " + response.Content.ReadAsStringAsync().Result);
			}
			catch (HttpRequestException ex)
			{
				LogRow(LogType.Error, "Can't connect to the DB server");
			}
		}

	}

	/// <summary>
	/// Custom Vector3 class used to keep track of 3D coordinates.
	/// Works more like the Vector3 included with Unity now.
	/// </summary>
	public static class Vector3Extensions
	{
		public static Vector3 ToVector3(this List<float> input)
		{
			if (input.Count != 3)
			{
				throw new Exception("Can't convert List to Vector3");
			}
			return new Vector3(input[0], input[1], input[2]);
		}

		public static Vector3 ToVector3(this float[] input)
		{
			if (input.Length != 3)
			{
				throw new Exception("Can't convert array to Vector3");
			}
			return new Vector3(input[0], input[1], input[2]);
		}

		public static float DistanceTo(this Vector3 v1, Vector3 v2)
		{
			return (float)Math.Sqrt(Math.Pow(v1.X - v2.X, 2) + Math.Pow(v1.Y - v2.Y, 2) + Math.Pow(v1.Z - v2.Z, 2));
		}

		public static Vector3 Normalized(this Vector3 v1)
		{
			return v1 / v1.Length();
		}
	}


}

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net.NetworkInformation;
using System.Linq;
using System.Net.Http;
using IgniteBot;

/// <summary>
/// Logs any data to a file or remotely
/// </summary>
public class Logger
{
	public const bool ENABLE_LOGGER = true;

	private static string logFolder = "Log";
	private const string fileExtension = ".tsv";
	private const string delimiter = "\t";
	private const string dateFormat = "yyyy/MM/dd HH:mm:ss.fff";
	private const string newLineChar = "\n";
	public static string webLogURL = "https://ignitevr.gg/CustomCode/Php/uploadLog.php";
	private const string passwordField = "password";

	public static string webLogPassword = "xxxxxxxxxxxxxxxx";
	public static string folder = "logs";
	public static List<string> subFolders = new List<string>();

	/// <summary>
	/// How many lines to wait before actually logging
	/// </summary>
	public static int lineLogInterval = 0;
	private static int numLinesLogged;

	public static bool usePerDeviceFolder = true;
	public static bool usePerLaunchFolder = true;
	public static bool enableLoggingLocal = true;
	public static bool enableLoggingRemote = true;

	public static bool useFullFileCache = true;
	public static List<string> fullFileCache = new List<string>();
	public static StringBuilder unusedFileCache = new StringBuilder();

	private static readonly HttpClient client = new HttpClient();

	public enum LogType
	{
		/// <summary>
		/// Just print to terminal and nothing else.
		/// </summary>
		Info,
		/// <summary>
		/// Print to custom file and terminal.
		/// </summary>
		File,
		/// <summary>
		/// Print to terminal and error file.
		/// </summary>
		Error
	}

	/// <summary>
	/// Dictionary of filename and list of lines that haven't been logged yet
	/// </summary>
	private static Dictionary<string, List<string>> dataToLog = new Dictionary<string, List<string>>();

	/// <summary>
	/// Writes a message to the console, file, or upload
	/// </summary>
	/// <param name="overwrite">Whether or not to override the file we are logging to. Only valid for LogType.File</param>
	/// <param name="elements">The columns in the row</param>
	public static void LogRow(LogType logType, params string[] elements)
	{
		if (!ENABLE_LOGGER) return;

		switch (logType)
		{
			case LogType.Info:
				if (elements.Length == 1)
				{
					if (Program.showDatabaseLog || !elements[0].Contains("[DB]"))
					{
						Console.WriteLine(elements[0]);
					}
				}
				else
					Console.WriteLine("LogType info with more or less than 1 argument.");
				break;
			case LogType.File:
				if (elements.Length >= 2)
				{
					List<string> list = new List<string>(elements);
					list.RemoveAt(0);
					LogRow(elements[0], list);
					Console.WriteLine(list[0]);
					if (useFullFileCache)
					{
						lock (Program.logOutputWriteLock)
						{
							fullFileCache.Add(list[0]);
							unusedFileCache.AppendLine(list[0]);

							// get rid of really old stuff
							if (fullFileCache.Count > 5000)
							{
								fullFileCache.RemoveRange(0, 1000);
							}
						}
					}
				}
				else
				{
					Console.WriteLine("LogType File with fewer than 2 parameters. Must specify a file name and data.");
				}
				break;
			case LogType.Error:
				if (elements.Length > 0)
				{
					Console.WriteLine(elements[0]);
					LogRow("error", elements);
				}
				break;
		}
	}

	/// <summary>
	/// Logs data to a file
	/// </summary>
	/// <param name="fileName">e.g. "movement"</param>
	/// <param name="data">List of columns to log</param>
	private static void LogRow(string fileName, IEnumerable<string> data)
	{
		if (!ENABLE_LOGGER) return;

		// add the data to the dictionary
		try
		{
			var macAddr =
				(
					from nic in NetworkInterface.GetAllNetworkInterfaces()
					where nic.OperationalStatus == OperationalStatus.Up
					select nic.GetPhysicalAddress().ToString()
				).FirstOrDefault();


			StringBuilder strBuilder = new StringBuilder();
			strBuilder.Append(DateTime.Now.ToString(dateFormat));
			strBuilder.Append(delimiter);
			strBuilder.Append(macAddr);
			strBuilder.Append(delimiter);
			foreach (var elem in data)
			{
				string newElem = elem;
				if (elem.Contains(delimiter))
				{
					newElem = elem.Replace(delimiter, "@");
					LogRow(LogType.Error, "Data contains delimiter: " + newElem);
				}

				strBuilder.Append(newElem);
				strBuilder.Append(delimiter);
			}
			if (!dataToLog.ContainsKey(fileName))
			{
				dataToLog.Add(fileName, new List<string>());
			}
			dataToLog[fileName].Add(strBuilder.ToString());
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}

		numLinesLogged++;

		UpdateLogger();

	}

	/// <summary>
	/// Just references the LogRow overload above
	/// </summary>
	/// <param name="fileName"></param>
	/// <param name="elements"></param>
	private static void LogRow(string fileName, params string[] elements)
	{
		if (!ENABLE_LOGGER) return;

		if (elements is null)
		{
			return;
		}

		LogRow(fileName, new List<string>(elements));

		UpdateLogger();
	}

	private static void ActuallyLog()
	{
		if (!ENABLE_LOGGER) return;

		StringBuilder allOutputData = new StringBuilder();

		foreach (var fileName in dataToLog.Keys)
		{
			StreamWriter fileWriter = null;
			if (enableLoggingLocal)
			{
				string filePath, directoryPath;

				// combine with some other data path, such as AppData
				directoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IgniteBot\\"+ logFolder);

				if (!Directory.Exists(directoryPath))
				{
					Directory.CreateDirectory(directoryPath);
				}

				filePath = Path.Combine(directoryPath, fileName + fileExtension);

				// create writer
				try
				{
					fileWriter = new StreamWriter(filePath, true);
				}
				catch (IOException e)
				{
					LogRow(LogType.Error, "Can't open log file for writing");
					continue;
				}
			}

			allOutputData.Clear();

			// actually log data
			try
			{
				foreach (var row in dataToLog[fileName])
				{
					if (enableLoggingLocal)
					{
						fileWriter.WriteLine(row);
					}
					if (enableLoggingRemote)
					{
						allOutputData.Append(row);
						allOutputData.Append(newLineChar);
					}
				}
				dataToLog[fileName].Clear();

				if (enableLoggingRemote)
				{
					Upload(fileName + fileExtension, allOutputData.ToString(), folder);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}

			if (enableLoggingLocal)
			{
				fileWriter.Close();
			}
		}

		numLinesLogged = 0;
	}

	static async void Upload(string name, string data, string appName)
	{
		if (!ENABLE_LOGGER) return;

		var values = new Dictionary<string, string>
			{{passwordField, webLogPassword },
				{ "file", name},
				{ "data", data},
				{ "folder", appName} };
		try
		{
			var content = new FormUrlEncodedContent(values);
			var response = await client.PostAsync(webLogURL, content);
			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine(response.Content.ToString());
			}
		}
		catch (Exception e)
		{
			Console.WriteLine("Cound not upload log to the server: " + data.Substring(0, data.Length > 1000 ? 1000 : data.Length) + "\n Error: " + e.ToString());
		}
	}

	public static void UpdateLogger()
	{
		if (!ENABLE_LOGGER) return;

		if (numLinesLogged > lineLogInterval)
		{
			ActuallyLog();
		}
	}
}
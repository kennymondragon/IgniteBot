﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace IgniteBot
{
	/// <summary>
	/// The idea is this will contain the entire set of data for the match, and will be used to review and then commit the data all at once 
	/// rather than little by little like before.
	/// 
	/// PlayerData represents the stats for a player only for a particular match
	/// </summary>
	public class MatchPlayer : DataContainer
	{
		public MatchPlayer(MatchData match, TeamData team, g_Player player)
		{
			matchData = match;
			teamData = team;
			Id = player.userid;
			Name = player.name;
			playspaceLocation = player.Position;
			PlayspaceAbuses = 0;
		}

		/// <summary>
		/// Function to transform player data into the desired format for firestore.
		/// </summary>
		/// <returns></returns>
		public Dictionary<string, object> ToDict()
		{
			Dictionary<string, object> values = new Dictionary<string, object>
			{
				{"session_id", matchData.SessionId},
				{"match_time", matchData.MatchTimeSQL },
				{"player_id", Id },
				{"player_name", Name },
				{"level", Level },
				{"number", Number },
				{"team_color", teamData.teamColor.ToString() },
				{"possession_time", PossessionTime},
				{"play_time", PlayTime},
				{"inverted_time", InvertedTime},
				{"points", Points},
				{"2_pointers",TwoPointers },
				{"3_pointers",ThreePointers },
				{"shots_taken",ShotsTaken },
				{"saves",Saves },
				{"goals",GoalsNum },
				{"stuns",Stuns },
				{"passes",Passes },
				{"catches",Catches },
				{"steals",Steals },
				{"blocks",Blocks },
				{"interceptions",Interceptions },
				{"assists", Assists },
				{"average_speed", averageSpeed[0] },
				{"average_speed_lhand", averageSpeed[1] },
				{"average_speed_rhand", averageSpeed[2] },
				{"wingspan", DistanceBetweenHands },
				{"playspace_abuses", PlayspaceAbuses },
				{"wins", Won }
			};

			return values;
		}

		#region Get/Set Methods
		public long Id { get; set; }
		/// <summary>
		/// Link to a image to be used as a profile picture on their page.
		/// This should probably be removed unless we choose to add a feature in which users can define their own image link (not safe).
		/// </summary>
		public string ProfileLink { get; set; }
		public string Name { get; set; }
		public int Level { get; set; }
		public int Number { get; set; }

		private playerStats currentStats = new playerStats();
		private playerStats cachedStats = new playerStats();
		private playerStats lastRoundStats = new playerStats();

		public float PossessionTime {
			get => cachedStats.possession_time + currentStats.possession_time - lastRoundStats.possession_time;
			set => currentStats.possession_time = value;
		}

		public float PlayTime { get; set; }
		public float InvertedTime { get; set; }

		public int Points {
			get => cachedStats.points + currentStats.points - lastRoundStats.points;
			set => currentStats.points = value;
		}

		public int ShotsTaken {
			get => cachedStats.shots_taken + currentStats.shots_taken - lastRoundStats.shots_taken;
			set => currentStats.shots_taken = value;
		}

		public int Saves {
			get => cachedStats.saves + currentStats.saves - lastRoundStats.saves;
			set => currentStats.saves = value;
		}

		public int GoalsNum { get; set; }
		public int TwoPointers { get; set; }
		public int ThreePointers { get; set; }

		public int Passes {
			get => cachedStats.passes + currentStats.passes - lastRoundStats.passes;
			set => currentStats.passes = value;
		}

		public int Catches {
			get => cachedStats.catches + currentStats.catches - lastRoundStats.catches;
			set => currentStats.catches = value;
		}

		public int Steals {
			get => cachedStats.steals + currentStats.steals - lastRoundStats.steals;
			set => currentStats.steals = value;
		}

		public int Stuns {
			get => cachedStats.stuns + currentStats.stuns - lastRoundStats.stuns;
			set => currentStats.stuns = value;
		}

		public int Blocks {
			get => cachedStats.blocks + currentStats.blocks - lastRoundStats.blocks;
			set => currentStats.blocks = value;
		}

		public int Interceptions {
			get => cachedStats.interceptions + currentStats.interceptions - lastRoundStats.interceptions;
			set => currentStats.interceptions = value;
		}

		public int Assists {
			get => cachedStats.assists + currentStats.assists - lastRoundStats.assists;
			set => currentStats.assists = value;
		}

		public float DistanceBetweenHands {
			get {
				distanceBetweenHands.Sort();
				if (distanceBetweenHands.Count > 100)
				{
					return distanceBetweenHands[(int)((distanceBetweenHands.Count - 1) * (99f / 100))];
				}
				else if (distanceBetweenHands.Count > 0)
				{
					return distanceBetweenHands.Last();
				}
				else
				{
					return 0;
				}
			}
		}
		public int Won { get; set; }
		public MatchData matchData;
		public TeamData teamData;
		public Vector3 playspaceLocation;
		public int PlayspaceAbuses { get; set; }

		// head, lhand, rhand
		public float[] averageSpeed = { 0, 0, 0 };

		/// <summary>
		/// Positions every 1s of game time
		/// </summary>
		public List<Vector3> sparsePositions = new List<Vector3>();
		public List<float> distanceBetweenHands = new List<float>();
		float[] avgSpeedTotal = { 0, 0, 0 };
		int[] avgSpeedCount = { 0, 0, 0 };
		public void UpdateAverageSpeed(float newSpeed)
		{
			avgSpeedTotal[0] += newSpeed;
			averageSpeed[0] = (avgSpeedTotal[0]) / ++(avgSpeedCount[0]);
		}

		public void UpdateAverageSpeedLHand(float newSpeed)
		{
			avgSpeedTotal[1] += newSpeed;
			averageSpeed[1] = (avgSpeedTotal[1]) / ++(avgSpeedCount[1]);
		}

		public void UpdateAverageSpeedRHand(float newSpeed)
		{
			avgSpeedTotal[2] += newSpeed;
			averageSpeed[2] = (avgSpeedTotal[2]) / ++(avgSpeedCount[2]);
		}

		#endregion
		/// <summary>
		/// Store players current stats in case we lose them from a crash.
		/// </summary>
		/// <param name="newPlayerStats"></param>
		public void CacheStats(playerStats newPlayerStats)
		{
			// if player joined back from spectator
			if ((newPlayerStats.possession_time + newPlayerStats.points + newPlayerStats.shots_taken + newPlayerStats.saves + newPlayerStats.passes + newPlayerStats.catches + newPlayerStats.steals + newPlayerStats.stuns + newPlayerStats.blocks + newPlayerStats.interceptions + newPlayerStats.assists) != 0)
			{
				return;
			}
			cachedStats += newPlayerStats;
		}

		/// <summary>
		/// Store players stats from last round for later use in determining stats on a per round basis.
		/// </summary>
		/// <param name="playerStats"></param>
		public void StoreLastRoundStats(MatchPlayer lastPlayer)
		{
			lastRoundStats = lastPlayer.lastRoundStats;

			lastRoundStats.possession_time += lastPlayer.PossessionTime;
			lastRoundStats.points += lastPlayer.Points;
			lastRoundStats.shots_taken += lastPlayer.ShotsTaken;
			lastRoundStats.saves += lastPlayer.Saves;
			lastRoundStats.passes += lastPlayer.Passes;
			lastRoundStats.catches += lastPlayer.Catches;
			lastRoundStats.steals += lastPlayer.Steals;
			lastRoundStats.stuns += lastPlayer.Stuns;
			lastRoundStats.blocks += lastPlayer.Blocks;
			lastRoundStats.interceptions += lastPlayer.Interceptions;
			lastRoundStats.assists += lastPlayer.Assists;
			lastRoundStats.goals += lastPlayer.GoalsNum;
		}
	}
}

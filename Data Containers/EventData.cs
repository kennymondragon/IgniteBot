using System.Collections.Generic;
using System.Numerics;

namespace IgniteBot
{
	/// <summary>
	/// Object containing data describing certain events like stuns, throws, quits, joins, etc.
	/// </summary>
	public class EventData : DataContainer
	{
		public EventData(MatchData match, EventType eventType, float gameClock, g_Player player, g_Player otherPlayer, Vector3 position, Vector3 vec2)
		{
			matchData = match;
			this.eventType = eventType;
			this.gameClock = gameClock;
			this.player = player;
			this.otherPlayer = otherPlayer;
			this.position = position;
			this.vec2 = vec2;
		}

		public MatchData matchData;
		public enum EventType
		{
			stun,
			block,
			save,
			@catch,
			pass,
			@throw,
			shot_taken,
			steal,
			playspace_abuse,
			player_joined,
			player_left
		}


		/// <summary>
		/// Whether or not this data has been sent to the DB or not
		/// </summary>
		public bool inDB = false;

		public EventType eventType;
		public float gameClock;
		public g_Player player;
		public g_Player otherPlayer;
		public Vector3 position;
		public Vector3 vec2;

		/// <summary>
		/// Function to transform event data into the desired format for firestore.
		/// </summary>
		/// <returns></returns>
		public Dictionary<string, object> ToDict()
		{
			var values = new Dictionary<string, object>
			{
				{"session_id", matchData.SessionId },
				{"match_time", matchData.MatchTimeSQL },
				{"game_clock", gameClock },
				{"player_id", player.userid },
				{"player_name", player.name },
				{"event_type", eventType.ToString() },
				{"other_player_id", otherPlayer != null ? (long?)otherPlayer.userid : null },
				{"other_player_name", otherPlayer != null ? otherPlayer.name : null },
				{"pos_x", position.X },
				{"pos_y", position.Y },
				{"pos_z", position.Z },
				{"x2", vec2 != Vector3.Zero ? (float?)vec2.X : null },
				{"y2", vec2 != Vector3.Zero ? (float?)vec2.Y : null },
				{"z2", vec2 != Vector3.Zero ? (float?)vec2.Z : null }
			};

			return values;
		}
	}
}

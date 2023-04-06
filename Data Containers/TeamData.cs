using System.Collections.Generic;
using static IgniteBot.equipo;

namespace IgniteBot
{
	/// <summary>
	/// Object containing a teams basic data and MatchPlayer for the corresponding team.
	/// </summary>
	public class TeamData
	{
		public Color teamColor;
		public string teamName;
		public int points;

		/// <summary>
		/// Dictionary of <userid, PlayerData>
		/// </summary>
		public Dictionary<long, MatchPlayer> players;

		public TeamData(Color teamColor, string teamName)
		{
			this.teamColor = teamColor;
			players = new Dictionary<long, MatchPlayer>();
			this.teamName = teamName;
		}

	}

}

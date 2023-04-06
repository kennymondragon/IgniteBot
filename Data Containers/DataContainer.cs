using System.Collections.Generic;

namespace IgniteBot
{
	/// <summary>
	/// Data container interface... this comment isn't necessary but hey life is short. 
	/// </summary>
	/// <returns></returns>
	interface DataContainer
	{
		Dictionary<string, object> ToDict();

	}
}

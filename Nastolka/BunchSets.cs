using System;
using System.Collections.Generic;

namespace Nastolka
{
	public class BunchSets
	{
		List<Set> Sets { get; set; }

		public void OpenAllSets()
		{
			Sets = Parser.ParseAllFromDirectory();
		}


		public string GetAllSetsInfo()
		{
			string result = "";
			foreach (Set set in Sets)
			{
				result += set.GetSetContent() + "\n";
			}
			return result;
		}
	}
}

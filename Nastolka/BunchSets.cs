using System;
using System.Collections.Generic;

namespace Nastolka
{
	/// <summary>
	/// Общая область наборов.
	/// </summary>
	public class BunchSets
	{
		List<Set> Sets { get; set; }

		public void OpenAllSets()
		{
			Sets = Parser.ParseAllFromDirectory();
		}

		/// <summary>
		/// Возвращает информацию о всех доступных наборах.
		/// </summary>
		/// <returns>Строка с содержимым всех наборов.</returns>
		public string GetAllSetsInfo()
		{
			string result = "";
			foreach (Set set in Sets)
			{
				result += set.GetSetContent() + "\n";
			}
			return result;
		}

		/// <summary>
		/// Генерирует случайную ситуацию для всех наборов.
		/// </summary>
		/// <returns></returns>
		public string RandomAll()
		{
			string result = "";
			foreach (var set in Sets)
			{
				result += set.RandomizeCharacters() + "\n";
			}
			return result;
		}
	}
}

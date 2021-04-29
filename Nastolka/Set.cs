using System;
using System.Collections.Generic;

namespace Nastolka
{
	/// <summary>
	/// Набор характеристик.
	/// </summary>
	public class Set
	{
		public string Name { get; set; }
		public List<Character> Characters { get; set; }

		public Set()
		{
			Name = "";
			Characters = new List<Character>();
		}
		public Set(List<Character> characters)
		{
			Name = "";
			Characters = characters;
		}
		public Set(string name, List<Character> characters)
		{
			Name = name;
			Characters = characters;
		}

		/// <summary>
		/// Возвращает строковое содержимое всего набора.
		/// </summary>
		/// <returns>Строка с характеристиками.</returns>
		public string GetSetContent()
		{
			string result = $"======Набор {Name}======\n";
			foreach (Character character in Characters)
			{
				result += character.GetAllVariables() + "\n";
			}
			return result;
		}

		/// <summary>
		/// Возвращает все характеристики набора со случайными значениями.
		/// </summary>
		/// <returns></returns>
		public string RandomizeCharacters()
		{
			string result = "";
			foreach (var character in Characters)
			{
				result += character.GetRandomVariable() + "\n";
			}
			return result;
		}
	}

	/// <summary>
	/// Характеристика чего-либо.
	/// </summary>
	public struct Character
	{
		private Random random;

		public string Name { get; set; }
		public List<string> Variables { get; set; }

		public Character(string name, List<string> variables)
		{
			random = new Random();
			Name = name;
			Variables = variables;
		}

		/// <summary>
		/// Возвращает все вариации характеристики на отдельных строках.
		/// </summary>
		/// <returns>Строка с вариантами характеристик.</returns>
		public string GetAllVariables()
		{
			string result = Name + ":\n";
			foreach (string variable in Variables)
			{
				result += variable + "\n";
			}
			return result;
		}

		/// <summary>
		/// Возвращает случайную характеристику и ее название.
		/// </summary>
		/// <returns>Строка с характеристикой.</returns>
		public string GetRandomVariable()
		{
			int randomVariableIndex = random.Next(Variables.Count);
			string result = Name + ": " + Variables[randomVariableIndex];
			return result;
		}
	}
}

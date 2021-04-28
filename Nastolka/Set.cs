using System;
using System.Collections.Generic;

namespace Nastolka
{
	public class Set
	{
		public string Name { get; set; }
		public List<Character> Characters = new List<Character>();

		public Set(string name, List<Character> characters)
		{
			Name = name;
			Characters = characters;
		}

		public string GetSetContent()
		{
			string result = $"======Набор {Name}======\n";
			foreach (Character character in Characters)
			{
				result += character.GetAllVariables() + "\n";
			}
			return result;
		}
	}

	public struct Character
	{
		public string Name { get; set; }
		public List<string> Variables { get; set; }

		public Character(string name, List<string> variables)
		{
			Name = name;
			Variables = variables;
		}

		public string GetAllVariables()
		{
			string result = Name + ":\n";
			foreach (string variable in Variables)
			{
				result += variable + "\n";
			}

			return result;
		}
	}
}

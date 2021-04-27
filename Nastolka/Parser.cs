using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nastolka
{
	public static class Parser
	{
		private const string directoryPath = "Sets";
		private static string[] sets;
	

		public static string ParseAllFromDirectory()
		{
			string[] filesNames = GetAllFiles();
			string fileText = "";
			foreach (string fileName in filesNames)
			{
				fileText = ParseFile(fileName);
			}
			return fileText;
		}

		public static string ParseFile(string path)
		{
			string fileText;
			List<string> characters;

			using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
			{
				fileText = sr.ReadToEnd();
			}
			fileText = fileText.Replace(Environment.NewLine, "");
			characters = CharactersFileSplit(fileText);

			string outputText = CreateSet(characters);



			return outputText;
		}

		//
		private static string[] GetAllFiles()
		{
			sets = Directory.GetFiles(directoryPath);
			return sets;
		}

		//
		private static List<string> CharactersFileSplit(string fileText)
		{
			List<string> characters = new List<string>();

			int startSelectionIndex = 0;
			int symbolCount = 1;
			string newCharText;
			for (int i = 0; i < fileText.Length; i++)
			{
				if (fileText[i] == '}')
				{
					newCharText = fileText.Substring(startSelectionIndex, 
						symbolCount);
					characters.Add(newCharText);
					startSelectionIndex = i + 1;
					symbolCount = 0;
				}
				symbolCount++;
			}
			return characters;
		}

		//
		private static string CreateSet(List<string> characters)
		{
			string[] splitedCharacter;
			string characterName;
			string characterTextVariables;
			List<string> characterVariables;

			string output = "";
			foreach (string characterText in characters)
			{
				splitedCharacter = characterText.Split(':');
				characterName = splitedCharacter[0].Trim();
				characterTextVariables = splitedCharacter[1].Trim();
				characterVariables = CleanVariables(characterTextVariables);
				var newCharacter = new Character(characterName, characterVariables);

				foreach (string i in characterVariables)
				{
					output += i + "|";
				}
				output += "\n";
			}
			return output;
		}

		//
		private static List<string> CleanVariables(string variables)
		{
			List<string> cleanedVariables = new List<string>();
			string createdVariable;

			variables = variables.Trim('{', '}');
			variables = ClearExtraSymbols(variables);
			foreach (string varialbe in variables.Split(" "))
			{
				createdVariable = varialbe.Trim();
				if (createdVariable.Length > 0)
					cleanedVariables.Add(createdVariable);
			}
			return cleanedVariables;
		}

		//
		private static string ClearExtraSymbols(string text)
		{
			string newText = text;
			newText = newText.Replace("\t", String.Empty);
			newText = newText.Replace("\n", String.Empty);
			while (newText.Contains("  "))
			{
				newText = newText.Replace("  ", " ");
			}
			return newText;
		}
	}
}

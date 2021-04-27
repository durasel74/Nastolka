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

			foreach (string characterText in characters)
			{
				CreateCharacter(characterText);
			}

			string outputText = "";
			foreach (string i in characters)
			{
				string newI1 = i.Split(':')[0];
				string newI2 = i.Split(':')[1];
				outputText += newI1 + "|" + newI2 + "\n";
			}


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
		private static Character CreateCharacter(string characterText)
		{
			


			return new Character("OK", new List<string>());
		}
	}
}

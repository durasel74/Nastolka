using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Nastolka
{
	/// <summary>
	/// Предоставляет методы для преобразования файлов в наборы характеристик.
	/// </summary>
	public static class Parser
	{
		private const string directoryPath = "Sets";
		private const string fileFormat = ".txt";

		/// <summary>
		/// Преобразование всех файлов, находящихся в директории.
		/// </summary>
		/// <returns>Список полученных наборов.</returns>
		public static List<Set> ParseAllFromDirectory()
		{
			string[] filesNames = Directory.GetFiles(directoryPath);

			Set newSet;
			List<Set> sets = new List<Set>();
			foreach (string fileName in filesNames)
			{
				newSet = ParseFile(fileName);
				sets.Add(newSet);
			}
			return sets;
		}

		/// <summary>
		/// Преобразует файл в набор характеристик.
		/// </summary>
		/// <param name="path">Путь к файлу.</param>
		/// <returns>Возвращает набор характеристик.</returns>
		public static Set ParseFile(string path)
		{
			string fileText;
			using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
			{
				fileText = sr.ReadToEnd();
			}
			fileText = fileText.Replace(Environment.NewLine, " ");
			var charactersText = CharactersFileSplit(fileText);
			var characters = ParseCharacters(charactersText);
			string setName = GetCleanFileName(path);
			var newSet = new Set(setName, characters);
			return newSet;
		}

		/// <summary>
		/// Возвращает индекс характеристики в списке, найденный по имени.
		/// </summary>
		/// <param name="characterName">Имя характеристики.</param>
		/// <param name="characters">Список характеристик.</param>
		/// <returns>Индекс характеристики в списке.</returns>
		public static int FindCharacterFromList(string characterName,
			List<Character> characters)
		{
			for (int i = 0; i < characters.Count; i++)
			{
				if (characters[i].Name == characterName)
					return i;
			}
			return -1;
		}

		// Выдает имя файла без расширения
		private static string GetCleanFileName(string path)
		{
			string result;
			result = path.Replace(directoryPath + "\\", "");
			result = result.Replace(fileFormat, "");
			return result;
		}

		// Выделение отдельных характеристик и их вариаций
		private static List<string> CharactersFileSplit(string fileText)
		{
			List<string> characters = new List<string>();
			string newCharText;

			int startSelectionIndex = 0;
			int symbolCount = 1;
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

		// Преобразует данные характеристик из файла в структуру
		private static List<Character> ParseCharacters(List<string> characters)
		{
			List<Character> newCharacters = new List<Character>();

			string[] splitedCharacter;
			string characterName;
			string characterTextVariables;
			List<string> characterVariables;

			foreach (string characterText in characters)
			{
				splitedCharacter = characterText.Split(':');
				characterName = splitedCharacter[0].Trim();
				characterTextVariables = splitedCharacter[1].Trim();
				characterVariables = CleanVariables(characterTextVariables);
				CreateNewCharacter(characterName, characterVariables, newCharacters);
			}
			return newCharacters;
		}

		// Создание новой характеристики, присваивает все варианты
		// характеристики уже существующей если такая существует.
		private static void CreateNewCharacter(string characterName, 
			List<string> characterVariables, List<Character> resultList)
		{
			int requiredIndex = FindCharacterFromList(characterName, resultList);
			if (requiredIndex == -1)
			{
				var newCharacter = new Character(characterName, characterVariables);
				resultList.Add(newCharacter);
			}
			else
				resultList[requiredIndex].Variables.AddRange(characterVariables);
		}

		// Преобразует варианты характеристик из файла в список вариантов
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

		// Очистка текста от символов переноса строки, табуляции и лишних пробелов
		private static string ClearExtraSymbols(string text)
		{
			string newText = text;
			newText = newText.Replace("\t", String.Empty);
			while (newText.Contains("  "))
			{
				newText = newText.Replace("  ", " ");
			}
			return newText;
		}
	}
}

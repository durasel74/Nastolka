using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Nastolka
{
	/// <summary>
	/// Общая область наборов.
	/// </summary>
	public class BunchSets : INotifyPropertyChanged
	{
		private List<Set> Sets;
		public ObservableCollection<Set> CommonSets { get; set; }
		public ObservableCollection<Set> SettingSets { get; set; }

		public BunchSets()
		{
			Sets = new List<Set>();
			CommonSets = new ObservableCollection<Set>();
			SettingSets = new ObservableCollection<Set>();
		}

		/// <summary>
		/// Открывает все доступные файлы и преобразует их в наборы.
		/// </summary>
		public void OpenAllSets()
		{
			Sets = Parser.ParseAllFromDirectory();
			AllSetsToCommon();
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
		/// Возвращает информацию о выбранном наборе.
		/// </summary>
		/// <param name="setName">Имя набора</param>
		/// <returns>Строка с содержимым набора.</returns>
		public string GetSetInfo(string setName)
		{
			string result = "";
			Set set = FindSetByName(setName);
			if (set != null)
				result = set.GetSetContent();
			return result;
		}

		/// <summary>
		/// Генерирует случайную ситуацию для наборов текущего сеттинга.
		/// </summary>
		/// <returns>Строка с результатом генерации.</returns>
		public string RandomizeSettingSets()
		{
			Set commonSet = SetsMerge(SettingSets);
			string result = commonSet.RandomizeCharacters();
			return result;
		}
		
		/// <summary>
		/// Перемещает все наборы в общую область.
		/// </summary>
		public void AllSetsToCommon()
		{
			SettingSets.Clear();
			CommonSets.Clear();
			foreach (Set set in Sets)
			{
				CommonSets.Add(set);
			}
		}

		/// <summary>
		/// Перемещает все наборы в область сеттинга.
		/// </summary>
		public void AllSetsToSetting()
		{
			SettingSets.Clear();
			CommonSets.Clear();
			foreach (Set set in Sets)
			{
				SettingSets.Add(set);
			}
		}

		/// <summary>
		/// Перемещает набор в сеттинг.
		/// </summary>
		/// <param name="setName">Имя набора.</param>
		public void SetToSetting(string setName)
		{
			var set = FindSetByName(setName);
			CommonSets.Remove(set);
			SettingSets.Add(set);
		}

		/// <summary>
		/// Перемещает набор в общую область.
		/// </summary>
		/// <param name="setName">Имя набора.</param>
		public void SetToCommon(string setName)
		{
			var set = FindSetByName(setName);
			SettingSets.Remove(set);
			CommonSets.Add(set);
		}

		/// <summary>
		/// Перемещает набор вверх, относительно других наборов в сеттинге.
		/// </summary>
		/// <param name="setName">Имя набора.</param>
		public void MoveSetUp(string setName)
		{
			var set = FindSetByName(setName);
			int setIndex = SettingSets.IndexOf(set);

			if (setIndex > 0)
			{
				SettingSets[setIndex] = SettingSets[setIndex - 1];
				SettingSets[setIndex - 1] = set;
			}
		}

		/// <summary>
		/// Перемещает набор вниз, относительно других наборов в сеттинге.
		/// </summary>
		/// <param name="setName">Имя набора.</param>
		public void MoveSetDown(string setName)
		{
			var set = FindSetByName(setName);
			int setIndex = SettingSets.IndexOf(set);

			if (setIndex < SettingSets.Count - 1)
			{
				SettingSets[setIndex] = SettingSets[setIndex + 1];
				SettingSets[setIndex + 1] = set;
			}
		}

		/// <summary>
		/// Возвращает набор, найденный по имени.
		/// </summary>
		/// <param name="setName">Имя набора.</param>
		/// <returns>Объект набора.</returns>
		public Set FindSetByName(string setName)
		{
			foreach (Set set in Sets)
			{
				if (set.Name == setName)
					return set;
			}
			return null;
		}

		/// <summary>
		/// Возвращает количество доступных наборов.
		/// </summary>
		/// <returns>Целое число количества наборов.</returns>
		public int GetCountSets()
		{
			return Sets.Count;
		}

		// Возвращает набор, как результат слияния списка наборов
		private Set SetsMerge(ObservableCollection<Set> sets)
		{
			Set resultSet = new Set();
			foreach (Set set in sets)
			{
				foreach (Character character in set.Characters)
				{
					var requiredIndex = Parser.FindCharacterFromList(character.Name,
						resultSet.Characters);
					if (requiredIndex == -1)
					{
						var variables = character.Variables;
						var newVariables = variables.GetRange(0, variables.Count);
						var newCharacter = new Character(character.Name, newVariables);
						resultSet.Characters.Add(newCharacter);
					}
					else
					{
						var variables = resultSet.Characters[requiredIndex].Variables;
						variables.AddRange(character.Variables);
					}
				}
			}
			return resultSet;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(prop));
		}
	}
}

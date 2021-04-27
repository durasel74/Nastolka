using System;
using System.Collections.Generic;

namespace Nastolka
{
	public class Set
	{




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
	}
}

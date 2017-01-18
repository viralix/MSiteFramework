using System.Collections.Generic;

namespace ostu
{
	public class Language
	{
		public string Name;
		public List<string> Strings;
		public Language (string name)
		{
			Strings = new List<string> ();
			Name = name;
		}
	}
}


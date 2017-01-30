using System.Collections.Generic;

namespace ostu
{
	public class Language
	{
		public string Name;
		public List<string> Strings;

        public Language (string name, string[] lang)
		{
			Strings = new List<string> (lang);
			Name = name;
		}
	}
}


using System.IO;
using System.Collections.Generic;
namespace MDatabase
{
	public static class Format
	{
		public static void Set(string loc, string key, string value)
		{
			if (!Directory.Exists(loc))
			{
				Directory.CreateDirectory(loc);
			}
			File.WriteAllText(loc + '/' + key,value);
		}
		public static string Get(string loc, string key)
		{
			if (!Directory.Exists(loc))
			{
				Directory.CreateDirectory(loc);
			}
			if (!File.Exists(loc + '/' + key))
			{
				File.WriteAllText(loc + '/' + key, "");
			}
			return File.ReadAllText(loc + '/' + key);
		}
		public static Database Load(string path)
		{
			Database db = new Database();
			db.loc = path;
			string[] file = File.ReadAllLines(path);
			foreach(string a in file)
			{
				db.Add(a);			
			}
			return db;
		}
		public static void Save(Database db, string path)
		{
			List<string> arr = new List<string>();
			foreach(KeyValuePair<string, Table> tab in db.table)
			{
				arr.Add(tab.Key);
			}
			File.WriteAllLines(path, arr.ToArray());
		}
	}
}


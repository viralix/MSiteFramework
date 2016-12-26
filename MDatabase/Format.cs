using System.IO;
using System.Collections.Generic;
namespace MDatabase
{
	public static class Format
	{
		public static Database Load(string path)
		{
			Database db = new Database();
			string[] file = File.ReadAllLines(path);
			foreach(string a in file)
			{
					Table t = new Table();
					string[] table = File.ReadAllLines(a.Split('=')[1]);
					foreach(string b in table)
					{
						t.key.Add(b.Split('=')[0], b.Split('=')[1]);
					}
					db.table.Add(a.Split('=')[0], t);					
			}
			return db;
		}
		public static void Save(Database db, string path)
		{
			int i = 1;
			List<string> arr = new List<string>();
			foreach(KeyValuePair<string, Table> tab in db.table)
			{
				arr.Add(tab.Key + "=" + path + "." + tab.Key);
				__st(tab.Value,  path + "." + tab.Key);
				i++;
			}
			File.WriteAllLines(path, arr.ToArray());
		}
		static void __st(Table tb, string path)
		{
			int i = 1;
			List<string> arr = new List<string>();
			foreach(KeyValuePair<string, string> tab in tb.key)
			{
				arr.Add(tab.Key + "=" + tab.Value);
				i++;
			}
			File.WriteAllLines(path, arr.ToArray());
		}
	}
}


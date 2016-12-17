using System.IO;
using System;
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
				try {
					Table t = new Table();
					string[] table = File.ReadAllLines(a.Split('=')[1]);
					foreach(string b in table)
					{
					try {
						t.key.Add(b.Split('=')[0], b.Split('=')[1]);
					}	catch (Exception){};
					}
					db.table.Add(a.Split('=')[0], t);					
				} catch (Exception){};
			}
			return db;
		}
		public static void Save(Database db, string path)
		{
			int i = 1;
			string[] arr = new string[1];
			foreach(KeyValuePair<string, Table> tab in db.table)
			{
				try {
				string[] oarr = arr;
				arr = new string[i];
				arr = oarr;
				arr[i - 1] = tab.Key + "=" + path + "." + tab.Key;
				__st(tab.Value,  path + "." + tab.Key);
				i++;
				} catch (Exception){};
			}
			File.WriteAllLines(path, arr);
		}
		private static void __st(Table tb, string path)
		{
			int i = 1;
			string[] arr = new string[1];
			foreach(KeyValuePair<string, string> tab in tb.key)
			{
				try {
				string[] oarr = arr;
				arr = new string[i];
				arr = oarr;
				arr[i - 1] = tab.Key + "=" + tab.Value;
				i++;
				} catch (Exception){};
			}
			try {
			File.WriteAllLines(path, arr);
			} catch (Exception){};
		}
	}
}


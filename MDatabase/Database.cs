using System.Collections.Generic;

namespace MDatabase
{
	public class Table
	{
		public string loc;
		public string Get(string key)
		{
			return Format.Get(loc, key);
		}
		public void Set(string key, string value)
		{
			Format.Set(loc, key, value); 
		}
	}
	public class Database
	{
		public string loc;
		public Dictionary<string, Table> table = new Dictionary<string, Table>();
		public Table Get(string Table)
		{
			if (!table.ContainsKey(Table))
			{
				Add(Table);
			}
			return table[Table];
		}
		public void Add(string Table)
		{
			if (!table.ContainsKey(Table))
			{
				Table n = new Table();
				n.loc = loc + "_db/" + Table;
				table.Add(Table, n);
			}
		}
	}
}


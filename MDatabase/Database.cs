using System.Collections.Generic;

namespace MDatabase
{
	public class Table
	{
		public Dictionary<string, string> key = new Dictionary<string, string>();

	}
	public class Database
	{
		public Dictionary<string, Table> table = new Dictionary<string, Table>();
	}
}


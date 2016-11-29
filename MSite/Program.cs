using MSiteFramework;
using System;

namespace MSite
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			try {
				if(args[0] == "config")
				{
					Config.Cfg();
				} else {
					throw new Exception("continue");
				}
			} catch (Exception) {
				try {
					MSiteFramework.Program.Main(args);
				} catch (Exception e) {
					Console.BackgroundColor = ConsoleColor.Red;
					Console.Clear ();
					Console.WriteLine (e);
					Console.ReadKey ();
				}
			}
		}
	}
}

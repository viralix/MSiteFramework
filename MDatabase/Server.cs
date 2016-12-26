using System;
using System.IO;
using System.Threading;
namespace MDatabase
{
	public class Server
	{
		private static string savejob = "null";
		public static string dbpath = "null";
		public static bool dbrun = true;
		public static int dbsleep;
		public static bool initdb = false;
		// Database
		public static Database db = new Database();
		// Force save method (still has to wait)
		public static void save()
		{
			savejob = "save";
		}

		// Raw Load Method that is private
		static private void __fload()
		{
			db = Format.Load(dbpath);
			initdb = true;
		}


		// Raw save method that is private
		static private void __fsave()
		{
			Format.Save(db, dbpath);
		}

		// Database host thread
		public static void host(string path, int sleep)
		{
			dbpath = path;
			dbsleep = sleep;
			if (File.Exists(dbpath))
			{
				__fload();
			}
			while (dbrun)
			{
				Console.WriteLine("Saving database...");
				try
				{
					__fsave();
					Console.WriteLine("Database saved!");
				}
				catch (Exception e)
				{
					Console.WriteLine("Database saving error: " + e.Message);
				}
				savejob = "null";
				int t = 0, s = 0;
				while ((savejob != "save") && (t < 100))
				{
					Thread.Sleep(100);
					s += 100;
					t = (int)((s / (float)dbsleep) * 100);
				}
			}
		}
	}
}

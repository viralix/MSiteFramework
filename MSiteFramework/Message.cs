using System;
 // Messages
namespace MSiteFramework
{
	public static class Message
	{
		public static void cr_co(int MaxCrash, int i, Exception e, int x)
		{
			if (x == 0)
			{
				Console.BackgroundColor = ConsoleColor.Red;
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine("Crashed the maximal amount of times allowed ({1}/{0}).", MaxCrash, i);
				Console.WriteLine("Set the crash parameter to -1 in the configuration file to disable this feature.");
				Console.ResetColor();
			}
			else if (x == 1)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Server has crashed: {0}", e.Message);
				Console.ResetColor();
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("Restarting server... {1}/{0}", MaxCrash, i);
				Console.ResetColor();
			}
			else {
				Console.WriteLine("Unknown message!");
			}
		}
		public static void ex_lc(Exception e, int id, int cid)
		{
			Console.ForegroundColor = __color(cid);
			switch (id)
			{
				case 0:
					Console.WriteLine("Error while trying to open config file: {0}", e.Message);
					break;
				case 1:
					Console.WriteLine("Your server is not verified.");
					Console.WriteLine("Please go to: http://" + Program.Hostname + ":{0}/verify/", Program.Port);
					break;
				case 2:
					Console.WriteLine("A new update has been found!");
					Console.WriteLine("Please go to: https://github.com/mihail-rotmg/MSiteFramework/releases");
					break;
				case 3:
					Console.WriteLine(e.Message);
					break;
				case 4:
					Console.WriteLine("{0} v{1}", Program.name, Program.version);
					Console.WriteLine();
					break;
				default:
					Console.WriteLine("Unknown error: {0}", e.Message);
					break;
			}
			Console.ResetColor();
		}
		private static ConsoleColor __color(int cid)
		{
			switch (cid)
			{
				case 0:
					return ConsoleColor.Red;
				case 1:
					return ConsoleColor.Yellow;
				case 2:
					return ConsoleColor.Green;
				case 3:
					return ConsoleColor.Cyan;
				default:
					return ConsoleColor.White;
			}
		}
	}
}

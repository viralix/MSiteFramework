using MSiteFramework;
using System;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Reflection;

namespace MSite
{
	class MainClass
	{
		public static void Main (string[] args)
        {
            Process[] y = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location));
            if (y.Count() > 1)
            {
                foreach (Process pr in y)
                { if (pr.Id != Process.GetCurrentProcess().Id) { pr.Kill(); } }
            }
            try
            {
                if (args[0] == "config")
                {
                    Config.Cfg();
                }
                else if (args[0] == "no-gui")
                {
                    try
                    {
                        Program.Main();
                    }
                    catch (Exception e)
                    {
                        Console.SetOut(Console.Out);
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        Console.WriteLine(e);
                    }
                }
                else
                {
					throw new Exception("continue");
				}
			} catch (Exception) {
                try
                {
                    Console.SetOut(Console.Out);
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Clear();
                    Console.WriteLine("Running as a Window Application...");
                    Console.WriteLine("If you close this window, the server will stop.");
                    Application.EnableVisualStyles();
                    Application.Run(new Main());
                }
                catch (Exception)
                {
                    Application.Restart();
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
            }
		}
	}
}

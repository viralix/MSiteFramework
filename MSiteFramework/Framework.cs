using System;
using SimpleHttpServer;
using System.Threading;
using MSiteDLL;
using Secure.Verify;
using System.IO;
using MDatabase;

namespace MSiteFramework
{
    public static class Program
    {
		static bool initdb = false;
		public static bool cupdate;
		public static bool uphpsh;
		public static string phpsh;
        public static bool fphp;
        public static string php;
        public static int Build;
        public static string Hostname;
        public static string Index;
        public static string Host;
        public static int Port;
        public static string name = "MSiteFramework";
        public static string version = "1.0.1";
        public static string verified = "null";
        public static int MaxCrash;
        public static string allowIndex = "true";
        public static string dbpath = "db/data";
        public static int dbsleep = 600000;
        public static Database db = new Database();
		private static string savejob = "null";
        public static void Main()
        {
            fphp = false;
            php = "disable";
            Hostname = "localhost";
            MaxCrash = 2;
            Port = 80;
            Build = -1;
            Index = "index.html";
            Host = "html";
			uphpsh = false;
			phpsh = "php.sh";
			cupdate = true;

            string file = "config.m";

            try
            {
                verified = File.ReadAllText("verified");
            } catch (Exception){}
            Console.Title =  name+" Server"+lc(verified, version);
            try
            {
                Host = Prop.Get(file, "html");
                Port = int.Parse(Prop.Get(file, "port"));
                Index = Prop.Get(file, "index");
                MaxCrash = int.Parse(Prop.Get(file, "crash"));
                allowIndex = Prop.Get(file, "dirlist");
                Hostname = Prop.Get(file, "hostname");
                Build = int.Parse(Prop.Get(file, "build"));
                fphp = bool.Parse(Prop.Get(file, "fphp"));
                php = Prop.Get(file, "php");
				uphpsh = bool.Parse(Prop.Get(file, "uphpsh"));
				phpsh = Prop.Get(file, "phpsh");
				cupdate = bool.Parse(Prop.Get(file, "cupdate"));
				dbpath = Prop.Get(file, "dbpath");
				dbsleep = int.Parse(Prop.Get(file, "dbsleep"));

            } catch (Exception e) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error while trying to open config file: {0}", e.Message);
                Console.ResetColor();
            }

            version = version + ".x["+Build+"]";

            if (verified.ToLower() != "true")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Your server is not verified.");
                Console.WriteLine("Please go to: http://"+Hostname+":{0}/verify/", Port);
                Console.ResetColor();
            } else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("{0} v{1}",name,version);
                Console.WriteLine();
                Console.ResetColor();
            }

			if (verified.ToLower() == "true" && cupdate == true)
            {
                if (Update.Check(Build))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("A new update has been found!");
                    Console.WriteLine("Please go to: https://github.com/mihail-rotmg/MSiteFramework/releases");
                    Console.ResetColor();
                } else {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Up to date!");
                    Console.ResetColor();
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Listening on port {0}...", Port);
            Console.ResetColor();
            Thread th = new Thread(new ThreadStart(__dbs));
            Thread thread = new Thread(new ThreadStart(StartServer));
            th.Start();
            thread.Start();
        }

		public static void save()
		{
			savejob = "save";
		}
		static private void __fload()
		{
			db = Format.Load(dbpath);
			initdb = true;
		}

		static private void __fsave()
		{
			Format.Save(db, dbpath);
			__fload ();
		}

        private static void __dbs()
        {
			if (File.Exists (dbpath)) {
				__fload ();
			}
            while(true) 
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Saving database...");
                Console.ResetColor();
				__fsave();
                try {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Database saved!");
                Console.ResetColor();
                } catch (Exception e) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Database saving error:", e.Message);
                Console.ResetColor();
                }
				savejob = "null";
				int t = 0, s = 0;
				while ((savejob != "save") && (t < 100)) {
					Thread.Sleep(100);
					s += 100;
					t = (int)((float)((float)s/(float)dbsleep) * (float)100);
				}
            }
        }
        private static string lc(string verified, string version)
        {
            if (verified.ToLower() == "true")
            {
                return " v"+version;
            } else
            {
                return " (NOT VERIFIED)";
            }
        }

        public static void StartServer()
        {
			while (!initdb) {
				Thread.Sleep (100);
			}
            if(!db.table.ContainsKey("session"))
            {
                db.table.Add("session", new Table());
            }
			if (!db.table ["session"].key.ContainsKey ("now")) {
				db.table ["session"].key.Add ("now", DateTime.Now.ToString ());
			} else {
				Console.WriteLine("Last started on: {0}", db.table["session"].key ["now"]);
				db.table ["session"].key["now"] = DateTime.Now.ToString ();
			}
			save ();
			int i = 0;
            HttpServer httpServer = new HttpServer(Port, Routes.GET);
            Start:
            if((i>=MaxCrash) && (MaxCrash!=-1))
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Crashed the maximal amount of times allowed ({1}/{0}).", MaxCrash, i);
                Console.WriteLine("Set the crash parameter to -1 in the configuration file to disable this feature.");
                Console.ResetColor();
                return;
            }
            i++;
            try
            {
                httpServer.Listen();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Server shutting down!");
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Server has crasehd: {0}", e.Message);
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Restarting server... {1}/{0}", MaxCrash, i);
                Console.ResetColor();
                goto Start;
            }
        }
    }
}
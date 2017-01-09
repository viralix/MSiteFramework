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
		// Variables
		static string engine;
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
        public static string name;
        public static string version;
        public static string verified;
        public static int MaxCrash;
        public static string allowIndex;
        public static string dbpath;
		public static int dbsleep;
		public static void Init()
		{
			// Define static data
			dbpath = "db/data";
			dbsleep = 600000;
			allowIndex = "true";
			name = "MSiteFramework";
			version = "1.0.1";
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
			// Default Engine
			engine = "mse1_0.dll";
			try
			{
				// Load verification information
				verified = File.ReadAllText("verified");
			}
			catch (Exception)
			{
				// Not verified
				verified = "null";
			}
			Console.Title = name + " Server" + lc(verified, version);
		}

		public static void Load(string file)
		{
			// Cancer
			engine = Prop.Get(file, "engine");
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
			// Add the build number
			version += "." + Build;
		}

        public static void Main()
        {

			// Initialize the variables
			Init();

			// Load the variables from configuration file
            try
            {
				Load("config.m");

            } catch (Exception e) {
				Message.ex_lc(e, 0, 0);
			}

            if (verified.ToLower() != "true")
            {
				Message.ex_lc(new Exception(), 1, 1);
            } else
            {
				Message.ex_lc(new Exception(), 4, 3);
            }

			if (verified.ToLower() == "true" && cupdate == true)
            {
                if (Update.Check(Build))
                {
                    Message.ex_lc(new Exception(), 2, 1);
                } else {
					Message.ex_lc(new Exception("Up to date!"), 3, 2);
                }
            }

			Message.ex_lc(new Exception("Listening on port "+Port+"..."), 3, 2);

			// Actually start the server

			// Threads
			Thread dbHost = new Thread(() => Server.host(dbpath, dbsleep));
            Thread server = new Thread(new ThreadStart(StartServer));

			// Start the threads
            dbHost.Start();
            server.Start();
        }



		// Version and verification
        private static string lc(string ver, string vers)
        {
            if (ver.ToLower() == "true")
            {
                return " v"+vers;
            } else
            {
                return " (NOT VERIFIED)";
            }
        }


		// Start server thread
        public static void StartServer()
        {
			// If database isn't loaded, sleep.
			while (!Server.initdb) {
				Thread.Sleep (100);
			}

			// Check for last session and print it out.
			Console.WriteLine("Last session: {0}", Server.db.Get("session").Get("now"));
			Server.db.Get("session").Set("now",DateTime.Now.ToString());

			// Save the current session.
			Server.save ();

			// Crash amount
			int i = 0;

			// Init the server now.
			HttpServer httpServer;


			// Load the HTTP server from engine
			Console.WriteLine("Using engine: {0}", engine);
			switch (engine)
			{
				default:
					httpServer = new LoadSite(engine).Server(Port);
					break;
			}

			// Check if max crash have been made
            Start:
            if((i>=MaxCrash) && (MaxCrash!=-1))
            {
				Message.cr_co(MaxCrash, i, new Exception(), 0);
                return;
            }

			// Try running the server
            try
            {
				// Actually start the server.
                httpServer.Listen();
				// Server peacefully shuts down.
				Message.ex_lc(new Exception("Server shutting down!"), 3, 1);
				return;
            }
            catch (Exception e)
            {
				// Add to error count
				i++;
				// Show message that the server has crashed
				Message.cr_co(MaxCrash, i, e, 1);
				// Restart server
                goto Start;
            }
        }
    }
}

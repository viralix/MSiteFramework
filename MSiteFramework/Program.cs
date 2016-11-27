using System;
using SimpleHttpServer;
using System.Threading;
using MSiteDLL;
using System.IO;

namespace MSiteFramework
{
    public static class Program
    {
        public static string Index;
        public static string Host;
        public static int Port;
        public static string name = "MSiteFramework";
        public static string version = "1.0.0";
        public static string verified = "null";
        public static int MaxCrash;
        public static string allowIndex = "true";

        public static void Main(string[] args)
        {
            try
            {
                verified = File.ReadAllText("verified");
            } catch (Exception){}
            MaxCrash = 50;
            Port = 80;
            Index = "index.html";
            Host = "html";
            string file = "config.m";
            try
            {
                file = args[0];
            } catch (Exception) {}
            Console.Title =  name+" Server"+lc(verified, version);
            try
            {
                Host = Prop.Get(file, "html");
                Port = int.Parse(Prop.Get(file, "port"));
                Index = Prop.Get(file, "index");
                MaxCrash = int.Parse(Prop.Get(file, "crash"));
                allowIndex = Prop.Get(file, "dirlist");

            } catch (Exception e) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error while trying to open config file: {0}", e.Message);
                Console.ResetColor();
            }
            if (verified.ToLower() != "true")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Your server is not verified.");
                Console.WriteLine("Please go to: http://localhost:{0}/verify/", Port);
                Console.ResetColor();
            } else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("{0} v{1}",name,version);
                Console.WriteLine();
                Console.ResetColor();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Listening on port {0}...", Port);
            Console.ResetColor();
            Thread thread = new Thread(new ThreadStart(StartServer));
			if (verified.ToLower() != "true")
                System.Diagnostics.Process.Start("http://localhost:"+Port.ToString()+"/verify/");
            thread.Start();
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
                Console.ReadKey(true);
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
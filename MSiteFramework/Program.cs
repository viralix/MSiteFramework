using System;
using SimpleHttpServer;
using System.Threading;
using MSiteDLL;

namespace MSiteFramework
{
    public static class Program
    {
        public static string Index;
        public static string Host;
        public static int Port;
        public static string name = "MSiteFramework";
        public static string version = "1.0.0";
        public static int MaxCrash;

        public static void Main(string[] args)
        {
            MaxCrash = 50;
            Port = 80;
            Index = "index.html";
            Host = "html";
            string file = "config.m";
            try
            {
                file = args[0];
            } catch (Exception) {}
            try
            {
                Host = Prop.Get(file, "html");
                Port = int.Parse(Prop.Get(file, "port"));
                Index = Prop.Get(file, "index");
                MaxCrash = int.Parse(Prop.Get(file, "crash"));

            } catch (Exception e) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error while trying to open config file: {0}", e.Message);
                Console.ResetColor();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Listening on port {0}...", Port);
            Console.ResetColor();
            Thread thread = new Thread(new ThreadStart(StartServer));
            thread.Start();
        }
        public static void StartServer()
        {
            int i = 0;
            HttpServer httpServer = new HttpServer(Port, Routes.GET);
            Start:
            if((i>=MaxCrash) || (MaxCrash==-1))
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
using System;
using MSiteFramework;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace MSite
{
    public partial class Main : Form
    {
        static Form cfg = new Configu();
        static TextWriter _writer = null;
        static Thread server;
        static string[] args = new string[1];
        public Main()
        {
            InitializeComponent();
            Config.Click += new System.EventHandler(Config_Click);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            args[0] = "config.m";
            server = new Thread(srv);
            server.IsBackground = true;
            _writer = new TextBoxStreamWriter(ConsoleText, this);
            Console.SetOut(_writer);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartButton.Enabled = false;
            try
            {
                server.Start();

            }
            catch (Exception)
            {
                Console.WriteLine("Server is already running!");
            }
        }

        private void srv()
        {
            Program.Main(args);
        }

        public static void Config_Click(object sender, EventArgs e)
        {
            cfg.ShowDialog();
        }

        private void restart_Click(object sender, EventArgs e)
        {
            Application.Restart();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}

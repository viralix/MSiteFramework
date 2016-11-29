using System;

namespace Secure.Verify
{
    public static class Update
    {
        public static bool Check(int build)
        {
            return (Current() > build);
        }
        public static int Current()
        {
            try {
                using (var wc = new System.Net.WebClient())
                return int.Parse(wc.DownloadString("https://raw.githubusercontent.com/mihail-rotmg/MSiteFramework/master/build"));
            } catch (Exception)
            {
                return -1;
            }
        }
    }
}
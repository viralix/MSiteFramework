using System.Net;

namespace MSiteDLL
{
    public static class DNS
    {
        public static string[] Resolve(string hostname)
        {
            IPAddress[] addresslist = Dns.GetHostAddresses(hostname);
            string x = ""; int i = 0;
            foreach (IPAddress theaddress in addresslist)
            {
                if (i != 0)
                {
                    x += "&" + theaddress.ToString();
                }
                else
                {
                    x = theaddress.ToString();
                }
                i++;
            }
            return x.Split('&');
        }
    }
}

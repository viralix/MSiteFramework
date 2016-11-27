using MSiteDLL;
using System;

namespace MSiteFramework.Files
{
    internal class Verify
    {
        internal static Document Generate(Data server)
        {

            bool Allow = false;

            string id = server.Get("host");

            try
            {
                string[] ips = DNS.Resolve(id);

                foreach (string ip in ips)
                {
                    if (check(ip))
                    {
                        Allow = true;
                        break;
                    }
                }
            } catch(Exception)
            {
                Allow = false;
            }

            Element[] head = {
                new Element("version", Program.version),
                new Element("name", Program.name),
            };

            Element[] body = {
                new Element("licensed", Allow.ToString()),
            };
            Block[] blocks = {
                    new Block("head", head),
                    new Block("body", body),
                };
            return new Document(blocks);
        }

        private static bool check(string ip)
        {
            string contents;
            using (var wc = new System.Net.WebClient())
                contents = wc.DownloadString("http");
        }
    }
}
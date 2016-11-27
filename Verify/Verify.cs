using MSiteDLL;
using System;

namespace Secure.Verify.Files
{
    public class Verify
    {
        public static Document Generate(Data server)
        {

            bool Allow = false;

            string id = server.Get("host");

            if (id != Prop.Get("config.m","hostname"))
            {
                Allow = false;
            } else {
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
            }
            Element[] head = {
                new Element("title","Verification"),
            };

            Element[] body = {
                new Element("div", Allow.ToString(), "id=licensed style=\"display: none; \""),
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
            try
            {
                using (var wc = new System.Net.WebClient())
                    contents = wc.DownloadString("https://raw.githubusercontent.com/mihail-rotmg/MSiteFramework/master/Verify/list");
                string[] lines = contents.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string line in lines)
                {
                    if (line.Split('=')[0] == ip)
                    {
                        bool x, y;
                        y = bool.TryParse(line.Split('=')[1],out x);
                        if (x!=y)
                        {
                            return false;
                        } else
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception) {
                try
                {
                    contents = Info.Load();
                    string[] lines = contents.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    foreach (string line in lines)
                    {
                        if (line.Split('=')[0] == ip)
                        {
                            bool x, y;
                            y = bool.TryParse(line.Split('=')[1], out x);
                            if (x != y)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                } catch (Exception) { return false; }
            }
        }
    }
}
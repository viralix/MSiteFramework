using MSiteDLL;
using System;
using System.IO;

namespace Secure.Verify.Files
{
    public class VerifyHandle
    {
		public static bool licVerify(string name)
		{
			switch (name.ToUpper()) {
			case "46DG-RW45-675G-86G4":
			case "SDF6-H453-GD4T-DG54":
			case "634F-FGSD-DF45-675K":
			case "F746-GD6H-DGS5-XC6E":
			case "CJHR-GBD5-JG34-D5SA":
			case "FA33-FA56-HDS4-GA4T":
			case "VXTG-UFH7-57GF-GY47":
				return true;
			default:
				return false;
			}
		}

        public static Document Generate(Data server)
        {
            Element[] head = {
             new Element("title", "Results"),
             new Empty(""),
            };
            string content, subc, id = "", lic = "";
            try
            {
                foreach (string s in server.post.Split('&'))
                {
                    string[] x = s.Split('=');
                    if (x[0] == "id")
                    {
                        id = x[1];
                        break;
                    }
				}
				foreach (string s in server.post.Split('&'))
				{
					string[] x = s.Split('=');
					if (x[0] == "key")
					{
						lic = x[1];
						break;
					}
				}
                if (bool.Parse(id))
                {
                    content = "Verification passed.";
                    subc = "This server has been verified.";
                    File.WriteAllText("verified", "true");
                    try
                    {
                        Directory.Delete(server.Location("html").Replace("handle.m", ""), true);
                        head[1] = new Element("meta", "", "http-equiv=\"refresh\" content=\"3;URL='../'\"");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Verified successfully, please restart the server.");
                        Console.ResetColor();
                    }
                    catch (Exception)
                    {
                        subc = "This server has been verified.</p><p>Feel free to delete the /html/verify/ folder.</p>";
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Verified successfully!");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Please delete the /html/verify folder and restart the server.");
                        Console.ResetColor();
                    }

                }
                else
                {
					if (licVerify(lic))
					{
						content = "Verification passed.";
						subc = "This server has been verified.";
						File.WriteAllText("verified", "true");
						try
						{
							Directory.Delete(server.Location("html").Replace("handle.m", ""), true);
							head[1] = new Element("meta", "", "http-equiv=\"refresh\" content=\"3;URL='../'\"");
							Console.ForegroundColor = ConsoleColor.Green;
							Console.WriteLine("Verified successfully, please restart the server.");
							Console.ResetColor();
						}
						catch (Exception)
						{
							subc = "This server has been verified.</p><p>Feel free to delete the /html/verify/ folder.</p>";
							Console.ForegroundColor = ConsoleColor.Green;
							Console.WriteLine("Verified successfully!");
							Console.ForegroundColor = ConsoleColor.Yellow;
							Console.WriteLine("Please delete the /html/verify folder and restart the server.");
							Console.ResetColor();
						}
					} else {
                    	content = "Verification Failed.";
                    	subc = "Your IP address is not registered in our database.";
					}
                }
            }
            catch (Exception)
            {
                content = "Verification Error";
                subc = "Please check if you are running the correct version or if you have internet connection.";

            }
            Element[] body = {
    new Element("h1", content),
    new Element("h4", subc),
};
            Block[] blocks = {
                    new Block("head", head),
                    new Block("body", body),
                    };
            return new Document(blocks);
        }
    }
}
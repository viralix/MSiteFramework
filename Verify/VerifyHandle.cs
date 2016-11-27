using MSiteDLL;
using System;
using System.IO;

namespace Secure.Verify.Files
{
    public class VerifyHandle
    {
        public static Document Generate(Data server)
        {
            Element[] head = {
             new Element("title", "Results"),
             new Element("meta",""),
            };
            string content, subc, id = "";
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
                        subc = "This server has been verified.</p><p>The product has been verified, feel free to delete the /html/verify/ folder.</p>";
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Verified successfully!");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Please delete the /html/verify folder and restart the server.");
                        Console.ResetColor();
                    }

                }
                else
                {
                    content = "Verification Failed.";
                    subc = "Your IP address is not registered in our database.";
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
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
                    }
                    catch (Exception)
                    {
                        subc = "This server has been verified.</p><p>The product has been verified, feel free to delete the /html/verify/ folder.</p>";
                        File.WriteAllText("verified", "true");
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
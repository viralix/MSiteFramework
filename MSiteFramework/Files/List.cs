using MSiteDLL;
using System.IO;
namespace MSiteFramework.Files
{
    public static class List
    {
        public static Document Generate(Data server)
        {
            if (Program.allowIndex != "true")
            {
                return Error.Generate(new System.Exception("NOT FOUND"), false);
            }
            string[] y,z;
            string x = "",loc = server.Location(Program.Host);
            string url = server.request.Url;
            y = Directory.GetDirectories(loc);
            z = Directory.GetFiles(loc);
            if (url != "/")
            {
                x += "<p>.. <a href=\"../\">></a></p>";
            }
            foreach(string yx in y)
            {
                x += "<p>+ "+yx+" <a href=\""+yx+"/\">></a></p>";
            }
            foreach (string zx in z)
            {
                x += "<p>- " + zx + " <a href=\"" + zx + "\">></a></p>";
            }
            Element[] head = {
                new Element("style",@"
a {
color: red;
text-decoration: none;
}
                "),
                new Element("title", "Index of " + url),
            };

            Element[] body = {
                new Element("h1",  "Index of " + url),
                new Element("div", x.Replace(loc, "")),
				new Element("h4", Program.name + " v" + Program.version),
            };

            Block[] blocks = {
                    new Block("head", head),
                    new Block("body", body),
                };

            return new Document(blocks); ;
        }
    }
}
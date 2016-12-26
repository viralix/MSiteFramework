using MSiteDLL;
using System;
using MSiteFramework;
namespace mse1_0.Files
{
    public static class Error
    {
        public static Document Generate(Exception e, bool draw=true)
        {

            Element[] head = {
                new Element("title", "Internal Server Error"),
            };

            Element[] body = {
                new Element("h1", e.Message),
                new Element("code", e.ToString()),
                new Element("h4", Program.name + " v" + Program.version),
            };

            Block[] blocks = {
                    new Block("head", head),
                    new Block("body", body),
                };

            Document site = new Document(blocks);
            if(draw)
            site.Write();

            return site;
        }
    }
}
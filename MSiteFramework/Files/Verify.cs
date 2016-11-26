using System;
using MSiteDLL;

namespace MSiteFramework.Files
{
    internal class Verify
    {
        internal static Document Generate(Data server)
        {

            bool Allow = false;

            string id = server.Get("code");



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
    }
}
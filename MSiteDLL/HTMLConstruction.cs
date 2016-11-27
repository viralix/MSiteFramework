using SimpleHttpServer.Models;
using System;
using System.Linq;

namespace MSiteDLL
{
    public class Data
    {
        public HttpRequest request;
        public string get;
        public bool hasData;
        public string address;
        public string post;
        public Data(HttpRequest Request)
        {
            request = Request;
            hasData = false;
            post = "";
            get = "";
        }

        public string Get(string id)
        {
            string key = "";
            foreach (string s in get.Split('&'))
            {
                if (s.Split('=').Count() > 1)
                {
                    if (s.Split('=')[0] == id)
                    {
                        key = s.Split('=')[1];
                        break;
                    }
                }
            }
            return key;
        }

        public void SetGet(string url)
        {
            address = url.Split('?')[0];
            try
            {
                get = url.Split('?')[1];
            } catch(Exception)
            {
            }
        }
        public string Location(string Host)
        {
            try {
                return Host+address;
            } catch (Exception)
            {
                return Host+"/index.html";
            }
        }
    }
    public class Document
    {
        Block[] Blocks;
        public Document(Block[] blocks)
        {
            Blocks = blocks;
        }
        public void Write()
        {
            Site.Tag("html");
            foreach(Block block in Blocks)
            {
                block.Write();
            }
            Site.Tag("html", false);
        }
    }
    public class Block
    {
        Element[] Elements;
        string ID;
        public Block(string id, Element[] elements)
        {
            Elements = elements;
            ID = id;
        }
        public void Write()
        {
            Site.Tag(ID);
            foreach(Element element in Elements)
            {
                element.Write();
            }
            Site.Tag(ID, false);
        }
    }
    public class Element
    {
        string Tag;
        string Text;
        string Args;
        public Element(string tag, string text, string args = null)
        {
            Tag = tag;
            Text = text;
            Args = args;
        }
        public void Write()
        {
            if(Args == null)
            {
                Site.Text(Tag, Text);
            } else {
                Site.Tag(Tag, true, Args);
                Site.Write(Text);
                Site.Tag(Tag,false);
            }
        }
    }
}
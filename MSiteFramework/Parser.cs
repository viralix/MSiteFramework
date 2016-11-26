using System;
using MSiteFramework.Files;
using System.Reflection;
using System.Diagnostics;
using MSiteDLL;

namespace MSiteFramework
{
    public static class Parser
    {
        public static Document Generate(Data server)
        {
            Prop file = new Prop(server.Location(Program.Host));
            string _mode = file.Str("mode");
            string _class = file.Str("class");
            string _allow = file.Str("allow");
            switch(_mode)
            {
                case "class":
                    return ClassMode(server, _class, _allow, server);
                case "list":
                    return ListMode(server, file, _allow, server);
                default:
                    return Error.Generate(new Exception("Mode not specified."),false);
            }
        }

        public static Document ListMode(Data server, Prop file, string _allow, Data site)
        {
            if (_allow == "false")
            {
                return Error.Generate(new Exception("Not allowed."), false);
            }
            else
            {
                string _list = file.Str("list");
                if (_list == "none")
                {
                    return Error.Generate(new Exception("No lists were found."), false);
                }
                else
                {
                    string[] lists = _list.Split(',');
                    Block body = Prs("body", file, site), head = Prs("head", file, site);
                    Block[] blocks = {
                        head,
                        body,
                    };
                    return new Document(blocks);
                }
            }
        }
        public static Block Prs(string type,Prop file, Data site)
        {
            int x = 0, h = 0;
            string[] _head = file.Str(type).Split(',');
            x++;
            foreach (string s in _head) { h++; };
            Element[] he = new Element[h];
            h = 0;
            foreach (string s in _head)
            {
                string text;
                string[] tmp = file.Str(s).Split('|');
                string tag = tmp[0]; string ids;
                if (tag.StartsWith("x:"))
                {
                    if (tmp[1] == "null")
                    {
                        ids = null;
                    }
                    else
                    {
                        ids = tmp[1];
                    }
                    text = tmp[2];
                }
                    else
                {
                        text = Exec(tmp);
                        tag = "null";
                        ids = null;
                }
                he[h] = new Element(tag, text, ids);
                h++;
            }
            return new Block(type, he);
        }

        private static string Exec(string[] x)
        {
            string output = "",exe = "",args = "";
            int i = 0;
            foreach(string arg in x)
            {
                if(i == 0)
                {
                    exe = arg;
                } else
                {
                    if (i == 1)
                    {
                        args = "\"" + arg + "\"";
                    } else
                    {
                        args += " \"" + arg + "\"";
                    }
                }
                i++;
            }
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = Environment.CurrentDirectory + @"/bins/" +exe;
            p.StartInfo.Arguments = args;
            p.Start();
            output = p.StandardOutput.ReadToEnd();
            output += p.StandardError.ReadToEnd();
            p.WaitForExit();
            return output;
        }

        public static Document ClassMode(Data server, string _class, string _allow, Data site)
        {
            if(_allow == "false")
            {
                return Error.Generate(new Exception("Not allowed."),false);
            } else {
                Type type = typeof(Class);
                MethodInfo method = type.GetMethod(_class);
                Class c = new Class(site);
                return (Document)method.Invoke(c, null);
            }
        }
    }
}
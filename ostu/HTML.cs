using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ostu
{
    public class HTMLWriter
    {
        string tag;
        public HTMLWriter(string tag)
        {
            this.tag = tag;
            buffer.AddRange("<" + tag + ">\n");
        }
        List<char> buffer = new List<char>();

        public void Write(string c)
        {
            buffer.AddRange(c);
        }

        public void WriteLine(string c)
        {
            buffer.AddRange(c + "\n");
        }

        public char[] ToArray()
        {
            buffer.AddRange("</" + tag + ">\n");
            return buffer.ToArray();
        }
    }
    public class HTML
    {
        public HTMLWriter head = new HTMLWriter("head");
        public HTMLWriter body = new HTMLWriter("body");

        Language iLang;

        public HTML(Language lang)
        {
            iLang = lang;
        }

        public override string ToString()
        {
            List<char> buffer = new List<char>();
            buffer.AddRange("<!DOCTYPE HTML>\n<html lang=\""+iLang.Name+"\">\n");
            buffer.AddRange(head.ToArray());
            buffer.AddRange(body.ToArray());
            buffer.AddRange("</html>\n");
            string content = "";
            for (int i = 0; i < buffer.Count; i++)
            {
                if (buffer[i] == '{' && buffer[i+1] == '$')
                {
                    i += 2;
                    string c = "";
                    while(buffer[i] != '}')
                    {
                        c += buffer[i++];
                    }
                    int r = 0;
                    if (int.TryParse(c, out r))
                        if (r < iLang.Strings.Count)
                            content += iLang.Strings[r];
                    continue;
                }
                content += buffer[i];
            }
            return content;
        }
    }
}

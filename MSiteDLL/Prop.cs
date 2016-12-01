using System;
using System.IO;

namespace MSiteDLL
{
    public class Prop
    {
        string[] lines;
        public Prop(string path)
        {
            lines = File.ReadAllLines(path);
        }
        public string Str(string name)
        {
            string ret = "none";
            foreach (string line in lines)
            {
                try
                {
                    string[] i = line.Split('~');
                    if (i[0] == name) { ret = i[1]; }
                }
                catch (Exception) { }
            }
            return ret;
        }
        public static string Get(string path, string name)
        {
            string[] lines = File.ReadAllLines(path);
            string ret = "none";
            foreach (string line in lines)
            {
                try
                {
                    string[] i = line.Split('~');
                    if (i[0] == name) { ret = i[1]; }
                }
                catch (Exception) { }
            }
            return ret;
        }
    }
}
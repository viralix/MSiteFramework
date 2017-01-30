using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ostu
{
    public class URL
    {
        Uri _inter;

        public URL(string url)
        {
            _inter = new Uri(url);

        }

        public string file()
        {
            if (File.Exists("html" + _inter.LocalPath))
                return File.ReadAllText("html" + _inter.LocalPath);
            else
                throw new Exception("File not found.");
        }

        public bool hasExt(string ext)
        {
            if (_inter.LocalPath.EndsWith("." + ext))
                return true;
            return false;
        }

        public Uri raw()
        {
            return _inter;
        }

        public bool exists()
        {
            if (File.Exists("html" + _inter.LocalPath))
                return true;
            return false;
        }
    }
}

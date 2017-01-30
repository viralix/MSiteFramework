using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ostu.Languages
{
    public class English : Language
    {
        public English() : base("en", new string[]
        {
                "Choose a language",
                "O.S.Т.U Gostivar",
                "Language choice",
                "Welcome to our web site",
                "Error :(",
                "An error has occured while trying to load the page.",
                "Error message"
        })
        { }
    }
}

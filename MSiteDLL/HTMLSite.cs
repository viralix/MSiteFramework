namespace MSiteDLL
{
    public static class Site
    {
        public static string Page { get; private set; }

        public static void Write(string text)
        {
            Page += text;
        }
        public static void Text(string tag, string content)
        {
            Tag(tag);
            Write(content);
            Tag(tag,false);
        }
        public static void Tag(string tag, bool open = true, string args = null)
        {
            string xarg = "", output = "<";
            if (args != null)
                xarg = " " + args;
            if(open == false)
            {
                output += "/";
            }
            output += tag + xarg;
            output += ">";
            Write(output);
        }

        public static void Reset()
        {
            Page = " ";
        }
    }
}
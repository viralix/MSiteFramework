using MSiteDLL;

namespace MSiteFramework.Files
{
    public class Class
    {
        private Data site;

        public Class(Data Site)
        {
            site = Site;
        }
        public Document Error()
        {
            return Files.Error.Generate(new System.Exception("Forced error."), false);
        }
        public Document Verify()
        {
            return Files.Verify.Generate(site);
        }
    }
}

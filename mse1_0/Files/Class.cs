using MSiteDLL;

namespace mse1_0.Files
{
    public class Class
    {
        private Data site;

        public Class(Data Site)
        {
            site = Site;
        }
        public Document List()
        {
            return Files.List.Generate(site);
        }
        public Document Error()
        {
            return Files.Error.Generate(new System.Exception("Forced error."), false);
        }
        public Document Verify()
        {
            return Secure.Verify.Files.Verify.Generate(site);
        }
        public Document VerifyHandle()
        {
            return Secure.Verify.Files.VerifyHandle.Generate(site);

        }
    }
}

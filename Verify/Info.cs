using System.IO;
using System.Reflection;

namespace Secure.Verify
{
    public class Info
    {
        public static string Load()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Verify.list";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}

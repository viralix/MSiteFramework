using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using MSiteDLL;
using System.IO;

namespace MSiteFramework
{
    public static class Script
    {
        public static Document Execute(string script, string file, Data server)
        {
            string source = Head(file)+@"
namespace SimpleScript
{
    public class Generator
    {
        public static Document Generate(Data server)
        {
            "+script+ @"
                    Block[] blocks = {
                    new Block(""head"", head),
                    new Block(""body"", body),
                    };
            return new Document(blocks);
        } 
    }
}

";
            Dictionary<string, string> providerOptions = new Dictionary<string, string>
                {
                    {"CompilerVersion", "v4.0"}
                };
            CSharpCodeProvider provider = new CSharpCodeProvider(providerOptions);

            CompilerParameters compilerParams = new CompilerParameters
            {
                GenerateInMemory = true,
                GenerateExecutable = false,
                ReferencedAssemblies = {
                "System.dll",
                },
            };
            foreach(string DLL in DLLs(file))
            {
                compilerParams.ReferencedAssemblies.Add(DLL);
            }
            CompilerResults results = provider.CompileAssemblyFromSource(compilerParams, source);

            if (results.Errors.Count != 0)
            {
                string output = "";
                foreach (CompilerError y in results.Errors)
                {
                    output += y.ErrorText + Environment.NewLine;
                }
                throw new Exception("Compile failed:" + output);
            }
                object o = results.CompiledAssembly.CreateInstance("SimpleScript.Generator");
                MethodInfo mi = o.GetType().GetMethod("Generate");
                Data[] parametersArray = new Data[] { server };
                Document x = (Document)mi.Invoke(o, parametersArray);
            return x;
        }

        private static string Head(string file)
        {
            string ret = "using System;using MSiteDLL;";
            if(File.Exists(file+".inc"))
            {
                ret = File.ReadAllText(file + ".inc");
            }
            return ret;
        }

		public static string PHPExec(string file, string Post, string Get)
        {
            string ret;
            if ((!File.Exists(Program.php) && Program.fphp == false) || Program.php == "disable")
            {
                ret = "NO PHP";
            } else {
                try {
					string[] test = UnsafePHP(Program.php, file, Post, Get);
					ret = test[1] + test[0];
                } catch(Exception e) {
                    Console.WriteLine("PHP {e}",e.Message);
                   ret = "PHP ERROR";
                }
            }
            return ret;
        }

		private static string[] UnsafePHP(string php, string file, string Post, string Get)
        {
			string[] ret = new string[2];
			Process process = new Process ();

			if (Program.uphpsh == true) {
				process.StartInfo.FileName = "bash";
				process.StartInfo.Arguments = Program.phpsh+" \"" + file + "\" \"" + Get + "\" \"" + Post + "\"";
			} else {
				process.StartInfo.FileName = php;
				process.StartInfo.Arguments = "-f \"" + file + "\" \"" + Get + "\" \"" + Post + "\"";
			}
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = true;
			process.Start ();
			ret [0] = process.StandardOutput.ReadToEnd ();
			ret [1] = process.StandardError.ReadToEnd ();
			process.WaitForExit ();
			return ret;

        }
        private static string[] DLLs(string file)
        {
            string[] ret = { "MSiteDLL.dll" };
            if (File.Exists(file + ".dlls"))
            {
                ret = File.ReadAllLines(file + ".dlls");
            }
            return ret;
        }

    }
}
using System;
using SimpleHttpServer;
using System.Reflection;
using System.IO;

namespace MSiteFramework
{
	public class LoadSite
	{
		dynamic site = null;
		public LoadSite(string dll)
		{
			Assembly DLL = Assembly.LoadFile(Directory.GetCurrentDirectory() + @"/" + dll);
			foreach (Type type in DLL.GetExportedTypes())
			{
				if (type.Name == "Routes")
				{
					this.site = Activator.CreateInstance(type);
				}
			}
			if (site == null)
			{
				throw (new Exception("Not a site DLL."));
			}
		}
		public HttpServer Server(int Port)
		{
			return new HttpServer(Port, this.site.Handler());
		}
	}
}

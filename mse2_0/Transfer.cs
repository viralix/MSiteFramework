using SimpleHttpServer.Models;
using MSiteFramework;
using MSSc;
using System.IO;
using System;

namespace mse2_0
{
	public class Transfer
	{
		public static HttpResponse Request(HttpResponse old, HttpRequest request)
		{
			try
			{
				if (request.Url == "/engine")
				{
					old.StatusCode = "200";
					old.ReasonPhrase = "OK";
					old.ContentAsUTF8 = "mse2_0";
					old.Headers["Content-Type"] = "text/plain";
				}
				else if (_rG(request.Url).EndsWith(".m"))
				{
					old.StatusCode = "500";
					old.ReasonPhrase = "Internal Server Error";
					old.ContentAsUTF8 = "<h1>Internal Server Error</h1><p>mse2_0 is used, please use mse1_0 to run [.m] files.</p>";
				}
				if (_rG(request.Url).EndsWith(".mssc"))
				{
					old = mssc(old,request);
				}
				else {
					old.StatusCode = "200";
					old.ReasonPhrase = "OK";
					old.Content = File.ReadAllBytes(MSiteFramework.Program.Host + _rG(request.Url));
				}
			}
			catch (Exception)
			{
				try
				{
					old.StatusCode = "200";
					old.ReasonPhrase = "OK";
					old.Content = File.ReadAllBytes(MSiteFramework.Program.Host + _rG(request.Url) + MSiteFramework.Program.Index);
				}
				catch (Exception)
				{
					old.Headers["Content-Type"] = "text/html";
					old.StatusCode = "404";
					old.ReasonPhrase = "Not Found";
					old = Other(old, request);
				}
			}
			return old;
		}
		public static HttpResponse mssc(HttpResponse xres, HttpRequest xreq)
		{
			MSSc.Program program = new Interpreter(File.ReadAllLines(MSiteFramework.Program.Host + _rG(xreq.Url))).Interpret();
			program.LoadHttp(xres, xreq);
			return new Executor(program).Execute().res;
		}
		public static HttpResponse Other(HttpResponse old, HttpRequest request)
		{
			return old;
		}
		private static string _rG(string url)
		{
			return url.Split('?')[0];
		}
	}
}

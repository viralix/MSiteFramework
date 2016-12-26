using SimpleHttpServer.Models;
using System.Collections.Generic;
using MSiteDLL;
using System;
using MSiteFramework;
using System.IO;
namespace mse1_0
{
	public class Routes
	{
		public List<Route> routes
		{
			get
			{
				return new List<Route>()
				{
					new Route()
					{
						Callable = Handle,
						UrlRegex = "(\\/)",
						Method = "GET"
					},
					new Route()
					{
						Callable = Handle,
						UrlRegex = "(\\/)",
						Method = "POST"
					}
				};

			}
		}
		public List<Route> Handler()
		{
			return routes;
		}

		public static string[] Handle(Data server)
		{
			string[] ret = new string[3];

			ret[0] = "";
			ret[1] = "OK";
			ret[2] = "200";
			if (server.Location(Program.Host).EndsWith(".m"))
			{
				try
				{
					Document doc = Parser.Generate(server);
					doc.Write();
					ret[0] = Site.Page;
				}
				catch (Exception e)
				{
					Document doc = Files.Error.Generate(e, false);
					doc.Write();
					ret[0] = Site.Page;
				}
			}
			else if (server.Location(Program.Host).EndsWith(".mc"))
			{
				try
				{
					Document doc = Script.Execute(File.ReadAllText(server.Location(Program.Host)), (server.Location(Program.Host)), server);
					doc.Write();
					ret[0] = Site.Page;
				}
				catch (Exception e)
				{
					Document doc = Files.Error.Generate(e, false);
					doc.Write();
					ret[0] = Site.Page;
				}
			}
			else
			{
				string url = server.Location(Program.Host);
				try
				{
					if (File.Exists(url))
					{
						ret[2] = "300";
						Site.Reset();
					}
					else
					{
						if (Directory.Exists(url))
						{
							if (File.Exists(url + "/" + Program.Index))
							{
								ret[2] = "200";
								Site.Write(File.ReadAllText(url + "/" + Program.Index));
							}
							else
							{
								ret[2] = "200";
								Document doc = Files.List.Generate(server);
								doc.Write();
							}
						}
						else
						{
								ret[2] = "404";
								throw (new Exception("NOT FOUND"));
						}
					}
					ret[0] = Site.Page;
				}
				catch (Exception e)
				{
					Document doc = Files.Error.Generate(e, false);
					doc.Write();
					ret[0] = Site.Page;
					ret[1] = e.Message;
					ret[2] = "500";
					if (e.Message.ToUpper() == "NOT FOUND")
					{
						ret[2] = "404";
					}
					if (server.request.Url == "/engine")
					{
						ret[0] = "mse1_0";
						ret[1] = "OK";
						ret[2] = "200";
					}
				}
			}
			Site.Reset();
			return ret;
		}
		private static HttpResponse Handle(HttpRequest request)
		{
			HttpResponse x = new HttpResponse();
			Data y = new Data(request);
			y.SetGet(request.Url);
			y.hasData = true;
			y.post = request.Content;
			x.Headers["Content-Type"] = SimpleHttpServer.RouteHandlers.QuickMimeTypeMapper.GetMimeType(Path.GetExtension(y.Location(Program.Host)));
			if (request.Url.EndsWith(".php"))
			{
				string url = y.Location(Program.Host);
				string output = "", r = "";
				try
				{
					output = Script.PHPExec(url, y.post, y.get);
				}
				catch (Exception e)
				{
					output = "PHP ERROR";
					r = e.Message;
				}
				if (File.Exists(url))
				{
					if (!(output == "PHP ERROR") && !(output == "NO PHP"))
					{
						x.ContentAsUTF8 = output;
						x.StatusCode = "200";
						x.ReasonPhrase = "OK";
					}
					else {
						x.ContentAsUTF8 = @"
                        <html>
                        <head>
                        <title>PHP Execution Error</title>
                        </head>
                        <body>
                        <h1>" + output + @"</h1>
                        <p>Please fix this issue in order to run PHP files.</p>
						<p>" + r + @"</p>
                        </body>
                        </html>";
						x.StatusCode = "500";
						x.ReasonPhrase = "InternalServerError";
					}
				}
				else {
					x.ContentAsUTF8 = @"
                        <html>
                        <head>
                        <title>File not found.</title>
                        </head>
                        <body>
                        <h1>" + output + @"</h1>
                        <p>That PHP file was not found.</p>
                        </body>
                        </html>";
					x.StatusCode = "404";
					x.ReasonPhrase = "NotFound";
				}
				return x;
			}
			else {
				string[] z = Handle(y);
				if (z[2] == "300")
				{
					x.Content = File.ReadAllBytes(y.Location(Program.Host));
					z[2] = "200";
				}
				else {
					x.ContentAsUTF8 = z[0];
				}
				x.ReasonPhrase = z[1];
				x.StatusCode = z[2];
				return x;
			}
		}
	}
}

using MSiteDLL;
using SimpleHttpServer.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace MSiteFramework
{
    static class Routes
    {

        public static List<Route> GET
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
                    Document doc = Script.Execute(File.ReadAllText(server.Location(Program.Host)),(server.Location(Program.Host)),server);
                    doc.Write();
                    ret[0] = Site.Page;
                }
                catch (Exception e)
                {
                    Document doc = Files.Error.Generate(e, false);
                    doc.Write();
                    ret[0] = Site.Page;
                }
            }  else
            {
                string url = server.Location(Program.Host);
                try
                {
                    if (File.Exists(url))
                    {
                        Site.Write(File.ReadAllText(url));
                    } else
                    {
                        if(Directory.Exists(url))
                        {
                            if(File.Exists(url  + "/" + Program.Index))
                            {
                                ret[2] = "200";
                                Site.Write(File.ReadAllText(url + "/" + Program.Index));
                            } else
                            {
                                ret[2] = "200";
                                Document doc = Files.List.Generate(server);
                                doc.Write();
                            }
                        } else
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
            string[] z = Handle(y);
            x.ContentAsUTF8 = z[0];
            x.ReasonPhrase = z[1];
            x.StatusCode = z[2];
            return x;
         }
    }
}

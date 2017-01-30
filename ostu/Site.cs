using SimpleHttpServer.Models;
using System.Collections.Generic;
using System.IO;
using CommonMark;
using MSiteFramework;
using System;

namespace ostu
{
    public class Site
    {
        HTML html;
        int code = 200;
        string reason = "OK";
        bool normal = true;
        URL current;
        HttpResponse other = new HttpResponse();
        public HttpResponse Response()
        {
            if (normal)
            {
                return new HttpResponse()
                {
                    StatusCode = code.ToString(),
                    ReasonPhrase = reason,
                    ContentAsUTF8 = html.ToString()
                };
            }
            return other;
        }

        public Site(Language lang, HttpRequest request)
        {
            try
            {
                html = new HTML(lang);
                current = new URL("http://" + request.Headers["Host"] + ":" + Program.Port + request.Url);
                Generate(request);
            }
            catch (Exception e)
            {
                html.head = new HTMLWriter("head");
                html.body = new HTMLWriter("body");
                if (e.Message == "File not found.")
                {
                    reason = "Not found";
                    code = 404;
                }
                Init();
                html.head.WriteLine("<title>{$4} - "+e.Message+"</title>");
                html.body.WriteLine("<h1>{$4}</h1>");
                html.body.WriteLine("<p>{$5}</h1>");
                html.body.WriteLine("<p>{$6}: "+e.Message+"</p>");
            }
        }

        private void Init()
        {
            html.head.WriteLine("<script src=\"/js/jquery.min.js\"></script>");
            html.head.WriteLine("<script src=\"/js/bootstrap.min.js\"></script>");
            html.head.WriteLine("<link rel=\"stylesheet\" type=\"text/css\" href=\"/css/bootstrap.min.css\">");
            html.head.WriteLine("<link rel=\"stylesheet\" type=\"text/css\" href=\"/css/bootstrap-theme.min.css\">");
        }

        private void LoadFile(HttpRequest request)
        {
            normal = false;
            other.StatusCode = "200";
            other.ReasonPhrase = "OK";
            other.Headers["Content-Type"] = SimpleHttpServer.RouteHandlers.QuickMimeTypeMapper.GetMimeType(Path.GetExtension(current.raw().LocalPath));
            other.Content = File.ReadAllBytes("html"+current.raw().LocalPath);
        }

        private void Internal(HttpRequest request)
        {
            html.head.WriteLine("<title>{$1} - {$2}</title>");
            html.body.WriteLine("Nope.");
        }

        private void Generate(HttpRequest request)
        {
            if (current.hasExt("md"))
            {
                Init();
                html.head.WriteLine("<title>" + current.raw().LocalPath + "</title>");
                html.body.WriteLine(CommonMarkConverter.Convert(current.file()));
            } else
            {
                if (current.exists())
                    LoadFile(request);
                else
                    Internal(request);
            }
        }
    }
}

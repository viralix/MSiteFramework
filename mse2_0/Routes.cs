using SimpleHttpServer.Models;
using System.Collections.Generic;
using System;

namespace mse2_0
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

		public HttpResponse Handle(HttpRequest request)
		{
			HttpResponse response = new HttpResponse();
			try
			{
				response.Headers["Content-Type"] = SimpleHttpServer.RouteHandlers.QuickMimeTypeMapper.GetMimeType(request.Url.Split('?')[0].Split('.')[request.Url.Split('?')[0].Split('.').Length - 1]);
			}
			catch (Exception)
			{
				response.Headers["Content-Type"] = "text/html";
			}

			return Transfer.Request(response, request);
		}
	}
}

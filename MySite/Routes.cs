using SimpleHttpServer.Models;
using System.Collections.Generic;
namespace MySite
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
			return new HttpResponse()
			{
				// Your own response
			};
		}
	}
}

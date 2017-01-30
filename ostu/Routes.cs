using SimpleHttpServer.Models;
using ostu.Languages;
using System.Collections.Generic;
namespace ostu
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
			Language lang = new English();
            return new Site(lang, request).Response();
		}
	}

}


using SimpleHttpServer.Models;
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
			
			return new HttpResponse()
			{
				ContentAsUTF8 = "<h1>Zdravo, Svetu!</h1>",
				// Your own response
			};
		}
	}

}


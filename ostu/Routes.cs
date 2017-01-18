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
			Language lang = new Language ("English");
			lang.Strings = new List<string> {
				"O.S.Т.U Gostivar",
				"Welcome to our web site",
			};
			return new HttpResponse()
			{
				ContentAsUTF8 = lang.Strings[1],
				// Your own response

			};
		}
	}

}


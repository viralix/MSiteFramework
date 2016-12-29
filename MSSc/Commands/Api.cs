using System;
using System.Collections.Generic;
using SimpleHttpServer.Models;
using System.Text;
namespace MSSc
{
	public class Api : Command
	{
		public Api(Instruction i, Http HTTP) : base(i, HTTP)
		{
			if (Index.Command(i.Command) != "http")
			{
				throw new Exception("Not valid command.");
			}
		}
		public override Response Execute()
		{
			int status = 2; Return ret = null;
			switch (Parameters[0])
			{
				case "init":
					HttpRequest oldReq = http.req;
					http = new Http
					{
						req = oldReq,
						res = new HttpResponse
						{
							Content = new byte[] { 0 },
							ReasonPhrase = "OK",
							StatusCode = "200",
						},
					};
					break;
				case "write":
					http.res.ContentAsUTF8 = Encoding.UTF8.GetString(AddBytes(http.res.Content,GetTextBytes()));
				break;
				case "status":
					if (Parameters[1].ToLower() == "code")
					{
						http.res.StatusCode = Parameters[2];
					}
					else if (Parameters[1].ToLower() == "text") {
						http.res.ReasonPhrase = Parameters[2];
					}
					break;
				case "get":
					ret = new Return()
					{
						Name = Parameters[1],
						Var = new Variable(GetURL(Parameters[2])),
					};
					status = 1;
					break;
			}
			return new Response()
			{
				Status = status,
				Site = http,
				Data = ret,
			};
		}
		public string GetURL(string x)
		{
			try
			{
				string r = "";
				foreach (string s in http.req.Url.Split('?')[1].Split('&'))
				{
					if (s.Split('=')[0] == x)
					{
						r = s.Split('=')[1];
						break;
					}
				}
				return r;

			}
			catch (Exception) {
				return "";
			}
		}
		public byte[] GetTextBytes()
		{
			string text = "";
			for (int i = 1; i != Parameters.Length; i++)
			{
				if (i == 1)
				{
					text = Parameters[i];
				}
				else {
					text += " " + Parameters[i];
				}
			}
			return Encoding.UTF8.GetBytes(text);
		}
		public byte[] AddBytes(byte[] old, byte[] add)
		{
			List<byte> Bytes = new List<byte>();
			if(old != null)
			Bytes.AddRange(old);
			if(add != null)
			Bytes.AddRange(add);
			return Bytes.ToArray();
		}
	}
}

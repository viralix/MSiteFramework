using SimpleHttpServer.Models;
using System.Collections.Generic;
namespace MSSc
{
	public class Program
	{
		public HttpResponse Response;
		public HttpRequest Request;
		public Dictionary<string, int> Labels = new Dictionary<string, int>();
		public List<Instruction> Code = new List<Instruction>();
		public Dictionary<string, Variable> Vars = new Dictionary<string, Variable>();
		public void LoadHttp(HttpResponse response, HttpRequest request)
		{
			Response = response;
			Request = request;
		}
		public Instruction DoVars(Instruction old)
		{
			List<string> param = old.Parameters, x = new List<string>();
			foreach (string s in param)
			{
				if (s.StartsWith("$"))
				{
					if (Vars.ContainsKey(s.TrimStart('$')))
					{
						x.Add(Vars[s.TrimStart('$')].Content);
					}
				}
				else {
					x.Add(s);
				}
			}
			old.Parameters = x;
			return old;
		}
	}
	public class Instruction
	{
		public int Command;
		public List<string> Parameters;
		public string Location;
		public Instruction(int command, List<string> parameters, string location)
		{
			Command = command;
			Parameters = parameters;
			Location = location;
		}
	}
	public class Variable
	{
		public string Content;
		public Variable(string content)
		{
			Content = content;
		}
	}
	public class Return
	{
		public Variable Var;
		public string Name;
	}
	public class Http
	{
		public HttpRequest req;
		public HttpResponse res;
	}
	public class Response
	{
		public Http Site;
		public int Status;
		public Return Data;
	}
	public class Command
	{
		public string[] Parameters;
		public Http http;
		public Command(Instruction instruction, Http HTTP)
		{
			Parameters = instruction.Parameters.ToArray();
			http = HTTP;
		}
		public virtual Response Execute()
		{
			return new Response
			{
				Status = 0,
				Data = null,
				Site = http,
			};
		}
	}
}

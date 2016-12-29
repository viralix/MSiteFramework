using System;
namespace MSSc
{
	public class Var : Command
	{
		public Var(Instruction i, Http HTTP) : base(i, HTTP)
		{
			if (Index.Command(i.Command) != "var")
			{
				throw new Exception("Not valid command.");
			}
		}
		public override Response Execute()
		{
			string output = ""; int i = 0;
			string name = "";
			foreach (string param in Parameters)
			{
				if (i == 0)
					name = param;
				else {
					if (i == 1)
						output = param;
					else
					output += " " + param;
				}
				i++;
			}
			return new Response()
			{
				Status = 1,
				Data = new Return()
				{
					Name = name,
					Var = new Variable(output),
				},
			};
		}
	}
}

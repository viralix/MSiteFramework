using System;
namespace MSSc
{
	public class Out : Command
	{
		public Out(Instruction i, Http HTTP) : base(i, HTTP)
		{
			if (Index.Command(i.Command) != "out")
			{
				throw new Exception("Not valid command.");
			}
		}
		public override Response Execute()
		{
			string output = "";
				int i = 0;
				foreach (string param in Parameters)
				{
					if (i == 0)
						output = param;
					else
						output += " " + param;
					i++;
				}
			Console.WriteLine(output);
			return new Response()
			{
				Status = 0,
				Data = null,
			};
		}
	}
}

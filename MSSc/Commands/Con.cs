using System;
namespace MSSc
{
	public class Con : Command
	{
		public Con(Instruction i, Http HTTP) : base(i, HTTP)
		{
			if (Index.Command(i.Command) != "con")
			{
				throw new Exception("Not valid command.");
			}
		}
		public override Response Execute()
		{
			int status = 0;
			Return data = null;
			switch (Parameters[0].ToLower())
			{
				case "clear":
					Console.Clear();
					break;
				case "title":
					Console.Title = Parameters[1];
					break;
				case "version":
					status = 1;
					data = new Return()
					{
						Name = Parameters[1],
						Var = new Variable(Environment.OSVersion.VersionString),
					};
					break;
				default:
					throw new Exception("Unknown function for Console.");
			}
			return new Response()
			{
				Status = status,
				Data = data
			};
		}
	}
}

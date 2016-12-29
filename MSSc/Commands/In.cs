using System;
namespace MSSc
{
	public class In : Command
	{
		public In(Instruction i, Http HTTP) : base(i, HTTP)
		{
			if (Index.Command(i.Command) != "in")
			{
				throw new Exception("Not valid command.");
			}
		}
		public override Response Execute()
		{
			string name = Parameters[0];
			string output = Console.ReadLine();
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

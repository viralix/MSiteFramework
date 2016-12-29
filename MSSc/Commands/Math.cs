using System;
namespace MSSc
{
	public class Math : Command
	{
		public Math(Instruction i, Http HTTP) : base(i, HTTP)
		{
			if (Index.Command(i.Command) != "math")
			{
				throw new Exception("Not valid command.");
			}
		}
		public override Response Execute()
		{
			string name = Parameters[0];
			string op = Parameters[2];
			double one = double.Parse(Parameters[1]);
			double two = double.Parse(Parameters[3]);
			double output = 0;
			switch (op)
			{
				case "/":
					output = one / two;
					break;
				case "*":
					output = one * two;
					break;
				case "-":
					output = one - two;
					break;
				case "+":
					output = one + two;
					break;
				default:
					throw new Exception("Incorrect operator.");
			}
			return new Response()
			{
				Status = 1,
				Data = new Return()
				{
					Name = name,
					Var = new Variable(output.ToString()),
				},
			};
		}
	}
}

namespace MSSc
{
	public static class Index
	{
		public static string Command(int Command)
		{
			switch (Command)
			{
				case 0x0:
					return "var";
				case 0x1:
					return "out";
				case 0x2:
					return "in";
				case 0x3:
					return "goto";
				case 0x4:
					return "math";
				case 0x5:
					return "if";
				case 0x6:
					return "exec";
				case 0x7:
					return "load";
				case 0x8:
					return "get";
				case 0x9:
					return "con";
				case 0xa:
					return "http";
				default:
					return "default";
			}
		}
		public static int Command(string Command)
		{
			switch (Command)
			{
				case "var":
					return 0x0;
				case "out":
					return 0x1;
				case "in":
					return 0x2;
				case "goto":
					return 0x3;
				case "math":
					return 0x4;
				case "if":
					return 0x5;
				case "exec":
					return 0x6;
				case "load":
					return 0x7;
				case "get":
					return 0x8;
				case "con":
					return 0x9;
				case "http":
					return 0xa;
				default:
					return -0x1;
			}
		}
		public static Command FromInstruction(Instruction i, Http http)
		{
			switch (i.Command)
			{
				case 0x0:
					return new Var(i, http);
				case 0x1:
					return new Out(i, http);
				case 0x2:
					return new In(i, http);
				case 0x4:
					return new Math(i, http);
				case 0x9:
					return new Con(i, http);
				case 0xa:
					return new Api(i, http);
				default:
					return new Command(i, http);
			}
		}
	}
}

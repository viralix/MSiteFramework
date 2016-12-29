using System;
using System.Collections.Generic;
namespace MSSc
{
	public class Interpreter
	{
		string[] File;
		public Interpreter(string[] file)
		{
			File = file;
		}
		public Program Interpret()
		{
			Program ret = new Program();
			int i = 0;
			foreach (string line in File)
			{
				int x = 0;
				string command;
			Start:
				command = line.Split(' ')[x];
				if (command.Length > 1)
				{
					try
					{
						if (command.StartsWith("#"))
						{
							ret.Labels.Add(command.TrimStart('#'), ret.Code.Count - 1);
						}
						else if (command.ToLower() == "def")
						{
							ret.Vars.Add(line.Split(' ')[1], new Variable(GetFrom(2, line.Split(' '))));
						}
						else {
							ret.Code.Add(Get(line, "line " + (i+1)));
						}
					}
					catch (Exception)
					{
						throw new Exception("Inncorect syntax at line: " + i);
					}
				}
				else {
					x++;
					if(line.Length-(x+1) > 1)
					goto Start;
				}
				i++;
			}
			return ret;
		}
		public static string GetFrom(int x, string[] s)
		{
			string ret = "";
			int i = 0; bool ok = false;
			foreach (string y in s)
			{
				if (i >= x)
				{
					if (ok)
					{
						ret += " " + y;
					}
					else {
						ret = y;
						ok = true;
					}
				}
				i++;
			}
			return ret;
		}
		public static Instruction Get(string line, string location)
		{
			List<string> param = new List<string>();
			bool isOk = false;
			foreach (string p in line.Split(' '))
			{
				if (isOk)
				{
					param.Add(p);
				}
				else {
					isOk = true;
				}
			}
			return new Instruction(Index.Command(line.Split(' ')[0].ToLower()), param, location);
		}
	}
}

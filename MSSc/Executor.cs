using System.Collections.Generic;
using System.IO;
using SimpleHttpServer.Models;
using System;
namespace MSSc
{
	public class ExecuteResponse
	{
		public Program ret;
		public HttpResponse res;
	}
	public class Executor
	{
		Program program;
		public Executor(Program program)
		{
			this.program = program;
		}
		public ExecuteResponse Execute()
		{
			int CurrentInstruction = 0;
			while (CurrentInstruction < program.Code.Count)
			{
				Instruction i = program.Code[CurrentInstruction];
				try
				{
					if (i.Command == 0x3)
					{
						if (program.Labels.ContainsKey(i.Parameters[0]))
						{
							CurrentInstruction = program.Labels[i.Parameters[0]];
						}
						else {
							throw new Exception("Label not defined.");
						}
					}
					else if (i.Command == 0x5)
					{
						i = program.DoVars(i);
						bool ret = false;
						switch (i.Parameters[1])
						{
							case "==":
								if (i.Parameters[0] == i.Parameters[2]) ret = true;
								break;
							case "!=":
								if (i.Parameters[0] != i.Parameters[2]) ret = true;
								break;
							case ">":
								if (double.Parse(i.Parameters[0]) > double.Parse(i.Parameters[2])) ret = true;
								break;
							case "<":
								if (double.Parse(i.Parameters[0]) < double.Parse(i.Parameters[2])) ret = true;
								break;
							case "<=":
								if (double.Parse(i.Parameters[0]) <= double.Parse(i.Parameters[2])) ret = true;
								break;
							case ">=":
								if (double.Parse(i.Parameters[0]) >= double.Parse(i.Parameters[2])) ret = true;
								break;
							default:
								throw new Exception("Incorrect operator.");
						}
						if (ret)
						{
							if (i.Parameters[3] == "goto")
							{
								if (program.Labels.ContainsKey(i.Parameters[4]))
								{
									CurrentInstruction = program.Labels[i.Parameters[4]];
								}
								else {
									throw new Exception("Label not defined.");
								}
							}
							else {
								throw new Exception("Expected goto instead of \"" + i.Parameters[3] + "\".");
							}
						}
					}
					else if (i.Command == 0x6)
					{
						new Executor(new Interpreter(File.ReadAllLines(i.Parameters[0])).Interpret()).Execute();
					}
					else if (i.Command == 0x7)
					{
						Program require = new Interpreter(File.ReadAllLines(i.Parameters[0])).Interpret();
						foreach (KeyValuePair<string, Variable> xVar in require.Vars)
						{
							if (program.Vars.ContainsKey(xVar.Key))
							{
								program.Vars[xVar.Key] = xVar.Value;
							}
							else {
								program.Vars.Add(xVar.Key, xVar.Value);
							}
						}
					}
					else if (i.Command == 0x8)
					{
						Program require = new Executor(new Interpreter(File.ReadAllLines(i.Parameters[0])).Interpret()).Execute().ret;
						foreach (KeyValuePair<string, Variable> xVar in require.Vars)
						{
							if (program.Vars.ContainsKey(xVar.Key))
							{
								program.Vars[xVar.Key] = xVar.Value;
							}
							else {
								program.Vars.Add(xVar.Key, xVar.Value);
							}
						}
					}
					else{
						Response res = Index.FromInstruction(
							program.DoVars(i),
						    new Http { req = program.Request, res = program.Response }
						).Execute();
						switch (res.Status)
						{
							case 0: // Do nothing, it's a void.
								break;
							case 1: // Save variable.
								if (program.Vars.ContainsKey(res.Data.Name))
									program.Vars[res.Data.Name] = res.Data.Var;
								else
									program.Vars.Add(res.Data.Name, res.Data.Var);
								break;
							case 2:
								program.Request = res.Site.req;
								program.Response = res.Site.res;
								break;
						}
					}
				}
				catch (Exception e)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine();
					Console.WriteLine("Error occurred: \"{0}\" at \"{1}\" ", e.Message, i.Location);
					Console.WriteLine();
					Console.ResetColor();
				}
				CurrentInstruction++;
			}
			return new ExecuteResponse()
			{
				ret = program,
				res = program.Response
			};
		}
	}
}

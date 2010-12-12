using System;

namespace Taskie
{
	public interface ICommandLineParser
	{
		ITask GetRequestedTask(string[] arguments);
	}

	public class CommandLineParser : ICommandLineParser
	{
		public ITask GetRequestedTask(string[] arguments)
		{
			throw new NotImplementedException();
		}
	}
}
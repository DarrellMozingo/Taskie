namespace Taskie
{
	public interface ICommandLineParser
	{
		ITask GetRequestedTask(string[] arguments);
	}

	public class CommandLineParser : ICommandLineParser
	{
		private readonly IScreenIO _screenIO;
		private readonly ITaskResolver _taskResolver;

		public const string RunCommand = "/run";

		public CommandLineParser(IScreenIO screenIo, ITaskResolver taskResolver)
		{
			_screenIO = screenIo;
			_taskResolver = taskResolver;
		}

		public ITask GetRequestedTask(string[] arguments)
		{
			var passedInArgumentsAreWrong = (arguments.Length != 2 || arguments[0] != RunCommand);

			if (passedInArgumentsAreWrong)
			{
				printUsageInformation();
				return new NullTask();
			}

			var friendlyTaskNameToRun = arguments[1];

			var task = _taskResolver.ResolveTask(friendlyTaskNameToRun);
			var cantFindTaskToRun = (task == null);

			if (cantFindTaskToRun)
			{
				printUnknownTaskErrorInformation(friendlyTaskNameToRun);
				return new NullTask();
			}

			return task;
		}

		private void printUnknownTaskErrorInformation(string friendlyTaskNameToRun)
		{
			_screenIO.WriteLine("Sorry, I don't have at task with that name ('{0}'). Please check the spelling and try again.".Substitute(friendlyTaskNameToRun));
		}

		private void printUsageInformation()
		{
			var runnableTasks = _taskResolver.GetAllRunnableTasks();

			_screenIO.WriteLine("");
			_screenIO.WriteLine("Taskie - Scheduled Task Manager");
			_screenIO.WriteLine("");
			_screenIO.WriteLine("Usage:");
			_screenIO.WriteLine("   {0} <task_name> - Runs the specified task, which can only be one of the following:".Substitute(RunCommand));

			runnableTasks.ForEach(runnableTask => _screenIO.WriteLine("     * " + runnableTask.Name + " - " + runnableTask.Description));
		}
	}
}
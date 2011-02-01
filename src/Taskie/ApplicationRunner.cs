using System;

namespace Taskie
{
	public interface IApplicationRunner : IDisposable
	{
		void RunWith(string[] arguments);
	}

	public class ApplicationRunner : IApplicationRunner
	{
		private readonly ITaskieApplication _taskieApplication;
		private readonly ICommandLineParser _commandLineParser;

		public ApplicationRunner(ICommandLineParser commandLineParser, ITaskieServiceLocator taskieServiceLocator, ITaskieApplication taskieApplication)
		{
			_commandLineParser = commandLineParser;

			_taskieApplication = getInstanceFromServiceLocator(taskieServiceLocator) ?? taskieApplication;
			_taskieApplication.Startup();
		}

		private static ITaskieApplication getInstanceFromServiceLocator(ITaskieServiceLocator taskieServiceLocator)
		{
			try
			{
				return taskieServiceLocator.GetInstance<ITaskieApplication>();
			}
			catch (Exception)
			{
				return null;
			}
		}

		public void RunWith(string[] arguments)
		{
			var taskToRun = _commandLineParser.GetRequestedTask(arguments);

			taskToRun.Run();
		}

		#region IDisposable
		private bool _disposed;

		~ApplicationRunner()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_taskieApplication.Shutdown();
				}
			}

			_disposed = true;
		}
		#endregion
	}
}
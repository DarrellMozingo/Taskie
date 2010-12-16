using System;

namespace Taskie
{
	public interface IApplicationRunner : IDisposable
	{
		void RunWith(string[] arguments);
	}

	public class ApplicationRunner : IApplicationRunner
	{
		private readonly IApplication _application;
		private readonly ICommandLineParser _commandLineParser;

		public ApplicationRunner(ICommandLineParser commandLineParser, IServiceLocator serviceLocator, IApplication application)
		{
			_commandLineParser = commandLineParser;

			_application = serviceLocator.GetInstance<IApplication>() ?? application;
			_application.Startup();
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
					_application.Shutdown();
				}
			}

			_disposed = true;
		}
		#endregion
	}
}
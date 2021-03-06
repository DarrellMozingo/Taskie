using AppUsingOwnRunner.Data;
using Taskie;

namespace AppUsingOwnRunner
{
	public class TaskieApplication : ITaskieApplication
	{
		private readonly ISessionBuilder _sessionBuilder;

		public TaskieApplication(ISessionBuilder sessionBuilder)
		{
			_sessionBuilder = sessionBuilder;
		}

		public void Startup()
		{
			_sessionBuilder.BuildSession();
		}

		public void Shutdown()
		{
		}
	}
}
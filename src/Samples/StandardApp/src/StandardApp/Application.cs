using StandardApp.Data;
using Taskie;

namespace StandardApp
{
	public class Application : IApplication
	{
		private readonly ISessionBuilder _sessionBuilder;

		public Application(ISessionBuilder sessionBuilder)
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
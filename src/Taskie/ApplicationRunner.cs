using System;
using Microsoft.Practices.ServiceLocation;

namespace Taskie
{
	public interface IApplicationRunner : IDisposable
	{
		void RunWith(string[] arguments);
	}

	public class ApplicationRunner : IApplicationRunner
	{
		private readonly IApplication _application;

		public ApplicationRunner()
		{
			_application = getApplicationInstance();
			_application.Startup();
		}

		private static IApplication getApplicationInstance()
		{
			try
			{
				return ServiceLocator.Current.GetInstance<IApplication>();
			}
			catch (ActivationException)
			{
				return IoC.Resolve<IApplication>();
			}
		}

		public void RunWith(string[] arguments)
		{
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
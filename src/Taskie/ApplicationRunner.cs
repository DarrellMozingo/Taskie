using System;

namespace Taskie
{
	public class ApplicationRunner : IDisposable
	{
		private readonly IApplication _application;

		public ApplicationRunner(IApplication application)
		{
			_application = application;
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
using CommonServiceLocator.NinjectAdapter;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using Taskie;

namespace StandardApp
{
	public class Application : IApplication
	{
		public void Startup()
		{
			var kernel = new StandardKernel(new StandardModule());
			ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(kernel));
		}

		public void Shutdown()
		{
		}
	}
}
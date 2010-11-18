using CommonServiceLocator.NinjectAdapter;
using Microsoft.Practices.ServiceLocation;
using Ninject;

namespace StandardApp
{
	public static class IoC
	{
		private static IKernel _kernel;

		public static void Bootstrap()
		{
			_kernel = new StandardKernel(new StandardModule());
			ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(_kernel));
		}
	}
}
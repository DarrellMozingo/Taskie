using System;

namespace Taskie
{
	public static class TaskieRunner
	{
		public static void RunWith(string[] arguments, IServiceLocator serviceLocator)
		{
			if (serviceLocator == null)
			{
				throw new ArgumentNullException("serviceLocator");
			}

			IoC.Bootstrap();
			IoC.Inject(serviceLocator);

			using (var applicationRunner = IoC.Resolve<IApplicationRunner>())
			{
				applicationRunner.RunWith(arguments);
			}
		}
	}
}
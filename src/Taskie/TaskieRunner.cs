using System;
using Taskie.Container;

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

			var container = IoC.CreateContainer();
			container.Inject(serviceLocator);

			using (var applicationRunner = container.GetInstance<IApplicationRunner>())
			{
				applicationRunner.RunWith(arguments);
			}
		}
	}
}
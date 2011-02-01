using System;
using Taskie.Container;

namespace Taskie
{
	public static class TaskieRunner
	{
		public static void RunWith(string[] arguments, ITaskieServiceLocator taskieServiceLocator)
		{
			if (taskieServiceLocator == null)
			{
				throw new ArgumentNullException("taskieServiceLocator");
			}

			var container = IoC.CreateContainer();
			container.Inject(taskieServiceLocator);

			using (var applicationRunner = container.GetInstance<IApplicationRunner>())
			{
				applicationRunner.RunWith(arguments);
			}
		}
	}
}
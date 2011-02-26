using System.Collections.Generic;
using Ninject;
using Taskie;

namespace AppUsingTaskieRunner.Container
{
	public class TaskieServiceLocator : ITaskieServiceLocator
	{
		public TaskieServiceLocator()
		{
			IoC.Bootstrap();
		}

		public INSTANCE GetInstance<INSTANCE>()
		{
			return IoC.Kernel.Get<INSTANCE>();
		}

		public IEnumerable<INSTANCE> GetAllInstances<INSTANCE>()
		{
			return IoC.Kernel.GetAll<INSTANCE>();
		}
	}
}
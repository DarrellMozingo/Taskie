using System.Collections.Generic;
using Ninject;
using Taskie;

namespace StandardApp.Container
{
	public class ServiceLocator : IServiceLocator
	{
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
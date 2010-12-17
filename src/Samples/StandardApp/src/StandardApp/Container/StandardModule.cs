using Ninject.Modules;
using StandardApp.Data;
using StandardApp.Services;
using StandardApp.Tasks;
using Taskie;

namespace StandardApp.Container
{
	public class StandardModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ICustomerService>().To<CustomerService>();
			Bind<ISessionBuilder>().To<SessionBuilder>();
			Bind<IApplication>().To<TaskieApplication>();
			Bind<ITask>().To<ProcessCustomersTask>();
			Bind<IServiceLocator>().To<ServiceLocator>();
		}
	}
}
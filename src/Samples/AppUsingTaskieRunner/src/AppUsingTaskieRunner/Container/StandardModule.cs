using AppUsingTaskieRunner.Data;
using AppUsingTaskieRunner.Services;
using AppUsingTaskieRunner.Tasks;
using Ninject.Modules;
using Taskie;

namespace AppUsingTaskieRunner.Container
{
	public class StandardModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ICustomerService>().To<CustomerService>();
			Bind<ISessionBuilder>().To<SessionBuilder>();
			Bind<ITaskieApplication>().To<TaskieApplication>();
			Bind<ITask>().To<ProcessCustomersTask>();
			Bind<ITaskieServiceLocator>().To<TaskieServiceLocator>();
		}
	}
}
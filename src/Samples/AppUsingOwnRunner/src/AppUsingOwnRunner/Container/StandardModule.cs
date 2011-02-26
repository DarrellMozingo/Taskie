using AppUsingOwnRunner.Data;
using AppUsingOwnRunner.Services;
using AppUsingOwnRunner.Tasks;
using Ninject.Modules;
using Taskie;

namespace AppUsingOwnRunner.Container
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
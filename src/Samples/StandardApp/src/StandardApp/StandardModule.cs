using Ninject.Modules;
using StandardApp.Services;
using StandardApp.Tasks;
using Taskie;

namespace StandardApp
{
	public class StandardModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ICustomerService>().To<CustomerService>();
			Bind<IApplication>().To<Application>();
			Bind<ITask>().To<ProcessCustomersTask>();
		}
	}
}
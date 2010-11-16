using Ninject.Modules;
using StandardSampleApplication.Services;

namespace StandardSampleApplication
{
	public class StandardModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ICustomerService>().To<CustomerService>();
		}
	}
}
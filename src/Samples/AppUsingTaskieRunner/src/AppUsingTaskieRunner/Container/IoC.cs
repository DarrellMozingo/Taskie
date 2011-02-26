using Ninject;

namespace AppUsingTaskieRunner.Container
{
	public static class IoC
	{
		public static IKernel Kernel;

		public static void Bootstrap()
		{
			Kernel = new StandardKernel(new StandardModule());
		}

		public static INSTANCE Resolve<INSTANCE>()
		{
			return Kernel.Get<INSTANCE>();
		}
	}
}
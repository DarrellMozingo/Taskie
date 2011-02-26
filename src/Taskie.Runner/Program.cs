using Taskie.Internal;

namespace Taskie.Runner
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var externalTaskieServiceLocator = TaskieServiceLocatorScanner.FindValidImplementation();
			TaskieRunner.RunWith(args, externalTaskieServiceLocator);
		}
	}
}
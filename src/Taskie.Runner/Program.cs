namespace Taskie.Runner
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var taskieServiceLocator = TaskieServiceLocatorScanner.Locate();
			TaskieRunner.RunWith(args, taskieServiceLocator);
		}
	}
}
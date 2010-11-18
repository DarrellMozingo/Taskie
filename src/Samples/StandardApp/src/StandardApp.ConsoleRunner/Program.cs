using Taskie;

namespace StandardApp.ConsoleRunner
{
	public class Program
	{
		public static void Main(string[] args)
		{
			IoC.Bootstrap();
			TaskieRunner.RunWith(args);
		}
	}
}
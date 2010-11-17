using System;
using Microsoft.Practices.ServiceLocation;

namespace Taskie
{
	public static class TaskieRunner
	{
		public static void RunWith(string[] arguments)
		{
			if (serviceLocatorIsNotSet())
			{
				throw new InvalidOperationException("You have to setup the SeriveLocator first with a call to ServiceLocator.SetLocatorProvider().");
			}
		}

		private static bool serviceLocatorIsNotSet()
		{
			try
			{
				return (ServiceLocator.Current != null);
			}
			catch (NullReferenceException)
			{
				return true;
			}
		}
	}
}
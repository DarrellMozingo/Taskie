using System;

namespace Taskie
{
	public class TaskieServiceLocatorScanner
	{
		public static ITaskieServiceLocator Locate()
		{
			// scan running directory for assemblies, load and look for implementation
			// throw helpful exception if one isn't found

			throw new NotImplementedException();
		}
	}
}
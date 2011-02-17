using System;
using System.IO;
using System.Linq;

namespace Taskie
{
	public class TaskieServiceLocatorScanner
	{
		public static ITaskieServiceLocator Locate()
		{
			var allAssemblies = Directory.GetFiles(Environment.CurrentDirectory, "*.dll", SearchOption.AllDirectories)
				.Where(assemblyName => assemblyName.EndsWith("Taskie.dll") == false);

			foreach (var assembly in allAssemblies)
			{
				try
				{
					return (ITaskieServiceLocator)Activator.CreateInstanceFrom(assembly, typeof(ITaskieServiceLocator).FullName).Unwrap();
				}
				catch
				{
					continue;
				}
			}

			throw new Exception("Couldn't find locator. WTF?");
		}
	}
}
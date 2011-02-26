using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Taskie.Internal
{
	public class TaskieServiceLocatorScanner
	{
		public static ITaskieServiceLocator Locate()
		{
			var allAssemblies = Directory.GetFiles(Environment.CurrentDirectory, "*.dll", SearchOption.AllDirectories);

			foreach (var assembly in allAssemblies)
			{
				var loadedAssembly = Assembly.LoadFrom(assembly);
				var locatorTypes = loadedAssembly.GetTypes().Where(type => type.Implements<ITaskieServiceLocator>());

				if (locatorTypes.Count() != 1)
				{
					continue;
				}

				var locatorType = locatorTypes.Single();

				try
				{
					return (ITaskieServiceLocator)Activator.CreateInstanceFrom(assembly, locatorType.FullName).Unwrap();
				}
				catch (Exception)
				{
					continue;
				}
			}

			throw new Exception("Couldn't find locator. WTF?");
		}
	}
}
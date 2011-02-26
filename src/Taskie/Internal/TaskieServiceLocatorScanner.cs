using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Taskie.Internal
{
	internal class TaskieServiceLocatorScanner
	{
		public static ITaskieServiceLocator FindValidImplementation()
		{
			var allAssemblies = Directory.GetFiles(Environment.CurrentDirectory, "*.dll", SearchOption.AllDirectories);

			foreach (var assembly in allAssemblies)
			{
				var loadedAssembly = Assembly.LoadFrom(assembly);
				var serviceLocatorTypesFromAssembly = loadedAssembly.GetTypes().Where(type => type.Implements<ITaskieServiceLocator>());

				if (serviceLocatorTypesFromAssembly.Count() != 1)
				{
					continue;
				}

				var serviceLocatorTypeFromAssembly = serviceLocatorTypesFromAssembly.Single();

				try
				{
					return (ITaskieServiceLocator)Activator.CreateInstanceFrom(assembly, serviceLocatorTypeFromAssembly.FullName).Unwrap();
				}
				catch
				{
					continue;
				}
			}

			var message = "Couldn't find an assembly with a public implementation of {0} in the current running directory ({1}). Check that and try again."
				.Substitute(typeof(ITaskieServiceLocator).Name, Environment.CurrentDirectory);

			throw new InvalidOperationException(message);
		}
	}
}
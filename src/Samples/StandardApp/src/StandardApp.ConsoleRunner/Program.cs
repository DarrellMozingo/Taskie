﻿using StandardApp.Container;
using Taskie;

namespace StandardApp.ConsoleRunner
{
	public class Program
	{
		public static void Main(string[] args)
		{
			IoC.Bootstrap();
			var serviceLocator = IoC.Resolve<IServiceLocator>();

			TaskieRunner.RunWith(args, serviceLocator);
		}
	}
}
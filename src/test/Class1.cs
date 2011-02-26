using System;
using System.Collections.Generic;
using Taskie;

namespace test
{
	public class Class1 : ITaskieServiceLocator
	{
		public INSTANCE GetInstance<INSTANCE>()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<INSTANCE> GetAllInstances<INSTANCE>()
		{
			return new[] { new ExternalTask() } as IEnumerable<INSTANCE>;
		}
	}

	public class ExternalTask : ITask
	{
		public void Run()
		{
			Console.WriteLine("running task!");
		}
	}
}
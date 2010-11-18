using System;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Taskie
{
	public class IoC
	{
		public static Action Bootstrap = () => ObjectFactory.Initialize(y => y.Scan(x =>
		                                                                            {
		                                                                            	x.TheCallingAssembly();
		                                                                            	x.WithDefaultConventions();
		                                                                            	x.LookForRegistries();

		                                                                            	x.ExcludeType<ITask>();
		                                                                            }));

		public static T Resolve<T>()
		{
			return ObjectFactory.GetInstance<T>();
		}

		public static void Inject<T>(T instance)
		{
			ObjectFactory.Inject(instance);
		}
	}
}
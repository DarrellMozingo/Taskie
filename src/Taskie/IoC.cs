using StructureMap;
using StructureMap.Configuration.DSL;

namespace Taskie
{
	public class IoC
	{
		private static bool _wasBootstrapped;

		public static void Bootstrap()
		{
			if (_wasBootstrapped)
			{
				return;
			}
			
			ObjectFactory.Initialize(y => y.Scan(x =>
			                                     {
			                                     	x.TheCallingAssembly();
			                                     	x.WithDefaultConventions();
			                                     }));

			_wasBootstrapped = true;
		}

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
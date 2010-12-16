using StructureMap.Configuration.DSL;

namespace Taskie.Container
{
	internal class StandardRegistry : Registry
	{
		public StandardRegistry()
		{
			For<IApplication>().Use<DefaultApplication>();
		}
	}
}
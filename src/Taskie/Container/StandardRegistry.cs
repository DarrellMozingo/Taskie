using StructureMap.Configuration.DSL;

namespace Taskie.Container
{
	public class StandardRegistry : Registry
	{
		public StandardRegistry()
		{
			For<IApplication>().Use<DefaultApplication>();
		}
	}
}
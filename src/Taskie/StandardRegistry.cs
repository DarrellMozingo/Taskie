using StructureMap.Configuration.DSL;

namespace Taskie
{
	public class StandardRegistry : Registry
	{
		public StandardRegistry()
		{
			For<IApplication>().Use<DefaultApplication>();
		}
	}
}
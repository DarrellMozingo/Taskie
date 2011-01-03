using System;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Taskie.Container
{
	internal class IoC
	{
		public static Func<IContainer> CreateContainer = () => new StructureMap.Container(y =>
		                                                                                  {
		                                                                                  	y.Scan(x =>
		                                                                                  	       {
		                                                                                  	       	x.AssemblyContainingType<IoC>();
		                                                                                  	       	x.WithDefaultConventions();

		                                                                                  	       	x.ExcludeType<ITask>();
		                                                                                  	       });

		                                                                                  	y.AddRegistry<StandardRegistry>();
		                                                                                  });
	}
}
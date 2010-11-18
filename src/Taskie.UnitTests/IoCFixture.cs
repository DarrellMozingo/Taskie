using NUnit.Framework;
using StructureMap;

namespace Taskie.UnitTests
{
	[TestFixture]
	public class IoCFixture
	{
		[Test]
		public void Should_build_a_valid_container_configuration()
		{
			IoC.Bootstrap();
			ObjectFactory.AssertConfigurationIsValid();
		}
	}
}
using NUnit.Framework;
using StructureMap;
using Taskie.UnitTests.TestingHelpers;

namespace Taskie.UnitTests
{
	[TestFixture]
	public class When_boot_strapping_the_IoC_container : SpecBase
	{
		protected override void because()
		{
			IoC.Bootstrap();
		}

		[Test]
		public void Should_build_a_valid_container_configuration()
		{
			ObjectFactory.AssertConfigurationIsValid();
		}
	}
}
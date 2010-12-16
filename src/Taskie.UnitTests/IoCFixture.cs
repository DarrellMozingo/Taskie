using FakeItEasy;
using NUnit.Framework;
using StructureMap;
using Taskie.Container;
using Taskie.UnitTests.TestingHelpers;

namespace Taskie.UnitTests
{
	[TestFixture]
	public class When_boot_strapping_the_IoC_container : SpecBase
	{
		protected override void because()
		{
			IoC.Bootstrap();
			IoC.Inject(A.Fake<IServiceLocator>());	// Normally provided by the calling app.
		}

		[Test]
		public void Should_build_a_valid_container_configuration()
		{
			ObjectFactory.AssertConfigurationIsValid();
		}
	}
}
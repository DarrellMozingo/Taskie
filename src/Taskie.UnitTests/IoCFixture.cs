using NUnit.Framework;
using StructureMap;
using Taskie.Container;
using Taskie.UnitTests.TestingHelpers;

namespace Taskie.UnitTests
{
	[TestFixture]
	public class When_creating_the_IoC_container : SpecBase
	{
		private IContainer _container;

		protected override void because()
		{
			_container = IoC.CreateContainer();
		}

		[Test]
		public void Should_build_a_valid_container_configuration()
		{
			_container.AssertConfigurationIsValid();
		}
	}
}
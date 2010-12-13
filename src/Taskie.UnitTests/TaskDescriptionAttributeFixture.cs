using NUnit.Framework;
using Taskie.UnitTests.TestingHelpers;

namespace Taskie.UnitTests
{
	public class TaskDescriptionAttributeFixture
	{
		[TestFixture]
		public class When_instantiating_the_attribute : SpecBase
		{
			private TaskDescriptionAttribute _attribute;

			protected override void because()
			{
				_attribute = new TaskDescriptionAttribute("description");
			}

			[Test]
			public void Should_set_the_description_property()
			{
				_attribute.Description.ShouldEqual("description");
			}
		}
	}
}
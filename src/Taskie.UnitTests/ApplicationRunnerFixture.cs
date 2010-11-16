using FakeItEasy;
using NUnit.Framework;
using Taskie.UnitTests.TestingHelpers;

namespace Taskie.UnitTests
{
	public class ApplicationRunnerFixture
	{
		[TestFixture]
		public class When_disposing_of_the_main_application_runner : SpecBase
		{
			private readonly IApplication _fakeApplicaton = A.Fake<IApplication>();

			protected override void because()
			{
				new ApplicationRunner(_fakeApplicaton).Dispose();
			}

			[Test]
			public void Should_shut_down_the_application()
			{
				A.CallTo(() => _fakeApplicaton.Shutdown()).MustHaveHappened();
			}
		}
	}
}
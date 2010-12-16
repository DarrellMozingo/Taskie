using System;
using FakeItEasy;
using NUnit.Framework;
using Taskie.Container;
using Taskie.UnitTests.TestingHelpers;

namespace Taskie.UnitTests
{
	public class TaskieRunnerFixture
	{
		[TestFixture]
		public class When_running_Taskie_without_passing_in_a_service_locator_implementation : SpecBase
		{
			private Action _runningTaskieWithoutSerivceLocator;
			
			protected override void because()
			{
				_runningTaskieWithoutSerivceLocator = () => TaskieRunner.RunWith(new string[0], null);
			}

			[Test]
			public void Should_throw_an_argument_null_exception()
			{
				_runningTaskieWithoutSerivceLocator.ShouldThrowAn<ArgumentNullException>();
			}
		}

		[TestFixture]
		public class When_running_Taskie_with_a_valid_service_locator_implementation : SpecBase
		{
			private readonly string[] _arguments = new[] { "arg1", "arg2" };
			private readonly IServiceLocator _fakeServiceLocator = A.Fake<IServiceLocator>();
			private readonly IApplicationRunner _fakeApplicationRunner = A.Fake<IApplicationRunner>();
			private bool _wasBootstrapped;

			protected override void context()
			{
				IoC.Bootstrap = () => _wasBootstrapped = true;
				IoC.Inject(_fakeApplicationRunner);
			}

			protected override void because()
			{
				TaskieRunner.RunWith(_arguments, _fakeServiceLocator);
			}

			[Test]
			public void Should_boot_strap_IoC_container()
			{
				_wasBootstrapped.ShouldBeTrue();
			}

			[Test]
			public void Should_inject_the_service_locator_into_the_IoC_container_for_future_use()
			{
				Action gettingServiceLocatorInstance = () => IoC.Resolve<IServiceLocator>();
				gettingServiceLocatorInstance.ShouldNotThrowAnyExceptions();
			}

			[Test]
			public void Should_resolve_the_application_runner_and_pass_off_the_provided_argument_list()
			{
				A.CallTo(() => _fakeApplicationRunner.RunWith(_arguments)).MustHaveHappened();
			}

			[Test]
			public void Should_dispose_of_the_application_runner()
			{
				A.CallTo(() => _fakeApplicationRunner.Dispose()).MustHaveHappened();
			}
		}
	}
}
using System;
using FakeItEasy;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using Taskie.UnitTests.TestingHelpers;

namespace Taskie.UnitTests
{
	public class TaskieRunnerFixture
	{
		[TestFixture]
		public class When_running_Taskie_without_having_first_setup_the_Service_Locator : SpecBase
		{
			Action _runningTaskieWithoutSerivceLocator;

			protected override void context()
			{
				ServiceLocator.SetLocatorProvider(null);
			}

			protected override void because()
			{
				_runningTaskieWithoutSerivceLocator = () => TaskieRunner.RunWith(new string[0]);
			}

			[Test]
			public void Should_throw_an_invalid_operation_exception_with_a_message()
			{
				_runningTaskieWithoutSerivceLocator
					.ShouldThrowAn<InvalidOperationException>()
					.Message.ShouldNotBeBlank();
			}
		}

		[TestFixture]
		public class When_running_Taskie_with_the_Service_Locator_all_setup : SpecBase
		{
			readonly string[] _arguments = new[] { "arg1", "arg2" };
			readonly IApplicationRunner _fakeApplicationRunner = A.Fake<IApplicationRunner>();
			bool _wasBootstrapped;

			protected override void context()
			{
				ServiceLocator.SetLocatorProvider(A.Fake<IServiceLocator>);
				IoC.Bootstrap = () => _wasBootstrapped = true;
				IoC.Inject(_fakeApplicationRunner);
			}

			protected override void because()
			{
				TaskieRunner.RunWith(_arguments);
			}

			[Test]
			public void Should_boot_strap_IoC_container()
			{
				_wasBootstrapped.ShouldBeTrue();
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
using System;
using FakeItEasy;
using NUnit.Framework;
using Taskie.UnitTests.TestingHelpers;

namespace Taskie.UnitTests
{
	public class ApplicationRunnerFixture
	{
		[TestFixture]
		public class When_constructing_the_main_application_runner_and_an_implementation_of_the_application_is_available_from_the_provided_service_locator_implemenation : SpecBase
		{
			private readonly IServiceLocator _fakeServiceLocator = A.Fake<IServiceLocator>();
			private Action settingUpApplication;

			protected override void context()
			{
				var fakeApplication = A.Fake<IApplication>();
				A.CallTo(() => fakeApplication.Startup()).Throws(new Exception("from_service_locator"));

				A.CallTo(() => _fakeServiceLocator.GetInstance<IApplication>()).Returns(fakeApplication);
			}

			protected override void because()
			{
				settingUpApplication = () => new ApplicationRunner(null, _fakeServiceLocator, null);
			}

			[Test]
			public void Should_use_the_application_implementation_from_the_service_locator_implementation()
			{
				settingUpApplication.ShouldThrowAn<Exception>()
					.Message.ShouldEqual("from_service_locator");
			}
		}

		[TestFixture]
		public class When_constructing_the_main_application_runner_and_an_implementation_of_the_application_is_not_available_from_the_Service_Locator : SpecBase
		{
			private readonly IServiceLocator _fakeServiceLocator = A.Fake<IServiceLocator>();
			private readonly IApplication _fakeApplication = A.Fake<IApplication>();
			private Action settingUpApplication;

			protected override void context()
			{
				A.CallTo(() => _fakeApplication.Startup()).Throws(new Exception("from_internal_container"));
				A.CallTo(() => _fakeServiceLocator.GetInstance<IApplication>()).Returns(null);
			}

			protected override void because()
			{
				settingUpApplication = () => new ApplicationRunner(null, _fakeServiceLocator, _fakeApplication);
			}

			[Test]
			public void Should_use_the_default_application_implementation_from_the_internal_container()
			{
				settingUpApplication.ShouldThrowAn<Exception>()
					.Message.ShouldEqual("from_internal_container");
			}
		}

		public class Having_an_application_instance_available : SpecBase
		{
			protected readonly IServiceLocator _fakeServiceLocator = A.Fake<IServiceLocator>();
			protected readonly IApplication _fakeApplication = A.Fake<IApplication>();
			protected readonly ICommandLineParser _fakeCommandLineParser = A.Fake<ICommandLineParser>();
			protected IApplicationRunner _applicationRunner;

			protected override void infrastructure_setup()
			{
				A.CallTo(() => _fakeServiceLocator.GetInstance<IApplication>()).Returns(null);

				_applicationRunner = new ApplicationRunner(_fakeCommandLineParser, _fakeServiceLocator, _fakeApplication);
			}
		}

		[TestFixture]
		public class When_disposing_of_the_main_application_runner : Having_an_application_instance_available
		{
			protected override void because()
			{
				_applicationRunner.Dispose();
			}

			[Test]
			public void Should_shut_down_the_application()
			{
				A.CallTo(() => _fakeApplication.Shutdown()).MustHaveHappened();
			}
		}

		[TestFixture]
		public class When_being_ran_with_a_valid_task : Having_an_application_instance_available
		{
			private readonly string[] _argumentsForValidTask = new[] { "valid_task" };
			private readonly ITask _fakeTask = A.Fake<ITask>();

			protected override void context()
			{
				A.CallTo(() => _fakeCommandLineParser.GetRequestedTask(_argumentsForValidTask)).Returns(_fakeTask);
			}

			protected override void because()
			{
				_applicationRunner.RunWith(_argumentsForValidTask);
			}

			[Test]
			public void Should_run_the_task()
			{
				A.CallTo(() => _fakeTask.Run()).MustHaveHappened();
			}
		}
	}
}
using System;
using FakeItEasy;
using NUnit.Framework;
using Taskie.UnitTests.TestingHelpers;
using Microsoft.Practices.ServiceLocation;

namespace Taskie.UnitTests
{
	public class ApplicationRunnerFixture
	{
		[TestFixture]
		public class When_constructing_the_main_application_runner_and_an_implementation_of_the_application_is_available_from_the_Service_Locator : SpecBase
		{
			private Action settingUpApplication;

			protected override void context()
			{
				var fakeApplication = A.Fake<IApplication>();
				A.CallTo(() => fakeApplication.Startup()).Throws(new Exception("from_service_locator"));

				var fakeServiceLocator = A.Fake<IServiceLocator>();
				A.CallTo(() => fakeServiceLocator.GetInstance<IApplication>()).Returns(fakeApplication);

				ServiceLocator.SetLocatorProvider(() => fakeServiceLocator);
			}

			protected override void because()
			{
				settingUpApplication = () => new ApplicationRunner();
			}

			[Test]
			public void Should_use_the_application_implementation_from_the_Service_Locator()
			{
				settingUpApplication.ShouldThrowAn<Exception>()
					.Message.ShouldEqual("from_service_locator");
			}
		}

		[TestFixture]
		public class When_constructing_the_main_application_runner_and_an_implementation_of_the_application_is_not_available_from_the_Service_Locator : SpecBase
		{
			private Action settingUpApplication;

			protected override void context()
			{
				var fakeApplication = A.Fake<IApplication>();
				A.CallTo(() => fakeApplication.Startup()).Throws(new Exception("from_internal_container"));

				IoC.Inject(fakeApplication);

				var fakeServiceLocator = A.Fake<IServiceLocator>();
				A.CallTo(() => fakeServiceLocator.GetInstance<IApplication>()).Throws(new ActivationException());

				ServiceLocator.SetLocatorProvider(() => fakeServiceLocator);
			}

			protected override void because()
			{
				settingUpApplication = () => new ApplicationRunner();
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
			protected readonly IApplication _fakeApplication = A.Fake<IApplication>();

			protected override void infrastructure_setup()
			{
				var fakeServiceLocator = A.Fake<IServiceLocator>();
				A.CallTo(() => fakeServiceLocator.GetInstance<IApplication>()).Returns(_fakeApplication);

				ServiceLocator.SetLocatorProvider(() => fakeServiceLocator);
			}
		}

		[TestFixture]
		public class When_disposing_of_the_main_application_runner : Having_an_application_instance_available
		{
			protected override void because()
			{
				new ApplicationRunner().Dispose();
			}

			[Test]
			public void Should_shut_down_the_application()
			{
				A.CallTo(() => _fakeApplication.Shutdown()).MustHaveHappened();
			}
		}
	}
}
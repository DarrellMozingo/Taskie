using System;
using NUnit.Framework;
using Taskie.UnitTests.TestingHelpers;

namespace Taskie.UnitTests
{
	public class TaskieRunnerFixture
	{
		[TestFixture]
		public class When_running_Taskie_without_having_first_setup_the_Service_Locater : SpecBase
		{
			private Action _runningTaskieWithoutSerivceLocator;

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
	}
}
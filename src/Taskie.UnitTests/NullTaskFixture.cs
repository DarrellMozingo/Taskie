using System;
using NUnit.Framework;
using Taskie.UnitTests.TestingHelpers;

namespace Taskie.UnitTests
{
	[TestFixture]
	public class When_running_a_null_task : SpecBase
	{
		private Action _runningNullTask;

		protected override void because()
		{
			_runningNullTask = () => new NullTask().Run();
		}

		[Test]
		public void Should_not_do_anything()
		{
			_runningNullTask.ShouldNotThrowAnyExceptions();
		}
	}
}
using NUnit.Framework;
using Taskie.UnitTests.TestingHelpers;

namespace Taskie.UnitTests
{
	[TestFixture]
	public class TaskMetaHelperFixture
	{
		[Test]
		public void Should_get_the_proper_task_name_from_a_given_task()
		{
			TaskMetaHelper.GetFriendlyNameFor(new FakeTask())
				.ShouldEqual("Fake");
		}

		[Test]
		public void Should_get_the_full_task_type_name_from_a_given_friendly_name()
		{
			TaskMetaHelper.GetFullNameFrom("Fake")
				.ShouldEqual("FakeTask");
		}

		[Test]
		public void Should_get_the_description_off_a_given_task()
		{
			TaskMetaHelper.GetDescriptionFor(new FakeWithDescriptionTask())
				.ShouldEqual("fake_task_description");
		}

		[Test]
		public void Should_return_a_default_description_if_one_is_not_provided_for_the_given_task()
		{
			TaskMetaHelper.GetDescriptionFor(new FakeTask())
				.ShouldEqual(TaskMetaHelper.NoTaskDescriptionGivenMessage);
		}

		private class FakeTask : ITask
		{
			public void Run()
			{
			}
		}

		[TaskDescription("fake_task_description")]
		private class FakeWithDescriptionTask : ITask
		{
			public void Run()
			{
			}
		}
	}
}
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using Taskie.UnitTests.TestingHelpers;

namespace Taskie.UnitTests
{
	public class TaskResolverFixture
	{
		[TestFixture]
		public class When_resolving_a_task_using_the_friendly_name_and_there_is_no_match : SpecBase
		{
			private IServiceLocator _fakeServiceLocator = A.Fake<IServiceLocator>();
			private ITask _resolvedTask;

			protected override void context()
			{
				A.CallTo(() => _fakeServiceLocator.GetAllInstances<ITask>()).ReturnsEnumerableContaining(new FooTask());
			}

			protected override void because()
			{
				_resolvedTask = new TaskResolver(_fakeServiceLocator).ResolveTask("bar");
			}

			[Test]
			public void Should_return_null_to_indicate_a_non_match()
			{
				_resolvedTask.ShouldBeNull();
			}

			private class FooTask : ITask
			{
				public void Run()
				{
				}
			}
		}

		[TestFixture]
		public class When_resolving_a_task_using_the_friendly_name_and_there_is_a_match : SpecBase
		{
			private IServiceLocator _fakeServiceLocator = A.Fake<IServiceLocator>();
			private ITask _resolvedTask;

			protected override void context()
			{
				A.CallTo(() => _fakeServiceLocator.GetAllInstances<ITask>()).ReturnsEnumerableContaining(new FooTask());
			}

			protected override void because()
			{
				_resolvedTask = new TaskResolver(_fakeServiceLocator).ResolveTask("foo");
			}

			[Test]
			public void Should_return_the_specified_task_using_a_non_case_match_on_the_friendly_name()
			{
				_resolvedTask.ShouldBeAnInstanceOf<FooTask>();
			}

			private class FooTask : ITask
			{
				public void Run()
				{
				}
			}
		}

		[TestFixture]
		public class When_getting_all_runnable_tasks_and_the_only_task_has_a_description : SpecBase
		{
			private IServiceLocator _fakeServiceLocator = A.Fake<IServiceLocator>();
			private IEnumerable<TaskInformation> _allRunnableTasks;

			protected override void context()
			{
				A.CallTo(() => _fakeServiceLocator.GetAllInstances<ITask>()).ReturnsEnumerableContaining(new TaskWithDescriptionTask());
			}

			protected override void because()
			{
				_allRunnableTasks = new TaskResolver(_fakeServiceLocator).GetAllRunnableTasks();
			}

			[Test]
			public void Should_set_the_task_name()
			{
				_allRunnableTasks.First().Name.ShouldEqual("TaskWithDescription");
			}

			[Test]
			public void Should_set_the_description()
			{
				_allRunnableTasks.First().Description.ShouldEqual("task_description");
			}

			[TaskDescription("task_description")]
			private class TaskWithDescriptionTask : ITask
			{
				public void Run()
				{
				}
			}
		}

		[TestFixture]
		public class When_getting_all_runnable_tasks_and_the_only_task_does_not_have_a_description : SpecBase
		{
			private IServiceLocator _fakeServiceLocator = A.Fake<IServiceLocator>();
			private IEnumerable<TaskInformation> _allRunnableTasks;

			protected override void context()
			{
				A.CallTo(() => _fakeServiceLocator.GetAllInstances<ITask>()).ReturnsEnumerableContaining(new TaskWithoutDescriptionTask());
			}

			protected override void because()
			{
				_allRunnableTasks = new TaskResolver(_fakeServiceLocator).GetAllRunnableTasks();
			}

			[Test]
			public void Should_set_the_task_name()
			{
				_allRunnableTasks.First().Name.ShouldEqual("TaskWithoutDescription");
			}

			[Test]
			public void Should_set_the_default_description()
			{
				_allRunnableTasks.First().Description.ShouldEqual(TaskMetaHelper.NoTaskDescriptionGivenMessage);
			}

			private class TaskWithoutDescriptionTask : ITask
			{
				public void Run()
				{
				}
			}
		}
	}
}
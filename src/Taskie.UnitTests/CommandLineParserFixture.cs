using FakeItEasy;
using NUnit.Framework;
using Taskie.UnitTests.TestingHelpers;

namespace Taskie.UnitTests
{
	public class CommandLineParserFixture
	{
		public class Using_the_command_line_parser : SpecBase
		{
			protected const string _runnableTaskName = "runnable_task_name";
			protected const string _runnableTaskDescription = "runnable_task_description";

			protected readonly IScreenIO _fakeScreenIO = A.Fake<IScreenIO>();
			protected readonly ITaskResolver _fakeTaskResolver = A.Fake<ITaskResolver>();
			protected ICommandLineParser _commandLineParser;

			protected ITask _returnedTask;

			protected override void infrastructure_setup()
			{
				A.CallTo(() => _fakeTaskResolver.GetAllRunnableTasks()).Returns(new[]
				                                                                	{
				                                                                		new TaskInformation
				                                                                			{
				                                                                				Name = _runnableTaskName,
				                                                                				Description = _runnableTaskDescription
				                                                                			}
				                                                                	});

				_commandLineParser = new CommandLineParser(_fakeScreenIO, _fakeTaskResolver);
			}
		}

		[TestFixture]
		public class When_passed_an_empty_agrument_list : Using_the_command_line_parser
		{
			protected override void because()
			{
				_returnedTask = _commandLineParser.GetRequestedTask(new string[0]);
			}

			[Test]
			public void Should_print_usage_instructions_to_the_screen()
			{
				A.CallTo(() => _fakeScreenIO.WriteLine(A<string>.That.Contains("Usage"))).MustHaveHappened();
			}

			[Test]
			public void Should_print_all_runnable_task_name_and_descriptions_to_the_screen()
			{
				A.CallTo(() => _fakeScreenIO.WriteLine(A<string>.That.Contains(_runnableTaskName).And.Contains(_runnableTaskDescription))).MustHaveHappened();
			}

			[Test]
			public void Should_return_a_null_task()
			{
				_returnedTask.ShouldBeAnInstanceOf<NullTask>();
			}
		}

		[TestFixture]
		public class When_passed_an_argument_list_with_one_argument : Using_the_command_line_parser
		{
			protected override void because()
			{
				_returnedTask = _commandLineParser.GetRequestedTask(new[] { "single_argument" });
			}

			[Test]
			public void Should_print_usage_instruactions_to_the_screen()
			{
				A.CallTo(() => _fakeScreenIO.WriteLine(A<string>.That.Contains("Usage"))).MustHaveHappened();
			}

			[Test]
			public void Should_print_all_runnable_task_name_and_descriptions_to_the_screen()
			{
				A.CallTo(() => _fakeScreenIO.WriteLine(A<string>.That.Contains(_runnableTaskName).And.Contains(_runnableTaskDescription))).MustHaveHappened();
			}

			[Test]
			public void Should_return_a_null_task()
			{
				_returnedTask.ShouldBeAnInstanceOf<NullTask>();
			}
		}

		[TestFixture]
		public class When_passed_an_argument_list_with_two_arguments_and_the_first_argument_is_incorrect : Using_the_command_line_parser
		{
			protected override void because()
			{
				_returnedTask = _commandLineParser.GetRequestedTask(new[] { "incorrect_first_argument", "second_argument" });
			}

			[Test]
			public void Should_print_usage_instruactions_to_the_screen()
			{
				A.CallTo(() => _fakeScreenIO.WriteLine(A<string>.That.Contains("Usage"))).MustHaveHappened();
			}

			[Test]
			public void Should_print_all_runnable_task_name_and_descriptions_to_the_screen()
			{
				A.CallTo(() => _fakeScreenIO.WriteLine(A<string>.That.Contains(_runnableTaskName).And.Contains(_runnableTaskDescription))).MustHaveHappened();
			}

			[Test]
			public void Should_return_a_null_task()
			{
				_returnedTask.ShouldBeAnInstanceOf<NullTask>();
			}
		}

		[TestFixture]
		public class When_passed_an_unknown_task_name_to_run : Using_the_command_line_parser
		{
			protected override void context()
			{
				A.CallTo(() => _fakeTaskResolver.ResolveTask("unknown_task")).Returns(null);
			}

			protected override void because()
			{
				_returnedTask = _commandLineParser.GetRequestedTask(new[] { CommandLineParser.RunCommand, "unknown_task" });
			}

			[Test]
			public void Should_print_an_error_message_about_the_unknown_task_to_the_screen()
			{
				A.CallTo(() => _fakeScreenIO.WriteLine(A<string>.That.Contains("Sorry").And.Contains("unknown_task"))).MustHaveHappened();
			}

			[Test]
			public void Should_return_a_null_task()
			{
				_returnedTask.ShouldBeAnInstanceOf<NullTask>();
			}
		}

		[TestFixture]
		public class When_passed_an_known_task_name_to_run : Using_the_command_line_parser
		{
			protected override void context()
			{
				A.CallTo(() => _fakeTaskResolver.ResolveTask("known_task")).Returns(new FakeTask());
			}

			protected override void because()
			{
				_returnedTask = _commandLineParser.GetRequestedTask(new[] { CommandLineParser.RunCommand, "known_task" });
			}

			[Test]
			public void Should_return_the_task()
			{
				_returnedTask.ShouldBeAnInstanceOf<FakeTask>();
			}

			private class FakeTask : ITask
			{
				public void Run()
				{
				}
			}
		}
	}
}
using System;
using System.CodeDom.Compiler;
using System.IO;
using Microsoft.CSharp;
using NUnit.Framework;
using Taskie.Internal;
using Taskie.UnitTests.TestingHelpers;

namespace Taskie.UnitTests.Internal
{
	[TestFixture]
	public class TaskieServiceLocatorScannerFixture
	{
		[Test]
		public void Should_scan_all_local_assemblies_for_the_one_that_implements_the_service_locator()
		{
			using (TestAssemblyGenerator.GenerateValidAssemblyThatThrows("expected_message"))
			{
				shouldFindLocatorThatThrows("expected_message");
			}
		}

		[Test]
		public void Should_skip_assemblies_that_contain_multiple_implementations_of_the_service_locator()
		{
			using (TestAssemblyGenerator.GenerateInvalidAssemblyWithTwoImplementationsThatThrows("expected_message_from_invalid_assembly"))
			{
				using (TestAssemblyGenerator.GenerateValidAssemblyThatThrows("expected_message_from_valid_assembly"))
				{
					shouldFindLocatorThatThrows("expected_message_from_valid_assembly");
				}
			}
		}

		private static void shouldFindLocatorThatThrows(string expectedExceptionMessage)
		{
			var locator = TaskieServiceLocatorScanner.Locate();

			Action runningLocator = () => locator.GetInstance<string>();

			runningLocator.ShouldThrowAn<Exception>()
				.Message.ShouldEqual(expectedExceptionMessage);
		}

		private class TestAssemblyGenerator : IDisposable
		{
			private static readonly Type _serviceLocatorType = typeof(ITaskieServiceLocator);
			private string _assemblyPath;

			private static readonly string _usingStatementCode = "using System; using System.Collections.Generic; using {0};".Substitute(_serviceLocatorType.Namespace);

			private static readonly string _serviceLocatorCode = "public class {0} : " + _serviceLocatorType.Name + " {{" +
			                                                     "public INSTANCE GetInstance<INSTANCE>() {{ throw new Exception(\"{1}\"); }}" +
			                                                     "public IEnumerable<INSTANCE> GetAllInstances<INSTANCE>() {{ throw new Exception(); }} }}";

			private static IDisposable generateAssembly(string code)
			{
				var parameters = new CompilerParameters
				                 	{
				                 		OutputAssembly = Guid.NewGuid() + ".dll",
				                 		GenerateExecutable = false
				                 	};

				parameters.ReferencedAssemblies.Add(_serviceLocatorType.Assembly.ManifestModule.Name);

				var results = new CSharpCodeProvider().CompileAssemblyFromSource(parameters, code);
				results.Errors.HasErrors.ShouldBeFalse();

				return new TestAssemblyGenerator { _assemblyPath = results.PathToAssembly };
			}

			public static IDisposable GenerateValidAssemblyThatThrows(string exceptionMessage)
			{
				var code = _usingStatementCode +
				           _serviceLocatorCode.Substitute("random_name_for_implementation", exceptionMessage);

				return generateAssembly(code);
			}

			public static IDisposable GenerateInvalidAssemblyWithTwoImplementationsThatThrows(string exceptionMessage)
			{
				var code = _usingStatementCode +
				           _serviceLocatorCode.Substitute("random_name_for_implementation_1", exceptionMessage) +
				           _serviceLocatorCode.Substitute("random_name_for_implementation_2", exceptionMessage);

				return generateAssembly(code);
			}

			public void Dispose()
			{
				if (File.Exists(_assemblyPath))
				{
					File.Delete(_assemblyPath);
				}
			}
		}
	}
}
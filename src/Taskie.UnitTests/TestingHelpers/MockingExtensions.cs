using System.Collections.Generic;
using FakeItEasy.Configuration;

namespace Taskie.UnitTests.TestingHelpers
{
	public static class MockingExtensions
	{
		public static IAfterCallSpecifiedWithOutAndRefParametersConfiguration ReturnsEnumerableContaining<T>(this IReturnValueConfiguration<IEnumerable<T>> configuration, params T[] values)
		{
			return configuration.ReturnsLazily(x => new List<T>(values));
		}
	}
}
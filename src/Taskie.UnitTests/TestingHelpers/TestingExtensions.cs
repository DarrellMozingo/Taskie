using System;
using NUnit.Framework;

namespace Taskie.UnitTests.TestingHelpers
{
	public static class TestingExtensions
	{
		public static T ShouldEqual<T>(this T actual, T expected)
		{
			Assert.AreEqual(expected, actual);
			return actual;
		}

		public static T ShouldNotEqual<T>(this T actual, T expected)
		{
			Assert.AreNotEqual(expected, actual);
			return actual;
		}

		public static object ShouldNotBeNull(this object item, string message)
		{
			Assert.IsNotNull(item, message);
			return item;
		}

		public static string ShouldNotBeBlank(this string item)
		{
			Assert.AreNotEqual(string.Empty, item);
			return item;
		}

		public static T ShouldBeAnInstanceOf<T>(this object item)
		{
			Assert.IsInstanceOf(typeof(T), item);
			return (T)item;
		}

		public static ExceptionType ShouldThrowAn<ExceptionType>(this Action work_to_perform)
			where ExceptionType : Exception
		{
			var resultingException = getExceptionFromPerforming(work_to_perform);

			resultingException.ShouldNotBeNull("No exception was thrown, but expected one to be.");
			resultingException.ShouldBeAnInstanceOf<ExceptionType>();

			return (ExceptionType)resultingException;
		}

		private static Exception getExceptionFromPerforming(Action work)
		{
			try
			{
				work();
				return null;
			}
			catch (Exception ex)
			{
				return ex;
			}
		}
	}
}
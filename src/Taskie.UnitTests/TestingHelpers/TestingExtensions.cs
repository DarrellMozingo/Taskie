using System;
using NUnit.Framework;

namespace Taskie.UnitTests.TestingHelpers
{
	public static class TestingExtensions
	{
		public static void ShouldEqual<T>(this T actual, T expected)
		{
			Assert.AreEqual(expected, actual);
		}

		public static void ShouldNotEqual<T>(this T actual, T expected)
		{
			Assert.AreNotEqual(expected, actual);
		}

		public static void ShouldBeNull(this object item)
		{
			Assert.IsNull(item);
		}

		public static void ShouldNotBeNull(this object item, string message)
		{
			Assert.IsNotNull(item, message);
		}

		public static void ShouldBeTrue(this bool item)
		{
			item.ShouldEqual(true);
		}

		public static void ShouldNotBeBlank(this string item)
		{
			Assert.AreNotEqual(string.Empty, item);
		}

		public static void ShouldBeAnInstanceOf<T>(this object item)
		{
			Assert.IsInstanceOf(typeof(T), item);
		}

		public static void ShouldNotThrowAnyExceptions(this Action workToPerform)
		{
			workToPerform();
		}

		public static ExceptionType ShouldThrowAn<ExceptionType>(this Action workToPerform)
			where ExceptionType : Exception
		{
			var resultingException = getExceptionFromPerforming(workToPerform);

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
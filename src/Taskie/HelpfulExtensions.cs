using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Taskie
{
	internal static class HelpfulExtensions
	{
		public static string Substitute(this string value, params object[] arguments)
		{
			return string.Format(value, arguments);
		}

		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			foreach (var item in enumerable)
			{
				action(item);
			}
		}

		public static T GetAttribute<T>(this MemberInfo member) where T : Attribute
		{
			return (T)member.GetCustomAttributes(false)
				.Where(attribute => attribute.GetType() == typeof(T))
				.SingleOrDefault();
		}

		public static string TrimEnd(this string source, string toTrim)
		{
			var lastPosition = source.LastIndexOf(toTrim);

			return source.Substring(0, lastPosition);
		}

		public static bool EqualsIgnoringCase(this string source, string stringToMatch)
		{
			return source.Equals(stringToMatch, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
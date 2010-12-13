using System;
using System.Collections.Generic;

namespace Taskie
{
	public static class HelpfulExtensions
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
	}
}
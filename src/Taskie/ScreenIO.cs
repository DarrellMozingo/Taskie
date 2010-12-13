using System;

namespace Taskie
{
	public interface IScreenIO
	{
		void WriteLine(string text);
	}

	public class ScreenIO : IScreenIO
	{
		public void WriteLine(string value)
		{
			Console.WriteLine(value);
		}
	}
}
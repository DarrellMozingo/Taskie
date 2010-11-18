using System;

namespace StandardApp.Data
{
	public interface ISessionBuilder
	{
		void BuildSession();
	}

	public class SessionBuilder : ISessionBuilder
	{
		public void BuildSession()
		{
			Console.WriteLine("Building NHibernate session...");
		}
	}
}
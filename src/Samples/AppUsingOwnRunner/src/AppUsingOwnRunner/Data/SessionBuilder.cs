using System;

namespace AppUsingOwnRunner.Data
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
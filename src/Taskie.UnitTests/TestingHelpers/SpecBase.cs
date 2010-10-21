using NUnit.Framework;

namespace Taskie.UnitTests.TestingHelpers
{
	public class SpecBase
	{
		[TestFixtureSetUp]
		public void Only_once_before_any_specification_is_ran()
		{
			context();
			because();
		}

		protected virtual void context()
		{
		}

		protected virtual void because()
		{
		}
	}
}
using System.Collections.Generic;

namespace Taskie
{
	public interface IServiceLocator
	{
		INSTANCE GetInstance<INSTANCE>();
		IEnumerable<INSTANCE> GetAllInstances<INSTANCE>();
	}
}
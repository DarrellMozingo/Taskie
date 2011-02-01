using System.Collections.Generic;

namespace Taskie
{
	public interface ITaskieServiceLocator
	{
		INSTANCE GetInstance<INSTANCE>();
		IEnumerable<INSTANCE> GetAllInstances<INSTANCE>();
	}
}
using System.Collections.Generic;

namespace Taskie
{
	public interface ITaskResolver
	{
		ITask ResolveTask(string friendlyTaskName);
		IEnumerable<TaskInformation> GetAllRunnableTasks();
	}

	public class TaskResolver : ITaskResolver
	{
		public ITask ResolveTask(string friendlyTaskName)
		{
			return null;
		}

		public IEnumerable<TaskInformation> GetAllRunnableTasks()
		{
			return null;
		}
	}
}
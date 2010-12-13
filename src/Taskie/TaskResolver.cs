using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;

namespace Taskie
{
	public interface ITaskResolver
	{
		ITask ResolveTask(string friendlyTaskName);
		IEnumerable<TaskInformation> GetAllRunnableTasks();
	}

	public class TaskResolver : ITaskResolver
	{
		private readonly IEnumerable<ITask> _allRunnableTasks;

		public TaskResolver()
		{
			_allRunnableTasks = ServiceLocator.Current.GetAllInstances<ITask>();
		}

		public ITask ResolveTask(string friendlyTaskName)
		{
			return _allRunnableTasks
				.Where(task => TaskMetaHelper.GetFriendlyNameFor(task).EqualsIgnoringCase(friendlyTaskName))
				.SingleOrDefault();
		}

		public IEnumerable<TaskInformation> GetAllRunnableTasks()
		{
			foreach (var task in _allRunnableTasks)
			{
				yield return new TaskInformation
				             	{
				             		Name = TaskMetaHelper.GetFriendlyNameFor(task),
				             		Description = TaskMetaHelper.GetDescriptionFor(task)
				             	};
			}
		}
	}
}
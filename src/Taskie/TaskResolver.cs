using System.Collections.Generic;
using System.Linq;

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

		public TaskResolver(IServiceLocator serviceLocator)
		{
			_allRunnableTasks = serviceLocator.GetAllInstances<ITask>();
		}

		public ITask ResolveTask(string friendlyTaskName)
		{
			return _allRunnableTasks
				.Where(task => TaskMetaHelper.GetFriendlyNameFor(task).EqualsIgnoringCase(friendlyTaskName))
				.SingleOrDefault();
		}

		public IEnumerable<TaskInformation> GetAllRunnableTasks()
		{
			return _allRunnableTasks.Select(task => new TaskInformation
			                                        	{
			                                        		Name = TaskMetaHelper.GetFriendlyNameFor(task),
			                                        		Description = TaskMetaHelper.GetDescriptionFor(task)
			                                        	});
		}
	}
}
namespace Taskie
{
	public static class TaskMetaHelper
	{
		public const string NoTaskDescriptionGivenMessage = "No description provided.";
		private const string _taskSuffix = "Task";

		public static string GetFriendlyNameFor(ITask task)
		{
			return task.GetType().Name.TrimEnd(_taskSuffix); ;
		}

		public static string GetFullNameFrom(string friendlyTaskName)
		{
			return friendlyTaskName + _taskSuffix;
		}

		public static string GetDescriptionFor(ITask task)
		{
			var taskDescriptionAttribute = task.GetType().GetAttribute<TaskDescriptionAttribute>();

			return taskDescriptionAttribute != null
					? taskDescriptionAttribute.Description
					: NoTaskDescriptionGivenMessage;
		}
	}
}
using System;

namespace Taskie
{
	public class TaskDescriptionAttribute : Attribute
	{
		public string Description { get; private set; }

		public TaskDescriptionAttribute(string description)
		{
			this.Description = description;
		}
	}
}
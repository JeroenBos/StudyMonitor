using StudyMonitor.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace StudyMonitor.Service
{
	public class StudyTasks : IStudyTasks
	{
		private readonly List<StudyTask> tasks = new List<StudyTask>();

		public void Add(StudyTask task)
		{
			tasks.Add(task);
		}

		public StudyTask GetTask(int id)
		{
			var context = new StudyTasksContext();
			var result = context.Tasks.FirstOrDefault(task => task.Id == id);
			if (result == null)
				throw new InvalidOperationException();
			return result.ToEntity();
		}
	}
}

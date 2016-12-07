using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace StudyMonitor.Service
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
	public class StudyTasks : IStudyTasks
	{
		private readonly List<StudyTask> tasks = new List<StudyTask>();

		public void Add(StudyTask task)
		{
			tasks.Add(task);
		}

		public StudyTask GetTask(int id)
		{
			var context = new Database.StudyTasks();
			var result = context.Tasks.FirstOrDefault(task => task.Id == id);
			if (result == null)
				throw new InvalidOperationException();
			return result.ToEntity();
		}
	}
}

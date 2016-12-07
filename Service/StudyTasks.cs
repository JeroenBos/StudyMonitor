using StudyMonitor.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace StudyMonitor.Service
{
	public class StudyTasksService : IStudyTasksService
	{
		public int Add(StudyTaskService task)
		{
			if (task == null) throw new ArgumentNullException(nameof(task));

			var context = new StudyTasksContext();
			var result = context.Tasks.Add(task.ToDBObject());
			if (result == null)
				throw new NotImplementedException();

			var saveChangeResult = context.SaveChanges();
			return result.Id;
		}

		public StudyTaskService GetTask(int id)
		{
			var context = new StudyTasksContext();
			var result = context.Tasks.FirstOrDefault(task => task.Id == id);
			if (result == null)
				throw new NotImplementedException();

			return result.ToService();
		}
	}
}

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
		public StudyTasksService()
		{
			
		}
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

		public void AddTimeSpanTo(StudyTaskService task, TaskTimeSpanService timeSpan)
		{
			var context = new StudyTasksContext();
			var timeSpanDB = context.TimeSpans.FirstOrDefault(_ => _.Id == timeSpan.Id);
			if (timeSpanDB != null)
			{
				context.TimeSpans.Remove(timeSpanDB);
			}
			else
			{
				timeSpanDB = timeSpan.ToDBObject();
			}

			context.TimeSpans.Add(timeSpanDB);
			context.SaveChanges();
		}

		public IEnumerable<TaskTimeSpanService> GetTimeSpansFor(StudyTaskService task)
		{
			var context = new StudyTasksContext();
			var result = context.TimeSpans
								.Where(timeSpanDB => timeSpanDB.TaskId == task.Id)
								.Select(timeSpanDB => timeSpanDB.ToService(this));
			return result;
		}
	}
}

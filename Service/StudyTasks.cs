using StudyMonitor.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
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

			using (var context = new StudyTasksContext())
			{
				var result = context.Tasks.Add(task.ToDBObject());
				if (result == null)
					throw new NotImplementedException();

				var saveChangeResult = context.SaveChanges();

				return result.Id;
			}
		}

		public StudyTaskService GetTask(int id)
		{
			using (var context = new StudyTasksContext())
			{
				var result = context.Tasks.FirstOrDefault(task => task.Id == id);
				return result?.ToService();
			}
		}

		public int AddTimeSpanTo(int taskId, TaskTimeSpanService timeSpan)
		{
			if (taskId == 0) throw new ArgumentOutOfRangeException(nameof(taskId));
			if (timeSpan == null) throw new ArgumentNullException(nameof(timeSpan));
			if (timeSpan.Id == 0) throw new ArgumentException();
			if (timeSpan.TaskId == 0) throw new ArgumentException();

			int timeSpanId = timeSpan.Id;
			using (var context = new StudyTasksContext())
			{
				var timeSpanDB = context.TimeSpans.FirstOrDefault(_ => _.Id == timeSpan.Id);
				if (timeSpanDB != null)
				{
					context.TimeSpans.Remove(timeSpanDB);
				}
				else
				{
					timeSpanDB = timeSpan.ToDBObject();
				}

				var createdTimeSpan = context.TimeSpans.Add(timeSpanDB);
				context.SaveChanges();
				timeSpanId = createdTimeSpan.Id;
			}
			return timeSpanId;
		}

		public IEnumerable<TaskTimeSpanService> GetTimeSpansFor(int taskId)
		{
			if (taskId == 0) throw new ArgumentOutOfRangeException(nameof(taskId));

			using (var context = new StudyTasksContext())
			{
				var dbResult = context.TimeSpans
									.Where(timeSpanDB => timeSpanDB.TaskId == taskId)
									.ToList();

				var result = dbResult.Select(x => x.ToService(this))
									 .ToList();

				return result;
			}
		}

		public void ClearAll()
		{
			using (var context = new StudyTasksContext())
			{
				context.Tasks.RemoveRange(context.Tasks);
				context.TimeSpans.RemoveRange(context.TimeSpans);
				context.SaveChanges();
			}
		}

		public IEnumerable<StudyTaskService> GetAllTasks()
		{
			using (var context = new StudyTasksContext())
			{
				return context.Tasks.ToList().Select(x => x.ToService());
			}
		}

		/// <summary> Gets the ID of the open time span (i.e. with <see cref="TaskTimeSpan.End"/> == null) pertaining to the specified task id; 
		/// or 0 in case there is no such open time span. </summary>
		public int GetOpenTimeSpanIdFor(int taskId)
		{
			using (var context = new StudyTasksContext())
			{
				var openTimeSpansDB = context.TimeSpans
											 .Where(timeSpanDB => timeSpanDB.TaskId == taskId)
											 .Where(timeSpanDB => timeSpanDB.End == null)
											 .ToList();

				Contract.Assert(openTimeSpansDB.Count <= 1, "There should at most one open timespan per task");

				if(openTimeSpansDB.Count == 1)
				{
					return openTimeSpansDB[0].Id;
				}
				else
				{
					return 0;
				}
			}
		}
	}
}

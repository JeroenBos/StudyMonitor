﻿using StudyMonitor.Database;
using System;

namespace StudyMonitor.Service
{
	static class DatabaseExtensions
	{
		public static StudyTaskService ToService(this StudyTask task)
		{
			return new StudyTaskService()
			{
				Name = task.Name,
                Id = task.Id,
                UserId = task.UserId,
                Estimate = TimeSpan.FromTicks(task.Estimate)
			};
		}
		public static StudyTask ToDBObject(this StudyTaskService task)
		{
			return new StudyTask()
			{
				Name = task.Name,
                UserId = task.UserId,
                Estimate = task.Estimate.Ticks
			};
		}

		public static TaskTimeSpanService ToService(this TaskTimeSpan timeSpan, IStudyTasksService service)
		{
			if (service == null) throw new ArgumentNullException(nameof(service));

			var task = service.GetTask(timeSpan.TaskId);
			if (task == null) throw new ArgumentException($"A task with Id {timeSpan.TaskId} does not exist in the database", nameof(timeSpan));

			return new TaskTimeSpanService
			{
				Id = timeSpan.Id,
				Start = timeSpan.Start,
				End = timeSpan.End,
				TaskId = timeSpan.TaskId
			};
		}
		public static TaskTimeSpan ToDBObject(this TaskTimeSpanService timeSpan)
		{
			return new TaskTimeSpan
			{
				Id = timeSpan.Id,
				Start = timeSpan.Start,
				End = timeSpan.End,
				TaskId = timeSpan.TaskId
			};
		}
	}
}

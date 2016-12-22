using StudyMonitor.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace StudyMonitor.Service
{
    public class StudyTasksService : IStudyTasksService
    {
        public StudyTasksService()
        {
        }

        /// <summary>
        /// Add a task to the database
        /// </summary>
        /// <param name="task">The task to add</param>
        /// <returns>The id of the task in the database</returns>
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

        /// <summary>
        /// Find a task with the given Id
        /// </summary>
        /// <param name="id">The id of the task</param>
        /// <returns>A task</returns>
        public StudyTaskService GetTask(int id)
        {
            using (var context = new StudyTasksContext())
            {
                var result = context.Tasks.FirstOrDefault(task => task.Id == id);
                return result?.ToService();
            }
        }

        /// <summary>
        /// Add a timespan to the database
        /// </summary>
        /// <param name="timeSpan">The timespan</param>
        /// <returns>The id of the timespan in the database</returns>
        public int AddTimeSpanTo(TaskTimeSpanService timeSpan)
        {
            if (timeSpan == null) throw new ArgumentNullException(nameof(timeSpan));
			if (timeSpan.Start == new DateTime()) throw new ArgumentException("timespan has the default DateTime instead of an assigned one");
            if (timeSpan.Id != 0) throw new ArgumentException("timespan has an ID assigned but is ignored");
            if (timeSpan.TaskId == 0) throw new ArgumentException("timespan has no assigned TaskId");

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

        /// <summary>
        /// Get the timespans for a given taskId
        /// </summary>
        /// <param name="taskId">The task id of the timespans</param>
        /// <returns>The timespans</returns>
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

        /// <summary>
        /// Removes all the entires in the Tasks and Timespans database
        /// </summary>
        public void ClearAll()
        {
            using (var context = new StudyTasksContext())
            {
                context.Tasks.RemoveRange(context.Tasks);
                context.TimeSpans.RemoveRange(context.TimeSpans);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all the tasks from the database of the given user
        /// </summary>
        /// <param name="userId">The user id of the tasks you want to get</param>
        /// <returns>All tasks of the given user</returns>
        public IEnumerable<StudyTaskService> GetAllTasksOfUser(string userId)
        {
            using (var context = new StudyTasksContext())
            {
                return context.Tasks.ToList().Select(x => x.ToService()).Where(x => x.UserId == userId);
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

                if (openTimeSpansDB.Count == 1)
                {
                    return openTimeSpansDB[0].Id;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary> Removes the task with specified <paramref name="taskId"/> from the database, and all associated time spans. 
        /// Does nothing if no task has that ID. </summary>
        public void RemoveTask(int taskId)
        {
            if (taskId == 0) throw new ArgumentOutOfRangeException(nameof(taskId));

            using (var context = new StudyTasksContext())
            {
                context.Tasks.RemoveRange(context.Tasks.Where(task => task.Id == taskId));
                context.TimeSpans.RemoveRange(context.TimeSpans.Where(timeSpan => timeSpan.TaskId == taskId));
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Removes a timespan by its Id
        /// </summary>
        /// <param name="timeSpanId">The timespan id</param>
        public void RemoveTimeSpan(int timeSpanId)
        {
            if (timeSpanId == 0) throw new ArgumentOutOfRangeException(nameof(timeSpanId));

            using (var context = new StudyTasksContext())
            {
                context.TimeSpans.RemoveRange(context.TimeSpans.Where(timeSpan => timeSpan.Id == timeSpanId));
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Find a timespan by the given id
        /// </summary>
        /// <param name="timeSpanId">The id of the timespan</param>
        /// <returns>A timespan or null</returns>
        public TaskTimeSpanService GetTimeSpan(int timeSpanId)
        {
            if (timeSpanId == 0) throw new ArgumentOutOfRangeException(nameof(timeSpanId));

            using (var context = new StudyTasksContext())
            {
                var dbResult = context.TimeSpans
                    .FirstOrDefault(timeSpanDB => timeSpanDB.Id == timeSpanId);

                return dbResult?.ToService(this);
            }
        }

		/// <summary> Updates the time span in the database with the specified properties. </summary>
		public void UpdateTimeSpan(TaskTimeSpanService messageObject)
		{
			using (var context = new StudyTasksContext())
			{
				var dbElement = context.TimeSpans.FirstOrDefault(timeSpanDB => timeSpanDB.Id == messageObject.Id);
				Contract.Assert(dbElement != null, "Cannot update an element that does not exist");
				dbElement.TaskId = messageObject.TaskId;
				dbElement.Start = messageObject.Start;
				dbElement.End = messageObject.End;
				context.SaveChanges();
			}
		}
		/// <summary> Updates the task in the database with the specified properties. </summary>
		public void UpdateTask(StudyTaskService messageObject)
		{
			using (var context = new StudyTasksContext())
			{
				var dbElement = context.Tasks.FirstOrDefault(taskDB => taskDB.Id == messageObject.Id);
				Contract.Assert(dbElement != null, "Cannot update an element that does not exist");
				dbElement.Name = messageObject.Name;
				dbElement.Estimate = messageObject.Estimate;
				Contract.Assert(dbElement.UserId == messageObject.UserId);
				context.SaveChanges();
			}
		}

		public string GetUserIdForTests()
        {
            using (var context = new StudyTasksContext())
            {
                var getFirstId = context.AspNetUsers.FirstOrDefault();
                if (getFirstId != null)
                {
                    return getFirstId.Id;
                }
                else
                {
                    context.AspNetUsers.Add(new AspNetUser()
                    {
                        UserName = "test",
                        EmailConfirmed = false,
                        Id = "TestingId",
                        PhoneNumberConfirmed = false,
                        TwoFactorEnabled = false,
                        AccessFailedCount = 0,
                        LockoutEnabled = false
                    });
                    context.SaveChanges();

                    return context.AspNetUsers.First().Id;
                }
            }
        }
    }
}
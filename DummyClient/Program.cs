using StudyMonitor.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyClient
{
	class Program
	{
		static void Main(string[] args)
		{
			var database = new StudyTasks();
			database.Tasks.Add(new StudyTaskDB { Id = 1, Name = "Hallo" });
			database.SaveChanges();

			var taskSpansDB = new TaskTimeSpans();
			taskSpansDB.TimeSpans.Add(new TaskTimeSpanDB { End = DateTime.Now, Start = DateTime.Now, Id = 1, TaskId = 1 });
			taskSpansDB.SaveChanges();
		}
	}
}

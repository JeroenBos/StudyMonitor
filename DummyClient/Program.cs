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
			var database = new StudyTasksContext();
			database.Tasks.Add(new StudyTask { Id = 1, Name = "Hallo" });
			database.SaveChanges();

			var taskSpans = new TaskTimeSpans();
			taskSpans.TimeSpans.Add(new TaskTimeSpan { End = DateTime.Now, Start = DateTime.Now, Id = 1, TaskId = 1 });
			taskSpans.SaveChanges();
		}
	}
}

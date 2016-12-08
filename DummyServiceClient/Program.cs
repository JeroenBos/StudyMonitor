using StudyMonitor.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyMonitor.DummyServiceClient
{
	class Program
	{
		static void Main(string[] args)
		{
			StudyTasksService tasks = new StudyTasksService();
			var task = new StudyTaskService() { Id = 3, Name = "first" };
			var timespan = new TaskTimeSpanService() { Id = 1, End = DateTime.Now, Start = DateTime.Now, Task = task };
			var id = tasks.Add(task);
			tasks.AddTimeSpanTo(task, timespan);
			Console.WriteLine("Added timespan to task");
			Console.ReadLine();
		}
	}
}

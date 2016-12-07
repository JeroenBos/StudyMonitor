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
			var id = tasks.Add(new StudyTaskService() { Id = 3, Name = "first" });
			Console.ReadLine();
		}
	}
}

using StudyMonitor;
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
			database.Tasks.Add(new StudyTask { Id = 1, Name = "Hallo" });
			database.SaveChanges();
		}
	}
}

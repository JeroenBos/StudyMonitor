using StudyMonitor.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyMonitor.Service
{
	static class DatabaseExtensions
	{
		public static StudyTaskService ToService(this StudyTask task)
		{
			return new StudyTaskService()
			{
				Name = task.Name
			};
		}
		public static StudyTask ToDBObject(this StudyTaskService task)
		{
			return new StudyTask()
			{
				Name = task.Name
			};
		}
	}
}

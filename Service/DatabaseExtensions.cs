using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyMonitor.Service
{
	static class DatabaseExtensions
	{
		public static StudyTask ToEntity(this Database.StudyTask task)
		{
			return new StudyTask()
			{
				Name = task.Name
			};
		}
	}
}

using StudyMonitor.ServiceAccess.ServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyMonitor.ServiceAccess
{
	public class StudyTask
	{
		private readonly IStudyTasksService client;
		internal readonly StudyTaskService service;
		public string Name { get; }
		public ObservableCollection<TaskTimeSpan> TimeSpans { get; }

		public StudyTask(IStudyTasksService client)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));

			this.service = new StudyTaskService();
			this.client = client;
			Name = service.Name;
			TimeSpans = new ObservableCollection<TaskTimeSpan>();
		}
	}
	public class TaskTimeSpan
	{
		internal readonly TaskTimeSpanService service;
		public TaskTimeSpan(TaskTimeSpanService service)
		{
			if (service == null)
			{
				throw new ArgumentNullException(nameof(service));
			}
			this.service = service;
		}
	}
}

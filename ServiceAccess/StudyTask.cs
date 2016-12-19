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
		private readonly IStudyTasksService tasks;
		internal readonly StudyTaskService service;
		public string Name { get; }
		public ObservableCollection<TaskTimeSpan> TimeSpans { get; }

		public StudyTask(StudyTaskService service, IStudyTasksService tasks)
		{
			this.service = service ?? throw new ArgumentNullException(nameof(service));
			this.tasks = tasks ?? throw new ArgumentNullException(nameof(tasks));
			Name = service.Name;
			TimeSpans = new ObservableCollection<TaskTimeSpan>();
		}
	}
	public class TaskTimeSpan
	{
		internal readonly TaskTimeSpanService service;
		public TaskTimeSpan(TaskTimeSpanService service)
		{
			this.service = service ?? throw new ArgumentNullException(nameof(service));
		}
	}
}

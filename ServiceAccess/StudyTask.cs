using StudyMonitor.ServiceAccess.ServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyMonitor.ServiceAccess
{
	class StudyTask
	{
		private readonly StudyTaskService service;
		private readonly ObservableCollection<TaskTimeSpan> timeSpans;
		public string Name { get; }
		public ReadOnlyObservableCollection<TaskTimeSpan> TimeSpans { get; }

		public StudyTask(StudyTaskService service)
		{
			this.service = service ?? throw new ArgumentNullException(nameof(service));
			Name = service.Name;
			timeSpans = new ObservableCollection<TaskTimeSpan>();
			TimeSpans = new ReadOnlyObservableCollection<TaskTimeSpan>(this.timeSpans);
		}
	}
	class TaskTimeSpan
	{
		private readonly TaskTimeSpanService service;
		public TaskTimeSpan(TaskTimeSpanService service)
		{
			this.service = service ?? throw new ArgumentNullException(nameof(service));
		}
	}
}

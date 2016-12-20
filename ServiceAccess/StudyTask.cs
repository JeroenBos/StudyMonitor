using StudyMonitor.ServiceAccess.ServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
			TimeSpans.CollectionChanged += TimeSpansChanged;
		}

		private void TimeSpansChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					foreach (var newTimeSpan in e.NewItems.Cast<TaskTimeSpan>())
					{
						this.client.AddTimeSpanTo(service.Id, newTimeSpan.service);
					}
					break;
				case NotifyCollectionChangedAction.Remove:
				case NotifyCollectionChangedAction.Replace:
				case NotifyCollectionChangedAction.Move:
				case NotifyCollectionChangedAction.Reset:
					throw new NotImplementedException();
				default:
					throw new Exception();
			}
		}
	}
	public class TaskTimeSpan
	{
		internal readonly TaskTimeSpanService service = new TaskTimeSpanService();

		public DateTime Start { get; set; }
		public DateTime? End { get; set; }
		public StudyTask Task { get; set; }
	}
}

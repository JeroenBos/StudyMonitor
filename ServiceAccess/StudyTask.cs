using JBSnorro;
using StudyMonitor.ServiceAccess.ServiceReference;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyMonitor.ServiceAccess
{
	public class StudyTask : DefaultINotifyPropertyChanged
	{
		private readonly IStudyTasksService client;
		internal readonly StudyTaskService service;
		public string Name
		{
			get { return name; }
			set
			{
				if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException(nameof(value));
				base.Set(ref name, value);
			}
		}
		public ObservableCollection<TaskTimeSpan> TimeSpans { get; }

		/// <summary> Creates a new task and adds it to the database. </summary>
		public StudyTask(IStudyTasksService client, string name)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));
			if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException(nameof(name));

			this.service = new StudyTaskService() { Name = name };
			this.client = client;
			this.Name = name;
			this.TimeSpans = new ObservableCollection<TaskTimeSpan>();

			// add to database
			this.service.Id = this.client.Add(this.service);

			this.TimeSpans.CollectionChanged += timeSpansChanged;
			this.PropertyChanged += propertyChanged;
		}

		private void propertyChanged(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case nameof(Name):
					service.Name = this.Name;
					break;
			}
		}

		private void timeSpansChanged(object sender, NotifyCollectionChangedEventArgs e)
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

		private string name;
	}
	public class TaskTimeSpan : DefaultINotifyPropertyChanged
	{
		internal readonly TaskTimeSpanService service = new TaskTimeSpanService();

		public DateTime Start
		{
			get { return start; }
			set { base.Set(ref start, value); }
		}
		public DateTime? End
		{
			get { return end; }
			set { base.Set(ref end, value); }
		}
		public StudyTask Task
		{
			get { return task; }
			set
			{
				if (value == null) throw new ArgumentNullException();
				base.Set(ref task, value);
			}
		}

		private DateTime start;
		private DateTime? end;
		private StudyTask task;

		/// <remarks> This ctor should not add this instance to the database because the <see cref="StudyTask.TimeSpans.CollectionChanged"/> is responsible for that. </remarks>
		public TaskTimeSpan(StudyTask task, DateTime start)
		{
			if (task == null) throw new ArgumentNullException(nameof(task));

			this.Task = task;
			this.Start = start;

			this.PropertyChanged += propertyChanged;
		}

		private void propertyChanged(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case nameof(Start):
					service.Start = this.Start;
					break;
				case nameof(End):
					service.End = this.End;
					break;
				case nameof(Task):
					service.TaskId = this.Task.service.Id;
					break;
			}
		}
	}
}

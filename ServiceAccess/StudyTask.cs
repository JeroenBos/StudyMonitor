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

		/// <summary> Creates a <see cref="StudyTask"/> instance representing an already existing task in the database. </summary>
		/// <param name="taskId"> The id of the task in the database to fetch. </param>
		public StudyTask(IStudyTasksService client, int taskId)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));

			this.service = client.GetTask(taskId);

			if (this.service == null) throw new ArgumentException("No task with the specified ID exists", nameof(taskId));
			Contract.Assert(this.service.Id == taskId);

			this.client = client;
			this.Name = this.service.Name;

			//retrieve time spans from database:
			var timeSpans = client.GetTimeSpansFor(taskId).Select(timeSpanDB => new TaskTimeSpan(timeSpanDB, this));
			this.TimeSpans = new ObservableCollection<TaskTimeSpan>(timeSpans);

			this.TimeSpans.CollectionChanged += timeSpansChanged;
			this.PropertyChanged += propertyChanged;
		}

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

		/// <summary> Removes the task represented by this instance from the database. </summary>
		public void RemoveFromDatabase()
		{
			const string removedErrorMessage = "The current instance has been removed from the database";

			client.RemoveTask(this.service.Id);
			this.TimeSpans.CollectionChanged -= timeSpansChanged;
			this.TimeSpans.CollectionChanged += (sender, e) => { throw new Exception(removedErrorMessage); };
			this.PropertyChanged -= propertyChanged;
			this.PropertyChanged += (sender, e) => { throw new Exception(removedErrorMessage); };
		}

		/// <summary> Gets the open time span associated to this task, if any; null otherwise. </summary>
		public TaskTimeSpan OpenTimeSpan
		{
			get { return TimeSpans.FirstOrDefault(timeSpan => timeSpan.End == null); }
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
						Contract.Assert(newTimeSpan.service.Id == 0, "The added time span is already added to another task");
						var assignedTimeSpanId = this.client.AddTimeSpanTo(service.Id, newTimeSpan.service);
						newTimeSpan.service.Id = assignedTimeSpanId;
					}
					Contract.Assert(hasAtMostOneOpenTimeSpan(), "A task may not have multiple open time spans associated to it");
					break;
				case NotifyCollectionChangedAction.Remove:
					foreach (var newTimeSpan in e.OldItems.Cast<TaskTimeSpan>())
					{
						this.client.RemoveTimeSpan(newTimeSpan.service.Id);
					}
					break;
				case NotifyCollectionChangedAction.Replace:
				case NotifyCollectionChangedAction.Move:
				case NotifyCollectionChangedAction.Reset:
					throw new NotImplementedException();
				default:
					throw new Exception();
			}
		}

		internal bool hasAtMostOneOpenTimeSpan()
		{
			return TimeSpans.Count(timeSpan => timeSpan.End == null) <= 1;
		}
		private string name;
	}
}

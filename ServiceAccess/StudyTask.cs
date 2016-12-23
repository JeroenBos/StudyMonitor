using JBSnorro;
using StudyMonitor.ServiceAccess.ServiceReference;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;

namespace StudyMonitor.ServiceAccess
{
	/// <summary> Represents a study task and handles updating the database. </summary>
	public class StudyTask : DefaultINotifyPropertyChanged
	{
		private readonly IStudyTasksService service;
		internal readonly StudyTaskService MessageObject;
		/// <summary> Gets or sets the name of this task. Setting will update the database. </summary>
		public string Name
		{
			get { return name; }
			set
			{
				if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException(nameof(value));
				base.Set(ref name, value);
			}
		}

		public DateTime Estimate
		{
			get { return estimate; }
			set { Set(ref estimate, value); }
		}
		/// <summary> Gets the collection of time spans associated with this task. Modifications will be propagated to the database. </summary>
		public ObservableCollection<TaskTimeSpan> TimeSpans { get; }
		/// <summary> Gets the ID this task has in the database. </summary>
		public int Id
		{
			get { return this.MessageObject.Id; }
		}
		/// <summary> Gets whether this study task is currently running. </summary>
		public bool IsOpen
		{
			get { return this.OpenTimeSpan != null; }
		}

		/// <summary> Gets the open time span associated to this task, if any; null otherwise. </summary>
		public TaskTimeSpan OpenTimeSpan
		{
			get { return TimeSpans.FirstOrDefault(timeSpan => timeSpan.End == null); }
		}

		/// <summary> Creates a <see cref="StudyTask"/> instance representing an already existing task in the database. </summary>
		/// <param name="taskId"> The id of the task in the database to fetch. </param>
		public StudyTask(IStudyTasksService service, int taskId)
			: this(service, service.GetTask(taskId))
		{
			Contract.Assert(this.MessageObject.Id == taskId);
		}
		/// <summary> Creates a <see cref="StudyTask"/> instance representing an already existing task in the database. </summary>
		public StudyTask(IStudyTasksService service, StudyTaskService task)
		{
			if (service == null) throw new ArgumentNullException(nameof(service));
			if (task == null) throw new ArgumentNullException(nameof(task));
			if (task.Id == 0) throw new ArgumentException();
			if (task.UserId == null) throw new ArgumentNullException(nameof(task.UserId));

			this.MessageObject = task;
			this.service = service;
			this.Name = this.MessageObject.Name;

			//retrieve time spans from database:
			var timeSpans = service.GetTimeSpansFor(task.Id).Select(timeSpanDB => new TaskTimeSpan(service, timeSpanDB, this));
			this.TimeSpans = new ObservableCollection<TaskTimeSpan>(timeSpans);

			this.TimeSpans.CollectionChanged += OnTimeSpansChanged;
			this.PropertyChanged += OnPropertyChanged;
		}
		/// <summary> Creates a new task. </summary>
		public StudyTask(IStudyTasksService client, string name, string userId, DateTime estimate)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));
			if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException(nameof(name));
			if (userId == null) throw new ArgumentNullException(nameof(userId));

			this.MessageObject = new StudyTaskService() { Name = name, UserId = userId, Estimate = estimate };
			this.service = client;
			this.Name = name;
			this.Estimate = estimate;
			this.TimeSpans = new ObservableCollection<TaskTimeSpan>();

			this.TimeSpans.CollectionChanged += OnTimeSpansChanged;
			this.PropertyChanged += OnPropertyChanged;
		}

		/// <summary> Gets the total length of all time spans in this task. </summary>
		public TimeSpan GetLength()
		{
			return TimeSpans.Select(taskTimeSpan => taskTimeSpan.Length)
							.Concat(new[] { new TimeSpan() })
							.Aggregate((a, b) => a + b);
		}
		/// <summary> Removes the task represented by this instance from the database. </summary>
		internal void OnRemoveFromDatabase()
		{
			const string removedErrorMessage = "The current instance has been removed from the database";

			this.TimeSpans.CollectionChanged -= OnTimeSpansChanged;
			this.TimeSpans.CollectionChanged += (sender, e) => { throw new Exception(removedErrorMessage); };
			this.PropertyChanged -= OnPropertyChanged;
			this.PropertyChanged += (sender, e) => { throw new Exception(removedErrorMessage); };
		}

		private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case nameof(Name):
					MessageObject.Name = this.Name;
					break;
				case nameof(Estimate):
					MessageObject.Estimate = this.Estimate;
					break;
			}
			this.service.UpdateTask(this.MessageObject);
		}
		private void OnTimeSpansChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					foreach (var newTimeSpan in e.NewItems.Cast<TaskTimeSpan>())
					{
						Contract.Assert(newTimeSpan.timeSpanMessageObject.Id == 0, "The added time span is already added to another task");
						var assignedTimeSpanId = this.service.AddTimeSpanTo(newTimeSpan.timeSpanMessageObject);
						newTimeSpan.timeSpanMessageObject.Id = assignedTimeSpanId;
					}
					Contract.Assert(HasAtMostOneOpenTimeSpan(), "A task may not have multiple open time spans associated to it");
					break;
				case NotifyCollectionChangedAction.Remove:
					foreach (var newTimeSpan in e.OldItems.Cast<TaskTimeSpan>())
					{
						this.service.RemoveTimeSpan(newTimeSpan.timeSpanMessageObject.Id);
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

		internal bool HasAtMostOneOpenTimeSpan()
		{
			return TimeSpans.Count(timeSpan => timeSpan.End == null) <= 1;
		}
		private string name;
		private DateTime estimate;
	}
}

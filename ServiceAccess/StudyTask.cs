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
		/// <summary> Gets a list of all tasks in the database. </summary>
		public static IEnumerable<StudyTask> GetAllTasksFromDatabase(IStudyTasksService client)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));

			return client.GetAllTasks()
						 .Select(taskService => new StudyTask(client, taskService));
		}

		private readonly IStudyTasksService client;
		internal readonly StudyTaskService Service;
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
		/// <summary> Gets the collection of time spans associated with this task. Modifications will be propagated to the database. </summary>
		public ObservableCollection<TaskTimeSpan> TimeSpans { get; }
		/// <summary> Gets the ID this task has in the database. </summary>
		public int Id
		{
			get { return this.Service.Id; }
		}

		/// <summary> Creates a <see cref="StudyTask"/> instance representing an already existing task in the database. </summary>
		/// <param name="taskId"> The id of the task in the database to fetch. </param>
		public StudyTask(IStudyTasksService client, int taskId)
			: this(client, client.GetTask(taskId))
		{
			Contract.Assert(this.Service.Id == taskId);
		}
		/// <summary> Creates a <see cref="StudyTask"/> instance representing an already existing task in the database. </summary>
		public StudyTask(IStudyTasksService client, StudyTaskService task)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));
			if (task == null) throw new ArgumentNullException(nameof(task));
			if (task.Id == 0) throw new ArgumentException();

			this.Service = task;
			this.client = client;
			this.Name = this.Service.Name;

			//retrieve time spans from database:
			var timeSpans = client.GetTimeSpansFor(task.Id).Select(timeSpanDB => new TaskTimeSpan(timeSpanDB, this));
			this.TimeSpans = new ObservableCollection<TaskTimeSpan>(timeSpans);

			this.TimeSpans.CollectionChanged += OnTimeSpansChanged;
			this.PropertyChanged += OnPropertyChanged;
		}
		/// <summary> Creates a new task and adds it to the database. </summary>
		public StudyTask(IStudyTasksService client, string name)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));
			if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException(nameof(name));

			this.Service = new StudyTaskService() { Name = name };
			this.client = client;
			this.Name = name;
			this.TimeSpans = new ObservableCollection<TaskTimeSpan>();

			// add to database
			this.Service.Id = this.client.Add(this.Service);

			this.TimeSpans.CollectionChanged += OnTimeSpansChanged;
			this.PropertyChanged += OnPropertyChanged;
		}

		/// <summary> Removes the task represented by this instance from the database. </summary>
		public void RemoveFromDatabase()
		{
			const string removedErrorMessage = "The current instance has been removed from the database";

			client.RemoveTask(this.Service.Id);
			this.TimeSpans.CollectionChanged -= OnTimeSpansChanged;
			this.TimeSpans.CollectionChanged += (sender, e) => { throw new Exception(removedErrorMessage); };
			this.PropertyChanged -= OnPropertyChanged;
			this.PropertyChanged += (sender, e) => { throw new Exception(removedErrorMessage); };
		}

		/// <summary> Gets the open time span associated to this task, if any; null otherwise. </summary>
		public TaskTimeSpan OpenTimeSpan
		{
			get { return TimeSpans.FirstOrDefault(timeSpan => timeSpan.End == null); }
		}

		private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case nameof(Name):
					Service.Name = this.Name;
					break;
			}
		}
		private void OnTimeSpansChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					foreach (var newTimeSpan in e.NewItems.Cast<TaskTimeSpan>())
					{
						Contract.Assert(newTimeSpan.service.Id == 0, "The added time span is already added to another task");
						var assignedTimeSpanId = this.client.AddTimeSpanTo(Service.Id, newTimeSpan.service);
						newTimeSpan.service.Id = assignedTimeSpanId;
					}
					Contract.Assert(HasAtMostOneOpenTimeSpan(), "A task may not have multiple open time spans associated to it");
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

		internal bool HasAtMostOneOpenTimeSpan()
		{
			return TimeSpans.Count(timeSpan => timeSpan.End == null) <= 1;
		}
		private string name;
	}
}

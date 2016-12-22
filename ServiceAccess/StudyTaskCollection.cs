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
	public class StudyTaskCollection : ObservableCollection<StudyTask>
	{
		private readonly IStudyTasksService client;

		private StudyTaskCollection(IStudyTasksService client, IEnumerable<StudyTask> initialTasks) : base(initialTasks)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));

			this.client = client;

			this.CollectionChanged += OnTasksChanged;
		}

		private void OnTasksChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					foreach (var newTask in e.NewItems.Cast<StudyTask>())
					{
						newTask.MessageObject.Id = this.client.Add(newTask.MessageObject);
					}
					break;
				case NotifyCollectionChangedAction.Remove:
					foreach (var removedTask in e.OldItems.Cast<StudyTask>())
					{
						this.client.RemoveTask(removedTask.MessageObject.Id);
						removedTask.OnRemoveFromDatabase();
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

		/// <summary> Creates an empty study tasks collection that is linked to the database. </summary>
		public static StudyTaskCollection Create(IStudyTasksService client)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));

			return new StudyTaskCollection(client, Enumerable.Empty<StudyTask>());
		}
		/// <summary> Gets a study tasks collection from all tasks in the database. </summary>
		public static StudyTaskCollection FromDatabase(IStudyTasksService client)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));

			var tasksFromDatabase = client.GetAllTasks()
										  .Select(taskService => new StudyTask(client, taskService))
										  .ToList();
			return new StudyTaskCollection(client, tasksFromDatabase);
		}
	}
}

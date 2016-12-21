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
	public class StudyTasks
	{
		private readonly IStudyTasksService client;
		public ObservableCollection<StudyTask> Tasks { get; }

		private StudyTasks(IStudyTasksService client, IEnumerable<StudyTask> initialTasks)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));
			if (initialTasks == null) throw new ArgumentNullException(nameof(initialTasks));

			this.client = client;
			this.Tasks = new ObservableCollection<StudyTask>(initialTasks);

			this.Tasks.CollectionChanged += OnTasksChanged;
		}

		private void OnTasksChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
		}

		/// <summary> Creates an empty study tasks collection that is linked to the database. </summary>
		public static StudyTasks Create(IStudyTasksService client)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));

			return new StudyTasks(client, Enumerable.Empty<StudyTask>());
		}
		/// <summary> Gets a study tasks collection from all tasks in the database. </summary>
		public static StudyTasks FromDatabase(IStudyTasksService client)
		{
			if (client == null) throw new ArgumentNullException(nameof(client));

			var tasksFromDatabase = client.GetAllTasks()
										  .Select(taskService => new StudyTask(client, taskService));
			return new StudyTasks(client, tasksFromDatabase);
		}
	}
}

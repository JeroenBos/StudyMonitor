using JBSnorro;
using StudyMonitor.ServiceAccess.ServiceReference;
using System;
using System.ComponentModel;

namespace StudyMonitor.ServiceAccess
{
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

			this.PropertyChanged += propertyChanged;

			this.Task = task;
			this.Start = start;
		}

		private void propertyChanged(object sender, PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case nameof(Start):
					service.Start = this.Start;
					break;
				case nameof(End):
					Contract.Assert(this.End != null || this.Task.hasAtMostOneOpenTimeSpan());
					service.End = this.End;
					break;
				case nameof(Task):
					service.TaskId = this.Task.service.Id;
					break;
			}
		}
	}
}

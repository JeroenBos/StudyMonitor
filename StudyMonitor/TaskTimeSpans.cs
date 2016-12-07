using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyMonitorDatabase
{
	public partial class TaskTimeSpans : DbContext
	{
		public TaskTimeSpans()
			: base("name=Tasks")
		{

		}

		public virtual DbSet<TaskTimeSpan> TimeSpans { get; set; }
	}
}

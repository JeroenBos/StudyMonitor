using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyMonitor
{
	public partial class StudyTasks : DbContext
	{
		public StudyTasks()
			: base("name=Tasks")
		{
				
		}

		public virtual DbSet<StudyTask> Tasks { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyMonitor.Database
{
	public partial class StudyTasksContext : DbContext
	{
		public StudyTasksContext()
			: base("name=Tasks")
		{
				
		}

		public virtual DbSet<StudyTaskDB> Tasks { get; set; }
	}
}

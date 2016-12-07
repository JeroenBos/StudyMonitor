using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyMonitor.Database
{
	public class TaskTimeSpan
	{
		public int Id { get; set; }

		[Required]
		public int TaskId { get; set; }
		[Required]
		public DateTime Start { get; set; }
		[Required]
		public DateTime End { get; set; }
	}
}

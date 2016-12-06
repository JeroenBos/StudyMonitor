using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyMonitor
{
	public class StudyTask
	{
		public int Id { get; set; }

		[Required]
		[StringLength(150)]
		public string Name { get; set; }
	}
}

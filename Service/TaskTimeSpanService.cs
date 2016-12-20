using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StudyMonitor.Service
{
	[DataContract]
	public class TaskTimeSpanService
	{
		[DataMember]
		public int Id { get; set; }
		[DataMember]
		public DateTime Start { get; set; }
		[DataMember]
		public DateTime? End { get; set; }
		[DataMember]
		public int TaskId { get; set; }
	}
}

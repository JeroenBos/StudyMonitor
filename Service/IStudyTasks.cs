using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace StudyMonitor.Service
{
	[ServiceContract]
	public interface IStudyTasks
	{
		[OperationContract]
		int Add(StudyTask task);

		[OperationContract]
		StudyTask GetTask(int id);
	}

	[DataContract]
	public class StudyTask
	{
		[DataMember]
		public int Id { get; set; }

		[DataMember]
		public string Name { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace StudyMonitor.Service
{
	[ServiceContract]
	public interface IStudyTasksService
	{
		[OperationContract]
		int Add(StudyTaskService task);

		[OperationContract]
		StudyTaskService GetTask(int id);
	}

	[DataContract]
	public class StudyTaskService
	{
		[DataMember]
		public int Id { get; set; }

		[DataMember]
		public string Name { get; set; }
	}
}

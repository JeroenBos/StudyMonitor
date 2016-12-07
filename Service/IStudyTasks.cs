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
		void Add(StudyTask task);
	}

	[DataContract]
	public class StudyTask
	{
		[DataMember]
		public string Name { get; set; }
	}
}

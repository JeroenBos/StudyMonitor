﻿using System;
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

		[OperationContract]
		int AddTimeSpanTo(int taskId, TaskTimeSpanService timeSpan);

		[OperationContract]
		IEnumerable<TaskTimeSpanService> GetTimeSpansFor(int taskId);

		[OperationContract]
		void ClearAll();
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

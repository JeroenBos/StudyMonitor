using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

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
		int AddTimeSpanTo(TaskTimeSpanService timeSpan);

		[OperationContract]
		IEnumerable<TaskTimeSpanService> GetTimeSpansFor(int taskId);

		[OperationContract]
		void ClearAll();

		[OperationContract]
        IEnumerable<StudyTaskService> GetAllTasksOfUser(string userId);

		[OperationContract]
		int GetOpenTimeSpanIdFor(int taskId);

		[OperationContract]
		void RemoveTask(int taskId);

		[OperationContract]
		void RemoveTimeSpan(int timeSpanId);

		[OperationContract]
		TaskTimeSpanService GetTimeSpan(int timeSpanId);

		[OperationContract]
		void UpdateTimeSpan(TaskTimeSpanService messageObject);

		[OperationContract]
		void UpdateTask(StudyTaskService messageObject);

	    [OperationContract]
	    string GetUserIdForTests();
	}

	[DataContract]
	public class StudyTaskService
	{
		[DataMember]
		public int Id { get; set; }

		[DataMember]
		public string Name { get; set; }

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public TimeSpan Estimate { get; set; }
	}
}

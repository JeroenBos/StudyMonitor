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
		IEnumerable<StudyTaskService> GetAllTasks();

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
	}
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyMonitor.ServiceAccess.ServiceReference;


namespace StudyMonitor.ServiceAccess.Tests
{
	[TestClass]
	public class WCFClientUnitTests : SharedUnitTestsConfiguration
	{
		public static void Main(string[] args)
		{
			new WCFClientUnitTests().InitializeTest();

			//new WCFClientUnitTests().OpenTimeSpanAdditionToTaskTest();
			new ServiceAccessUnitTests().SetTaskTimeSpanEndTest();
		}

	    private StudyTaskService CreateStudyTaskServiceWithName(string name)
	    {
	        return new StudyTaskService() { Name = name, UserId = this.UserId, Estimate = DateTime.Now };
	    }

	    private TaskTimeSpanService CreateDefaultTaskTimeSpanService(int taskId)
	    {
	        return new TaskTimeSpanService() { Start = DateTime.Now, End = DateTime.Now, TaskId = taskId };
	    }

		[TestInitialize]
		public void InitializeTest()
		{
			client.ClearAll();
		}

		[TestCleanup]
		public void CalledEverytimeAfterATest()
		{
			client.ClearAll();
		}

		[TestMethod]
		public void TaskIdAssignmentTest()
		{
			var taskId = client.Add(CreateStudyTaskServiceWithName("Erik"));

			var retrievedTask = client.GetTask(taskId);

			Assert.AreNotEqual(retrievedTask.Id, 0);
		}

		[TestMethod]
		public void RetrieveAllTasksTest()
		{
			var taskId1 = client.Add(CreateStudyTaskServiceWithName("Erik"));
			var taskId2 = client.Add(CreateStudyTaskServiceWithName("Jeroen"));
			var taskId3 = client.Add(CreateStudyTaskServiceWithName("Nobody"));
            
			var allTasks = client.GetAllTasksOfUser(this.UserId);

			Assert.AreEqual(allTasks.Length, 3);
		}

		[TestMethod]
		public void TaskCreationAndRetrievalTest()
		{
			var taskId = client.Add(CreateStudyTaskServiceWithName("Erik"));

			var retrievedTask = client.GetTask(taskId);

			Assert.AreEqual(taskId, retrievedTask.Id);
		}

		[TestMethod]
		public void TaskWithNameCreationTest()
		{
			const string name = "myname";
			var taskId = client.Add(CreateStudyTaskServiceWithName(name));

			var retrievedTask = client.GetTask(taskId);

			Assert.AreEqual(retrievedTask.Name, name);
		}

		[TestMethod]
		public void TimeSpanIdAssignmentTest()
		{
			const string name = "myname";
			var taskId = client.Add(CreateStudyTaskServiceWithName(name));
			var timeSpanId = client.AddTimeSpanTo(CreateDefaultTaskTimeSpanService(taskId));

			Assert.AreNotEqual(timeSpanId, 0);
		}

		[TestMethod]
		public void TimeSpanAdditionToTaskTest()
		{
			const string name = "myname";
			var task = CreateStudyTaskServiceWithName(name);
			var taskId = client.Add(task);
			var timeSpanId = client.AddTimeSpanTo(CreateDefaultTaskTimeSpanService(taskId));

			var taskTimeSpans = client.GetTimeSpansFor(taskId);
			Assert.AreEqual(taskTimeSpans.Length, 1);
		}

		[TestMethod]
		public void TimeSpanRemovalTest()
		{
			const string name = "myname";
			var task = CreateStudyTaskServiceWithName(name);
			var taskId = client.Add(task);
			var timeSpanId = client.AddTimeSpanTo(CreateDefaultTaskTimeSpanService(taskId));

			client.RemoveTimeSpan(timeSpanId);

			object expected = null;
			var result = client.GetTimeSpan(timeSpanId);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void OpenTimeSpanAdditionToTaskTest()
		{
			const string name = "myname";
			var task = CreateStudyTaskServiceWithName(name);
			var taskId = client.Add(task);
			var closedTimeSpanId = client.AddTimeSpanTo(CreateDefaultTaskTimeSpanService(taskId));
			var expectedOpenTimeSpanId = client.AddTimeSpanTo(new TaskTimeSpanService() { Start = DateTime.Now, End = null, TaskId = taskId });

			var obtainedOpenTimeSpanId = client.GetOpenTimeSpanIdFor(taskId);
			Assert.AreEqual(expectedOpenTimeSpanId, obtainedOpenTimeSpanId);
		}
		[TestMethod]
		public void RemoveTaskTest()
		{
			var taskId = client.Add(CreateStudyTaskServiceWithName("Erik"));
			client.RemoveTask(taskId);
			var result = client.GetAllTasksOfUser(this.UserId).Length;
			var expected = 0;
			Assert.AreEqual(expected, result);
		}
		[TestMethod]
		public void GetNonExistentTaskTest()
		{
			var result = client.GetTask(int.MaxValue / 3);

			Assert.IsNull(result);
		}
		[TestMethod]
		public void GetNonExistentTimeSpanTest()
		{
			object expected = null;
			var result = client.GetTimeSpan(int.MaxValue / 3);

			Assert.AreEqual(expected, result);
		}
		[TestMethod]
		public void GetTimeSpanTest()
		{
			const string name = "myname";
			var task = CreateStudyTaskServiceWithName(name);
			var taskId = client.Add(task);
			int expectedTimeSpanId = client.AddTimeSpanTo(CreateDefaultTaskTimeSpanService(taskId));

			var resultTimeSpan = client.GetTimeSpan(expectedTimeSpanId);
			Assert.AreEqual(expectedTimeSpanId, resultTimeSpan.Id);
		}

	}
}

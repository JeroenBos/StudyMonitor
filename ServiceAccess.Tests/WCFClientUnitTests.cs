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
			var testObject = new WCFClientUnitTests();
			testObject.InitializeTest();
			testObject.OpenTimeSpanAdditionToTaskTest();

			testObject.TaskIdAssignmentTest();
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
			var taskId = client.Add(new StudyTaskService() { Name = "Erik" });

			var retrievedTask = client.GetTask(taskId);

			Assert.AreNotEqual(retrievedTask.Id, 0);
		}

		[TestMethod]
		public void RetrieveAllTasksTest()
		{
			var taskId1 = client.Add(new StudyTaskService() { Name = "Erik" });
			var taskId2 = client.Add(new StudyTaskService() { Name = "Jeroen" });
			var taskId3 = client.Add(new StudyTaskService() { Name = "Nobody" });

			var allTasks = client.GetAllTasks();

			Assert.AreEqual(allTasks.Length, 3);
		}

		[TestMethod]
		public void TaskCreationAndRetrievalTest()
		{
			var taskId = client.Add(new StudyTaskService() { Name = "Erik" });

			var retrievedTask = client.GetTask(taskId);

			Assert.AreEqual(taskId, retrievedTask.Id);
		}

		[TestMethod]
		public void TaskWithNameCreationTest()
		{
			const string name = "myname";
			var taskId = client.Add(new StudyTaskService() { Name = name });

			var retrievedTask = client.GetTask(taskId);

			Assert.AreEqual(retrievedTask.Name, name);
		}

		[TestMethod]
		public void TimeSpanIdAssignmentTest()
		{
			const string name = "myname";
			var taskId = client.Add(new StudyTaskService() { Name = name });
			var timeSpanId = client.AddTimeSpanTo(taskId, new TaskTimeSpanService() { Start = DateTime.Now, End = DateTime.Now, TaskId = taskId });

			Assert.AreNotEqual(timeSpanId, 0);
		}

		[TestMethod]
		public void TimeSpanAdditionToTaskTest()
		{
			const string name = "myname";
			var task = new StudyTaskService() { Name = name };
			var taskId = client.Add(task);
			var timeSpanId = client.AddTimeSpanTo(taskId, new TaskTimeSpanService() { Start = DateTime.Now, End = DateTime.Now, TaskId = taskId });

			var taskTimeSpans = client.GetTimeSpansFor(taskId);
			Assert.AreEqual(taskTimeSpans.Length, 1);
		}

		[TestMethod]
		public void OpenTimeSpanAdditionToTaskTest()
		{
			const string name = "myname";
			var task = new StudyTaskService() { Name = name };
			var taskId = client.Add(task);
			var closedTimeSpanId = client.AddTimeSpanTo(taskId, new TaskTimeSpanService() { Start = DateTime.Now, End = DateTime.Now, TaskId = taskId });
			var expectedOpenTimeSpanId = client.AddTimeSpanTo(taskId, new TaskTimeSpanService() { Start = DateTime.Now, End = null, TaskId = taskId });

			var obtainedOpenTimeSpanId = client.GetOpenTimeSpanIdFor(taskId);
			Assert.AreEqual(expectedOpenTimeSpanId, obtainedOpenTimeSpanId);
		}
	}
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyMonitor.ServiceAccess.ServiceReference;


namespace StudyMonitor.ServiceAccess.Tests
{
	[TestClass]
	public class UnitTests
	{
		public static void Main(string[] args)
		{
			var testObject = new UnitTests();
			testObject.InitializeTest();
			testObject.RetrieveAllTasksTest();

			testObject.TaskIdAssignmentTest();
		}

		private readonly StudyTaskService service = new StudyTaskService();
		private readonly StudyTasksServiceClient client = new StudyTasksServiceClient("BasicHttpBinding_IStudyTasksService");

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
	}


}

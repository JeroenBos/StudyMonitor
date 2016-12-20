using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyMonitor.ServiceAccess.Tests
{
	[TestClass]
	public class ServiceAccessUnitTests : SharedUnitTestsConfiguration
	{
		[TestMethod]
		public void AddTaskTest()
		{
			const string expected = "taskName";
			var task = new StudyTask(base.client, expected);

			var result = client.GetTask(task.service.Id).Name;
			Assert.AreEqual(expected, result);
		}
		[TestMethod]
		public void AddTimeSpanTest()
		{
			const string expected = "taskName";
			var task = new StudyTask(base.client, name: expected);
			task.TimeSpans.Add(new TaskTimeSpan(task, DateTime.Now));

			var result = client.GetTask(task.service.Id).Name;
			Assert.AreEqual(expected, result);
		}
	}
}

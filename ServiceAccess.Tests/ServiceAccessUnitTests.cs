﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

			var result = client.GetTask(task.Service.Id).Name;
			Assert.AreEqual(expected, result);
		}
		[TestMethod]
		public void AddTimeSpanTest()
		{
			const string expected = "taskName";
			var task = new StudyTask(base.client, name: expected);
			task.TimeSpans.Add(new TaskTimeSpan(task, DateTime.Now));

			var result = client.GetTask(task.Service.Id).Name;
			Assert.AreEqual(expected, result);
		}
		[TestMethod]
		public void RemoveTaskTest()
		{
			object expected = null;
			var task = new StudyTask(base.client, "name");

			task.RemoveFromDatabase();

			var result = client.GetTask(task.Service.Id);
			Assert.AreEqual(expected, result);
		}
		[TestMethod]
		public void RemoveTimeSpanTest()
		{
			var task = new StudyTask(base.client, "taskName");
			task.TimeSpans.Add(new TaskTimeSpan(task, DateTime.Now));
			task.TimeSpans.RemoveAt(0);

			var result = client.GetTimeSpansFor(task.Service.Id);
			Assert.AreEqual(0, result.Length);
		}
		[TestMethod]
		public void TaskRetrievalTest()
		{
			const string expected = "taskName";
			var task = new StudyTask(base.client, expected);

			task = new StudyTask(base.client, task.Service.Id);
			Assert.AreEqual(expected, task.Name);
		}
		[TestMethod]
		public void TaskRetrievalWithTimeSpansTest()
		{
			var task = new StudyTask(base.client, "taskName");
			task.TimeSpans.Add(new TaskTimeSpan(task, DateTime.Now));

			int expectedTimeSpanCount = 1;
			task = new StudyTask(base.client, task.Service.Id);
			Assert.AreEqual(expectedTimeSpanCount, task.TimeSpans.Count);
		}
	}
}

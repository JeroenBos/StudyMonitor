﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
			var task = new StudyTask(base.client, expected, this.UserId);
			StudyTaskCollection.Create(base.client).Add(task);

			var result = client.GetTask(task.Service.Id).Name;
			Assert.AreEqual(expected, result);
		}
		[TestMethod]
		public void AddTimeSpanTest()
		{
			const string expected = "taskName";
			var task = new StudyTask(base.client, expected, this.UserId);
			StudyTaskCollection.Create(base.client).Add(task);
			task.TimeSpans.Add(new TaskTimeSpan(task, DateTime.Now));

			var result = client.GetTask(task.Service.Id).Name;
			Assert.AreEqual(expected, result);
		}
		[TestMethod]
		public void RemoveTaskTest()
		{
			var tasks = StudyTaskCollection.Create(base.client);
			var task = new StudyTask(base.client, "name", this.UserId);
			tasks.Add(task);
			tasks.Remove(task);

			var result = client.GetTask(task.Service.Id);
			Assert.IsNull(result);
		}
		[TestMethod]
		public void RemoveTimeSpanTest()
		{
			var task = new StudyTask(base.client, "taskName", this.UserId);
			StudyTaskCollection.Create(base.client).Add(task);
			task.TimeSpans.Add(new TaskTimeSpan(task, DateTime.Now));
			task.TimeSpans.RemoveAt(0);

			var result = client.GetTimeSpansFor(task.Service.Id);
			Assert.AreEqual(0, result.Length);
		}
		[TestMethod]
		public void TaskRetrievalTest()
		{
			const string expected = "taskName";
			var task = new StudyTask(base.client, expected, this.UserId);
			StudyTaskCollection.Create(base.client).Add(task);

			task = new StudyTask(base.client, task.Service.Id);
			Assert.AreEqual(expected, task.Name);
		}
		[TestMethod]
		public void TaskRetrievalWithTimeSpansTest()
		{
			var task = new StudyTask(base.client, "taskName", this.UserId);
			StudyTaskCollection.Create(base.client).Add(task);
			task.TimeSpans.Add(new TaskTimeSpan(task, DateTime.Now));

			int expectedTimeSpanCount = 1;
			task = new StudyTask(base.client, task.Service.Id);
			Assert.AreEqual(expectedTimeSpanCount, task.TimeSpans.Count);
		}
	}
}

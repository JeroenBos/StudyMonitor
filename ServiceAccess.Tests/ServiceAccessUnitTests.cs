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

	    private StudyTask CreateStudyTaskWithName(string name)
	    {
	        return new StudyTask(base.client, name, this.UserId, TimeSpan.FromSeconds(10));
        }

		[TestMethod]
		public void AddTaskTest()
		{
			const string expected = "taskName";
			var task = CreateStudyTaskWithName(expected);
			StudyTaskCollection.Create(base.client).Add(task);

			var result = client.GetTask(task.MessageObject.Id).Name;
			Assert.AreEqual(expected, result);
		}
		[TestMethod]
		public void AddTimeSpanTest()
		{
			const string expected = "taskName";
		    var task = CreateStudyTaskWithName(expected);
			StudyTaskCollection.Create(base.client).Add(task);
			task.TimeSpans.Add(new TaskTimeSpan(base.client, task, DateTime.Now));

			var result = client.GetTask(task.MessageObject.Id).Name;
			Assert.AreEqual(expected, result);
		}
		[TestMethod]
		public void RemoveTaskTest()
		{
			var tasks = StudyTaskCollection.Create(base.client);
			var task = CreateStudyTaskWithName("name");
			tasks.Add(task);
			tasks.Remove(task);

			var result = client.GetTask(task.MessageObject.Id);
			Assert.IsNull(result);
		}
		[TestMethod]
		public void RemoveTimeSpanTest()
		{
			var task = CreateStudyTaskWithName("taskName");
			StudyTaskCollection.Create(base.client).Add(task);
			task.TimeSpans.Add(new TaskTimeSpan(base.client, task, DateTime.Now));
			task.TimeSpans.RemoveAt(0);

			var result = client.GetTimeSpansFor(task.MessageObject.Id);
			Assert.AreEqual(0, result.Length);
		}
		[TestMethod]
		public void TaskRetrievalTest()
		{
			const string expected = "taskName";
			var task = CreateStudyTaskWithName(expected);
			StudyTaskCollection.Create(base.client).Add(task);

			task = new StudyTask(base.client, task.MessageObject.Id);
			Assert.AreEqual(expected, task.Name);
		}
		[TestMethod]
		public void TaskRetrievalWithTimeSpansTest()
		{
			var task = CreateStudyTaskWithName("taskName");
			StudyTaskCollection.Create(base.client).Add(task);
			task.TimeSpans.Add(new TaskTimeSpan(base.client, task, DateTime.Now));

			int expectedTimeSpanCount = 1;
			task = new StudyTask(base.client, task.MessageObject.Id);
			Assert.AreEqual(expectedTimeSpanCount, task.TimeSpans.Count);
		}
		[TestMethod]
		public void SetTaskTimeSpanEndTest()
		{
			var task = CreateStudyTaskWithName("taskName");
			StudyTaskCollection.Create(base.client).Add(task);
			task.TimeSpans.Add(new TaskTimeSpan(base.client, task, DateTime.Now));
			task.TimeSpans[0].End = DateTime.Today;

			var taskTimeSpanEndFromDatabase = client.GetTimeSpan(task.TimeSpans[0].timeSpanMessageObject.Id).End;
			Assert.IsNotNull(taskTimeSpanEndFromDatabase);
		}
		[TestMethod]
		public void CumulativeTaskTimeSpansLengthTest()
		{
			var timeSpansInSeconds = new int[] { 5, 3, 10 };

			var task = CreateStudyTaskWithName("taskName");
			StudyTaskCollection.Create(base.client).Add(task);

			DateTime now = DateTime.Now;
			foreach(var seconds in timeSpansInSeconds)
			{
				var taskTimeSpan = new TaskTimeSpan(base.client, task, now);
				task.TimeSpans.Add(taskTimeSpan);
				taskTimeSpan.End = now + TimeSpan.FromSeconds(seconds);
			}

			var expectedCumulativeLength = TimeSpan.FromSeconds(timeSpansInSeconds.Sum());
			Assert.AreEqual(expectedCumulativeLength, task.GetLength());
		}
		[TestMethod]
		public void ChangeTaskEstimateTest()
		{
			var task = CreateStudyTaskWithName("taskName");
			StudyTaskCollection.Create(base.client).Add(task);

			var expected = TimeSpan.FromSeconds(99);
			task.Estimate = expected;

			var taskEstimateFromDatabase = client.GetTask(task.MessageObject.Id).Estimate;
			Assert.AreEqual(expected, taskEstimateFromDatabase);
		}
	}
}

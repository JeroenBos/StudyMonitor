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
		public void CreateServiceAccessTest()
		{
			var task = new StudyTask(base.client, "taskName");
		}
		[TestMethod]
		public void AddTaskTest()
		{
			var task = new StudyTask(base.client, "taskName");
			task.TimeSpans.Add(new TaskTimeSpan(task, DateTime.Now));

		}
	}
}

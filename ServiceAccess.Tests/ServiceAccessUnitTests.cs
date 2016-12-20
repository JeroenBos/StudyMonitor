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
		public void CreateServiceAccessTest()
		{
			var task = new StudyTask(base.service, base.client);
		}
		[TestMethod]
		public void AddTaskTest()
		{
			var task = new StudyTask(base.service, base.client);
			task.TimeSpans.Add(new TaskTimeSpan(new ServiceReference.TaskTimeSpanService()));
		}
	}
}

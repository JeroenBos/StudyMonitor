using StudyMonitor.ServiceAccess.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyMonitor.ServiceAccess.Tests
{
	/// <summary> Contains the WCF end point configuration. </summary>
	public class SharedUnitTestsConfiguration
	{
		protected readonly StudyTasksServiceClient client = new StudyTasksServiceClient("BasicHttpBinding_IStudyTasksService");
	}
}

using System;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Web.Mvc;
using StudyMonitor.ServiceAccess.ServiceReference;
using Website.Models;
using StudyMonitor.ServiceAccess;

namespace Website.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var client = CreateTasksClient();
			var allTasks = StudyTaskCollection.FromDatabase(client);

			allTasks.Add(new StudyTask(client, "TestCase")); // is added to database as well

			return View(allTasks);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

        [HttpPost]
		public ActionResult Select(object data)
		{
			string taskId = ((string[])data)[0];
			int id;
			bool validData = int.TryParse(taskId, out id);
			if (validData)
			{
				var task = new StudyTask(CreateTasksClient(), id);
				task.TimeSpans.Add(new TaskTimeSpan(task, DateTime.Now));
			}

			return View();
		}

		public ActionResult Add(object data)
		{
			string taskName = ((string[])data)[0];
			// Check the string for a valid task name
			if (true)
			{
				var client = CreateTasksClient();
				var databaseConnection = StudyTaskCollection.FromDatabase(client);
				databaseConnection.Add(new StudyTask(client, taskName));
			}

			return View();
		}

		/// <summary> Encapsulates the construction of a <see cref="StudyTasksServiceClient"/>. </summary>
		private StudyTasksServiceClient CreateTasksClient()
		{
			return new StudyTasksServiceClient("BasicHttpBinding_IStudyTasksService");
		}
	}
}
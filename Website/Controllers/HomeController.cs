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
			client.Add(new StudyTaskService() { Name = "TestCase" });
			var studyTasksModel = new StudyTasksModel
			{
				StudyTaskModels = client.GetAllTasks().Select(
					e => new StudyTaskModel() { Id = e.Id, Name = e.Name }).ToList()
			};
			return View(studyTasksModel);
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

			return View("Index");
		}

		public ActionResult Add(object data)
		{
			string taskName = ((string[])data)[0];
			// Check the string for a valid task name
			if (true)
			{
				StudyTasksServiceClient client = CreateTasksClient();
				client.Add(new StudyTaskService() { Name = taskName });
			}

			return View("Index");
		}

		/// <summary> Encapsulates the construction of a <see cref="StudyTasksServiceClient"/>. </summary>
		private StudyTasksServiceClient CreateTasksClient()
		{
			return new StudyTasksServiceClient("BasicHttpBinding_IStudyTasksService");
		}
	}
}
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StudyMonitor.ServiceAccess.ServiceReference;
using Website.Models;
using StudyMonitor.ServiceAccess;
using Website.Views.Helpers;

namespace Website.Controllers
{
	public class HomeController : Controller
	{
		/// <summary>
		/// Creates a Study Tasks and loads all study tasks to be shown
		/// </summary>
		/// <returns>A view which shows all tasks</returns>
		public ActionResult Index()
		{
			var client = CreateTasksWCFService();
			var userId = User.Identity.GetUserId();
			if (userId != null)
			{
				var userTasks = StudyTaskCollection.FromDatabase(client, userId);
				return View(userTasks);
			}


			return View();
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
		public void Remove(string taskId)
		{
			int id;
			if (int.TryParse(taskId, out id))
			{
				var service = CreateTasksWCFService();
				service.RemoveTask(id);
			}
		}
		/// <summary>
		/// This method is invoked when the client selects a task
		/// </summary>
		/// <param name="data">A string array with the taskId at index 0</param>
		/// <returns>a string of the task name, task total length and task estimate in seconds, separated by commas</returns>
		public string Select(string taskId, string taskWasOpen)
		{
			int id;
			bool taskOpen;
			if (int.TryParse(taskId, out id) && bool.TryParse(taskWasOpen, out taskOpen))
			{
				StudyTask task;
				var service = CreateTasksWCFService();
				if (!taskOpen)
				{
					task = new StudyTask(service, id);
					task.TimeSpans.Add(new TaskTimeSpan(service, task, DateTime.Now));
				}
				else
				{
					task = new StudyTask(service, id);
					var openTimeSpan = task.OpenTimeSpan;
					if (openTimeSpan != null)
					{
						openTimeSpan.End = DateTime.Now;
					}
				}

				return string.Join(",", task.Name, task.GetLength().ToStringInSeconds(), task.Estimate.ToStringInSeconds());
			}
			return 0.ToString();
		}

		/// <summary>
		/// This method is invoked when the client adds a task
		/// </summary>
		/// <param name="taskName">The name of the task</param>
		/// <param name="estimateString">String representation of the estimated for the new task</param>
		/// <returns>a string of the task id and task total length in seconds, separated by commas</returns>
		public string Add(string taskName, string estimateString)
		{
			int estimate;
			// Check the string for a valid task name
			if (int.TryParse(estimateString, out estimate))
			{
				string userId = User.Identity.GetUserId();
				var client = CreateTasksWCFService();
				var databaseConnection = StudyTaskCollection.FromDatabase(client, userId);
				var task = new StudyTask(client, taskName, userId, TimeSpan.FromSeconds(estimate));
				databaseConnection.Add(task);


				var totalLength = task.GetLength();
				return string.Join(",", task.Id, totalLength.ToStringInSeconds());
			}
			return 0.ToString();
		}

		public ActionResult TasksInfo()
		{
			var client = CreateTasksWCFService();
			var userId = User.Identity.GetUserId();
			if (userId != null)
			{
				var userTasks = StudyTaskCollection.FromDatabase(client, userId);
				return View(userTasks);
			}


			return View();
		}
		/// <summary> Encapsulates the construction of a <see cref="StudyTasksServiceClient"/>. </summary>
		private StudyTasksServiceClient CreateTasksWCFService()
		{
			return new StudyTasksServiceClient("BasicHttpBinding_IStudyTasksService");
		}
	}
}
﻿using System;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StudyMonitor.ServiceAccess.ServiceReference;
using Website.Models;
using StudyMonitor.ServiceAccess;

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
			var client = CreateTasksClient();
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

        /// <summary>
        /// This method is invoked when the client selects a task
        /// </summary>
        /// <param name="data">A string array with the taskId at index 0</param>
        /// <returns>Nothing</returns>
        [HttpPost]
		public ActionResult Select(string taskId, string taskWasOpen)
		{
			int id;
            bool taskOpen;
			if (int.TryParse(taskId, out id) && bool.TryParse(taskWasOpen, out taskOpen))
			{
				var service = CreateTasksClient();
				if (!taskOpen)
			    {
			        var task = new StudyTask(service, id);
			        task.TimeSpans.Add(new TaskTimeSpan(service, task, DateTime.Now));
			    }
			    else
			    {
					var openTimeSpan = new StudyTask(service, id).OpenTimeSpan;
			        if (openTimeSpan != null)
			        {
			            openTimeSpan.End = DateTime.Now;
			        }
			    }
			}

			return View();
		}

	    /// <summary>
	    /// This method is invoked when the client adds a task
	    /// </summary>
	    /// <param name="taskName">The name of the task</param>
		/// <param name="estimateString">String representation of the estimated for the new task</param>
	    /// <returns>the task id created for the task name</returns>
		public string Add(string taskName, string estimateString)
		{
			DateTime estimate;
			string userId = User.Identity.GetUserId();
			// Check the string for a valid task name
			if (DateTime.TryParse(estimateString, out estimate))
			{
				var client = CreateTasksClient();
				var databaseConnection = StudyTaskCollection.FromDatabase(client, userId);
				var task = new StudyTask(client, taskName, userId, estimate);
				databaseConnection.Add(task);

				return task.Id.ToString();
			}
			return 0.ToString();
		}

		/// <summary> Encapsulates the construction of a <see cref="StudyTasksServiceClient"/>. </summary>
		private StudyTasksServiceClient CreateTasksClient()
		{
			return new StudyTasksServiceClient("BasicHttpBinding_IStudyTasksService");
		}
	}
}
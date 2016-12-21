﻿using System;
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
        /// <summary>
        /// Creates a Study Tasks and loads all study tasks to be shown
        /// </summary>
        /// <returns>A view which shows all tasks</returns>
		public ActionResult Index()
		{
			var client = CreateTasksClient();
			new StudyTask(client, "TestCase");// is added to database

			var allTasks = new StudyTasksModel
			{
				StudyTaskModels = StudyTask.GetAllTasksFromDatabase(client)
										   .Select(task => new StudyTaskModel() { Name = task.Name, Id = task.Id })
										   .ToList()
			};
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

        /// <summary>
        /// This method is invoked when the client selects a task
        /// </summary>
        /// <param name="data">A string array with the taskId at index 0</param>
        /// <returns>Nothing</returns>
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

        /// <summary>
        /// This method is invoked when the client adds a task
        /// </summary>
        /// <param name="data">A string array with the taskname at index 0</param>
        /// <returns>Nothing</returns>
        [HttpPost]
		public ActionResult Add(object data)
		{
			string taskName = ((string[])data)[0];
			// Check the string for a valid task name
			if (true)
			{
				// adds itself to database
				new StudyTask(CreateTasksClient(), taskName);
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
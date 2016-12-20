using System;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Web.Mvc;
using StudyMonitor.ServiceAccess.ServiceReference;
using Website.Models;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var client = new StudyTasksServiceClient("BasicHttpBinding_IStudyTasksService");
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
            string[] taskId = (string[])data;
            int id;
            bool validData = int.TryParse(taskId[0], out id);
            if (validData)
            {
                var startingTaskItemTimeSpan = new TaskTimeSpanService()
                {
                    End = null,
                    TaskId = id,
                    Start = DateTime.Now
                };

                StudyTasksServiceClient client = new StudyTasksServiceClient("BasicHttpBinding_IStudyTasksService");
                client.AddTimeSpanTo(id, startingTaskItemTimeSpan);
            }

            return View();
        }
	}
}
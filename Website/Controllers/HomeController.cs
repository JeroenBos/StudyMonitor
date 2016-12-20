using System.Web.Mvc;
using System.Web;
using StudyMonitor.ServiceAccess.ServiceReference;

namespace Website.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			StudyTaskService service = new StudyTaskService();
			StudyTasksServiceClient client = new StudyTasksServiceClient("BasicHttpBinding_IStudyTasksService");
			var task = client.GetTask(client.Add(new StudyTaskService() { Name = "Erik" }));
			ViewBag.Id = task.Id;
			ViewBag.Name = task.Name;
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

		public ActionResult Select()
		{
			return View();
		}
	}
}
using System.Linq;
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
            StudyTaskService service = new StudyTaskService();
            StudyTasksServiceClient client = new StudyTasksServiceClient("BasicHttpBinding_IStudyTasksService");
            StudyTasksModel studyTasksModel = new StudyTasksModel();
            studyTasksModel.StudyTaskModels = client.GetAllTasks().Select(
                e => new StudyTaskModel() {Id = e.Id, Name = e.Name}).ToList();
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
    }
}
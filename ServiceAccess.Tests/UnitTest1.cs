using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudyMonitor.ServiceAccess.ServiceReference;


namespace ServiceAccesTests
{
    [TestClass]
    public class UnitTests
    {
        public static void Main(string[] args)
        {
			new UnitTests().test();
            Console.ReadLine();
        }
        private readonly StudyTaskService service = new StudyTaskService();
        private readonly StudyTasksServiceClient client = new StudyTasksServiceClient("BasicHttpBinding_IStudyTasksService");

        [TestCleanup]
        public void calledEverytimeAfterATest()
        {
            //client.ClearAll();
        }

        [TestMethod]
        public void test()
        {
            client.Add(new StudyTaskService());
        }
    }


}

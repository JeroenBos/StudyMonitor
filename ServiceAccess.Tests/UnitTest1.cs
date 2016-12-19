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
			var testObject = new UnitTests();
			testObject.CalledEverytimeAfterATest();
			testObject.test();
			Console.WriteLine("Ran test");
            Console.ReadLine();
        }

        private readonly StudyTaskService service = new StudyTaskService();
        private readonly StudyTasksServiceClient client = new StudyTasksServiceClient("BasicHttpBinding_IStudyTasksService");

        [TestCleanup]
        public void CalledEverytimeAfterATest()
        {
            client.ClearAll();
        }

        [TestMethod]
        public void test()
        {
            client.Add(new StudyTaskService());
        }
    }


}

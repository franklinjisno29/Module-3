using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using Serilog;
using RestSharp;

namespace CaseStudyNunit.Utilities
{
    public class CoreCodes
    {
        protected RestClient client;
        protected ExtentReports extent;
        ExtentSparkReporter sparkReporter;
        protected ExtentTest test;
        private string baseUrl = "https://restful-booker.herokuapp.com";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            string currDir = Directory.GetParent(@"../../../").FullName;
            extent = new ExtentReports();
            sparkReporter = new ExtentSparkReporter(currDir + "/ExtentReports/extent-report"
                + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".html");
            extent.AttachReporter(sparkReporter);

            string? logfilePath = currDir + "/Logs/log_" + DateTime.Now.ToString("yyyy.mm.dd_HH.mm.ss") + ".txt";
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logfilePath, rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        [SetUp]
        public void SetUp()
        {
            client = new RestClient(baseUrl);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            extent.Flush();

        }
    }
}

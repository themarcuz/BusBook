using Xlns.Google.Maps.Directions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Xlns.BusBook.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for RequestTest and is intended
    ///to contain all RequestTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RequestTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion
        
        [TestMethod()]
        public void CreaRichiestaConSoloOrigineEDestinazione()
        {
            string origine = "43.8430139000000000,10.5079940000000530";
            string destinazione = "48.2081743000000000,16.3738189000000600";            
            Request target = new Request(origine, destinazione);
            string expected = "origin=43.8430139000000000,10.5079940000000530&destination=48.2081743000000000,16.3738189000000600&sensor=false";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);            
        }

        [TestMethod()]
        public void CreaRichiestaConUnWaypoint()
        {
            string origine = "43.8430139000000000,10.5079940000000530";
            string destinazione = "48.2081743000000000,16.3738189000000600";
            var waypoints = new List<String>() 
            {
                "44.4070624,8.933988900000031"
            };
            Request target = new Request(origine, destinazione);
            target.Waypoints = waypoints;
            string expected = "origin=43.8430139000000000,10.5079940000000530&destination=48.2081743000000000,16.3738189000000600&sensor=false&waypoints=44.4070624,8.933988900000031";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CreaRichiestaConQualcheWaypoint()
        {
            string origine = "43.8430139000000000,10.5079940000000530";
            string destinazione = "48.2081743000000000,16.3738189000000600";
            var waypoints = new List<String>() 
            {
                "44.4070624,8.933988900000031",
                "44.405,8.934",
                "44.407,8.938",
                "44.409,8.936"
            };
            Request target = new Request(origine, destinazione);
            target.Waypoints = waypoints;
            string expected = "origin=43.8430139000000000,10.5079940000000530"
                + "&destination=48.2081743000000000,16.3738189000000600"
                + "&sensor=false"
                + "&waypoints=44.4070624,8.933988900000031"
                + "|44.405,8.934"
                + "|44.407,8.938"
                + "|44.409,8.936";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}

using Xlns.BusBook.Core.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Xlns.BusBook.Core.Model;
using System.Collections.Generic;

namespace Xlns.BusBook.IntegrationTest
{
    
    
    /// <summary>
    ///This is a test class for AgenziaServiceTest and is intended
    ///to contain all AgenziaServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AgenziaServiceTest
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

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            ConfigurationManager.Configurator.configFileName = @"C:\Xlns\BuX\BusBook\Xlns.BusBook.UI.Web\Config\BusBook.config";
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


        /// <summary>
        ///A test for GetAllAgenzie
        ///</summary>
        [TestMethod()]
        public void getAllAgenzieTest()
        {
            AgenziaRepository target = new AgenziaRepository();
            
            IList<Agenzia> actual;
            actual = target.GetAllAgenzie();
            Assert.IsTrue(actual.Count > 1);
        }
    }
}
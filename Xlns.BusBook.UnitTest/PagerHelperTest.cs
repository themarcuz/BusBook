using Xlns.BusBook.UI.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace Xlns.BusBook.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for PagerHelperTest and is intended
    ///to contain all PagerHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PagerHelperTest
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


        /// <summary>
        ///A test for calcolaNumeroDiPagine
        ///</summary>
        [TestMethod()]
        public void numero_elementi_19_elementi_per_pagina_4_pagine_totali_5()
        {
            int numeroElementi = 19; 
            int numeroElementiPerPagina = 4;
            int expected = 5; 
            int actual;
            actual = PagerHelper.calcolaNumeroDiPagine(numeroElementi, numeroElementiPerPagina);
            Assert.AreEqual(expected, actual);
            
        }

        [TestMethod()]
        public void numero_elementi_19_elementi_per_pagina_5_pagine_totali_4()
        {
            int numeroElementi = 19;
            int numeroElementiPerPagina = 5;
            int expected = 4;
            int actual;
            actual = PagerHelper.calcolaNumeroDiPagine(numeroElementi, numeroElementiPerPagina);
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void numero_elementi_20_elementi_per_pagina_5_pagine_totali_4()
        {
            int numeroElementi = 20;
            int numeroElementiPerPagina = 5;
            int expected = 4;
            int actual;
            actual = PagerHelper.calcolaNumeroDiPagine(numeroElementi, numeroElementiPerPagina);
            Assert.AreEqual(expected, actual);

        }
    }
}

using SS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using SpreadsheetUtilities;

namespace SpreadsheetTests
{
    
    
    /// <summary>
    ///This is a test class for SpreadsheetTest and is intended
    ///to contain all SpreadsheetTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SpreadsheetTest
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
        ///A test for Spreadsheet Constructor
        ///</summary>
        [TestMethod()]
        public void SpreadsheetConstructorTest3()
        {
            Spreadsheet target = new Spreadsheet();
            HashSet<object> expected = new HashSet<object>();

            target.SetContentsOfCell("A4", "6");
            target.SetContentsOfCell("A5", "7");
            target.SetContentsOfCell("C3", "words");

            expected.Add("A4");
            expected.Add("A5");
            expected.Add("C3");

            IEnumerator<object> a;
            IEnumerator<object> e = expected.GetEnumerator();

            a = target.GetNamesOfAllNonemptyCells().GetEnumerator();
            while (e.MoveNext())
            {
                a.MoveNext();
                Assert.AreEqual(a.Current, e.Current);
            }
        }

        /// <summary>
        ///A test for Spreadsheet Constructor
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void SpreadsheetConstructorTest4()
        {
            Spreadsheet target = new Spreadsheet();
            HashSet<object> expected = new HashSet<object>();

            target.SetContentsOfCell("A??", "6");
            target.SetContentsOfCell("A5", "7");
            target.SetContentsOfCell("C3", "words");

            expected.Add("A4");
            expected.Add("A5");
            expected.Add("C3");

            IEnumerator<object> a;
            IEnumerator<object> e = expected.GetEnumerator();

            a = target.GetNamesOfAllNonemptyCells().GetEnumerator();
            while (e.MoveNext())
            {
                a.MoveNext();
                Assert.AreEqual(a.Current, e.Current);
            }
        }

        /// <summary>
        ///A test for Spreadsheet Constructor, four args.
        ///</summary>
        ///[
        [TestMethod()]
        public void SpreadsheetConstructorTest6()
        {
            //string filePath = @"C:\Users\Spaldex\Documents\Visual Studio 2010\Projects\PS5\TestResults\Spaldex_SPALDEX-PC 2012-10-15 22_16_59\Out\testyMcTesty"; 
            //Func<string, bool> isValid = (s)=>true; 
            //Func<string, string> normalize = s=>s.ToUpper(); 
            //string version = "default"; 
            //Spreadsheet target = new Spreadsheet(filePath, isValid, normalize, version);
            //double h = (double)target.GetCellContents("A4");
            //Assert.AreEqual(4, h);
        }

        /// <summary>
        ///A test for Spreadsheet Constructor, four args. File Read Error.
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void SpreadsheetConstructorTest()
        {
            string filePath = "test1";
            Func<string, bool> isValid = (s) => true;
            Func<string, string> normalize = s => s.ToUpper();
            string version = "A";
            Spreadsheet target = new Spreadsheet(filePath, isValid, normalize, version);
        }

        /// <summary>
        ///A test for Spreadsheet Constructor, three args.
        ///</summary>
        ///[
        [TestMethod()]
        public void SpreadsheetConstructorTest5()
        {
            Func<string, bool> isValid = (s) => true;
            Func<string, string> normalize = s => s.ToUpper();
            string version = "A";
            Spreadsheet target = new Spreadsheet(isValid, normalize, version);

            Assert.AreEqual(target.IsValid("A3"), true);
            Assert.AreEqual(target.Version, "A");

            ISet<string> cells = target.SetContentsOfCell("c4", "5");
            IEnumerator<string> e = cells.GetEnumerator();
            e.MoveNext();
            string newstring = e.Current;
            Assert.AreEqual(newstring, "C4");
        }

        /// <summary>
        ///A test for Spreadsheet Constructor, zero args.
        ///</summary>
        [TestMethod()]
        public void SpreadsheetConstructorTest2()
        {
            Spreadsheet target = new Spreadsheet();
            Assert.AreEqual(target.GetCellContents("A1"), "");

            target.SetContentsOfCell("A1", "1");
            Assert.AreEqual(target.GetCellContents("A1"), (double)1);
        }

        /// <summary>
        ///A test for GetCellContents
        ///</summary>
        [TestMethod()]
        public void GetCellContentsTest()
        {
            Spreadsheet target = new Spreadsheet();
            string name = "A4";
            target.SetContentsOfCell(name, "8");
            object expected = "";
            object actual;
            actual = target.GetCellContents("c6");
            Assert.AreEqual(expected, actual);


            double d = 8;
            expected = d;
            actual = target.GetCellContents(name);
            Assert.AreEqual(expected, actual);

            name = "A088";
            target.GetCellContents(name); //no exception expected
            target.SetContentsOfCell(name, "=A4");
            actual = target.GetCellContents(name);
            expected = "A4";
            Assert.AreEqual(expected, actual.ToString());
        }

        /// <summary>
        ///A test for GetCellContents
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void GetCellContentsTest1()
        {
            Spreadsheet target = new Spreadsheet();
            string name = "A??";

            target.GetCellContents(name);

        }


        /// <summary>
        ///A test for GetCellContents
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void GetCellContentsTest2()
        {
            Spreadsheet target = new Spreadsheet();
            string name = null;
            target.GetCellContents(name);

        }

        /// <summary>
        ///A test for GetCellValue
        ///</summary>
        [TestMethod()]
        public void GetCellValueTest()
        {
            //test 1
            Spreadsheet target = new Spreadsheet();
            string name = "A4";
            target.SetContentsOfCell(name, "8");
            double d = 8;
            object expected = d;
            object actual = target.GetCellValue(name);
            Assert.AreEqual(expected, actual);

            //test 2
            expected = "";
            actual = target.GetCellValue("c6");
            Assert.AreEqual(expected, actual);

            //test 3
            expected = "word";
            target.SetContentsOfCell("c6", "word");
            actual = target.GetCellValue("c6");
            Assert.AreEqual(expected, actual);

            //test 4: should return 20
            name = "c6";
            target.SetContentsOfCell(name, "=A4 * 2 + 4");
            actual = target.GetCellValue(name);
            expected = (double) 20;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetCellValue
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void GetCellValueTest1()
        {
            Spreadsheet target = new Spreadsheet();
            target.GetCellValue("p");
        }

        /// <summary>
        ///A test for GetCellValue
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void GetCellValueTest2()
        {
            Spreadsheet target = new Spreadsheet();
            target.GetCellValue(null);
        }

        /// <summary>
        ///A test for GetDirectDependents
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Spreadsheet.dll")]
        public void GetDirectDependentsTest()
        {
            Spreadsheet_Accessor target = new Spreadsheet_Accessor();
            HashSet<object> e = new HashSet<object>();

            target.SetCellContents("A4", new Formula("C3 + C2", (s)=>true, s=>s.ToUpper()));
            target.SetCellContents("C3", new Formula("C4*3", (s) => true, s => s.ToUpper()));
            target.SetCellContents("C2", 5);

            e.Add("C3");
            e.Add("C2");

            IEnumerator<object> actual;
            IEnumerator<object> expected = e.GetEnumerator();

            actual = target.GetDirectDependents("A4").GetEnumerator();

            while (expected.MoveNext())
            {
                actual.MoveNext();
                Assert.AreEqual(actual.Current, expected.Current);
            }
        }

        /// <summary>
        ///A test for GetDirectDependents : ArgumentNullException
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Spreadsheet.dll")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetDirectDependentsTest1()
        {
            Spreadsheet_Accessor target = new Spreadsheet_Accessor();
            string s = null;
            IEnumerator<object> actual;
            actual = target.GetDirectDependents(s).GetEnumerator();

        }

        /// <summary>
        ///A test for GetDirectDependents : InvalidNameException
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Spreadsheet.dll")]
        [ExpectedException(typeof(InvalidNameException))]
        public void GetDirectDependentsTest2()
        {
            Spreadsheet_Accessor target = new Spreadsheet_Accessor();
            string s = "A04!";
            IEnumerator<object> actual;
            actual = target.GetDirectDependents(s).GetEnumerator();

        }

        /// <summary>
        ///A test for GetNamesOfAllNonemptyCells
        ///</summary>
        [TestMethod()]
        public void GetNamesOfAllNonemptyCellsTest()
        {
            Spreadsheet target = new Spreadsheet();
            string[] s = new string[5];
            s[0] = "aaaakkkkk333";
            s[1] = "C43";
            s[2] = "Z4";
            s[3] = "XX2";
            s[4] = "A1";
            target.SetContentsOfCell(s[0], "5");
            target.SetContentsOfCell(s[1], "w");
            target.SetContentsOfCell(s[2], s[3] + "/ 2");
            target.SetContentsOfCell(s[3], "8");
            target.SetContentsOfCell(s[4], s[0] + "+ 3");

            HashSet<string> expected = new HashSet<string>();
            IEnumerable<string> actual;
            actual = target.GetNamesOfAllNonemptyCells();

            foreach (string n in actual)
                expected.Add(n);
            Assert.IsTrue(expected.Contains(s[0]));
            Assert.IsTrue(expected.Contains(s[1]));
            Assert.IsTrue(expected.Contains(s[2]));
            Assert.IsTrue(expected.Contains(s[3]));
            Assert.IsTrue(expected.Contains(s[4]));
        }

        /// <summary>
        ///A test for GetSavedVersion
        ///</summary>
        [TestMethod()]
        public void GetSavedVersionTest()
        {
            Func<string, bool> isValid = (s) => true;
            Func<string, string> normalize = s => s.ToUpper();
            string version = "A";
            Spreadsheet target = new Spreadsheet(isValid, normalize, version);

            //Assert.AreEqual(target.GetSavedVersion(Solution.Item(testyMcTesty), "default");

        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            Spreadsheet target = new Spreadsheet();
            target.SetContentsOfCell("A4", "4");
            target.SetContentsOfCell("b4", "string");
            target.SetContentsOfCell("c4", "=A4+2");
            string filename = "testyMcTesty"; 
            //refer to file in Solution Items for formatting, etc.
            target.Save(filename);
        }


        /// <summary>
        ///A test for SetContentsOfCell
        ///</summary>
        [TestMethod()]
        public void SetContentsOfCellTest()
        {
            Spreadsheet target = new Spreadsheet();

            target.SetContentsOfCell("A4", "8");
            target.SetContentsOfCell("B4", "A4");
            Assert.AreEqual((double) 8, target.GetCellValue("A4"));
            Assert.AreEqual("A4", target.GetCellValue("B4"));
            target.SetContentsOfCell("C4", "=A4*9");
            Assert.AreEqual((double)72, target.GetCellValue("C4"));
            target.SetContentsOfCell("D4", "=Z4");
            object o = target.GetCellValue("D4");
            bool b = (o.GetType() == typeof(FormulaError));
            Assert.IsTrue(b);
            target.SetContentsOfCell("asdasdasda4", "a");
            target.SetContentsOfCell("asdasdasda00003", "a");
            target.SetContentsOfCell("a0004", "a");
            target.SetContentsOfCell("a444444444", "a");
            target.SetContentsOfCell("a0004", "word");
            target.SetContentsOfCell("A4", "10");
            target.SetContentsOfCell("C4", "=A4*9");



        }

        /// <summary>
        ///A test for SetContentsOfCell
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetContentsOfCellTest1()
        {
            Spreadsheet target = new Spreadsheet();
            target.SetContentsOfCell(null, "a");
        }

        /// <summary>
        ///A test for SetContentsOfCell
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetContentsOfCellTest2()
        {
            Spreadsheet target = new Spreadsheet();
            target.SetContentsOfCell("p", "a");

        }

        /// <summary>
        ///A test for SetContentsOfCell
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetContentsOfCellTest3()
        {
            Spreadsheet target = new Spreadsheet();
            target.SetContentsOfCell("", "");
            target.SetContentsOfCell("asdasdasda4asdasdasda", "a");

        }

        /// <summary>
        ///A test for SetContentsOfCell
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetContentsOfCellTest4()
        {
            Spreadsheet target = new Spreadsheet();
            target.SetContentsOfCell("asdasdasda4asdasdasda", "a");

        }

        /// <summary>
        ///A test for SetContentsOfCell
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(CircularException))]
        public void SetContentsOfCellTest5()
        {
            Spreadsheet target = new Spreadsheet();

            target.SetContentsOfCell("A4", "8");
            target.SetContentsOfCell("B4", "=A4");
            target.SetContentsOfCell("A4", "=B4");

        }

        /// <summary>
        ///A test for SetContentsOfCell
        ///</summary>
        [TestMethod()]
        public void SetContentsOfCellTest6()
        {
            Spreadsheet target = new Spreadsheet();

            target.SetContentsOfCell("A4", "8");
            target.SetContentsOfCell("B4", "=A4");
            target.SetContentsOfCell("C4", "=B4 + A4");
            target.SetContentsOfCell("B4", "20");

        }

        /// <summary>
        ///A test for SetContentsOfCell
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(CircularException))]
        public void SetContentsOfCellTest7()
        {
            Spreadsheet target = new Spreadsheet();

            target.SetContentsOfCell("A5", "=A4");
            target.SetContentsOfCell("A4", "8");
            target.SetContentsOfCell("B4", "=A5");
            target.SetContentsOfCell("C4", "=B4 + A4");
            target.SetContentsOfCell("A5", "=C4 + A3");

        }

        /// <summary>
        ///A test for SetContentsOfCell
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetContentsOfCellTest8()
        {
            Spreadsheet target = new Spreadsheet();

            target.SetContentsOfCell("A5", null);

        }

        /// <summary>
        ///A test for Changed
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Spreadsheet.dll")]
        public void ChangedTest()
        {
            Spreadsheet_Accessor target = new Spreadsheet_Accessor();
            Assert.IsFalse(target.Changed);

            target.SetContentsOfCell("A4", "it has now changed");
            Assert.IsTrue(target.Changed);

            Spreadsheet_Accessor target1 = new Spreadsheet_Accessor();
            target1.SetContentsOfCell("A4", "8");
            Assert.IsTrue(target1.Changed);

            Spreadsheet_Accessor target2 = new Spreadsheet_Accessor();
            target2.SetContentsOfCell("A4", "=12-4");
            Assert.IsTrue(target2.Changed);
        }

    }
}

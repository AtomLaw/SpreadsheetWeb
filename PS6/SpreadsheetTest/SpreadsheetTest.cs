using SS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using SpreadsheetUtilities;

namespace SpreadsheetTest
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
        public void SpreadsheetConstructorTest()
        {
            Spreadsheet_Accessor target = new Spreadsheet_Accessor();
            HashSet<object> expected = new HashSet<object>();

            target.SetCellContents("A4", 6);
            target.SetCellContents("A5", 7);
            target.SetCellContents("C3", "words");

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
        public void SpreadsheetConstructorTest1()
        {
            Spreadsheet_Accessor target = new Spreadsheet_Accessor();

            HashSet<object> expected = new HashSet<object>();

            target.SetCellContents("A??", 6);
            target.SetCellContents("A5", 7);
            target.SetCellContents("C3", "words");

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
        ///A test for GetCellContents
        ///</summary>
        [TestMethod()]
        public void GetCellContentsTest()
        {
            Spreadsheet_Accessor target = new Spreadsheet_Accessor();

            string name = "A4";
            target.SetCellContents(name, 8);
            object expected = "";
            object actual;
            actual = target.GetCellContents("c6");
            Assert.AreEqual(expected, actual);

            double d = 8;
            expected = d;
            actual = target.GetCellContents(name);

            Assert.AreEqual(expected, actual);
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
            string name = "A088";
            target.GetCellContents(name);

        }

        /// <summary>
        ///A test for GetCellContents
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void GetCellContentsTest3()
        {
            Spreadsheet target = new Spreadsheet();
            string name = null;
            target.GetCellContents(name);

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

            target.SetCellContents("A4", new Formula("C3 + C2", s=>true, s=>s.ToUpper()));
            target.SetCellContents("C3", new Formula("C4*3", s => true, s => s.ToUpper()));
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
            string s = "A04";
            IEnumerator<object> actual;
            actual = target.GetDirectDependents(s).GetEnumerator();

        }

        /// <summary>
        ///A test for GetNamesOfAllNonemptyCells
        ///</summary>
        [TestMethod()]
        public void GetNamesOfAllNonemptyCellsTest()
        {
            Spreadsheet_Accessor target = new Spreadsheet_Accessor();
            string[] s = new string[5];
            s[0] = "aaaakkkkk333";
            s[1] = "C43";
            s[2] = "Z4";
            s[3] = "XX2";
            s[4] = "A1";
            target.SetCellContents(s[0], 5);
            target.SetCellContents(s[1], "w");
            target.SetCellContents(s[2], new Formula(s[3] + "/ 2", q => true, q => q.ToUpper()));
            target.SetCellContents(s[3], 8);
            target.SetCellContents(s[4], new Formula(s[0] + "+ 3", q => true, q => q.ToUpper()));

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
        ///A test for SetCellContents : vast
        ///</summary>
        [TestMethod()]
        public void SetCellContentsTest()
        {
            Spreadsheet_Accessor target = new Spreadsheet_Accessor();
            string[] s = new string[6];
            s[0] = "B52";
            s[1] = "C43";
            s[2] = "Z4";
            s[3] = "XX2";
            s[4] = "A1";
            s[5] = "T6";

            target.SetCellContents(s[0], 5);
            target.SetCellContents(s[1], "w");
            target.SetCellContents(s[1], 6);
            target.SetCellContents(s[1], new Formula("10+32", q => true, q => q.ToUpper()));
            target.SetCellContents(s[1], new Formula(s[5], q => true, q => q.ToUpper()));
            target.SetCellContents(s[5], "ow");
            target.SetCellContents(s[5], "wow");
            target.SetCellContents(s[3], 8);
            target.SetCellContents(s[4], new Formula(s[0] + "+ 3", q => true, q => q.ToUpper()));
            target.SetCellContents(s[4], new Formula(s[0] + " + " + s[3], q => true, q => q.ToUpper()));

            string name = string.Empty;
            string text = string.Empty;
            ISet<string> expected = new HashSet<string>();
            ISet<string> actual;
            actual = target.SetCellContents(s[2], new Formula(s[3] + "/ 2 + " + s[4], q => true, q => q.ToUpper()));

            expected.Add("Z4");
            expected.Add("A1");
            expected.Add("B52");
            expected.Add("XX2");

            IEnumerator<string> e = expected.GetEnumerator();
            IEnumerator<string> a = actual.GetEnumerator();

            while (a.MoveNext())
            {
                e.MoveNext();
                Assert.AreEqual(a.Current, e.Current);
            }

        }

        /// <summary>
        ///A test for SetCellContents(string, string) Test for ArgumentNullException.
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetCellContentsTest1()
        {
            Spreadsheet_Accessor target = new Spreadsheet_Accessor();
            string[] s = new string[5];
            s[0] = "B52";
            s[1] = "C43";
            s[2] = "Z4";
            s[3] = "XX2";
            s[4] = null;

            target.SetCellContents(s[0], s[4]);
        }

        /// <summary>
        ///A test for SetCellContents(string, string). Test for InvalidNameException: null.
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellContentsTest2()
        {
            Spreadsheet_Accessor target = new Spreadsheet_Accessor();
            string[] s = new string[5];
            s[0] = "B52";
            s[1] = "C43";
            s[2] = "Z4";
            s[3] = "A06";
            s[4] = null;

            target.SetCellContents(s[4], s[2]);
        }

        /// <summary>
        ///A test for SetCellContents(string, string). Test for InvalidNameException: invalid name.
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellContentsTest3()
        {
            Spreadsheet_Accessor target = new Spreadsheet_Accessor();
            string[] s = new string[5];
            s[0] = "B52";
            s[1] = "C43";
            s[2] = "Z4";
            s[3] = "A06";
            s[4] = null;

            target.SetCellContents(s[3], s[2]);
        }

        /// <summary>
        ///A test for SetCellContents(string, double). Test for InvalidNameException: null.
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellContentsTest4()
        {
            Spreadsheet_Accessor target = new Spreadsheet_Accessor();
            string[] s = new string[5];
            s[0] = "B52";
            s[1] = "C43";
            s[2] = "Z4";
            s[3] = "A06";
            s[4] = null;

            target.SetCellContents(s[4], 6);
        }

        /// <summary>
        ///A test for SetCellContents(string, string). Test for InvalidNameException: invalid name.
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellContentsTest5()
        {
            Spreadsheet_Accessor target = new Spreadsheet_Accessor();
            string[] s = new string[5];
            s[0] = "B52";
            s[1] = "C43";
            s[2] = "Z4";
            s[3] = "A06";
            s[4] = null;

            target.SetCellContents(s[3], 6);
        }

        /// <summary>
        ///A test for SetCellContents(string, formula) Test for ArgumentNullException.
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetCellContentsTest6()
        {
            Spreadsheet_Accessor target = new Spreadsheet_Accessor();
            string[] s = new string[5];
            s[0] = "B52";
            s[1] = "C43";
            s[2] = "Z4";
            s[3] = "XX2";
            s[4] = null;
            Formula f = null;

            target.SetCellContents(s[0], f);
        }

        /// <summary>
        /// A test for SetCellContents(string, formula). Test for InvalidNameException: null.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellContentsTest7()
        {
            Spreadsheet_Accessor target = new Spreadsheet_Accessor();
            string[] s = new string[5];
            s[0] = "B52";
            s[1] = "C43";
            s[2] = "Z4";
            s[3] = "A06";
            s[4] = null;
            Formula f = new Formula(s[0] + " + " + s[1], q => true, q => q.ToUpper());

            target.SetCellContents(s[4], f);
        }

        /// <summary>
        ///A test for SetCellContents(string, formula). Test for InvalidNameException: invalid name.
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void SetCellContentsTest8()
        {
            Spreadsheet_Accessor target = new Spreadsheet_Accessor();
            string[] s = new string[5];
            s[0] = "B52";
            s[1] = "C43";
            s[2] = "Z4";
            s[3] = "A06";
            s[4] = null;
            Formula f = new Formula(s[0] + " + " + s[1], q => true, q => q.ToUpper());

            target.SetCellContents(s[3], f);
        }

        /// <summary>
        ///A test for SetCellContents(string, formula). Test for Circular Exception.
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(CircularException))]
        public void SetCellContentsTest9()
        {
            Spreadsheet_Accessor target = new Spreadsheet_Accessor();
            string[] s = new string[5];
            s[0] = "A1";
            s[1] = "A2";
            s[2] = "A3";
            s[3] = "A4";
            s[4] = "A5";
            Formula f1 = new Formula(s[0] + " + " + s[1], q => true, q => q.ToUpper());
            Formula f2 = new Formula(s[0] + " + " + s[2], q => true, q => q.ToUpper());

            target.SetCellContents(s[2], f1);
            target.SetCellContents(s[1], f2);

        }

        /// <summary>
        ///A test for SetCellContents(string, formula). Test for Circular Exception : original replaced.
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(CircularException))]
        public void SetCellContentsTest10()
        {
            Spreadsheet_Accessor target = new Spreadsheet_Accessor();
            string[] s = new string[5];
            s[0] = "A1";
            s[1] = "A2";
            s[2] = "A3";
            s[3] = "A4";
            s[4] = "A5";
            Formula f1 = new Formula(s[0] + " + " + s[1], q => true, q => q.ToUpper());
            Formula f2 = new Formula(s[0] + " + " + s[3], q => true, q => q.ToUpper());
            Formula f3 = new Formula(s[3] + " + " + s[2], q => true, q => q.ToUpper());


            target.SetCellContents(s[2], f1);
            target.SetCellContents(s[1], f2);
            target.SetCellContents(s[1], f3);


        }
    }
}

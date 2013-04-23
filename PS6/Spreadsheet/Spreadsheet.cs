using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpreadsheetUtilities;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

namespace SS
{
    /// <summary>
    /// A Spreadsheet object is the data structure for the 
    /// spreadsheet GUI.  It inherits the abstract functions of AbstractSpreadsheet.
    /// A spreadsheet consists of an infinite number of named cells.
    /// 
    /// Rules from the Inherited Base Class:
    /// 
    /// A string is a cell name if and only if it consists of one or more letters, 
    /// followed by a non-zero digit, followed by zero or more digits.
    /// 
    /// For example, "A15", "a15", "XY32", and "BC7" are cell names.  (Note that despite
    /// their similarity, "A15" and "a15" are different cell names.)  On the other hand, 
    /// "Z", "X07", and "hello" are not cell names."
    /// 
    /// A spreadsheet contains a cell corresponding to every possible cell name.  
    /// In addition to a name, each cell has a contents and a value.  The distinction is
    /// important.
    /// 
    /// The contents of a cell can be (1) a string, (2) a double, or (3) a Formula.  If the
    /// contents is an empty string, we say that the cell is empty.  (By analogy, the contents
    /// of a cell in Excel is what is displayed on the editing line when the cell is selected.)
    /// 
    /// In a new spreadsheet, the contents of every cell is the empty string.
    ///  
    /// The value of a cell can be (1) a string, (2) a double, or (3) a FormulaError.  
    /// (By analogy, the value of an Excel cell is what is displayed in that cell's position
    /// in the grid.)
    /// 
    /// If a cell's contents is a string, its value is that string.
    /// 
    /// If a cell's contents is a double, its value is that double.
    /// 
    /// If a cell's contents is a Formula, its value is either a double or a FormulaError,
    /// as reported by the Evaluate method of the Formula class.  The value of a Formula,
    /// of course, can depend on the values of variables.  The value of a variable is the 
    /// value of the spreadsheet cell it names (if that cell's value is a double) or 
    /// is undefined (otherwise).
    /// 
    /// Spreadsheets are never allowed to contain a combination of Formulas that establish
    /// a circular dependency.  A circular dependency exists when a cell depends on itself.
    /// For example, suppose that A1 contains B1*2, B1 contains C1*2, and C1 contains A1*2.
    /// A1 depends on B1, which depends on C1, which depends on A1.  That's a circular
    /// dependency. 
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet 
    {
        //private fields of the Spreadsheet class
        private Dictionary<string, Cell> spreadsheet; // library set to store the names and cells of the spreadsheet
        // the DependencyGraph that handles the dependent relationships of the cells of the spreadsheet
        private DependencyGraph dg;

        private Queue<string> changesQueue;

        private bool changed;

        /// <inheritdoc />
        public Spreadsheet()
            :base (x => true, y => y, "default") //stub
        {
            //instantiate Spreadsheet fields
            spreadsheet = new Dictionary<string, Cell>();
            dg = new DependencyGraph();

            changed = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isValid"></param>
        /// <param name="normalize"></param>
        /// <param name="version"></param>
        public Spreadsheet(Func<string, bool> isValid, Func<string, string> normalize, string version)
            : base(isValid, normalize, version)
        {
            //instantiate Spreadsheet fields
            spreadsheet = new Dictionary<string, Cell>();
            dg = new DependencyGraph();

            changed = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="isValid"></param>
        /// <param name="normalize"></param>
        /// <param name="version"></param>
        public Spreadsheet(string filePath, Func<string, bool> isValid, Func<string, string> normalize, string version)
            : base(isValid, normalize, version)
        {
            //instantiate Spreadsheet fields
            spreadsheet = new Dictionary<string, Cell>();
            dg = new DependencyGraph();

          

            //if there is a version mismatch, throw exception
            if (!version.Equals(GetSavedVersion(filePath)))
                throw new SpreadsheetReadWriteException("The version of this saved file does not match the version requested.");

            string cellName = "", contents = "";

            try
            {
                using (XmlReader r = XmlReader.Create(new StringReader(filePath)))
                {
                    while (r.Read())
                    {
                        if (r.IsStartElement())
                        {
                            switch (r.Name)
                            {
                                case "spreadsheet version":
                                    break;

                                case "cell":
                                    break;

                                case "name":
                                    r.Read();
                                    if (!IsValidCellName(r.Value))
                                        throw new SpreadsheetReadWriteException("An invalid cell name was found in the file.");
                                    if (!r.Value.Equals(cellName))
                                        cellName = r.Value;
                                    else
                                        cellName = "";
                                    break;

                                case "contents":
                                    r.Read();
                                    contents = r.Value;
                                    if (cellName.Length != 0)
                                        SetContentsOfCell(Normalize(cellName), contents);
                                    break;
                            }
                            
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (e is CircularException)
                    throw new SpreadsheetReadWriteException("Circular dependencies were found in the file.");
                else if (e is FormulaFormatException)
                    throw new SpreadsheetReadWriteException("An invalid Formula was found in the file.");
                else if (e is SpreadsheetReadWriteException)
                    throw e;
                else
                    throw new SpreadsheetReadWriteException("An unknown error occurred reading the file.");
            }

            //treat the file as new
            changed = false;
        }

        /// <inheritdoc />
        public override bool Changed
        {
            get
            {
                return changed;
            }
            protected set
            {
                changed = value;
            }
        }

        //Still Needs Exceptions
        /// <inheritdoc />
        public override string GetSavedVersion(string filename)
        {
            try
            {
                using (XmlReader r = XmlReader.Create(new StringReader(filename)))
                {
                    while (r.Read())
                    {
                        if (r.Name.Equals("spreadsheet"))
                        {
                            return r["version"];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if(e is System.IO.FileNotFoundException)
                    throw new SpreadsheetReadWriteException("No such filename was found.");
            }
            throw new SpreadsheetReadWriteException("Unkown error occurred.");
        }

        //Still Needs Exceptions
        /// <inheritdoc />
        public override void Save(string filename)
        {
            try
            {
                using (XmlWriter w = XmlWriter.Create(filename))
                {
                    IEnumerator<Cell> cells = spreadsheet.Values.GetEnumerator();
                    Cell c;
                    w.WriteStartDocument();

                    w.WriteStartElement("spreadsheet");
                    w.WriteAttributeString("version", Version);

                    while (cells.MoveNext())
                    {
                        c = cells.Current;
                        w.WriteStartElement("cell");
                        w.WriteStartElement("name");
                        w.WriteString(c.Name);
                        w.WriteEndElement();
                        w.WriteStartElement("contents");
                        {
                            if (c.Contents.GetType() == typeof(string))
                                w.WriteString((string)c.Contents);
                            if (c.Contents.GetType() == typeof(double))
                                w.WriteString(((double)c.Contents).ToString());
                            if (c.Contents.GetType() == typeof(Formula))
                                w.WriteString("=" + ((Formula)c.Contents).ToString());
                        }
                        w.WriteEndElement();
                        w.WriteEndElement();
                    }
                    w.WriteEndElement();
                    w.WriteEndDocument();
                }
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                throw new SpreadsheetReadWriteException("Specified directory unknown.");
            }
            changed = false;
        }

        /// <inheritdoc />
        public override object GetCellValue(string name)
        {
            name = Normalize(name);
            // If name is null or invalid, throw an InvalidNameException.
            if (name == null || !IsValidCellName(name))
                throw new InvalidNameException();
            Cell c; //Cell object to store the the fetched cell
            object o = ""; //object to store value of c
            if (spreadsheet.TryGetValue(name, out c))
                o = c.Value;

            return o;
        }

        /// <inheritdoc />
        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            return spreadsheet.Keys; //The key of each pair is the name, return the set of keys.
        }

        /// <inheritdoc />
        public override object GetCellContents(string name)
        {
            name = Normalize(name);

            // If name is null or invalid, throw an InvalidNameException.
            if (name == null || !IsValidCellName(name))
                throw new InvalidNameException();
            Cell c; //Cell object to store the the fetched cell
            object o = ""; //object to store contents of c
            if (spreadsheet.TryGetValue(name, out c))
                o = c.Contents;

            return o;
        }

        /// <summary>
        /// This method is to support the Lookup Delagate for Formula.Evaluate(Func lookup).
        /// It looks up the value of a variable name (cell name).
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private double Lookup(string name)
        {
            name = Normalize(name);
            Cell c; //Cell object to store the the fetched cell

            if (spreadsheet.TryGetValue(name, out c))
                if (c.Value.GetType() == typeof(double))
                    return (double) c.Value;

            throw new ArgumentException();

        }

        /// <inheritdoc />
        public override ISet<string> SetContentsOfCell(string name, string content)
        {
            double d;
            name = Normalize(name);

            // If content is null, throws an ArgumentNullException.
            if (content == null)
                throw new ArgumentNullException();

            // Otherwise, if name is null or invalid, throws an InvalidNameException.
            if (name == null || !IsValidCellName(name))
                throw new InvalidNameException();

            // Otherwise, if content parses as a double, the contents of the named
            // cell becomes that double.
            if (double.TryParse(content, out d))
            {
                return SetCellContents(name, d);
            }

            //if content is a formula, the contents of the named cell becomes
            //that double, if no exception is thrown.
            if (content.StartsWith("="))
            {
                //create a formula based on content removing the "=" from the front.
                //This will throw a FormulaFormatException from the SpreadsheetUtilities 
                //if the string can't be parsed into a formula.
                Formula f = new Formula(content.Substring(1), IsValid, Normalize);
                
                //Now we'll attempt to set the contents of the named cell to be f.
                //If a circular dependency is found, a CircularException will be thrown.
                return SetCellContents(name, f);
            }

            return SetCellContents(name, content);
        }

        /// <inheritdoc />
        protected override ISet<string> SetCellContents(string name, double number)
        {
            name = Normalize(name);

            //create a new cell with the name and number.
            Cell c = new Cell(name, number);

            //if the spreadsheet contains this cell, remove it, then add it with the new number.
            spreadsheet.Remove(name);
            spreadsheet.Add(name, c);

            //this is the set including name and its dependents
            ISet<string> set = new HashSet<string>();

            set.Add(name); //add name to the set
            IEnumerable<string> e = dg.GetDependents(name);
            foreach (string s in e)
                set.Add(s); //add each dependent to the set

            IEnumerable<string> recalc = GetCellsToRecalculate(name);
            foreach (string s in recalc)
            {
                if (!s.Equals(name))
                {
                    Formula f = new Formula(GetCellContents(s).ToString(), IsValid, Normalize);
                    SetCellContents(s, f);
                }
            }

            changed = true;

            return set;
        }

        /// <inheritdoc />
        protected override ISet<string> SetCellContents(string name, string text)
        {
            name = Normalize(name);

            //create the cell with name and text
            Cell c = new Cell(name, text);

            //if the cell name exists, remove it and add the new cell
            if (spreadsheet.Remove(name))
                spreadsheet.Add(name, c);
            else
                spreadsheet.Add(name, c); //otherwise just add the new cell

            //the set that contains name and its dependents
            ISet<string> set = new HashSet<string>();

            set.Add(name); //add name to the set
            IEnumerable<string> e = dg.GetDependents(name);
            foreach (string s in e)
                set.Add(s); //add each dependent to the set

            IEnumerable<string> recalc = GetCellsToRecalculate(name);
            foreach (string s in recalc)
            {
                if (!s.Equals(name))
                {
                    Formula f = new Formula(GetCellContents(s).ToString(), IsValid, Normalize);
                    SetCellContents(s, f);
                }
            }

            changed = true;

            return set;
        }

        /// <inheritdoc />
        protected override ISet<string> SetCellContents(string name, Formula formula)
        {
            name = Normalize(name);

            //copy the original cell and its dependents if it exists
            Cell original;
            spreadsheet.TryGetValue(name, out original); //get the original cell if it exists and store it in original
            
            //store the dependents of the original cell if they exist
            List<string> originaldependents = dg.GetDependents(name).ToList();
            bool originalExisted = false; //track whether an original cell existed.
            //IEnumerable<string> changedVairables; //IEnumerable of all the changed variables

            //create a new cell with name, formula
            Cell c = new Cell(name, formula);

            //try to add the new cell to the spreadsheet
            try
            {
                //if it exists in the spreadsheet already, remove it
                //then add the new cell with the new contents
                //also remove the old dependencies
                if (spreadsheet.Remove(name))
                {
                    originalExisted = true; //flag that have replaced an original copy.
                    spreadsheet.Add(name, c); //add new cell
                    foreach (string d in originaldependents)
                        dg.RemoveDependency(name, d); //remove old dependencies
                }
                else
                    spreadsheet.Add(name, c); //add it anyway, if it didn't already exist

                //add the new dependencies
                foreach (string v in formula.GetVariables())
                    dg.AddDependency(name, v);
               
            }

            //if we get a circular exception, undo all the changes
            catch (CircularException ce)
            {
                //remove the new cell, and replace it with the old one if it existed.
                spreadsheet.Remove(name);
                if (originalExisted)
                {
                    spreadsheet.Add(name, original);
                    //restore the dependencies
                    foreach (string d in originaldependents)
                        dg.AddDependency(name, d);
                }
                //remove the dependencies
                foreach (string v in formula.GetVariables())
                    dg.RemoveDependency(name, v);
                //rethrow the Circular Exception
                throw ce;
            }

            //this is the set that will contain name, and all its dependents
            ISet<string> set = new HashSet<string>();

            c.Value = formula.Evaluate(Lookup);

            set.Add(name); //add name to the set
            foreach (string s in dg.GetDependees(name))
                set.Add(s); //add each dependent to the set

            //recalculate cells, check for Circular Dependencies
            IEnumerable<string> recalc = GetCellsToRecalculate(name);
            foreach (string s in recalc)
            {
                if (!s.Equals(name))
                {
                    Formula f = new Formula(GetCellContents(s).ToString(), IsValid, Normalize);
                    SetCellContents(s, f);
                }
            }

            changed = true;

            return set;
        }

        /// <inheritdoc />
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            name = Normalize(name);

            // If name is null, throw an ArgumentNullException.
            if (name == null)
                throw new ArgumentNullException();

            //if name isn't a valid cell name, throw an InvalidNameException.
            if (!IsValidCellName(name))
                throw new InvalidNameException();

            //The dependency tracks direct dependents.
            //return the set from GetDependents()
            return dg.GetDependees(name);
        }

        /// <summary>
        /// Private method to compute the validity of a cell name. The rules of a cell name are 
        /// described in the Spreadsheet class description. 
        /// 
        /// From Spreadsheet class description:
        /// A string is a cell name if and only if it consists of one or more letters,
        /// followed by one or more digits AND it satisfies the predicate IsValid.
        /// For example, "A15", "a15", "XY032", and "BC7" are cell names so long as they
        /// satisfy IsValid.  On the other hand, "Z", "X_", and "hello" are not cell names,
        /// regardless of IsValid.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private bool IsValidCellName(string n)
        {
            String namePattern = @"([a-zA-Z]+)([0-9]\d*)$";

            n = Normalize(n);

            if (Regex.IsMatch(n, namePattern) && IsValid(n))
                return true;
            else
                return false;
        }

    }

    /// <summary>
    /// A Cell is an object used for the Spreadsheet class that is given a name as a 
    /// string and contains a contents field as a string.  Contents is stored as string,
    /// but can be a string, double, or Formula object. In the near future, Cell will
    /// also contain a value field that will be represented by a string, double or
    /// FormulaError. The class has a getter and setter for name and contents.
    /// </summary>
    class Cell
    {
        //private fields
        private string name;
        private object contents;
        private object value;

        /// <summary>
        /// Constructor that takes the name and contents parameters as a string.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="c"></param>
        public Cell(string n, string c)
        {
            name = n;
            contents = c;
            value = c;
        }

        /// <summary>
        /// Constructor that takes the name as a string and the contents as a double.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="c"></param>
        public Cell(string n, double c)
        {
            name = n;
            contents = c;
            value = c;
        }

        /// <summary>
        /// Constructor that takes the name as a string and the contents as a Formula.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="c"></param>
        public Cell(string n, Formula c)
        {
            name = n;
            contents = c;
            //value = c.Evaluate(k=>7); //stub
        }

        /// <summary>
        /// getter/setter for Name field
        /// </summary>
        public String Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// getter and private setter for Contents field
        /// </summary>
        public Object Contents
        {
            get { return this.contents; }
            private set { this.contents = value; }
        }

        /// <summary>
        /// getter and for Contents field
        /// </summary>
        public Object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

    }
}

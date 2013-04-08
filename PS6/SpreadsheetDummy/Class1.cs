using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SS;
using SpreadsheetUtilities;

namespace SS
{
    public class Class1: AbstractSpreadsheet
    {
        public override bool Changed
        {
            get
            {
                throw new NotImplementedException();
            }
            protected set
            {
                throw new NotImplementedException();
            }
        }

        public override string GetSavedVersion(string filename)
        {
            throw new NotImplementedException();
        }

        public override void Save(string filename)
        {
            throw new NotImplementedException();
        }

        public override object GetCellValue(string name)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            throw new NotImplementedException();
        }

        public override object GetCellContents(string name)
        {
            throw new NotImplementedException();
        }

        public override ISet<string> SetContentsOfCell(string name, string content)
        {
            throw new NotImplementedException();
        }

        protected override ISet<string> SetCellContents(string name, double number)
        {
            throw new NotImplementedException();
        }

        protected override ISet<string> SetCellContents(string name, string text)
        {
            throw new NotImplementedException();
        }

        protected override ISet<string> SetCellContents(string name, SpreadsheetUtilities.Formula formula)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            throw new NotImplementedException();
        }
    }
}

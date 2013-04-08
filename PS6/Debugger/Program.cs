using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpreadsheetUtilities;
using SS;

namespace Debugger
{
    class Program
    {
        static void Main(string[] args)
        {
            Spreadsheet ss = new Spreadsheet();

            ss.SetContentsOfCell("A1", "=D1");
            Console.WriteLine(ss.GetCellValue("A1").ToString());
            ss.SetContentsOfCell("D1", "5");
            Console.WriteLine(ss.GetCellValue("D1").ToString());
            Console.WriteLine(ss.GetCellValue("A1").ToString());



            //IEnumerable<string> dependents = ss.SetContentsOfCell("C1", "=A1*2");
            //foreach(string s in dependents)
            //    Console.WriteLine(s);

            //string filePath = @"C:\Users\Spaldex\Documents\Visual Studio 2010\Projects\PS5\TestResults\Spaldex_SPALDEX-PC 2012-10-15 22_16_59\Out\testyMcTesty";
            //Func<string, bool> isValid = (s) => true;
            //Func<string, string> normalize = s => s.ToUpper();
            //string version = "default";
            //Spreadsheet target = new Spreadsheet(filePath, isValid, normalize, version);
            //Console.WriteLine(target.GetCellContents("A4"));


            //Spreadsheet sheet = new Spreadsheet();
            //sheet.SetContentsOfCell("A4", "5");
            //sheet.SetContentsOfCell("B4", "A4");
            //Console.WriteLine(((sheet.GetCellValue("B4")).ToString()));
            
            //sheet.SetContentsOfCell("B4", "=A4");
            //Console.WriteLine(((sheet.GetCellValue("B4")).ToString()));

            //sheet.SetContentsOfCell("C4", "=B4 * 2");
            //Console.WriteLine(((sheet.GetCellValue("C4")).ToString()));


            //Spreadsheet ss = new Spreadsheet();

            //ss.SetCellContents("A1", 5);
            //ss.SetCellContents("B1", new Formula("C1 + A1"));
            //ss.SetCellContents("X8", new Formula("C1*2"));
            //ISet<string> set = ss.SetCellContents("C1", 8);
            //foreach (string s in set)
            //    Console.WriteLine(s);

            //object o;
            //Console.WriteLine(o = ss.GetCellContents("A1").ToString());
            //ss.GetDDs("C1");
            
            //object o = new object();

            //Cell c = new Cell("A4", o);

            //Console.WriteLine(ss.IsValidCellName("A4"));
            //Console.WriteLine(ss.IsValidCellName("A04"));
            //Console.WriteLine(ss.IsValidCellName("A40"));
            //Console.WriteLine(ss.IsValidCellName("A4000a"));
            //Console.WriteLine(ss.IsValidCellName("4"));
            //Console.WriteLine(ss.IsValidCellName("A"));
            //Console.WriteLine(ss.IsValidCellName("text"));
            //Console.WriteLine(ss.IsValidCellName("this string has spaces is that valid 8"));
            //Console.WriteLine(ss.IsValidCellName("aaaaaaaaaaassssssssssssssssssssssssdddddddddddddfffffffffffffffff8888888888888882222222222222222222"));
            //Console.WriteLine(ss.IsValidCellName("ksssssssalskdjalkdj.?5"));

            //Spreadsheet target = new Spreadsheet();
            //string[] s = new string[5];
            //s[0] = "B52";
            //s[1] = "C43";
            //s[2] = "Z4";
            //s[3] = "XX2";
            //s[4] = "A1";

            //target.SetCellContents(s[0], 5);
            //target.SetCellContents(s[1], "w");
            //target.SetCellContents(s[3], 8);
            //target.SetCellContents(s[4], new Formula(s[0] + "+ 3"));

            //string name = string.Empty;
            //string text = string.Empty;
            //ISet<string> expected = new HashSet<string>();
            //ISet<string> actual;
            //actual = target.SetCellContents(s[2], new Formula(s[3] + "/ 2 + " + s[4]));

            //expected.Add("XX2");
            //expected.Add("A1");
            //expected.Add("B52");




            Console.Read();


        }
    }
}

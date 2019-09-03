using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CustFiles
{
    public class CustFileOne : IStdOut
    {
        public void FileToStdDT(ref DataTable StdTable)
        {
            //Read in lines from file #1.
            //foreach (string line in File.ReadLines("c:\\file1.csv"))
            //{
            //    try
            //    {
            //        verify line by regular express and add to string[] lines or convert only one line per loop
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Incorrect data format in customer file 1, line {0}, with error {1}", i, ex.Message);
            //    }
            //}

            //After read lines from file #1 and verifed we got: 
            string[] lines = {  "123|accountcode1,myacct1,2,01-01-2018,CD",
                                "123|accountcode2,myacct2,1,02-16-2018,US",
                                "123|accountcode3,myacct3,4,03-21-2018,CD",
                                "123|accountcode4,myacct4,3,05-22-2018,US" };

            try
            {
                var query = from readrow in lines
                            let tmprow = readrow.ToString().Split(',')
                            select new
                            {
                                AccountCode = tmprow[0].Split('|')[1],
                                Name = tmprow[1],
                                Type = tmprow[2] == "1" ? "Trading" :
                                       tmprow[2] == "2" ? "RRSP" :
                                       tmprow[2] == "3" ? "RESP" : "Fund",
                                OpenDate = DateTime.ParseExact(tmprow[3], "MM-dd-yyyy", null).ToString("yyyy-MM-dd"),
                                Currency = tmprow[4] == "CD" ? "CAD" : "USD"
                            };

                var result = query.ToList();

                foreach (var RowObj in result)
                {
                    StdTable.Rows.Add(RowObj.AccountCode, RowObj.Name, RowObj.Type, RowObj.OpenDate, RowObj.Currency);
                }
            }
            catch(Exception ex)
            {
                //Writelog("Catch error when transfer data in file 1# with error {0}:", ex.Message);
            }
        }
    }
}

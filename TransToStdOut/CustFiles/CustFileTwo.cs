using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CustFiles
{
    public class CustFileTwo : IStdOut
    {
        public void FileToStdDT(ref DataTable StdTable)
        {
            //Read in lines from file #2.
            //foreach (string line in File.ReadLines("c:\\file2.csv"))
            //{
            //    try
            //    {
            //        verify line by regular express and add to string[] lines convert only one line per loop
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Incorrect data format in customer file 2, line {0}, with error {1}", i, ex.Message);
            //    }
            //}

            //After read lines from file #2 and verifed we got: 
            string[] lines = {  "myacct5,RRSP,C,accountcode5",
                                "myacct6,RESP,U,accountcode6",
                                "myacct7,Fund,C,accountcode7",
                                "myacct8,Trading,U,accountcode8" };

            try
            {
                var query = from readrow in lines
                            let tmprow = readrow.ToString().Split(',')
                            select new
                            {
                                AccountCode = tmprow[3],
                                Name = tmprow[0],
                                Type = tmprow[1],
                                OpenDate = "0000-00-00",  //or with other default date value
                                Currency = tmprow[2] == "C" ? "CAD" : "USD"
                            };

                foreach (var RowObj in query)
                {
                    StdTable.Rows.Add(RowObj.AccountCode, RowObj.Name, RowObj.Type, RowObj.OpenDate, RowObj.Currency);
                }
            }
            catch (Exception ex)
            {
                //Writelog("Catch error when transfer data in file 2# with error {0}:", ex.Message);
            }
        }
    }
}

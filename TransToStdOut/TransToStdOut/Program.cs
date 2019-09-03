using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CustFiles;
using System.IO;

namespace TransToStdOut
{
    class Program
    {
        static void Main(string[] args)
        {           
            DataTable Stddt = new DataTable();        // Create standar output table to wirte into file or insert into DB
            Stddt.Columns.Add("AccountCode", typeof(string));
            Stddt.Columns.Add("Name", typeof(string));
            Stddt.Columns.Add("Type", typeof(string));
            Stddt.Columns.Add("OpenDate", typeof(string));
            Stddt.Columns.Add("Currency", typeof(string));

            var ObjStdOut = new List<IStdOut>
            {
                new CustFileOne(),
                new CustFileTwo()
            };

            foreach (IStdOut objstdout in ObjStdOut)
                objstdout.FileToStdDT(ref Stddt);

            string FileSavePath = @"C:\\Temp\\StdExportFile.csv";  //export to standar format file, or import to database table

            WriteToCSV(Stddt, FileSavePath);  

            Console.ReadLine();
        }

        static void WriteToCSV(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //Write headers   
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
    }
}

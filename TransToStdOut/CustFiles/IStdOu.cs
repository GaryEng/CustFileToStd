using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CustFiles
{
    public interface IStdOut
    {
        void FileToStdDT(ref DataTable StdTable);
    }
}

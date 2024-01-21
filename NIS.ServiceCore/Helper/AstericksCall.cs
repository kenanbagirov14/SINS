using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NIS.ServiceCore.Helper
{
    public class AstericksCall
    {
        public int CustomerNumber { get; set; }
        public string ContactNumber { get; set; }
        public int SourceCode { get; set; }
    }

    public class AsterikssCalls
    {
        public List<AstericksCall> calls { get; set; }
    }
}

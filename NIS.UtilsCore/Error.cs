using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NIS.UtilsCore.Enums;

namespace NIS.UtilsCore
{
    public class Error
    {
        #region Properties

        public OperationResultCode Type { get; set; }
        public string Code { get; set; }
        public string Text { get; set; }
        public int? CustomerNumber { get; set; }
        #endregion
    }
}

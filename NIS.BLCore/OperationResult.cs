using NIS.UtilsCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIS.BLCore
{
  public class LogicResult<TResult> where TResult : class
  {
    public List<Error> ErrorList { get; set; } = new List<Error>();
    public bool IsSuccess => !ErrorList.Any();
    public int? TotalCount { get; set; }
    public TResult Output { get; set; }
  }
}
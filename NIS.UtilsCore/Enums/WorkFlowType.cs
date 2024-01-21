using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIS.UtilsCore.Enums
{
    public enum WorkFlowType
    {
        CreateRequest = 1,
        CreateTask,
        ForwardTask,
        ConfirmTask
    }
    public enum WorkFlowCondition
    {
        StatusCompleted = 1
    }
}

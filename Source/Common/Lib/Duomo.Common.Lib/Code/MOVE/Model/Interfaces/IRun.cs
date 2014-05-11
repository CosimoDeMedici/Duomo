using System;
using System.Collections.Generic;


namespace Duomo.Common.Lib.MOVE
{
    public interface IRun
    {
        IOperationNode OperaionNode { get; set; }
        IModelHolder ModelHolder { get; set; }
        RunStatusEnumeration RunStatus { get; set; }
        IList<IRun> SubRuns { get; }
    }
}

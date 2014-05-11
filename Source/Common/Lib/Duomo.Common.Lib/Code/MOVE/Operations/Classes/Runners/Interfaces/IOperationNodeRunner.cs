using System;


namespace Duomo.Common.Lib.MOVE
{
    public interface IOperationNodeRunner
    {
        IRun Run(IOperationNode operationNode, IModelHolder modelHolder);
    }
}

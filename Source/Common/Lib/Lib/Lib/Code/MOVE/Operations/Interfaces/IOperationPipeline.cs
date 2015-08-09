using System;
using System.Collections.Generic;


namespace Duomo.Common.Lib.MOVE
{
    public interface IOperationPipeline : IOperationNode, IList<IOperationNode>
    {
        event OperationNodesListChangedEventHandler OperationNodesListChanged;


        IModelHolder GetModelHolderFromParentModelHolder(IModelHolder parentModelHolder);
        void SetModelHolderInParentModelHolder(IModelHolder modelHolder, IModelHolder parentModelHolder);
    }

    public interface IOperationPipeline<IModelType> : IOperationNode<IModelType>, IList<IOperationNode<IModelType>>, IOperationPipeline
        where IModelType : IModel
    {
        IOperationPipeline<IModelType> Register(IOperationNode<IModelType> operationNode);
        IOperationPipeline<IModelType> RegisterAt(IOperationNode<IModelType> operationNode, int index);
        IOperationPipeline<IModelType> RegisterBefore(IOperationNode<IModelType> operationNode, IOperationNode<IModelType> beforeOperationNode);
        IOperationPipeline<IModelType> RegisterAfter(IOperationNode<IModelType> operationNode, IOperationNode<IModelType> afterOperationNode);
        new void Remove(IOperationNode<IModelType> operationNode);
        new IOperationNode<IModelType> RemoveAt(int index);
        IOperationNode<IModelType> RemoveBefore(IOperationNode<IModelType> beforeOperationNode);
        IOperationNode<IModelType> RemoveAfter(IOperationNode<IModelType> afterOperationNode);
    }
}

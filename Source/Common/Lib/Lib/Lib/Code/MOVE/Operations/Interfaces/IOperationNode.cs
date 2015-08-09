using System;


namespace Duomo.Common.Lib.MOVE
{
    public interface IOperationNode
    {
        bool OnErrorBlock { get; }
    }


    public interface IOperationNode<IModelType> : IOperationNode
        where IModelType : IModel
    {
    }
}

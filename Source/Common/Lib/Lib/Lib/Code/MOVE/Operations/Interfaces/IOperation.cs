using System;


namespace Duomo.Common.Lib.MOVE
{
    public interface IOperation : IOperationNode
    {
        void Execute(IModelHolder modelHolder);
    }


    public interface IOperation<IModelType> : IOperationNode<IModelType>, IOperation
        where IModelType : IModel
    {
        void Execute(IModelHolder<IModelType> modelHolder);
    }
}

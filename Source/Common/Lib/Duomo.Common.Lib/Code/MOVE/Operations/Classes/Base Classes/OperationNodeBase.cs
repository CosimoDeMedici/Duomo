using System;


namespace Duomo.Common.Lib.MOVE
{
    public abstract class OperationNodeBase : IOperationNode
    {
        #region IOperationNode Members

        public virtual bool OnErrorBlock
        {
            get
            {
                return true;
            }
        }

        #endregion
    }


    public abstract class OperationNodeBase<IModelType> : OperationNodeBase, IOperationNode<IModelType>
        where IModelType : IModel
    {
    }
}

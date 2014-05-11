using System;


namespace Duomo.Common.Lib.MOVE
{
    public abstract class OperationBase<IModelType> : OperationNodeBase<IModelType>, IOperation<IModelType>
        where IModelType : IModel
    {
        #region IOperation<IModelType> Members

        public abstract void Execute(IModelHolder<IModelType> modelHolder);

        #endregion

        #region IOperation Members

        public void Execute(IModelHolder modelHolder)
        {
            Execute((IModelHolder<IModelType>)modelHolder); // TODO, create and use DuomoWrongTypeException.
        }

        #endregion
    }
}

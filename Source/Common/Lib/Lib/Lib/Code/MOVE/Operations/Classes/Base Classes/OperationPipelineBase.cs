using System;
using System.Collections;
using System.Collections.Generic;


namespace Duomo.Common.Lib.MOVE
{
    public abstract class OperationPipelineBase<IModelType, IParentModelType> : OperationNodeBase<IParentModelType>, IOperationPipeline<IModelType>
        where IModelType : IModel
        where IParentModelType : IModel
    {
        #region IOperationPipeline<IModelType> Members

        public IOperationPipeline<IModelType> Register(IOperationNode<IModelType> operationNode)
        {
            int indexToAddAt = zOperationNodes.Count;

            return RegisterAt(operationNode, indexToAddAt);
        }

        public IOperationPipeline<IModelType> RegisterAt(IOperationNode<IModelType> operationNode, int index)
        {
            zOperationNodes.Insert(index, operationNode);

            OnOperationNodesListChanged(operationNode, index, AddedOrRemovedEnumeration.Added);

            return this;
        }

        public IOperationPipeline<IModelType> RegisterBefore(IOperationNode<IModelType> operationNode, IOperationNode<IModelType> beforeOperationNode)
        {
            int indexToAddAt = zOperationNodes.IndexOf(beforeOperationNode) - 1;

            return RegisterAt(operationNode, indexToAddAt);
        }

        public IOperationPipeline<IModelType> RegisterAfter(IOperationNode<IModelType> operationNode, IOperationNode<IModelType> afterOperationNode)
        {
            int indexToAddAt = zOperationNodes.IndexOf(afterOperationNode) + 1;

            return RegisterAt(operationNode, indexToAddAt);
        }

        public void Remove(IOperationNode<IModelType> operationNode)
        {
            int indexToRemoveAt = zOperationNodes.IndexOf(operationNode);

            RemoveAt(indexToRemoveAt);
        }

        public IOperationNode<IModelType> RemoveAt(int index)
        {
            IOperationNode<IModelType> retValue = zOperationNodes[index];
            zOperationNodes.RemoveAt(index);

            OnOperationNodesListChanged(retValue, index, AddedOrRemovedEnumeration.Removed);

            return retValue;
        }

        public IOperationNode<IModelType> RemoveBefore(IOperationNode<IModelType> beforeOperationNode)
        {
            int indexToRemoveAt = zOperationNodes.IndexOf(beforeOperationNode) - 1;

            return RemoveAt(indexToRemoveAt);
        }

        public IOperationNode<IModelType> RemoveAfter(IOperationNode<IModelType> afterOperationNode)
        {
            int indexToRemoveAt = zOperationNodes.IndexOf(afterOperationNode) + 1;

            return RemoveAt(indexToRemoveAt);
        }

        #endregion

        #region IList<IOperationNode<IModelType>> Members

        int IList<IOperationNode<IModelType>>.IndexOf(IOperationNode<IModelType> item)
        {
            return zOperationNodes.IndexOf(item);
        }

        void IList<IOperationNode<IModelType>>.Insert(int index, IOperationNode<IModelType> item)
        {
            RegisterAt(item, index);
        }

        void IList<IOperationNode<IModelType>>.RemoveAt(int index)
        {
            RemoveAt(index);
        }

        public IOperationNode<IModelType> this[int index]
        {
            get
            {
                return zOperationNodes[index];
            }
            set
            {
                zOperationNodes[index] = value;
            }
        }

        #endregion

        #region ICollection<IOperationNode<IModelType>> Members

        void ICollection<IOperationNode<IModelType>>.Add(IOperationNode<IModelType> item)
        {
            Register(item);
        }

        void ICollection<IOperationNode<IModelType>>.Clear()
        {
            for (int iOperationNode = 0; iOperationNode < zOperationNodes.Count; iOperationNode++)
            {
                RemoveAt(0);
            }
        }

        bool ICollection<IOperationNode<IModelType>>.Contains(IOperationNode<IModelType> item)
        {
            return zOperationNodes.Contains(item);
        }

        void ICollection<IOperationNode<IModelType>>.CopyTo(IOperationNode<IModelType>[] array, int arrayIndex)
        {
            zOperationNodes.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return zOperationNodes.Count;
            }
        }

        bool ICollection<IOperationNode<IModelType>>.IsReadOnly
        {
            get
            {
                return ((ICollection<IOperationNode<IModelType>>)zOperationNodes).IsReadOnly;
            }
        }

        bool ICollection<IOperationNode<IModelType>>.Remove(IOperationNode<IModelType> item)
        {
            bool retValue = false;
            if (zOperationNodes.Contains(item))
            {
                Remove(item);

                retValue = zOperationNodes.Contains(item);
            }

            return retValue;
        }

        #endregion

        #region IEnumerable<IOperationNode<IModelType>> Members

        public IEnumerator<IOperationNode<IModelType>> GetEnumerator()
        {
            return zOperationNodes.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return zOperationNodes.GetEnumerator();
        }

        #endregion

        #region IOperationPipeline Members

        public event OperationNodesListChangedEventHandler OperationNodesListChanged;
        protected void OnOperationNodesListChanged(IOperationNode operationNode, int position, AddedOrRemovedEnumeration addedOrRemoved)
        {
            if (null != OperationNodesListChanged)
            {
                OperationNodesListChanged(this, new OperationsNodesListChangedEventArgs(operationNode, position, addedOrRemoved));
            }
        }

        public IModelHolder GetModelHolderFromParentModelHolder(IModelHolder parentModelHolder)
        {
            IModelHolder<IParentModelType> typedParentModelHolder = (IModelHolder<IParentModelType>)parentModelHolder; // TODO, create and use DuomoWrongTypeException.

            IModelType model = GetTypedModelFromParentModelHolder(typedParentModelHolder);

            IModelHolder<IModelType> retValue = new ModelHolder<IModelType>(model);

            return retValue;
        }

        public void SetModelHolderInParentModelHolder(IModelHolder modelHolder, IModelHolder parentModelHolder)
        {
            IModelHolder<IParentModelType> typedParentModelHolder = (IModelHolder<IParentModelType>)parentModelHolder; // TODO, create and use DuomoWrongypeException.

            IModelHolder<IModelType> typedModelHolder = (IModelHolder<IModelType>)modelHolder; // TODO, create and use DuomoWrongypeException.

            SetTypedModelHolderInParentModelHolder(typedModelHolder, typedParentModelHolder);
        }

        #endregion

        #region IList<IOperationNode> Members

        int IList<IOperationNode>.IndexOf(IOperationNode item)
        {
            IOperationNode<IModelType> typedItem = Convert(item);

            return zOperationNodes.IndexOf(typedItem);
        }

        void IList<IOperationNode>.Insert(int index, IOperationNode item)
        {
            IOperationNode<IModelType> typedItem = Convert(item);

            RegisterAt(typedItem, index);
        }

        void IList<IOperationNode>.RemoveAt(int index)
        {
            RemoveAt(index);
        }

        IOperationNode IList<IOperationNode>.this[int index]
        {
            get
            {
                return this[index];
            }
            set
            {
                IOperationNode<IModelType> typedValue = Convert(value);

                zOperationNodes[index] = typedValue;
            }
        }

        #endregion

        #region ICollection<IOperationNode> Members

        void ICollection<IOperationNode>.Add(IOperationNode item)
        {
            IOperationNode<IModelType> typedItem = Convert(item);

            Register(typedItem);
        }

        void ICollection<IOperationNode>.Clear()
        {
            ((ICollection<IOperationNode<IModelType>>)this).Clear();
        }

        bool ICollection<IOperationNode>.Contains(IOperationNode item)
        {
            IOperationNode<IModelType> typedItem = Convert(item);

            return zOperationNodes.Contains(typedItem);
        }

        void ICollection<IOperationNode>.CopyTo(IOperationNode[] array, int arrayIndex)
        {
            ((ICollection<IOperationNode>)zOperationNodes).CopyTo(array, arrayIndex);
        }

        bool ICollection<IOperationNode>.IsReadOnly
        {
            get
            {
                return ((ICollection<IOperationNode<IModelType>>)this).IsReadOnly;
            }
        }

        bool ICollection<IOperationNode>.Remove(IOperationNode item)
        {
            IOperationNode<IModelType> typedItem = Convert(item);

            return ((ICollection<IOperationNode<IModelType>>)this).Remove(typedItem);
        }

        #endregion

        #region IEnumerable<IOperationNode> Members

        IEnumerator<IOperationNode> IEnumerable<IOperationNode>.GetEnumerator()
        {
            return zOperationNodes.GetEnumerator();
        }

        #endregion


        protected IList<IOperationNode<IModelType>> zOperationNodes;


        public OperationPipelineBase()
        {
            zOperationNodes = new List<IOperationNode<IModelType>>();

            RegisterOperations();
        }

        protected abstract void RegisterOperations();

        public abstract IModelType GetTypedModelFromParentModelHolder(IModelHolder<IParentModelType> parentModelHolder);

        public abstract void SetTypedModelHolderInParentModelHolder(IModelHolder<IModelType> modelHolder, IModelHolder<IParentModelType> parentModelHolder);

        protected virtual IOperationNode<IModelType> Convert(IOperationNode operationNode)
        {
            return (IOperationNode<IModelType>)operationNode; // TODO, create and use DuomoWrongTypeException.
        }
    }
}

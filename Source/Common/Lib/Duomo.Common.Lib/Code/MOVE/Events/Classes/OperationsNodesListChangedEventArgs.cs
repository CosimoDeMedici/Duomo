using System;


namespace Duomo.Common.Lib.MOVE
{
    public delegate void OperationNodesListChangedEventHandler(object sender, OperationsNodesListChangedEventArgs e);


    [Serializable]
    public class OperationsNodesListChangedEventArgs : EventArgs
    {
        public IOperationNode OperationNode { get; protected set; }
        public int Position { get; protected set; }
        public AddedOrRemovedEnumeration AddedOrRemoved { get; protected set; }


        public OperationsNodesListChangedEventArgs(IOperationNode operationNode, int position, AddedOrRemovedEnumeration addedOrRemoved)
        {
            OperationNode = operationNode;
            Position = position;
            AddedOrRemoved = addedOrRemoved;
        }
    }
}

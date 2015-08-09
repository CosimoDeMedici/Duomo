using System;


namespace Duomo.Common.Lib.MOVE
{
    public delegate void InstanceChangedEventHandler(object sender, InstanceChangedEventArgs e);


    [Serializable]
    public class InstanceChangedEventArgs : EventArgs
    {
        public IModel PriorInstance { get; protected set; }
        public IModel CurrentInstance { get; protected set; }


        public InstanceChangedEventArgs(IModel priorInstance, IModel currentInstance)
        {
            PriorInstance = priorInstance;
            CurrentInstance = currentInstance;
        }
    }
}

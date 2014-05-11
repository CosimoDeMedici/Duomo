using System;


namespace Duomo.Common.Lib.MOVE
{
    public interface IModelHolder
    {
        event InstanceChangedEventHandler InstanceChanged;


        IModel Instance { get; set; }
    }


    public interface IModelHolder<IModelType> : IModelHolder
        where IModelType : IModel
    {
        new IModelType Instance { get; set; }
    }
}

using System;


namespace Duomo.Common.Lib.MOVE
{
    public class ModelHolder<IModelType> : IModelHolder<IModelType>
        where IModelType : IModel
    {
        #region IModelHolder<IModelType> Members

        private IModelType zInstance;
        public IModelType Instance
        {
            get
            {
                return zInstance;
            }
            set
            {
                if (null != zInstance)
                {
                    if (!zInstance.Equals(value)) // Reference equals.
                    {
                        // Only fire InstanceChanged if the instance has actually changed.
                        IModelType priorInstance = zInstance;

                        zInstance = value;

                        OnInstanceChanged(priorInstance, zInstance);
                    }
                }
                else
                {
                    zInstance = value;
                }
            }
        }

        #endregion

        #region IModelHolder Members

        public event InstanceChangedEventHandler InstanceChanged;
        protected void OnInstanceChanged(IModelType priorInstance, IModelType currentInstance)
        {
            if (null != InstanceChanged)
            {
                InstanceChanged(this, new InstanceChangedEventArgs(priorInstance, currentInstance));
            }
        }

        IModel IModelHolder.Instance
        {
            get
            {
                return Instance;
            }
            set
            {
                Instance = (IModelType)value; // TODO, add exception if value is not of the right type. DuomoWrongTypeException.
            }
        }

        #endregion


        public ModelHolder()
        {
        }

        public ModelHolder(IModelType instance)
        {
            zInstance = instance;
        }
    }
}

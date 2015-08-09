using System;


namespace Duomo.Common.Lib
{
    public class LabeledValueBase<ValueType> : ILabeledValue<ValueType>
    {
        #region ILabeledValue<ValueType> Members

        public ValueType Value { get; set; }

        #endregion

        #region ILabeledValue Members

        public string Name { get; set; }
        public string Description { get; set; }
        object ILabeledValue.Value
        {
            get
            {
                return Value;
            }
            set
            {
                Value = (ValueType)value;
            }
        }

        #endregion


        public LabeledValueBase()
        {
        }

        public LabeledValueBase(ValueType value)
        {
            Value = value;
        }
    }
}

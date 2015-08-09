using System;


namespace Duomo.Common.Lib
{
    public interface ILabeledValue : ILabeled
    {
        object Value { get; set; }
    }

    public interface ILabeledValue<ValueType> : ILabeledValue
    {
        new ValueType Value { get; set; }
    }
}

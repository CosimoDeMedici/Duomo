using System;


namespace Duomo.Common.Lib.Objects
{
    public interface ILabeledValue
    {
        string Name { get; set; }
        string Description { get; set; }
        object Value { get; set; }
    }

    public interface ILabeledValue<ValueType> : ILabeledValue
    {
        new ValueType Value { get; set; }
    }
}

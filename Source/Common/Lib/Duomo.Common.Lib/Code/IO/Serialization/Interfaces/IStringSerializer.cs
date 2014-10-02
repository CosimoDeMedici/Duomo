using System;


namespace Duomo.Common.Lib.IO.Serialization
{
    public interface IStringSerializer<T>
    {
        string SerializeToStringInstance(T value);
        T DeserializeFromStringInstance(string str);
    }
}

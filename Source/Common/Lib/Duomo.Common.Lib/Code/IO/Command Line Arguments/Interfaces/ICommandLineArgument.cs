using System;

using Duomo.Common.Lib.Objects;


namespace Duomo.Common.Lib.IO
{
    public interface ICommandLineArgument : ILabeledValue<string>
    {
        Type DataType { get; }
        Action<ICommandLineArgument> SetPropertyFromString { get; }
    }
}

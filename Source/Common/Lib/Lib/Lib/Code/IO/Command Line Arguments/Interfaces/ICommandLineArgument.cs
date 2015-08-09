using System;

using Duomo.Common.Lib;


namespace Duomo.Common.Lib.IO
{
    public interface ICommandLineArgument : ILabeledValue<string>
    {
        Type DataType { get; }
        Action<ICommandLineArgument> SetPropertyFromString { get; }
    }
}

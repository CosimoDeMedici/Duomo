using System;


namespace Duomo.Common.Lib.IO
{
    public interface IDataArgument : ICommandLineArgument
    {
        bool Optional { get; }
        int Ordinal { get; }
    }
}

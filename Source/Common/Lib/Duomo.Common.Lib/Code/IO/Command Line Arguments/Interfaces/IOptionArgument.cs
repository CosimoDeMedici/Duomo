using System;


namespace Duomo.Common.Lib.IO
{
    public interface IOptionArgument : ICommandLineArgument
    {
        string Flag { get; }
    }
}

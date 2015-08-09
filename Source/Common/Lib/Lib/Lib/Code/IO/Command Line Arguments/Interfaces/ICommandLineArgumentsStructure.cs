using System;
using System.Collections.Generic;


namespace Duomo.Common.Lib.IO
{
    public interface ICommandLineArgumentsStructure
    {
        string ProgramName { get; }
        string Usage { get; }
        IList<ICommandLineArgument> Arguments { get; }


        void Parse(IList<string> arguments);
        void Parse(string[] arguments);
    }
}

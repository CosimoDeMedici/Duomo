using System;
using System.Collections.Generic;

using Duomo.Common.Lib.IO;


namespace Duomo.Common.Gunther.Lib
{
    public class GuntherCommandLineArgumentsStructure : CommandLineArgumentsStructureBase
    {
        public string ConfigurationFileRootedPath { get; set; }


        public GuntherCommandLineArgumentsStructure(string[] args)
            : base("Gunther Scheduler", args)
        {
        }

        protected override IList<ICommandLineArgument> GetArgumentDefinitions()
        {
            List<ICommandLineArgument> retValue = new List<ICommandLineArgument>();

            retValue.Add(new OptionArgument("ConfigFileRootedPath", "Configuration file roouted path.", typeof(string), "configFileRootedPath", (x) => this.ConfigurationFileRootedPath = x.Value));

            return retValue;
        }
    }
}
